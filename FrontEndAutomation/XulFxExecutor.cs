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
            //if(DoEventsBeforeExecute)
            //    Gecko.Xpcom.DoEvents();
            SetVariables();
            object res = Window.Evaluate(code);
            GetVariables();
            return res;
        }

        private void GetVariables()
        {
            try {
                GeckoNode head = Window.Document.GetElementsByTagName("body")[0];
            } catch(Exception ex)
            {
                return;
            }
            GeckoNode varsNode = Window.Document.GetElementById("variables_div");
            Scope.Variables.Clear();
            if (varsNode != null)
            {
                foreach(GeckoNode node in varsNode.ChildNodes)
                {
                    GeckoHTMLElement htmlEl = ((GeckoHTMLElement)node);
                    Scope.Variables.Add(htmlEl.Id.Substring("FrontEndAutomation_".Length), htmlEl.InnerHtml);
                }
            }
        }

        public override void SetVariables()
        {
            try {
                GeckoNode head = Window.Document.GetElementsByTagName("body")[0];
                GeckoNode varsNode = Window.Document.GetElementById("variables_div");
                if (varsNode != null)
                    varsNode.ParentNode.RemoveChild(varsNode);
                GeckoElement scriptEl = Window.Document.CreateElement("div");
                foreach (string variable in Scope.Variables.Keys)
                {
                    GeckoHTMLElement varEl = (GeckoHTMLElement)Window.Document.CreateElement("div");
                    varEl.SetAttribute("id", "FrontEndAutomation_" + variable);
                    varEl.InnerHtml = Scope.Variables[variable];
                    scriptEl.AppendChild(varEl);
                }
                scriptEl.SetAttribute("id", "variables_div");
                head.AppendChild(scriptEl);
                //foreach (string variable in Scope.Variables.Keys)
                //{
                //    Window.Evaluate("var " + variable + " = '" +Scope.Variables[variable] + "';");
                //}
            }catch(Exception ex)
            {

            }
        }
    }
}
