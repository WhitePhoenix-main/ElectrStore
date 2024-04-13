using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

// ReSharper disable once CheckNamespace
namespace ElectrStore
{
    [Authorize(Roles = "Owner")]
    public class ProductDeleteModel : PageModel
    {
        private readonly StoreContext _dbContext;

        private IProductsRepository _productsRepository;

        public ProductDeleteModel(StoreContext context, IProductsRepository productsRepository)
        {
            _dbContext = context;
            _productsRepository = productsRepository;
        }

        [BindProperty]
        public ProductRecord? ProductRecord { get; set; }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();
            ProductRecord = await _dbContext.ProductRecords
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ProductRecord is null)
                return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();
            ProductRecord = await _dbContext.ProductRecords
                .FindAsync(id);
            if (ProductRecord == null) 
                return NotFound();
            _dbContext.ProductRecords
                .Remove(ProductRecord);
            _productsRepository.DelFolderWithFiles(_productsRepository.GetFolder(ProductRecord));
            await _dbContext.SaveChangesAsync();
            return RedirectToPage("/Products/Pages/ProductIndex");
        }
    }
}
