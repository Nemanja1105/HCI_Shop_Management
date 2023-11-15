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
    /// Interaction logic for AddSuplierWindow.xaml
    /// </summary>
    public partial class AddSuplierWindow : Window
    {
        public Supplier Supplier { get; set; }
        public AddSuplierWindow()
        {
            InitializeComponent();
            this.Supplier = new Supplier();
        }

        public AddSuplierWindow(Supplier supplier)
        {
            InitializeComponent();
            this.Supplier = new Supplier { Id = supplier.Id, Name = supplier.Name,Address=supplier.Address,PhoneNumber=supplier.PhoneNumber };
            this.buttonText.Text = "Update";
            buttonImage.Source = new BitmapImage(new Uri("/Images/update.png", UriKind.Relative));
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Supplier = null;
            this.Close();
        }

        private void addUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty((string)this.Supplier.Name)||string.IsNullOrEmpty(this.Supplier.Address)||string.IsNullOrEmpty(this.Supplier.PhoneNumber))
            {
                MessageBox.Show("Sva polja forme moraju biti validno popunjena"+Supplier.Name+"/"+Supplier.Address+"/"+Supplier.PhoneNumber, "Greska pri unosu", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = Supplier;
        }

       
    }
}
