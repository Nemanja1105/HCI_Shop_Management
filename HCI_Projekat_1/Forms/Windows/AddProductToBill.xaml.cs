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
    /// Interaction logic for AddProductToBill.xaml
    /// </summary>
    public partial class AddProductToBill : Window
    {
        public decimal? Quantity { get; set; } = 1.0m;

        bool isClosedByButton = false;
        public AddProductToBill()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isClosedByButton)
            {
                e.Cancel = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Quantity = decimal.Parse(quantityTextBox.Text);
                if (Quantity <= 0)
                {
                    quantityTextBox.Clear();
                    MessageBox.Show(LanguageUtil.GetTranslation("PriceQuantityError"), LanguageUtil.GetTranslation("InputError"), MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                isClosedByButton = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageUtil.GetTranslation("PriceQuantityError"), LanguageUtil.GetTranslation("InputError"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Quantity = null;
            isClosedByButton = true;
            this.Close();
        }
    }
}
