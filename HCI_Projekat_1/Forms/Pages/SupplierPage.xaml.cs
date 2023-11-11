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
    /// Interaction logic for SupplierPage.xaml
    /// </summary>
    public partial class SupplierPage : Page
    {
        private ObservableCollection<Supplier> suppliers = new ObservableCollection<Supplier>();
        public SupplierPage()
        {
            InitializeComponent();
            suppliers.Add(new Supplier { Id = 1, Name = "Takovo", Address = "Trn 12", PhoneNumber = "056434567" });
            suppliers.Add(new Supplier { Id = 2, Name = "Podravka", Address = "Zagreg 2", PhoneNumber = "056589667" });
            suppliers.Add(new Supplier { Id = 3, Name = "Vitaminka", Address = "Banja Luka 12", PhoneNumber = "0214567" });
            suppliers.Add(new Supplier { Id = 4, Name = "Semberija", Address = "Biljena 45", PhoneNumber = "0559687" });
            supplierGrid.DataContext = suppliers;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            new AddSuplierWindow().ShowDialog();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Supplier)supplierGrid.SelectedValue;
            if(selected != null)
            {
                var Result = MessageBox.Show("Are you sure you want to delete the supplier?", "Delete supplier?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (Result == MessageBoxResult.Yes)
                    suppliers.Remove(selected);
                supplierGrid.UnselectAll();
            }
        }
    }
}
