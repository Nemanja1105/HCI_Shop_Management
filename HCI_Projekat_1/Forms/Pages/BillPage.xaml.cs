using HCI_Projekat_1.Forms.Windows;
using HCI_Projekat_1.Models;
using HCI_Projekat_1.Models.Enums;
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
            paymentCombo.ItemsSource = billViewModel.PaymentTypes;
          /*  paymentCombo.ItemsSource = paymentTypes;
            bills.Add(new Bill { Id = 1, DateOfIssue = DateTime.Now, PaymentType = true, TotalPrice = 123M, Employee = new Employee { Name = "Miki", Surname = "Mikic" } });
            bills.Add(new Bill { Id = 2, DateOfIssue = DateTime.Now, PaymentType = false, TotalPrice = 23M, Employee = new Employee { Name = "Miki", Surname = "Mikic" } });
            var tmp = new Bill { Id = 3, DateOfIssue = DateTime.Now, PaymentType = true, TotalPrice = 123M, Employee = new Employee { Name = "Maja", Surname = "Polic" } };
            tmp.Billitem.Add(new Billitem { Id = 1, Quantity = 2M, Price = 1.4M, Product = new Product { Name = "Cokolada Milka" } });
            tmp.Canceledbill.Add(new Canceledbill { Reason="Greska u stampi, nestalo papira, internet pukao sve otislo u helac",Employee=new Employee { Name="Marko",Surname="Markovic"},Date=DateTime.Now });
            bills.Add(tmp);
            billGrid.DataContext = bills;*/
        }

        private async Task InitializeAsync()
        {
            try
            {
                await billViewModel.FindAll();
            }
            catch (Exception e)
            {
                MessageBox.Show("Desila se greska prilikom komunikacije sa bazom podataka", "Greska u komunikaciji", MessageBoxButton.OK, MessageBoxImage.Error);
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
            InitializeAsync();
        }
    }
}
