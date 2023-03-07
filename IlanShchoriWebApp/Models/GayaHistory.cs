using System;

namespace IlanShchoriWebApp.Models
{
    public class GayaHistory
    {
        public int Id { get; set; }
        public string Query { get; set; }
        public double Result { get; set; }
        public DateTime Timestamp { get; set; }
    }
}