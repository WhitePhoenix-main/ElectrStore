using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ElectrStore;

public class AdminOrderDetail : PageModel
{
    public AdminOrderDetail(StoreContext storeContext)
    {
        _storeContext = storeContext;
    }
    private StoreContext _storeContext { get; set; }
    public List<OrderItemsRecord> OrderItems { get; set; } = new();
    public async Task<IActionResult> OnGet(string id)
    {
        var orderItems = await _storeContext.OrderItemsRecords
            .Where(oRec => oRec.OrderId == id)
            .ToListAsync();
        OrderItems = orderItems;
        return Page();
    }
}