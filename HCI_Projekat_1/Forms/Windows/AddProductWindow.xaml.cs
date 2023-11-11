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
        private List<Category> categories = new() { new Category("Voce"),new Category("Cokolada")};
        private List<String> units = new(Enum.GetNames<UnitOfMeasure>());

        public AddProductWindow()
        {
            InitializeComponent();
            Product = new Product();
            categoryCombo.ItemsSource = categories;
            unitCombo.ItemsSource = units;
            this.DataContext = Product;
            

        }

        public AddProductWindow(Product product)
        {
            InitializeComponent();
            this.Product = new Product(product);
            this.mainLabel.Content = "Update product";
            categoryCombo.ItemsSource = categories;
            unitCombo.ItemsSource = units;
            this.DataContext = Product;
            
           
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
