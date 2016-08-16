using System.Collections.Generic;

namespace FrontEndAutomation
{
    public class Scope
    {
        private Dictionary<string, object> variables;
        
        public Scope Parent { get; set; }

        public Scope(Scope parent)
        {
            variables = new Dictionary<string, object>();
        }

        public object Resolve(string name)
        {
            return variables[name];
        }

        public void Set(string name, object value)
        {
            variables[name] = value;
        }
    }
}