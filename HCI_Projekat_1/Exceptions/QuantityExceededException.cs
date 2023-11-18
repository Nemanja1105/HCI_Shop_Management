using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat_1.Exceptions
{
    class QuantityExceededException:Exception
    {
        public QuantityExceededException():base("Ne postoji data kolicina proizvoda") { }
        public QuantityExceededException(string message) : base(message) { }
    }
}
