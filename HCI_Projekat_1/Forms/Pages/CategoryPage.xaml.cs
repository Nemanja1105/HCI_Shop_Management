﻿using HCI_Projekat_1.Forms.Windows;
using HCI_Projekat_1.Models;
using HCI_Projekat_1.Util;
using HCI_Projekat_1.ViewModel;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
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
        private CategoryViewModel categoryViewModel = new CategoryViewModel();
        public CategoryPage()
        {
            InitializeComponent();

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
          await  InitializeAsync();

        }
        private async Task InitializeAsync()
        {
            try
            {
                await categoryViewModel.FindAll();
            }
            catch (Exception e)
            {
                MessageBox.Show(LanguageUtil.GetTranslation("DbExceptionMain"), LanguageUtil.GetTranslation("DbExceptionMessage"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.DataContext = categoryViewModel;
        }

        private async void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Category)categoryGrid.SelectedValue;
            if (selected != null)
            {
                var Result = MessageBox.Show(LanguageUtil.GetTranslation("DeleteCategoryQuest"), LanguageUtil.GetTranslation("DeleteCategory"), MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (Result == MessageBoxResult.Yes)
                {
                    try
                    {
                        await categoryViewModel.Delete(selected);

                    }
                    catch (DbUpdateException ex)
                    {
                        if (ex.InnerException is MySqlException mySqlEx && mySqlEx.Number == 1451)
                        {
                            MessageBox.Show(LanguageUtil.GetTranslation("DeleteCategoryExistProduct"), LanguageUtil.GetTranslation("DeleteCategoryExistProductMessage"), MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                            MessageBox.Show(LanguageUtil.GetTranslation("DbExceptionMain"), LanguageUtil.GetTranslation("DbExceptionMessage"), MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                categoryGrid.UnselectAll();
            }
        }

        private async void addButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddCategoryWindow();
            dialog.ShowDialog();
            if (dialog.Category == null)
                return;
            try
            {
                await categoryViewModel.Insert(dialog.Category);
            }
            catch (Exception ex)
            {
                MessageBox.Show(LanguageUtil.GetTranslation("DbExceptionMain"), LanguageUtil.GetTranslation("DbExceptionMessage"), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void updateButton_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Category)categoryGrid.SelectedValue;
            if (selected != null)
            {
                var updateWindows = new AddCategoryWindow(selected);
                updateWindows.ShowDialog();
                categoryGrid.UnselectAll();
                var updated = updateWindows.Category;
                if (updated != null)
                {
                    try
                    {
                        await this.categoryViewModel.Update(updated);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(LanguageUtil.GetTranslation("DbExceptionMain"), LanguageUtil.GetTranslation("DbExceptionMessage"), MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
