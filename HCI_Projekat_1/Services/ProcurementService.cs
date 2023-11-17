using HCI_Projekat_1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HCI_Projekat_1.Services
{
    internal class ProcurementService
    {
        public async Task<List<Procurement>> FindAll()
        {
            using (var dbContext = new ShopManagementContext())
            {
                return await dbContext.Procurement.Include(p=>p.Supplier).Include(p=>p.Employee).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Procurementitem>> FindAllItemForProcurement(Procurement procurement)
        {
            using (var dbContext = new ShopManagementContext())
            {
                return await dbContext.Procurementitem.Where(b => b.ProcurementId == procurement.Id).Include(b => b.Product).AsNoTracking().ToListAsync();
            }
        }

        public async Task<Procurement> Insert(Procurement procurement)
        {
            var emp = procurement.Employee; procurement.Employee = null;
            var supp=procurement.Supplier; procurement.Supplier = null;
            var tmp = procurement.Procurementitem.Select(item => new Procurementitem { Price = item.Price, Quantity = item.Quantity, ProductId = item.ProductId }).ToList();
            var items = procurement.Procurementitem; procurement.Procurementitem = tmp;
            using (var dbContext = new ShopManagementContext())
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        await dbContext.Procurement.AddAsync(procurement);
                        foreach (var item in items)
                        {
                           var product = await dbContext.Product.FindAsync(item.ProductId);
                            if (product != null)
                            {
                                product.Quantity += item.Quantity;
                                product.PurchasePrice = item.Price;
                                dbContext.Entry(product).State = EntityState.Modified;
                            }
                        }
                        await dbContext.SaveChangesAsync();
                        procurement.Employee = emp;
                        procurement.Supplier = supp;
                        transaction.Commit();
                        return procurement;
                    }
                    catch(Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw new Exception();
                    }
                }
            }
        }


    }
}
