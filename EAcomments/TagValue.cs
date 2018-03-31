using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAcomments
{
    public class TagValue
    {
        public string name { get; set; }
        public string value { get; set; }

        public TagValue(string name, string value)
        {
            this.name = name;
            this.value = value;
        }

        public string getValueByName(string pattern)
        {
            if(this.name.Equals(pattern))
            {
                return this.value;
            }
            return null;
        }
    }
}
