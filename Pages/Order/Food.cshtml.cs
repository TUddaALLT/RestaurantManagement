using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Models;

namespace RestaurantManagement.Pages.Order
{
    public class IndexModel : PageModel
    {

        private readonly RestaurantManagementContext _context;
        public IndexModel(RestaurantManagementContext context)
        {
            _context = context;
        }
        public List<Models.Food> food = new List<Food> { };
        public List<Models.FoodCategory> foodCategories = new List<FoodCategory> { };
        public List<Models.Combo> combo = new List<Models.Combo> { };
        public List<Models.FoodCombo> foodcombo = new List<FoodCombo> { };
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("IsAuthenticated") != "true")
            {
                // User is authenticated, you can redirect to a secured page
                return RedirectToPage("/SecurePage");
            }
            else
            {
                foodCategories = _context.FoodCategories.Include("Foods").ToList();
                combo = _context.Combos.Include("FoodCombos").ToList();
                foodcombo = _context.FoodCombos.Include("Food").ToList();
                return Page();
            }

          
        }
        public IActionResult OnPost(String[] foodId, String[] comboId )
        {
            if(foodId.Length!= 0)
            {

                 foodId.ToList().ForEach(food => {
                     _context.FoodTables.Add(new Models.FoodTable
                     {
                         FoodId =int.Parse(food),
                         TableOrderCustomerId = int.Parse(HttpContext.Session.GetString("TableOrderCustomerId")) // get from session

                     });
                    _context.SaveChanges();
                 });
            }
            if(comboId.Length!= 0)
            {
                comboId.ToList().ForEach(combo => {

                    _context.FoodTables.Add(new Models.FoodTable
                    {
                        ComboId = int.Parse(combo),
                        TableOrderCustomerId = int.Parse(HttpContext.Session.GetString("TableOrderCustomerId"))// get from session
                    });
                    _context.SaveChanges();
                });
            }
         
            return RedirectToPage("/order/success");
        }
    }
}
