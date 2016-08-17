using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using FrontEndAutomation;
using System.Xml;

namespace AutomaticBlog
{
    public partial class Main : Form
    {
        XulFxExecutor executor;
        Dictionary<string, string> feeds;
        Dictionary<string, Blog> blogs;
        Scope scope;

        public Main()
        {
            Gecko.Xpcom.Initialize(Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) , "xulrunner"));
            InitializeComponent();
            scope = new Scope(null);
            feeds = new Dictionary<string, string>();
            blogs = new Dictionary<string, Blog>();
            loadConfiguration("Config.xml");
        }

        private void loadConfiguration(string confFileName)
        {
            feedsCheckedListBox.Items.Clear();
            feeds = new Dictionary<string, string>();
            blogsCheckListBox.Items.Clear();
            XmlDocument doc = new XmlDocument();
            doc.Load(confFileName);
            XmlNodeList feedNodes =  doc.GetElementsByTagName("feed");
            foreach(XmlNode node in feedNodes)
            {
                string url = node["url"].InnerText;
                string script = node["script"].InnerText;
                feeds.Add(url, script);
                feedsCheckedListBox.Items.Add(url);
            }

            blogs = new Dictionary<string, Blog>();
            XmlNodeList blogNodes = doc.GetElementsByTagName("blog");
            foreach(XmlNode node in blogNodes)
            {
                blogs.Add(node["url"].InnerText, new Blog()
                {
                    LoginScript = node["login"].InnerText,
                    PostScript = node["post"].InnerText,
                    Username = node["username"].InnerText,
                    Password = node["password"].InnerText
                });
                blogsCheckListBox.Items.Add(node["url"].InnerText);
            }
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            loadConfiguration("Config.xml");
        }
    }
}
