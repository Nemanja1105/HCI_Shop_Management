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
    /// Interaction logic for CancelBillWindow.xaml
    /// </summary>
    public partial class CancelBillWindow : Window
    {
        private bool isClosedByButton = false;
        public Canceledbill Canceledbill { get; set; }
        public CancelBillWindow(Bill bill)
        {
            InitializeComponent();
            this.Canceledbill=new Canceledbill { BillId = bill.Id,Bill=bill };
            this.DataContext = Canceledbill;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isClosedByButton)
            {
                e.Cancel = true;
            }
        }

        private void addUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty((string)this.Canceledbill.Reason))
            {
                MessageBox.Show(LanguageUtil.GetTranslation("FormNotValid"), LanguageUtil.GetTranslation("InputError"), MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Canceledbill.Date = DateTime.Now;
            Canceledbill.Employee=ManagerMain.Employee;
            Canceledbill.EmployeeId = ManagerMain.Employee.Id;
            isClosedByButton = true;
            this.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Canceledbill = null;
            this.isClosedByButton = true;
            this.Close();
        }
    }
}
