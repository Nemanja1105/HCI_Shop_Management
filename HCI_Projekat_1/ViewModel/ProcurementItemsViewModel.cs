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
using System.Windows.Controls.Primitives;

namespace HCI_Projekat_1.ViewModel
{
    class ProcurementItemsViewModel:INotifyPropertyChanged
    {
        private ProcurementService procurementService = new ProcurementService();
        private ProductService productService = new ProductService();
        private SupplierService supplierService = new SupplierService();

        public ObservableCollection<Procurementitem> Procurementitems { get; }=new ObservableCollection<Procurementitem>();
        private decimal totalPrice=0.0M;

        public List<Product> Products { get; set; }
        public List<Supplier> Suppliers { get; set; }
        public decimal TotalPrice { get { return this.totalPrice; } set { this.totalPrice = value; OnPropertyChanged(); } }
        public Supplier Supplier { get; set; }


        public async Task LoadAllData()
        {
            var res1 = new List<Product>();
            var res2 = new List<Supplier>();
            try
            {
                res1 = await productService.FindAllTracked();
                res2 = await supplierService.FindAll();
            }
            finally
            {
                Products = res1;
                Suppliers = res2;
            }
        }

        public void Insert(Product product, decimal price, decimal quantity)
        {
            var tmp = Procurementitems.FirstOrDefault(item => item.ProductId == product.Id);
            if (tmp == null)
            {
                var item = new Procurementitem {Price = price, Quantity=quantity,ProductId=product.Id,Product=product};
                Procurementitems.Add(item);
                TotalPrice += item.TotalPrice;
            }
            else
            {
                tmp.Quantity += quantity;
                TotalPrice += quantity * tmp.Price;
            }
        }

        public void Remove(Procurementitem item)
        {
            this.Procurementitems.Remove(item);
            TotalPrice-=item.TotalPrice;
        }

        public Procurement CreateProcurement()
        {
            
            return new Procurement { TotalPrice = this.TotalPrice, EmployeeId = ManagerMain.Employee.Id, DateOfAcquisition = DateTime.Now, Procurementitem = Procurementitems,SupplierId=Supplier.Id,Supplier=Supplier,Employee=ManagerMain.Employee };
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
