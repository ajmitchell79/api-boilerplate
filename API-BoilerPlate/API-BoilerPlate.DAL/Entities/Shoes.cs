using System;
using System.Collections.Generic;

namespace API_BoilerPlate.DAL.Entities
{
    public partial class Shoes
    {
        public Shoes()
        {
            OrderShoes = new HashSet<OrderShoes>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime? Added { get; set; }

        public ICollection<OrderShoes> OrderShoes { get; set; }
    }
}
