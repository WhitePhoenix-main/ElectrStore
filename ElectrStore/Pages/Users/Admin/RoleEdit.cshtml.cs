using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ElectrStore;


public class RoleEdit : PageModel
{
    private RoleManager<IdentityRole> _roleManager { get; set; }
    private UserManager<IdentityUser> _userManager { get; set; }

    public List<IdentityRole> AllRoles { get; set; }

    public List<IdentityUser>? AllUsers { get; private set; }
    [BindProperty] public string? RoleName { get; set; }
    
    [BindProperty] public string? RoleId { get; set; }
    [BindProperty] public IdentityRole Role { get; set; }
    public string? userId { get; set; }

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

    public async Task<IActionResult> OnPostCreateAsync()
    {
        if (string.IsNullOrEmpty(RoleName))
        {
            ModelState.AddModelError("name", "Название не может быть пустым");
            return Page();
        }

        var result = await _roleManager.CreateAsync(new IdentityRole(RoleName));

        if (result.Succeeded)
            return RedirectToPage("/Users/Admin/RolesIndex");
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("name", error.Description);
        }

        return RedirectToPage("/Users/Admin/RolesIndex");
    }
    
    public async Task<IActionResult> OnGetEditAsync(string roleId)
    {
        /*if (!User.IsInRole("admin"))
            return Forbid();*/
        if (roleId is not null)
        {
            Role = await _roleManager.FindByIdAsync(roleId);
        }
        IsNewRec = false;
        return Page();
    }

    public async Task<IActionResult> OnPostAddToRoleAsync(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        await _userManager.AddToRoleAsync(user, roleName);
        /*await _userManager.UpdateAsync(user);*/
        return RedirectToPage("/Users/Admin/RolesIndex");
    }

    public async Task<IActionResult> OnPostEdit(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        role.Name = RoleName;
        await _roleManager.UpdateAsync(role);
        return RedirectToPage("/Users/Admin/RolesIndex");
    }
}