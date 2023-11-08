using HCI_Projekat_1.Forms.Windows;
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
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();
            var data = new ObservableCollection<object>();
            data.Add(new {Id=1,Name="Marko",Surname="Markovic",JMB="1234567890123",Address="Beogradska 3",PhoneNumber="066391652",Role="Manager"});
            data.Add(new { Id = 2, Name = "Pera", Surname = "Peric", JMB = "1234567890123", Address = "Beogradska 4", PhoneNumber = "066391652", Role = "Manager" });
            data.Add(new { Id = 3, Name = "Marko", Surname = "Markovic", JMB = "1234567890123", Address = "Beogradska 3", PhoneNumber = "066391652", Role = "Manager" });
            data.Add(new { Id = 4, Name = "Pera", Surname = "Peric", JMB = "1234567890123", Address = "Beogradska 4", PhoneNumber = "066391652", Role = "Manager" });
            userGrid.DataContext = data;
        }

      

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddUserWindow().ShowDialog();
        }
    }
}
