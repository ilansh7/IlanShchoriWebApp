using System.Collections.Generic;
using System.Data;

namespace IlanShchoriWebApp.Services
{
    public class DbServices
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

        public Dictionary<int, string> GetOperationsList()
        {
            _methodeName = "GetOperationsList";
            //Common.Logger.Logging(Common.LoggingMode.Debug, "Starting {controller}\\{methode}", _controllereName, _methodeName);

            try
            {
                Dictionary<int, string> list = Data.GetOperationsList();
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
