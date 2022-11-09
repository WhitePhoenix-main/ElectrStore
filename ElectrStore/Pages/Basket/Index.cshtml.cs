using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ElectrStore;

namespace ElectrStore.Pages.Buscket
{
    public class IndexModel : PageModel
    {
        private readonly ElectrStore.StoreContext _context;

        public IndexModel(ElectrStore.StoreContext context)
        {
            _context = context;
        }

        public IList<OrderItemsRecord> OrderItemsRecord { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.OrderItemsRecords != null)
            {
                OrderItemsRecord = await _context.OrderItemsRecords.ToListAsync();
            }
        }
    }
}
