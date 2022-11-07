using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ElectrStore.Pages.Products.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly StoreContext _context;

        public DeleteModel(StoreContext context)
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

            var productRecord = await _context.ProductRecords.FirstOrDefaultAsync(m => m.Id == id);

            if (productRecord == null)
            {
                return NotFound();
            }
            else 
            {
                ProductRecord = productRecord;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _context.ProductRecords == null)
            {
                return NotFound();
            }
            var productRecord = await _context.ProductRecords.FindAsync(id);

            if (productRecord != null)
            {
                ProductRecord = productRecord;
                _context.ProductRecords.Remove(ProductRecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
