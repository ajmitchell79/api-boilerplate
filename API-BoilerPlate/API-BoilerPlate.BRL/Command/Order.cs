using System;

namespace API_BoilerPlate.BRL.Command
{
    public class Order
    {
        public int Id { get; set; }

        public string OrderedBy { get; set; }

        public DateTime? OrderedDate { get; set; }
    }
}