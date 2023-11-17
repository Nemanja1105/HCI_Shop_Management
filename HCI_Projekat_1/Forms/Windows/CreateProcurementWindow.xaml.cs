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
using System.Windows.Shapes;

namespace HCI_Projekat_1.Forms.Windows
{
    /// <summary>
    /// Interaction logic for CreateProcurementWindow.xaml
    /// </summary>
    public partial class CreateProcurementWindow : Window
    {
        private ProcurementItemsViewModel procurementViewModel = new ProcurementItemsViewModel();
        private bool isClosedByButton = false;
        public Procurement Procurement { get; set; }
        public CreateProcurementWindow()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            isClosedByButton = true;
            Procurement = null;
            this.Close();
        }

        private void transferButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Product)currProductGrid.SelectedValue;
            if (selected != null)
            {
                var dialog = new AddProductToProcurement();
                dialog.ShowDialog();
                currProductGrid.UnselectAll();
                if (!dialog.Quantity.HasValue || !dialog.Price.HasValue)
                    return;
                procurementViewModel.Insert(selected, dialog.Price.Value, dialog.Quantity.Value);

            }
            else
            {
                var sel2 = (Procurementitem)procurementGrid.SelectedValue;
                if (sel2 != null)
                {
                    procurementViewModel.Remove(sel2);
                    procurementGrid.UnselectAll();
                }
            }
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            if(procurementViewModel.Supplier==null || procurementViewModel.Procurementitems.Count == 0)
            {
                MessageBox.Show("Sva polja forme moraju biti validno popunjena", "Greska pri unosu", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Procurement = procurementViewModel.CreateProcurement();
            isClosedByButton = true;
            this.Close();
        }

        private async Task InitializeAsync()
        {
            try
            {
                await this.procurementViewModel.LoadAllData();
            }
            catch (Exception e)
            {
                MessageBox.Show("Desila se greska prilikom komunikacije sa bazom podataka", "Greska u komunikaciji", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            currProductGrid.DataContext = procurementViewModel.Products;
            procurementGrid.DataContext = procurementViewModel.Procurementitems;
            totalLabel.DataContext = procurementViewModel;
            supplierCombo.ItemsSource = procurementViewModel.Suppliers;
            supplierCombo.DataContext = procurementViewModel;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeAsync();
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
