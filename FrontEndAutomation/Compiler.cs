using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FrontEndAutomation
{
    public class Compiler
    {
        public Statement Compile(XmlDocument doc)
        {
            throw new NotImplementedException();
        }
        public Statement Compile(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            return Compile(doc);
        }
    }
}
