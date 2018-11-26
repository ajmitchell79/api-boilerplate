using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API_BoilerPlate.BRL.Query;

namespace API_BoilerPlate.DAL.Interfaces
{
    public interface IShoesRepository
    {
        Task<List<Shoes>> GetAllShoes();


        Task<Shoes> GetShoes(int shoeId);


        Task<bool> SaveShoes(BRL.Command.Shoes shoes);
    }
}