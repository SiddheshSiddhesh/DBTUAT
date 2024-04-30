using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CommanClsLibrary;
using CommanClsLibrary.Repository.Models;
using static CommanClsLibrary.Repository.Enums;
using CommanClsLibrary.Repository.Classes;

namespace DBTPoCRA.CronJob
{
    public partial class SendB2BAndRejectTransactionWithNotification2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AddErrorLog("Cron Started : SendB2BAndRejectTransactionWithNotification2.aspx");

            MyClass cla = new MyClass();

            List<String> lst = new List<string>();
            DataTable dt = cla.GetDtByProcedureForReport("Usp_GetB2BDataForRejection", lst);

            if (dt.Rows.Count > 0)
            {
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

                        string TodayDate = DateTime.Now.ToString("dd/MM/yyyy");

                        ApplicationID = dr["ApplicationID"].ToString();
                        ApplicationCode = dr["ApplicationCode"].ToString();
                        ActivityGroupNameMr = dr["ActivityGroupNameMr"].ToString();
                        MobileNo = dr["MobileNumber"].ToString();
                        SMSContent = "विहित मुदतीत " + ActivityGroupNameMr + " व " + ApplicationCode + " घटकाची अंमलबजावणी न केल्याने सदर घटकाची पूर्वसंमती दि. " + TodayDate + " रोजी रद्द करण्यात आलेली आहे. PoCRA";
                        //MobileNo = "9421007763";
                        int result = SMS.SendSMS(SMSContent, MobileNo, "1407169389487638332");

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


            AddErrorLog("Cron Ended : SendB2BAndRejectTransactionWithNotification2.aspx");
        }


        public void UpdateLog(string ApplicationID)
        {
            try
            {
                MyClass cla = new MyClass();
                List<String> lst2 = new List<string>();
                lst2.Add(ApplicationID);
                cla.ExecuteByProcedure("Usp_UpdateB2BStatus2Rejection", lst2);
            }
            catch (Exception ex)
            {


            }


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