using HCI_Projekat_1.Models;
using HCI_Projekat_1.Models.Enums;
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

namespace HCI_Projekat_1
{
    /// <summary>
    /// Interaction logic for ManagerMain.xaml
    /// </summary>
    public partial class ManagerMain : Window
    {
        internal static Employee Employee { get; set; }
        public ManagerMain(Employee employee)
        {
            InitializeComponent();
            if(employee.Theme!="OrangeTheme")
                ThemeUtil.ChangeTheme(employee.Theme);
            if (employee.Language != "SerbianLanguage")
                LanguageUtil.ChangeLanguage(employee.Language);
            LanguageUtil.CurrentLanguage = employee.Language;
            Employee = employee;
            if (!Employee.Uloga)//radnik
            {
                userItem.Visibility = Visibility.Collapsed;
                productItem.Visibility = Visibility.Collapsed;
                procurementItem.Visibility = Visibility.Collapsed;
                supplierItem.Visibility = Visibility.Collapsed;
                categoryItem.Visibility = Visibility.Collapsed;
                billItem.Margin = new Thickness(0, 10, 0, 10);
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is ListViewItem selectedItem)
            {
                string pageTag = selectedItem.Tag as string;
                if (pageTag != null)
                {
                    Uri pageUri = new Uri($"/Forms/Pages/{pageTag}.xaml", UriKind.Relative);
                    mainFrame.Navigate(pageUri);
                }
            }
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
