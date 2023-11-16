using HCI_Projekat_1.Models;
using HCI_Projekat_1.Services;
using HCI_Projekat_1.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat_1.ViewModel
{
    internal class SettingsViewModel
    {
        private EmployeeService service=new EmployeeService();
        private Employee employee;
        public SettingsViewModel(Employee employee)
        {
            this.employee = employee;
            this.Theme = new Theme { Name = employee.Theme };
            this.Language = new Language {Name= employee.Language };
            this.Themes= ThemeUtil.GetThemes();
            this.Languages=LanguageUtil.GetLanguages();
        }
        public List<Theme> Themes { get; set; }
        public List<Language> Languages { get; set; }
        public Theme Theme { get; set; }
        public Language Language {  get; set; }
        

        public async Task ChangeTheme()
        {
            Employee request=new Employee { Id = employee.Id,Theme=Theme.Name };
            await this.service.Update(request);
            ThemeUtil.ChangeTheme(new Uri(Theme.Path));
        }

        public async Task ChangeLanguage()
        {
            Employee request = new Employee { Id = employee.Id, Language = Language.Name };
            await this.service.Update(request);
            employee.Language = Language.Name;
            LanguageUtil.ChangeLanguage(Language);
        }

        public async Task ChangePassword(String oldPassword,String newPassword)
        {
            await this.service.ChangePassword(oldPassword, newPassword, employee.Id);
        }
    }
}
