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
    /// Interaction logic for AddSuplierWindow.xaml
    /// </summary>
    public partial class AddSuplierWindow : Window
    {
        public Supplier Supplier { get; set; }
        private bool isClosedByButton = false;
        public AddSuplierWindow()
        {
            InitializeComponent();
            this.Supplier = new Supplier();
        }

        public AddSuplierWindow(Supplier supplier)
        {
            InitializeComponent();
            this.Supplier = new Supplier { Id = supplier.Id, Name = supplier.Name,Address=supplier.Address,PhoneNumber=supplier.PhoneNumber };
            this.buttonText.Text = LanguageUtil.GetTranslation("Update");
            buttonImage.Source = new BitmapImage(new Uri("/Images/update.png", UriKind.Relative));
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Supplier = null;
            isClosedByButton = true;
            this.Close();
        }

        private void addUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty((string)this.Supplier.Name)||string.IsNullOrEmpty(this.Supplier.Address)||string.IsNullOrEmpty(this.Supplier.PhoneNumber))
            {
                MessageBox.Show(LanguageUtil.GetTranslation("FormNotValid"), LanguageUtil.GetTranslation("InputError"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            isClosedByButton = true;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = Supplier;
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
