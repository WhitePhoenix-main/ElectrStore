using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ElectrStore;

namespace ElectrStore.Pages.Buscket
{
    public class IndexModel : PageModel
    {
        private readonly StoreContext _context;
        private IProductsRepository _productsRepository { get; set; }
        public IndexModel(StoreContext context, IProductsRepository productsRepository)
        {
            _context = context;
            _productsRepository = productsRepository;
        }

        public bool AvaChecker { get; set; } = true;
        public IList<OrderItemsRecord> OrderItemsRecord { get;set; } = default!;
        
        [BindProperty] 
        public OrderRecord OrderRecord { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.OrderItemsRecords != null)
            {
                OrderItemsRecord = await _context.OrderItemsRecords.ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostSubmitOrderAsync(string userId)
        {
            var order = await _context.OrderRecords.Where(u => u.UserId == userId && u.Status == 0)
                .FirstOrDefaultAsync();
            var orderItems = _context.OrderItemsRecords
                .Where(orItems => order.Id == orItems.OrderId && order.Status == 0)
                .ToList();
            foreach (var orderItem in orderItems)
            {
                var item = _context.ProductRecords
                    .Where(p => orderItem.ProductId == p.Id)
                    .ToList();
                foreach (var product in item)
                {
                    product.Quantity -= orderItem.BuyQuantity;
                    _context.ProductRecords.Update(product);
                }
            }
            order.Status = 1;
            
            _context.OrderRecords.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Index");
        } 
    }
}
