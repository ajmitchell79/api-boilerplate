using System.Collections.Generic;
using System.Threading.Tasks;
using API_BoilerPlate.BRL.Interfaces;
using API_BoilerPlate.BRL.Query;
using API_BoilerPlate.DAL.Interfaces;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace API_BoilerPlate.Services
{
    public class OrdersService : IOrdersService
    {
        //FOR DAPPER Repo's
        //private readonly IOrdersRepository _ordersRepository;
        //public OrdersService(IOrdersRepository ordersRepository)
        //{
        //    _ordersRepository = ordersRepository;
        //}

        private readonly IRepository<DAL.Entities.Orders> _ordersRepository;

        public OrdersService(IRepository<DAL.Entities.Orders> ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            var result = await _ordersRepository.GetAll()
                .Select(x =>
                    Mapper.Map<Order>(x))
                    //new BRL.Query.Order()
                    //{
                    //   Id = x.Id,
                    //    OrderedBy = x.OrderedBy,
                    //    OrderedDate = x.Date
                    //})
                    .ToListAsync();
            return result;
        }

        public async Task<List<OrderDetailed>> GetAllOrdersDetailed()
        {
            var result = await _ordersRepository.GetAll()
                    .Include(a=> a.OrderShoes)
                    .ThenInclude(b=>b.Shoe)
                    .Select(x =>
                    new OrderDetailed()
                    {
                        OrderId = x.Id
                       
                    }
                )
                    //Mapper.Map<Order>(x))
                    
                .ToListAsync();
            return result;
        }
        public async Task<Order> GetOrder(int orderId)
        {
            var result = await _ordersRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == orderId);

            return Mapper.Map<BRL.Query.Order>(result);
        }

        public async Task<bool> SaveOrder(BRL.Command.Order order)
        {
            //var o = new DAL.Entities.Orders() {Id = order.Id, OrderedBy = order.OrderedBy, Date = order.OrderedDate};
            //return await _ordersRepository.Create(Mapper.Map<Order>(order)) > 0;
            return await _ordersRepository.Create(Mapper.Map<DAL.Entities.Orders>(order)) > 0;
           
        }
    }
}