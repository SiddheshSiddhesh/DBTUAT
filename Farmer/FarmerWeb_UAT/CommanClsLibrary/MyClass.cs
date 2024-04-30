using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using CommanClsLibrary.Repository.Classes;
using CommanClsLibrary.Repository.Models;
using static CommanClsLibrary.Repository.Enums;

namespace CommanClsLibrary
{
    public class MyClass
    {
        private SqlConnection Cnn = new SqlConnection();
        private SqlCommand Cmd = new SqlCommand();
        private SqlDataAdapter Da = new SqlDataAdapter();
        public DataSet Ds = new DataSet();

        //private string[] dtN;
        public MyClass()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Boolean OpenCnn()
        {
            try
            {
                if (Cnn.ConnectionString != "")
                {
                    Cnn.Dispose();
                }
                string a = clsSettings.strCoonectionString;

                Cnn.ConnectionString = a;
                return true;
            }
            catch (Exception ex)
            {
                MyClass.SendErrorMail(ex.ToString());
                ex.ToString();
                return false;

            }
        }
        public void ExecuteCommand(string Sql, SqlCommand cmdn)
        {
            try
            {
                cmdn.CommandType = CommandType.Text;
                cmdn.CommandText = Sql;
                int x = cmdn.ExecuteNonQuery();
            }
            catch (Exception ex)
            { 
                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "ExecuteCommand():MyClass";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "Sql :" + Sql + "\n Exception : " + ex.Message + " \n Inner Exception :" + ex.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);
            }
        }


        public bool ExecuteCommandWithResult(string Sql, SqlCommand cmdn)
        {
            try
            {
                cmdn.CommandType = CommandType.Text;
                cmdn.CommandText = Sql;
                int x = cmdn.ExecuteNonQuery();

                if (x > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "ExecuteCommand():MyClass";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "Sql :" + Sql + "\n Exception : " + ex.Message + " \n Inner Exception :" + ex.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);

                return false;
            }
        }


