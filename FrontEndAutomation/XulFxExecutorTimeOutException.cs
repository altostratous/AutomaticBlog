using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndAutomation
{
    public class XulFxExecutorTimeOutException : Exception
    {
        public XulFxExecutorTimeOutException(string message) : base(message)
        {

        }
    }
}
