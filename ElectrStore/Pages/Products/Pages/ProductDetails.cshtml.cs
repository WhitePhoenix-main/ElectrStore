using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ElectrStore.Pages.Products.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly StoreContext _context;

        public DetailsModel(StoreContext context)
        {
            _context = context;
        }

      public ProductRecord ProductRecord { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.ProductRecords == null)
            {
                return NotFound();
            }

            var productrecord = await _context.ProductRecords.FirstOrDefaultAsync(m => m.Id == id);
            if (productrecord == null)
            {
                return NotFound();
            }
            else 
            {
                ProductRecord = productrecord;
            }
            return Page();
        }
    }
}
