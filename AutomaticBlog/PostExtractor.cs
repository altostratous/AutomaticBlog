using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using HtmlAgilityPack;

namespace AutomaticBlog
{
    public class PostExtractor
    {
        public char[] Seperators { get; set; }
        public PostExtractor()
        {
            Posts = new List<Post>();
            Seperators = new char[] { ' ', '.', '،', '؟', '!', '؛', '\n' };
        }
        public List<string> Urls {
            get { return urls; }
            set { urls = value; } }
        public List<Post> Posts { get; set; }
        private List<string> urls = new List<string>();
        private List<RssItem> rssItems = new List<RssItem>();
        public List<RssItem> RssItems { get { return rssItems; } set { rssItems = value; } }
        public void AddUrl(string url)
        {
            urls.Add(url);
        }
        public void AddUrlsFromRssFeed(string rssFeed)
        {
            RssReader reader = new RssReader(rssFeed);
            
            reader.Execute();
            foreach(RssItem item in reader.Items)
            {
                AddUrl(item.Link);
                //HtmlDocument doc = new HtmlDocument();
                //doc.LoadHtml(item.Description);
                Posts.Add(new Post()
                {
                    
                    //Abstract = doc.DocumentNode.InnerText,
                    Abstract = item.Description,
                    Title = item.Title,
                    Link = item.Link,
                    Date = item.Date
                });
            }
        }
        public void Process()
        {
            int counter = 0;
            foreach (string url in urls)
            {
                //NReadability.WebTranscodingInput input = null;
                //if (url.EndsWith(".rss"))
                //    input = new NReadability.WebTranscodingInput(url.Substring(0, url.Length - 4));
                //else
                //    input = new NReadability.WebTranscodingInput(url);
                string address = url;
                if (url.EndsWith(".rss"))
                    address = url.Substring(0, url.Length - 4);

                //string page = new NReadability.NReadabilityWebTranscoder().Transcode(input).ExtractedContent;

                //HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                //doc.LoadHtml(page);

                //// removing repeatetive title
                //HtmlNode node = doc.DocumentNode.SelectSingleNode("//div[@id='readInner']");
                //if(node.FirstChild.Name.StartsWith("h") && node.FirstChild.Name.Length == 2)
                //{
                //    node.RemoveChild(node.FirstChild);
                //}

                //var title = doc.DocumentNode.SelectSingleNode("//title").InnerText;
                //var imgUrl = doc.DocumentNode.SelectSingleNode("//meta[@property='og:image']").Attributes["content"].Value;
                //Posts[counter].Content = doc.DocumentNode.SelectSingleNode("//div[@id='readInner']").InnerHtml;
                Posts[counter].Content = new WebClient() { Encoding = Encoding.UTF8 }.DownloadString(address);
                counter++;
            }
            Tuple<string, List<string>> roi = getROI();

            foreach (Post post in Posts)
            {
                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(post.Content);
                document.LoadHtml(document.DocumentNode.SelectSingleNode(roi.Item1).OuterHtml);
                foreach (string xpath in roi.Item2)
                {
                    HtmlNode node = document.DocumentNode.SelectSingleNode(xpath);

                    if (node != null) gitRemove(node);
                }
                //document.LoadHtml(document.DocumentNode.OuterHtml);
                //foreach(HtmlNode node in  document.DocumentNode.SelectNodes("//form"))
                //{
                //    gitRemove(node);

                makeReferencesAbsolute(document, post.Link);
                post.Content = document.DocumentNode.OuterHtml;
                post.Content = removeForms(post.Content);
            }

            // Continue extractor ... 
            foreach(Post post in Posts)
            {
                post.ReadMore = extractReadMore(post);
            }
        }

        private void makeReferencesAbsolute(HtmlDocument document, string baseAddress)
        {
            foreach (HtmlNode node in document.DocumentNode.SelectNodes("//*"))
            {
                foreach (HtmlAttribute attr in node.Attributes)
                {
                    if (attr.Name == "src" || attr.Name == "href")
                    {
                        attr.Value = getAbsoluteAddress(baseAddress, attr.Value);
                    }
                }
            }
        }

        private string getAbsoluteAddress(string baseAddress, string value)
        {
            return new Uri(new Uri(baseAddress), value).ToString();
        }

        private string removeForms(string content)
        {
            if (!content.Contains("<form")) return content;
            int start = content.IndexOf("<form");
            int end = content.LastIndexOf("</form>");
            return content.Remove(start, end - start + 7);
        }

