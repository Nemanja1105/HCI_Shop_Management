using HCI_Projekat_1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat_1.Services
{
    internal class BillService
    {
        public async Task<List<Bill>> FindAll()
        {
            using (var dbContext = new ShopManagementContext())
            {
                return await dbContext.Bill.Include(b=>b.Employee).Include(b=>b.Canceledbill).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Billitem>> FindAllItemForBill(Bill bill)
        {
            using (var dbContext=new ShopManagementContext()){
                return await dbContext.Billitem.Where(b => b.BillId == bill.Id).Include(b=>b.Product).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Canceledbill>> FindCanceledForBill(Bill bill)
        {
            using (var dbContext = new ShopManagementContext())
            {
                return await dbContext.Canceledbill.Where(b => b.BillId == bill.Id).Include(b=>b.Employee).AsNoTracking().ToListAsync();
            }
        }
    }
}
