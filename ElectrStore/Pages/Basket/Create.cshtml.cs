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
        private readonly ElectrStore.StoreContext _context;

        public CreateModel(ElectrStore.StoreContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public OrderItemsRecord OrderItemsRecord { get; set; } = default!;

        [BindProperty] 
        public OrderRecord OrderRecord { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostCreateAsync(string productId, string userId)
        {
            var orders = await _context.OrderRecords.Where(u => u.UserId == userId && OrderRecord.Status == 0)
                .FirstOrDefaultAsync();
            if (orders is null)
            {
                OrderRecord = new OrderRecord { Id = Guid.NewGuid().ToString() };
                OrderRecord.Status = 0;
                OrderRecord.UserId = userId;
                _context.OrderRecords.Add(OrderRecord);
                await _context.SaveChangesAsync();
                OrderItemsRecord.OrderId = OrderRecord.Id;
            }
            else
            {
                OrderItemsRecord = new OrderItemsRecord { Id = Guid.NewGuid().ToString() };
                OrderItemsRecord.OrderId = orders.Id;
            }
            OrderItemsRecord.ProductId = productId;
            _context.OrderItemsRecords.Add(OrderItemsRecord);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
