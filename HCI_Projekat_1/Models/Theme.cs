using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat_1.Models
{
    class Theme:IEquatable<Theme>
    {
        public String Name { get; set; }
        public String Path { get; set; }

        public bool Equals(Theme other)
        {
            if(other == null) return false;
            return Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
