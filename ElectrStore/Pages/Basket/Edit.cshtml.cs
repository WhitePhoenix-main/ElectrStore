using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElectrStore;

namespace ElectrStore.Pages.Buscket
{
    public class EditModel : PageModel
    {
        private readonly ElectrStore.StoreContext _context;

        public EditModel(ElectrStore.StoreContext context)
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

            var orderitemsrecord =  await _context.OrderItemsRecords.FirstOrDefaultAsync(m => m.Id == id);
            if (orderitemsrecord == null)
            {
                return NotFound();
            }
            OrderItemsRecord = orderitemsrecord;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(OrderItemsRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderItemsRecordExists(OrderItemsRecord.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OrderItemsRecordExists(string id)
        {
          return (_context.OrderItemsRecords?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
