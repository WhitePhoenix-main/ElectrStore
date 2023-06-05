using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ElectrStore
{
    public class ProductIndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)] public string? SearchText { get; set; }
        [BindProperty(SupportsGet = true)] public string? RecordType { get; set; }
        [BindProperty(SupportsGet = true)] public int PageSize { get; set; } = 8;
        [BindProperty(SupportsGet = true)] public int PageNumber { get; set; } = 1;
        [BindProperty(SupportsGet = true)] public string SortType { get; set; } = "Price";
        public StoreContext Context { get; }
        public int Counter { get; set;}
        
        public int QueryCounter { get; set;}
        public ProductIndexModel(StoreContext context)
        {
            Context = context;
        }
        
        public IList<ProductRecord> ProductList { get;set; }
        
        public IList<ProductRecord> Query { get; set; }

        public async Task OnGetAsync()
        {
            //TODO: Пересмотреть логику
            var query = Context.ProductRecords.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(SearchText) && string.IsNullOrWhiteSpace(RecordType))
                query = query
                    .Where(product => product.CategoryId == SearchText 
                    || product.ProductName.Contains(SearchText));
            else if (!string.IsNullOrWhiteSpace(RecordType) && !string.IsNullOrWhiteSpace(SearchText))
                query = query
                    .Where(product => product.CategoryId == RecordType
                                      || product.ProductName.Contains(SearchText));
            else if (!string.IsNullOrWhiteSpace(RecordType) )
                query = query
                    .Where(product => product.CategoryId == RecordType);
            if (SortType == "Name")
            {
                ProductList = await query
                    .OrderBy(product => product.CategoryId)
                    .ThenBy(product => product.ProductName)
                    .ToListAsync();
                Counter = ProductList.Count;
                if (PageNumber - 1 != 0)
                {
                    ProductList = ProductList
                        .Skip(PageSize * (PageNumber - 1))
                        .ToList();
                }
                ProductList =ProductList
                    .Take(PageSize)
                    .ToList();
                QueryCounter = ProductList.Count;
            }
            else if(SortType == "Price")
            {
                ProductList = await query
                    .OrderBy(product => product.CategoryId)
                    .ThenBy(product => product.Price)
                    .ToListAsync();
                Counter = ProductList.Count;
                if (PageNumber - 1 != 0)
                {
                    ProductList = ProductList
                        .Skip(PageSize * (PageNumber - 1))
                        .ToList();
                }
                ProductList =ProductList
                    .Take(PageSize)
                    .ToList();
                
                QueryCounter = ProductList.Count;
            }
        }

        public PageResult OnPost()
        {
            return Page();
        }
    }
}
