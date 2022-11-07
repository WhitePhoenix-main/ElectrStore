using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ElectrStore.Pages.Products.Pages
{
    public class EditModel : PageModel
    {
        private readonly StoreContext _context;

        public EditModel(StoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProductRecord ProductRecord { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.ProductRecords == null)
            {
                return NotFound();
            }

            var productrecord =  await _context.ProductRecords.FirstOrDefaultAsync(m => m.Id == id);
            if (productrecord == null)
            {
                return NotFound();
            }
            ProductRecord = productrecord;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ProductRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductRecordExists(ProductRecord.Id))
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

        private bool ProductRecordExists(string id)
        {
          return (_context.ProductRecords?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
