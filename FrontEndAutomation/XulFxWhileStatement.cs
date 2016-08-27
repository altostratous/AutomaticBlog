using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FrontEndAutomation
{
    public class XulFxWhileStatement : Statement
    {
        public XulFxWhileStatement(string name, XmlNode xmlContent)
        {
            Name = name;
            Condition = xmlContent["Condition"].InnerText;
            Do = Compiler.Compile(xmlContent["Do"].FirstChild);
        }
        public string Name { get; set; }
        public string Condition { get; set; }
        public Statement Do { get; set; }
        
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
                Do.Process(executor);
                System.Threading.Thread.Sleep(Common.WAIT_INTERVAL);

                for (int i = 0; i < 5; i++)
                {
                    Gecko.Xpcom.DoEvents(true);
                    Gecko.Xpcom.DoEvents();
                    System.Windows.Forms.Application.DoEvents();
                }
                counter++;
                if(Common.WAIT_TIMEOUT_COUNT < counter)
                {
                    throw new XulFxExecutorTimeOutException("While statement timed out. ");
                }
            }
            return null;
        }
    }
}
