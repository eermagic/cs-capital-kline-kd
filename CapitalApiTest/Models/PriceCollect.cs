using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalApiTest.Models
{
    public class ClosePriceModel
    {
        public DateTime Datetime { get; set; }
        public double Price { get; set; }
        public int Qty { get; set; }
    }
}
