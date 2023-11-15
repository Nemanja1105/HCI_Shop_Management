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

namespace HCI_Projekat_1.ViewModel
{
    internal class CategoryViewModel:INotifyPropertyChanged
    {
        private CategoryService service = new CategoryService();
        private List<Category> categories;
        private ObservableCollection<Category> data;
        public ObservableCollection<Category> Data { get { return this.data; } set { data = value; OnPropertyChanged(); } }
        private String searchFilter = "";
        public String SearchFilter { get { return searchFilter; } set { this.searchFilter = value; FindAllByFilter(); } }

        public async Task FindAll()
        {
            List<Category> result = new List<Category>();
            try
            {
                result = await service.FindAll();
            }
            finally
            {
                this.categories = result;
                Data = new ObservableCollection<Category>(result);
            }
        }

        public void FindAllByFilter()
        {
            if (String.IsNullOrEmpty(SearchFilter))
            {
                Data = new ObservableCollection<Category>(categories);
                return;
            }
            var query = categories.AsQueryable();
            if (!String.IsNullOrEmpty(SearchFilter))
                query = query.Where((el) => el.Name.ToUpper().StartsWith(SearchFilter.ToUpper()));
            Data = new ObservableCollection<Category>(query.ToList());
        }

        public async Task Delete(Category category)
        {
            await this.service.Delete(category);
            categories.Remove(category);
            if (Data.Contains(category))
                Data.Remove(category);
        }

        public async Task Update(Category category)
        {
          var result = await this.service.Update(category);
            int index = this.categories.IndexOf(category);
            if (index != -1)
                this.categories[index] = result;
            this.FindAllByFilter();

        }

        public async Task Insert(Category category)
        {
            await this.service.Insert(category);
            this.categories.Add(category);
            this.FindAllByFilter();//trigerujemo azuriranje view iz view modela
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
