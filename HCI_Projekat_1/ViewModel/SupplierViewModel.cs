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
    internal class SupplierViewModel:INotifyPropertyChanged
    {
        private SupplierService service = new SupplierService();
        private List<Supplier> suppliers;
        private ObservableCollection<Supplier> data;
        public ObservableCollection<Supplier> Data { get { return this.data; } set { data = value; OnPropertyChanged(); } }
        private String searchFilter = "";
        public String SearchFilter { get { return searchFilter; } set { this.searchFilter = value; FindAllByFilter(); } }

        public async Task FindAll()
        {
            List<Supplier> result = new List<Supplier>();
            try
            {
                result = await service.FindAll();
            }
            finally
            {
                this.suppliers = result;
                Data = new ObservableCollection<Supplier>(result);
            }
        }

        public void FindAllByFilter()
        {
            if (String.IsNullOrEmpty(SearchFilter))
            {
                Data = new ObservableCollection<Supplier>(suppliers);
                return;
            }
            var query = suppliers.AsQueryable();
            if (!String.IsNullOrEmpty(SearchFilter))
                query = query.Where((el) => el.Name.ToUpper().StartsWith(SearchFilter.ToUpper()));
            Data = new ObservableCollection<Supplier>(query.ToList());
        }

        public async Task Delete(Supplier supplier)
        {
            await this.service.Delete(supplier);
            suppliers.Remove(supplier);
            if (Data.Contains(supplier))
                Data.Remove(supplier);
        }

        public async Task Update(Supplier supplier)
        {
            var result = await this.service.Update(supplier);
            int index = this.suppliers.IndexOf(supplier);
            if (index != -1)
                this.suppliers[index] = result;
            this.FindAllByFilter();

        }

        public async Task Insert(Supplier supplier)
        {
            await this.service.Insert(supplier);
            this.suppliers.Add(supplier);
            this.FindAllByFilter();//trigerujemo azuriranje view iz view modela
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
