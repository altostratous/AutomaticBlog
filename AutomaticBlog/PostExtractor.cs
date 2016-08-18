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
                NReadability.WebTranscodingInput input = new NReadability.WebTranscodingInput(url);

                string page = new NReadability.NReadabilityWebTranscoder().Transcode(input).ExtractedContent;

                HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(page);

                //var title = doc.DocumentNode.SelectSingleNode("//title").InnerText;
                //var imgUrl = doc.DocumentNode.SelectSingleNode("//meta[@property='og:image']").Attributes["content"].Value;
                Posts[counter].Content = doc.DocumentNode.SelectSingleNode("//div[@id='readInner']").InnerHtml;

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
