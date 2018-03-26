using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EAcomments
{
    public static class XMLParser
    {
        public static string parseXML(string pattern, string input)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(input);
            XmlNodeList XMLnodes = document.GetElementsByTagName(pattern);
            if (XMLnodes.Count > 0)
                return XMLnodes[0].InnerText;
            else
                return null;
        }
    }
}
