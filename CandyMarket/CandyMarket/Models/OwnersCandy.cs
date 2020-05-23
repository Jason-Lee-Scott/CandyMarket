using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandyMarket.Models
{
    public class OwnersCandy
    {
        public int ID { get; set; }
        public DateTime DateReceived { get; set; }
        public int UserId { get; set; }
        public int CandyId { get; set; }
        public bool IsEaten { get; set; }
        public DateTime EatenDate { get; set; }
    }

    public class EatenOwnersCandy
    {
        public int OwnersCandyId { get; set; }
        public DateTime DateReceived { get; set; }
        public string UserName { get; set; }
        public string CandyName { get; set; }
        public bool IsEaten { get; set; }
        public DateTime EatenDate { get; set; }
    }
}
