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
            return Compile(doc.DocumentElement);
        }

        private Statement Compile(XmlElement documentElement)
        {
            Statement statement = (Statement)Activator.CreateInstance(Type.GetType(documentElement.Attributes.GetNamedItem("class").InnerText), new object[] { documentElement.Name, documentElement as XmlNode });
            return statement;
        }
        private Statement Compile(XmlNode documentElement)
        {
            Statement statement = (Statement)Activator.CreateInstance(Type.GetType(documentElement.Attributes.GetNamedItem("class").InnerText), new object[] { documentElement.Name, documentElement as XmlNode });
            return statement;
        }
        public Statement Compile(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            return Compile(doc);
        }
    }
}
