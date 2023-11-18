using HCI_Projekat_1.Forms.Windows;
using HCI_Projekat_1.Models;
using HCI_Projekat_1.Models.Enums;
using HCI_Projekat_1.Util;
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
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        private ProductViewModel productViewModel=new ProductViewModel();
        public ProductPage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeAsync();
        }

        private async void addButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddProductWindow(productViewModel.Categories);
            dialog.ShowDialog();
            if (dialog.Product == null)
                return;
             try
             {
                await productViewModel.Insert(dialog.Product);
             }
             catch (Exception ex)
             {
                 MessageBox.Show(LanguageUtil.GetTranslation("DbExceptionMain"), LanguageUtil.GetTranslation("DbExceptionMessage"), MessageBoxButton.OK, MessageBoxImage.Error);
             }
        }

        private async void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Product)productGrid.SelectedValue;
            if (selected != null)
            {
                var Result = MessageBox.Show(LanguageUtil.GetTranslation("DeleteProductQuest"), LanguageUtil.GetTranslation("DeleteProduct"), MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (Result == MessageBoxResult.Yes)
                {
                    try
                    {
                        await productViewModel.Delete(selected);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(LanguageUtil.GetTranslation("DbExceptionMain"), LanguageUtil.GetTranslation("DbExceptionMessage"), MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                productGrid.UnselectAll();

            }
        }

        private async void updateButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Product)productGrid.SelectedValue;
            if (selected != null)
            {
                var updateWindows = new AddProductWindow(productViewModel.Categories,selected);
                updateWindows.ShowDialog();
                productGrid.UnselectAll();
                var updated = updateWindows.Product;
                if (updated != null)
                {
                    try
                    {
                        await this.productViewModel.Update(updated);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(LanguageUtil.GetTranslation("DbExceptionMain"), LanguageUtil.GetTranslation("DbExceptionMessage"), MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
           
        }

        private async Task InitializeAsync()
        {
            try
            {
                await productViewModel.FindAllCategories();
                await productViewModel.FindAll();
            }
            catch (Exception e)
            {
                MessageBox.Show(LanguageUtil.GetTranslation("DbExceptionMain"), LanguageUtil.GetTranslation("DbExceptionMessage"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
             this.DataContext = productViewModel;
        }

       
    }
}
