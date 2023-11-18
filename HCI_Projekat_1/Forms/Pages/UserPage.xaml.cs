using HCI_Projekat_1.Forms.Windows;
using HCI_Projekat_1.Models;
using HCI_Projekat_1.Models.Enums;
using HCI_Projekat_1.Util;
using HCI_Projekat_1.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_Projekat_1.Forms.Pages
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private EmployeeViewModel employeeViewModel = new EmployeeViewModel();
        private List<UserType> userTypes = Enum.GetValues(typeof(UserType)).Cast<UserType>().ToList();
        private bool isComboBoxInitialized = false;
        public UserPage()
        {
            InitializeComponent();
            initUserCombo();
        }

        private void initUserCombo()
        {
            var map=new Dictionary<UserType,string>();
            foreach (var userType in userTypes)
            {
                map.Add(userType, LanguageUtil.GetTranslation(userType.ToString()));
            }
            userTypeCombo.ItemsSource = map;
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await InitializeAsync();
            isComboBoxInitialized = true;
        }

        private async Task InitializeAsync()
        {
            try
            {
                await employeeViewModel.FindAll();
            }
            catch (Exception e)
            {
                MessageBox.Show(LanguageUtil.GetTranslation("DbExceptionMain"), LanguageUtil.GetTranslation("DbExceptionMessage"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.DataContext = employeeViewModel;
        }



        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddUserWindow();
            dialog.ShowDialog();
            if (dialog.Employee == null)
                return;
            try
            {
                await employeeViewModel.Insert(dialog.Employee);
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageUtil.GetTranslation("DbExceptionMain"), LanguageUtil.GetTranslation("DbExceptionMessage"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async void updateButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Employee)userGrid.SelectedValue;
            if (selected != null)
            {
                var updateWindows = new UpdateUserWindow(selected);
                updateWindows.ShowDialog();
                userGrid.UnselectAll();
                var updated = updateWindows.Employee;
                if (updated != null)
                {
                    try
                    {
                        this.employeeViewModel.Update(updated);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(LanguageUtil.GetTranslation("DbExceptionMain"), LanguageUtil.GetTranslation("DbExceptionMessage"), MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }


        }

        private async void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Employee)userGrid.SelectedValue;
            if (selected != null)
            {
                var Result = MessageBox.Show(LanguageUtil.GetTranslation("DeleteUserQues"), LanguageUtil.GetTranslation("DeleteUser"), MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (Result == MessageBoxResult.Yes)
                {
                    try
                    {
                        await employeeViewModel.Delete(selected);
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(LanguageUtil.GetTranslation("DbExceptionMain"), LanguageUtil.GetTranslation("DbExceptionMessage"), MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                userGrid.UnselectAll();
            }
        }
    }
}
