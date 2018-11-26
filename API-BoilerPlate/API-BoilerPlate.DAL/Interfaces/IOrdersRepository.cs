using System.Collections.Generic;
using System.Threading.Tasks;
using API_BoilerPlate.BRL.Query;

namespace API_BoilerPlate.DAL.Interfaces
{
    public interface IOrdersRepository
    {
        Task<List<Order>> GetAllOrders();

        Task<Order> GetOrder(int orderId);

        Task<bool> SaveOrder(BRL.Command.Order order);
    }
}