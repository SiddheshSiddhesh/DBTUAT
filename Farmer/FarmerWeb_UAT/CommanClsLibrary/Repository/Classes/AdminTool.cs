using CommanClsLibrary.Repository.Interfaces;
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
  public  class AdminTool : IAdminTool
    {
        public DataTable GetUserLastUpdatedPasswordDetails(string UserID)
        {
            DataSet ds = new DataSet();
            string CommandString = "SELECT UserId,ISNULL(LastPasswordUpdatedOn,GETDATE()-91) LastPasswordUpdatedOn,DATEDIFF(DAY, ISNULL(LastPasswordUpdatedOn, GETDATE() - 91), GETDATE()) as LastPasswordUpdatePeriod FROM Tbl_M_LoginDetails  WHERE(UserId = " + UserID + ")";
            try
            {
                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString(), CommandType.Text, CommandString);
            }
            catch (Exception)
            {
                throw;
            }
            return ds.Tables[0];
        }

        public bool UpdateOfficialLoginPassword(string Username, string UserID, string IP, string Password, string Source)
        {
            bool result = false;
            string spName = "Usp_ChangePwdLogWeb";
            int RowsAffected = 0;
            SqlParameter[] parameter = new SqlParameter[10];

            parameter[0] = new SqlParameter("@Username", Username);
            parameter[1] = new SqlParameter("@UserID", UserID);
            parameter[2] = new SqlParameter("@IP", IP);
            parameter[3] = new SqlParameter("@Password", Password);
            parameter[4] = new SqlParameter("@Source", Source);
            try
            {
                RowsAffected = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString(), CommandType.StoredProcedure, spName, parameter);
                result = RowsAffected > 0 ? true : false;
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        public DataTable GetOfficialUserInfoByUsername(string Username)
        {
            DataSet ds = new DataSet();
            string CommandString = "select UserId,Mobile,FullName From Tbl_M_LoginDetails where UserName = '" + Username + "' and ISNULL(IsDeleted,0)= 0";
            try
            {
                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString(), CommandType.Text, CommandString);
            }
            catch (Exception)
            {
                throw;
            }
            return ds.Tables[0];
        }

    }
}
