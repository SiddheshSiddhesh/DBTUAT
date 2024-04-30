using CommanClsLibrary.Repository.Interfaces;
using CommanClsLibrary.Repository.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CommanClsLibrary.Repository.Enums;

namespace CommanClsLibrary.Repository.Classes
{
    public class Disubursement : IDisubursement
    {
        public DataSet GetPaymentSourceList()
        {
            DataSet ds = new DataSet();
            string CommandString = "Usp_GetPaymentSourceList";
            try
            {
                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString(), CommandType.StoredProcedure, CommandString);
            }
            catch (Exception ex)
            {
                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "GetPaymentSourceList():Disubursement Exception";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "Sql :" + CommandString + "\n Exception : " + ex.Message + " \n Inner Exception :" + ex.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);

            }
            return ds;
        }
    }
}
