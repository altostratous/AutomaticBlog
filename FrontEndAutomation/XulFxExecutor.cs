using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gecko.DOM;
using Gecko;

namespace FrontEndAutomation
{
    public class XulFxExecutor : Executor
    {
        public GeckoWindow Window { get; set; }
        public bool DoEventsBeforeExecute { get; set; }
        public XulFxExecutor (Scope scope, Statement statement, GeckoWindow window) : base (scope, statement)
        {
            Window = window;
            DoEventsBeforeExecute = true;
        }
        public override object Execute(string code)
        {
            if(DoEventsBeforeExecute)
                Gecko.Xpcom.DoEvents();
            object res = Window.Evaluate(code);
            return res;
        }

        public override void SetVariables()
        {
            GeckoNode head = Window.Document.GetElementsByTagName("head")[0];
            GeckoElement scriptEl = Window.Document.CreateElement("variables");
            foreach(string variable in Scope.Variables.Keys)
            {
                GeckoElement varEl = Window.Document.CreateElement("variable");
                varEl.SetAttribute("id", "FrontEndAutomation." + variable);
                varEl.TextContent = Scope.Variables[variable];
                scriptEl.AppendChild(varEl);
            }
            head.AppendChild(scriptEl);
        }
    }
}
