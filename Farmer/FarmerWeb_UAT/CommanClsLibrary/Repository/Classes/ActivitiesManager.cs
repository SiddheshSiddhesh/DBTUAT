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
    public class ActivitiesManager : IActivitiesManager
    {

        public DataSet GetRemainingB2BNotification()
        {
            DataSet ds = new DataSet();
            string CommandString = "Usp_RemainingB2BNotification";
            try
            {
                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString(), CommandType.StoredProcedure, CommandString);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }


        public bool UpdateB2BNotificationSendStatus(int ApplicationID)
        {
            bool result = false;
            string spName = "Usp_UpdateB2BNotificationStatus";
            int RowsAffected = 0;
            SqlParameter[] parameter = new SqlParameter[10];

            parameter[0] = new SqlParameter("@ApplicationID", ApplicationID);
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

        public DataSet GetPresanctionCancellationListByFarmerID(string FarmerRegistrationID)
        {
            DataSet ds = new DataSet();
            string CommandString = "Usp_PresanctionCancellationListByFarmerRegisterID";
            try
            {
                SqlParameter[] parameter = new SqlParameter[10];
                parameter[0] = new SqlParameter("@FarmerRegistrationID", FarmerRegistrationID);
                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString(), CommandType.StoredProcedure, CommandString, parameter);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public DataSet GetApplicationDetailsByID(string ApplicationID)
        {
            DataSet ds = new DataSet();
            string CommandString = "Usp_GetApplicationDetailByID_Farmers";
            try
            {
                SqlParameter[] parameter = new SqlParameter[10];
                parameter[0] = new SqlParameter("@ApplicationID", ApplicationID);

                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString(), CommandType.StoredProcedure, CommandString, parameter);
            }
            catch (Exception)
            {
                throw;
            }

            return ds;
        }

        public bool SendSMSForAddingPrevocationDate(string MobileNumber, string ApplicationCode, string ActivityGroupName, string ActivityGroupNameMr, string PresanctionLimitDate)
        {
            string SMSTemplate = "आपल्या @ApplicationGrpName व @ApplicationCode या मंजूर घटकाची पूर्वसंमती आपल्या विनंतीनुसार दि. @PresanctionLimitDate रोजी पुनर्जीवित करण्यात आली असून ७ दिवसांचे आत अनुदान मागणी अद्ययावत करावी अन्यथा सदरची पूर्वसंमती रद्द करणेत येईल PoCRA";
            SMSTemplate = SMSTemplate.Replace("@ApplicationGrpName", ActivityGroupNameMr);
            SMSTemplate = SMSTemplate.Replace("@ApplicationCode", ApplicationCode);
            SMSTemplate = SMSTemplate.Replace("@PresanctionLimitDate", PresanctionLimitDate);

            int result = SMS.SendSMS(SMSTemplate, MobileNumber, "1407169709017274764");

            return result > 0 ? true : false;
        }

        public bool UpdateRevokePresanctionCancellation(int ApplicationID, DateTime PresanctionRevokeDate, int PresanctionRevokeBy)
        {
            bool result = false;
            string spName = "Usp_UpdateRevokePresanctionCancellation";
            int RowsAffected = 0;
            SqlParameter[] parameter = new SqlParameter[10];

            parameter[0] = new SqlParameter("@ApplicationID", ApplicationID);
            parameter[1] = new SqlParameter("@PresanctionRevokeDate", PresanctionRevokeDate);
            parameter[2] = new SqlParameter("@PresanctionRevokeBy", PresanctionRevokeBy);
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

        public bool SendSMSForRemovingPresanction_ForAddedInvocation(string MobileNumber, string ApplicationCode, string ActivityGroupName, string ActivityGroupNameMr, string PresanctionCancellationDate)
        {
            string SMSTemplate = "विहित मुदतीत @ApplicationGrpName व @ApplicationCode घटकाची अनुदान मागणी अद्ययावत न केलेने सदरची पूर्वसंमती दि. @PresanctionCancellationDate रोजी रद्द करण्यात आलेली आहे. PoCRA";
            SMSTemplate = SMSTemplate.Replace("@ApplicationGrpName", ActivityGroupNameMr);
            SMSTemplate = SMSTemplate.Replace("@ApplicationCode", ApplicationCode);
            SMSTemplate = SMSTemplate.Replace("@PresanctionCancellationDate", PresanctionCancellationDate);

            int result = SMS.SendSMS(SMSTemplate, MobileNumber, "1407169709034170858");

            return result > 0 ? true : false;
        }

        public DataSet GetListOfPresanctionInvocationApplicationsToCancel()
        {
            DataSet ds = new DataSet();
            string CommandString = "Usp_GetListOfPresanctionInvocationApplicationsToCancel";
            try
            {
                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString(), CommandType.StoredProcedure, CommandString);
            }
            catch (Exception)
            {
                throw;
            }

            return ds;
        }

        public bool UpdatePresanctionInvocation_SetToRejectedAfterInvocation(int ApplicationID)
        {
            bool result = false;
            string spName = "Usp_UpdatePresanctionInvocation_SetToRejectedAfterInvocation";
            int RowsAffected = 0;
            SqlParameter[] parameter = new SqlParameter[10];

            parameter[0] = new SqlParameter("@ApplicationID", ApplicationID);
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


    }
}
