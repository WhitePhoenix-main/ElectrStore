using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElectrStore.Pages.Products.Pages
{
    public class CreateModel : PageModel
    {
        private readonly StoreContext _context;

        public CreateModel(StoreContext context)
        {
            _context = context;
        }
        [BindProperty]
        public ProductRecord ProductRecord { get; set; } = default!;
        
        public IActionResult OnGet()
        {
            ProductRecord = new ProductRecord { Id = Guid.NewGuid().ToString() };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.ProductRecords == null || ProductRecord == null)
          {
              return Page();
          }

          _context.ProductRecords.Add(ProductRecord);
          await _context.SaveChangesAsync();

          return RedirectToPage("./ProductIndex");
        }
    }
}
