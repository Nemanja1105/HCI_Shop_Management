using HCI_Projekat_1.Models;
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
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
        private List<String> units = new(Enum.GetNames<UnitOfMeasure>());
        private bool isClosedByButton = false;



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
            this.mainLabel.Content = LanguageUtil.GetTranslation("UpdateProduct");
            Categories = categories;
            categoryCombo.ItemsSource = Categories;
            unitCombo.ItemsSource = units;
            this.DataContext = Product;
            this.Title = LanguageUtil.GetTranslation("UpdateProduct");
            this.buttonText.Text = LanguageUtil.GetTranslation("Update");
            buttonImage.Source = new BitmapImage(new Uri("/Images/update.png", UriKind.Relative));


        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Product = null;
            isClosedByButton = true;
            this.Close();
        }

        private void addupdateButton_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(Product.Name) || Product.Quantity <= 0.0M || string.IsNullOrEmpty(Product.Barkod) || string.IsNullOrEmpty(Product.UnitOfMeasure) ||
                Product.PurchasePrice <= 0.0M || Product.SellingPrice <= 0.0M || Product.Category == null || Product.UnitOfMeasure==null)
            {
                MessageBox.Show(LanguageUtil.GetTranslation("FormNotValid"), LanguageUtil.GetTranslation("InputError"), MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show(LanguageUtil.GetTranslation("FormNotValid"), LanguageUtil.GetTranslation("InputError"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
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
