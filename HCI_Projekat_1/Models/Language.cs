using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat_1.Models
{
    internal class Language : Theme, IEquatable<Language>
    {
        public bool Equals(Language other)
        {
            if (other == null)
                return false;
            return this.Name.Equals(other.Name); 
        }
    }
}
