using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantManagement.Models;

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
        public void OnGet()
        {
            tableOrders = _context.TableOrders.ToList();
            tableOrderCustomers = _context.TableOrderCustomers.ToList();
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
