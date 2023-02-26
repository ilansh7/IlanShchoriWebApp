using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace IlanShchoriWebApp.Models
{
    public class GayaModel
    {
        public int Id { get; set; }
        [Display(Name = "קלט :")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "יש להזין ספרות בלבד")]
        public double Input01 { get; set; }
        [Display(Name = "קלט :")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "יש להזין ספרות בלבד")]
        public double Input02 { get; set; }
        [Display(Name = "פלט :")]
        public double Result { get; set; }
        [Display(Name = "פעולה :")]
        public string Operation { get; set; }
        public int OperationsFromBeginingOfCurrentMonth { get; set; }
        public double MinResultByOperation { get; set; }
        public double MaxResultByOperation { get; set; }
        public double AvgResultByOperation { get; set; }
        public List<GayaHistory> History { get; set; }
    }
}