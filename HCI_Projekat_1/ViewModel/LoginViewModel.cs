using HCI_Projekat_1.Models;
using HCI_Projekat_1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat_1.ViewModel
{
    internal class LoginViewModel
    {
        private EmployeeService service=new EmployeeService();
        public LoginDTO LoginDTO { get; set; }=new LoginDTO();

        public Employee Login()
        {
            return this.service.Login(LoginDTO);
        }

    }
}
