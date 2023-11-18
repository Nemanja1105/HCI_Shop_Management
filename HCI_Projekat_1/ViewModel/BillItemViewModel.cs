using HCI_Projekat_1.Exceptions;
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
    class BillItemViewModel : INotifyPropertyChanged
    {
        private BillService billService = new BillService();
        private ProductService productService = new ProductService();

        public ObservableCollection<Billitem> BillItems { get; } = new ObservableCollection<Billitem>();
        private decimal totalPrice = 0.0M;

        public List<PaymentType> PaymentTypes = Enum.GetValues(typeof(PaymentType)).Cast<PaymentType>().ToList();
        public PaymentType PaymentType { get; set; } = PaymentType.Card;

        public ObservableCollection<Product> Products { get; set; }
        public decimal TotalPrice { get { return this.totalPrice; } set { this.totalPrice = value; OnPropertyChanged(); } }


        public async Task LoadAllData()
        {
            var res1 = new List<Product>();
            try
            {
                res1 = await productService.FindAllTracked();
                PaymentTypes.Remove(PaymentType.All);
            }
            finally
            {
                Products = new ObservableCollection<Product>(res1);
            }
        }

        public void Insert(Product product, decimal quantity)
        {
            var tmp = BillItems.FirstOrDefault(item => item.ProductId == product.Id);
            decimal numOfDec = quantity % 1;
            if (numOfDec != 0 && product.UnitOfMeasure == UnitOfMeasure.Kom.ToString())
                throw new QuantityUnitException();
            if (tmp == null)
            {
                if (product.Quantity < quantity)
                    throw new QuantityExceededException();
                var item = new Billitem { Price = product.SellingPrice, Quantity = quantity, ProductId = product.Id, Product = product };
                BillItems.Add(item);
                TotalPrice += item.TotalPrice;
            }
            else
            {
                if (product.Quantity < quantity + tmp.Quantity)
                    throw new QuantityExceededException();
                tmp.Quantity += quantity;
                TotalPrice += quantity * tmp.Price;
            }
        }

        public void Remove(Billitem item)
        {
            this.BillItems.Remove(item);
            TotalPrice -= item.TotalPrice;
        }

        public Bill CreateBill()
        {
            return new Bill { TotalPrice = this.TotalPrice, EmployeeId = ManagerMain.Employee.Id, DateOfIssue = DateTime.Now, Billitem = BillItems, Employee = ManagerMain.Employee, PaymentType = (PaymentType == PaymentType.Card) };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
