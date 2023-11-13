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
    /// Interaction logic for ProcurementDetailsWindow.xaml
    /// </summary>
    public partial class ProcurementDetailsWindow : Window
    {
        private Procurement procurement;
        public ProcurementDetailsWindow(Procurement procurement)
        {
            InitializeComponent();
            this.procurement= procurement;
            this.DataContext = procurement;
            this.procurementGrid.DataContext = procurement.Procurementitem;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
