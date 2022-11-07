﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElectrStore;
/*[Authorize(Roles="admin")]*/
public class UsersList : PageModel
{
    private RoleManager<IdentityRole> _roleManager { get; }
    public List<IdentityRole> AllRoles { get; set; }

    public UsersList(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public IActionResult OnGet()
    {
        AllRoles = _roleManager.Roles.ToList();
        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role != null)
        { await _roleManager.DeleteAsync(role);
            
        }
        return RedirectToAction("/Admin/RolesIndex");
    }

    
}