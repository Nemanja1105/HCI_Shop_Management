using HCI_Projekat_1.Models;
using HCI_Projekat_1.Models.Enums;
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
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public Employee Employee { get; set; } = null;
        private List<UserType> userTypes = new List<UserType> { UserType.Worker,UserType.Manager};
        private bool isClosedByButton = false;
        public AddUserWindow()
        {
            InitializeComponent();
            initUserCombo();
            //roleComboBox.DataContext = userTypes;
        }

        private void initUserCombo()
        {
            var map = new Dictionary<UserType, string>();
            foreach (var userType in userTypes)
            {
                map.Add(userType, LanguageUtil.GetTranslation(userType.ToString()));
            }
            roleComboBox.ItemsSource = map;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Employee = null;
            isClosedByButton = true;
            this.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(usernameTextBox.Text) || string.IsNullOrEmpty(passwordTextBox.Text) || String.IsNullOrEmpty(repeatPasswordTextBox.Text) ||
                string.IsNullOrEmpty(nameTextBox.Text) || string.IsNullOrEmpty(surnameTextBox.Text) || string.IsNullOrEmpty(jmbTextBox.Text) ||
                string.IsNullOrEmpty(addressTextBox.Text) || string.IsNullOrEmpty(numberTextBox.Text))
            {
                MessageBox.Show(LanguageUtil.GetTranslation("FormNotValid"), LanguageUtil.GetTranslation("InputError"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (passwordTextBox.Text != repeatPasswordTextBox.Text)
            {
                MessageBox.Show(LanguageUtil.GetTranslation("FormNotValid"), LanguageUtil.GetTranslation("InputError"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            bool role = ((UserType)roleComboBox.SelectedValue) == UserType.Manager;
            Employee=new Employee { Username = usernameTextBox.Text, Password = passwordTextBox.Text,Name=nameTextBox.Text,Surname=surnameTextBox.Text,
            Jmb=jmbTextBox.Text,Adresa=addressTextBox.Text,PhoneNumber=numberTextBox.Text,Uloga=role};
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
