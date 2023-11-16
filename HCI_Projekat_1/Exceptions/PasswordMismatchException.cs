using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat_1.Exceptions
{
    internal class PasswordMismatchException:Exception
    {
        public PasswordMismatchException():base("Lozinke se ne poklapaju") {}
        public PasswordMismatchException(string message) : base(message) { }
    }
}
