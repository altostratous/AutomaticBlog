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
        public ConfigurationEditor(string configFilePath, string scriptsDirectory)
        {
            ConfigFilePath = configFilePath;
            ScriptsDirectory = scriptsDirectory;
        }
        public void Save()
        {
            document.Save(ConfigFilePath);
        }
        public void Load()
        {
            document.Load(ConfigFilePath);
        }
        public void SetBlog(string key, Blog blog)
        {
            List<Blog> blogs = new List<Blog>();
            XmlNodeList blogNodes = document.GetElementsByTagName("blog");
            XmlNode theBlog = null;
            foreach (XmlNode node in blogNodes)
            {
                if (node["url"].InnerText == key)
                    theBlog = node;
            }
            XmlNode blogNode;
            if (theBlog == null)
            {
                blogNode = document.CreateElement("blog");
            }
            else
            {
                blogNode = theBlog;
            }
            //blogNode["url"] = 
        }
        public void SetFeed(string key, string feed)
        {

        }
        public Blog GetBlog(string key)
        {
        }
        public string GetFeed(string key)
        {

        }
    }
}
