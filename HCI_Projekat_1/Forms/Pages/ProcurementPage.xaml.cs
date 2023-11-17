using HCI_Projekat_1.Forms.Windows;
using HCI_Projekat_1.Models;
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
    /// Interaction logic for ProcurementPage.xaml
    /// </summary>
    public partial class ProcurementPage : Page
    {
        private ProcurementViewModel procurementViewModel = new ProcurementViewModel(); 
        public ProcurementPage()
        {
            InitializeComponent();

        

     
        }

        private void viewButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Procurement)procurementGrid.SelectedValue;
            if (selected != null)
            {
                new ProcurementDetailsWindow(selected).Show();
                procurementGrid.UnselectAll();
            }
        }

        private async void createButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CreateProcurementWindow();
            dialog.ShowDialog();
            if (dialog.Procurement == null)
                return;
            try
            {
                await procurementViewModel.Insert(dialog.Procurement);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Desila se greska prilikom komunikacije sa bazom podataka", "Greska u komunikaciji", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            try
            {
                await procurementViewModel.FindAll();
                procurementViewModel.FindAllByFilter();
            }
            catch (Exception e)
            {
                MessageBox.Show("Desila se greska prilikom komunikacije sa bazom podataka", "Greska u komunikaciji", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.DataContext = procurementViewModel;
        }
    }
}
