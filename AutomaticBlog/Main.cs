﻿using System;
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
        List<Post> posts;
        int fetchSelectedFeedsCount = 0;
        int postsToPostCount = 0;

        public Main()
        {
            Gecko.Xpcom.Initialize(Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) , "xulrunner"));
            InitializeComponent();
            executor = new XulFxExecutor(scope, null, webView.Window);
            scope = new Scope(null);
            feeds = new Dictionary<string, string>();
            blogs = new Dictionary<string, Blog>();
            posts = new List<Post>();
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

        
        private void fetchFeedsButton_Click(object sender, EventArgs e)
        {
            fetchBackgroundWorker.RunWorkerAsync();
            fetchSelectedFeedsCount = feedsCheckedListBox.CheckedItems.Count;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (fetchBackgroundWorker.IsBusy)
            {
                fetchBackgroundWorker.CancelAsync();
            }
            if(postBackgroundWorker.IsBusy )
            {
                postBackgroundWorker.CancelAsync();
            }
            log("Cancel.");
        }

        private void log(string toLog)
        {
            logListBox.BeginInvoke(new Action(delegate {
                logListBox.Items.Add(toLog);
            }));
        }

        private void fetchBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            fetchBackgroundWorker.ReportProgress(0);   
            int counter = 0;
            int overalCounter = 0;
            foreach (string feed in feeds.Keys)
            {
                if (feedsCheckedListBox.CheckedIndices.Contains(overalCounter))
                {
                    PostExtractor postExtractor = new PostExtractor();
                    if (fetchBackgroundWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    log("Fetching urls from feed: " + feed);
                    postExtractor.AddUrlsFromRssFeed(feed);
                    if (fetchBackgroundWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    log("Processing posts from feed: " + feed);
                    postExtractor.Process();
                    foreach (Post post in postExtractor.Posts)
                    {
                        posts.Add(post);
                    }
                    log("Added posts to memory from " + feed);
                    counter++;
                    fetchBackgroundWorker.ReportProgress(100 * counter / fetchSelectedFeedsCount);
                }
                overalCounter++;
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.BeginInvoke(new Action(delegate {
                progressBar.Value = e.ProgressPercentage;
            }));
        }

        private void postBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // posting in blogs
            postBackgroundWorker.ReportProgress(0);
            int counter = 0;
            int overalCounter = 0;
            foreach (string blogUrl in blogs.Keys)
            {
                if (blogsCheckListBox.CheckedIndices.Contains(overalCounter))
                {
                    BlogPoster poster = new BlogPoster(blogs[blogUrl], executor);
                    poster.Login();
                    foreach(Post post in posts)
                    {
                        if (postBackgroundWorker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        poster.Post(post);
                        counter++;
                        postBackgroundWorker.ReportProgress(100 * counter / postsToPostCount);
                    }
                    
                }
                overalCounter++;
            }
        }

        private void postBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                log("Posting cancelled.");
            }
            else {
                log("Completed posting.");
            }
        }

        private void fetchBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                log("Fetching cancelled.");
            }
            else {
                log("Completed fetching.");
            }
        }

        private void postFeedsButton_Click(object sender, EventArgs e)
        {
            postsToPostCount = posts.Count * blogsCheckListBox.CheckedIndices.Count;
            postBackgroundWorker.RunWorkerAsync();
        }
    }
}
