using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace AutomaticBlog
{
    public class ConfigurationEditor
    {
        public string ConfigFilePath { get; set; }
        public string ScriptsDirectory { get; set; }
        private XmlDocument document = new XmlDocument();
        public Dictionary<string, Blog>Blogs { get; set; }
        public List<string> Feeds { get; set; }
        public ConfigurationEditor(string configFilePath, string scriptsDirectory)
        {
            ConfigFilePath = configFilePath;
            ScriptsDirectory = scriptsDirectory;
        }
        public void Save()
        {
            document = new XmlDocument();
            document.LoadXml("<config><feeds></feeds><blogs></blogs></config>");
            XmlNode blogsElement = document.GetElementsByTagName("blogs")[0];
            foreach(string url in Blogs.Keys)
            {
                XmlElement blogNode = document.CreateElement("blog");
                blogNode.AppendChild(document.CreateElement("url"));
                blogNode.AppendChild(document.CreateElement("username"));
                blogNode.AppendChild(document.CreateElement("password"));
                blogNode.AppendChild(document.CreateElement("login"));
                blogNode.AppendChild(document.CreateElement("post"));
                blogNode["url"].InnerText = Blogs[url].Url;
                blogNode["username"].InnerText = Blogs[url].Username;
                blogNode["password"].InnerText = Blogs[url].Password;
                blogNode["login"].InnerText = Blogs[url].LoginScript;
                blogNode["post"].InnerText = Blogs[url].PostScript;
                blogsElement.AppendChild(blogNode);
            }
            XmlNode feedsElement = document.GetElementsByTagName("feeds")[0];
            foreach (string url in Feeds)
            {
                XmlElement feedNode = document.CreateElement("feed");
                feedNode.AppendChild(document.CreateElement("url"));
                feedNode["url"].InnerText = url;
                feedsElement.AppendChild(feedNode);
            }
            document.Save(ConfigFilePath);
        }
        public void Load()
        {
            Feeds = new List<string>();
            document.Load(ConfigFilePath);
            Blogs = new Dictionary<string, Blog>();
            XmlNodeList blogNodes = document.GetElementsByTagName("blog");
            foreach (XmlNode node in blogNodes)
            {
                Blogs.Add(node["url"].InnerText, new Blog()
                {
                    LoginScript = node["login"].InnerText,
                    PostScript = node["post"].InnerText,
                    Username = node["username"].InnerText,
                    Password = node["password"].InnerText,
                    Url = node["url"].InnerText
                });
            }
            XmlNodeList feedNodes = document.GetElementsByTagName("feed");
            foreach (XmlNode node in feedNodes)
            {
                string url = node["url"].InnerText;
                Feeds.Add(url);
            }
        }
    }
}
