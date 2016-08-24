using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FrontEndAutomation
{
    public class XulFxWaitForAWhileStatement : Statement
    {
        public XulFxWaitForAWhileStatement(string name, XmlNode xmlContent)
        {
            Name = name;
            Count = Convert.ToInt32( xmlContent["Count"].InnerText);
        }
        public string Name { get; set; }
        public int Count { get; set; }
        
        public object Process(Executor executor)
        {
            for(int i = 0; i < Count; i++)
            {
                System.Threading.Thread.Sleep(Common.WAIT_INTERVAL);
                Gecko.Xpcom.DoEvents();
                //System.Windows.Forms.Application.DoEvents();
            }
            return null;
        }
    }
}
