using System;
using System.Collections.Generic;

namespace API_BoilerPlate.DAL.Entities
{
    public partial class Orders
    {
        public Orders()
        {
            OrderShoes = new HashSet<OrderShoes>();
        }

        public int Id { get; set; }
        public string OrderedBy { get; set; }
        public DateTime? Date { get; set; }

        public ICollection<OrderShoes> OrderShoes { get; set; }
    }
}
