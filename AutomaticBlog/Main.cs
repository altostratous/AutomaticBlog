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
        List<Post> posts;
        int fetchSelectedFeedsCount = 0;
        int postsToPostCount = 0;

        public Main()
        {
            Gecko.Xpcom.Initialize(Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) , "xulrunner"));
            InitializeComponent();
            scope = new Scope(null);
            executor = new XulFxExecutor(scope, null, webView.Window);
            scope = new Scope(null);
            feeds = new Dictionary<string, string>();
            blogs = new Dictionary<string, Blog>();
            posts = new List<Post>();
            loadConfiguration("Config.xml");

            // for test 
            posts.Add(new Post()
            {
                Title = "hsh",
                Abstract = "hsdhl",
                Content = "sdkfj",
                ReadMore = "dljgas"
            });
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
                string script = "";
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
                    Password = node["password"].InnerText,
                    Url = node["url"].InnerText
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
            operationsPanel.Enabled = false;
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
            log("Cancelling operation. ");
        }

        private void log(string toLog)
        {
            logListBox.BeginInvoke(new Action(delegate {
                if (toLog != null)
                    logListBox.Items.Add(toLog);
                else
                    logListBox.Items.Add("null");
                logListBox.TopIndex = logListBox.Items.Count - 1;
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
            AutomaticBlog.Blog currentBlog = null;
            BlogPoster poster = new BlogPoster(new AutomaticBlog.Blog(), executor);
            foreach (DataGridViewRow row in postsGrid.Rows)
            {
                if (!(bool)row.Cells["Status"].Value)
                {
                    Post post = posts.Find(item => { return item.Link == (string)row.Cells["Url"].Value; });
                    AutomaticBlog.Blog blog = blogs[row.Cells["Blog"].Value.ToString()];
                    if(currentBlog != blog)
                    {
                        poster = new BlogPoster(blog, executor);
                        webView.Invoke(new Action(delegate {
                            poster.Login();
                        }));
                        currentBlog = blog;
                    }
                    webView.Invoke(new Action(delegate {
                        poster.Post(post);
                    }));

                    checkPostBlogPair(currentBlog, post);
                }
                postBackgroundWorker.ReportProgress((row.Index + 1) * 100 / postsGrid.RowCount);
            }
        }

        private void checkPostBlogPair(Blog currentBlog, Post post)
        {
            postsGrid.Invoke(new Action(delegate
            {
                foreach(DataGridViewRow row in postsGrid.Rows)
                {
                    if(post.Link == (string)row.Cells["Url"].Value && currentBlog.Url == (string)row.Cells["BLog"].Value)
                    {
                        row.Cells["Status"].Value = true;
                        return;
                    }
                }
            }));
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
            operationsPanel.Enabled = true;
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
            operationsPanel.Enabled = true;
        }

        private void postFeedsButton_Click(object sender, EventArgs e)
        {
            // restart the browser
            Common.RemoveAll();
            webView.Navigate("about:blank");
            for(int i = 0; i < 10; i++)
            {
                Gecko.Xpcom.DoEvents();
                Application.DoEvents();
                System.Threading.Thread.Sleep(Common.WAIT_INTERVAL);
            }
            postsToPostCount = posts.Count * blogsCheckListBox.CheckedIndices.Count;
            postBackgroundWorker.RunWorkerAsync();
            operationsPanel.Enabled = false;
        }

        private void consoleTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                try {
                    log(webView.Window.Evaluate(consoleTextBox.Text));
                }
                catch (Exception ex)
                {
                    log(ex.Message);
                }
                consoleTextBox.Text = "";
            }
        }

        private void generatePairsBtn_Click(object sender, EventArgs e)
        {
            postsGrid.Rows.Clear();
            foreach (object selectedBlog in blogsCheckListBox.CheckedItems)
            {
                foreach (Post post in posts)
                {
                    AutomaticBlog.Blog blog = blogs[(string)selectedBlog];
                    postsGrid.Rows.Add(post.Link, blog.Url, false);
                }
            }
        }

        private void timeFilterButton_Click(object sender, EventArgs e)
        {
            HashSet<string> postToRemove = new HashSet<string>();
            foreach(DataGridViewRow row in postsGrid.Rows)
            {
                Post post = posts.Find(item => { return item.Link == (string)row.Cells["Url"].Value; });
                if(post.Date.CompareTo(fromDateTime.Value) < 0 || post.Date.CompareTo(toDateTime.Value) > 0)
                {
                    postToRemove.Add(post.Link);
                }
            }
            for(int i = postsGrid.Rows.Count - 1; i >= 0; i--)
            {
                if(postToRemove.Contains((string)postsGrid.Rows[i].Cells["Url"].Value))
                {
                    postsGrid.Rows.RemoveAt(i);
                }
            }
        }
    }
}
