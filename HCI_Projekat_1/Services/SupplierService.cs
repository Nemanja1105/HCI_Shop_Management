using HCI_Projekat_1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat_1.Services
{
    internal class SupplierService
    {
        public async Task<List<Supplier>> FindAll()
        {
            using (var dbContext = new ShopManagementContext())
            {
                return await dbContext.Supplier.AsNoTracking().ToListAsync();
            }
        }

        public async Task Delete(Supplier supplier)
        {
            using (var dbContext = new ShopManagementContext())
            {
                dbContext.Supplier.Remove(supplier);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<Supplier> Update(Supplier request)
        {
            using (var dbContext = new ShopManagementContext())
            {
                var supplier = await dbContext.Supplier.FirstOrDefaultAsync(category => category.Id == request.Id);
                if (!string.IsNullOrEmpty(request.Name))
                    supplier.Name = request.Name;
                if(!string.IsNullOrEmpty(request.PhoneNumber))
                    supplier.PhoneNumber = request.PhoneNumber;
                if(!string.IsNullOrEmpty(request.Address))
                    supplier.Address = request.Address;
                await dbContext.SaveChangesAsync();
                return supplier;
            }
        }

        public async Task Insert(Supplier supplier)
        {
            using (var dbContext = new ShopManagementContext())
            {
                await dbContext.Supplier.AddAsync(supplier);
                await dbContext.SaveChangesAsync();
            }
        }

    }
}
