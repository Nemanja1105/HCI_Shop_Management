using HCI_Projekat_1.Models;
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
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
        private List<String> units = new(Enum.GetNames<UnitOfMeasure>());
      

        public AddProductWindow(List<Category> categories)
        {
            InitializeComponent();
            Product = new Product();
            Categories = categories;
            categoryCombo.ItemsSource = Categories;
            unitCombo.ItemsSource = units;
            this.DataContext = Product;


        }

        public AddProductWindow(List<Category> categories, Product product)
        {
            InitializeComponent();
            this.Product = new Product(product);
            this.mainLabel.Content = "Update product";
            Categories = categories;
            categoryCombo.ItemsSource = Categories;
            unitCombo.ItemsSource = units;
            this.DataContext = Product;
            this.buttonText.Text = "Update";
            buttonImage.Source = new BitmapImage(new Uri("/Images/update.png", UriKind.Relative));


        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Product = null;
            this.Close();
        }

        private void addupdateButton_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(Product.Name) || Product.Quantity <= 0.0M || string.IsNullOrEmpty(Product.Barkod) || string.IsNullOrEmpty(Product.UnitOfMeasure) ||
                Product.PurchasePrice <= 0.0M || Product.SellingPrice <= 0.0M || Product.Category == null || Product.UnitOfMeasure==null)
            {
                MessageBox.Show("Sva polja forme moraju biti validno popunjena", "Greska pri unosu", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                decimal.Parse(quantityTextBox.Text);
                decimal.Parse(purcTextBox.Text);
                decimal.Parse(sellTextBox.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Sva polja forme moraju biti validno popunjena36", "Greska pri unosu", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            this.Close();
        }
    }
}
