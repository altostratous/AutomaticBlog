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
        ConfigurationEditor configurationEditor;

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
            configurationEditor = new ConfigurationEditor("Config.xml", "Scripts");
            loadConfigEditor();
            // for test 
            //posts.Add(new Post()
            //{
            //    Title = "hsh",
            //    Abstract = "hsdhl",
            //    Content = "sdkfj",
            //    ReadMore = "dljgas"
            //});
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
                    if (currentBlog != blog)
                    {
                        poster = new BlogPoster(blog, executor);
                        if (postBackgroundWorker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        webView.Invoke(new Action(delegate
                        {
                            try
                            {
                                poster.Login();
                            }
                            catch (Exception ex)
                            {
                                log(ex.Message);
                            }
                        }));
                        if (postBackgroundWorker.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        currentBlog = blog;
                    }
                    if (postBackgroundWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    webView.Invoke(new Action(delegate
                    {
                        try
                        {
                            poster.Post(post);
                            checkPostBlogPair(currentBlog, post);
                        }
                        catch (Exception ex)
                        {
                            currentBlog = null;
                            safeRestartBrowser();
                            log(ex.Message);
                        }
                    }));
                }
                postBackgroundWorker.ReportProgress((row.Index + 1) * 100 / postsGrid.RowCount);
            }
        }

        private void safeRestartBrowser()
        {
            Common.RemoveAll();
            webView.Navigate("about:blank");
            for (int i = 0; i < 10; i++)
            {
                Gecko.Xpcom.DoEvents();
                Application.DoEvents();
                System.Threading.Thread.Sleep(Common.WAIT_INTERVAL);
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
            safeRestartBrowser();
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

        private void loadBtn_Click(object sender, EventArgs e)
        {
            loadConfigEditor();
        }

        private void loadConfigEditor()
        {
            configurationEditor.Load();
            blogsListBox.Items.Clear();
            feedList.Items.Clear();
            foreach (string url in configurationEditor.Blogs.Keys)
            {
                blogsListBox.Items.Add(url);
            }
            foreach (string feed in configurationEditor.Feeds)
            {
                feedList.Items.Add(feed);
            }
            blogTypeComboBox.Items.Clear();
            HashSet<string> blogTypes = new HashSet<string>();
            foreach (string scriptName in Directory.GetFiles(configurationEditor.ScriptsDirectory))
            {
                blogTypes.Add(Path.GetFileNameWithoutExtension(scriptName).Replace("Login", "").Replace("Post", ""));
            }
            foreach (string blogType in blogTypes)
            {
                blogTypeComboBox.Items.Add(blogType);
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {

            configurationEditor.Save();
        }

        private void blogsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (updateLock)
                return;
            viewLock = true;
            if(feedList.SelectedIndex != -1)
            {
                feedUrlTextBox.Text = (string)feedList.SelectedItem;
            }
            if(blogsListBox.SelectedIndex != -1)
            {
                Blog blog = configurationEditor.Blogs[(string)blogsListBox.SelectedItem];
                urlTextBox.Text = blog.Url;
                usernameTextBox.Text = blog.Username;
                passwordTextBox.Text = blog.Password;
                blogTypeComboBox.SelectedIndex = blogTypeIndex(blog);
            }
            viewLock = false;
        }

        private int blogTypeIndex(Blog blog)
        {
            return blogTypeComboBox.Items.IndexOf(Path.GetFileNameWithoutExtension(blog.LoginScript).Replace("Login", ""));
        }

        private void feedUrlTextBox_TextChanged(object sender, EventArgs e)
        {
            if(feedList.SelectedIndex != -1)
            {
                feedList.Items[feedList.SelectedIndex] = feedUrlTextBox.Text;
                configurationEditor.Feeds[feedList.SelectedIndex] = feedUrlTextBox.Text;
            }
        }

        private void urlTextBox_TextChanged(object sender, EventArgs e)
        {
            if (viewLock)
                return;
            updateLock = true;
            if (blogsListBox.SelectedIndex != -1)
            {
                configurationEditor.Blogs.Remove(blogsListBox.SelectedItem.ToString());
                blogsListBox.Items[blogsListBox.SelectedIndex] = urlTextBox.Text;
                configurationEditor.Blogs.Add((string)blogsListBox.SelectedItem, new AutomaticBlog.Blog());
                configurationEditor.Blogs[(string)blogsListBox.SelectedItem].Url = urlTextBox.Text;
                configurationEditor.Blogs[(string)blogsListBox.SelectedItem].Username = usernameTextBox.Text;
                configurationEditor.Blogs[(string)blogsListBox.SelectedItem].Password = passwordTextBox.Text;
                configurationEditor.Blogs[(string)blogsListBox.SelectedItem].LoginScript = loginScriptFromBlogType((string)blogTypeComboBox.SelectedItem);
                configurationEditor.Blogs[(string)blogsListBox.SelectedItem].PostScript = postScriptFromBlogType((string)blogTypeComboBox.SelectedItem);
            }
            updateLock = false;
        }

        bool updateLock = false;
        bool viewLock = true;

        private string postScriptFromBlogType(string selectedItem)
        {
            return Path.Combine(configurationEditor.ScriptsDirectory, selectedItem + "Post.xml");
        }

        private string loginScriptFromBlogType(string selectedItem)
        {
            return Path.Combine(configurationEditor.ScriptsDirectory, selectedItem + "Login.xml");
        }

        private void addFeed_Click(object sender, EventArgs e)
        {
            if(feedList.Items.Contains("New feed."))
            {
                MessageBox.Show("First configure the last feed.");
            }
            feedList.Items.Add("New feed.");
            configurationEditor.Feeds.Add("New feed.");
        }

        private void addBlogBtn_Click(object sender, EventArgs e)
        {
            try {
                configurationEditor.Blogs.Add("New blog.", new AutomaticBlog.Blog());
                 blogsListBox.Items.Add("New blog.");
            }
            catch (Exception)
            {
                MessageBox.Show("First configure the last added blog.");
            }
        }

        private void removeFeedBtn_Click(object sender, EventArgs e)
        {
            if (feedList.SelectedIndex != -1)
            {
                configurationEditor.Feeds.Remove((string)feedList.SelectedItem);
                feedList.Items.RemoveAt(feedList.SelectedIndex);
            }
        }

        private void removeBlogsBtn_Click(object sender, EventArgs e)
        {
            if(blogsListBox.SelectedIndex != -1)
            {
                configurationEditor.Blogs.Remove((string)blogsListBox.SelectedItem);
                blogsListBox.Items.RemoveAt(blogsListBox.SelectedIndex);
            }
        }
    }
}
