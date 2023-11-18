using HCI_Projekat_1.Models.Enums;
using HCI_Projekat_1.Models;
using HCI_Projekat_1.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HCI_Projekat_1.Util;

namespace HCI_Projekat_1.ViewModel
{
    internal class ProductViewModel : INotifyPropertyChanged
    {
        private ProductService service = new ProductService();
        private CategoryService categoryService = new CategoryService();
        private List<Product> products;
        private ObservableCollection<Product> data;
        public ObservableCollection<Product> Data { get { return this.data; } set { data = value; OnPropertyChanged(); } }
        public List<Category> Categories { get; set; } = new List<Category>();

        private Category categoryFilter = new Category(LanguageUtil.GetTranslation("All"));
        private String searchFilter = "";
        public Category CategoryFilter { get { return categoryFilter; } set { this.categoryFilter = value; FindAllByFilter(); } }
        public String SearchFilter { get { return searchFilter; } set { this.searchFilter = value; FindAllByFilter(); } }

        public async Task FindAll()
        {
            List<Product> result = new List<Product>();
            try
            {
                result = await service.FindAll();
            }
            finally
            {
                this.products = result;
                Data = new ObservableCollection<Product>(result);
            }
        }

        public async Task FindAllCategories()
        {
            List<Category> result = new List<Category>();
            try
            {
                result = await categoryService.FindAll();
            }
            finally
            {
                result.Add(categoryFilter);
                this.Categories = result;
            }
        }



        public void FindAllByFilter()
        {
            if (CategoryFilter.Name == "All" && String.IsNullOrEmpty(SearchFilter))
            {
                Data = new ObservableCollection<Product>(products);
                return;
            }
            var query = products.AsQueryable();
            if (!String.IsNullOrEmpty(SearchFilter))
                query = query.Where((el) => el.Name.ToUpper().StartsWith(SearchFilter.ToUpper()));
            if (CategoryFilter.Name != "All")
            {
                query = query.Where((el) => el.Category.Name == CategoryFilter.Name);
            }
            Data = new ObservableCollection<Product>(query.ToList());
        }

        public async Task Delete(Product product)
        {
            await this.service.Delete(product);
            products.Remove(product);
            if (Data.Contains(product))
                Data.Remove(product);
        }

        public async Task Update(Product product)
        {
            await this.service.Update(product);
            int index = this.products.IndexOf(product);
            if (index != -1)
                this.products[index] = product;
            this.FindAllByFilter();

        }

        public async Task Insert(Product product)
        {
            await this.service.Insert(product);
            this.products.Add(product);
            this.FindAllByFilter();//trigerujemo azuriranje view iz view modela
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}