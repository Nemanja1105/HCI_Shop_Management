using HCI_Projekat_1.Forms.Windows;
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

        public async Task<List<Bill>> FindAllByEmployyeId(int id)
        {
            using (var dbContext = new ShopManagementContext())
            {
                return await dbContext.Bill.Include(b => b.Employee).Include(b => b.Canceledbill).Where(bill=>bill.EmployeeId==id).AsNoTracking().ToListAsync();
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

        public async Task<Bill> Insert(Bill bill)
        {
            var emp = bill.Employee; bill.Employee = null;
            var tmp = bill.Billitem.Select(item => new Billitem { Price = item.Price, Quantity = item.Quantity, ProductId = item.ProductId }).ToList();
            var items = bill.Billitem; bill.Billitem = tmp;
            using (var dbContext = new ShopManagementContext())
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        await dbContext.Bill.AddAsync(bill);
                        foreach (var item in items)
                        {
                            var product = await dbContext.Product.FindAsync(item.ProductId);
                            if (product != null)
                            {
                                product.Quantity -= item.Quantity;
                                dbContext.Entry(product).State = EntityState.Modified;
                            }
                        }
                        await dbContext.SaveChangesAsync();
                        bill.Employee = emp;
                        transaction.Commit();
                        return bill;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw new Exception();
                    }
                }
            }
        }

        public async Task CancelBill(Canceledbill canceledbill)
        {
            var bill = canceledbill.Bill; canceledbill.Bill = null; canceledbill.Employee = null;
            using (var dbContext = new ShopManagementContext())
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        await dbContext.Canceledbill.AddAsync(canceledbill);
                        var items= await this.FindAllItemForBill(bill);
                        foreach (var item in items)
                        {
                            var product = await dbContext.Product.FindAsync(item.ProductId);
                            if (product != null)
                            {
                                product.Quantity += item.Quantity;
                                dbContext.Entry(product).State = EntityState.Modified;
                            }
                        }
                        await dbContext.SaveChangesAsync();
                        bill.Canceledbill.Add(canceledbill);
                        canceledbill.Bill = bill;
                        transaction.Commit();  
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw new Exception();
                    }
                }
            }
        }
    }
}
