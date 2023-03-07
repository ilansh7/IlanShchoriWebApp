using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace IlanShchoriWebApp.Services
{
    public class DbServices : ICalc<double>
    {
        #region variables
        private static Data.DbData _data;
        private static string _controllereName = "DbServices";
        private static string _methodeName = "";
        #endregion variables
        #region propertirs
        public Data.DbData Data
        {
            get
            {
                if (_data == null)
                {
                    return _data = new Data.DbData();
                }
                else
                {
                    return _data;
                }
            }
        }

        #endregion propertirs
        #region c'tor
        public DbServices()
        {
            _data = new Data.DbData();
        }
        #endregion c'tor
        #region intreface
        public double Add(double num1, double num2)
        {
            return num1 + num2;
        }
        public double Sub(double num1, double num2)
        {
            return num1 - num2;
        }
        public double Mul(double num1, double num2)
        {
            return num1 * num2;
        }
        public double Div(double num1, double num2)
        {
            return num1 / num2;
        }
        public double CustomOper1(double num1, double num2)
        {
            return Math.Pow(num1, num2);
        }
        public double CustomOper2(double num1, double num2)
        {
            throw new NotImplementedException();
        }
        public double Operation(string operation, double num1, double num2)
        {
            //throw new NotImplementedException();
            double res = 0;
            switch (operation)
            {
                case "Add":
                    res = Add(num1, num2);
                    break;
                case "Sub":
                    res = Sub(num1, num2);
                    break;
                case "Mul":
                    res = Mul(num1, num2);
                    break;
                case "Div":
                    res = Div(num1, num2);
                    break;
                case "CustomOper1":
                    res = CustomOper1(num1, num2);
                    break;
                default:
                    break;
            }
            return res;
        }
        #endregion intreface
        public Dictionary<int, string> GetOperationsList()
        {
            _methodeName = "GetOperationsList";
            //Common.Logger.Logging(Common.LoggingMode.Debug, "Starting {controller}\\{methode}", _controllereName, _methodeName);

            try
            {
                //Dictionary<int, string> list = Data.GetOperationsList();
                Dictionary<int, string> list = new Dictionary<int, string>();
                //var lista = GetMethods(Type.GetType("Interface"));
                var lista = typeof(ICalc<double>).GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                int idx = 0;
                list.Add(idx++, String.Empty);
                foreach (var item in lista)
                {
                    list.Add(idx++, item.Name);
                }
                //Common.Logger.Logging(Common.LoggingMode.Debug, "Exit {controller}\\{methode}", _controllereName, _methodeName);
                return list;
            }
            catch (System.Exception ex)
            {
                //Common.Logger.Logging(Common.LoggingMode.Error, "\t Exception on {controller}\\{methode} : {@ex}", _controllereName, _methodeName, ex);
                throw ex;
            }
        }

        public int WriteHistory(Entities.Gaya model, bool toDeleteFlag)
        {
            _methodeName = "WriteHistory";
            //Common.Logger.Logging(Common.LoggingMode.Debug, "Starting {controller}\\{methode}", _controllereName, _methodeName);

            try
            {
                int ret = Data.WriteHistory(model, toDeleteFlag);
                //Common.Logger.Logging(Common.LoggingMode.Debug, "Exit {controller}\\{methode}", _controllereName, _methodeName);
                return ret;
            }
            catch (System.Exception ex)
            {
                //Common.Logger.Logging(Common.LoggingMode.Error, "\t Exception on {controller}\\{methode} : {@ex}", _controllereName, _methodeName, ex);
                throw ex;
            }

        }

        public DataTable GetHistoryByOperation(int depth, string operation)
        {
            _methodeName = "GetHistoryByOperation";
            //Common.Logger.Logging(Common.LoggingMode.Debug, "Starting {controller}\\{methode}", _controllereName, _methodeName);

            try
            {
                DataTable dt = Data.GetHistoryByOperation(depth, operation);
                //Common.Logger.Logging(Common.LoggingMode.Debug, "Exit {controller}\\{methode}", _controllereName, _methodeName);
                return dt;
            }
            catch (System.Exception ex)
            {
                //Common.Logger.Logging(Common.LoggingMode.Error, "\t Exception on {controller}\\{methode} : {@ex}", _controllereName, _methodeName, ex);
                throw ex;
            }
        }

        public DataTable GetStatisticsByOperation(string operation)
        {
            _methodeName = "GetStatisticsByOperation";
            //Common.Logger.Logging(Common.LoggingMode.Debug, "Starting {controller}\\{methode}", _controllereName, _methodeName);

            try
            {
                DataTable dt = Data.GetStatisticsByOperation(operation);
                //Common.Logger.Logging(Common.LoggingMode.Debug, "Exit {controller}\\{methode}", _controllereName, _methodeName);
                return dt;
            }
            catch (System.Exception ex)
            {
                //Common.Logger.Logging(Common.LoggingMode.Error, "\t Exception on {controller}\\{methode} : {@ex}", _controllereName, _methodeName, ex);
                throw ex;
            }
        }

        
    }
}
