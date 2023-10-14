using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RestaurantManagement.Models;

namespace RestaurantManagement.Pages.Authentication
{
    public class LoginModel : PageModel
    {

        private readonly RestaurantManagementContext _context;
        public LoginModel(RestaurantManagementContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            
        }
        public String message;
        public IActionResult OnPost(String Username, String Password)
        {

           if (_context.Customers.FirstOrDefault(x => x.Username == Username && x.Password == Password)!= null)
            {
                    HttpContext.Session.SetString("IsAuthenticated", "true");
                    return RedirectToPage("/Order/Table");
            }
            else
            {
                message = "Login Failed";
                return Page();
            }

        }
    }
}
