using CommanClsLibrary.Repository.Classes.StaticClasses;
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
using static CommanClsLibrary.Repository.Enums;

namespace CommanClsLibrary.Repository.Classes
{
    public class FPO : IFPO
    {

        public string GetFPORegistrationID(string RegistrationNo)
        {
            string FPORegistrationID = "";

            string CommandString = "FPO_GetFPORegistrationID";
            try
            {
                DataSet ds = new DataSet();

                SqlParameter[] parameter = new SqlParameter[10];
                parameter[0] = new SqlParameter("@FPORegistrationNo", RegistrationNo.Sanitize());

                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString(), CommandType.StoredProcedure, CommandString, parameter);
                if (CommonUtility.IsValidDataset(ds))
                {
                    if (CommonUtility.IsValidDataTable(ds.Tables[0]))
                    {
                        FPORegistrationID = ds.Tables[0].Rows[0]["FPORegistrationID"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "GetFPORegistrationID(string RegistrationNo) : Web";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "Sql :" + CommandString + " \n RegistrationNo :" + RegistrationNo + "\n Exception : " + ex.Message + " \n Inner Exception :" + ex.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);
            }

            return FPORegistrationID;
        }

        public string CheckFPORegistrationID(string RegistrationNo)
        {
            string FPORegistrationID = "";

            string CommandString = "FPO_CheckFPORegistrationID";
            try
            {
                DataSet ds = new DataSet();

                SqlParameter[] parameter = new SqlParameter[10];
                parameter[0] = new SqlParameter("@FPORegistrationNo", RegistrationNo.Sanitize());

                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString(), CommandType.StoredProcedure, CommandString, parameter);
                if (CommonUtility.IsValidDataset(ds))
                {
                    if (CommonUtility.IsValidDataTable(ds.Tables[0]))
                    {
                        FPORegistrationID = ds.Tables[0].Rows[0]["FPORegistrationID"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "CheckFPORegistrationID(string RegistrationNo) : Web";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "Sql :" + CommandString + " \n RegistrationNo :" + RegistrationNo + "\n Exception : " + ex.Message + " \n Inner Exception :" + ex.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);
            }

            return FPORegistrationID;
        }


        public bool IsFPOApplicationPaid(string ApplicationID)
        {
            bool IsPaid = false;

            string CommandString = "select * from FPO_A_FPOApplication_PaymentRequest where FPOApplicationID=" + ApplicationID + " and IsPaid=1";
            try
            {
                DataSet ds = new DataSet();



                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString(), CommandType.Text, CommandString);
                if (CommonUtility.IsValidDataset(ds))
                {
                    if (CommonUtility.IsValidDataTable(ds.Tables[0]))
                    {
                        if (ds.Tables.Count > 0)
                        {
                            IsPaid = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "IsFPOApplicationPaid(string ApplicationID) : Web";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "Sql :" + CommandString + " \n ApplicationID :" + ApplicationID + "\n Exception : " + ex.Message + " \n Inner Exception :" + ex.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);
            }

            return IsPaid;
        }


        public DataSet GetAllFPODeskCountData()
        {
            DataSet ds = new DataSet();

            string query = "Usp_FPODeskWiseApplicationSummary_Updated";//Usp_FPODeskWiseApplicationSummary
            ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString(), CommandType.StoredProcedure, query);

            return ds;
        }



        public DataSet GetAllFPODDeskData()
        {
            DataSet ds = new DataSet();

            //string query = @"select FPOApplicationID,Desk,RegisterName,ApplicationCode,ActivityCodes,Liability from Vw_FPODeskWiseApplicationDetails_Updated order by OrderNo asc";

            string query = @"select AP.OrderNo,AP.FPOApplicationID,Desk,RegisterName,ApplicationCode,ActivityCodes,Liability ,
CASE WHEN AP.OrderNo=8 THEN CONVERT(nvarchar(20), R.UpdateOnDate, 103) END AS 'Back to Stage Date'
--,R.FPOPaymentRequestLogID
from Vw_FPODeskWiseApplicationDetails_Updated as AP
left join FPO_A_FPOApplication_PaymentRequest PayR on PayR.FpoApplicationID=AP.FPOApplicationID
left join FPO_A_FPOApplication_PaymentRequest_Log AS R on 
R.FPOPaymentRequestLogID=
(select top 1 LG.FPOPaymentRequestLogID from FPO_A_FPOApplication_PaymentRequest_Log LG where ApprovalStageID=62 and  IsBackToPrevious=1
and  LG.PaymentRequestID=PayR.PaymentRequestID order by FPOPaymentRequestLogID desc)
where 
--AP.OrderNo>7 and 
PayR.IsDeleted is null
order by AP.OrderNo asc";

            ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString(), CommandType.Text, query);

            return ds;
        }
    }
}
