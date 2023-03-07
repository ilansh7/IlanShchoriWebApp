using IlanShchoriWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        private int _histDepth = 10;
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
        public int HistortDepth
        {
            get { return _histDepth; }
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
            Dictionary<int, string> Operations = service.GetOperationsList();
            ViewBag.Operations = Operations;
            ViewBag.Operation = Operations[int.Parse(model.Operation)];
            ViewBag.HistortDepth = HistortDepth;
            model.Result = service.Operation(Operations[int.Parse(model.Operation)], model.Input01, model.Input02);
            if (int.Parse(model.Operation) > 0)
            {
                Entities.Gaya gaya = new Entities.Gaya();
                gaya.Id = model.Id;
                gaya.Result = model.Result;
                if (double.IsInfinity(gaya.Result))
                {
                    gaya.Result = double.MaxValue;    
                }
                //gaya.Query = String.Concat(model.Input01.ToString(), " ", Operations[int.Parse(model.Operation)], " ", model.Input02.ToString());
                gaya.Operation = Operations[int.Parse(model.Operation)];
                gaya.Input01 = model.Input01;
                gaya.Input02 = model.Input02;
                int ret = service.WriteHistory(gaya, false);

                DataTable dt = service.GetHistoryByOperation(HistortDepth, Operations[int.Parse(model.Operation)]);
                model.History = (from rw in dt.AsEnumerable()
                                     select new GayaHistory()
                                     {
                                         Id = Convert.ToInt32(rw["Id"]),
                                         Operation = Convert.ToString(rw["Operation"]),
                                         Input1 = Convert.ToDouble(rw["Input1"]),
                                         Input2 = Convert.ToDouble(rw["Input2"]),
                                         //Input3 = Convert.ToDouble(rw["Input3"]),
                                         //Query = Convert.ToString(rw["Query"]),
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
            ViewBag.Message = "About Calculator";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "אילן שחורי";
            ViewBag.Title = "";

            return View();
        }
    }
}