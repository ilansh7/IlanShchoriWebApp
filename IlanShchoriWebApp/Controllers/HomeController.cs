using IlanShchoriWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace IlanShchoriWebApp
{
    public class HomeController : Controller
    {
        #region variables
        //private static Data.DbData _data;
        //private static string _controllereName = "DLPServices";
        //private static string _methodeName = "";
        private static Services.DbServices _srv;
        public enum Operations
        {
            Add = 1,
            Sub = 2,
            Mul = 3,
            Div = 4,
        }
        #endregion variables
        #region propertirs
        public Services.DbServices service
        {
            get
            {
                if (_srv == null)
                {
                    return _srv = new Services.DbServices();
                }
                else
                {
                    return _srv;
                }
            }
        }
        #endregion propertirs
        #region c'tor
        public HomeController()
        {
            _srv = new Services.DbServices();
        }
        #endregion c'tor
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Operations = service.GetOperationsList();
            //GayaModel model = new GayaModel();
            //return View(model);
            return View();
        }

        [HttpPost]
        public ActionResult Index(GayaModel model)
        {
            switch (model.Operation)
            {
                case "1":
                    model.Result = model.Input01 + model.Input02;
                    break;
                case "2":
                    model.Result = model.Input01 - model.Input02;
                    break;
                case "3":
                    model.Result = model.Input01 * model.Input02;
                    break;
                case "4":
                    model.Result = model.Input01 / model.Input02;
                    break;
                case "5":
                    model.Result = Math.Pow(model.Input01, model.Input02);
                    break;
                default:
                    break;
            }
            Dictionary<int, string> Operations = service.GetOperationsList();
            ViewBag.Operations = Operations;
            if (!string.IsNullOrEmpty(model.Operation))
            {
                Entities.Gaya gaya = new Entities.Gaya();
                gaya.Id = model.Id;
                gaya.Result = model.Result;
                if (double.IsInfinity(gaya.Result))
                {
                    gaya.Result = double.MaxValue;    
                }
                gaya.Query = String.Concat(model.Input01.ToString(), " ", Operations[int.Parse(model.Operation)], " ", model.Input02.ToString());
                int ret = service.WriteHistory(gaya, false);

                DataTable dt = service.GetHistoryByOperation(3, Operations[int.Parse(model.Operation)]);
                model.History = (from rw in dt.AsEnumerable()
                                     select new GayaHistory()
                                     {
                                         Id = Convert.ToInt32(rw["Id"]),
                                         Query = Convert.ToString(rw["Query"]),
                                         Result = Convert.ToDouble(rw["Result"]),
                                         Timestamp = Convert.ToDateTime(rw["Timestamp"])
                                     }).ToList();

                dt = service.GetStatisticsByOperation(Operations[int.Parse(model.Operation)]);
                model.OperationsFromBeginingOfCurrentMonth = int.Parse(dt.Rows[0]["op_count"].ToString());
                model.MinResultByOperation = double.Parse(dt.Rows[0]["op_min"].ToString());
                model.MaxResultByOperation = double.Parse(dt.Rows[0]["op_max"].ToString());
                model.AvgResultByOperation = double.Parse(dt.Rows[0]["op_avg"].ToString());
            }

            return View(model);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}