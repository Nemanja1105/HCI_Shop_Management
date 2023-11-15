using HCI_Projekat_1.Models;
using HCI_Projekat_1.Models.Enums;
using HCI_Projekat_1.Services;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Cms;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat_1.ViewModel
{
    internal class EmployeeViewModel : INotifyPropertyChanged
    {
        private EmployeeService service = new EmployeeService();
        private List<Employee> employees;
        private ObservableCollection<Employee> data;
        public ObservableCollection<Employee> Data { get { return this.data; } set { data = value; OnPropertyChanged(); } }

        private UserType userType = UserType.All;
        private String searchFilter = "";
        public UserType UserFilter { get { return userType; } set { this.userType = value; FindAllByFilter(); } }
        public String SearchFilter { get { return searchFilter; } set { this.searchFilter = value; FindAllByFilter(); } }

        public async Task FindAll()
        {
            List<Employee> result = new List<Employee>();
            try
            {
                result = await service.FindAll();
            }
            finally
            {
                this.employees = result;
                Data = new ObservableCollection<Employee>(result);
            }
        }

        public void FindAllByFilter()
        {
            if (UserFilter == UserType.All && String.IsNullOrEmpty(SearchFilter))
            {
                Data = new ObservableCollection<Employee>(employees);
                return;
            }
            var query = employees.AsQueryable();
            if (!String.IsNullOrEmpty(SearchFilter))
                query = query.Where((el) => el.Name.ToUpper().StartsWith(SearchFilter.ToUpper()));
            if (UserFilter != UserType.All)
            {
                bool status = UserFilter == UserType.Worker ? false : true;
                query = query.Where((el) => el.Uloga == status);
            }
            Data = new ObservableCollection<Employee>(query.ToList());
        }

        public async Task Delete(Employee employee)
        {
            await this.service.Delete(employee);
            employees.Remove(employee);
            if (Data.Contains(employee))
                Data.Remove(employee);
        }

        public async Task Update(Employee employee)
        {
            await this.service.Update(employee);
            int index=this.employees.IndexOf(employee);
            if(index != -1)
                this.employees[index] = employee;
            this.FindAllByFilter();
            
        }

        public async Task Insert(Employee employee)
        {
            await this.service.Insert(employee);
            this.employees.Add(employee);
            this.FindAllByFilter();//trigerujemo azuriranje view iz view modela
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


    }
}
