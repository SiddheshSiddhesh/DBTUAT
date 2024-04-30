using CommanClsLibrary.Repository.Interfaces;
using CommanClsLibrary.Repository.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanClsLibrary.Repository.Classes
{
    public class ErrorLogManager : IErrorLogManager
    {
        public bool InsertErrorLog(ErrorLogModel error)
        {

            string spName = "SP_INSERT_ERRORLOG";
            int RowsAffected = 0;
            SqlParameter[] parameter = new SqlParameter[10];

            parameter[0] = new SqlParameter("@ErrorDescription", error.ErrorDescription);
            parameter[1] = new SqlParameter("@ErrorSeverity", error.ErrorSeverity);
            parameter[2] = new SqlParameter("@ProjectName", error.ProjectName);
            parameter[3] = new SqlParameter("@ErrorTitle", error.ErrorTitle);

            try
            {
                RowsAffected = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString(), CommandType.StoredProcedure, spName, parameter);
            }
            catch (Exception ex)
            {

            }

            return true;
        }
    }
}
