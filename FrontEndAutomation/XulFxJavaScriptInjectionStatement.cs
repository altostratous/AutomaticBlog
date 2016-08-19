using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gecko;
using Gecko.DOM;
using System.IO;
using System.Xml;

namespace FrontEndAutomation
{
    public class XulFxJavaScriptInjectionStatement : Statement
    {
        public XulFxJavaScriptInjectionStatement(string name, XmlNode xmlContent)
        {
            Name = name;
            FileName = xmlContent["FileName"].InnerText;
        }
        public XulFxJavaScriptInjectionStatement() { }
        public string Name { get; set; }
        public string FileName { get; set; }

        public Scope Scope
        {
            get; set;
        }

        public object Process(Executor executor)
        {
            XulFxExecutor xulFxExecutor;
            try
            {
                xulFxExecutor = executor as XulFxExecutor;
            }
            catch (Exception)
            {
                throw new Exception("Can not execute xulfx statement in non-xulfxexecutor executor.");
            }
            GeckoNode head = xulFxExecutor.Window.Document.GetElementsByTagName("head")[0];
            GeckoElement scriptEl = xulFxExecutor.Window.Document.CreateElement("script");
            scriptEl.TextContent = File.ReadAllText(FileName);
            head.AppendChild(scriptEl);
            return null;
        }
    }
}
