using Microsoft.AspNetCore.Http;
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

        public List<Models.TableOrder> tableOrders = new List<Models.TableOrder> { };
         public Models.Customer  customer =  new Models.Customer();
        public void OnGet()
        {
            try
            {
                customer = _context.Customers.Include("TableOrderCustomers")
                .FirstOrDefault(x => x.Username == HttpContext.Session.GetString("Username"));
                tableOrderCustomers = customer.TableOrderCustomers.ToList();
                tableOrderCustomers.ForEach(x => {
                    Models.TableOrder tableOrder = _context.TableOrders.FirstOrDefault(y => y.Id == x.TableOrderId);
                    tableOrders.Add(tableOrder);
                });
            }catch (Exception ex) { 
                
            }


        }
    }
}
