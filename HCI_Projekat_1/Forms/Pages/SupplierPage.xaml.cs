using HCI_Projekat_1.Forms.Windows;
using HCI_Projekat_1.Models;
using HCI_Projekat_1.ViewModel;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
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
    /// Interaction logic for SupplierPage.xaml
    /// </summary>
    public partial class SupplierPage : Page
    {
        private SupplierViewModel supplierViewModel=new SupplierViewModel();
        public SupplierPage()
        {
            InitializeComponent();
            
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeAsync();

        }
        private async Task InitializeAsync()
        {
            try
            {
                await supplierViewModel.FindAll();
            }
            catch (Exception e)
            {
                MessageBox.Show("Desila se greska prilikom komunikacije sa bazom podataka", "Greska u komunikaciji", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.DataContext = supplierViewModel;
        }

        private async void addButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddSuplierWindow();
            dialog.ShowDialog();
            if (dialog.Supplier == null)
                return;
            try
            {
                await supplierViewModel.Insert(dialog.Supplier);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Desila se greska prilikom komunikacije sa bazom podataka", "Greska u komunikaciji", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Supplier)supplierGrid.SelectedValue;
            if (selected != null)
            {
                var Result = MessageBox.Show("Are you sure you want to delete the supplier??", "Delete category?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (Result == MessageBoxResult.Yes)
                {
                    try
                    {
                        await supplierViewModel.Delete(selected);

                    }
                    catch (DbUpdateException ex)
                    {
                        if (ex.InnerException is MySqlException mySqlEx && mySqlEx.Number == 1451)
                        {
                            MessageBox.Show("Dobavljac je povezan sa drugim entitetima u sistemu. Brisanje nije dozvoljeno.", "Brisanje nije moguce", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                            MessageBox.Show("Desila se greska prilikom komunikacije sa bazom podataka", "Greska u komunikaciji", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                supplierGrid.UnselectAll();
            }
        }

        private async void updateButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Supplier)supplierGrid.SelectedValue;
            if (selected != null)
            {
                var updateWindows = new AddSuplierWindow(selected);
                updateWindows.ShowDialog();
                supplierGrid.UnselectAll();
                var updated = updateWindows.Supplier;
                if (updated != null)
                {
                    try
                    {
                        await this.supplierViewModel.Update(updated);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Desila se greska prilikom komunikacije sa bazom podataka", "Greska u komunikaciji", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
