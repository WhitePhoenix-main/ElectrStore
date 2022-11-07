using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ElectrStore;

namespace ElectrStore.Pages.Products.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ElectrStore.StoreContext _context;

        public IndexModel(StoreContext context)
        {
            _context = context;
        }

        public IList<ProductRecord> ProductRecord { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ProductRecords != null)
            {
                ProductRecord = await _context.ProductRecords.ToListAsync();
            }
        }
    }
}
