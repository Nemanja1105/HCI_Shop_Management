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
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        private ObservableCollection<Product> products;
        public ProductPage()
        {
            InitializeComponent();
            products = new ObservableCollection<Product>();
            products.Add(new Product{ Id=1,Name="Jabuke", Quantity =8.0M, Barkod ="123456", UnitOfMeasure =UnitOfMeasure.Kg.ToString(), PurchasePrice =1.4M, SellingPrice =2.4M, Category =new Category("Voce")});
            products.Add(new Product { Id = 2, Name = "Kruske", Quantity = 18.0M, Barkod = "12345698", UnitOfMeasure = "Kg", PurchasePrice = 2.4M, SellingPrice = 3.4M, Category = new Category("Voce") });
            products.Add(new Product { Id = 3, Name = "Milka mlijecna", Quantity = 20.0M, Barkod = "223456", UnitOfMeasure = "Kom", PurchasePrice = 1.0M, SellingPrice = 1.4M, Category = new Category("Cokolada") });
            products.Add(new Product { Id = 4, Name = "Najljepse zelje", Quantity = 20.0M, Barkod = "2235456", UnitOfMeasure = "Kom", PurchasePrice = 0.90M, SellingPrice = 1.25M, Category = new Category("Cokolada") });
            productGrid.DataContext = products;
            
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            new AddProductWindow().ShowDialog();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Product)productGrid.SelectedValue;
            if (selected != null)
            {
                var Result = MessageBox.Show("Are you sure you want to delete the product?", "Delete product?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (Result == MessageBoxResult.Yes)
                    products.Remove(selected);
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Product)productGrid.SelectedValue;
            if (selected != null)
            {
                var updateWindows = new AddProductWindow(selected);
                updateWindows.ShowDialog();
                productGrid.UnselectAll();
            }


        }
    }
}
