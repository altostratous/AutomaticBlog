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
            this.SuspendLayout();
            // 
            // webView
            // 
            this.webView.Location = new System.Drawing.Point(278, 12);
            this.webView.Name = "webView";
            this.webView.Size = new System.Drawing.Size(631, 423);
            this.webView.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 447);
            this.Controls.Add(this.webView);
            this.Name = "Main";
            this.Text = "Automatic Blog";
            this.ResumeLayout(false);

        }

        #endregion

        private Gecko.Windows.WebView webView;
    }
}

