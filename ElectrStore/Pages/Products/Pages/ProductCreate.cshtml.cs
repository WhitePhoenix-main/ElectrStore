using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
// ReSharper disable once CheckNamespace
namespace ElectrStore

{  
    [Authorize(Roles = "Owner")]
    public class ProductCreateModel : PageModel, IHasProduct
    {
        private StoreContext _context { get; }
        private INormalizer _normalizer { get; init; }

        public ProductCreateModel(StoreContext context, INormalizer normalizer)
        {
            _context = context;
            _normalizer = normalizer;
        }
        

        public IActionResult OnGet()
        {
            ProductRecord = new ProductRecord { Id = Guid.NewGuid().ToString() };
            return Page();
        }

        [BindProperty]
        public ProductRecord ProductRecord { get; set; }

        public bool IsNewRec { get; set; } = true;
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!String.IsNullOrWhiteSpace(ProductRecord.ProductTypeNew))
            {
                ProductRecord.CategoryId = ProductRecord.ProductTypeNew;
            }

            ProductRecord.Price = _normalizer.GetPriceInPennies(ProductRecord.PriceInput);
            ProductRecord.CreationTime = DateTime.UtcNow;
            ProductRecord.EditorName = User.Identity.Name;
            _context.ProductRecords.Add(ProductRecord);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Products/Pages/ProductIndex");
        }
    }
}