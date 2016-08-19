using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FrontEndAutomation
{
    public class XulFxWaitStatement : Statement
    {
        public XulFxWaitStatement(string name, XmlNode xmlContent)
        {
            Name = name;
            Condition = xmlContent["Condition"].InnerText;
        }
        public string Name { get; set; }
        public string Condition { get; set; }
        
        public object Process(Executor executor)
        {
            while (executor.Execute(Condition).ToString().Equals("True"))
            {
                System.Threading.Thread.Sleep(Common.WAIT_INTERVAL);
            }
            return null;
        }
    }
}
