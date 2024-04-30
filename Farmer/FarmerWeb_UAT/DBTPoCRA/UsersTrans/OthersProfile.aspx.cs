using System.Text;
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
    public partial class OthersProfile : System.Web.UI.Page
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
                    txtDate.Text = cla.SvrDate();
                    // ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CheckUpdate();  </script>", false);
                    ViewState["FPORegistrationID"] = Request.QueryString["ID"].ToString();
                    //ShowRecords(ViewState["FPORegistrationID"].ToString());
                    ifram.Src = "~/FPOUsers/FpoFpcProfileEdit.aspx?RID="+ Request.QueryString["ID"].ToString().Trim();
                    Literal5.Text =" REGISTRATION VERIFICATION";
                    Literal1.Text = cla.GetExecuteScalar("Select ApprovalStatus from FPO_M_RegistrationDetails where FPORegistrationID="+ ViewState["FPORegistrationID"].ToString().Trim()+ "");
                    //ddlApplicationStatus.DataSource = Comcls.GetApplicationStatus("R");
                    //ddlApplicationStatus.DataTextField = "ApplicationStatus";
                    //ddlApplicationStatus.DataValueField = "ApplicationStatusID";
                    //ddlApplicationStatus.DataBind();
                    //ddlApplicationStatus.Items.Insert(0, new ListItem("--Select--", "0"));
                    FillLog();

                    chkFeasibility.DataSource = Comcls.GetApprovalCheckList("5", "1");
                    chkFeasibility.DataTextField = "FeasibilityRpt";
                    chkFeasibility.DataValueField = "FPOCheckListID";
                    chkFeasibility.DataBind();


                    if (Session["Lang"].ToString().Trim() == "mr-IN")
                    {
                        rm = new ResourceManager("Resources.UserManagement", System.Reflection.Assembly.Load("App_GlobalResources")); ci = Thread.CurrentThread.CurrentCulture;
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("mr-IN");
                        LoadString(Thread.CurrentThread.CurrentCulture);
                        LoadString(ci);
                    }

                }
                else
                {
                    if (Session["Lang"].ToString().Trim() == "mr-IN")
                    {
                        rm = new ResourceManager("Resources.UserManagement", System.Reflection.Assembly.Load("App_GlobalResources")); ci = Thread.CurrentThread.CurrentCulture;
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
            Literal2.Text = rm.GetString("VERIFICATION", ci);
            Literal3.Text = rm.GetString("Application_Status", ci);
            Literal6.Text = rm.GetString("Date", ci);
            Literal7.Text = rm.GetString("Current_Status", ci);
            Literal8.Text = rm.GetString("Reason", ci);
            Literal9.Text = rm.GetString("Remark_if_any", ci);
            btnUpdate.Text = rm.GetString("Update", ci);

            
            Literal73.Text = rm.GetString("VERIFICATION", ci);
            Literal74.Text = rm.GetString("VERIFICATION_LOG", ci);

        }

       
        protected void ddlApplicationStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkReasons.DataSource = Comcls.GetReasonVerifecationFPO("52", ddlApplicationStatus.SelectedValue.Trim());
            chkReasons.DataTextField = "Reasons";
            chkReasons.DataValueField = "ReasonID";
            chkReasons.DataBind();

            txtResion.Enabled = true;
            if (ddlApplicationStatus.SelectedValue.Trim() == "7")
            {
                txtResion.Enabled = false;
            }
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CheckUpdate();  </script>", false);
        }

        private void FillLog()
        {
            grdLog.DataSource = cla.GetDataTable("SELECT Convert(nvarchar(20),UpdateOnDate,103) as Date,UpdateBy as FullName,RegistrationStatus as Status,LogDetails as Reason FROM FPO_M_RegistrationDetails_Log where FPORegistrationID="+ViewState["FPORegistrationID"].ToString().Trim()+" order by dateTime_Table");
            grdLog.DataBind();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {


            LiteralMsg.Text = "";
            //------------validations ---------------------// 

            if (chkReasons.Items.Count > 0)
            {
                Boolean IsAcept = false;
                for (int x = 0; x != chkReasons.Items.Count; x++)
                {
                    if (chkReasons.Items[x].Selected)
                    {
                        IsAcept = true;
                    }
                }

                if (IsAcept == false)
                {
                    clsMessages.Warningmsg(LiteralMsg, "Please Select Reason From list.");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CheckUpdate();  </script>", false);
                    return;
                }
            }

            if (chkFeasibility.Items.Count > 0)
            {
                Boolean IsAcept = false;
                for (int x = 0; x != chkFeasibility.Items.Count; x++)
                {
                    String[] s = chkFeasibility.Items[x].Text.Trim().Split('~');
                    if (s[1].Trim() == "Mandatory")
                    {
                        if (chkFeasibility.Items[x].Selected)
                        {
                            IsAcept = true;
                        }
                        else
                        {
                            IsAcept = false;
                            break;
                        }
                    }

                }
                if (ddlApplicationStatus.SelectedValue.Trim() == "7")
                {
                    if (IsAcept == false)
                    {
                        Util.ShowMessageBox(this.Page, "Required", "Please Select Mandatory Checklist From list.", "error");
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CheckUpdate();  </script>", false);
                        return;
                    }
                }
            }

            //------------End validations ---------------------// 


            String str = "";
            //int RegistrationLogID = cla.TableID("FPO_M_RegistrationDetails_Log", "RegistrationLogID");
            //int RegistrationLogReasonID = cla.TableID("FPO_M_RegistrationDetails_LogReason", "RegistrationLogReasonID");

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



                    // UPDATE

                    String Reasons = "";
                    for (int x = 0; x != chkReasons.Items.Count; x++)
                    {
                        if (chkReasons.Items[x].Selected)
                        {
                            if(Reasons.Trim().Length==0)
                            {
                                Reasons = chkReasons.Items[x].Text.Trim();
                            }
                            else
                            {
                                Reasons += "<br>"+chkReasons.Items[x].Text.Trim();
                            }
                        }
                    }

                    if (txtResion.Text.Trim().Replace("'","").Replace("'","").Replace("'","").Length > 0)
                    {
                        if (Reasons.Trim().Length == 0)
                        {
                            Reasons += txtResion.Text.Trim().Replace("'","").Replace("'","");
                        }
                        else
                        {
                            Reasons += "<br>" + txtResion.Text.Trim().Replace("'","").Replace("'","");
                        }
                    }

                    str = " INSERT INTO FPO_M_RegistrationDetails_Log (FPORegistrationID, LogDetails, RegistrationStatus, UpdateOnDate, UpdateByRegID,UpdateBy)";
                    str += " VALUES(" + ViewState["FPORegistrationID"].ToString() + ",N'" + Reasons.Trim() + "','" + ddlApplicationStatus.SelectedItem.Text.Trim() + "','" + cla.mdy(txtDate.Text.Trim()) + "'," + Session["UserId"].ToString() + ",'" + Session["UsersName"].ToString() + "')";
                    cla.ExecuteCommand(str, command);

                    str = " UPDATE FPO_M_RegistrationDetails SET ApprovalStatus='" + ddlApplicationStatus.SelectedItem.Text + "'  WHERE FPORegistrationID=" + ViewState["FPORegistrationID"].ToString() + "";
                    cla.ExecuteCommand(str, command);

                    cla.ExecuteCommand("Update FPO_M_RegistrationDetails_CheckList set IsDeleted='1' where ApprovalStageID=52 and FPORegistrationID=" + ViewState["FPORegistrationID"].ToString() + "  ", command);
                    for (int x = 0; x != chkFeasibility.Items.Count; x++)
                    {
                        if (chkFeasibility.Items[x].Selected)
                        {
                            str = " INSERT INTO FPO_M_RegistrationDetails_CheckList (ApprovalStageID, FPORegistrationID, FPOCheckListID,CheckListTypeID)";
                            str += " VALUES(53," + ViewState["FPORegistrationID"].ToString() + "," + chkFeasibility.Items[x].Value.Trim() + ",1)";
                            cla.ExecuteCommand(str, command);

                        }
                    }

                    transaction.Commit();
                    clsMessages.Sucessmsg(LiteralMsg, "U");

                    //-------SMS--------------------------
                    try
                    {
                        String Mob = cla.GetExecuteScalar("Select a.CeoAuthorisedPersonMob from FPO_M_RegistrationDetails a where a.FPORegistrationID=" + ViewState["FPORegistrationID"].ToString().Trim() + "");
                        if(ddlApplicationStatus.SelectedValue.Trim()== "Back To Beneficiary")
                        {
                            FPOSMS.FPCRegScrutinyBacktoApplicant(Mob.Trim(), ViewState["FPOApplicationID"].ToString().Trim());
                        }
                        if (ddlApplicationStatus.SelectedValue.Trim() == "Rejected")
                        {
                            FPOSMS.FPCRegScrutinyRejection(Mob.Trim(), ViewState["FPOApplicationID"].ToString().Trim());
                        }

                        
                    }
                    catch
                    {

                    }
                    //------------------------------------


                    FillLog();
                    Literal1.Text = ddlApplicationStatus.SelectedItem.Text;
                   
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Msgbox", "<script>  callParentClick();  </script>", false);
                }
                catch (Exception ex)
                {
                    //String error = "Error in Add Journey Save button Click " + ex.ToString();
                    //WriteError(error, Session["UserEmailID"].ToString());
                    try
                    {
                        transaction.Rollback();

                    }
                    catch
                    {

                    }
                    clsMessages.Errormsg(LiteralMsg, "Please provide correct input." + ex.Message.Trim());
                }
                finally
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CheckUpdate();  </script>", false);
                    if (connection != null) { connection.Close(); }
                    command.Dispose();
                }

            }

        }
    }
}
