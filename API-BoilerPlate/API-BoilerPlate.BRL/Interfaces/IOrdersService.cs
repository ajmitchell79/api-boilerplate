using System.Collections.Generic;
using System.Threading.Tasks;
using API_BoilerPlate.BRL.Query;

namespace API_BoilerPlate.BRL.Interfaces
{
    public interface IOrdersService
    {
        Task<List<Order>> GetAllOrders();

        Task<Order> GetOrder(int orderId);

        Task<bool> SaveOrder(BRL.Command.Order order);

        Task<List<OrderDetailed>> GetAllOrdersDetailed();
    }
}