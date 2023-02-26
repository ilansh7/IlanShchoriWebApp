using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IlanShchoriWebApp.Entities
{
    public class Gaya
    {
        public int Id { get; set; }
        public double Input01 { get; set; }
        public double Input02 { get; set; }
        public double Result { get; set; }
        public string Operation { get; set; }
        public string Query { get; set; }
    }
}
