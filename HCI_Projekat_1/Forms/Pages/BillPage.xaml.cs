using HCI_Projekat_1.Forms.Windows;
using HCI_Projekat_1.Models;
using HCI_Projekat_1.Models.Enums;
using HCI_Projekat_1.Util;
using HCI_Projekat_1.ViewModel;
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
    /// Interaction logic for BillPage.xaml
    /// </summary>
    public partial class BillPage : Page
    {

       /* private List<String> paymentTypes = new(Enum.GetNames<PaymentType>().Append("All"));
        private ObservableCollection<Bill> bills = new ObservableCollection<Bill>();*/
       private BillViewModel billViewModel=new BillViewModel();
        public BillPage()
        {
            InitializeComponent();
            initUserCombo();
            if(ManagerMain.Employee.Uloga)
            {
                cancelButton.Visibility=Visibility.Collapsed;
                createButton.Visibility=Visibility.Collapsed;
            }
        }

        private void initUserCombo()
        {
            var map = new Dictionary<PaymentType, string>();
            foreach (var payment in billViewModel.PaymentTypes)
            {
                map.Add(payment, LanguageUtil.GetTranslation(payment.ToString()));
            }
            paymentCombo.ItemsSource = map;
        }

        private async Task InitializeAsync()
        {
            try
            {
                await billViewModel.FindAll();
                billViewModel.FindAllByFilter();
            }
            catch (Exception e)
            {
                MessageBox.Show(LanguageUtil.GetTranslation("DbExceptionMain"), LanguageUtil.GetTranslation("DbExceptionMessage"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.DataContext = billViewModel;
        }

        private void viewButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Bill)billGrid.SelectedValue;
            if (selected != null)
            {
                new BillDetailsWindow(selected).Show();
                billGrid.UnselectAll();
            }
        }

        private async void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
           await InitializeAsync();
        }

        private async void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Bill)billGrid.SelectedValue;
            if (selected != null)
            {
                if (selected.isCanceled)
                    return;
                var cancelWindow = new CancelBillWindow(selected);
                cancelWindow.ShowDialog();
                billGrid.UnselectAll();
                var updated = cancelWindow.Canceledbill;
                if (updated != null)
                {
                    try
                    {
                        await this.billViewModel.CancelBill(updated);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(LanguageUtil.GetTranslation("DbExceptionMain"), LanguageUtil.GetTranslation("DbExceptionMessage"), MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
