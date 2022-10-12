using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalApiTest.Models
{
    public class KlineModel
    {
        public int idx { get; set; }
        public DateTime KlineTime { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public int Qty { get; set; }
        public double? Kd_K { get; set; }
        public double? Kd_D { get; set; }
    }
}
