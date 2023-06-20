using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppCommandProject.Pages.PizzeriaPages
{
    [Authorize(Roles = "user")]
    public class ClientPageModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
