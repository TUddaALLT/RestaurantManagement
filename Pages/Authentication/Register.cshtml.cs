using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantManagement.Models;

namespace RestaurantManagement.Pages.Authentication
{
    public class RegisterModel : PageModel
    {
        private readonly RestaurantManagementContext _context;
        public RegisterModel(RestaurantManagementContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost(Models.Customer c)
        {
            _context.Customers.Add(c);
            _context.SaveChanges();

            return RedirectToPage("/Authentication/Login");

        }
    }
}
