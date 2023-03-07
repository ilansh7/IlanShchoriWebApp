using System;

namespace IlanShchoriWebApp.Models
{
    public class GayaHistory
    {
        public int Id { get; set; }
        public string Operation { get; set; }
        public double Input1 { get; set; }
        public double Input2 { get; set; }
        public double Input3 { get; set; }
        public string Query { get; set; }
        public double Result { get; set; }
        public DateTime Timestamp { get; set; }
    }
}