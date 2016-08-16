using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndAutomation
{
    public abstract class Executor
    {
        private Scope scope;
        private Statement statement;
        public Executor(Scope scope, Statement statement)
        {
            this.scope = scope;
            this.statement = statement;
        }
        public object Execute()
        {
            return statement.Process(this);
        }
        public abstract object Execute(string code);
    }
}
