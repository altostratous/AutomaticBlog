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
            Gecko.GeckoPreferences.User["capability.policy.default.Window.alert"] = "noAccess";
            Gecko.GeckoPreferences.User["capability.policy.default.Window.confirm"] = "noAccess";
            Gecko.GeckoPreferences.User["capability.policy.default.Window.prompt"] = "noAccess";
            Gecko.GeckoPreferences.Default["capability.policy.default.Window.alert"] = "noAccess";
            Gecko.GeckoPreferences.Default["capability.policy.default.Window.confirm"] = "noAccess";
            Gecko.GeckoPreferences.Default["capability.policy.default.Window.prompt"] = "noAccess";
            InitializeComponent();
            executor = new XulFxExecutor(scope, new XulFxJavaScriptStatement() { Script = "alert(\"Hello therer\");" }, webView.Window);
            executor.Execute();
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.GetType() == this.GetType()
                    && frm != this)
                {
                    frm.Close();
                }
            }
        }
    }
}
