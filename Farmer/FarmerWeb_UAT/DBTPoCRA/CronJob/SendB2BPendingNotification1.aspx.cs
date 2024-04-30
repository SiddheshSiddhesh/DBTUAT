using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CommanClsLibrary;
using CommanClsLibrary.Repository.Classes;
using CommanClsLibrary.Repository.Models;
using static CommanClsLibrary.Repository.Enums;

namespace DBTPoCRA.CronJob
{
    public partial class SendB2BPendingNotification1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AddErrorLog("Cron Started : SendB2BPendingNotification1.aspx");

            ActivitiesManager act = new ActivitiesManager();
            DataSet ds = act.GetRemainingB2BNotification();

            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {

                    string ApplicationID = "";
                    string ApplicationCode = "";
                    string ActivityGroupNameMr = "";
                    string MobileNo = "";
                    string SMSContent = "";

                    string entryLog = "Ok";
                    try
                    {
                        ApplicationID = dr["ApplicationID"].ToString();
                        ApplicationCode = dr["ApplicationCode"].ToString();
                        ActivityGroupNameMr = dr["ActivityGroupNameMr"].ToString();
                        MobileNo = dr["MobileNumber"].ToString();


                        //SMSContent = "आपल्या " + ActivityGroupNameMr + " व " + ApplicationCode + " या मंजूर घटकास Back to stage करून १ वर्षापेक्षा जास्त कालावधी झालेने ७ दिवसांचे आत अनुदान मागणी अद्ययावत न केलेस सदरची पूर्वसंमती रद्द करणेत येईल. PoCRA";

                        SMSContent = "आपल्या " + ActivityGroupNameMr + " व " + ApplicationCode + " या मंजूर घटकास Back to stage करून २ महिन्यापेक्षा जास्त कालावधी झालेने ७ दिवसांचे आत अनुदान मागणी अद्ययावत न केलेस सदरची पूर्वसंमती रद्द करणेत येईल. PoCRA";

                        //MobileNo = "9421007763";
                        int result = SMS.SendSMS(SMSContent, MobileNo, "1407170201522590090");


                        UpdateLog(ApplicationID); 
                    }
                    catch (Exception ex)
                    {
                        entryLog = ex.Message;
                    }
                    finally
                    {
                        #region Page Log                     
                        Response.Write("<div style='border-style:solid;border-width:1px;margin-bottom:10px;margin-bottom:5px;padding:5px;'>");
                        Response.Write("ApplicationID :" + ApplicationID);
                        Response.Write("<br>");
                        Response.Write("ApplicationCode :" + ApplicationCode);
                        Response.Write("<br>");
                        Response.Write("ActivityGroupNameMr :" + ActivityGroupNameMr);
                        Response.Write("<br>");
                        Response.Write("MobileNo :" + MobileNo);
                        Response.Write("<br>");
                        Response.Write("SMSContent :" + SMSContent);


                        Response.Write("<br>");
                        Response.Write("Log :" + entryLog);

                        Response.Write("</div>");
                        #endregion

                    }

                }

            }

            AddErrorLog("Cron Ended : SendB2BPendingNotification1.aspx");
        }

        public void UpdateLog(string ApplicationID)
        {
            ActivitiesManager act = new ActivitiesManager();
            act.UpdateB2BNotificationSendStatus(Convert.ToInt32(ApplicationID));
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