using HCI_Projekat_1.Models;
using HCI_Projekat_1.Services;
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
    /// Interaction logic for ProcurementDetailsWindow.xaml
    /// </summary>
    public partial class ProcurementDetailsWindow : Window
    {
        private Procurement procurement;
        private ProcurementService service=new ProcurementService();
        public ProcurementDetailsWindow(Procurement procurement)
        {
            InitializeComponent();
            this.procurement= procurement;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            try
            {
                procurement.Procurementitem = await service.FindAllItemForProcurement(this.procurement);            }
            catch (Exception e)
            {
                MessageBox.Show(LanguageUtil.GetTranslation("DbExceptionMain"), LanguageUtil.GetTranslation("DbExceptionMessage"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.DataContext = procurement;
            this.procurementGrid.DataContext = procurement.Procurementitem;
            
        }
    }
}
