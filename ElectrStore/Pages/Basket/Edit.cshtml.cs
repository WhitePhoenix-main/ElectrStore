using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ElectrStore
{
    public class EditModel : PageModel
    {
        private readonly StoreContext _context;

        public EditModel(StoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrderItemsRecord OrderItemsRecord { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.OrderItemsRecords == null)
            {
                return NotFound();
            }

            var orderitemsrecord =  await _context.OrderItemsRecords
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderitemsrecord == null)
            {
                return NotFound();
            }
            OrderItemsRecord = orderitemsrecord;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var rec = new OrderItemsRecord();
            if (!ModelState.IsValid)
                return Page();
            rec.BuyQuantity = OrderItemsRecord.BuyQuantity;
            rec.ProductName = OrderItemsRecord.ProductName;
            rec.Id = OrderItemsRecord.Id;
            rec.OrderId = OrderItemsRecord.OrderId;
            rec.Price = OrderItemsRecord.Price;
            rec.ProductId = OrderItemsRecord.ProductId;
            _context.Update(rec);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        private bool OrderItemsRecordExists(string id)
        {
          return (_context.OrderItemsRecords?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
