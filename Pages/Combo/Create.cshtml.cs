using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RestaurantManagement.Pages.Combo
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
            return Page();
        }

        [BindProperty]
        public Models.Combo Combo { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Combos == null || Combo == null)
            {
                return Page();
            }

            _context.Combos.Add(Combo);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