        public String ExecuteCommand(string Sql)
        {

            try
            {
                if (Cnn.ConnectionString != "") Cnn.Dispose();
                OpenCnn();
                Cmd.CommandText = "";
                Cmd = new SqlCommand(Sql, Cnn);
                Cmd.Connection.Open();
                Cmd.ExecuteNonQuery();
                Cmd.Dispose();
                return "";
            }
            catch (Exception ex)
            {
                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "public String ExecuteCommand(string Sql) : Exception";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "Sql :" + Sql + "\n Exception : " + ex.Message + " \n Inner Exception :" + ex.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);


                LogError(ex);
                Cmd.Dispose();
                CloseCnn();
                return ex.ToString();
            }
        }

        public bool ExecuteCommandB(string Sql)
        {

            try
            {
                if (Cnn.ConnectionString != "") Cnn.Dispose();
                OpenCnn();
                Cmd.CommandText = "";
                Cmd = new SqlCommand(Sql, Cnn);
                Cmd.Connection.Open();
                Cmd.ExecuteNonQuery();
                Cmd.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "public bool ExecuteCommandB(string Sql) : Exception";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "Sql :" + Sql + "\n Exception : " + ex.Message + " \n Inner Exception :" + ex.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);


                MyClass.SendErrorMail(ex.ToString());
                Cmd.Dispose();
                CloseCnn();
                return false;
            }
        }

        public Boolean OpenReturenDs(string Sql)
        {

            Sql = Sql.Replace("waitfor", "");
            Sql = Sql.Replace("delay", "");
            Sql = Sql.Replace("DELAY", "");

            try
            {
                if (Cnn.ConnectionString != "") Cnn.Dispose();
                OpenCnn();
                Da.Dispose();
                Ds = null;
                Ds = new DataSet();
                Da = new SqlDataAdapter(Sql, Cnn);
                Da.Fill(Ds);

                return true;
            }
            catch (Exception Err)
            {
                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "public Boolean OpenReturenDs(string Sql) : Exception";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "Sql :" + Sql + "  \n Exception : " + Err.Message + "  \n Inner Exception :" + Err.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);


                if (Cnn.ConnectionString != "") Cnn.Dispose();
                Da.Dispose();
                Err.ToString();
                return false;
            }
        }



        //***********************************************************************************
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strQuery"></param>
        /// <returns></returns>
        public string GetExecuteScalar(string strQuery)
        {
            string strret = "";
            strQuery = strQuery.Replace("waitfor", "");
            strQuery = strQuery.Replace("delay", "");
            strQuery = strQuery.Replace("DELAY", "");
            strQuery = strQuery.Replace("--", "");

            using (SqlConnection connection = new SqlConnection(clsSettings.strCoonectionString))
            {
                try
                {
                    SqlCommand cmd =
                        new SqlCommand(strQuery, connection);
                    connection.Open();
                    object strret1 = cmd.ExecuteScalar();
                    if (strret1 != null)
                    {
                        if (strret1.ToString().Length > 0)
                            strret = strret1.ToString();
                    }

                }
                catch (Exception ex)
                {

                    ErrorLogModel err = new ErrorLogModel();
                    err.ErrorTitle = "public string GetExecuteScalar(string strQuery) : Exception";
                    err.ProjectName = "POCRA WEB CLASS LIBRARY";
                    err.ErrorDescription = "Sql :" + strQuery + "\n Exception : " + ex.Message + " \n Inner Exception :" + ex.InnerException;
                    err.ErrorSeverity = (int)ErrorSeverity.High;
                    new ErrorLogManager().InsertErrorLog(err);

                    // MyClass.SendErrorMail(ex.ToString() + "    " + strQuery);
                }
                finally
                {
                    if (connection != null) { connection.Close(); }

                }
            }




            return strret;
        }


        public string GetExecuteScalar(string strQuery, SqlCommand cmd)
        {
            string strret = "";

            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                object strret1 = cmd.ExecuteScalar();
                if (strret1 != null)
                {
                    if (strret1.ToString().Length > 0)
                        strret = strret1.ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "public string GetExecuteScalar(string strQuery, SqlCommand cmd) : Exception";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "Sql :" + strQuery + "\nException : " + ex.Message + " \nInner Exception :" + ex.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);
            }

            return strret;
        }

        //*************************************************************************************
        public String GetSingleValueByProcedure(string Procedure, List<String> value)
        {
            String strret = "";
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.strCoonectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = Procedure;
                    connection.Open();
                    List<StoreProcedureParamList> parm = ListParms(Procedure);
                    int x = 0;
                    foreach (StoreProcedureParamList p in parm)
                    {
                        if (value[x].ToString().Trim().Length > 0)
                            command.Parameters.AddWithValue(p.ParmName, p.ParmDbType).Value = value[x].ToString();
                        else
                            command.Parameters.AddWithValue(p.ParmName, p.ParmDbType).Value = System.Data.SqlTypes.SqlString.Null;
                        x++;
                    }



                    object strret1 = command.ExecuteScalar();
                    if (strret1 != null)
                    {
                        if (strret1.ToString().Length > 0)
                            strret = strret1.ToString();
                    }
                    return strret;
                }
            }
            catch (Exception ex)
            {

                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "public String GetSingleValueByProcedure(string Procedure, List<String> value) : Exception";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "Sql :" + Procedure + "\nException : " + ex.Message + " \nInner Exception :" + ex.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);

                return strret;
            }

        }

        //*********************************************************************************
        public Boolean CloseCnn()
        {
            try
            {

                Cmd.Dispose();
                Ds.Dispose();
                Da.Dispose();
                Cnn.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "public Boolean CloseCnn() : Exception";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "Exception : " + ex.Message + " \nInner Exception :" + ex.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);


                ex.ToString();
                return false;
            }
        }



        public DataTable GetProcedure(string Procedure)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.strCoonectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = Procedure;
                    connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "public DataTable GetProcedure(string Procedure) : Exception";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "Query :" + Procedure + "Exception : " + ex.Message + " \nInner Exception :" + ex.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);
                //MyClass.SendErrorMail(ex.ToString());
                return dt;
            }

        }



        // By ashutosh ///----------------------------------------


        public class StoreProcedureParamList
        {
            private string _ParmName = "";
            public string ParmName
            {
                get { return _ParmName; }
                set { _ParmName = value; }
            }
            private DbType _ParmDbType;
            public DbType ParmDbType
            {
                get { return _ParmDbType; }
                set { _ParmDbType = value; }
            }

        }

        private List<StoreProcedureParamList> ListParms(string procname)
        {
            List<StoreProcedureParamList> parm = new List<StoreProcedureParamList>();
            try
            {
                using (SqlConnection conn = new SqlConnection(clsSettings.strCoonectionString))
                {
                    SqlCommand cmd = new SqlCommand(procname, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    SqlCommandBuilder.DeriveParameters(cmd);


                    foreach (SqlParameter p in cmd.Parameters)
                    {
                        if (p.ParameterName.ToUpper() != "@RETURN_VALUE")
                        {
                            StoreProcedureParamList obj = new StoreProcedureParamList();
                            obj.ParmName = p.ParameterName;
                            obj.ParmDbType = p.DbType;
                            parm.Add(obj);
                        }
                    }
                    conn.Close();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "private List<StoreProcedureParamList> ListParms(string procname) : Exception";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "Query :" + procname + "Exception : " + ex.Message + " \nInner Exception :" + ex.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);
            }

            return parm;
        }

        /// <summary>
        /// GetDtByProcedure
        /// </summary>
        /// <param name="Procedure"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTable GetDtByProcedure(string Procedure, List<String> value)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.strCoonectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = Procedure;
                    command.CommandTimeout = 3600;
                    connection.Open();
                    List<StoreProcedureParamList> parm = ListParms(Procedure);
                    int x = 0;
                    foreach (StoreProcedureParamList p in parm)
                    {
                        if (value[x].ToString().Trim().Length > 0)
                            command.Parameters.AddWithValue(p.ParmName, p.ParmDbType).Value = value[x].ToString();
                        else
                            command.Parameters.AddWithValue(p.ParmName, p.ParmDbType).Value = System.Data.SqlTypes.SqlString.Null;
                        x++;
                    }




                    SqlDataAdapter da = new SqlDataAdapter(command);

                    da.Fill(dt);
                    if (connection != null) { connection.Close(); }
                    command.Dispose();
                    value.Clear();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "public DataTable GetDtByProcedure(string Procedure, List<String> value) : Exception";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "Query :" + Procedure + " \nException : " + ex.Message + " \nInner Exception :" + ex.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);


                MyClass.SendErrorMail(ex.ToString() + "   " + Procedure);
                return dt;
            }

        }



        /// <summary>
        /// GetDtByProcedure
        /// </summary>
        /// <param name="Procedure"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataTable GetDtByProcedureForReport(string Procedure, List<String> value)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.strCoonectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = Procedure;
                    command.CommandTimeout = 0;
                    connection.Open();
                    List<StoreProcedureParamList> parm = ListParms(Procedure);
                    int x = 0;
                    foreach (StoreProcedureParamList p in parm)
                    {
                        if (value[x].ToString().Trim().Length > 0)
                            command.Parameters.AddWithValue(p.ParmName, p.ParmDbType).Value = value[x].ToString();
                        else
                            command.Parameters.AddWithValue(p.ParmName, p.ParmDbType).Value = System.Data.SqlTypes.SqlString.Null;
                        x++;
                    }




                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(dt);
                    if (connection != null) { connection.Close(); }
                    command.Dispose();
                    value.Clear();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                string spParams = String.Join(",", value);

                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "public DataTable GetDtByProcedureForReport(string Procedure, List<String> value) : Exception";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "Query :" + Procedure + " ,Parameters : (" + spParams + ") , Exception : " + ex.Message + " \nInner Exception :" + ex.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);

                MyClass.SendErrorMail(ex.ToString() + "   " + Procedure);
                return dt;
            }

        }

        /// <summary>
        /// GetDtByProcedure
        /// </summary>
        /// <param name="Procedure"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public DataSet GetDataSetByProcedure(string Procedure, List<String> value)
        {
            DataSet dt = new DataSet();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.strCoonectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = Procedure;
                    command.CommandTimeout = 3600;
                    connection.Open();
                    List<StoreProcedureParamList> parm = ListParms(Procedure);
                    int x = 0;
                    foreach (StoreProcedureParamList p in parm)
                    {
                        if (value[x].ToString().Trim().Length > 0)
                            command.Parameters.AddWithValue(p.ParmName, p.ParmDbType).Value = value[x].ToString();
                        else
                            command.Parameters.AddWithValue(p.ParmName, p.ParmDbType).Value = System.Data.SqlTypes.SqlString.Null;
                        x++;
                    }




                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(dt);
                    connection.Close();
                    command.Dispose();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "public DataSet GetDataSetByProcedure(string Procedure, List<String> value) : Exception";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "Query :" + Procedure + "Exception : " + ex.Message + " \nInner Exception :" + ex.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);

                return dt;
            }

        }


        public DataSet GetDataSet(string Query)
        {
            DataSet dt = new DataSet();
            try
            {
                if (Cnn.ConnectionString != "") Cnn.Dispose();
                OpenCnn();
                Da.Dispose();
                Ds = null;
                Ds = new DataSet();
                Da = new SqlDataAdapter(Query, Cnn);
                Da.Fill(Ds);
                dt = Ds;
                return dt;

            }
            catch (Exception ex)
            {
                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "public DataSet GetDataSet(string Query) : Exception";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "Query :" + Query + "Exception : " + ex.Message + " \nInner Exception :" + ex.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);

                return dt;
            }

        }

        /// <summary>
        /// ExecuteByProcedure with self command
        /// </summary>
        /// <param name="Procedure"></param>
        /// <param name="value"></param>
        public bool ExecuteByProcedure(string Procedure, List<String> value)
        {
            bool ret = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.strCoonectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = Procedure;
                    command.CommandTimeout = 3600;
                    connection.Open();
                    List<StoreProcedureParamList> parm = ListParms(Procedure);
                    int x = 0;
                    foreach (StoreProcedureParamList p in parm)
                    {
                        if (value[x].ToString().Trim().Length > 0)
                            command.Parameters.AddWithValue(p.ParmName, p.ParmDbType).Value = value[x].ToString();
                        else
                            command.Parameters.AddWithValue(p.ParmName, p.ParmDbType).Value = System.Data.SqlTypes.SqlString.Null;
                        x++;
                    }
                    int d = command.ExecuteNonQuery();
                    if (d != 0)
                    {
                        ret = true;
                    }
                    connection.Close();
                    command.Dispose();
                }
            }
            catch (Exception ex)
            {

                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "public bool ExecuteByProcedure(string Procedure, List<String> value) : Exception";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "Query :" + Procedure + "\n Exception : " + ex.Message + " \nInner Exception :" + ex.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);
            }

            return ret;
        }

        /// <summary>
        /// ExecuteByProcedure
        /// </summary>
        /// <param name="Procedure"></param>
        /// <param name="value"></param>
        /// <param name="command"></param>
        public void ExecuteByProcedure(string Procedure, List<String> value, SqlCommand command)
        {
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = Procedure;
                command.CommandTimeout = 3600;
                List<StoreProcedureParamList> parm = ListParms(Procedure);
                int x = 0;
                foreach (StoreProcedureParamList p in parm)
                {
                    if (value[x].ToString().Trim().Length > 0)
                        command.Parameters.AddWithValue(p.ParmName, p.ParmDbType).Value = value[x].ToString();
                    else
                        command.Parameters.AddWithValue(p.ParmName, p.ParmDbType).Value = System.Data.SqlTypes.SqlString.Null;
                    x++;
                }
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                value.Clear();

            }
            catch (Exception ex)
            {
                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "public bool ExecuteByProcedure(string Procedure, List<String> value) : Exception";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "Query :" + Procedure + "Exception : " + ex.Message + " \nInner Exception :" + ex.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);
            }

        }

        // end ----------------------------------------



        //#################################### To Send Mail Message..................................... 


        public string SvrDatePrevious(string DateMDY)
        {
            MyClass scnn = new MyClass();
            string dt = "";
            try
            {
                string Svrdt;
                scnn.OpenCnn();
                Svrdt = Convert.ToDateTime(scnn.GetExecuteScalar("SELECT     (CONVERT(DATETIME,'" + DateMDY + "')-1 ) AS dt")).ToShortDateString().Trim();
                dt = Convert.ToDateTime(Svrdt).ToString("dd/MM/yyyy");
            }
            catch (Exception ex)
            {

                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "public string SvrDatePrevious(string DateMDY) : Exception";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "DateMDY: " + DateMDY + "Exception: " + ex.Message + " \nInner Exception :" + ex.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);

            }


            return dt;
        }

        public string mdy(string strdt)
        {
            if (strdt.Trim().Length > 0)
            {
                string[] dt; string ndt;
                if (strdt.Contains("/"))
                {
                    dt = strdt.Trim().Split('/');
                    ndt = dt[1] + "/" + dt[0] + "/" + dt[2];

                }
                else
                {
                    dt = strdt.Trim().Split('-');
                    ndt = dt[1] + "/" + dt[0] + "/" + dt[2];
                }

                string stt = ndt;
                return stt;
            }
            else
            {
                return "";
            }
        }
        ///////////////
        public string dmy(string strdt)
        {
            if (strdt == "") return null;
            DateTime d;
            string[] dt; string ndt;
            if (strdt.Contains("/"))
            {
                dt = strdt.Trim().Split('/');
                ndt = dt[0] + "/" + dt[1] + "/" + dt[2];
            }
            else
            {
                dt = strdt.Trim().Split('-');
                ndt = dt[0] + "/" + dt[1] + "/" + dt[2];
            }
            try
            {
                d = DateTime.Parse(ndt);
            }
            catch
            {
                ndt = dt[0] + "/" + dt[1] + "/" + dt[2];
                d = DateTime.Parse(ndt);
            }
            string stt = d.ToString("dd/MM/yyyy");
            return stt;
        }
        public string ymd(string strdt)
        {
            if (strdt == "") return null;
            string[] dt; string ndt;
            if (strdt.Contains("/"))
            {
                dt = strdt.Trim().Split('/');
                // ndt = dt[0] + "/" + dt[1] + "/" + dt[2];
            }
            else
            {
                dt = strdt.Trim().Split('-');
                //ndt = dt[0] + "/" + dt[1] + "/" + dt[2];
            }

            ndt = dt[2] + "/" + dt[1] + "/" + dt[0];

            return ndt.Trim();
        }

        public string ymd2(string strdt)
        {
            if (strdt == "") return null;
            string[] dt; string ndt;
            if (strdt.Contains("/"))
            {
                dt = strdt.Trim().Split('/');
                // ndt = dt[0] + "/" + dt[1] + "/" + dt[2];
            }
            else
            {
                dt = strdt.Trim().Split('-');
                //ndt = dt[0] + "/" + dt[1] + "/" + dt[2];
            }

            ndt = dt[2] + "-" + dt[1] + "-" + dt[0];

            return ndt.Trim();
        }

        public int TableID(string TableName, string FieldName, SqlCommand cmd)
        {

            int TableID = 0;
            string query = "SELECT  MAX(" + FieldName + ") AS ID FROM  " + TableName + " HAVING  MAX(" + FieldName + ") IS NOT NULL";
            try
            {
                DataTable dt = GetDataTable(query, cmd);
                if (dt.Rows.Count != 0)
                {
                    TableID = Convert.ToInt32(dt.Rows[0]["ID"].ToString()) + 1;
                }
                else
                    TableID = 1;
            }
            catch (Exception ex)
            {
                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "public int TableID(string TableName, string FieldName, SqlCommand cmd) : Exception";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "Query :" + query + "Exception : " + ex.Message + " \nInner Exception :" + ex.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);
            }

            return TableID;
        }
        public int TableID(string TableName, string FieldName)
        {
            int TableID = 0;
            string query = "SELECT  MAX(" + FieldName + ") AS ID FROM  " + TableName + " HAVING  MAX(" + FieldName + ") IS NOT NULL";


            try
            {
                MyClass scnn = new MyClass();

                scnn.OpenCnn();
                String str = scnn.GetExecuteScalar(query);
                if (str.Length > 0)
                {
                    TableID = Convert.ToInt32(str) + 1;
                }
                else
                    TableID = 1;
            }
            catch (Exception ex)
            {
                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "public int TableID(string TableName, string FieldName, SqlCommand cmd) : Exception";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "Query :" + query + "Exception : " + ex.Message + " \nInner Exception :" + ex.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);
            }

            return TableID;
        }

        public DataTable GetDataTable(string strQuery)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettings.strCoonectionString))
                {
                    SqlCommand command =
                        new SqlCommand(strQuery, connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "public DataTable GetDataTable(string strQuery) : Exception";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "Query :" + strQuery + "Exception : " + ex.Message + " \nInner Exception :" + ex.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);

                MyClass.SendErrorMail(ex.ToString() + "  " + strQuery);

            }
            return dt;
        }



        public DataTable GetDataTable(string strQuery, SqlCommand cmd)
        {
            DataTable dt = new DataTable();

            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "public DataTable GetDataTable(string strQuery, SqlCommand cmd) : Exception";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "Query :" + strQuery + "Exception : " + ex.Message + " \nInner Exception :" + ex.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);
            }

            return dt;

        }
        public string SvrDate()
        {
            string Svrdt;
            Svrdt = GetExecuteScalar("SELECT   CONVERT(VARCHAR(20) ,GETDATE(),103) AS dt");
            return Svrdt;

        }

        public int SvrCurrentMonth()
        {
            int Svrdt;
            Svrdt = Convert.ToInt32(GetExecuteScalar("SELECT MONTH(GETDATE()) AS dt"));
            return Svrdt;

        }

        public string GetSqlUnikNO(String Degits)
        {
            string Svrdt;
            Svrdt = GetExecuteScalar("select CONVERT(varchar(10), right(newid()," + Degits + "))");
            return Svrdt.Trim();
        }


        public bool checkExprationValidation(String value, string regex)
        {
            bool ret = true;
            var match = Regex.Match(value, regex, RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                // does not match
                ret = false;
            }
            return ret;

        }

        public static string PutIntoQuotes(string value)
        {
            return "\"" + value + "\"";
        }

        public static string SendErrorMail(string MailDetails)
        {
            try
            {

                //string MailTo = "ashutosh@orias.in"; string MailSub = "Error in API";
                //System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                //mail.To.Add(MailTo);
                //mail.From = new MailAddress("info@orias.co.in", " Error ");
                //mail.Subject = MailSub;
                //string Body = " <HTML> <HEAD> </HEAD> <BODY> <center>  <table cellpadding='8'  cellspacing='1' border='0' width='751px' style='font-family: verdana; font-size: 10pt;border: 1px solid #808080;'><tr valign='top' bgcolor='#1374c9'> <td style='color: #FFFFFF; font-weight: bold'> Business Technology Innovation </td> </tr>       <tr>   <td> <br>  " + MailDetails + " </td></tr> </table> </center>  </BODY> </HTML>  ";
                //mail.Body = Body;
                //mail.IsBodyHtml = true;
                //SmtpClient smtp = new SmtpClient();
                //smtp.Host = "mail.orias.co.in";
                //smtp.Port = 25;
                //smtp.UseDefaultCredentials = true;
                //smtp.Credentials = new System.Net.NetworkCredential("info@orias.co.in", "Ashu@751985#");
                //smtp.Send(mail);

                return "Email Send Sucessfully";

            }
            catch
            {
                return "";

            }
        }


        public String ExecuteMultiQueryWithCommand(List<String> lst)
        {
            String ret = "";

            MyClass cla = new MyClass();
            using (SqlConnection connection = new SqlConnection(clsSettings.strCoonectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction("CTransaction");
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {


                    foreach (String str in lst)
                    {
                        cla.ExecuteCommand(str, command);
                    }





                    transaction.Commit();
                    ret = "";
                    lst.Clear();

                }
                catch (Exception ex)
                {
                    //String error = "Error in Add Journey Save button Click " + ex.ToString();
                    //WriteError(error, Session["UserEmailID"].ToString());
                    try
                    {
                        transaction.Rollback();

                    }
                    catch
                    {

                    }
                    ret = ex.Message.Trim();
                }
                finally
                {
                    connection.Close();
                    command.Dispose();
                }

            }

            return ret;
        }
        #region change data from row to column
        public DataTable FlipDataTable(DataTable dt)
        {
            DataTable table = new DataTable();
            //Get all the rows and change into columns
            for (int i = 0; i <= dt.Rows.Count; i++)
            {
                table.Columns.Add(Convert.ToString(i));
            }
            DataRow dr;
            //get all the columns and make it as rows
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                dr = table.NewRow();
                dr[0] = dt.Columns[j].ToString();
                for (int k = 1; k <= dt.Rows.Count; k++)
                    dr[k] = dt.Rows[k - 1][j];
                table.Rows.Add(dr);
            }

            return table;
        }
        #endregion

        public static void LogError(Exception ex)
        {
            try
            {

                string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                message += string.Format("Message: {0}", ex.Message);
                message += Environment.NewLine;
                message += string.Format("StackTrace: {0}", ex.StackTrace);
                message += Environment.NewLine;
                message += string.Format("Source: {0}", ex.Source);
                message += Environment.NewLine;
                message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
                message += Environment.NewLine;
                message += string.Format("Exception: {0}", ex.ToString());
                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                try
                {
                    if (HttpContext.Current.Session["UserId"] != null)
                    {
                        message += Environment.NewLine;
                        message += string.Format("UserID: {0}", HttpContext.Current.Session["UserId"].ToString());
                        message += Environment.NewLine;
                    }
                    else
                    {
                        message += Environment.NewLine;
                        message += string.Format("UserID: {0}", "Not Logged In");
                        message += Environment.NewLine;
                    }
                }
                catch { }

                message += "-----------------------------------------------------------";
                message += Environment.NewLine;

                try
                {
                    string ipaddress;
                    ipaddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (ipaddress == "" || ipaddress == null)
                        ipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];


                    message += string.Format("IPAddress: {0}", ipaddress.ToString());
                    message += Environment.NewLine;
                    message += "-----------------------------------------------------------";
                    message += Environment.NewLine;
                }
                catch
                {

                }


                String PathUp = "~/DocMasters/MyClassErroLog";
                String path = System.Web.HttpContext.Current.Server.MapPath(PathUp);
                if (!Directory.Exists(path))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }
                path = path + "/ErroLog" + DateTime.Now.ToString("MM/dd/yyyy").Replace("/", "") + ".txt";

                FileInfo fi = new FileInfo(path);
                if (fi.Exists)
                {
                    string seperator = "-------------------------------------------\n\r";
                    File.AppendAllText(path,
                        seperator + DateTime.Now.ToString() + "\n\r" + message + "\n\r" + seperator);

                }
                else
                {
                    using (StreamWriter sw = fi.CreateText())
                    {
                        sw.WriteLine(message);
                    }
                }

            }
            catch { }

        }
    }
}
