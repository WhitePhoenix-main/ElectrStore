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
    public class DeleteModel : PageModel
    {
        private readonly ElectrStore.StoreContext _context;

        public DeleteModel(ElectrStore.StoreContext context)
        {
            _context = context;
        }

        [BindProperty]
      public OrderItemsRecord OrderItemsRecord { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.OrderItemsRecords == null)
            {
                return NotFound();
            }

            var orderitemsrecord = await _context.OrderItemsRecords.FirstOrDefaultAsync(m => m.Id == id);

            if (orderitemsrecord == null)
            {
                return NotFound();
            }
            else 
            {
                OrderItemsRecord = orderitemsrecord;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _context.OrderItemsRecords == null)
            {
                return NotFound();
            }
            var orderitemsrecord = await _context.OrderItemsRecords.FindAsync(id);

            if (orderitemsrecord != null)
            {
                OrderItemsRecord = orderitemsrecord;
                _context.OrderItemsRecords.Remove(OrderItemsRecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
