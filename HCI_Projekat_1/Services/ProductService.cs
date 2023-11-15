using HCI_Projekat_1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat_1.Services
{
    internal class ProductService
    {
        public async Task<List<Product>> FindAll()
        {
            using (var dbContext = new ShopManagementContext())
                return await dbContext.Product.Include(p => p.Category).AsNoTracking().ToListAsync();
        }

        public async Task Delete(Product product)
        {
            using (var dbContext = new ShopManagementContext())
            {
                dbContext.Product.Remove(product);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task Update(Product request)
        {
            using (var dbContext = new ShopManagementContext())
            {
                var product = await dbContext.Product.FirstOrDefaultAsync(product => product.Id == request.Id);
                if (!string.IsNullOrEmpty(request.Name))
                    product.Name = request.Name;
                if (request.Quantity > 0)
                    product.Quantity = request.Quantity;
                if(!string.IsNullOrEmpty(request.Barkod))
                    product.Barkod=request.Barkod;
                if(request.CategoryId > 0)
                    product.CategoryId = request.CategoryId;
                if(!string.IsNullOrEmpty(request.UnitOfMeasure))
                    product.UnitOfMeasure=request.UnitOfMeasure;
                if(request.PurchasePrice > 0)
                    product.PurchasePrice = request.PurchasePrice;
                if(request.SellingPrice > 0)
                    product.SellingPrice = request.SellingPrice;
                await dbContext.SaveChangesAsync();
            }
        }

            public async Task Insert(Product product)
            {

                var tmp = product.Category;
                product.Category = null;
                product.CategoryId = tmp.Id;
                using (var dbContext = new ShopManagementContext())
                {
                    await dbContext.Product.AddAsync(product);
                    await dbContext.SaveChangesAsync();
                    product.Category = tmp;

                }
            }
        }
    }
