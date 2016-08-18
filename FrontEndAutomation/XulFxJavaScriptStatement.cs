using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FrontEndAutomation
{
    public class XulFxJavaScriptStatement : Statement
    {
        public XulFxJavaScriptStatement(string name, XmlNode xmlContent)
        {
            Name = name;
            throw new NotImplementedException();
        }
        public XulFxJavaScriptStatement() { }
        public string Name { get; set; }
        public string Script { get; set; }

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
            return xulFxExecutor.Execute(Script);
        }
    }
}
