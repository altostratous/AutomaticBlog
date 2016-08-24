using System;

namespace FrontEndAutomation
{
    public class EmptyStatement : Statement
    {
        public string Name
        {
            get; set;
        }

        public object Process(Executor executor)
        {
            return null;
        }
    }
}