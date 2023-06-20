using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppCommandProject.Pages.PizzeriaPages
{
    [Authorize(Roles = "manager")]
    public class ManagerPageModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
