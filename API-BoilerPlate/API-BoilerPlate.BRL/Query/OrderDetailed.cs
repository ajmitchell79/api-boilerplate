using System;
using System.Collections.Generic;

namespace API_BoilerPlate.BRL.Query
{
    public class OrderDetailed
    {
        public int OrderId { get; set; }

        public string OrderedBy { get; set; }

        public DateTime? OrderedDate { get; set; }

        public List<Shoes> OrderedShoes { get; set; }
    }
}