using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ElectrStore;

[Authorize]
public class RolesIndex : PageModel
{
    private RoleManager<IdentityRole> _roleManager { get; }
    private StoreContext _storeContext { get; }
    public List<IdentityRole> AllRoles { get; set; }
    public List<IdentityUser> UserList { get; set; }

    public RolesIndex(RoleManager<IdentityRole> roleManager, StoreContext storeContext)
    {
        _roleManager = roleManager;
        _storeContext = storeContext;
    }

    public IActionResult OnGet()
    {
        AllRoles = _roleManager.Roles.ToList();
        UserList = _storeContext.Users.ToList();
        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role != null)
            await _roleManager.DeleteAsync(role);
        return RedirectToAction("/Admin/RolesIndex");
    }
}