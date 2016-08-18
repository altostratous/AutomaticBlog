using System.Collections.Generic;

namespace FrontEndAutomation
{
    public class Scope
    {        
        public Scope Parent { get; set; }
        public Dictionary<string, string> Variables { get;  set; }

        public Scope(Scope parent)
        {
            Variables = new Dictionary<string, string>();
        }

        public object Resolve(string name)
        {
            return Variables[name];
        }

        public void Set(string name, string value)
        {
            Variables[name] = value;
        }
    }
}