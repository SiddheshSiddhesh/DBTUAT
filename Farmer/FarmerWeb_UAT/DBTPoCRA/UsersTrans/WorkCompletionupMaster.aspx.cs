using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommanClsLibrary;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace DBTPoCRA.UsersTrans
{
    public partial class WorkCompletionupMaster : System.Web.UI.Page
    {

        MyClass cla = new MyClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    if (Request.QueryString.Count > 0)
                    {

                        ViewState["ApplicationID"] = Request.QueryString["ID"].ToString();

                        //ApprovalStageID>=5
                        String s = cla.GetExecuteScalar("Select Tbl_T_ApplicationDetails.ApprovalStageID from  Tbl_T_ApplicationDetails where ApplicationID=" + Request.QueryString["ID"].ToString() + "");
                        if (s.Length == 0) s = "0";

                        if (Convert.ToInt32(s) < 5)
                        {
                            Literal1.Text = "Please call to Pocra office";
                            return;
                        }
                        s = cla.GetExecuteScalar("Select Tbl_T_ApplicationDetails.RegistrationID from  Tbl_T_ApplicationDetails where ApplicationID=" + Request.QueryString["ID"].ToString() + "");
                        if (s.Length == 0) s = "0";

                        if (s != Session["RegistrationID"].ToString().Trim())
                        {
                            Literal1.Text = "Please call to Pocra office";
                            return;
                        }



                        FillDetails();

                        //new payment request button
                        String str = cla.GetExecuteScalar("SELECT  count(WorkReportID)   FROM Tbl_T_Application_WorkReport where ApplicationID=" + Request.QueryString["ID"].ToString() + " and IsDeleted is null  and ApplicationStatusID not in (15,2,25)  ");
                        if (str.Trim().Length == 0)
                        {
                            Literal1.Text = "";
                        }
                        else
                        {
                            if (Convert.ToInt32(str) == 0)
                            {
                                if (cla.GetExecuteScalar("SELECT  top 1 WorkReportID  FROM Tbl_T_Application_WorkReport where  ApplicationID=" + Request.QueryString["ID"].ToString() + " and IsDeleted is null  and ApplicationStatusID=20").Length == 0)
                                {
                                    // if back to beni then add new will not avilab
                                    Literal1.Text = "<a class='btn btn-primary' href='WorkCompletionUp.aspx?ID=" + Request.QueryString["ID"].ToString() + "&AID=" + Request.QueryString["AID"].ToString() + "'> Add New Payment Request </a>";
                                }
                            }
                        }



                        // Activities for which part payment is not available to be closed once the payment is done for that activity. Re-payment option to be made unavailable even if partial payment is made 
                        if (cla.GetExecuteScalar("SELECT  top 1 WorkReportID   FROM Tbl_T_Application_WorkReport where ApplicationID=" + Request.QueryString["ID"].ToString() + " and IsDeleted is null and ApplicationStatusID not in (2,25) ").Length > 0)// rejected nahi hay
                        {
                            // one payment done
                            if (cla.GetExecuteScalar("SELECT PartialPaymentAllowed FROM Tbl_M_ActivityMaster where ActivityID=(Select Tbl_T_ApplicationDetails.ActivityID from Tbl_T_ApplicationDetails where ApplicationID=" + Request.QueryString["ID"].ToString() + " and Tbl_T_ApplicationDetails.IsDeleted is null ) and IsDeleted is null") == "NO")
                            {
                                Literal1.Text = "Not available for this Activity";
                            }
                        }

                        #region  Code By Mayur 
                        //Partial Payment Request Not Allowed for Temporary 
                        //Remove this code on Phase 2

                        string PoCRAPhase = WebConfigurationManager.AppSettings["CurrentPoCRAPhase"].ToString().ToUpper();
                        if (PoCRAPhase == "PHASE1")
                        {
                            string ActivityStatus = cla.GetExecuteScalar("SELECT PartialPaymentAllowed FROM Tbl_M_ActivityMaster where IsDeleted is null and  ActivityID=(Select Tbl_T_ApplicationDetails.ActivityID from Tbl_T_ApplicationDetails where ApplicationID=" + Request.QueryString["ID"].ToString() + " and Tbl_T_ApplicationDetails.IsDeleted is null ) and IsDeleted is null");
                            if (ActivityStatus.ToUpper() == "YES" || ActivityStatus.ToUpper()=="")
                            {
                                Literal1.Text = "Multiple Payment Request are not allowed for this activity";
                            }
                        }
                        #endregion

                    }

                }
            }
            catch { }

        }
        private void FillDetails()
        {
            List<String> lst = new List<string>();

            lst.Add(Request.QueryString["ID"].ToString());
            DataTable dt = new DataTable();
            dt = cla.GetDtByProcedure("SP_GetWorkCompletionRequests", lst);
            grdData.DataSource = dt;
            grdData.DataBind();

            if (dt.Rows.Count == 0)
            {
                Response.Redirect("~/UsersTrans/WorkCompletionUp.aspx?ID=" + Request.QueryString["ID"].ToString() + "&AID=" + Request.QueryString["AID"].ToString() + "", false);

            }

        }

    }
}