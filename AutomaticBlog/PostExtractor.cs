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
                Posts.Add(new Post()
                {
                    Abstract = item.Description,
                    Title = item.Title
                });
            }
        }
        public void Process()
        {
            List<string> htmls = new List<string>();
            foreach(string url in urls)
            {
                htmls.Add(load(url));
            }

            //int minlength = htmls.Min(item => { return item.Length; });
            //List<bool> matches = new List<bool>();
            //for(int i = 0; i < minlength; i++)
            //{
            //    bool test = true;
            //    for(int j = 1; j < htmls.Count; j++)
            //    {
            //        if(htmls[j][i]!=htmls[j - 1][i])
            //        {
            //            test = false;
            //            break;
            //        }
            //    }
            //    matches.Add(test);
            //}
            //for(int i = 1; i < matches.Count - 1; i++)
            //{
            //    if((matches[i - 1])&&(matches[i + 1]))
            //    {
            //        matches[i] = true;
            //    }
            //}
            //int matchesCount = 0;
            //foreach(bool match in matches)
            //{
            //    if (!match)
            //        break;
            //    matchesCount++;
            //}

            //for(int i = 0; i < htmls.Count; i++)
            //{
            //    Posts[i].Content = htmls[i].Substring(matchesCount);
            //}
        }

        private string load(string url)
        {
            WebRequest request = HttpWebRequest.Create(url);
            request.Method = "GET";
            return new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
        }
    }
}
