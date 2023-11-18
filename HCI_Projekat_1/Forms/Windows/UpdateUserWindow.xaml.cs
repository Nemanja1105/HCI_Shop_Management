using HCI_Projekat_1.Models;
using HCI_Projekat_1.Util;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace HCI_Projekat_1.Forms.Windows
{
    /// <summary>
    /// Interaction logic for UpdateUserWindow.xaml
    /// </summary>
    public partial class UpdateUserWindow : Window
    {
        public Employee Employee { get; set; }
        private bool isClosedByButton = false;
        public UpdateUserWindow(Employee employee)
        {
            InitializeComponent();
            Employee = employee;
            this.DataContext = Employee;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Employee = null;
            isClosedByButton = true;
            this.Close();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(nameTextBox.Text) || string.IsNullOrEmpty(surnameTextBox.Text) || string.IsNullOrEmpty(jmbTextBox.Text) ||
                string.IsNullOrEmpty(addressTextBox.Text) || string.IsNullOrEmpty(phoneNumberTextBox.Text))
            {
                MessageBox.Show(LanguageUtil.GetTranslation("FormNotValid"), LanguageUtil.GetTranslation("InputError"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Employee.Name = nameTextBox.Text;
            Employee.Surname = surnameTextBox.Text;
            Employee.Jmb = jmbTextBox.Text;
            Employee.Adresa = addressTextBox.Text;
            Employee.PhoneNumber = phoneNumberTextBox.Text;
            isClosedByButton = true;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isClosedByButton)
            {
                e.Cancel = true;
            }
        }
    }
}
