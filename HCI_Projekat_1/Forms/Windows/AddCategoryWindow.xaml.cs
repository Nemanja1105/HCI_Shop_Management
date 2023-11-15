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
    /// Interaction logic for AddCategoryWindow.xaml
    /// </summary>
    public partial class AddCategoryWindow : Window
    {

        public Category Category { get; set; }
        public AddCategoryWindow()
        {
            InitializeComponent();
            this.Category = new Category();
        }

        public AddCategoryWindow(Category category)
        {
            InitializeComponent();
            this.Category = new Category { Id = category.Id, Name = category.Name };
            this.buttonText.Text = "Update";
            buttonImage.Source = new BitmapImage(new Uri("/Images/update.png", UriKind.Relative));
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Category = null;
            this.Close();
        }

        private void addUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty((string)this.Category.Name))
            {
                MessageBox.Show("Sva polja forme moraju biti validno popunjena", "Greska pri unosu", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = Category;
        }
    }
}
