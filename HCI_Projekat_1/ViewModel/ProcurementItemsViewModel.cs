using HCI_Projekat_1.Models;
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
    class ProcurementItemsViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<Procurementitem> Procurementitems { get; }=new ObservableCollection<Procurementitem>();
        private decimal totalPrice=0.0M;

        public decimal TotalPrice { get { return this.totalPrice; } set { this.totalPrice = value; OnPropertyChanged(); } }
        public Supplier Supplier { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
