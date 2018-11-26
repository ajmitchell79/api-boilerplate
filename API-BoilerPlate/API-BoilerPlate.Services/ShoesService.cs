using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_BoilerPlate.BRL.Interfaces;
using API_BoilerPlate.BRL.Query;
using API_BoilerPlate.DAL.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API_BoilerPlate.Services
{
    public class ShoesService : IShoesService
    {
        //FOR DAPPER Repo's
        //private readonly IShoesRepository _shoesRepository;
        //public ShoesService(IShoesRepository shoesRepository)
        //{
        //    _shoesRepository = shoesRepository;
        //}

        private readonly IRepository<DAL.Entities.Shoes> _shoesRepository;

        public ShoesService(IRepository<DAL.Entities.Shoes> shoesRepository)
        {
            _shoesRepository = shoesRepository;
        }

        public async Task<List<Shoes>> GetAllShoes()
        {
            var result = await _shoesRepository.GetAll()
                .Select(x =>
                    Mapper.Map<Shoes>(x))
                .ToListAsync();
            return result;
        }

        public async Task<Shoes> GetShoes(int orderId)
        {
            var result = await _shoesRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == orderId);

            return Mapper.Map<BRL.Query.Shoes>(result);
        }

        public async Task<bool> SaveShoes(BRL.Command.Shoes shoes)
        {
           // var o = new DAL.Entities.Shoes() { Id = shoes.Id,Price = shoes.Price,Added = shoes.DateAdded,Name = shoes.Name};
            
            //return await _ordersRepository.Create(Mapper.Map<Order>(order)) > 0;
            return await _shoesRepository.Create(Mapper.Map<DAL.Entities.Shoes>(shoes)) > 0;
        }
    }
}