using HCI_Projekat_1.Forms.Windows;
using HCI_Projekat_1.Models;
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
        private ObservableCollection<Employee> data;
        public UserPage()
        {
            InitializeComponent();
            data = new ObservableCollection<Employee>();
            data.Add(new Employee { Id = 1, Username = "Marko123", Name = "Marko", Surname = "Markovic", Jmb = "1234567890123", Adresa = "Beogradska 3", PhoneNumber = "066391652", Uloga = true });
            data.Add(new Employee { Id = 2, Username = "Marko123", Name = "Pera", Surname = "Peric", Jmb = "1234567890123", Adresa = "Beogradska 4", PhoneNumber = "066391652", Uloga = false });
            data.Add(new Employee { Id = 3, Username = "Marko123", Name = "Marko", Surname = "Markovic", Jmb = "1234567890123", Adresa = "Beogradska 3", PhoneNumber = "066391652", Uloga = true });
            data.Add(new Employee { Id = 4, Username = "Marko123", Name = "Pera", Surname = "Peric", Jmb = "1234567890123", Adresa = "Beogradska 4", PhoneNumber = "066391652", Uloga = false });
            userGrid.DataContext = data;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddUserWindow().ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Employee)userGrid.SelectedValue;
            if (selected != null)
            {
                var updateWindows = new UpdateUserWindow(selected);
                updateWindows.ShowDialog();
                userGrid.UnselectAll();
                var updated = updateWindows.Employee;
                MessageBox.Show(updated.Name);
            }


        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Employee)userGrid.SelectedValue;
            if (selected != null)
            {
                var Result = MessageBox.Show("Are you sure you want to delete the user?", "Delete user?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (Result == MessageBoxResult.Yes)
                    data.Remove(selected);
            }
        }
    }
}
