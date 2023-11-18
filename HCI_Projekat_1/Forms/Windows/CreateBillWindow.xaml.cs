using HCI_Projekat_1.Exceptions;
using HCI_Projekat_1.Models;
using HCI_Projekat_1.Util;
using HCI_Projekat_1.ViewModel;
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
    /// Interaction logic for CreateBillWindow.xaml
    /// </summary>
    public partial class CreateBillWindow : Window
    {
        private BillItemViewModel billItemViewModel=new BillItemViewModel();
        private bool isClosedByButton = false;
        public Bill Bill { get; set; }
        public CreateBillWindow()
        {
            InitializeComponent();
        }

        private void initUserCombo()
        {
            var map = new Dictionary<PaymentType, string>();
            foreach (var payment in billItemViewModel.PaymentTypes)
            {
                map.Add(payment, LanguageUtil.GetTranslation(payment.ToString()));
            }
            paymentCombo.ItemsSource = map;
            paymentCombo.DataContext = billItemViewModel;
        }

        private async Task InitializeAsync()
        {
            try
            {
                await this.billItemViewModel.LoadAllData();
                initUserCombo();
            }
            catch (Exception e)
            {
                MessageBox.Show(LanguageUtil.GetTranslation("DbExceptionMain"), LanguageUtil.GetTranslation("DbExceptionMessage"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            currProductGrid.DataContext = billItemViewModel.Products;
            billGrid.DataContext = billItemViewModel.BillItems;
            totalLabel.DataContext = billItemViewModel;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await InitializeAsync();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isClosedByButton)
            {
                e.Cancel = true;
            }
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            if (billItemViewModel.BillItems.Count == 0 || billItemViewModel.PaymentType==null)
            {
                MessageBox.Show(LanguageUtil.GetTranslation("FormNotValid"), LanguageUtil.GetTranslation("InputError"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Bill = billItemViewModel.CreateBill();
            isClosedByButton = true;
            this.Close();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            isClosedByButton = true;
            Bill = null;
            this.Close();
        }

        private void transferButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Product)currProductGrid.SelectedValue;
            if (selected != null)
            {
                var dialog = new AddProductToBill();
                dialog.ShowDialog();
                currProductGrid.UnselectAll();
                if (!dialog.Quantity.HasValue)
                    return;
                try
                {
                    billItemViewModel.Insert(selected, dialog.Quantity.Value);
                }
                catch(QuantityUnitException ex)
                {
                    MessageBox.Show(LanguageUtil.GetTranslation("QuantityUnitNotSupported"), LanguageUtil.GetTranslation("InputError"), MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch(QuantityExceededException ex)
                {
                    MessageBox.Show(LanguageUtil.GetTranslation("QuantityExceeded"),LanguageUtil.GetTranslation("InputError"),MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                var sel2 = (Billitem)billGrid.SelectedValue;
                if (sel2 != null)
                {
                    billItemViewModel.Remove(sel2);
                    billGrid.UnselectAll();
                }
            }
        }
    }
}
