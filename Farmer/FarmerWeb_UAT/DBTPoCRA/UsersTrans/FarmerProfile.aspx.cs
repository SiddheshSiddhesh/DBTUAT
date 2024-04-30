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
    public partial class FarmerProfile : System.Web.UI.Page
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
                    ViewState["RegistrationID"] = Request.QueryString["ID"].ToString();
                    divFarmer.Visible = false;
                    ddlApplicationStatus.DataSource = Comcls.GetApplicationStatus("R");
                    ddlApplicationStatus.DataTextField = "ApplicationStatus";
                    ddlApplicationStatus.DataValueField = "ApplicationStatusID";
                    ddlApplicationStatus.DataBind();
                    ddlApplicationStatus.Items.Insert(0, new ListItem("--Select--", "0"));

                    LiteralLandless.Text = "Landless Farmer Certificate";
                    if (Request.QueryString["T"].ToString().Trim() == "RM")
                    {
                        ShowRecords(ViewState["RegistrationID"].ToString());
                        Literal5.Text = "FARMER REGISTRATION VERIFICATION";
                        divFarmer.Visible = true;
                    }
                    else if (Request.QueryString["T"].ToString().Trim() == "C")
                    {
                        //ShowRecordsComu(ViewState["RegistrationID"].ToString());
                        LiteralCom.Text = "<a class='btn btn-success' onclick=ShowDetails('CommunityProfile.aspx?T=RM&ID=" + ViewState["RegistrationID"].ToString().ToString().Trim() +"&TB_iframe=true&height=560&width=100')> Registration Details </a>"; ;
                        Literal5.Text = "Community REGISTRATION VERIFICATION".ToUpper();
                    }
                    if (Session["Lang"].ToString().Trim() == "mr-IN")
                    {
                        rm = new ResourceManager("Resources.UserManagement", System.Reflection.Assembly.Load("App_GlobalResources")); ci = Thread.CurrentThread.CurrentCulture;
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("mr-IN");
                        LoadString(Thread.CurrentThread.CurrentCulture);
                        LoadString(ci);
                        LiteralLandless.Text = "भूमिहीन शेतकरी प्रमाणपत्र";
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
                        LiteralLandless.Text = "भूमिहीन शेतकरी प्रमाणपत्र";
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Auto Correct if WorkVillage not match with Land Village
        /// </summary>
        /// <param name="RegistrationID"></param>
        private void UpdateWorkVillageAsPerUpdate(String RegistrationID)
        {
            DataTable dt = cla.GetDataTable("SELECT RegistrationID FROM Tbl_M_RegistrationDetails where RegistrationID="+ RegistrationID + " AND isdeleted is null  and Work_VillageID <> (Select top 1 VillageID from Tbl_M_RegistrationLand where RegistrationID=Tbl_M_RegistrationDetails.RegistrationID and IsDeleted is null and VillageID is not null AND (ParentLandID IS NULL) and VillageID<>0  order by LandID)");
            for (int x1 = 0; x1 != dt.Rows.Count; x1++)
            {
                
                DataTable dtLand = cla.GetDataTable("SELECT  TOP (1) L.VillageID, V.City_ID, V.ClustersMasterID, V.TalukaID, V.SubdivisionID FROM Tbl_M_RegistrationLand AS L INNER JOIN Tbl_M_VillageMaster AS V ON V.VillageID = L.VillageID WHERE (L.RegistrationID = " + RegistrationID.Trim() + ") AND (L.ParentLandID IS NULL) and L.isdeleted is null and L.VillageID <> 0 ORDER BY L.LandID");
                if (dtLand.Rows.Count > 0)
                {
                    for (int x = 0; x != dtLand.Rows.Count; x++)
                    {
                        String str = " UPDATE  Tbl_M_RegistrationDetails SET Work_City_ID=" + dtLand.Rows[x]["City_ID"].ToString() + ", Work_TalukaID=" + dtLand.Rows[x]["TalukaID"].ToString() + ", Work_VillageID=" + dtLand.Rows[x]["VillageID"].ToString() + " ";
                        if (dtLand.Rows[x]["ClustersMasterID"].ToString().Trim().Length > 0)
                        {
                            str += " , Work_ClustersMasterID=" + dtLand.Rows[x]["ClustersMasterID"].ToString() + "";
                        }
                        if (dtLand.Rows[x]["SubdivisionID"].ToString().Trim().Length > 0)
                        {
                            str += " , Work_SubdivisionID=" + dtLand.Rows[x]["SubdivisionID"].ToString().Trim() + "";
                        }
                        str += " WHERE (RegistrationID = " + RegistrationID.Trim() + ")";
                        cla.ExecuteCommand(str);
                    }
                }

            }
        }
        private void LoadString(CultureInfo ci)
        {
            Literal6.Text = rm.GetString("VERIFICATION", ci);
            Literal7.Text = rm.GetString("Application_Status", ci);
            Literal8.Text = rm.GetString("Date", ci);
            Literal9.Text = rm.GetString("Current_Status", ci);
            Literal10.Text = rm.GetString("Reason", ci);
            Literal11.Text = rm.GetString("Remark_if_any", ci);
            btnUpdate.Text = rm.GetString("Update", ci);

            Literal12.Text = rm.GetString("BASIC_DETAILS", ci);
            Literal2.Text = rm.GetString("AADHAR_NUMBER", ci);
            Literal4.Text = rm.GetString("FARMER_NAME", ci);
            Literal13.Text = rm.GetString("GENDER", ci);
            Literal3.Text = rm.GetString("DATE_OF_BIRTH", ci);
            Literal14.Text = rm.GetString("AUTHORISED_PERSON_MOBILE", ci);
            Literal15.Text = rm.GetString("AUTHORISED_PERSON_EMAILID", ci);
            Literal16.Text = rm.GetString("CATEGORY", ci);

            Literal17.Text = rm.GetString("ADDRESS_AND_OTHER_DETAILS", ci);
            Literal18.Text = rm.GetString("HOUSE_NO", ci);
            Literal19.Text = rm.GetString("STREET_NO", ci);
            Literal20.Text = rm.GetString("DISTRICT", ci);
            Literal21.Text = rm.GetString("TALUKA", ci);
            Literal22.Text = rm.GetString("POST", ci);
            Literal23.Text = rm.GetString("PIN_CODE", ci);
            Literal24.Text = rm.GetString("VILLAGE", ci);
            Literal25.Text = rm.GetString("CLUSTER_CODE", ci);
            Literal26.Text = rm.GetString("MOBILE1", ci);
            Literal27.Text = rm.GetString("MOBILE2", ci);
            Literal28.Text = rm.GetString("LANDLINE_NO", ci);
            Literal29.Text = rm.GetString("EMAIL_ID", ci);
            Literal30.Text = rm.GetString("PAN_NO", ci);
            Literal31.Text = rm.GetString("PHYSICALLY_HANDICAP", ci);
            Literal32.Text = rm.GetString("DISABILITY_PERCENTAGE", ci);

            Literal33.Text = rm.GetString("LAND_DETAILS", ci);
            Literal34.Text = rm.GetString("LAND_STATUS", ci);

            Literal35.Text = rm.GetString("VERIFICATION", ci);
            Literal36.Text = rm.GetString("VERIFICATION_LOG", ci);
        }

        private void ShowRecords(String RegistrationID)
        {
            List<String> lst = new List<String>();
            lst.Add(RegistrationID);
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("1");
            dt = new DataTable();
            dt = cla.GetDtByProcedure("SP_FormarRegistration_Details", lst);

            if (dt.Rows.Count > 0)
            {
                lblAadharNu.Text = dt.Rows[0]["AaDharNumber"].ToString();
                lblCategy.Text = dt.Rows[0]["CategoryMaster"].ToString();
                if (dt.Rows[0]["CastCategoryDoc"].ToString().Length > 0)
                    lblCertificate.Text = "<a target=_blank  href='" + clsSettings.BaseUrl + "" + dt.Rows[0]["CastCategoryDoc"].ToString() + "'> View Certificate </a>";
                lblCluster.Text = dt.Rows[0]["Clusters"].ToString();
                lblDisabilityPer.Text = dt.Rows[0]["DisabilityPer"].ToString();
                lblDistrict.Text = dt.Rows[0]["Cityname"].ToString();
                lblDOB.Text = dt.Rows[0]["DateOfBirth"].ToString();
                lblEmail.Text = dt.Rows[0]["EmailID"].ToString();
                lblGENDER.Text = dt.Rows[0]["Gender"].ToString();
                lblHANDICAP.Text = dt.Rows[0]["PhysicallyHandicap"].ToString();
                if (dt.Rows[0]["PhysicallyHandicapDoc"].ToString().Length > 0)
                    lblHandiChetificate.Text = "<a target=_blank  href='" + clsSettings.BaseUrl + "" + dt.Rows[0]["PhysicallyHandicapDoc"].ToString() + "'> View Certificate </a>";// dt.Rows[0]["PhysicallyHandicapDoc"].ToString();
                lblHouseNo.Text = dt.Rows[0]["Address1HouseNo"].ToString();
                lblLandLine.Text = dt.Rows[0]["LandLineNumber"].ToString();
                lblLandStatus.Text = dt.Rows[0]["LandStatus"].ToString();
                lblMobile.Text = dt.Rows[0]["MobileNumber"].ToString();
                lblMobile2.Text = dt.Rows[0]["MobileNumber2"].ToString();
                lblName.Text = dt.Rows[0]["RegisterName"].ToString();
                lblPanNo.Text = dt.Rows[0]["PanNumber"].ToString();
                lblPinCode.Text = dt.Rows[0]["Address1PinCode"].ToString();
                lblPost.Text = dt.Rows[0]["PostName"].ToString();
                lblStreetNo.Text = dt.Rows[0]["Address1StreetName"].ToString();
                lblTaluka.Text = dt.Rows[0]["Taluka"].ToString();
                lblVillage.Text = dt.Rows[0]["VillageName"].ToString();

                Literal1.Text = dt.Rows[0]["ApprovalStatus"].ToString();

                if (dt.Rows[0]["LandLessCertificate"].ToString().Trim().Length > 0)
                    Label4.Text = "<a target=_blank  href='" + clsSettings.BaseUrl + "" + dt.Rows[0]["LandLessCertificate"].ToString() + "'> View Certificate </a>";
                else
                {
                    Label4.Text = "Not found";

                }

                lst.Clear();
                lst.Add(RegistrationID);
                dt = cla.GetDtByProcedure("SP_GetLandParentDetails", lst);
                grdSubject.DataSource = dt;
                grdSubject.DataBind();
            }

            lst.Clear();
            lst.Add(RegistrationID);
            dt = new DataTable();
            dt = cla.GetDtByProcedure("SP_FormarRegistration_Log", lst);
            grdLog.DataSource = dt;
            grdLog.DataBind();

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CheckUpdate();  </script>", false);
        }



        private void ShowRecordsComu(String RegistrationID)
        {

            Literal2.Text = "GRAM PANCHAYAT CODE";
            Literal4.Text = "AUTHORISED PERSON NAME";
            Literal3.Text = "";
            TrCat.Visible = false;
            grdSubject.Visible = false;
            divLand.Visible = false;
            ulFarmer.Visible = false;
            ulCom.Visible = true;
            TrComOnly.Visible = true;
            rdDis.Visible = false;

            List<String> lst = new List<String>();
            lst.Add(RegistrationID);
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            dt = new DataTable();
            dt = cla.GetDtByProcedure("SP_CommunityRegistration_Details", lst);

            if (dt.Rows.Count > 0)
            {
                lblAadharNu.Text = dt.Rows[0]["GramPanchayatCode"].ToString();

                //lblCategy.Text = dt.Rows[0]["CategoryMaster"].ToString();
                //if (dt.Rows[0]["CastCategoryDoc"].ToString().Length > 0)
                //    lblCertificate.Text = "<a target=_blank  href='" + dt.Rows[0]["CastCategoryDoc"].ToString() + "'> View Certificate </a>";
                Label2.Text = dt.Rows[0]["GramPanchayatMobile"].ToString();
                Label3.Text = dt.Rows[0]["GramPanchayatEmail"].ToString();

                lblCluster.Text = dt.Rows[0]["Clusters"].ToString();
                //lblDisabilityPer.Text = dt.Rows[0]["DisabilityPer"].ToString();
                lblDistrict.Text = dt.Rows[0]["Cityname"].ToString();
                //lblDOB.Text = dt.Rows[0]["DateOfBirth"].ToString();
                lblEmail.Text = dt.Rows[0]["EmailID"].ToString();
                lblGENDER.Text = dt.Rows[0]["Gender"].ToString();
                //lblHANDICAP.Text = dt.Rows[0]["PhysicallyHandicap"].ToString();
                //if (dt.Rows[0]["PhysicallyHandicapDoc"].ToString().Length > 0)
                //    lblHandiChetificate.Text = "<a target=_blank  href='" + dt.Rows[0]["PhysicallyHandicapDoc"].ToString() + "'> View Certificate </a>";// dt.Rows[0]["PhysicallyHandicapDoc"].ToString();
                lblHouseNo.Text = dt.Rows[0]["Address1HouseNo"].ToString();
                lblLandLine.Text = dt.Rows[0]["LandLineNumber"].ToString();
                //lblLandStatus.Text = dt.Rows[0]["LandStatus"].ToString();
                lblMobile.Text = dt.Rows[0]["MobileNumber"].ToString();
                lblMobile2.Text = dt.Rows[0]["MobileNumber2"].ToString();
                lblName.Text = dt.Rows[0]["RegisterName"].ToString();
                lblPanNo.Text = dt.Rows[0]["PanNumber"].ToString();
                lblPinCode.Text = dt.Rows[0]["Address1PinCode"].ToString();
                lblPost.Text = dt.Rows[0]["PostName"].ToString();
                lblStreetNo.Text = dt.Rows[0]["Address1StreetName"].ToString();
                lblTaluka.Text = dt.Rows[0]["Taluka"].ToString();
                lblVillage.Text = dt.Rows[0]["VillageName"].ToString();

                Literal1.Text = dt.Rows[0]["ApprovalStatus"].ToString();




            }

            lst.Clear();
            lst.Add(RegistrationID);
            dt = new DataTable();
            dt = cla.GetDtByProcedure("SP_FormarRegistration_Log", lst);
            grdLog.DataSource = dt;
            grdLog.DataBind();

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CheckUpdate();  </script>", false);
        }




        protected void grdSubject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string ParentLandID = grdSubject.DataKeys[e.Row.RowIndex]["LandID"].ToString();
                String RegistrationID = grdSubject.DataKeys[e.Row.RowIndex]["RegistrationID"].ToString();
                GridView SC = (GridView)e.Row.FindControl("grdChild");


                DataTable dt = cla.GetDataTable("SELECT   SurveyNo712, Hectare712, Are712,  '<a target=_blank href=''https://dbtpocradata.blob.core.windows.net'+Extracts712Doc+'''> View Extracts  </a>' as Extracts712Doc FROM  Tbl_M_RegistrationLand WHERE  (ParentLandID = " + ParentLandID + ") AND RegistrationID="+ RegistrationID + " AND (IsDeleted IS NULL)");
                SC.DataSource = dt;
                SC.DataBind();
            }
        }

        protected void ddlApplicationStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

            chkReasons.DataSource = Comcls.GetReasonVerifecation("1", ddlApplicationStatus.SelectedValue.Trim());
            chkReasons.DataTextField = "Reasons";
            chkReasons.DataValueField = "ReasonID";
            chkReasons.DataBind();

            divReg.Visible = true;
            if (ddlApplicationStatus.SelectedValue.Trim() == "7")
            {
                divReg.Visible = false;
            }

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CheckUpdate();  </script>", false);
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

            //------------End validations ---------------------// 


            String str = "";
            int RegistrationLogID = cla.TableID("Tbl_M_Registration_Log", "RegistrationLogID");
            int RegistrationLogReasonID = cla.TableID("Tbl_M_Registration_LogReason", "RegistrationLogReasonID");

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
                    str = " INSERT INTO Tbl_M_Registration_Log (RegistrationLogID, RegistrationID, LogDetails, RegistrationStatus, UpdateOnDate, UpdateByRegID)";
                    str += " VALUES(" + RegistrationLogID + "," + ViewState["RegistrationID"].ToString() + ",'" + txtResion.Text.Trim().Replace("'","") + "','" + ddlApplicationStatus.SelectedItem.Text.Trim() + "','" + cla.mdy(txtDate.Text.Trim()) + "'," + Session["UserId"].ToString() + ")";
                    cla.ExecuteCommand(str, command);

                    for (int x = 0; x != chkReasons.Items.Count; x++)
                    {
                        if (chkReasons.Items[x].Selected)
                        {
                            str = " INSERT INTO Tbl_M_Registration_LogReason (RegistrationLogReasonID, RegistrationLogID, ReasonID)";
                            str += " VALUES(" + RegistrationLogReasonID + "," + RegistrationLogID + "," + chkReasons.Items[x].Value.Trim() + ")";
                            cla.ExecuteCommand(str, command);
                            RegistrationLogReasonID++;
                        }
                    }

                    str = " UPDATE Tbl_M_RegistrationDetails SET ApprovalStatus='" + ddlApplicationStatus.SelectedItem.Text + "'  WHERE RegistrationID=" + ViewState["RegistrationID"].ToString() + "";
                    cla.ExecuteCommand(str, command);

                    transaction.Commit();
                    clsMessages.Sucessmsg(LiteralMsg, "U");
                    String Mob = lblMobile.Text;
                    String RegNo = cla.GetExecuteScalar("SELECT   RegistrationNo  FROM Tbl_M_RegistrationDetails where RegistrationID=" + ViewState["RegistrationID"].ToString() + "");
                    SMS.SendSmsOnRegistrationStatusChanged(Mob, ddlApplicationStatus.SelectedItem.Text, RegNo);
                    Literal1.Text = ddlApplicationStatus.SelectedItem.Text;
                    //ClientScript.RegisterStartupScript(this.GetType(), "CallParent", "callParentClick();", true);
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
