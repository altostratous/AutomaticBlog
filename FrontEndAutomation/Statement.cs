using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndAutomation
{
    public interface Statement
    {
        object Process(Executor executor);
        string Name { get; set; }
    }
}
