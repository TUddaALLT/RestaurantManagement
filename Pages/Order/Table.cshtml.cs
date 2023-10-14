using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Models;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace RestaurantManagement.Pages.Order
{
    public class TableModel : PageModel
    {
        private readonly RestaurantManagementContext _context;
        public TableModel(RestaurantManagementContext context)
        {
            _context = context;
        }
        public List<Models.TableOrder> tableOrders =new List<Models.TableOrder>();
        public List<Models.TableOrderCustomer> tableOrderCustomers =new List<Models.TableOrderCustomer>();
        public IActionResult OnGet()
        {

            if (HttpContext.Session.GetString("IsAuthenticated") != "true")
            {
                // User is authenticated, you can redirect to a secured page
                return RedirectToPage("/SecurePage");
            }
            else
            {
                tableOrders = _context.TableOrders.ToList();
                tableOrderCustomers = _context.TableOrderCustomers.ToList();
                return Page();
            }

         
         /*   tableOrderCustomers.ForEach(x =>
            {
                if (x.Start<DateTime.Now&& x.End>DateTime.Now)
                {
                    tableOrders = tableOrders.Where(tb => x.TableOrderId == tb.Id).ToList();
                }
                  
            });*/

            
            
        }
        public IActionResult OnPost(int table_id, DateTime start, DateTime end)
        {
            // get customer_id from session
            _context.TableOrderCustomers.Add(new TableOrderCustomer
            {
                TableOrderId = table_id,
                Start = start,
                End = end   ,
                CustomerId = 2
            });
            _context.SaveChanges();
            TableOrderCustomer tableOrderCustomer = _context.TableOrderCustomers.FirstOrDefault(x=>x.CustomerId==2
                                                                                              && x.TableOrderId==table_id && x.Start == start);
            HttpContext.Session.SetString("TableOrderCustomerId", tableOrderCustomer.Id!=null?tableOrderCustomer.Id+"":0+"");

            // session set  tableOrderCustomer.Id
            Console.WriteLine(tableOrderCustomer.Id);
            return RedirectToPage("/order/food");
        }
    }
}
