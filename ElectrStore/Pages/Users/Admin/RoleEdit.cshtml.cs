using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ElectrStore;

public class RoleEdit : PageModel
{
    private RoleManager<IdentityRole> _roleManager { get; set; }
    private UserManager<IdentityUser> _userManager { get; set; }
    
    private readonly StoreContext _context;

    public List<IdentityRole> AllRoles { get; set; }

    public List<IdentityUser>? AllUsers { get; private set; }
    public string? RoleName { get; set; }
    
    public string? RoleId { get; set; }

    public bool IsNewRec { get; private set; }

    public RoleEdit(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<IActionResult> OnGetDeleteAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role != null)
        {
            await _roleManager.DeleteAsync(role);
        }

        return RedirectToPage("/admin/rolesindex");
    }

    public async Task<IActionResult> OnPostCreateAsync(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            ModelState.AddModelError("name", "Название не может быть пустым");
            return Page();
        }

        var result = await _roleManager.CreateAsync(new IdentityRole(name));

        if (result.Succeeded)
            return RedirectToPage("/Users/Admin/RolesIndex");
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("name", error.Description);
        }

        return RedirectToPage("/Users/Admin/RolesIndex");
    }

    public IActionResult OnGetEdit(string id)
    {
        /*if (!User.IsInRole("admin"))
            return Forbid();*/
        RoleId = id;
        IsNewRec = false;
        return Page();
    }

    public async Task<IActionResult> OnPostEdit(string id, string name)
    {
        var role = await _roleManager.FindByIdAsync(id);
        role.Name = name;
        await _roleManager.UpdateAsync(role);
        return RedirectToPage("/Users/Admin/RolesIndex");
    }
    
}