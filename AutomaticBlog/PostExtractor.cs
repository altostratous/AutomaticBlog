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
                NReadability.WebTranscodingInput input = null;
                if (url.EndsWith(".rss"))
                    input = new NReadability.WebTranscodingInput(url.Substring(0, url.Length - 4));
                else
                    input = new NReadability.WebTranscodingInput(url);

                string page = new NReadability.NReadabilityWebTranscoder().Transcode(input).ExtractedContent;

                HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(page);

                // removing repeatetive title
                HtmlNode node = doc.DocumentNode.SelectSingleNode("//div[@id='readInner']");
                if(node.FirstChild.Name.StartsWith("h") && node.FirstChild.Name.Length == 2)
                {
                    node.RemoveChild(node.FirstChild);
                }

                //var title = doc.DocumentNode.SelectSingleNode("//title").InnerText;
                //var imgUrl = doc.DocumentNode.SelectSingleNode("//meta[@property='og:image']").Attributes["content"].Value;
                Posts[counter].Content = doc.DocumentNode.SelectSingleNode("//div[@id='readInner']").InnerHtml;

                counter++;
            }   

            // Continue extractor ... 
            foreach(Post post in Posts)
            {
                post.ReadMore = extractReadMore(post);
            }
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
