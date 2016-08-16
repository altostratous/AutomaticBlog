using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gecko.DOM;

namespace FrontEndAutomation
{
    public class XulFxExecutor : Executor
    {
        public GeckoWindow Window { get; set; }
        public bool DoEventsBeforeExecute { get; set; }
        public XulFxExecutor (Scope scope, Statement statement, GeckoWindow window) : base (scope, statement)
        {
            Window = window;
            DoEventsBeforeExecute = false;
        }
        public override object Execute(string code)
        {
            if(DoEventsBeforeExecute)
                Gecko.Xpcom.DoEvents();
            return Window.Evaluate(code);
        }
    }
}
