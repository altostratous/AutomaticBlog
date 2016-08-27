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
            int counter = 0;
            while (true)
            {
                try {
                    if (!executor.Execute(Condition).ToString().Equals("True"))
                        break;
                }catch(Exception ex)
                {
                }
                System.Threading.Thread.Sleep(Common.WAIT_INTERVAL);

                for (int i = 0; i < 5; i++)
                {
                    Gecko.Xpcom.DoEvents(true);
                    Gecko.Xpcom.DoEvents();
                    System.Windows.Forms.Application.DoEvents();
                }
                counter++;
                if(counter > Common.WAIT_TIMEOUT_COUNT)
                {
                    throw new XulFxExecutorTimeOutException("Wait statement timed out.");
                }
            }
            return null;
        }
    }
}
