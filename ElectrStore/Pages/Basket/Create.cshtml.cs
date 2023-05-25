using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ElectrStore;
using Microsoft.EntityFrameworkCore;

namespace ElectrStore.Pages.Buscket
{
    public class CreateModel : PageModel
    {
        private readonly StoreContext _context;

        public CreateModel(StoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        //Создание корзины 
        public async Task<IActionResult> OnPostCreateAsync(string productId, string userId)
        {
            //Поиск товара по ID
            var product = await _context.ProductRecords.Where(p => p.Id == productId)
                .FirstOrDefaultAsync();
            var order = await _context.OrderRecords.Where(u => u.UserId == userId && u.Status == 0)
                .FirstOrDefaultAsync();
            var orderItemsRecord = new OrderItemsRecord { Id = Guid.NewGuid().ToString() };
            if (order is null)
            {
                order = new OrderRecord { Id = Guid.NewGuid().ToString() };
                order.Status = 0;
                order.UserId = userId;
                _context.OrderRecords.Add(order);
                await _context.SaveChangesAsync();
                orderItemsRecord.OrderId = order.Id;
            }
            else
            {
                orderItemsRecord.OrderId = order.Id;
            }
            orderItemsRecord.ProductId = product.Id;
            orderItemsRecord.ProductName = product.ProductName;
            _context.OrderItemsRecords.Add(orderItemsRecord);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
