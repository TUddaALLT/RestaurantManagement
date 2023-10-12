using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Models;

namespace RestaurantManagement.Pages.Order
{
    public class ListOrderDetailModel : PageModel
    {

        private readonly RestaurantManagementContext _context;
        public ListOrderDetailModel(RestaurantManagementContext context)
        {
            _context = context;
        }
        public List<TableOrderCustomer> tableOrderCustomers = new List<TableOrderCustomer> { };
         public Models.Customer  customer =  new Models.Customer();
        public void OnGet()
        {
            customer = _context.Customers.Include("TableOrderCustomers").FirstOrDefault(x => x.Id == 1);


        }
    }
}