        private void gitRemove(HtmlNode node)
        {
            HtmlNode parent = node.ParentNode;
            if (parent.ChildNodes.Count == 1)
            {
                gitRemove(node.ParentNode);
            }
            parent.RemoveChild(node);
        }

        private Tuple<string, List<string>> getROI()
        {
            List<HtmlDocument> docs = new List<HtmlDocument>();
            Dictionary<string, List<string>> xpaths = new Dictionary<string, List<string>>();

            foreach (Post post in Posts)
            {
                HtmlDocument doc = new HtmlDocument();
                docs.Add(doc);
                doc.LoadHtml(post.Content);
                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//*"))
                {
                    foreach (HtmlAttribute atrib in node.Attributes)
                    {
                        atrib.Value = "";
                    }
                }
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//body//*[not(*)]");
                foreach (HtmlNode node in nodes)
                {
                    if (!xpaths.ContainsKey(node.XPath))
                    {
                        xpaths.Add(node.XPath, new List<string>());
                    }
                    xpaths[node.XPath].Add(node.OuterHtml);
                }
            }
            Dictionary<string, int> distances = new Dictionary<string, int>();
            foreach (string xpath in xpaths.Keys)
            {
                if (!distances.Keys.Contains(xpath))
                {
                    distances.Add(xpath, 0);
                }
                foreach (string content in xpaths[xpath])
                {
                    
                    foreach (string secondContent in xpaths[xpath])
                    {
                        if (secondContent.Length == content.Length)
                            continue;
                        distances[xpath] += Math.Abs(secondContent.Length - content.Length);
                    }
                }
            }
            List<string> changedXpaths = new List<string>();
            List<string> goodXpaths = new List<string>();
            foreach (string xpath in distances.Keys)
            {
                if (distances[xpath] == 0)
                    goodXpaths.Add(xpath);
                else
                {
                    changedXpaths.Add(xpath);
                }
            }
            string result = "";
            string shortest = changedXpaths.First();
            foreach (string changedXpath in changedXpaths)
            {
                if (changedXpath .Length < shortest.Length)
                    shortest = changedXpath;
            }
            for (int i = 0; i < shortest.Length; i++)
            {
                bool test = true;
                foreach (string goodXpath in changedXpaths)
                {
                    if (goodXpath[i] != shortest[i])
                    {
                        test = false;
                    }
                }
                if (!test)
                    break;
                result += shortest[i];
            }
            result = result.Substring(0, result.LastIndexOf('/'));
            return new Tuple<string, List<string>>(result, goodXpaths);
        }


        private static int CalcLevenshteinDistance(string a, string b)
        {
            if (String.IsNullOrEmpty(a) || String.IsNullOrEmpty(b)) return 0;

            int lengthA = a.Length;
            int lengthB = b.Length;
            var distances = new int[lengthA + 1, lengthB + 1];
            for (int i = 0; i <= lengthA; distances[i, 0] = i++) ;
            for (int j = 0; j <= lengthB; distances[0, j] = j++) ;

            for (int i = 1; i <= lengthA; i++)
                for (int j = 1; j <= lengthB; j++)
                {
                    int cost = b[j - 1] == a[i - 1] ? 0 : 1;
                    distances[i, j] = Math.Min
                        (
                        Math.Min(distances[i - 1, j] + 1, distances[i, j - 1] + 1),
                        distances[i - 1, j - 1] + cost
                        );
                }
            return distances[lengthA, lengthB];
        }

        private string extractReadMore(Post post)
        {
            string[] wordsInAbstract = post.Abstract.Split(Seperators);
            string res = post.Content;
            foreach(string wordInAbstract in wordsInAbstract)
            {
                if (res.Contains(wordInAbstract))
                {
                    int lengthAdder = 0;
                    if(Seperators.Contains( res[res.IndexOf(wordInAbstract) + wordInAbstract.Length]))
                    {
                        lengthAdder = 1;
                    }
                    res = res.Remove(res.IndexOf(wordInAbstract), wordInAbstract.Length + lengthAdder);
                }
            }
            while(res.Length > 0)
            {
                if (Seperators.Contains(res[0]))
                {
                    res = res.Substring(1);
                }
                else
                {
                    break;
                }
            }
            return res;
        }

        private string load(string url)
        {
            WebRequest request = HttpWebRequest.Create(url);
            request.Method = "GET";
            return new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
        }
    }
}
