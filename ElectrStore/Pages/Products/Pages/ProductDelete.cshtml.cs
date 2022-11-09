using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ElectrStore
{
    [Authorize(Roles = "Owner")]
    public class ProductDeleteModel : PageModel
    {
        private readonly StoreContext _context;

        private IProductsRepository _productsRepository;

        public ProductDeleteModel(StoreContext context, IProductsRepository productsRepository)
        {
            _context = context;
            _productsRepository = productsRepository;
        }

        [BindProperty]
        public ProductRecord ProductRecord { get; set; }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
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

        public async Task<IActionResult> OnPostAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductRecord = await _context.ProductRecords.FindAsync(id);

            if (ProductRecord != null)
            {
                _context.ProductRecords.Remove(ProductRecord);
                _productsRepository.DelFolderWithFiles(_productsRepository.GetFolder(ProductRecord));
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/Products/Pages/ProductIndex");
        }
    }
}
