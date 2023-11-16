using HCI_Projekat_1.Models;
using HCI_Projekat_1.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_Projekat_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LoginViewModel loginViewModel=new LoginViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = loginViewModel;
            new ShopManagementContext().Database.EnsureCreatedAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            loginViewModel.LoginDTO.Password=passwordBox.Password;
            if (string.IsNullOrEmpty(loginViewModel.LoginDTO.Username) || string.IsNullOrEmpty(loginViewModel.LoginDTO.Password))
            {
                MessageBox.Show("Sva polja moraju biti validno popunjena","Greska prilikom prijave",MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
           // try
            //{

                var result = loginViewModel.Login();
                if (result != null)
                {
                    new ManagerMain(result).Show();
                    this.Close();
                }
                else
                    MessageBox.Show("Korisnicko ime ili lozinka nisu ispravni", "Pogresni kredencijali", MessageBoxButton.OK, MessageBoxImage.Error);
          /*  }
            catch(Exception ex)
            {
                MessageBox.Show("Desila se greska prilikom komunikacije sa bazom podataka", "Greska u komunikaciji", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
        }
    }
}
