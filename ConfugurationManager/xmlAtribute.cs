using System;
using System.IO;

namespace ConfugurationManager
{
    [AttributeUsage(AttributeTargets.All)]
    public class pathAttribute:Attribute
    {
        public string name { get; set; }
        
        public pathAttribute(string scratchXml){
            if (scratchXml.EndsWith(".xml"))
            {
                name = scratchXml;
            }
            else
            {
                name = Path.ChangeExtension(scratchXml, "xml");
            }
        }
    }
    
    
}
