using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API_BoilerPlate.BRL.Query;
using API_BoilerPlate.DAL.Interfaces;

namespace API_BoilerPlate.DAL.DapperRepositories
{
    public class OrdersRepository : BaseRepository, IOrdersRepository
    {
        public OrdersRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            throw new Exception();
        }

        public async Task<Order> GetOrder(int orderId)
        {
            throw new Exception();
        }

        public async Task<bool> SaveOrder(BRL.Command.Order order)
        {
            throw new Exception();
        }

    }
}
