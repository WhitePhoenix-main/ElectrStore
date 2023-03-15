using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElectrStore.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    private readonly StoreContext _context;

    public readonly IPasswordHasher<IdentityUser> _passwordHasher;

    public IndexModel(StoreContext context, ILogger<IndexModel> logger, IPasswordHasher<IdentityUser> passwordHasher)
    {
        _context = context;
        _logger = logger;
        _passwordHasher = passwordHasher;
    }

    public void OnGet()
    {
    }
}