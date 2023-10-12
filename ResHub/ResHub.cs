

using Microsoft.AspNetCore.SignalR;
using RestaurantManagement.Models;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace RestaurantManagement.ResHub
{
    public class ResHub: Hub
    {

        private readonly RestaurantManagementContext _context;
        public ResHub(RestaurantManagementContext context)
        {
            _context = context;
        }


        public async Task DeleteTableOrder(int id)
        {
            TableOrder tableOrder = _context.TableOrders.FirstOrDefault(f=>f.Id==id);
            _context.TableOrders.Remove(tableOrder);
            _context.SaveChanges();
            await Clients.All.SendAsync("LoadTableOrder",_context.TableOrders.ToList());
        }
       

    }
    
}
