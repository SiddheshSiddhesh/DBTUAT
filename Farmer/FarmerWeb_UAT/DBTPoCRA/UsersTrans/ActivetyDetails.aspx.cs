using CommanClsLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBTPoCRA.UsersTrans
{
    public partial class ActivetyDetails : System.Web.UI.Page
    {
        #region"Declarection"
        MyClass cla = new MyClass();
        MyCommanClass Comcls = new MyCommanClass();
        ResourceManager rm;
        CultureInfo ci;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    FillActivety();
                    ((Literal)Master.FindControl("lblHeadings")).Text = "Activety Details";

                }
            }
            catch { }
        }
        private void FillActivety()
        {
            DataTable dt = new DataTable();
            String SubComponentID = "0";
            List<String> lst = new List<string>();
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add(Request.QueryString["T"].ToString());
            lst.Add("");
            lst.Add("");
            lst.Add(Session["RegistrationID"].ToString());

            if (Session["Lang"].ToString().Trim() == "en-IN")
            {
                dt = cla.GetDtByProcedure("SP_GetActivetySearchNew", lst);
                FormView1.DataSource = dt;
                FormView1.DataBind();
                if (dt.Rows.Count > 0)
                    SubComponentID = dt.Rows[0]["SubComponentID"].ToString();
            }
            else
            {
                dt = cla.GetDtByProcedure("SP_GetActivetySearchNewMr", lst);
                FormView1.DataSource = dt;
                FormView1.DataBind();
                if (dt.Rows.Count > 0)
                    SubComponentID = dt.Rows[0]["SubComponentID"].ToString();
            }

            LiteralMsg.Text = "";
            // to alow single activity from a group
            //String a = cla.GetExecuteScalar("select top 1 A.ApplicationID from Tbl_T_ApplicationDetails A inner join  Tbl_M_ActivityMaster AC on AC.ActivityID=A.ActivityID where A.IsDeleted is null and A.RegistrationID=" + Session["RegistrationID"].ToString().Trim() + " and A.ApplicationStatusID<>2 and AC.SubComponentID="+SubComponentID+" ");
            //if (a.Length > 0)
            //{
            //    // You can only apply a single activity in each sub component
            //    clsMessages.Warningmsg(LiteralMsg, "You can only apply a single activity in each sub component");

            //}

        }

        protected void FormView1_DataBound(object sender, EventArgs e)
        {

            string id = Request.QueryString["T"].ToString();
            //  string id = (FormView1.FindControl("ActivityID") as Label).Text.Trim();
            GridView grdSubject = FormView1.FindControl("grdSubject") as GridView;
            GridView grdChild = FormView1.FindControl("grdChild") as GridView;//grdExeTL
            GridView grdExeT = FormView1.FindControl("grdExeT") as GridView;


            if (Session["Lang"].ToString().Trim() == "mr-IN")
            {


                rm = new ResourceManager("Resources.ApplyForNewScheme", System.Reflection.Assembly.Load("App_GlobalResources")); ci = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("mr-IN");
                ci = Thread.CurrentThread.CurrentCulture;


                grdSubject.Columns[0].HeaderText = rm.GetString("Term", ci);
                grdSubject.Columns[1].HeaderText = rm.GetString("Common_Subsidy", ci);
                grdSubject.Columns[2].HeaderText = rm.GetString("SC_ST_Subsidy", ci);

                grdChild.Columns[0].HeaderText = rm.GetString("Required", ci);
                grdChild.Columns[1].HeaderText = rm.GetString("Details", ci);


                ((Label)FormView1.FindControl("Literal1")).Text = rm.GetString("Back_To_Activity", ci);
                ((Label)FormView1.FindControl("Literal2")).Text = rm.GetString("Continue_to_Application", ci);
                ((Label)FormView1.FindControl("Literal3")).Text = rm.GetString("Activity_Name", ci);
                ((Label)FormView1.FindControl("Literal4")).Text = rm.GetString("Activity_Code", ci);
                ((Label)FormView1.FindControl("Literal5")).Text = rm.GetString("Component_Name", ci);
                ((Label)FormView1.FindControl("Literal6")).Text = rm.GetString("Sub_Component_Name", ci);
                ((Label)FormView1.FindControl("Literal7")).Text = rm.GetString("Subsidy_Details", ci);
            }





            List<String> lst = new List<String>();
            lst.Add("");
            lst.Add(Request.QueryString["T"].ToString());
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("S");
            lst.Add("");
            lst.Add("");
            DataTable da = new DataTable();
            da = cla.GetDtByProcedure("SP_ActivityPaymentTermsWeb", lst);//SP_ActivityPaymentTermsWeb
            grdSubject.DataSource = da;
            grdSubject.DataBind();

            lst.Clear();
            lst.Add(Request.QueryString["T"].ToString());//id.ToString()
            if (Session["Lang"].ToString().Trim() == "mr-IN")
                lst.Add("M");//id.ToString()
            else
                lst.Add("M");
            DataTable da2 = cla.GetDtByProcedure("SP_GetActivityWiseDocEliDetailsFARMER", lst);
            grdChild.DataSource = da2;
            grdChild.DataBind();

            //grdExeT.DataSource = cla.GetDataTable(" Select awt.TimeLineDay , sa.ApprovalStages from Tbl_M_ActivityStageWiseTimeLine awt inner Join Tbl_M_ActivityApprovalStage sas On awt.ApprovalStageID = sas.ActivityApprovalStageID inner Join Tbl_M_ApprovalStages sa On awt.ApprovalStageID = sa.ApprovalStageID where (awt.ActivityID = " + Request.QueryString["T"].ToString() + ")");
            //grdExeT.DataBind();

            // FormView1.DataBind();







        }





    }
}


