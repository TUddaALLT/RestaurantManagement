using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Models;

namespace RestaurantManagement.Pages.category
{
    public class IndexModel : PageModel
    {
        private readonly RestaurantManagement.Models.RestaurantManagementContext _context;

        public IndexModel(RestaurantManagement.Models.RestaurantManagementContext context)
        {
            _context = context;
        }

        public IList<FoodCategory> FoodCategory { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.FoodCategories != null)
            {
                FoodCategory = await _context.FoodCategories
                .GroupBy(fc => fc.Name)
                .Select(group => group.First())
                .ToListAsync();
            }
        }
    }
}
