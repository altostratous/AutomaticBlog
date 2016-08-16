using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndAutomation
{
    public interface Statement
    {
        Scope Scope { get; set; }
        object Process(Executor executor);
    }
}
