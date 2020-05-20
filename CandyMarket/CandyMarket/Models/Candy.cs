using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyMarket.Models
{
    public class Candy
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Flavor { get; set; }
        public string Manufacturer { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
