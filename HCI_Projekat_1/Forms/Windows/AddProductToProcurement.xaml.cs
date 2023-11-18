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
    /// Interaction logic for AddProductToProcurement.xaml
    /// </summary>
    public partial class AddProductToProcurement : Window
    {
        public decimal? Quantity { get; set; } = 1.0m;
        public decimal? Price { get; set; } = 1.0m;

        bool isClosedByButton = false;
        public AddProductToProcurement()
        {
            InitializeComponent();

        }

       



        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Quantity = Price = null;
            isClosedByButton = true;
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Quantity = decimal.Parse(quantityTextBox.Text);
                Price = decimal.Parse(priceTextBox.Text);
                isClosedByButton = true;
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(LanguageUtil.GetTranslation("PriceQuantityError"), LanguageUtil.GetTranslation("InputError"),MessageBoxButton.OK,MessageBoxImage.Error);
            }
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
