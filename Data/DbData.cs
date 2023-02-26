using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace IlanShchoriWebApp.Data
{
    public class DbData
    {
        #region variables
        //private static Data.DbData _data;
        private static string _controllereName = "DbData";
        private static string _methodeName = "";
        #endregion variables
        #region propertirs
        public string DbConnStr
        {
            get { return ConfigurationManager.ConnectionStrings["ConnStr"].ToString(); }
        }
        #endregion propertirs
        #region c'tor
        public DbData()
        {
            //_data = new Data.DbData();
        }
        #endregion c'tor
        public Dictionary<int, string> GetOperationsList()
        {
            _methodeName = "GetOperationsList";
            //Common.Logger.Logging(Common.LoggingMode.Debug, "Starting {controller}\\{methode}", _controllereName, _methodeName);

            try
            {
                // Fill Operation list manually
                //Dictionary<int, string> retList = new Dictionary<int, string>();
                //retList.Add(0, " ");
                //retList.Add(1, "+");
                //retList.Add(2, "-");
                //retList.Add(3, "*");
                //retList.Add(4, "/");

                // Fill Operation list from DB
                Dictionary<int, string> retList = GetListValues("Operations");

                return retList;
            }
            catch (Exception ex)
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
                //var query = string.IsNullOrEmpty(tlsName) ? "" : "where Name like '%" + tlsName.ToLower() + "%'";
                using (var connection = new SqlConnection(DbConnStr))
                {
                    int ret = 0;// = -1;

                    if (toDeleteFlag)
                    {
                        ret = 1;// connection.Delete(model);
                        //Common.Logger.Logging(Common.LoggingMode.Information, "\t Record {model} deleted by {user}.", model.Name, user);
                    }
                    else
                    {
                        connection.Open();

                        SqlCommand cmd = new SqlCommand("WriteHistory", connection);

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@query", model.Query));
                        cmd.Parameters.Add(new SqlParameter("@result", model.Result));

                        ret = cmd.ExecuteNonQuery();
                    }
                    //Common.Logger.Logging(Common.LoggingMode.Debug, "Exit {controller}\\{methode}", _controllereName, _methodeName);
                    return (int)ret;
                }
            }
            catch (System.Exception ex)
            {
                //Common.Logger.Logging(Common.LoggingMode.Error, "\t Exception on {controller}\\{methode} : {@ex}", _controllereName, _methodeName, ex);
                throw ex;
            }
        }

        public DataTable GetHistoryByOperation(int depth, string operation)
        {
            _methodeName = "WriteHistory";
            //Common.Logger.Logging(Common.LoggingMode.Debug, "Starting {controller}\\{methode}", _controllereName, _methodeName);

            try
            {
                using (var connection = new SqlConnection(DbConnStr))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("GetHistoryByOperation", connection);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@depth", depth));
                    cmd.Parameters.Add(new SqlParameter("@operation", operation));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return dt;
                }
            }
            catch (System.Exception ex)
            {
                //Common.Logger.Logging(Common.LoggingMode.Error, "\t Exception on {controller}\\{methode} : {@ex}", _controllereName, _methodeName, ex);
                throw ex;
            }
        }

        public DataTable GetStatisticsByOperation(string operation)
        {
            _methodeName = "WriteHistory";
            //Common.Logger.Logging(Common.LoggingMode.Debug, "Starting {controller}\\{methode}", _controllereName, _methodeName);

            try
            {
                using (var connection = new SqlConnection(DbConnStr))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("GetStatisticsByOperation", connection);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@operation", operation));

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return dt;
                }
            }
            catch (System.Exception ex)
            {
                //Common.Logger.Logging(Common.LoggingMode.Error, "\t Exception on {controller}\\{methode} : {@ex}", _controllereName, _methodeName, ex);
                throw ex;
            }
        }

        public Dictionary<int, string> GetListValues(string listName)
        {
            _methodeName = "GetListValues";
            //Common.Logger.Logging(Common.LoggingMode.Debug, "Starting {controller}\\{methode}", _controllereName, _methodeName);

            try
            {
                Dictionary<int, string> retList = new Dictionary<int, string>();
                retList.Add(0, " ");
                using (var connection = new SqlConnection(DbConnStr))
                {
                    switch (listName)
                    {
                        case "Operations":
                            string query = "select Id, Value from [dbo].[Operation] order by Id";

                            using (var cmd = new SqlCommand(query, connection))
                            {
                                connection.Open();
                                SqlDataAdapter da = new SqlDataAdapter(cmd);
                                DataTable dt = new DataTable();
                                da.Fill(dt);

                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    var Id = int.Parse(dt.Rows[i]["Id"].ToString());
                                    var Value = dt.Rows[i]["Value"].ToString();
                                    retList.Add(Id, Value);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    //Common.Logger.Logging(Common.LoggingMode.Debug, "Exit {controller}\\{methode}", _controllereName, _methodeName);
                    return retList;
                }
            }
            catch (Exception ex)
            {
                //Common.Logger.Logging(Common.LoggingMode.Error, "\t Exception on {controller}\\{methode} : {@ex}", _controllereName, _methodeName, ex);
                throw ex;
            }
        }


    }
}
