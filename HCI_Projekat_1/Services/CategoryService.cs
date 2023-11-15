using HCI_Projekat_1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Projekat_1.Services
{
    internal class CategoryService
    {
        public async Task<List<Category>> FindAll()
        {
            using (var dbContext = new ShopManagementContext())
            {
                return await dbContext.Category.AsNoTracking().ToListAsync();
            }
        }

        public async Task Delete(Category category)
        {
            using (var dbContext = new ShopManagementContext())
            {
                dbContext.Category.Remove(category);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<Category> Update(Category request)
        {
            using (var dbContext = new ShopManagementContext())
            {
                var category = await dbContext.Category.FirstOrDefaultAsync(category => category.Id == request.Id);
                if (!string.IsNullOrEmpty(request.Name))
                    category.Name = request.Name;
                await dbContext.SaveChangesAsync();
                return category;
            }
        }

        public async Task Insert(Category category)
        {
            using (var dbContext = new ShopManagementContext())
            {
                await dbContext.Category.AddAsync(category);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
