using System;

namespace API_BoilerPlate.BRL.Query
{
    public class Order
    {
        public int Id { get; set; }

        public string OrderedBy { get; set; }

        public DateTime? OrderedDate { get; set; }
    }
}