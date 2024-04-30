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


namespace DBTPoCRA.Registration
{
    public partial class CommunityRegistrationProfile : System.Web.UI.Page
    {
        ResourceManager rm;

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
                    multiViewEmployee.ActiveViewIndex = 0;
                    ViewState["RegistrationID"] = "";
                    FillDropDowns();
                    FillDetails();
                    ((Literal)Master.FindControl("lblHeadings")).Text = "Profile View Edit ";

                    // For Default Language (Marathi)

                    //  Session["Lang"] = Request.QueryString["LAN"].ToString();


                    if (Session["Lang"].ToString().Trim() == "mr-IN")
                    {
                        rm = new ResourceManager("Resources.CommunityRegistration", System.Reflection.Assembly.Load("App_GlobalResources"));
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("mr-IN");
                        LoadString(Thread.CurrentThread.CurrentCulture);

                    }
                }
                else
                {
                    //rm = new ResourceManager("Resources.CommunityRegistration", System.Reflection.Assembly.Load("App_GlobalResources"));
                    //ci = Thread.CurrentThread.CurrentCulture;
                    //if (ci.Name == "mr-IN")
                    //{
                    //    LoadString(ci);
                    //}
                    if (Session["Lang"].ToString().Trim() == "mr-IN")
                    {
                        rm = new ResourceManager("Resources.CommunityRegistration", System.Reflection.Assembly.Load("App_GlobalResources"));
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("mr-IN");
                        LoadString(Thread.CurrentThread.CurrentCulture);

                    }
                }
            }
            catch { }
        }


        private void LoadString(CultureInfo ci)
        {
            Menu1.Items[0].Text = rm.GetString("REGISTRATION_DETAILS", ci);
            Menu1.Items[1].Text = rm.GetString("REGISTERED_ADDRESS", ci);
            Menu1.Items[2].Text = rm.GetString("VCRMC_BANK_DETAILS", ci);
            Menu1.Items[3].Text = rm.GetString("Declaration", ci);

            Literal1.Text = rm.GetString("GRAM_PANCHAYAT_CODE", ci);
            Literal2.Text = rm.GetString("AUTHORISED_PERSON_NAME", ci);
            Literal3.Text = rm.GetString("GENDER", ci);
            Literal4.Text = rm.GetString("AUTHORISED_PERSON_MOBILE", ci);
            Literal5.Text = rm.GetString("AUTHORISED_PERSON_EMAILID", ci);
            Literal6.Text = rm.GetString("REGISTERED_ADDRESS", ci);
            Literal7.Text = rm.GetString("GRAM_PANCHAYAT_ADDRESS", ci);
            Literal8.Text = rm.GetString("STREET_NO", ci);
            Literal9.Text = rm.GetString("DISTRICT", ci);
            Literal10.Text = rm.GetString("TALUKA", ci);
            Literal11.Text = rm.GetString("POST", ci);
            Literal12.Text = rm.GetString("PIN_CODE", ci);
            Literal13.Text = rm.GetString("VILLAGE", ci);
            Literal14.Text = rm.GetString("CLUSTER_CODE", ci);
            Literal15.Text = rm.GetString("MOBILE_1", ci);
            Literal16.Text = rm.GetString("MOBILE_2", ci);
            Literal17.Text = rm.GetString("LANDLINE_NO", ci);
            Literal18.Text = rm.GetString("EMAILID", ci);
            Literal19.Text = rm.GetString("VCRMC_BANK_DETAILS", ci);
            //Literal20.Text = rm.GetString("BANK_Ac_NO", ci); not found in marathi
            Literal21.Text = rm.GetString("Ac_HOLDER_NAME", ci);
            // Literal22.Text = rm.GetString("BANK_NAME", ci); not found in marathi
            //Literal23.Text = rm.GetString("BRANCH_NAME", ci); not found in marathi
            Literal24.Text = rm.GetString("IFSC_CODE", ci);
            Literal25.Text = rm.GetString("Declaration", ci);
            Literal26.Text = rm.GetString("Declarations", ci);
            Literal27.Text = rm.GetString("Info1", ci);
            //Literal28.Text = rm.GetString("Info2", ci); not found in marathi
            //Literal29.Text = rm.GetString("Info3", ci);
            Literal30.Text = rm.GetString("REGISTRATION_DETAILS", ci);


        }


        //
        private void FillDetails()
        {
            ViewState["RegistrationID"] = Session["RegistrationID"].ToString();
            List<String> lst = new List<String>();
            lst.Add(Session["RegistrationID"].ToString());

            dt = new DataTable();
            dt = cla.GetDtByProcedure("SP_CommunityRegistration_View_Details", lst);

            if (dt.Rows.Count > 0)
            {
                //ddlGramPanchayatCode.SelectedValue = (dt.Rows[0]["GramPanchayatCode"].ToString());
                txtGramPanchayatCode.Text = dt.Rows[0]["GramPanchayatCode"].ToString();

                txtName.Text = dt.Rows[0]["RegisterName"].ToString();//RegisterName  
                rdoGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
                txtAuthMobileNo.Text = dt.Rows[0]["MobileNumber"].ToString();
                txtAuthMobileEmail.Text = dt.Rows[0]["GramPanchayatEmail"].ToString();//FPORegistrationDate
                txtHouseNo.Text = dt.Rows[0]["Address1HouseNo"].ToString();
                txtStreetNo.Text = dt.Rows[0]["Address1StreetName"].ToString();
                ddlDISTRICT.SelectedValue = dt.Rows[0]["Address1City_ID"].ToString();//Address1City_ID
                ddlDISTRICT_SelectedIndexChanged(null, null);
                ddlTALUKA.SelectedValue = dt.Rows[0]["Address1TalukaID"].ToString();//Address1TalukaID
                ddlTALUKA_SelectedIndexChanged(null, null);
                ddlPOST.SelectedValue = dt.Rows[0]["Address1Post_ID"].ToString();//Address1Post_ID
                ddlPOST_SelectedIndexChanged(null, null);
                txtPostPin.Text = dt.Rows[0]["Address1PinCode"].ToString();//
                ddlVILLAGE.SelectedValue = dt.Rows[0]["Address1VillageID"].ToString();//Address1VillageID
                txtCLUSTARCODE.Text = dt.Rows[0]["Clusters"].ToString();
                txtMobile1.Text = dt.Rows[0]["MobileNumber"].ToString();
                txtMobile2.Text = dt.Rows[0]["MobileNumber2"].ToString();
                txtLandLine.Text = dt.Rows[0]["LandLineNumber"].ToString();
                txtEmailID.Text = dt.Rows[0]["GramPanchayatEmail"].ToString();
                txtBankACNo.Text = dt.Rows[0]["BankAccountNo"].ToString();
                txtBankHolder.Text = dt.Rows[0]["BankAccountHolder"].ToString();
                ddlBankName.SelectedValue = dt.Rows[0]["NameOFbank"].ToString();
                ddlBankName_SelectedIndexChanged(null, null);
                ddlBranchName.SelectedValue = dt.Rows[0]["BranchName"].ToString();
                ddlBranchName_SelectedIndexChanged(null, null);
                ddlIFSC.Text = dt.Rows[0]["IFSCCode"].ToString();

            }


        }

        //
        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            LiteralMsg.Text = "";
            if (Menu1.SelectedValue.Trim() == "0")
            {
                multiViewEmployee.ActiveViewIndex = 0;
            }
            else if (Menu1.SelectedValue.Trim() == "1")
            {
                multiViewEmployee.ActiveViewIndex = 1;
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
            }
            else if (Menu1.SelectedValue.Trim() == "2")
            {
                multiViewEmployee.ActiveViewIndex = 2;
            }
            else if (Menu1.SelectedValue.Trim() == "3")
            {
                multiViewEmployee.ActiveViewIndex = 3;
            }
            //else if (Menu1.SelectedValue.Trim() == "4")
            //{
            //    multiViewEmployee.ActiveViewIndex = 4;
            //}
        }

        #region"DDLs"


        private void FillDropDowns()
        {
            DataTable dt = Comcls.GetCityPocra("27");
            ddlDISTRICT.DataSource = dt;
            ddlDISTRICT.DataTextField = "Cityname";
            ddlDISTRICT.DataValueField = "City_ID";
            ddlDISTRICT.DataBind();
            ddlDISTRICT.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlDISTRICT.SelectedIndex = 0;


            ddlBankName.DataSource = Comcls.GetBank();
            ddlBankName.DataTextField = "NameOFbank";
            ddlBankName.DataValueField = "NameOFbank";
            ddlBankName.DataBind();
            ddlBankName.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlBankName.SelectedIndex = 0;






            //ddlCATEGORY.DataSource = Comcls.GetCostCategoryMaster();
            //ddlCATEGORY.DataTextField = "CategoryMaster";
            //ddlCATEGORY.DataValueField = "CategoryMasterID";
            //ddlCATEGORY.DataBind();
            //ddlCATEGORY.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlCATEGORY.SelectedIndex = 0;


            //////ddlGramPanchayatCode.DataSource = Comcls.GetVillage();
            //////ddlGramPanchayatCode.DataTextField = "VillageName";
            //////ddlGramPanchayatCode.DataValueField = "VillageID";
            //////ddlGramPanchayatCode.DataBind();
            //////ddlGramPanchayatCode.Items.Insert(0, new ListItem("--Select--", "0"));
            //////ddlGramPanchayatCode.SelectedIndex = 0;




        }




        protected void ddlDISTRICT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlVILLAGE.Items.Count > 0)
                ddlVILLAGE.Items.Clear();

            ddlTALUKA.DataSource = Comcls.GetTalukaMaster(ddlDISTRICT.SelectedValue.Trim());
            ddlTALUKA.DataTextField = "Taluka";
            ddlTALUKA.DataValueField = "TalukaID";
            ddlTALUKA.DataBind();
            ddlTALUKA.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlTALUKA.SelectedIndex = 0;

            ddlPOST.DataSource = Comcls.GetPostMaster(ddlDISTRICT.SelectedValue.Trim());
            ddlPOST.DataTextField = "PostName";
            ddlPOST.DataValueField = "Post_ID";
            ddlPOST.DataBind();
            ddlPOST.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlPOST.SelectedIndex = 0;



            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);

        }

        protected void ddlTALUKA_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlVILLAGE.DataSource = Comcls.GetVillage(ddlDISTRICT.SelectedValue.Trim(), ddlTALUKA.SelectedValue.Trim());
            ddlVILLAGE.DataTextField = "VillageName";
            ddlVILLAGE.DataValueField = "VillageID";
            ddlVILLAGE.DataBind();
            ddlVILLAGE.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlVILLAGE.SelectedIndex = 0;


            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
        }

        protected void ddlVILLAGE_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCLUSTARCODE.Text = "";
            txtCLUSTARCODE.Text = cla.GetExecuteScalar("SELECT   Tbl_M_ClustersMaster.Clusters FROM   dbo.Tbl_M_VillageMaster INNER JOIN  dbo.Tbl_M_ClustersMaster ON dbo.Tbl_M_VillageMaster.ClustersMasterID = dbo.Tbl_M_ClustersMaster.ClustersMasterID WHERE  (dbo.Tbl_M_VillageMaster.VillageID =" + ddlVILLAGE.SelectedValue.Trim() + ")");


            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
        }

        protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlBranchName.DataSource = Comcls.GetBankWiseBrach(ddlBankName.SelectedValue.Trim());
            ddlBranchName.DataTextField = "BranchName";
            ddlBranchName.DataValueField = "BranchName";
            ddlBranchName.DataBind();
            ddlBranchName.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlBranchName.SelectedIndex = 0;
        }

        protected void ddlBranchName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlIFSC.DataSource = Comcls.GetBankBranchWiseIFSC(ddlBankName.SelectedValue.Trim(), ddlBranchName.SelectedValue.Trim());
            ddlIFSC.DataTextField = "IFSCCode";
            ddlIFSC.DataValueField = "IFSCCode";
            ddlIFSC.DataBind();
            ddlIFSC.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlIFSC.SelectedIndex = 0;
        }

        protected void ddlPOST_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPostPin.Text = "";
            txtPostPin.Text = cla.GetExecuteScalar("SELECT  PinCode FROM Tbl_M_CityWisePost where Post_ID=" + ddlPOST.SelectedValue.Trim() + " and IsDeleted is null");


            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
        }
        #endregion



        #region "Save Registration"

        protected void btnSaveAAudhar_Click(object sender, EventArgs e)
        {
            LiteralMsg.Text = "";
            //------------validations ---------------------// 
            if (Comcls.RecordExistanceChk("Tbl_M_RegistrationDetails", "GramPanchayatCode", txtGramPanchayatCode.Text.Trim(), "RegistrationID", Convert.ToInt32(ViewState["RegistrationID"].ToString())) == false)
            {
                //clsMessages.Errormsg(LiteralMsg, "Gram Panchayat Code  " + txtGramPanchayatCode.Text.ToUpper() + "  is already registered");
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG19", Session["Lang"].ToString()), "error");
                return;
            }


            LiteralMsg.Text = "";

            //  if (ddlGramPanchayatCode.SelectedIndex == 0)
            if (txtGramPanchayatCode.Text.Trim().Length == 0)
            {
                // clsMessages.Warningmsg(LiteralMsg, "Please Fill GRAM PANCHAYAT CODE");
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG11", Session["Lang"].ToString()), "error");
                return;
            }
            else if (txtName.Text.Trim().Length == 0)
            {
                // clsMessages.Warningmsg(LiteralMsg, "Please Fill AAudhar Number");
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG12", Session["Lang"].ToString()), "error");
                return;
            }
            else if (txtAuthMobileNo.Text.Trim().Length == 0)
            {
                // clsMessages.Warningmsg(LiteralMsg, "Please Fill Mobile Number");
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG13", Session["Lang"].ToString()), "error");
                return;
            }
            //------------End validations ---------------------// 

            //List<String> lst = new List<string>();
            String str = "";


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


                    if (ViewState["RegistrationID"].ToString().Trim().Length > 0)
                    {
                        //UPDATE
                        str = " UPDATE  Tbl_M_RegistrationDetails SET";
                        str += " RegisterName='" + txtName.Text.Trim() + "',Gender='" + rdoGender.SelectedValue.Trim() + "',GramPanchayatMobile='" + txtAuthMobileNo.Text.Trim() + "',GramPanchayatEmail='" + txtAuthMobileEmail.Text.Trim() + "' ";
                        str += " WHERE (RegistrationID = " + ViewState["RegistrationID"].ToString().Trim() + ")";
                        cla.ExecuteCommand(str, command);

                    }

                    if (cla.GetExecuteScalar("Select ApprovalStatus from Tbl_M_RegistrationDetails WHERE RegistrationID = " + Session["RegistrationID"].ToString() + "   ", command).Trim() != "Back To Beneficiary")
                    {
                        str = " UPDATE Tbl_M_RegistrationDetails SET ApprovalStatus='Pending' WHERE RegistrationID = " + Session["RegistrationID"].ToString() + " ";
                        cla.ExecuteCommand(str, command);
                    }
                    transaction.Commit();
                    //clsMessages.Sucessmsg(LiteralMsg, "S");

                    FillDetails();
                    //clsMessages.Sucessmsg(LiteralMsg, "U");
                    Util.ShowMessageBox(this.Page, "Success", MyCommanClass.GetMsgInEnForDB("MSG17", Session["Lang"].ToString()), "success");
                    Menu1.Items[1].Enabled = true;
                    multiViewEmployee.ActiveViewIndex = 1;




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
                    //clsMessages.Errormsg(LiteralMsg, "Please provide correct input." + ex.Message.Trim());
                    Util.ShowMessageBox(this.Page, "Error", ex.Message.Trim(), "error");
                }
                finally
                {
                    if (connection != null) { connection.Close(); }
                    command.Dispose();
                }

            }




        }

        #endregion

        #region "Save Basic"
        protected void btnBasic_Click(object sender, EventArgs e)
        {
            LiteralMsg.Text = "";
            //------------validations ---------------------// 



            //------------End validations ---------------------// 


            String str = "", Work_CircleID = "", Work_ClustersMasterID = "";
            Work_ClustersMasterID = cla.GetExecuteScalar("Select ClustersMasterID from Tbl_M_VillageMaster where VillageID=" + ddlVILLAGE.SelectedValue.Trim() + "");
            Work_CircleID = cla.GetExecuteScalar("Select CircleID from Tbl_M_VillageMaster where VillageID=" + ddlVILLAGE.SelectedValue.Trim() + "");

            LiteralMsg.Text = "";
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


                    if (ViewState["RegistrationID"].ToString().Trim().Length > 0)
                    {


                        // UPDATE
                        str = " UPDATE  Tbl_M_RegistrationDetails SET  MobileNumber ='" + txtMobile1.Text.Trim() + "', ";
                        str += " MobileNumber2 ='" + txtMobile2.Text.Trim() + "', LandLineNumber ='" + txtLandLine.Text.Trim() + "', EmailID ='" + txtEmailID.Text.Trim() + "',  ";
                        str += " Address1HouseNo ='" + txtHouseNo.Text.Trim() + "', Address1StreetName ='" + txtStreetNo.Text.Trim() + "', Address1City_ID =" + ddlDISTRICT.SelectedValue.Trim() + ", Address1TalukaID =" + ddlTALUKA.SelectedValue.Trim() + ", Address1Post_ID =" + ddlPOST.SelectedValue.Trim() + ", ";
                        str += " Address1VillageID =" + ddlVILLAGE.SelectedValue.Trim() + ", Address1PinCode ='" + txtPostPin.Text.Trim() + "', IsBothAddressSame ='0' ";

                        str += " , Work_City_ID=" + ddlDISTRICT.SelectedValue.Trim() + ", Work_TalukaID=" + ddlTALUKA.SelectedValue.Trim() + ", Work_VillageID=" + ddlVILLAGE.SelectedValue.Trim() + " ";
                        if (Work_ClustersMasterID.Trim().Length > 0)
                        {
                            str += " , Work_ClustersMasterID=" + Work_ClustersMasterID.Trim() + "";
                        }
                        if (Work_CircleID.Trim().Length > 0)
                        {
                            str += " , Work_CircleID=" + Work_CircleID.Trim() + "";
                        }

                        str += " WHERE (RegistrationID = " + ViewState["RegistrationID"].ToString().Trim() + ")";
                        cla.ExecuteCommand(str, command);

                        txtName.ReadOnly = true;
                    }

                    if (cla.GetExecuteScalar("Select ApprovalStatus from Tbl_M_RegistrationDetails WHERE RegistrationID = " + Session["RegistrationID"].ToString() + "   ", command).Trim() != "Back To Beneficiary")
                    {
                        str = " UPDATE Tbl_M_RegistrationDetails SET ApprovalStatus='Pending' WHERE RegistrationID = " + Session["RegistrationID"].ToString() + " ";
                        cla.ExecuteCommand(str, command);
                    }
                    transaction.Commit();
                    FillDetails();
                    //clsMessages.Sucessmsg(LiteralMsg, "U");
                    Util.ShowMessageBox(this.Page, "Success", MyCommanClass.GetMsgInEnForDB("MSG17", Session["Lang"].ToString()), "success");
                    Menu1.Items[2].Enabled = true;
                    multiViewEmployee.ActiveViewIndex = 2;
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
                    //clsMessages.Errormsg(LiteralMsg, "Please provide correct input." + ex.Message.Trim());
                    Util.ShowMessageBox(this.Page, "Error", ex.Message.Trim(), "error");
                }
                finally
                {
                    if (connection != null) { connection.Close(); }
                    command.Dispose();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                }

            }

        }
        #endregion

        #region " Save Bank"

        protected void btnLandNext_Click(object sender, EventArgs e)
        {

            LiteralMsg.Text = "";
            //------------validations ---------------------// 
            String RBIBankID = "";
            RBIBankID = cla.GetExecuteScalar("SELECT RBIBankID FROM Tbl_M_RBIBankMaster where NameOFbank='" + ddlBankName.SelectedItem.Text.Trim() + "' and IFSCCode='" + ddlIFSC.SelectedItem.Text.Trim() + "'");
            if (RBIBankID.Length == 0)
            {
                //  clsMessages.Warningmsg(LiteralMsg, "Please Select Bank");
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG14", Session["Lang"].ToString()), "error");
                return;
            }

            //------------End validations ---------------------// 

            LiteralMsg.Text = "";
            String str = "";

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


                    if (ViewState["RegistrationID"].ToString().Trim().Length > 0)
                    {


                        // UPDATE
                        str = " UPDATE  Tbl_M_RegistrationDetails SET  BankAccountNo ='" + txtBankACNo.Text.Trim() + "', BankAccountHolder ='" + txtBankHolder.Text.Trim() + "', RBIBankID =" + RBIBankID + "  ";
                        str += " WHERE (RegistrationID = " + ViewState["RegistrationID"].ToString().Trim() + ")";
                        cla.ExecuteCommand(str, command);

                        txtName.ReadOnly = true;
                    }

                    if (cla.GetExecuteScalar("Select ApprovalStatus from Tbl_M_RegistrationDetails WHERE RegistrationID = " + Session["RegistrationID"].ToString() + "   ", command).Trim() != "Back To Beneficiary")
                    {
                        str = " UPDATE Tbl_M_RegistrationDetails SET ApprovalStatus='Pending' WHERE RegistrationID = " + Session["RegistrationID"].ToString() + " ";
                        cla.ExecuteCommand(str, command);
                    }
                    transaction.Commit();
                    FillDetails();
                    //clsMessages.Sucessmsg(LiteralMsg, "U");
                    Util.ShowMessageBox(this.Page, "Success", MyCommanClass.GetMsgInEnForDB("MSG17", Session["Lang"].ToString()), "success");
                    Menu1.Items[3].Enabled = true;
                    multiViewEmployee.ActiveViewIndex = 3;
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
                    //  clsMessages.Errormsg(LiteralMsg, "Please provide correct input." + ex.Message.Trim());
                    Util.ShowMessageBox(this.Page, "Error", ex.Message.Trim(), "error");
                }
                finally
                {
                    if (connection != null) { connection.Close(); }
                    command.Dispose();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                }

            }
        }

        #endregion

        #region "Save Declretion"

        protected void btnFinalSave_Click(object sender, EventArgs e)
        {
            LiteralMsg.Text = "";
            //------------validations ---------------------// 
            Boolean IsAcept = true;
            LiteralMsg.Text = "";
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

            if (IsAcept == false)
            {
                //clsMessages.Warningmsg(LiteralMsg, "Please accept all Declarations to register under Nanaji Deshmukh Krishi Sanjivani Prakalp");
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG9", Session["Lang"].ToString()), "error");
                return;
            }


            //------------End validations ---------------------// 


            String str = "";
            int UserId = cla.TableID("Tbl_M_LoginDetails", "UserId");

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


                    if (ViewState["RegistrationID"].ToString().Trim().Length > 0)
                    {
                        // UPDATE

                        str = " UPDATE  Tbl_M_RegistrationDetails SET   IAgree ='1' , IsDeleted=NULL ,UserID='" + txtGramPanchayatCode.Text.Trim() + "' ";
                        str += " WHERE(RegistrationID = " + ViewState["RegistrationID"].ToString().Trim() + ")";
                        cla.ExecuteCommand(str, command);

                    }

                    if (cla.GetExecuteScalar("Select ApprovalStatus from Tbl_M_RegistrationDetails WHERE RegistrationID = " + Session["RegistrationID"].ToString() + "   ", command).Trim() != "Back To Beneficiary")
                    {
                        str = " UPDATE Tbl_M_RegistrationDetails SET ApprovalStatus='Pending' WHERE RegistrationID = " + Session["RegistrationID"].ToString() + " ";
                        cla.ExecuteCommand(str, command);
                    }
                    transaction.Commit();
                    //clsMessages.Sucessmsg(LiteralMsg, "U");
                    Util.ShowMessageBox(this.Page, "Success", MyCommanClass.GetMsgInEnForDB("MSG17", Session["Lang"].ToString()), "success");
                    FillDetails();
                    //Session["RegistrationIDPass"] = ViewState["RegistrationID"].ToString().Trim();
                    //Response.Redirect("RegisterSucess.aspx?" + EncryptDecryptQueryString.encrypt(string.Format("ID={0}", ViewState["RegistrationID"].ToString().Trim())), true);
                    //Response.Redirect("RegisterSucess.aspx?T=2", false);
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
                    //clsMessages.Errormsg(LiteralMsg, "Please provide correct input." + ex.Message.Trim());
                    Util.ShowMessageBox(this.Page, "Error", ex.Message.Trim(), "error");
                }
                finally
                {
                    if (connection != null) { connection.Close(); }
                    command.Dispose();
                }

            }

        }



        #endregion


    }
}