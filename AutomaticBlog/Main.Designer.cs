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
            this.SuspendLayout();
            // 
            // webView
            // 
            this.webView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webView.Location = new System.Drawing.Point(309, 12);
            this.webView.Name = "webView";
            this.webView.Size = new System.Drawing.Size(600, 423);
            this.webView.TabIndex = 0;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBar.Location = new System.Drawing.Point(10, 383);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(293, 23);
            this.progressBar.TabIndex = 2;
            // 
            // fetchFeedsButton
            // 
            this.fetchFeedsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.fetchFeedsButton.Location = new System.Drawing.Point(10, 412);
            this.fetchFeedsButton.Name = "fetchFeedsButton";
            this.fetchFeedsButton.Size = new System.Drawing.Size(78, 23);
            this.fetchFeedsButton.TabIndex = 3;
            this.fetchFeedsButton.Text = "Fetch Feeds";
            this.fetchFeedsButton.UseVisualStyleBackColor = true;
            // 
            // feedsCheckedListBox
            // 
            this.feedsCheckedListBox.FormattingEnabled = true;
            this.feedsCheckedListBox.Location = new System.Drawing.Point(10, 12);
            this.feedsCheckedListBox.Name = "feedsCheckedListBox";
            this.feedsCheckedListBox.Size = new System.Drawing.Size(293, 154);
            this.feedsCheckedListBox.TabIndex = 4;
            // 
            // blogsCheckListBox
            // 
            this.blogsCheckListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.blogsCheckListBox.FormattingEnabled = true;
            this.blogsCheckListBox.Location = new System.Drawing.Point(10, 172);
            this.blogsCheckListBox.Name = "blogsCheckListBox";
            this.blogsCheckListBox.Size = new System.Drawing.Size(293, 199);
            this.blogsCheckListBox.TabIndex = 5;
            // 
            // postFeedsButton
            // 
            this.postFeedsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.postFeedsButton.Location = new System.Drawing.Point(94, 412);
            this.postFeedsButton.Name = "postFeedsButton";
            this.postFeedsButton.Size = new System.Drawing.Size(78, 23);
            this.postFeedsButton.TabIndex = 6;
            this.postFeedsButton.Text = "Post Feeds";
            this.postFeedsButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.Location = new System.Drawing.Point(242, 412);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(61, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // reloadButton
            // 
            this.reloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.reloadButton.Location = new System.Drawing.Point(178, 412);
            this.reloadButton.Name = "reloadButton";
            this.reloadButton.Size = new System.Drawing.Size(58, 23);
            this.reloadButton.TabIndex = 8;
            this.reloadButton.Text = "Reload";
            this.reloadButton.UseVisualStyleBackColor = true;
            this.reloadButton.Click += new System.EventHandler(this.reloadButton_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 447);
            this.Controls.Add(this.reloadButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.postFeedsButton);
            this.Controls.Add(this.blogsCheckListBox);
            this.Controls.Add(this.feedsCheckedListBox);
            this.Controls.Add(this.fetchFeedsButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.webView);
            this.Name = "Main";
            this.Text = "Automatic Blog";
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
    }
}

