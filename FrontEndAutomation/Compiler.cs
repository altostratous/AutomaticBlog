using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace FrontEndAutomation
{
    public class Compiler
    {
        public static Statement Compile(XmlDocument doc)
        {
            return Compile(doc.DocumentElement);
        }

        public static Statement Compile(XmlElement documentElement)
        {
            Statement statement = (Statement)Activator.CreateInstance(Type.GetType(documentElement.Attributes.GetNamedItem("class").InnerText), new object[] { documentElement.Name, documentElement as XmlNode });
            return statement;
        }
        public static Statement Compile(XmlNode documentElement)
        {
            Statement statement = (Statement)Activator.CreateInstance(Type.GetType(documentElement.Attributes.GetNamedItem("class").InnerText), new object[] { documentElement.Name, documentElement as XmlNode });
            return statement;
        }
        public static Statement Compile(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            return Compile(doc);
        }
    }
}
