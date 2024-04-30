using CommanClsLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Resources;
using System.Globalization;
using System.Threading;

namespace DBTPoCRA.UsersTrans
{
    public partial class ApplicationSubmit : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;
        DataTable dt = new DataTable();
        #region"Declarection"
        MyClass cla = new MyClass();
        MyCommanClass Comcls = new MyCommanClass();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FillDll();



                    ((Literal)Master.FindControl("lblHeadings")).Text = "Application Form";

                    //  Session["Lang"] = Request.QueryString["LAN"].ToString();
                    if (Session["Lang"].ToString().Trim() == "mr-IN")
                    {
                        rm = new ResourceManager("Resources.ApplyForNewScheme", System.Reflection.Assembly.Load("App_GlobalResources")); ci = Thread.CurrentThread.CurrentCulture;
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("mr-IN");
                        LoadString(Thread.CurrentThread.CurrentCulture);
                        LoadString(ci);
                    }
                }
                else
                {
                    if (Session["Lang"].ToString().Trim() == "mr-IN")
                    {
                        rm = new ResourceManager("Resources.ApplyForNewScheme", System.Reflection.Assembly.Load("App_GlobalResources")); ci = Thread.CurrentThread.CurrentCulture;
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("mr-IN");
                        LoadString(Thread.CurrentThread.CurrentCulture);
                        LoadString(ci);
                    }
                }
            }
            catch { }
        }

        private void LoadString(CultureInfo ci)
        {
            //Literal25.Text = rm.GetString("Back_To_Activety_Details", ci);
            //  Literal1.Text = rm.GetString("Activity_Name", ci);
            // Literal2.Text = rm.GetString("Activity_Name", ci);
            Literal24.Text = rm.GetString("Land_Detail", ci);
            Literal4.Text = rm.GetString("Account_Number", ci);
            Literal5.Text = rm.GetString("Survey_Number", ci);
            Literal6.Text = rm.GetString("Area_used_for_this_activety_from_Selected", ci);
            Literal7.Text = rm.GetString("Bank_Detail", ci);
            Literal8.Text = rm.GetString("BANK_Ac_NO", ci);
            Literal9.Text = rm.GetString("BANK_NAME", ci);
            Literal10.Text = rm.GetString("IFSC_CODE", ci);

            Literal11.Text = rm.GetString("Other_Details", ci);
            Literal12.Text = rm.GetString("Estimated_cost_of_the_activity_applied_for", ci);
            Literal13.Text = rm.GetString("Expected_financial_assistance_as_per_the_cost_norms", ci);
            Literal14.Text = rm.GetString("How_would_the_applicant_arrange_for_the_balance_funds", ci);
            Literal15.Text = rm.GetString("Whether_the_applicant_or_his_family_member", ci);
            Literal16.Text = rm.GetString("Provide_details_thereof_including_year_amount_scheme_etc", ci);
            Literal17.Text = rm.GetString("Undertaking", ci);
            Literal18.Text = rm.GetString("If_I_am_selected_for_assistance_under", ci);
            Literal19.Text = rm.GetString("I_am_aware_that_I", ci);
            Literal20.Text = rm.GetString("I_am_aware_that_the", ci);

            Literal21.Text = rm.GetString("I_hereby_declare_that", ci);
            Literal22.Text = rm.GetString("I_confirm_that_I_have", ci);
            Literal23.Text = rm.GetString("All_the_information", ci);


        }
        private void FillDll()
        {
            DataTable dt = Comcls.GetForm8A(Session["RegistrationID"].ToString().Trim());
            ddl8A.DataSource = dt;
            ddl8A.DataTextField = "Names";
            ddl8A.DataValueField = "LandID";
            ddl8A.DataBind();
            ddl8A.Items.Insert(0, new ListItem("--Select--", "0"));
            ddl8A.SelectedIndex = 0;

            DivLand.Visible = false;
            if (Session["BeneficiaryTypesID"].ToString().Trim() == "1")
            {

                String LandStatus = cla.GetExecuteScalar("select LandStatus from Tbl_M_RegistrationDetails where RegistrationID=" + Session["RegistrationID"].ToString().Trim() + "");
                if (LandStatus.Trim().ToUpper() == "YES")
                {
                    DivLand.Visible = true;
                }
            }

            //

            //dt = Comcls.GetCrop();
            //ddlCrops.DataSource = dt;
            //ddlCrops.DataTextField = "Name";
            //ddlCrops.DataValueField = "ID";
            //ddlCrops.DataBind();
            //ddlCrops.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlCrops.SelectedIndex = 0;
            String SubComponentID = "";
            List<String> lst = new List<string>();
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add(Request.QueryString["T"].ToString());
            lst.Add("");
            lst.Add("");
            //dt = cla.GetDtByProcedure("SP_GetActivetySearch", lst);
            //FormView1.DataSource = dt;
            //FormView1.DataBind();
            if (Session["Lang"].ToString().Trim() == "en-IN")
            {

                ddlArenge.Items.Insert(0, new ListItem("--Select--", "0"));
                ddlArenge.Items.Insert(0, new ListItem("Self", "Self"));
                ddlArenge.Items.Insert(0, new ListItem("Bank Loan", "Bank Loan"));

                dt = cla.GetDtByProcedure("SP_GetActivetySearch", lst);
                FormView1.DataSource = dt;
                FormView1.DataBind();
                if (dt.Rows.Count > 0)
                    SubComponentID = dt.Rows[0]["SubComponentID"].ToString();


            }
            else
            {
                ddlArenge.Items.Insert(0, new ListItem("--Select--", "0"));
                ddlArenge.Items.Insert(0, new ListItem("स्वतः", "Self"));
                ddlArenge.Items.Insert(0, new ListItem("बँक", "Bank Loan"));


                dt = cla.GetDtByProcedure("SP_GetActivetySearchMr", lst);
                FormView1.DataSource = dt;
                FormView1.DataBind();
                if (dt.Rows.Count > 0)
                    SubComponentID = dt.Rows[0]["SubComponentID"].ToString();
            }


            //String a = cla.GetExecuteScalar("select top 1 A.ApplicationID from Tbl_T_ApplicationDetails A inner join  Tbl_M_ActivityMaster AC on AC.ActivityID=A.ActivityID where A.IsDeleted is null and A.RegistrationID=" + Session["RegistrationID"].ToString().Trim() + " and A.ApplicationStatusID<>2 and AC.SubComponentID=" + SubComponentID + " ");
            //if (a.Length > 0)
            //{
            //    // You can only apply a single activity in each sub component
            //    //clsMessages.Warningmsg(LiteralMsg, "You can only apply a single activity in each sub component");
            //    Util.ShowMessageBox(this.Page, "Error", "You can only apply a single activity in each sub component", "error");
            //    btnSave.Visible = false;
            //}



        }

        protected void ddl8A_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = Comcls.GetForm712Details(Session["RegistrationID"].ToString().Trim(), ddl8A.SelectedValue.Trim());
            ddl712.DataSource = dt;
            ddl712.DataTextField = "Names";
            ddl712.DataValueField = "LandID";
            ddl712.DataBind();
            ddl712.Items.Insert(0, new ListItem("--Select--", "0"));
            ddl712.SelectedIndex = 0;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            LiteralMsg.Text = "";
            string s = "";
            //------------validations ---------------------// 

            String ActivityGroupID = cla.GetExecuteScalar("Select A.ActivityGroupID from Tbl_M_ActivityMaster A where A.ActivityID=" + Request.QueryString["T"].ToString() + "");
            String MultipleApplicationAlow = cla.GetExecuteScalar("Select g.MultipleApplicationAlow from Tbl_M_ActivityMaster A inner join Tbl_M_Activity_Groups G on A.ActivityGroupID=G.ActivityGroupID where a.ActivityID=" + Request.QueryString["T"].ToString() + " ");
            if (MultipleApplicationAlow.Length == 0) MultipleApplicationAlow = "NO";

            if (MultipleApplicationAlow.Trim().Length > 0)
            {
                if (MultipleApplicationAlow == "NO")
                {
                    s = cla.GetExecuteScalar("SELECT top 1 ApplicationID FROM Tbl_T_ApplicationDetails WHERE  (RegistrationID =" + Session["RegistrationID"].ToString().Trim() + ") and IsDeleted is null  and ActivityGroupID=" + ActivityGroupID + " and ApplicationStatusID not in (2,25) ");
                    if (s.Length > 0)
                    {
                        Util.ShowMessageBox(this.Page, "Error", "Either Your Application for same activity on same land is already in process or you are not allow to do more than one application . Please check status of your application on your dashboard.", "error");
                        return;
                    }
                }

            }





            //s = cla.GetExecuteScalar("SELECT top 1 ApplicationID FROM Tbl_T_ApplicationDetails WHERE  (RegistrationID =" + Session["RegistrationID"].ToString().Trim() + ") and IsDeleted is null  and ActivityID=" + Request.QueryString["T"].ToString() + " and ApplicationStatusID not in (2,3) ");
            //if (s.Length > 0)
            //{
            //    Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG20", Session["Lang"].ToString()), "error");
            //    return;
            //}

            //if (ddl712.SelectedIndex > 0)
            //{
            //    s = cla.GetExecuteScalar("SELECT top 1 ApplicationID FROM Tbl_T_ApplicationDetails WHERE  (RegistrationID =" + Session["RegistrationID"].ToString().Trim() + ") and IsDeleted is null   and LandID=" + ddl712.SelectedValue.Trim() + " and ActivityID=" + Request.QueryString["T"].ToString() + "  and ApplicationStatusID not in (2,3)");
            //    if (s.Length > 0)
            //    {
            //        //clsMessages.Warningmsg(LiteralMsg, "Your Application for same activity on same land is already in process. Please check status of your application on your dashboard.");
            //        //MyCommanClass.GetMsgInEnForDB("MSG20", Session["Lang"].ToString());
            //        // Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG20", Session["Lang"].ToString()), "error");
            //        Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG20", Session["Lang"].ToString()), "error");
            //        return;
            //    }
            //}
            //else
            //{
            //    s = cla.GetExecuteScalar("SELECT top 1 ApplicationID FROM Tbl_T_ApplicationDetails WHERE  (RegistrationID =" + Session["RegistrationID"].ToString().Trim() + ") and IsDeleted is null  AND ActivityID=" + Request.QueryString["T"].ToString() + "  and ApplicationStatusID not in (2,3)");
            //    if (s.Length > 0)
            //    {
            //        //clsMessages.Warningmsg(LiteralMsg, "Your Application for same activity on same land is already in process. Please check status of your application on your dashboard.");
            //        // MyCommanClass.GetMsgInEnForDB("MSG20", Session["Lang"].ToString());
            //        Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG20", Session["Lang"].ToString()), "error");
            //        return;
            //    }
            //}





            Boolean IsAcept = true;

            if (CheckBox1.Checked == false)
            {
                IsAcept = false;
            }
            if (CheckBox2.Checked == false)
            {
                IsAcept = false;
            }
            if (CheckBox3.Checked == false)
            {
                IsAcept = false;
            }
            if (CheckBox4.Checked == false)
            {
                IsAcept = false;
            }
            if (CheckBox5.Checked == false)
            {
                IsAcept = false;
            }


            if (IsAcept == false)
            {
                //clsMessages.Warningmsg(LiteralMsg, "Please accept all Undertaking.");
                // MyCommanClass.GetMsgInEnForDB("MSG21", Session["Lang"].ToString());
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG21", Session["Lang"].ToString()), "error");
                return;
            }

            int ApplicationID = cla.TableID("Tbl_T_ApplicationDetails", "ApplicationID");
            int ApplicationLogID = cla.TableID("Tbl_T_ApplicationDetails_Log", "ApplicationLogID");
            LiteralMsg.Text = "";

            String ApprovalStageID = cla.GetExecuteScalar("SELECT     top 1  Tbl_M_ActivityApprovalStage.ApprovalStageID FROM Tbl_M_ActivityApprovalStage INNER JOIN Tbl_M_ApprovalStages ON Tbl_M_ActivityApprovalStage.ApprovalStageID = Tbl_M_ApprovalStages.ApprovalStageID WHERE  (Tbl_M_ActivityApprovalStage.ActivityID =" + Request.QueryString["T"].ToString().Trim() + ") AND (Tbl_M_ActivityApprovalStage.IsDeleted IS NULL) order by Tbl_M_ApprovalStages.OrderNo");

            String ApplicationCode = cla.GetExecuteScalar("SELECT   Tbl_M_VillageMaster.VillageCode FROM Tbl_M_RegistrationDetails INNER JOIN Tbl_M_VillageMaster ON dbo.Tbl_M_RegistrationDetails.Work_VillageID = dbo.Tbl_M_VillageMaster.VillageID WHERE  ( Tbl_M_RegistrationDetails.RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ")");
            ApplicationCode = ApplicationCode + "/" + cla.GetExecuteScalar("select ActivityCode from Tbl_M_ActivityMaster where ActivityID=" + Request.QueryString["T"].ToString() + "").ToUpper();
            ApplicationCode = ApplicationCode + "/" + ApplicationID.ToString();

            DataTable dts = new DataTable();
            if (ddl712.SelectedIndex > 0)
            {
                dts = cla.GetDataTable(" Select L.City_ID,L.TalukaID,L.VillageID,v.SubdivisionID,v.ClustersMasterID from Tbl_M_RegistrationLand as L inner join Tbl_M_VillageMaster v on v.VillageID=l.VillageID where L.LandID=(Select  ParentLandID from Tbl_M_RegistrationLand where LandID="+ ddl712.SelectedValue.Trim()+ " )");
            }
            else
            {
                dts = cla.GetDataTable("Select L.Work_City_ID as City_ID ,L.Work_TalukaID as TalukaID ,L.Work_VillageID as VillageID ,v.SubdivisionID as SubdivisionID ,v.ClustersMasterID as ClustersMasterID from Tbl_M_RegistrationDetails as L inner join Tbl_M_VillageMaster v on v.VillageID=l.Work_VillageID where L.RegistrationID=" + Session["RegistrationID"].ToString().Trim() + " ");
            }

            if(dts.Rows.Count==0)
            {
                Util.ShowMessageBox(this.Page, "Error", "Error in Data", "error");
                return;

            }

            List<String> lst = new List<string>();
            using (SqlConnection connection = new SqlConnection(clsSettings.strCoonectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction("CTransaction");
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    // Tbl_T_ApplicationDetails
                    List<string> param = new List<string>();
                    param.Add(ApplicationID.ToString());//ApplicationID
                    param.Add(ApplicationCode);//ApplicationCode
                    param.Add(cla.mdy(cla.SvrDate()));//ApplicationDate
                    param.Add(Session["RegistrationID"].ToString());//RegistrationID

                    if (ddl712.SelectedIndex > 0)
                        param.Add(ddl712.SelectedValue.ToString().Trim());//LandID
                    else
                    {
                        param.Add("");
                    }

                    param.Add(Request.QueryString["T"].ToString());//ActivityID
                    param.Add("");//CropID
                    param.Add(txtEstimatedCost.Text.Trim());//EstimatedCost
                    param.Add(txtExpectedCost.Text.Trim());//ExpectedFinAssistance
                    param.Add(ddlArenge.SelectedValue);//BalanceFundSource
                    param.Add(txtLastDetails.Text.Trim());//PastBenefitHistory
                    param.Add("1");// ApplicationStatusID                                 
                    param.Add("I");
                    cla.ExecuteByProcedure("SP_T_ApplicationDetails", param, command);

                    //Tbl_T_ApplicationDetails_Log

                    param.Add(ApplicationLogID.ToString());//ApplicationLogID
                    param.Add(ApplicationID.ToString());//ApplicationID
                    param.Add("");//LogDetails
                    param.Add("1");//ApplicationStatusID 
                    param.Add(Session["RegistrationID"].ToString());//UpdateByRegID
                    param.Add("I");
                    cla.ExecuteByProcedure("SP_T_ApplicationDetails_Log", param, command);
                    param.Clear();


                    cla.ExecuteCommand("UPDATE Tbl_T_ApplicationDetails SET ApprovalStageID =" + ApprovalStageID + " , AreaOfLandUsed='" + txtUsedAre.Value.Trim() + "'   WHERE (ApplicationID =" + ApplicationID.ToString().Trim() + ")", command);


                    foreach(DataRow dr in dts.Rows)
                    {
                        
                        cla.ExecuteCommand("UPDATE Tbl_T_ApplicationDetails SET   APP_City_ID="+dr["City_ID"].ToString() + ",APP_TalukaID=" + dr["TalukaID"].ToString() + ",APP_VillageID=" + dr["VillageID"].ToString() + ",APP_ClustersMasterID=" + dr["ClustersMasterID"].ToString() + ",APP_SubdivisionID=" + dr["SubdivisionID"].ToString() + "  WHERE (ApplicationID =" + ApplicationID.ToString().Trim() + ")", command);
                    }



                    transaction.Commit();
                    if (Session["Lang"].ToString().Trim() == "en-IN")
                    {
                        SMS.SendSmsOnSchemeApplication(cla.GetExecuteScalar("SELECT   MobileNumber  FROM Tbl_M_RegistrationDetails where RegistrationID=" + Session["RegistrationID"].ToString() + ""), ApplicationCode.Trim(), cla.GetExecuteScalar("select ActivityName from Tbl_M_ActivityMaster where ActivityID=" + Request.QueryString["T"].ToString() + "").ToUpper());
                    }
                    else
                    {
                        SMS.SendSmsOnSchemeApplication(cla.GetExecuteScalar("SELECT   MobileNumber  FROM Tbl_M_RegistrationDetails where RegistrationID=" + Session["RegistrationID"].ToString() + ""), ApplicationCode.Trim(), cla.GetExecuteScalar("select ActivityNameMr from Tbl_M_ActivityMaster where ActivityID=" + Request.QueryString["T"].ToString() + "").ToUpper());

                    }
                    Response.Redirect("ApplicationSucess.aspx?T=" + Request.QueryString["T"].Trim() + "&A=" + ApplicationID.ToString() + "", false);



                }
                catch (Exception ex)
                {
                    clsMessages.Errormsg(LiteralMsg, MyCommanClass.GetMsgInEnForDB("MSG22", Session["Lang"].ToString()) + ex.Message.Trim());////
                    try
                    {
                        transaction.Rollback();

                    }
                    catch
                    {
                        //  
                    }

                }
                finally
                {

                    if (connection != null) { connection.Close(); }
                    command.Dispose();
                }

            }

        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            divd.Visible = false;
            if (RadioButtonList1.SelectedValue.Trim() == "YES")
            {
                divd.Visible = true;
            }
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


                //grdSubject.Columns[0].HeaderText = rm.GetString("Term", ci);
                //grdSubject.Columns[1].HeaderText = rm.GetString("Common_Subsidy", ci);
                //grdSubject.Columns[2].HeaderText = rm.GetString("SC_ST_Subsidy", ci);

                //grdChild.Columns[0].HeaderText = rm.GetString("Required", ci);
                //grdChild.Columns[1].HeaderText = rm.GetString("Details", ci);


                ((Literal)FormView1.FindControl("Literal25")).Text = rm.GetString("Back_To_Activety_Details", ci);
                ((Literal)FormView1.FindControl("Literal1")).Text = rm.GetString("Activity_Name", ci);
                ((Literal)FormView1.FindControl("Literal2")).Text = rm.GetString("Activity_Code", ci);

            }



        }
    }
}
