using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ElectrStore
{
    [Authorize]
    public class ProductDetailsModel : PageModel
    {
        private readonly StoreContext _context;

        public ProductDetailsModel(StoreContext context)
        {
            _context = context;
        }

        public ProductRecord ProductRecord { get; set; }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            // TODO: Проверка на пустую строку
            if (id == null)
            {
                return NotFound();
            }

            ProductRecord = await _context.ProductRecords.FirstOrDefaultAsync(m => m.Id == id);

            if (ProductRecord == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
