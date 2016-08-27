namespace AutomaticBlog
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.webView = new Gecko.Windows.WebView();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.fetchFeedsButton = new System.Windows.Forms.Button();
            this.feedsCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.blogsCheckListBox = new System.Windows.Forms.CheckedListBox();
            this.postFeedsButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.reloadButton = new System.Windows.Forms.Button();
            this.logListBox = new System.Windows.Forms.ListBox();
            this.fetchBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.postBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.consoleTextBox = new System.Windows.Forms.TextBox();
            this.operationsPanel = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.operationTab = new System.Windows.Forms.TabPage();
            this.toDateTime = new System.Windows.Forms.DateTimePicker();
            this.toLbl = new System.Windows.Forms.Label();
            this.fromLbl = new System.Windows.Forms.Label();
            this.fromDateTime = new System.Windows.Forms.DateTimePicker();
            this.timeFilterButton = new System.Windows.Forms.Button();
            this.generatePairsBtn = new System.Windows.Forms.Button();
            this.postsGrid = new System.Windows.Forms.DataGridView();
            this.Url = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Blog = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Status = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.configTab = new System.Windows.Forms.TabPage();
            this.browserTab = new System.Windows.Forms.TabPage();
            this.feedList = new System.Windows.Forms.ListBox();
            this.blogsListBox = new System.Windows.Forms.ListBox();
            this.saveBtn = new System.Windows.Forms.Button();
            this.loadBtn = new System.Windows.Forms.Button();
            this.editFeedGroupBox = new System.Windows.Forms.GroupBox();
            this.editBlogGroupBox = new System.Windows.Forms.GroupBox();
            this.addFeed = new System.Windows.Forms.Button();
            this.removeFeedBtn = new System.Windows.Forms.Button();
            this.removeBlogsBtn = new System.Windows.Forms.Button();
            this.addBlogBtn = new System.Windows.Forms.Button();
            this.operationsPanel.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.operationTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.postsGrid)).BeginInit();
            this.configTab.SuspendLayout();
            this.browserTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // webView
            // 
            this.webView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webView.Location = new System.Drawing.Point(6, 6);
            this.webView.Name = "webView";
            this.webView.Size = new System.Drawing.Size(674, 347);
            this.webView.TabIndex = 0;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBar.Location = new System.Drawing.Point(3, 324);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(293, 23);
            this.progressBar.TabIndex = 2;
            // 
            // fetchFeedsButton
            // 
            this.fetchFeedsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.fetchFeedsButton.Location = new System.Drawing.Point(3, 4);
            this.fetchFeedsButton.Name = "fetchFeedsButton";
            this.fetchFeedsButton.Size = new System.Drawing.Size(78, 23);
            this.fetchFeedsButton.TabIndex = 3;
            this.fetchFeedsButton.Text = "Fetch Feeds";
            this.fetchFeedsButton.UseVisualStyleBackColor = true;
            this.fetchFeedsButton.Click += new System.EventHandler(this.fetchFeedsButton_Click);
            // 
            // feedsCheckedListBox
            // 
            this.feedsCheckedListBox.FormattingEnabled = true;
            this.feedsCheckedListBox.Location = new System.Drawing.Point(3, 6);
            this.feedsCheckedListBox.Name = "feedsCheckedListBox";
            this.feedsCheckedListBox.Size = new System.Drawing.Size(293, 94);
            this.feedsCheckedListBox.TabIndex = 4;
            // 
            // blogsCheckListBox
            // 
            this.blogsCheckListBox.FormattingEnabled = true;
            this.blogsCheckListBox.Location = new System.Drawing.Point(3, 106);
            this.blogsCheckListBox.Name = "blogsCheckListBox";
            this.blogsCheckListBox.Size = new System.Drawing.Size(293, 124);
            this.blogsCheckListBox.TabIndex = 5;
            // 
            // postFeedsButton
            // 
            this.postFeedsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.postFeedsButton.Location = new System.Drawing.Point(87, 4);
            this.postFeedsButton.Name = "postFeedsButton";
            this.postFeedsButton.Size = new System.Drawing.Size(78, 23);
            this.postFeedsButton.TabIndex = 6;
            this.postFeedsButton.Text = "Post Feeds";
            this.postFeedsButton.UseVisualStyleBackColor = true;
            this.postFeedsButton.Click += new System.EventHandler(this.postFeedsButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.Location = new System.Drawing.Point(235, 354);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(61, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // reloadButton
            // 
            this.reloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.reloadButton.Location = new System.Drawing.Point(171, 4);
            this.reloadButton.Name = "reloadButton";
            this.reloadButton.Size = new System.Drawing.Size(58, 23);
            this.reloadButton.TabIndex = 8;
            this.reloadButton.Text = "Reload";
            this.reloadButton.UseVisualStyleBackColor = true;
            this.reloadButton.Click += new System.EventHandler(this.reloadButton_Click);
            // 
            // logListBox
            // 
            this.logListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.logListBox.FormattingEnabled = true;
            this.logListBox.HorizontalScrollbar = true;
            this.logListBox.Location = new System.Drawing.Point(3, 236);
            this.logListBox.Name = "logListBox";
            this.logListBox.Size = new System.Drawing.Size(293, 82);
            this.logListBox.TabIndex = 9;
            // 
            // fetchBackgroundWorker
            // 
            this.fetchBackgroundWorker.WorkerReportsProgress = true;
            this.fetchBackgroundWorker.WorkerSupportsCancellation = true;
            this.fetchBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.fetchBackgroundWorker_DoWork);
            this.fetchBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.fetchBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.fetchBackgroundWorker_RunWorkerCompleted);
            // 
            // postBackgroundWorker
            // 
            this.postBackgroundWorker.WorkerReportsProgress = true;
            this.postBackgroundWorker.WorkerSupportsCancellation = true;
            this.postBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.postBackgroundWorker_DoWork);
            this.postBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.postBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.postBackgroundWorker_RunWorkerCompleted);
            // 
            // consoleTextBox
            // 
            this.consoleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.consoleTextBox.Location = new System.Drawing.Point(6, 359);
            this.consoleTextBox.Name = "consoleTextBox";
            this.consoleTextBox.Size = new System.Drawing.Size(674, 20);
            this.consoleTextBox.TabIndex = 10;
            this.consoleTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.consoleTextBox_KeyPress);
            // 
            // operationsPanel
            // 
            this.operationsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.operationsPanel.Controls.Add(this.fetchFeedsButton);
            this.operationsPanel.Controls.Add(this.postFeedsButton);
            this.operationsPanel.Controls.Add(this.reloadButton);
            this.operationsPanel.Location = new System.Drawing.Point(0, 350);
            this.operationsPanel.Name = "operationsPanel";
            this.operationsPanel.Size = new System.Drawing.Size(232, 30);
            this.operationsPanel.TabIndex = 11;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.operationTab);
            this.tabControl.Controls.Add(this.configTab);
            this.tabControl.Controls.Add(this.browserTab);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(694, 412);
            this.tabControl.TabIndex = 12;
            // 
            // operationTab
            // 
            this.operationTab.Controls.Add(this.toDateTime);
            this.operationTab.Controls.Add(this.toLbl);
            this.operationTab.Controls.Add(this.fromLbl);
            this.operationTab.Controls.Add(this.fromDateTime);
            this.operationTab.Controls.Add(this.timeFilterButton);
            this.operationTab.Controls.Add(this.generatePairsBtn);
            this.operationTab.Controls.Add(this.postsGrid);
            this.operationTab.Controls.Add(this.feedsCheckedListBox);
            this.operationTab.Controls.Add(this.operationsPanel);
            this.operationTab.Controls.Add(this.progressBar);
            this.operationTab.Controls.Add(this.logListBox);
            this.operationTab.Controls.Add(this.blogsCheckListBox);
            this.operationTab.Controls.Add(this.cancelButton);
            this.operationTab.Location = new System.Drawing.Point(4, 22);
            this.operationTab.Name = "operationTab";
            this.operationTab.Padding = new System.Windows.Forms.Padding(3);
            this.operationTab.Size = new System.Drawing.Size(686, 386);
            this.operationTab.TabIndex = 2;
            this.operationTab.Text = "Operations";
            this.operationTab.UseVisualStyleBackColor = true;
            // 
            // toDateTime
            // 
            this.toDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.toDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.toDateTime.Location = new System.Drawing.Point(591, 355);
            this.toDateTime.Name = "toDateTime";
            this.toDateTime.Size = new System.Drawing.Size(87, 20);
            this.toDateTime.TabIndex = 19;
            // 
            // toLbl
            // 
            this.toLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.toLbl.AutoSize = true;
            this.toLbl.Location = new System.Drawing.Point(565, 359);
            this.toLbl.Name = "toLbl";
            this.toLbl.Size = new System.Drawing.Size(20, 13);
            this.toLbl.TabIndex = 18;
            this.toLbl.Text = "To";
            // 
            // fromLbl
            // 
            this.fromLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.fromLbl.AutoSize = true;
            this.fromLbl.Location = new System.Drawing.Point(436, 358);
            this.fromLbl.Name = "fromLbl";
            this.fromLbl.Size = new System.Drawing.Size(30, 13);
            this.fromLbl.TabIndex = 17;
            this.fromLbl.Text = "From";
            // 
            // fromDateTime
            // 
            this.fromDateTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.fromDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.fromDateTime.Location = new System.Drawing.Point(472, 355);
            this.fromDateTime.Name = "fromDateTime";
            this.fromDateTime.Size = new System.Drawing.Size(87, 20);
            this.fromDateTime.TabIndex = 16;
            // 
            // timeFilterButton
            // 
            this.timeFilterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.timeFilterButton.Location = new System.Drawing.Point(369, 354);
            this.timeFilterButton.Name = "timeFilterButton";
            this.timeFilterButton.Size = new System.Drawing.Size(61, 23);
            this.timeFilterButton.TabIndex = 14;
            this.timeFilterButton.Text = "Filter";
            this.timeFilterButton.UseVisualStyleBackColor = true;
            this.timeFilterButton.Click += new System.EventHandler(this.timeFilterButton_Click);
            // 
            // generatePairsBtn
            // 
            this.generatePairsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.generatePairsBtn.Location = new System.Drawing.Point(302, 354);
            this.generatePairsBtn.Name = "generatePairsBtn";
            this.generatePairsBtn.Size = new System.Drawing.Size(61, 23);
            this.generatePairsBtn.TabIndex = 13;
            this.generatePairsBtn.Text = "Generate";
            this.generatePairsBtn.UseVisualStyleBackColor = true;
            this.generatePairsBtn.Click += new System.EventHandler(this.generatePairsBtn_Click);
            // 
            // postsGrid
            // 
            this.postsGrid.AllowUserToAddRows = false;
            this.postsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.postsGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.postsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.postsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Url,
            this.Blog,
            this.Status});
            this.postsGrid.Location = new System.Drawing.Point(302, 6);
            this.postsGrid.Name = "postsGrid";
            this.postsGrid.ReadOnly = true;
            this.postsGrid.Size = new System.Drawing.Size(376, 341);
            this.postsGrid.TabIndex = 12;
            // 
            // Url
            // 
            this.Url.HeaderText = "Url";
            this.Url.Name = "Url";
            this.Url.ReadOnly = true;
            // 
            // Blog
            // 
            this.Blog.HeaderText = "Blog";
            this.Blog.Name = "Blog";
            this.Blog.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // configTab
            // 
            this.configTab.Controls.Add(this.addBlogBtn);
            this.configTab.Controls.Add(this.removeBlogsBtn);
            this.configTab.Controls.Add(this.removeFeedBtn);
            this.configTab.Controls.Add(this.addFeed);
            this.configTab.Controls.Add(this.editBlogGroupBox);
            this.configTab.Controls.Add(this.editFeedGroupBox);
            this.configTab.Controls.Add(this.loadBtn);
            this.configTab.Controls.Add(this.saveBtn);
            this.configTab.Controls.Add(this.blogsListBox);
            this.configTab.Controls.Add(this.feedList);
            this.configTab.Location = new System.Drawing.Point(4, 22);
            this.configTab.Name = "configTab";
            this.configTab.Padding = new System.Windows.Forms.Padding(3);
            this.configTab.Size = new System.Drawing.Size(686, 386);
            this.configTab.TabIndex = 1;
            this.configTab.Text = "Configuration";
            this.configTab.UseVisualStyleBackColor = true;
            // 
            // browserTab
            // 
            this.browserTab.Controls.Add(this.webView);
            this.browserTab.Controls.Add(this.consoleTextBox);
            this.browserTab.Location = new System.Drawing.Point(4, 22);
            this.browserTab.Name = "browserTab";
            this.browserTab.Padding = new System.Windows.Forms.Padding(3);
            this.browserTab.Size = new System.Drawing.Size(686, 386);
            this.browserTab.TabIndex = 0;
            this.browserTab.Text = "Browser";
            this.browserTab.UseVisualStyleBackColor = true;
            // 
            // feedList
            // 
            this.feedList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.feedList.FormattingEnabled = true;
            this.feedList.Location = new System.Drawing.Point(8, 8);
            this.feedList.Name = "feedList";
            this.feedList.Size = new System.Drawing.Size(285, 134);
            this.feedList.TabIndex = 0;
            // 
            // blogsListBox
            // 
            this.blogsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.blogsListBox.FormattingEnabled = true;
            this.blogsListBox.Location = new System.Drawing.Point(299, 8);
            this.blogsListBox.Name = "blogsListBox";
            this.blogsListBox.Size = new System.Drawing.Size(379, 134);
            this.blogsListBox.TabIndex = 1;
            // 
            // saveBtn
            // 
            this.saveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveBtn.Location = new System.Drawing.Point(8, 355);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.TabIndex = 2;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            // 
            // loadBtn
            // 
            this.loadBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.loadBtn.Location = new System.Drawing.Point(89, 355);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(75, 23);
            this.loadBtn.TabIndex = 3;
            this.loadBtn.Text = "Load";
            this.loadBtn.UseVisualStyleBackColor = true;
            // 
            // editFeedGroupBox
            // 
            this.editFeedGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.editFeedGroupBox.Location = new System.Drawing.Point(8, 148);
            this.editFeedGroupBox.Name = "editFeedGroupBox";
            this.editFeedGroupBox.Size = new System.Drawing.Size(285, 201);
            this.editFeedGroupBox.TabIndex = 4;
            this.editFeedGroupBox.TabStop = false;
            this.editFeedGroupBox.Text = "Edit Feed";
            // 
            // editBlogGroupBox
            // 
            this.editBlogGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editBlogGroupBox.Location = new System.Drawing.Point(299, 148);
            this.editBlogGroupBox.Name = "editBlogGroupBox";
            this.editBlogGroupBox.Size = new System.Drawing.Size(379, 201);
            this.editBlogGroupBox.TabIndex = 5;
            this.editBlogGroupBox.TabStop = false;
            this.editBlogGroupBox.Text = "Edit blog";
            // 
            // addFeed
            // 
            this.addFeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addFeed.Location = new System.Drawing.Point(170, 355);
            this.addFeed.Name = "addFeed";
            this.addFeed.Size = new System.Drawing.Size(75, 23);
            this.addFeed.TabIndex = 6;
            this.addFeed.Text = "Add feed";
            this.addFeed.UseVisualStyleBackColor = true;
            // 
            // removeFeedBtn
            // 
            this.removeFeedBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeFeedBtn.Location = new System.Drawing.Point(251, 355);
            this.removeFeedBtn.Name = "removeFeedBtn";
            this.removeFeedBtn.Size = new System.Drawing.Size(88, 23);
            this.removeFeedBtn.TabIndex = 7;
            this.removeFeedBtn.Text = "Remove feeds";
            this.removeFeedBtn.UseVisualStyleBackColor = true;
            // 
            // removeBlogsBtn
            // 
            this.removeBlogsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.removeBlogsBtn.Location = new System.Drawing.Point(426, 355);
            this.removeBlogsBtn.Name = "removeBlogsBtn";
            this.removeBlogsBtn.Size = new System.Drawing.Size(96, 23);
            this.removeBlogsBtn.TabIndex = 8;
            this.removeBlogsBtn.Text = "Remove blogs";
            this.removeBlogsBtn.UseVisualStyleBackColor = true;
            // 
            // addBlogBtn
            // 
            this.addBlogBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addBlogBtn.Location = new System.Drawing.Point(345, 355);
            this.addBlogBtn.Name = "addBlogBtn";
            this.addBlogBtn.Size = new System.Drawing.Size(75, 23);
            this.addBlogBtn.TabIndex = 9;
            this.addBlogBtn.Text = "Add blog";
            this.addBlogBtn.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 412);
            this.Controls.Add(this.tabControl);
            this.Name = "Main";
            this.Text = "Automatic Blog";
            this.operationsPanel.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.operationTab.ResumeLayout(false);
            this.operationTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.postsGrid)).EndInit();
            this.configTab.ResumeLayout(false);
            this.browserTab.ResumeLayout(false);
            this.browserTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Gecko.Windows.WebView webView;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button fetchFeedsButton;
        private System.Windows.Forms.CheckedListBox feedsCheckedListBox;
        private System.Windows.Forms.CheckedListBox blogsCheckListBox;
        private System.Windows.Forms.Button postFeedsButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button reloadButton;
        private System.Windows.Forms.ListBox logListBox;
        private System.ComponentModel.BackgroundWorker fetchBackgroundWorker;
        private System.ComponentModel.BackgroundWorker postBackgroundWorker;
        private System.Windows.Forms.TextBox consoleTextBox;
        private System.Windows.Forms.Panel operationsPanel;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage browserTab;
        private System.Windows.Forms.TabPage configTab;
        private System.Windows.Forms.TabPage operationTab;
        private System.Windows.Forms.DataGridView postsGrid;
        private System.Windows.Forms.DateTimePicker toDateTime;
        private System.Windows.Forms.Label toLbl;
        private System.Windows.Forms.Label fromLbl;
        private System.Windows.Forms.DateTimePicker fromDateTime;
        private System.Windows.Forms.Button timeFilterButton;
        private System.Windows.Forms.Button generatePairsBtn;
        private System.Windows.Forms.DataGridViewLinkColumn Url;
        private System.Windows.Forms.DataGridViewLinkColumn Blog;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Status;
        private System.Windows.Forms.Button addBlogBtn;
        private System.Windows.Forms.Button removeBlogsBtn;
        private System.Windows.Forms.Button removeFeedBtn;
        private System.Windows.Forms.Button addFeed;
        private System.Windows.Forms.GroupBox editBlogGroupBox;
        private System.Windows.Forms.GroupBox editFeedGroupBox;
        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.ListBox blogsListBox;
        private System.Windows.Forms.ListBox feedList;
    }
}

