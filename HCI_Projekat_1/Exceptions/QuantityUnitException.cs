using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat_1.Exceptions
{
    class QuantityUnitException:Exception
    {
        public QuantityUnitException() : base("Proizvodi se prodaju na komad") { }
        public QuantityUnitException(string message) : base(message) { }
    }
}
