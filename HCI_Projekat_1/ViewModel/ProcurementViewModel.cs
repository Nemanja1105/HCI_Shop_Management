using HCI_Projekat_1.Models;
using HCI_Projekat_1.Services;
using System;
using System.CodeDom;
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
    internal class ProcurementViewModel : INotifyPropertyChanged
    {
        private ProcurementService service = new ProcurementService();
        private List<Procurement> procurements;
        private ObservableCollection<Procurement> data;
        public ObservableCollection<Procurement> Data { get { return this.data; } set { data = value; OnPropertyChanged(); } }
        private DateTime startTime = DateTime.Now;
        private DateTime toFilter;
        private DateTime fromFilter;

        public ProcurementViewModel()
        {
            this.toFilter = this.fromFilter = startTime;
        }
        public DateTime ToFilter { get { return toFilter; } set { toFilter = value; FindAllByFilter(); } }
        public DateTime FromFilter { get { return fromFilter; } set { fromFilter = value; FindAllByFilter(); } }

        public async Task FindAll()
        {
            List<Procurement> result = new List<Procurement>();
            try
            {
                result = await service.FindAll();
            }
            finally
            {
                this.procurements = result;
                Data = new ObservableCollection<Procurement>(result);
            }
        }

        public void FindAllByFilter()
        {
            var query = procurements.AsQueryable();
            query = query.Where((el) => el.DateOfAcquisition.Date <= toFilter.Date);
            query = query.Where((el) => el.DateOfAcquisition.Date >= fromFilter.Date);
            Data = new ObservableCollection<Procurement>(query.ToList());
        }

        public async Task Insert(Procurement procurement)
        {
            var result = await service.Insert(procurement);
            procurements.Add(result);
            FindAllByFilter();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
