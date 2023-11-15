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
        private List<Product> products = new List<Product>();
        private ProcurementItemsViewModel procurementViewModel=new ProcurementItemsViewModel();
        private List<Supplier> suppliers = new List<Supplier>();
        public CreateProcurementWindow()
        {
            InitializeComponent();
           /* products.Add(new Product { Id = 1, Name = "Jabuke", Quantity = 8.0M, Barkod = "123456", UnitOfMeasure = UnitOfMeasure.Kg.ToString(), PurchasePrice = 1.4M, SellingPrice = 2.4M, Category = new Supplier("Voce") });
            products.Add(new Product { Id = 2, Name = "Kruske", Quantity = 18.0M, Barkod = "12345698", UnitOfMeasure = "Kg", PurchasePrice = 2.4M, SellingPrice = 3.4M, Category = new Supplier("Voce") });
            products.Add(new Product { Id = 3, Name = "Milka mlijecna", Quantity = 20.0M, Barkod = "223456", UnitOfMeasure = "Kom", PurchasePrice = 1.0M, SellingPrice = 1.4M, Category = new Supplier("Cokolada") });
            products.Add(new Product { Id = 4, Name = "Najljepse zelje", Quantity = 20.0M, Barkod = "2235456", UnitOfMeasure = "Kom", PurchasePrice = 0.90M, SellingPrice = 1.25M, Category = new Supplier("Cokolada") });
            suppliers.Add(new Supplier { Id = 1, Name = "Takovo", Address = "Trn 12", PhoneNumber = "056434567" });
            suppliers.Add(new Supplier { Id = 2, Name = "Podravka", Address = "Zagreg 2", PhoneNumber = "056589667" });
            suppliers.Add(new Supplier { Id = 3, Name = "Vitaminka", Address = "Banja Luka 12", PhoneNumber = "0214567" });
            suppliers.Add(new Supplier { Id = 4, Name = "Semberija", Address = "Biljena 45", PhoneNumber = "0559687" });
            currProductGrid.DataContext = products;
            procurementGrid.DataContext = procurementViewModel.Procurementitems;
            totalLabel.DataContext = procurementViewModel;
            supplierCombo.ItemsSource = suppliers;
            supplierCombo.DataContext = procurementViewModel.Supplier;*/
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void transferButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Product)currProductGrid.SelectedValue;
            if (selected != null)
            {
                var dialog = new AddProductToProcurement();
                dialog.ShowDialog();
                if (dialog.Quantity <= 0)
                    return;
                var tmp = procurementViewModel.Procurementitems.FirstOrDefault(item => item.Product.Id == selected.Id);
                if (tmp == null)
                {
                    var item = new Procurementitem { Price = selected.PurchasePrice, Quantity = dialog.Quantity, Product = selected };
                    procurementViewModel.Procurementitems.Add(item);
                    procurementViewModel.TotalPrice += item.TotalPrice;
                }
                else
                {
                    tmp.Quantity += dialog.Quantity;
                    procurementViewModel.TotalPrice += dialog.Quantity * tmp.Price;
                }
                procurementGrid.UnselectAll();
            }
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
