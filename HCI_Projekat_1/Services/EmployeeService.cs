using HCI_Projekat_1.Exceptions;
using HCI_Projekat_1.Models;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Pkcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat_1.Services
{
    internal class EmployeeService
    {
        private readonly string DEFAULT_THEME = "OrangeTheme";
        private readonly string DEFAULT_LANGUAGE = "SerbianLanguage";

        public async Task<List<Employee>> FindAll()
        {
            using (var dbContext = new ShopManagementContext())
                return await dbContext.Employee.AsNoTracking().ToListAsync();
        }

        public async Task Delete(Employee employee)
        {
            using (var dbContext = new ShopManagementContext())
            {
                dbContext.Employee.Remove(employee);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task Update(Employee request)
        {
            using(var dbContext = new ShopManagementContext())
            {
                var employee = await dbContext.Employee.FirstOrDefaultAsync(user=>user.Id==request.Id);
                if (!string.IsNullOrEmpty(request.Name))
                    employee.Name = request.Name;
                if(!string.IsNullOrEmpty(request.Surname))
                    employee.Surname = request.Surname;
                if(!string.IsNullOrEmpty(request.Jmb))
                    employee.Jmb = request.Jmb;
                if(!string.IsNullOrEmpty(request.Adresa))
                    employee.Adresa = request.Adresa;
                if(!string.IsNullOrEmpty (request.PhoneNumber))
                    employee.PhoneNumber = request.PhoneNumber;
                if(!string.IsNullOrEmpty(request.Theme))
                    employee.Theme = request.Theme;
                if (!string.IsNullOrEmpty(request.Language))
                    employee.Language = request.Language;
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task Insert(Employee employee)
        {
            employee.Theme = DEFAULT_THEME;
            employee.Language = DEFAULT_LANGUAGE;
            employee.Password = GetSha512Hash(employee.Password);
            using (var dbContext = new ShopManagementContext())
            {
                await dbContext.Employee.AddAsync(employee);
                await dbContext.SaveChangesAsync();
            }
        }

        public Employee Login(LoginDTO loginDTO)
        {
            loginDTO.Password=GetSha512Hash(loginDTO.Password);
            using (var dbContext=new ShopManagementContext()){
                var employee = dbContext.Employee.FirstOrDefault(e=>e.Username == loginDTO.Username && e.Password==loginDTO.Password);
                return employee;
            }
        }

        public async Task ChangePassword(String oldPassword,String newPassword,int id)
        {
            using (var dbContext = new ShopManagementContext())
            {
                var employee = await dbContext.Employee.FirstOrDefaultAsync(e => e.Id==id);
                if (employee.Password != GetSha512Hash(oldPassword))
                    throw new PasswordMismatchException();
                employee.Password = GetSha512Hash(newPassword);
               await dbContext.SaveChangesAsync();   
            }
        }

        private String GetSha512Hash(String text)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(text);
                byte[] hashBytes = sha512.ComputeHash(sourceBytes);
                return BitConverter.ToString(hashBytes).Replace("-", String.Empty);
            }
        }
    }
}
