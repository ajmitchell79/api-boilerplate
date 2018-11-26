using System;
using System.Collections.Generic;
using API_BoilerPlate.DAL.Entities;

namespace API_BoilerPlate.DAL.Entities
{
    public partial class OrderShoes
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ShoeId { get; set; }

        public Orders Order { get; set; }
        public Shoes Shoe { get; set; }
    }
}
