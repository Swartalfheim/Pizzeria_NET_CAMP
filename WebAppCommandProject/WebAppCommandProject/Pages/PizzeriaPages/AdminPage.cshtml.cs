using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppCommandProject.Pages.PizzeriaPages
{
    [Authorize(Roles = "admin")]
    public class AdminPageModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
