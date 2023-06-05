using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ElectrStore
{
    public class ProductIndexModel : PageModel
    {
        public StoreContext Context { get; }

        public ProductIndexModel(StoreContext context)
        {
            Context = context;
        }
        
        public IList<ProductRecord> Product { get;set; }

        public async Task OnGetAsync(string? search, string? productType)
        {
            ViewData["search"] = search;
            ViewData["productType"] = productType;
            //TODO: Пересмотреть логику
            var query = Context.ProductRecords.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(search) && string.IsNullOrWhiteSpace(productType))
                query = query
                    .Where(product => product.CategoryId == search 
                    || product.ProductName.Contains(search));
            else if (!string.IsNullOrWhiteSpace(productType) && !string.IsNullOrWhiteSpace(search))
                query = query
                    .Where(product => product.CategoryId == productType
                                      || product.ProductName.Contains(search));
            else if (!string.IsNullOrWhiteSpace(productType) )
                query = query
                    .Where(product => product.CategoryId == productType);
            
            Product = await query
                .OrderBy(product => product.CategoryId)
                .ThenBy(product => product.ProductName)
                .ToListAsync();
        }
        
    }
}
