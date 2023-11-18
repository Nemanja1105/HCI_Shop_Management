using HCI_Projekat_1.Models;
using HCI_Projekat_1.Models.Enums;
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
    internal class BillViewModel : INotifyPropertyChanged
    {
        private BillService service = new BillService();
        private List<Bill> bills;
        private ObservableCollection<Bill> data;
        public List<PaymentType> PaymentTypes = Enum.GetValues(typeof(PaymentType)).Cast<PaymentType>().ToList();
        public ObservableCollection<Bill> Data { get { return this.data; } set { data = value; OnPropertyChanged(); } }

        private PaymentType paymentTypeFilter = PaymentType.All;
        private DateTime startTime = DateTime.Now;
        private DateTime toFilter;
        private DateTime fromFilter;

        public BillViewModel()
        {
            this.toFilter = this.fromFilter = startTime;
        }
        public DateTime ToFilter { get { return toFilter; } set { toFilter = value; FindAllByFilter(); } }
        public DateTime FromFilter { get { return fromFilter; } set { fromFilter = value; FindAllByFilter(); } }
        public PaymentType PaymentType { get { return paymentTypeFilter; } set { paymentTypeFilter = value; FindAllByFilter(); } }


        public async Task FindAll()
        {
            List<Bill> result = new List<Bill>();
            try
            {
                if (ManagerMain.Employee.Uloga)
                    result = await service.FindAll();
                else
                    result = await service.FindAllByEmployyeId(ManagerMain.Employee.Id);
            }
            finally
            {
                this.bills = result;
                Data = new ObservableCollection<Bill>(result);
            }
        }

        public async Task CancelBill(Canceledbill canceledbill)
        {
            await this.service.CancelBill(canceledbill);
            int index = this.bills.IndexOf(canceledbill.Bill);
            if (index != -1)
                this.bills[index] = canceledbill.Bill;
            this.FindAllByFilter();
        }

        public void FindAllByFilter()
        {
            var query = bills.AsQueryable();
            if (PaymentType != PaymentType.All)
            {
                bool status = PaymentType == PaymentType.Cash ? false : true;
                query = query.Where((el) => el.PaymentType == status);
            }
            query = query.Where((el) => el.DateOfIssue.Date <= toFilter.Date);
            query = query.Where((el) => el.DateOfIssue.Date >= fromFilter.Date);
            Data = new ObservableCollection<Bill>(query.ToList());
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
