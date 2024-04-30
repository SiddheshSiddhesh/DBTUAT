using CommanClsLibrary.Repository.Classes;
using CommanClsLibrary.Repository.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static CommanClsLibrary.Repository.Enums;

namespace DBTPoCRA.CronJob
{
    public partial class PresanctionCancellationAndSendSMSforInvocation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            AddErrorLog("Cron Started : PresanctionCancellationAndSendSMSforInvocation.aspx");

            ActivitiesManager act = new ActivitiesManager();

            string MobileNumber = ""
            , ApplicationCode = ""
            , ActivityGroupName = ""
            , ActivityGroupNameMr = ""
            , PresanctionCancellationDate = "";
            int ApplicationID = 0;
            DataSet ds = act.GetListOfPresanctionInvocationApplicationsToCancel();
            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    MobileNumber = dr["MobileNumber"].ToString();
                    ApplicationCode = dr["ApplicationCode"].ToString();
                    ActivityGroupName = dr["ActivityGroupName"].ToString();
                    ActivityGroupNameMr = dr["ActivityGroupNameMr"].ToString();
                    //PresanctionCancellationDate = dr["AddedDateForPresanctionCancellation"].ToString();
                    PresanctionCancellationDate = DateTime.Now.ToString("dd/MM/yyyy");

                    ApplicationID = Convert.ToInt32(dr["ApplicationID"].ToString());

                    // MobileNumber = "9421007763";
                    bool SMSStatus = act.SendSMSForRemovingPresanction_ForAddedInvocation(MobileNumber, ApplicationCode, ActivityGroupName, ActivityGroupNameMr, PresanctionCancellationDate);
                    if (SMSStatus == true)
                    {
                        act.UpdatePresanctionInvocation_SetToRejectedAfterInvocation(ApplicationID);
                    }
                }
            }
             
            AddErrorLog("Cron Ended : PresanctionCancellationAndSendSMSforInvocation.aspx");
        }

        void AddErrorLog(string Message)
        {
            ErrorLogModel err = new ErrorLogModel();
            err.ErrorTitle = "Cron Job";
            err.ProjectName = "POCRA WEB";
            err.ErrorDescription = Message;
            err.ErrorSeverity = (int)ErrorSeverity.Information;
            new ErrorLogManager().InsertErrorLog(err);
        }
    }
}