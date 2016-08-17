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

namespace AutomaticBlog
{
    public partial class Main : Form
    {
        XulFxExecutor executor;
        Scope scope;

        public Main()
        {
            Gecko.Xpcom.Initialize(Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) , "xulrunner"));
            InitializeComponent();
            webView.Navigate("http://google.com");
            scope = new Scope(null);
            XulFxJavaScriptInjectionStatement script = new XulFxJavaScriptInjectionStatement() { FileName = "jquery-2.1.4.js" };
            executor = new XulFxExecutor(scope, script, webView.Window);
            executor.Execute();
            webView.Window.Evaluate("$('body').hide();");
        }
    }
}
