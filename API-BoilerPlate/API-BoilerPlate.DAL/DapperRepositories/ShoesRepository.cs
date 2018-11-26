using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API_BoilerPlate.BRL.Query;
using API_BoilerPlate.DAL.DapperRepositories;
using API_BoilerPlate.DAL.Interfaces;

namespace API_BoilerPlate.DAL.DapperRepositories
{
    public class ShoesRepository : BaseRepository, IShoesRepository
    {
        public ShoesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Shoes>> GetAllShoes()
        {
            throw new Exception();
        }

        public async Task<Shoes> GetShoes(int shoeId)
        {
            throw new Exception();
        }

        public async Task<bool> SaveShoes(BRL.Command.Shoes order)
        {
            throw new Exception();
        }
    }
}