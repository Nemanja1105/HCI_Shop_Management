using HCI_Projekat_1.Forms.Windows;
using HCI_Projekat_1.Models;
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
    /// Interaction logic for ProcurementPage.xaml
    /// </summary>
    public partial class ProcurementPage : Page
    {
        private ObservableCollection<Procurement> procurements = new ObservableCollection<Procurement>();
        public ProcurementPage()
        {
            InitializeComponent();

            var tmp = new Procurement { Id = 1, DateOfAcquisition = DateTime.Now, TotalPrice = 1200M, Employee = new Employee { Name = "Marko" }, Supplier = new Supplier { Name = "Takovo" } };
            tmp.Procurementitem.Add(new Procurementitem { Id = 1, Price = 12M, Quantity = 34.0M, Product = new Product { Name = "Dzem" } });
            tmp.Procurementitem.Add(new Procurementitem { Id = 2, Price = 6M, Quantity = 147.0M, Product = new Product { Name = "Ajvar" } });
            tmp.Procurementitem.Add(new Procurementitem { Id = 2, Price = 10M, Quantity = 17.0M, Product = new Product { Name = "Kiseli krastavci" } });
            procurements.Add(tmp);

            procurementGrid.DataContext = procurements;
        }

        private void viewButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Procurement)procurementGrid.SelectedValue;
            if (selected != null)
            {
                new ProcurementDetailsWindow(selected).Show();
                procurementGrid.UnselectAll();
            }
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            new CreateProcurementWindow().Show();
        }
    }
}
