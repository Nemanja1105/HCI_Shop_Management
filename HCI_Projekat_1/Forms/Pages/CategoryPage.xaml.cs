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
    /// Interaction logic for CategoryPage.xaml
    /// </summary>
    public partial class CategoryPage : Page
    {
        private ObservableCollection<Category> categories=new ObservableCollection<Category>();
        public CategoryPage()
        {
            InitializeComponent();
            categories.Add(new Category("Voce"));
            categories.Add(new Category("Povrce"));
            categories.Add(new Category("Suhomesnato"));
            categories.Add(new Category("Sir"));
            categories.Add(new Category("Meso"));
            categories.Add(new Category("Gotova jela"));
            categoryGrid.DataContext = categories;

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Category)categoryGrid.SelectedValue;
            if (selected != null)
            {
                var Result = MessageBox.Show("Are you sure you want to delete the category?", "Delete category?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (Result == MessageBoxResult.Yes)
                    categories.Remove(selected);
                categoryGrid.UnselectAll();
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            new AddCategoryWindow().ShowDialog();
        }
    }
}
