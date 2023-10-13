using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantManagement.Models;

namespace RestaurantManagement.Pages.FoodMng
{
    public class CreateModel : PageModel
    {
        private readonly RestaurantManagement.Models.RestaurantManagementContext _context;

        public CreateModel(RestaurantManagement.Models.RestaurantManagementContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["Name"] = new SelectList(_context.FoodCategories.Select(fc => fc.Name).Distinct());
            return Page();
        }

        [BindProperty]
        public Food Food { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Foods == null || Food == null)
            {
                return Page();
            }

            _context.Foods.Add(Food);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
