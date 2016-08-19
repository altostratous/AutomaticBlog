using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndAutomation
{
    public abstract class Executor
    {
        public Executor(Scope scope, Statement statement)
        {
            this.Scope = scope;
            this.Statement = statement;
        }

        public Scope Scope { get; set; }
        public Statement Statement { get; set; }

        public object Execute()
        {
            //foreach(string variable in Scope.Variables.Keys)
            //{
            //    Execute("ver " + variable + " = '" + javaScriptifyString(Scope.Variables[variable]) + "';");

            //}
            SetVariables();
            return Statement.Process(this);
        }

        //private string javaScriptifyString(string v)
        //{
        //    return v
        //        .Replace("\\", "\\\\")
        //        .Replace("'", "\\'");
        //}

        public abstract void SetVariables();
        public abstract object Execute(string code);
    }
}
