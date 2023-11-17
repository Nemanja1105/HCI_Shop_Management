using HCI_Projekat_1.Models;
using HCI_Projekat_1.Services;
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
    /// Interaction logic for BillDetailsWindow.xaml
    /// </summary>
    public partial class BillDetailsWindow : Window
    {
        private Bill bill;
        private BillService service = new BillService();
        public BillDetailsWindow(Bill bill)
        {
            InitializeComponent();
            this.bill = bill;

        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async Task InitializeAsync()
        {
            try
            {
             bill.Billitem=  await service.FindAllItemForBill(this.bill);
             bill.Canceledbill=await service.FindCanceledForBill(this.bill);
            }
            catch (Exception e)
            {
                MessageBox.Show("Desila se greska prilikom komunikacije sa bazom podataka", "Greska u komunikaciji", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.DataContext = bill;
            this.billGrid.DataContext = bill.Billitem;
            cancelGrid.DataContext = bill;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
          await  InitializeAsync();
        }
    }
}
