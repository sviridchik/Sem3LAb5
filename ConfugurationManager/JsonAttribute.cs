using System;
using System.IO;

namespace ConfugurationManager
{
    public class pathJAttribute:Attribute
    {
        public string path { get; set; }

        public pathJAttribute(string scratchJson)
        {
            if (scratchJson.EndsWith(".json"))
            {
                path = scratchJson;
            }
            else
            {
                path = Path.ChangeExtension(scratchJson, "json");
            }
        }
    }
}


