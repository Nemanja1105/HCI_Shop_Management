using HCI_Projekat_1.Exceptions;
using HCI_Projekat_1.Models;
using HCI_Projekat_1.Util;
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

namespace HCI_Projekat_1.Forms.Pages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {

        private SettingsViewModel settingsViewModel = new SettingsViewModel(ManagerMain.Employee);
        private bool loaded = false;
        public SettingsPage()
        {
            InitializeComponent();
            this.DataContext = settingsViewModel;
        }

        private async void themeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loaded)
            {
                try
                {
                    await this.settingsViewModel.ChangeTheme();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Desila se greska prilikom komunikacije sa bazom podataka", "Greska u komunikaciji", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async void language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loaded)
            {
                try
                {
                    await this.settingsViewModel.ChangeLanguage();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Desila se greska prilikom komunikacije sa bazom podataka", "Greska u komunikaciji", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.loaded = true;
        }

        private async void updatePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(oldPasswordBox.Password) || string.IsNullOrEmpty(newPasswordBox.Password))
            {
                MessageBox.Show("Sva polja forme moraju biti validno popunjena", "Greska pri unosu", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                await this.settingsViewModel.ChangePassword(oldPasswordBox.Password, newPasswordBox.Password);
                MessageBox.Show("Lozinka uspjesno promjenjena", "Operacija uspjesna", MessageBoxButton.OK, MessageBoxImage.Information);
                oldPasswordBox.Clear();
                newPasswordBox.Clear();
            }
            catch(PasswordMismatchException ex)
            {
                MessageBox.Show("Lozinke se ne poklapaju. Pokusajte ponovo", "Izmjena lozinke neuspjesna", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Desila se greska prilikom komunikacije sa bazom podataka", "Greska u komunikaciji", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
