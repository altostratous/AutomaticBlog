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
        public PostExtractor()
        {
            Posts = new List<Post>();
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
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(item.Description);
                Posts.Add(new Post()
                {
                    
                    Abstract = doc.DocumentNode.InnerText,
                    Title = item.Title
                });
            }
        }
        public void Process()
        {
            int counter = 0;
            foreach (string url in urls)
            {
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(load(url));
                IEnumerable<HtmlNode> nodes = doc.DocumentNode.DescendantsAndSelf();
                List<HtmlNode> goodNodes = new List<HtmlNode>();
                foreach(HtmlNode node in nodes)
                {
                    if (node.InnerText.Contains(Posts[counter].Abstract.Substring(0, 20)))
                    {
                        goodNodes.Add(node);
                    }
                }
                for(int i = goodNodes.Count - 1; i >=0; i--)
                {
                    bool test = false;
                    IEnumerable<HtmlNode> descendants = goodNodes[i].Descendants();
                    foreach (HtmlNode node in goodNodes)
                    {
                        if(descendants.Contains(node))
                        {
                            test = true;
                            break;
                        }
                    }
                    if (test)
                    {
                        goodNodes.RemoveAt(i);
                    }
                }
                if(goodNodes.Count == 0)
                {
                    Posts[counter].Content = "AutoBLog Could not extract post content.";
                }else
                {
                    Posts[counter].Content = goodNodes.First().OuterHtml;
                }
                counter++;
            }   
        }

        private string load(string url)
        {
            WebRequest request = HttpWebRequest.Create(url);
            request.Method = "GET";
            return new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
        }
    }
}
