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
using DBTPoCRA.APPData;
using System.Net;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace DBTPoCRA.Registration
{
    public partial class CommunityRegistration : System.Web.UI.Page
    {
        ResourceManager rm;

        DataTable dt = new DataTable();
        #region"Declarection"
        MyClass cla = new MyClass();
        MyCommanClass Comcls = new MyCommanClass();
        AzureBlobHelper fileRet = new AzureBlobHelper();


        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {



                if (clsSettings.IsRegistrationStart == false)
                {
                    Response.Redirect("~/Default.aspx", true);
                }


                if (!IsPostBack)
                {

                    ViewState["SanctionedVDP"] = "";
                    FillDropDowns();
                    ViewState["IsFromMLP"] = "NO";
                    //Session["Lang"] = Request.QueryString["LAN"].ToString();
                    ViewState["RegistrationID"] = "";
                    cla.ExecuteCommand("update Tbl_T_ApplicationDetails set ProposedAmount=0 where ProposedAmount is null and GatNo is not null ");
                    if (Request.QueryString.Count > 0)
                    {


                        String sRgid = Convert.ToString(Request["ID"]);
                        if (!string.IsNullOrEmpty(sRgid))
                        {

                            ViewState["RegistrationID"] = Request.QueryString["ID"].ToString().Trim();
                            String VIllageID = cla.GetExecuteScalar(" Select VillageID from Tbl_M_RegistrationDetails where RegistrationID=" + ViewState["RegistrationID"].ToString().Trim() + " ");
                            ddlGramPanchayatCode.SelectedValue = VIllageID;
                            ddlGramPanchayatCode_SelectedIndexChanged(null, null);
                            ddlGramPanchayatCode.Enabled = false;
                            btnUpdate.Visible = true;
                            btnSaveAAudhar.Visible = false;
                        }


                    }

                    Page.Form.Attributes.Add("enctype", "multipart/form-data");


                }

            }
            catch (Exception ex)
            {


            }
        }



        #region"DDLs"


        private void FillDropDowns()
        {

            // ddlGramPanchayatCode.DataSource = Comcls.GetPocraVillageAsPerWorkArea();

            String sRgidm = Convert.ToString(Request["ID"]);

            if (!String.IsNullOrEmpty(sRgidm))
            {
                ddlGramPanchayatCode.DataSource = Comcls.GetPocraVillageAsPerWorkArea();

            }
            else
            {
                ddlGramPanchayatCode.DataSource = Comcls.GetPocraVillageAsPerWorkAreaCommunity();
            }
            ddlGramPanchayatCode.DataTextField = "VillageName";
            ddlGramPanchayatCode.DataValueField = "VillageID";
            ddlGramPanchayatCode.DataBind();
            ddlGramPanchayatCode.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlGramPanchayatCode.SelectedIndex = 0;

            DataTable dt = cla.GetDataTable("SELECT WM.SoilAndWaterRetentionWorksID ,WM.SoilAndWaterRetentionWorksGroup ,WM.SoilAndWaterRetentionWorksMr +' - '+A.ActivityCode as SoilAndWaterRetentionWorksMr ,WM.UnitofMess,'' FYearPhysical ,'' FYearAmount  ,''SYearPhysical ,''SYearAmount ,''TYearPhysical ,''TYearAmount  ,''TotalPhysical ,''TotalAmount ,WM.ActivityID, ''TotalPysicalNumber,WM.UnitofMessId ,A.ActivityCode  FROM Tbl_M_SoilAndWaterRetentionWorks as  WM inner join Tbl_M_ActivityMaster A on A.ActivityID=WM.ActivityID  where WM.IsDeleted is null order by WM.OrderbyNo,WM.SoilAndWaterRetentionWorksGroup desc");
            GrdWorkDetails.DataSource = dt;
            GrdWorkDetails.DataBind();


        }
        #endregion

        //#region communtyEdit
        //private void FillDropDownsEdit()
        //{

        //    //ddlGramPanchayatCode.DataSource = Comcls.GetPocraVillageAsPerWorkAreaCommunity();
        //     ddlGramPanchayatCode.DataSource = Comcls.GetPocraVillageAsPerWorkArea();
        //    ddlGramPanchayatCode.DataTextField = "VillageName";
        //    ddlGramPanchayatCode.DataValueField = "VillageID";
        //    ddlGramPanchayatCode.DataBind();
        //    ddlGramPanchayatCode.Items.Insert(0, new ListItem("--Select--", "0"));
        //    ddlGramPanchayatCode.SelectedIndex = 0;

        //    DataTable dt = cla.GetDataTable("SELECT WM.SoilAndWaterRetentionWorksID ,WM.SoilAndWaterRetentionWorksGroup ,WM.SoilAndWaterRetentionWorksMr ,WM.UnitofMess,'' FYearPhysical ,'' FYearAmount  ,''SYearPhysical ,''SYearAmount ,''TYearPhysical ,''TYearAmount  ,''TotalPhysical ,''TotalAmount FROM Tbl_M_SoilAndWaterRetentionWorks as  WM  where WM.IsDeleted is null ");
        //    GrdWorkDetails.DataSource = dt;
        //    GrdWorkDetails.DataBind();



        //}
        //#endregion



        #region "Save Registration"

        protected void btnSaveAAudhar_Click(object sender, EventArgs e)
        {
            LiteralMsg.Text = "";
            //------------validations ---------------------// 
            Double Total1 = 0, Total2 = 0, Total3 = 0, Total = 0;

            Total1 = Convert.ToDouble(txtForestrybasedfarmingPractices.Text) + Convert.ToDouble(txtTotalorchardPlanting.Text) + Convert.ToDouble(txtTotalalkalinelandmanagement.Text) + Convert.ToDouble(txtTotalprotectedfarming.Text) + Convert.ToDouble(txtTotalIntegratedfarming.Text) + Convert.ToDouble(ImprovingoverallSoilhealth.Text);
            Total2 = Convert.ToDouble(txtEfficientAndsustainableuseoftotalwater.Text) + Convert.ToDouble(txtWaterStorageAtthebase.Text) + Convert.ToDouble(txtFineIrrigation.Text) + Convert.ToDouble(txtAvailabilitywaterForProtectedIrrigation.Text);
            Total3 = Convert.ToDouble(txtCreatingInfrastructure.Text) + Convert.ToDouble(txtAgriculturalEquipmentCenter.Text) + Convert.ToDouble(txtClimatefriendlyvarieties.Text) + Convert.ToDouble(txtTotalSeedHubInfrastructure.Text);


            Total = Total1 + Total2 + Total3;
            txtCom_VillageDevelopmentPlan.Text = Total.ToString("N2");

            String For21ProjectReport = Total1.ToString("N2"), UseOfWaterProjectReport = Total2.ToString("N2"), CFCReport = Total3.ToString("N2");

            if (Comcls.RecordExistanceChk("Tbl_M_RegistrationDetails", "GramPanchayatCode", ddlGramPanchayatCode.SelectedItem.Text, "RegistrationID") == false)
            {

                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG16", Session["Lang"].ToString()), "error");
                return;
            }


            String phase = cla.GetExecuteScalar("Select Phase from Tbl_M_VillageMaster where VillageID=" + ddlGramPanchayatCode.SelectedValue.Trim() + "");
            if (phase.Trim().Length == 0) phase = "0";
            if (Convert.ToInt32(phase) != 1)
            {
                if (ViewState["IsFromMLP"].ToString().Trim() != "YES")
                {
                    Util.ShowMessageBox(this.Page, "Error", "Please click Get Data From MLP APP button to Update the data. ", "error");
                    return;
                }
            }


            //String BeneficiaryTypesID = "2";
            String BeneficiaryTypesID = "99";
            LiteralMsg.Text = "";
            //------------validations ---------------------// 
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
            if (ddlGramPanchayatCode.SelectedIndex == 0)
            {

                Util.ShowMessageBox(this.Page, "Required", MyCommanClass.GetMsgInEnForDB("MSG11", Session["Lang"].ToString()), "error");
                return;
            }
            //else if (ddlVCRMC.SelectedIndex == 0)
            //{

            //    Util.ShowMessageBox(this.Page, "Required", MyCommanClass.GetMsgInEnForDB("MSG12", Session["Lang"].ToString()), "error");
            //    return;
            //}
            //else if (txtAuthMobileNo.Text.Trim().Length == 0)
            //{

            //    Util.ShowMessageBox(this.Page, "Required", MyCommanClass.GetMsgInEnForDB("MSG13", Session["Lang"].ToString()), "error");
            //    return;
            //}


            //String str = "", SanctionedVDP = "";//, PathUp = "", fileName = "";
            DataTable dt = cla.GetDataTable("  Select City_ID,TalukaID,ClustersMasterID,VillageID,SubdivisionID from Tbl_M_VillageMaster where VillageID=" + ddlGramPanchayatCode.SelectedValue.Trim() + "");


            //if (FileUpload1.HasFile == true)
            //{
            //    string FileError = MyCommanClass.CheckAllowedFileExtension(System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName.ToString()));
            //    if (FileError.Length > 0)
            //    {
            //        Util.ShowMessageBox(this.Page, "Error", FileError, "error");

            //        return;
            //    }
            //    FileError = Util.CheckAllowedFileName(FileUpload1);
            //    if (FileError.Length > 0)
            //    {
            //        Util.ShowMessageBox(this.Page, "Error", FileError, "error");

            //        return;
            //    }


            //}
            if (ViewState["SanctionedVDP"].ToString().Length == 0)
            {
                Util.ShowMessageBox(this.Page, "Error", "Required ! मंजूर व्हीडीपी / सविस्तर प्रकल्प अहवाल अपलोड करण्याची पीडीएफ प्रत अपलोड करा  ", "error");

                return;
            }


            //------------End validations ---------------------// 









            int RegistrationID = cla.TableID("Tbl_M_RegistrationDetails", "RegistrationID");


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

                    //PathUp = "DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "";
                    //SanctionedVDP = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "/SanctionedVDP" + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName.ToString());
                    //fileName = "SanctionedVDP" + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName.ToString());

                    // ADD NEW 
                    String str = " INSERT INTO Tbl_M_RegistrationDetails (RegistrationID, BeneficiaryTypesID, GramPanchayatCode, VillageID , RegisterName,IAgree ";
                    str += " , Com_PeriodofMicroplanningFrom ,Com_PeriodofMicroplanningTo ,Com_ProjectReportDate ,Com_DCCApprovalDate ,Com_For21ProjectReport ,Com_UseOfWaterProjectReport ,Com_CFCReport  ,Com_VillageDevelopmentPlan ,Com_UpscaleTCM ,Com_PreventionTCM ,Com_BalanceExclusionTCM ,Com_Troubleshoot,SanctionedVDP,eTerndingBy ";
                    str += " ,  ForestrybasedfarmingPractices ,TotalorchardPlanting ,Totalalkalinelandmanagement ,Totalprotectedfarming ,TotalIntegratedfarming ,ImprovingoverallSoilhealth ";
                    str += " ,  EfficientAndsustainableuseoftotalwater ,WaterStorageAtthebase  ,FineIrrigation  ,AvailabilitywaterForProtectedIrrigation ";
                    str += " ,  CreatingInfrastructure,AgriculturalEquipmentCenter,Climatefriendlyvarieties,TotalSeedHubInfrastructure ";
                    str += " , Work_City_ID,Work_TalukaID,Work_SubdivisionID,Work_ClustersMasterID,Work_VillageID,ApplicationStatusID,ApprovalStageID,IsFromMLP ) ";

                    str += " VALUES (" + RegistrationID + "," + BeneficiaryTypesID + ",'" + ddlGramPanchayatCode.SelectedItem.Text.Trim().ToUpper() + "'," + ddlGramPanchayatCode.SelectedValue.Trim() + ",N'" + ddlVCRMC.SelectedItem.Text.Trim() + "','1'";
                    str += "  ,'" + cla.mdy(txtCom_PeriodofMicroplanningFrom.Text) + "','" + cla.mdy(txtCom_PeriodofMicroplanningFromTo.Text.Trim()) + "','" + cla.mdy(txtCom_ProjectReportDate.Text.Trim()) + "','" + cla.mdy(txtCom_DCCApprovalDate.Text.Trim()) + "',N'" + For21ProjectReport + "',N'" + UseOfWaterProjectReport + "',N'" + CFCReport.Trim() + "',N'" + txtCom_VillageDevelopmentPlan.Text.Trim() + "' ";
                    str += " ,N'" + txtCom_UpscaleTCM.Text.Trim() + "',N'" + txtCom_PreventionTCM.Text.Trim() + "',N'" + txtCom_BalanceExclusionTCM.Text.Trim() + "',N'" + txtCom_TroubleshootTCM.Text.Trim() + "','" + ViewState["SanctionedVDP"].ToString().Trim() + "','" + DropDownList1.SelectedValue.Trim() + "' ";
                    str += ", N'" + txtForestrybasedfarmingPractices.Text.Trim() + "',N'" + txtTotalorchardPlanting.Text.Trim() + "',N'" + txtTotalalkalinelandmanagement.Text.Trim() + "',N'" + txtTotalprotectedfarming.Text.Trim() + "',N'" + txtTotalIntegratedfarming.Text.Trim() + "','" + ImprovingoverallSoilhealth.Text.Trim() + "' ";
                    str += ", N'" + txtEfficientAndsustainableuseoftotalwater.Text.Trim() + "',N'" + txtWaterStorageAtthebase.Text.Trim() + "',N'" + txtFineIrrigation.Text.Trim() + "',N'" + txtAvailabilitywaterForProtectedIrrigation.Text.Trim() + "' ";
                    str += ", N'" + txtCreatingInfrastructure.Text.Trim() + "',N'" + txtAgriculturalEquipmentCenter.Text.Trim() + "',N'" + txtClimatefriendlyvarieties.Text.Trim() + "',N'" + txtTotalSeedHubInfrastructure.Text.Trim() + "' ";
                    str += " , " + dt.Rows[0]["City_ID"].ToString().Trim() + " ," + dt.Rows[0]["TalukaID"].ToString().Trim() + "," + dt.Rows[0]["SubdivisionID"].ToString().Trim() + "," + dt.Rows[0]["ClustersMasterID"].ToString().Trim() + "," + dt.Rows[0]["VillageID"].ToString().Trim() + ",1,21,'" + ViewState["IsFromMLP"].ToString().Trim() + "') ";
                    cla.ExecuteCommand(str, command);
                    ddlGramPanchayatCode.Enabled = false;
                    //  txtName.ReadOnly = true;
                    ddlVCRMC.Enabled = false;
                    cla.ExecuteCommand("Update Tbl_M_RegistrationDetails SET  IsRegDraft=NULL  where RegistrationID=" + RegistrationID + " ", command);

                    foreach (GridViewRow gr in GrdWorkDetails.Rows)
                    {
                        String SoilAndWaterRetentionWorksID = GrdWorkDetails.DataKeys[gr.RowIndex]["SoilAndWaterRetentionWorksID"].ToString();
                        TextBox TextBox1 = (TextBox)(gr.FindControl("TextBox1"));
                        TextBox TextBox2 = (TextBox)(gr.FindControl("TextBox2"));
                        TextBox TextBox3 = (TextBox)(gr.FindControl("TextBox3"));
                        TextBox TextBox4 = (TextBox)(gr.FindControl("TextBox4"));
                        TextBox TextBox5 = (TextBox)(gr.FindControl("TextBox5"));
                        TextBox TextBox6 = (TextBox)(gr.FindControl("TextBox6"));
                        Label Label1 = (Label)(gr.FindControl("lblTotalPysical"));
                        Label Label2 = (Label)(gr.FindControl("Label2"));
                        Label Label4 = (Label)(gr.FindControl("lblTotalPysicalNumber"));
                        HiddenField ActivityId = (HiddenField)(gr.FindControl("hfActivityID"));
                        List<String> lst = new List<string>();
                        lst.Add(SoilAndWaterRetentionWorksID);
                        lst.Add(RegistrationID.ToString());
                        lst.Add(TextBox1.Text.Trim());
                        lst.Add(TextBox2.Text.Trim());
                        lst.Add(TextBox3.Text.Trim());
                        lst.Add(TextBox4.Text.Trim());
                        lst.Add(TextBox5.Text.Trim());
                        lst.Add(TextBox6.Text.Trim());
                        lst.Add(Label1.Text);
                        lst.Add(Label2.Text);
                        lst.Add("I");
                        lst.Add("");
                        lst.Add(Label4.Text);
                        lst.Add(ActivityId.Value);
                        cla.ExecuteByProcedure("SP_Insert_SoilAndWaterWorkRegistration", lst, command);
                    }

                    //cla.ExecuteCommand("Update Tbl_M_RegistrationDetails SET  IsRegDraft=0  where RegistrationID=" + RegistrationID + " ", command);

                    //int UserId = cla.TableID("Tbl_M_LoginDetails", "UserId",command);
                    //str = " INSERT INTO Tbl_M_LoginDetails (UserId, RegistrationID, UserName, LoginAs, FullName,UPass)";
                    //str += " VALUES(" + UserId + "," + RegistrationID.ToString().Trim() + ",'" + ddlGramPanchayatCode.SelectedItem.Text.Trim() + "','Beneficiary','" + txtName.Text.Trim() + "','123@123')";
                    //cla.ExecuteCommand(str, command);

                    LiteralName.Text = "Gram Panchayat Code :: " + ddlGramPanchayatCode.SelectedItem.Text.Trim().ToUpper();


                    ViewState["RegistrationID"] = RegistrationID;
                    transaction.Commit();


                    //byte[] bin = FileUpload1.FileBytes;
                    //String ret = fileRet.UploadData(PathUp, fileName, bin);

                    Util.ShowMessageBox(this.Page, "Success", MyCommanClass.GetMsgInEnForDB("MSG10", Session["Lang"].ToString()), "success");
                    Session["RegistrationIDPass"] = ViewState["RegistrationID"].ToString().Trim();
                    Response.Redirect("RegisterSucess.aspx?" + EncryptDecryptQueryString.encrypt(string.Format("ID={0}", ViewState["RegistrationID"].ToString().Trim())), false);

                }
                catch (Exception ex)
                {
                    Util.LogError(ex);
                    try
                    {
                        transaction.Rollback();

                    }
                    catch
                    {

                    }

                    Util.ShowMessageBox(this.Page, "Error", ex.Message.Trim(), "error");
                }
                finally
                {
                    if (connection != null) { connection.Close(); }
                    command.Dispose();
                }

            }




        }


        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            LiteralMsg.Text = "";
            //------------validations ---------------------// 


            if (ViewState["RegistrationID"].ToString().Trim().Length == 0)
            {

                Util.ShowMessageBox(this.Page, "Error", "Error", "error");
                return;
            }


            String phase = cla.GetExecuteScalar("Select Phase from Tbl_M_VillageMaster where VillageID="+ ddlGramPanchayatCode.SelectedValue.Trim()+ "");
            if (phase.Trim().Length == 0) phase = "0";
            if (Convert.ToInt32(phase) != 1)
            {
                if (ViewState["IsFromMLP"].ToString().Trim() != "YES")
                {
                    Util.ShowMessageBox(this.Page, "Error", "Please click Get Data From MLP APP button to Update the data. ", "error");
                    return;
                }
            }



            //String BeneficiaryTypesID = "2";
            String BeneficiaryTypesID = "99";
            LiteralMsg.Text = "";
            //------------validations ---------------------// 
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
            if (ddlGramPanchayatCode.SelectedIndex == 0)
            {

                Util.ShowMessageBox(this.Page, "Required", MyCommanClass.GetMsgInEnForDB("MSG11", Session["Lang"].ToString()), "error");
                return;
            }




            Regex regex = new Regex(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$");

            //Verify whether date entered in dd/MM/yyyy format.
            bool isValid = regex.IsMatch(txtCom_PeriodofMicroplanningFrom.Text.Trim());
            //Verify whether entered date is Valid date.
            DateTime dt1;
            isValid = DateTime.TryParseExact(txtCom_PeriodofMicroplanningFrom.Text, "dd/MM/yyyy", new CultureInfo("en-GB"), DateTimeStyles.None, out dt1);
            if (!isValid)
            {
                Util.ShowMessageBox(this.Page, "Required", "Date should be in DD/MM/YYYY only", "error");
                return;
            }

            isValid = regex.IsMatch(txtCom_PeriodofMicroplanningFromTo.Text.Trim());
            //Verify whether entered date is Valid date.
            isValid = DateTime.TryParseExact(txtCom_PeriodofMicroplanningFromTo.Text, "dd/MM/yyyy", new CultureInfo("en-GB"), DateTimeStyles.None, out dt1);
            if (!isValid)
            {
                Util.ShowMessageBox(this.Page, "Required", "Date should be in DD/MM/YYYY only", "error");
                return;
            }

            isValid = regex.IsMatch(txtCom_ProjectReportDate.Text.Trim());
            //Verify whether entered date is Valid date.
            isValid = DateTime.TryParseExact(txtCom_ProjectReportDate.Text, "dd/MM/yyyy", new CultureInfo("en-GB"), DateTimeStyles.None, out dt1);
            if (!isValid)
            {
                Util.ShowMessageBox(this.Page, "Required", "Date should be in DD/MM/YYYY only", "error");
                return;
            }



            String str = "";//, SanctionedVDP = "", PathUp = "", fileName = "";
            DataTable dt = cla.GetDataTable("Select City_ID,TalukaID,ClustersMasterID,VillageID,SubdivisionID from Tbl_M_VillageMaster where VillageID=" + ddlGramPanchayatCode.SelectedValue.Trim() + "");


            //if (FileUpload1.HasFile == true)
            //{


            //    //if (FileUpload1.PostedFile.ContentLength > 3145728)//remove limit as per sohel
            //    //{

            //    //    Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG29", Session["Lang"].ToString()), "error");

            //    //    return;
            //    //}

            //    string FileError = MyCommanClass.CheckAllowedFileExtension(System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName.ToString()));
            //    if (FileError.Length > 0)
            //    {
            //        Util.ShowMessageBox(this.Page, "Error", FileError, "error");

            //        return;
            //    }
            //    FileError = Util.CheckAllowedFileName(FileUpload1);
            //    if (FileError.Length > 0)
            //    {
            //        Util.ShowMessageBox(this.Page, "Error", FileError, "error");

            //        return;
            //    }


            //}
            //else
            if (ViewState["SanctionedVDP"].ToString().Trim().Length == 0)
            {
                if (HiddenField1.Value.Trim().Length == 0)
                {
                    Util.ShowMessageBox(this.Page, "Error", "Required ! मंजूर व्हीडीपी / सविस्तर प्रकल्प अहवाल अपलोड करण्याची पीडीएफ प्रत अपलोड करा  ", "error");

                    return;
                }
            }


            //------------End validations ---------------------// 






            String For21ProjectReport = txtTotal1.Text, UseOfWaterProjectReport = txtTotal2.Text, CFCReport = txtTotal3.Text;


            int RegistrationID = cla.TableID("Tbl_M_RegistrationDetails", "RegistrationID");


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

                    // cla.ExecuteCommand("Update Tbl_M_RegistrationDetails SET  IsDeleted='1' where RegistrationID=" + ViewState["RegistrationID"].ToString().Trim() + " ", command);

                    if (ViewState["RegistrationID"].ToString().Trim().Trim().Length == 0)
                    {
                        // ADD NEW 
                        str = " INSERT INTO Tbl_M_RegistrationDetails (RegistrationID, BeneficiaryTypesID, GramPanchayatCode, VillageID , RegisterName,IAgree ";
                        str += " , Com_PeriodofMicroplanningFrom ,Com_PeriodofMicroplanningTo ,Com_ProjectReportDate ,Com_DCCApprovalDate ,Com_For21ProjectReport ,Com_UseOfWaterProjectReport ,Com_CFCReport  ,Com_VillageDevelopmentPlan ,Com_UpscaleTCM ,Com_PreventionTCM ,Com_BalanceExclusionTCM ,Com_Troubleshoot,SanctionedVDP,eTerndingBy ";
                        str += " ,  ForestrybasedfarmingPractices ,TotalorchardPlanting ,Totalalkalinelandmanagement ,Totalprotectedfarming ,TotalIntegratedfarming ,ImprovingoverallSoilhealth ";
                        str += " ,  EfficientAndsustainableuseoftotalwater ,WaterStorageAtthebase  ,FineIrrigation  ,AvailabilitywaterForProtectedIrrigation ";
                        str += " ,  CreatingInfrastructure,AgriculturalEquipmentCenter,Climatefriendlyvarieties,TotalSeedHubInfrastructure ";
                        str += " , Work_City_ID,Work_TalukaID,Work_SubdivisionID,Work_ClustersMasterID,Work_VillageID,ApplicationStatusID,ApprovalStageID,IsFromMLP ) ";

                        str += " VALUES (" + RegistrationID + "," + BeneficiaryTypesID + ",'" + ddlGramPanchayatCode.SelectedItem.Text.Trim().ToUpper() + "'," + ddlGramPanchayatCode.SelectedValue.Trim() + ",N'" + ddlVCRMC.SelectedItem.Text.Trim() + "','1'";
                        str += "  ,'" + cla.mdy(txtCom_PeriodofMicroplanningFrom.Text) + "','" + cla.mdy(txtCom_PeriodofMicroplanningFromTo.Text.Trim()) + "','" + cla.mdy(txtCom_ProjectReportDate.Text.Trim()) + "','" + cla.mdy(txtCom_DCCApprovalDate.Text.Trim()) + "',N'" + For21ProjectReport + "',N'" + UseOfWaterProjectReport + "',N'" + CFCReport.Trim() + "',N'" + txtCom_VillageDevelopmentPlan.Text.Trim() + "' ";

                        str += " ,N'" + txtCom_UpscaleTCM.Text.Trim() + "',N'" + txtCom_PreventionTCM.Text.Trim() + "',N'" + txtCom_BalanceExclusionTCM.Text.Trim() + "',N'" + txtCom_TroubleshootTCM.Text.Trim() + "','" + ViewState["SanctionedVDP"].ToString().Trim() + "','" + DropDownList1.SelectedValue.Trim() + "' ";
                        str += ", N'" + txtForestrybasedfarmingPractices.Text.Trim() + "',N'" + txtTotalorchardPlanting.Text.Trim() + "',N'" + txtTotalalkalinelandmanagement.Text.Trim() + "',N'" + txtTotalprotectedfarming.Text.Trim() + "',N'" + txtTotalIntegratedfarming.Text.Trim() + "','" + ImprovingoverallSoilhealth.Text.Trim() + "' ";
                        str += ", N'" + txtEfficientAndsustainableuseoftotalwater.Text.Trim() + "',N'" + txtWaterStorageAtthebase.Text.Trim() + "',N'" + txtFineIrrigation.Text.Trim() + "',N'" + txtAvailabilitywaterForProtectedIrrigation.Text.Trim() + "' ";
                        str += ", N'" + txtCreatingInfrastructure.Text.Trim() + "',N'" + txtAgriculturalEquipmentCenter.Text.Trim() + "',N'" + txtClimatefriendlyvarieties.Text.Trim() + "',N'" + txtTotalSeedHubInfrastructure.Text.Trim() + "' ";
                        str += " , " + dt.Rows[0]["City_ID"].ToString().Trim() + " ," + dt.Rows[0]["TalukaID"].ToString().Trim() + "," + dt.Rows[0]["SubdivisionID"].ToString().Trim() + "," + dt.Rows[0]["ClustersMasterID"].ToString().Trim() + "," + dt.Rows[0]["VillageID"].ToString().Trim() + ",1,21,'" + ViewState["IsFromMLP"].ToString().Trim() + "') ";

                        cla.ExecuteCommand(str, command);

                    }
                    else
                    {

                        RegistrationID = Convert.ToInt32(ViewState["RegistrationID"].ToString().Trim());
                        str = "  Update Tbl_M_RegistrationDetails Set ";
                        str += "  Com_PeriodofMicroplanningFrom='" + cla.mdy(txtCom_PeriodofMicroplanningFrom.Text) + "' ,Com_PeriodofMicroplanningTo='" + cla.mdy(txtCom_PeriodofMicroplanningFromTo.Text.Trim()) + "' ,Com_ProjectReportDate='" + cla.mdy(txtCom_ProjectReportDate.Text.Trim()) + "' ,Com_DCCApprovalDate='" + cla.mdy(txtCom_DCCApprovalDate.Text.Trim()) + "' ,Com_For21ProjectReport=N'" + For21ProjectReport + "' ,Com_UseOfWaterProjectReport=N'" + UseOfWaterProjectReport + "' ,Com_CFCReport=N'" + CFCReport.Trim() + "'  ,Com_VillageDevelopmentPlan=N'" + txtCom_VillageDevelopmentPlan.Text.Trim() + "'";
                        str += " , Com_UpscaleTCM =N'" + txtCom_UpscaleTCM.Text.Trim() + "',Com_PreventionTCM=N'" + txtCom_PreventionTCM.Text.Trim() + "' ,Com_BalanceExclusionTCM=N'" + txtCom_BalanceExclusionTCM.Text.Trim() + "' ,Com_Troubleshoot=N'" + txtCom_TroubleshootTCM.Text.Trim() + "',eTerndingBy='" + DropDownList1.SelectedValue.Trim() + "'";
                        str += " , ForestrybasedfarmingPractices=N'" + txtForestrybasedfarmingPractices.Text.Trim() + "' ,TotalorchardPlanting=N'" + txtTotalorchardPlanting.Text.Trim() + "' ,Totalalkalinelandmanagement=N'" + txtTotalalkalinelandmanagement.Text.Trim() + "' ,Totalprotectedfarming=N'" + txtTotalprotectedfarming.Text.Trim() + "' ,TotalIntegratedfarming=N'" + txtTotalIntegratedfarming.Text.Trim() + "' ,ImprovingoverallSoilhealth='" + ImprovingoverallSoilhealth.Text.Trim() + "' ";
                        str += " , EfficientAndsustainableuseoftotalwater=N'" + txtEfficientAndsustainableuseoftotalwater.Text.Trim() + "' ,WaterStorageAtthebase=N'" + txtWaterStorageAtthebase.Text.Trim() + "'  ,FineIrrigation=N'" + txtFineIrrigation.Text.Trim() + "'  ,AvailabilitywaterForProtectedIrrigation=N'" + txtAvailabilitywaterForProtectedIrrigation.Text.Trim() + "'";
                        str += " , CreatingInfrastructure=N'" + txtCreatingInfrastructure.Text.Trim() + "',AgriculturalEquipmentCenter=N'" + txtAgriculturalEquipmentCenter.Text.Trim() + "',Climatefriendlyvarieties=N'" + txtClimatefriendlyvarieties.Text.Trim() + "',TotalSeedHubInfrastructure=N'" + txtTotalSeedHubInfrastructure.Text.Trim() + "' ";
                        str += " , ApplicationStatusID=1,ApprovalStageID=21,IsRegDraft=1";
                        str += " where RegistrationID = " + RegistrationID + "";
                        cla.ExecuteCommand(str, command);
                    }


                    //if (FileUpload1.HasFile == true)
                    //{
                    //    PathUp = "DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "";
                    //    SanctionedVDP = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "/SanctionedVDP" + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName.ToString());
                    //    fileName = "SanctionedVDP" + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName.ToString());
                    //    //SanctionedVDP
                    //}




                    if (ViewState["SanctionedVDP"].ToString().Length > 0)
                    {
                        cla.ExecuteCommand("Update Tbl_M_RegistrationDetails SET  SanctionedVDP='" + ViewState["SanctionedVDP"].ToString().Trim() + "'  where RegistrationID=" + RegistrationID + " ", command);
                    }
                    else
                    {
                        cla.ExecuteCommand("Update Tbl_M_RegistrationDetails SET  SanctionedVDP='" + HiddenField1.Value.Trim() + "'  where RegistrationID=" + RegistrationID + " ", command);
                    }

                    cla.ExecuteCommand("Update Tbl_M_RegistrationDetails SET  IsRegDraft=NULL  where RegistrationID=" + RegistrationID + " ", command);


                    ddlGramPanchayatCode.Enabled = false;
                    //   txtName.ReadOnly = true;
                    ddlVCRMC.Enabled = false;

                    cla.ExecuteCommand("update Tbl_T_SoilAndWaterRetentionWorks set IsDeleted='1' where RegistrationID=" + RegistrationID + " ", command);
                    foreach (GridViewRow gr in GrdWorkDetails.Rows)
                    {
                        String SoilAndWaterRetentionWorksID = GrdWorkDetails.DataKeys[gr.RowIndex]["SoilAndWaterRetentionWorksID"].ToString();
                        TextBox TextBox1 = (TextBox)(gr.FindControl("TextBox1"));
                        TextBox TextBox2 = (TextBox)(gr.FindControl("TextBox2"));
                        TextBox TextBox3 = (TextBox)(gr.FindControl("TextBox3"));
                        TextBox TextBox4 = (TextBox)(gr.FindControl("TextBox4"));
                        TextBox TextBox5 = (TextBox)(gr.FindControl("TextBox5"));
                        TextBox TextBox6 = (TextBox)(gr.FindControl("TextBox6"));
                        Label Label1 = (Label)(gr.FindControl("lblTotalPysical"));
                        Label Label2 = (Label)(gr.FindControl("Label2"));
                        Label Label4 = (Label)(gr.FindControl("lblTotalPysicalNumber"));
                        HiddenField ActivityId = (HiddenField)(gr.FindControl("hfActivityID"));
                        List<String> lst = new List<string>();
                        lst.Add(SoilAndWaterRetentionWorksID);
                        lst.Add(RegistrationID.ToString());
                        lst.Add(TextBox1.Text.Trim());
                        lst.Add(TextBox2.Text.Trim());
                        lst.Add(TextBox3.Text.Trim());
                        lst.Add(TextBox4.Text.Trim());
                        lst.Add(TextBox5.Text.Trim());
                        lst.Add(TextBox6.Text.Trim());
                        lst.Add(Label1.Text);
                        lst.Add(Label2.Text);
                        lst.Add("I");
                        lst.Add("");
                        lst.Add(Label4.Text);
                        lst.Add(ActivityId.Value);
                        cla.ExecuteByProcedure("SP_Insert_SoilAndWaterWorkRegistration", lst, command);
                    }

                    //int UserId = cla.TableID("Tbl_M_LoginDetails", "UserId",command);
                    //str = " INSERT INTO Tbl_M_LoginDetails (UserId, RegistrationID, UserName, LoginAs, FullName,UPass)";
                    //str += " VALUES(" + UserId + "," + RegistrationID.ToString().Trim() + ",'" + ddlGramPanchayatCode.SelectedItem.Text.Trim() + "','Beneficiary','" + txtName.Text.Trim() + "','123@123')";
                    //cla.ExecuteCommand(str, command);

                    LiteralName.Text = "Gram Panchayat Code :: " + ddlGramPanchayatCode.SelectedItem.Text.Trim().ToUpper();


                    ViewState["RegistrationID"] = RegistrationID;
                    transaction.Commit();

                    //if (FileUpload1.HasFile == true)
                    //{
                    //    byte[] bin = FileUpload1.FileBytes;
                    //    String ret = fileRet.UploadData(PathUp, fileName, bin);
                    //}

                    Util.ShowMessageBox(this.Page, "Success", MyCommanClass.GetMsgInEnForDB("MSG10", Session["Lang"].ToString()), "success");
                    Session["RegistrationIDPass"] = ViewState["RegistrationID"].ToString().Trim();
                    Response.Redirect("RegisterSucess.aspx?" + EncryptDecryptQueryString.encrypt(string.Format("ID={0}", ViewState["RegistrationID"].ToString().Trim())), false);

                }
                catch (Exception ex)
                {
                    Util.LogError(ex);
                    try
                    {
                        transaction.Rollback();

                    }
                    catch
                    {

                    }

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
        protected void ddlGramPanchayatCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSaveAAudhar.Visible = true;

            string sname = string.Empty;

            string scodvillage = string.Empty;

            btnMlp.Enabled = true;

            string sviilege = ddlGramPanchayatCode.SelectedItem.Text.Trim().ToUpper();

            if (!string.IsNullOrEmpty(sviilege))
            {

                char[] splitchar = { '-' };
                // strArr = str.Split(splitchar);

                string[] scode = sviilege.Split(splitchar);
                sname = scode[0];
                scodvillage = scode[1];

                ddlVCRMC.DataSource = Comcls.GetVCRMC(scodvillage);
                ddlVCRMC.DataTextField = "VCRMCName";
                ddlVCRMC.DataValueField = "VCRMCID";
                ddlVCRMC.DataBind();
                //  ddlVCRMC.Items.Insert(0, new ListItem("--Select--", "0"));
                //  ddlVCRMC.SelectedIndex = 0;

            }




            if (Comcls.RecordExistanceChk("Tbl_M_RegistrationDetails", "GramPanchayatCode", ddlGramPanchayatCode.SelectedItem.Text, "RegistrationID") == false)
            {
                btnSaveAAudhar.Visible = false;
                if (Request.QueryString.Count == 0)
                {
                    Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG16", Session["Lang"].ToString()), "error");
                }
                //return;
            }

            string RegID = cla.GetExecuteScalar("select RegistrationID from Tbl_M_RegistrationDetails where GramPanchayatCode='" + ddlGramPanchayatCode.SelectedItem + "' and IsDeleted is null  ");
            if (RegID.Length > 0)
            {
                // btnMlp.Enabled = false;

                List<String> lst = new List<String>();
                lst.Add(RegID.ToString());

                dt = new DataTable();
                ViewState["RegistrationID"] = RegID;
                dt = cla.GetDtByProcedure("SP_CommunityRegistration_View_Details_comm", lst);
                //Convert.ToInt32(dt.Rows[0]["RegistrationID"]);
                if (dt.Rows.Count > 0)
                {
                    //txtName.Text = dt.Rows[0]["RegisterName"].ToString();//RegisterName  

                    try
                    {
                        ddlVCRMC.SelectedValue = dt.Rows[0]["RegisterName"].ToString();
                    }
                    catch { }

                    txtAgriculturalEquipmentCenter.Text = dt.Rows[0]["AgriculturalEquipmentCenter"].ToString();
                    txtAvailabilitywaterForProtectedIrrigation.Text = dt.Rows[0]["AvailabilitywaterForProtectedIrrigation"].ToString();
                    txtClimatefriendlyvarieties.Text = dt.Rows[0]["Climatefriendlyvarieties"].ToString();
                    txtCom_BalanceExclusionTCM.Text = dt.Rows[0]["Com_BalanceExclusionTCM"].ToString();
                    txtCom_DCCApprovalDate.Text = dt.Rows[0]["Com_DCCApprovalDate"].ToString();
                    txtCom_PeriodofMicroplanningFrom.Text = dt.Rows[0]["Com_PeriodofMicroplanningFrom"].ToString();
                    txtCom_PeriodofMicroplanningFromTo.Text = dt.Rows[0]["Com_PeriodofMicroplanningTo"].ToString();
                    txtCom_PreventionTCM.Text = dt.Rows[0]["Com_PreventionTCM"].ToString();
                    //  txtCom_ProjectReportDate.Text = dt.Rows[0]["Com_PreventionTCM"].ToString();
                    txtCom_ProjectReportDate.Text = dt.Rows[0]["Com_ProjectReportDate"].ToString();
                    txtCom_TroubleshootTCM.Text = dt.Rows[0]["Com_Troubleshoot"].ToString();
                    txtCom_UpscaleTCM.Text = dt.Rows[0]["Com_UpscaleTCM"].ToString();
                    txtCom_VillageDevelopmentPlan.Text = dt.Rows[0]["Com_VillageDevelopmentPlan"].ToString();
                    txtCreatingInfrastructure.Text = dt.Rows[0]["CreatingInfrastructure"].ToString();
                    txtEfficientAndsustainableuseoftotalwater.Text = dt.Rows[0]["EfficientAndsustainableuseoftotalwater"].ToString();
                    txtFineIrrigation.Text = dt.Rows[0]["FineIrrigation"].ToString();
                    txtForestrybasedfarmingPractices.Text = dt.Rows[0]["ForestrybasedfarmingPractices"].ToString();

                    txtTotal1.Text = dt.Rows[0]["Com_For21ProjectReport"].ToString();
                    txtTotal2.Text = dt.Rows[0]["Com_UseOfWaterProjectReport"].ToString();
                    txtTotal3.Text = dt.Rows[0]["Com_CFCReport"].ToString();



                    txtTotalalkalinelandmanagement.Text = dt.Rows[0]["Totalalkalinelandmanagement"].ToString();
                    txtTotalIntegratedfarming.Text = dt.Rows[0]["TotalIntegratedfarming"].ToString();
                    txtTotalorchardPlanting.Text = dt.Rows[0]["TotalorchardPlanting"].ToString();
                    txtTotalprotectedfarming.Text = dt.Rows[0]["Totalprotectedfarming"].ToString();
                    txtTotalSeedHubInfrastructure.Text = dt.Rows[0]["TotalSeedHubInfrastructure"].ToString();
                    txtWaterStorageAtthebase.Text = dt.Rows[0]["WaterStorageAtthebase"].ToString();
                    ImprovingoverallSoilhealth.Text = dt.Rows[0]["ImprovingoverallSoilhealth"].ToString();

                    HiddenField1.Value = dt.Rows[0]["SanctionedVDP"].ToString();
                    if (dt.Rows[0]["SanctionedVDP"].ToString().Trim().Length > 0)
                    {
                        LabelAnyOtherCertificate.Text = "<a href='" + clsSettings.BaseUrl + "" + dt.Rows[0]["SanctionedVDP"].ToString() + "' > View & Download </a> ";//SanctionedVDP;
                        ViewState["SanctionedVDP"] = dt.Rows[0]["SanctionedVDP"].ToString();
                    }
                    try
                    {
                        DropDownList1.SelectedValue = dt.Rows[0]["eTerndingBy"].ToString();
                    }
                    catch { }


                    DataTable dtAdd = cla.GetDataTable("SELECT wm.SoilAndWaterRetentionWorksID  FROM Tbl_M_SoilAndWaterRetentionWorks as  WM  where wm.IsDeleted is null and wm.SoilAndWaterRetentionWorksID not in (Select SoilAndWaterRetentionWorksID from Tbl_T_SoilAndWaterRetentionWorks  where RegistrationID=" + ViewState["RegistrationID"].ToString().Trim() + " and IsDeleted is null)");
                    foreach (DataRow dr in dtAdd.Rows)
                    {
                        String SoilAndWaterRetentionWorksID = dr["SoilAndWaterRetentionWorksID"].ToString();
                        //int SoilAndWaterWorksID = cla.TableID("Tbl_T_SoilAndWaterRetentionWorks", "SoilAndWaterWorksID");
                        cla.ExecuteCommand("insert into Tbl_T_SoilAndWaterRetentionWorks (SoilAndWaterRetentionWorksID,RegistrationID) VALUES (" + SoilAndWaterRetentionWorksID + "," + ViewState["RegistrationID"].ToString().Trim() + ") ");
                    }


                    dt = cla.GetDataTable("SELECT distinct WM.SoilAndWaterRetentionWorksID ,WM.SoilAndWaterRetentionWorksGroup ,WM.SoilAndWaterRetentionWorksMr ,WM.UnitofMess,A.FYearPhysical ,A.FYearAmount  ,A.SYearPhysical ,A.SYearAmount ,A.TYearPhysical ,A.TYearAmount  ,A.TotalPhysical ,A.TotalAmount,WM.ActivityID,A.TotalPysicalNumber,WM.UnitofMessId , aa.ActivityCode  FROM Tbl_M_SoilAndWaterRetentionWorks as  WM  left outer join Tbl_T_SoilAndWaterRetentionWorks A on A.SoilAndWaterRetentionWorksID=WM.SoilAndWaterRetentionWorksID inner join Tbl_M_ActivityMaster aa on aa.ActivityID=wm.ActivityID where A.IsDeleted is null and WM.IsDeleted is null and A.RegistrationID=" + ViewState["RegistrationID"].ToString().Trim() + " ");
                    GrdWorkDetails.DataSource = dt;
                    GrdWorkDetails.DataBind();


                    Double a = 0, b = 0, c = 0, d = 0, e1 = 0, f = 0, g = 0, h = 0, i = 0;
                    foreach (GridViewRow gr in GrdWorkDetails.Rows)
                    {
                        Double Amount = 0, Physical = 0;
                        TextBox TextBox1 = (TextBox)(gr.FindControl("TextBox1"));
                        TextBox TextBox2 = (TextBox)(gr.FindControl("TextBox2"));


                        TextBox TextBox3 = (TextBox)(gr.FindControl("TextBox3"));
                        TextBox TextBox4 = (TextBox)(gr.FindControl("TextBox4"));
                        TextBox TextBox5 = (TextBox)(gr.FindControl("TextBox5"));
                        TextBox TextBox6 = (TextBox)(gr.FindControl("TextBox6"));


                        Label Label1 = (Label)(gr.FindControl("lblTotalPysical"));
                        Label Label2 = (Label)(gr.FindControl("Label2"));
                        Label Label4 = (Label)(gr.FindControl("lblTotalPysicalNumber"));
                        Label Label3 = (Label)(gr.FindControl("Label3"));
                        HiddenField ActivityId = (HiddenField)(gr.FindControl("hfActivityID"));

                        if (TextBox1.Text.Trim().Length > 0)
                        {
                            Physical = Physical + Convert.ToDouble(TextBox1.Text.Trim());
                            a = a + Convert.ToDouble(TextBox1.Text.Trim());
                        }
                        if (TextBox2.Text.Trim().Length > 0)
                        {
                            Amount = Amount + Convert.ToDouble(TextBox2.Text.Trim());
                            b = b + Convert.ToDouble(TextBox2.Text.Trim());
                        }
                        if (TextBox3.Text.Trim().Length > 0)
                        {
                            Physical = Physical + Convert.ToDouble(TextBox3.Text.Trim());
                            c = c + Convert.ToDouble(TextBox3.Text.Trim());
                        }
                        if (TextBox4.Text.Trim().Length > 0)
                        {
                            Amount = Amount + Convert.ToDouble(TextBox4.Text.Trim());
                            d = d + Convert.ToDouble(TextBox4.Text.Trim());
                        }
                        if (TextBox5.Text.Trim().Length > 0)
                        {
                            Physical = Physical + Convert.ToDouble(TextBox5.Text.Trim());
                            e1 = e1 + Convert.ToDouble(TextBox5.Text.Trim());
                        }
                        if (TextBox6.Text.Trim().Length > 0)
                        {
                            Amount = Amount + Convert.ToDouble(TextBox6.Text.Trim());
                            f = f + Convert.ToDouble(TextBox6.Text.Trim());
                        }





                        Label2.Text = Amount.ToString("0.00");
                        if (Label3.Text.ToString() == "1")
                        {
                            Label1.Text = Physical.ToString("0.00");
                            Label4.Text = "0.00";
                        }
                        else
                        {
                            Label4.Text = Physical.ToString("0.00");
                            Label1.Text = "0.00";
                        }


                        if (Label4.Text.Trim().Length > 0)
                        {
                            i = i + Convert.ToDouble(Label2.Text.Trim());
                        }
                        if (Label2.Text.Trim().Length > 0)
                        {
                            g = g + Convert.ToDouble(Label2.Text.Trim());
                        }
                        if (Label1.Text.Trim().Length > 0)
                        {
                            h = h + Convert.ToDouble(Label1.Text.Trim());
                        }
                    }
                    GrdWorkDetails.FooterRow.Cells[2].Text = "एकूण एकंदर ";
                    GrdWorkDetails.FooterRow.Cells[3].Text = a.ToString();
                    GrdWorkDetails.FooterRow.Cells[4].Text = b.ToString();
                    GrdWorkDetails.FooterRow.Cells[5].Text = c.ToString();
                    GrdWorkDetails.FooterRow.Cells[6].Text = d.ToString();
                    GrdWorkDetails.FooterRow.Cells[7].Text = e1.ToString();
                    GrdWorkDetails.FooterRow.Cells[8].Text = f.ToString();
                    GrdWorkDetails.FooterRow.Cells[9].Text = h.ToString();
                    GrdWorkDetails.FooterRow.Cells[10].Text = i.ToString();
                    GrdWorkDetails.FooterRow.Cells[11].Text = g.ToString();

                }
            }


        }

        protected void TextBox1_TextChangedPhysical(object sender, EventArgs e)
        {

            TextBox txtFocus = (TextBox)sender;
            GridViewRow gridViewRow = (GridViewRow)txtFocus.NamingContainer;
            Double a = 0, b = 0, c = 0, d = 0, e1 = 0, f = 0, g = 0, h = 0, i = 0;
            foreach (GridViewRow gr in GrdWorkDetails.Rows)
            {
                Double Amount = 0, Physical = 0;
                TextBox TextBox1 = (TextBox)(gr.FindControl("TextBox1"));
                TextBox TextBox2 = (TextBox)(gr.FindControl("TextBox2"));
                TextBox TextBox3 = (TextBox)(gr.FindControl("TextBox3"));
                TextBox TextBox4 = (TextBox)(gr.FindControl("TextBox4"));
                TextBox TextBox5 = (TextBox)(gr.FindControl("TextBox5"));
                TextBox TextBox6 = (TextBox)(gr.FindControl("TextBox6"));
                Label Label1 = (Label)(gr.FindControl("lblTotalPysical"));
                Label Label2 = (Label)(gr.FindControl("Label2"));
                Label Label4 = (Label)(gr.FindControl("lblTotalPysicalNumber"));
                Label Label3 = (Label)(gr.FindControl("Label3"));
                HiddenField ActivityId = (HiddenField)(gr.FindControl("hfActivityID"));
                if (TextBox1.Text.Trim().Length > 0)
                {
                    Physical = Physical + Convert.ToDouble(TextBox1.Text.Trim());
                    a = a + Convert.ToDouble(TextBox1.Text.Trim());
                }
                if (TextBox2.Text.Trim().Length > 0)
                {
                    Amount = Amount + Convert.ToDouble(TextBox2.Text.Trim());
                    b = b + Convert.ToDouble(TextBox2.Text.Trim());
                }
                if (TextBox3.Text.Trim().Length > 0)
                {
                    Physical = Physical + Convert.ToDouble(TextBox3.Text.Trim());
                    c = c + Convert.ToDouble(TextBox3.Text.Trim());
                }
                if (TextBox4.Text.Trim().Length > 0)
                {
                    Amount = Amount + Convert.ToDouble(TextBox4.Text.Trim());
                    d = d + Convert.ToDouble(TextBox4.Text.Trim());
                }
                if (TextBox5.Text.Trim().Length > 0)
                {
                    Physical = Physical + Convert.ToDouble(TextBox5.Text.Trim());
                    e1 = e1 + Convert.ToDouble(TextBox5.Text.Trim());
                }
                if (TextBox6.Text.Trim().Length > 0)
                {
                    Amount = Amount + Convert.ToDouble(TextBox6.Text.Trim());
                    f = f + Convert.ToDouble(TextBox6.Text.Trim());
                }
                Label2.Text = Amount.ToString("0.00");
                if (Label3.Text.ToString() == "1")
                {
                    Label1.Text = Physical.ToString("0.00");
                    Label4.Text = "0.00";
                }
                else
                {
                    Label4.Text = Physical.ToString("0.00");
                    Label1.Text = "0.00";
                }


                if (Label4.Text.Trim().Length > 0)
                {
                    i = i + Convert.ToDouble(Label2.Text.Trim());
                }
                if (Label2.Text.Trim().Length > 0)
                {
                    g = g + Convert.ToDouble(Label2.Text.Trim());
                }
                if (Label1.Text.Trim().Length > 0)
                {
                    h = h + Convert.ToDouble(Label1.Text.Trim());
                }
            }
            GrdWorkDetails.FooterRow.Cells[2].Text = "एकूण एकंदर ";
            GrdWorkDetails.FooterRow.Cells[3].Text = a.ToString();
            GrdWorkDetails.FooterRow.Cells[4].Text = b.ToString();
            GrdWorkDetails.FooterRow.Cells[5].Text = c.ToString();
            GrdWorkDetails.FooterRow.Cells[6].Text = d.ToString();
            GrdWorkDetails.FooterRow.Cells[7].Text = e1.ToString();
            GrdWorkDetails.FooterRow.Cells[8].Text = f.ToString();
            GrdWorkDetails.FooterRow.Cells[9].Text = h.ToString();
            GrdWorkDetails.FooterRow.Cells[10].Text = i.ToString();
            GrdWorkDetails.FooterRow.Cells[11].Text = g.ToString();


            #region "Focus"
            if (txtFocus.ID == "TextBox1")
            {
                TextBox txt = (TextBox)gridViewRow.FindControl("TextBox2");
                txt.Focus();
            }
            else if (txtFocus.ID == "TextBox2")
            {
                TextBox txt = (TextBox)gridViewRow.FindControl("TextBox3");
                txt.Focus();
            }
            else if (txtFocus.ID == "TextBox3")
            {
                TextBox txt = (TextBox)gridViewRow.FindControl("TextBox4");
                txt.Focus();
            }
            else if (txtFocus.ID == "TextBox4")
            {
                TextBox txt = (TextBox)gridViewRow.FindControl("TextBox5");
                txt.Focus();
            }
            else if (txtFocus.ID == "TextBox5")
            {
                TextBox txt = (TextBox)gridViewRow.FindControl("TextBox6");
                txt.Focus();
            }
            else
            {
                GrdWorkDetails.Focus();
                txtFocus.Focus();
            }
            #endregion
        }

        protected void btnDraft_Click(object sender, EventArgs e)
        {

            LiteralMsg.Text = "";
            //------------validations ---------------------// 
            Double Total1 = 0, Total2 = 0, Total3 = 0, Total = 0;

            Total1 = Convert.ToDouble(txtForestrybasedfarmingPractices.Text) + Convert.ToDouble(txtTotalorchardPlanting.Text) + Convert.ToDouble(txtTotalalkalinelandmanagement.Text) + Convert.ToDouble(txtTotalprotectedfarming.Text) + Convert.ToDouble(txtTotalIntegratedfarming.Text) + Convert.ToDouble(ImprovingoverallSoilhealth.Text);
            Total2 = Convert.ToDouble(txtEfficientAndsustainableuseoftotalwater.Text) + Convert.ToDouble(txtWaterStorageAtthebase.Text) + Convert.ToDouble(txtFineIrrigation.Text) + Convert.ToDouble(txtAvailabilitywaterForProtectedIrrigation.Text);
            Total3 = Convert.ToDouble(txtCreatingInfrastructure.Text) + Convert.ToDouble(txtAgriculturalEquipmentCenter.Text) + Convert.ToDouble(txtClimatefriendlyvarieties.Text) + Convert.ToDouble(txtTotalSeedHubInfrastructure.Text);


            Total = Total1 + Total2 + Total3;
            txtCom_VillageDevelopmentPlan.Text = Total.ToString("N2");

            String For21ProjectReport = Total1.ToString("N2"), UseOfWaterProjectReport = Total2.ToString("N2"), CFCReport = Total3.ToString("N2");


            String Isupdate = Convert.ToString(ViewState["RegistrationID"]);

            if (!string.IsNullOrEmpty(Isupdate)) { }
            else
            {

                if (Comcls.RecordExistanceChk("Tbl_M_RegistrationDetails", "GramPanchayatCode", ddlGramPanchayatCode.SelectedItem.Text, "RegistrationID") == false)
                {

                    Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG16", Session["Lang"].ToString()), "error");
                    return;
                }
            }

            //String BeneficiaryTypesID = "2";
            String BeneficiaryTypesID = "99";
            LiteralMsg.Text = "";
            //------------validations ---------------------// 
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
            if (ddlGramPanchayatCode.SelectedIndex == 0)
            {

                Util.ShowMessageBox(this.Page, "Required", MyCommanClass.GetMsgInEnForDB("MSG11", Session["Lang"].ToString()), "error");
                return;
            }


            String str = "";//, SanctionedVDP = "", PathUp = "", fileName = "";
            DataTable dt = cla.GetDataTable("Select City_ID,TalukaID,ClustersMasterID,VillageID,SubdivisionID from Tbl_M_VillageMaster where VillageID=" + ddlGramPanchayatCode.SelectedValue.Trim() + "");




            //------------End validations ---------------------// 









            int RegistrationID = cla.TableID("Tbl_M_RegistrationDetails", "RegistrationID");


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
                    if (ViewState["RegistrationID"].ToString().Trim().Length == 0)
                    {
                        // ADD NEW 
                        str = " INSERT INTO Tbl_M_RegistrationDetails (RegistrationID, BeneficiaryTypesID, GramPanchayatCode, VillageID , RegisterName,IAgree ";
                        str += " , Com_PeriodofMicroplanningFrom ,Com_PeriodofMicroplanningTo ,Com_ProjectReportDate ,Com_DCCApprovalDate ,Com_For21ProjectReport ,Com_UseOfWaterProjectReport ,Com_CFCReport  ,Com_VillageDevelopmentPlan ,Com_UpscaleTCM ,Com_PreventionTCM ,Com_BalanceExclusionTCM ,Com_Troubleshoot,eTerndingBy ";
                        str += " ,  ForestrybasedfarmingPractices ,TotalorchardPlanting ,Totalalkalinelandmanagement ,Totalprotectedfarming ,TotalIntegratedfarming ,ImprovingoverallSoilhealth ";
                        str += " ,  EfficientAndsustainableuseoftotalwater ,WaterStorageAtthebase  ,FineIrrigation  ,AvailabilitywaterForProtectedIrrigation ";
                        str += " ,  CreatingInfrastructure,AgriculturalEquipmentCenter,Climatefriendlyvarieties,TotalSeedHubInfrastructure ";
                        str += " , Work_City_ID,Work_TalukaID,Work_SubdivisionID,Work_ClustersMasterID,Work_VillageID,ApplicationStatusID,ApprovalStageID,IsRegDraft ) ";

                        str += " VALUES (" + RegistrationID + "," + BeneficiaryTypesID + ",'" + ddlGramPanchayatCode.SelectedItem.Text.Trim().ToUpper() + "'," + ddlGramPanchayatCode.SelectedValue.Trim() + ",N'" + ddlVCRMC.SelectedItem.Text.Trim() + "','1'";
                        str += " ,'" + cla.mdy(txtCom_PeriodofMicroplanningFrom.Text) + "','" + cla.mdy(txtCom_PeriodofMicroplanningFromTo.Text.Trim()) + "','" + cla.mdy(txtCom_ProjectReportDate.Text.Trim()) + "','" + cla.mdy(txtCom_DCCApprovalDate.Text.Trim()) + "',N'" + For21ProjectReport + "',N'" + UseOfWaterProjectReport + "',N'" + CFCReport.Trim() + "',N'" + txtCom_VillageDevelopmentPlan.Text.Trim() + "' ";
                        str += " ,N'" + txtCom_UpscaleTCM.Text.Trim() + "',N'" + txtCom_PreventionTCM.Text.Trim() + "',N'" + txtCom_BalanceExclusionTCM.Text.Trim() + "',N'" + txtCom_TroubleshootTCM.Text.Trim() + "','" + DropDownList1.SelectedValue.Trim() + "' ";
                        str += ", N'" + txtForestrybasedfarmingPractices.Text.Trim() + "',N'" + txtTotalorchardPlanting.Text.Trim() + "',N'" + txtTotalalkalinelandmanagement.Text.Trim() + "',N'" + txtTotalprotectedfarming.Text.Trim() + "',N'" + txtTotalIntegratedfarming.Text.Trim() + "','" + ImprovingoverallSoilhealth.Text.Trim() + "' ";

                        str += ", N'" + txtEfficientAndsustainableuseoftotalwater.Text.Trim() + "',N'" + txtWaterStorageAtthebase.Text.Trim() + "',N'" + txtFineIrrigation.Text.Trim() + "',N'" + txtAvailabilitywaterForProtectedIrrigation.Text.Trim() + "' ";
                        str += ", N'" + txtCreatingInfrastructure.Text.Trim() + "',N'" + txtAgriculturalEquipmentCenter.Text.Trim() + "',N'" + txtClimatefriendlyvarieties.Text.Trim() + "',N'" + txtTotalSeedHubInfrastructure.Text.Trim() + "' ";
                        str += " , " + dt.Rows[0]["City_ID"].ToString().Trim() + " ," + dt.Rows[0]["TalukaID"].ToString().Trim() + "," + dt.Rows[0]["SubdivisionID"].ToString().Trim() + "," + dt.Rows[0]["ClustersMasterID"].ToString().Trim() + "," + dt.Rows[0]["VillageID"].ToString().Trim() + ",1,21,1) ";
                        cla.ExecuteCommand(str, command);
                    }
                    else
                    {
                        RegistrationID = Convert.ToInt32(ViewState["RegistrationID"].ToString().Trim());
                        str = "  Update Tbl_M_RegistrationDetails Set ";
                        str += "  Com_PeriodofMicroplanningFrom='" + cla.mdy(txtCom_PeriodofMicroplanningFrom.Text) + "' ,Com_PeriodofMicroplanningTo='" + cla.mdy(txtCom_PeriodofMicroplanningFromTo.Text.Trim()) + "' ,Com_ProjectReportDate='" + cla.mdy(txtCom_ProjectReportDate.Text.Trim()) + "' ,Com_DCCApprovalDate='" + cla.mdy(txtCom_DCCApprovalDate.Text.Trim()) + "' ,Com_For21ProjectReport=N'" + For21ProjectReport + "' ,Com_UseOfWaterProjectReport=N'" + UseOfWaterProjectReport + "' ,Com_CFCReport=N'" + CFCReport.Trim() + "'  ,Com_VillageDevelopmentPlan=N'" + txtCom_VillageDevelopmentPlan.Text.Trim() + "'";
                        str += " , Com_UpscaleTCM =N'" + txtCom_UpscaleTCM.Text.Trim() + "',Com_PreventionTCM=N'" + txtCom_PreventionTCM.Text.Trim() + "' ,Com_BalanceExclusionTCM=N'" + txtCom_BalanceExclusionTCM.Text.Trim() + "' ,Com_Troubleshoot=N'" + txtCom_TroubleshootTCM.Text.Trim() + "',eTerndingBy='" + DropDownList1.SelectedValue.Trim() + "'";
                        str += " , ForestrybasedfarmingPractices=N'" + txtForestrybasedfarmingPractices.Text.Trim() + "' ,TotalorchardPlanting=N'" + txtTotalorchardPlanting.Text.Trim() + "' ,Totalalkalinelandmanagement=N'" + txtTotalalkalinelandmanagement.Text.Trim() + "' ,Totalprotectedfarming=N'" + txtTotalprotectedfarming.Text.Trim() + "' ,TotalIntegratedfarming=N'" + txtTotalIntegratedfarming.Text.Trim() + "' ,ImprovingoverallSoilhealth='" + ImprovingoverallSoilhealth.Text.Trim() + "' ";
                        str += " , EfficientAndsustainableuseoftotalwater=N'" + txtEfficientAndsustainableuseoftotalwater.Text.Trim() + "' ,WaterStorageAtthebase=N'" + txtWaterStorageAtthebase.Text.Trim() + "'  ,FineIrrigation=N'" + txtFineIrrigation.Text.Trim() + "'  ,AvailabilitywaterForProtectedIrrigation=N'" + txtAvailabilitywaterForProtectedIrrigation.Text.Trim() + "'";
                        str += " , CreatingInfrastructure=N'" + txtCreatingInfrastructure.Text.Trim() + "',AgriculturalEquipmentCenter=N'" + txtAgriculturalEquipmentCenter.Text.Trim() + "',Climatefriendlyvarieties=N'" + txtClimatefriendlyvarieties.Text.Trim() + "',TotalSeedHubInfrastructure=N'" + txtTotalSeedHubInfrastructure.Text.Trim() + "' ";
                        str += " , ApplicationStatusID=1,ApprovalStageID=21,IsRegDraft=1";
                        str += " where RegistrationID = " + RegistrationID + "";
                        cla.ExecuteCommand(str, command);

                    }

                    //if (FileUpload1.HasFile == true)
                    //{
                    //    PathUp = "DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "";
                    //    SanctionedVDP = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "/SanctionedVDP" + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName.ToString());
                    //    fileName = "SanctionedVDP" + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName.ToString());
                    //    //SanctionedVDP
                    //}


                    ddlGramPanchayatCode.Enabled = false;
                    ddlVCRMC.Enabled = false;

                    if (ViewState["SanctionedVDP"].ToString().Trim().Length > 0)
                    {
                        cla.ExecuteCommand("Update Tbl_M_RegistrationDetails SET  SanctionedVDP='" + ViewState["SanctionedVDP"].ToString().Trim() + "'  where RegistrationID=" + RegistrationID + " ", command);
                    }
                    else
                    {
                        if (HiddenField1.Value.Trim().Length > 0)
                            cla.ExecuteCommand("Update Tbl_M_RegistrationDetails SET  SanctionedVDP='" + HiddenField1.Value.Trim() + "'  where RegistrationID=" + RegistrationID + " ", command);
                    }

                    cla.ExecuteCommand("update Tbl_T_SoilAndWaterRetentionWorks set IsDeleted='1' where RegistrationID=" + RegistrationID + " ", command);
                    foreach (GridViewRow gr in GrdWorkDetails.Rows)
                    {
                        String SoilAndWaterRetentionWorksID = GrdWorkDetails.DataKeys[gr.RowIndex]["SoilAndWaterRetentionWorksID"].ToString();
                        TextBox TextBox1 = (TextBox)(gr.FindControl("TextBox1"));
                        TextBox TextBox2 = (TextBox)(gr.FindControl("TextBox2"));
                        TextBox TextBox3 = (TextBox)(gr.FindControl("TextBox3"));
                        TextBox TextBox4 = (TextBox)(gr.FindControl("TextBox4"));
                        TextBox TextBox5 = (TextBox)(gr.FindControl("TextBox5"));
                        TextBox TextBox6 = (TextBox)(gr.FindControl("TextBox6"));
                        Label Label1 = (Label)(gr.FindControl("lblTotalPysical"));
                        Label Label2 = (Label)(gr.FindControl("Label2"));
                        Label Label4 = (Label)(gr.FindControl("lblTotalPysicalNumber"));
                        HiddenField ActivityId = (HiddenField)(gr.FindControl("hfActivityID"));
                        List<String> lst = new List<string>();
                        lst.Add(SoilAndWaterRetentionWorksID);
                        lst.Add(RegistrationID.ToString());
                        lst.Add(TextBox1.Text.Trim());
                        lst.Add(TextBox2.Text.Trim());
                        lst.Add(TextBox3.Text.Trim());
                        lst.Add(TextBox4.Text.Trim());
                        lst.Add(TextBox5.Text.Trim());
                        lst.Add(TextBox6.Text.Trim());
                        lst.Add(Label1.Text);
                        lst.Add(Label2.Text);
                        lst.Add("I");
                        lst.Add("");
                        lst.Add(Label4.Text);
                        lst.Add(ActivityId.Value);
                        cla.ExecuteByProcedure("SP_Insert_SoilAndWaterWorkRegistration", lst, command);
                    }


                    LiteralName.Text = "Gram Panchayat Code :: " + ddlGramPanchayatCode.SelectedItem.Text.Trim().ToUpper();


                    ViewState["RegistrationID"] = RegistrationID;
                    transaction.Commit();


                    //byte[] bin = FileUpload1.FileBytes;
                    //String ret = fileRet.UploadData(PathUp, fileName, bin);

                    Util.ShowMessageBox(this.Page, "Success", MyCommanClass.GetMsgInEnForDB("MSG10", Session["Lang"].ToString()), "success");
                    Session["RegistrationIDPass"] = ViewState["RegistrationID"].ToString().Trim();
                    Response.Redirect("RegisterSucess.aspx?" + EncryptDecryptQueryString.encrypt(string.Format("ID={0}", ViewState["RegistrationID"].ToString().Trim())), false);
                    //  Response.Redirect("../AdminTrans/CommunityProfileEditDraft.aspx?" + EncryptDecryptQueryString.encrypt(string.Format("ID={0}", ViewState["RegistrationID"].ToString().Trim())), false);


                }
                catch (Exception ex)
                {
                    Util.LogError(ex);
                    try
                    {
                        transaction.Rollback();

                    }
                    catch
                    {

                    }

                    Util.ShowMessageBox(this.Page, "Error", ex.Message.Trim(), "error");
                }
                finally
                {
                    if (connection != null) { connection.Close(); }
                    command.Dispose();
                }

            }



        }

        protected void btnMlp_Click(object sender, EventArgs e)
        {
            if (ddlGramPanchayatCode.SelectedIndex == 0)
            {
                Util.ShowMessageBox(this.Page, "Error", "Please Select Village", "error");
                return;
            }

            string sviilege = ddlGramPanchayatCode.SelectedItem.Text.Trim().ToUpper();
            String scodvillage = "";
            if (!string.IsNullOrEmpty(sviilege))
            {
                char[] splitchar = { '-' };
                string[] scode = sviilege.Split(splitchar);
                scodvillage = scode[1];



            }
            if (scodvillage.Length > 0)
                FillDataFromMLP(scodvillage.Trim());


        }


        private void FillDataFromMLP(String census_code)
        {


            // Proposed Structure Data 
            SetReadonly(this, false);
            try
            {
                String key = "a910d2ba49ef2e4a74f8e0056749b10d";
                String url = "https://api-mlp.mahapocra.gov.in/v2/ReportService/water-budget-all-zone-reports";
                string jsonContent = "{\"api_key\":\"" + key.Trim() + "\",\"census_code\":\"" + census_code.Trim() + "\"}";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.KeepAlive = true;
                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                Byte[] byteArray = encoding.GetBytes(jsonContent);

                request.ContentLength = byteArray.Length;
                request.ContentType = @"application/json";

                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
                long length = 0;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    length = response.ContentLength;
                    StreamReader sr = new StreamReader(response.GetResponseStream());
                    string results = sr.ReadToEnd();
                    var myDetails = JsonConvert.DeserializeObject<MLPData>(results);
                    String status = myDetails.status;
                    if (status.Trim() == "200")
                    {
                        try
                        {
                            txtCom_PeriodofMicroplanningFrom.Text = myDetails.data.from.Replace("-", "/").Replace("'", "");
                            txtCom_PeriodofMicroplanningFromTo.Text = myDetails.data.to.Replace("-", "/").Replace("'", "");
                            txtCom_ProjectReportDate.Text = myDetails.data.tharav_date.Replace("-", "/").Replace("'", "");
                        }
                        catch { }
                        txtForestrybasedfarmingPractices.Text = myDetails.data.total_1.Replace("'", "");
                        txtTotalorchardPlanting.Text = myDetails.data.total_2.Replace("'", "");
                        txtTotalalkalinelandmanagement.Text = myDetails.data.total_3.Replace("'", "");
                        txtTotalprotectedfarming.Text = myDetails.data.total_4.Replace("'", "");
                        txtTotalIntegratedfarming.Text = myDetails.data.total_5.Replace("'", "");
                        ImprovingoverallSoilhealth.Text = myDetails.data.total_6.Replace("'", "");


                        txtEfficientAndsustainableuseoftotalwater.Text = myDetails.data.total_7.Replace("'", "");
                        txtWaterStorageAtthebase.Text = myDetails.data.total_8.Replace("'", "");
                        txtFineIrrigation.Text = myDetails.data.total_9.Replace("'", "");
                        txtAvailabilitywaterForProtectedIrrigation.Text = myDetails.data.total_10.Replace("'", "");

                        txtCreatingInfrastructure.Text = myDetails.data.total_11.Replace("'", "");
                        txtAgriculturalEquipmentCenter.Text = myDetails.data.total_12.Replace("'", "");
                        txtClimatefriendlyvarieties.Text = myDetails.data.total_13.Replace("'", "");
                        txtTotalSeedHubInfrastructure.Text = myDetails.data.total_14.Replace("'", "");


                        txtTotal1.Text = myDetails.data.total_15.Replace("'", "");
                        txtTotal2.Text = myDetails.data.total_16.Replace("'", "");
                        txtTotal3.Text = myDetails.data.total_17.Replace("'", "");

                        txtCom_UpscaleTCM.Text = myDetails.data.total_18.Replace("'", "");
                        txtCom_VillageDevelopmentPlan.Text = myDetails.data.total_19.Replace("'", "");


                        txtCom_PreventionTCM.Text = myDetails.data.total_20.Replace("'", "");
                        txtCom_BalanceExclusionTCM.Text = myDetails.data.total_21.Replace("'", "");
                        txtCom_TroubleshootTCM.Text = myDetails.data.total_22.Replace("'", "");

                        List<proposed_structure> proposed_structure = myDetails.data.proposed_structure;

                        DataTable dt = cla.GetDataTable("SELECT WM.SoilAndWaterRetentionWorksID ,WM.SoilAndWaterRetentionWorksGroup ,WM.SoilAndWaterRetentionWorksMr +' - '+A.ActivityCode as SoilAndWaterRetentionWorksMr ,WM.UnitofMess,'' FYearPhysical ,'' FYearAmount  ,''SYearPhysical ,''SYearAmount ,''TYearPhysical ,''TYearAmount  ,''TotalPhysical ,''TotalAmount ,WM.ActivityID, ''TotalPysicalNumber,WM.UnitofMessId,A.ActivityCode FROM Tbl_M_SoilAndWaterRetentionWorks as  WM inner join Tbl_M_ActivityMaster A on A.ActivityID=WM.ActivityID  where WM.IsDeleted is null order by WM.OrderbyNo,WM.SoilAndWaterRetentionWorksGroup desc");
                        GrdWorkDetails.DataSource = dt;
                        GrdWorkDetails.DataBind();

                        foreach (GridViewRow gr in GrdWorkDetails.Rows)
                        {
                            //TextBox1 भौतिक
                            //लाख TextBox2
                            TextBox TextBox1 = (TextBox)(gr.FindControl("TextBox1"));
                            TextBox TextBox2 = (TextBox)(gr.FindControl("TextBox2"));
                            //A.ActivityCode
                            String ActivityCode = GrdWorkDetails.DataKeys[gr.RowIndex]["ActivityCode"].ToString();
                            foreach (proposed_structure s in proposed_structure)
                            {
                                if (s.code.ToString().Trim() == ActivityCode.Trim())
                                {
                                    TextBox1.Text = s.t_structure_count.Replace("'", "");
                                    TextBox2.Text = s.t_structure_amt.Replace("'", "");
                                    if (TextBox2.Text.Trim().Length == 0)
                                    {
                                        TextBox1.Text = "0";
                                        TextBox2.Text = "0";
                                    }
                                }

                            }

                            TextBox TextBox3 = (TextBox)(gr.FindControl("TextBox3"));
                            TextBox TextBox4 = (TextBox)(gr.FindControl("TextBox4"));
                            TextBox TextBox5 = (TextBox)(gr.FindControl("TextBox5"));
                            TextBox TextBox6 = (TextBox)(gr.FindControl("TextBox6"));

                            TextBox3.Text = "0";
                            TextBox4.Text = "0";
                            TextBox5.Text = "0";
                            TextBox6.Text = "0";
                            //TextBox1.Enabled = false;
                            //TextBox2.Enabled = false;
                            //TextBox3.Enabled = false;
                            //TextBox4.Enabled = false;
                            //TextBox5.Enabled = false;
                            //TextBox6.Enabled = false;

                        }

                        #region
                        Double a = 0, b = 0, c = 0, d = 0, e1 = 0, f = 0, g = 0, h = 0, i = 0;
                        foreach (GridViewRow gr in GrdWorkDetails.Rows)
                        {
                            Double Amount = 0, Physical = 0;
                            TextBox TextBox1 = (TextBox)(gr.FindControl("TextBox1"));
                            if (TextBox1.Text.Trim().Length == 0) TextBox1.Text = "0";
                            TextBox TextBox2 = (TextBox)(gr.FindControl("TextBox2"));
                            if (TextBox2.Text.Trim().Length == 0) TextBox2.Text = "0";
                            TextBox TextBox3 = (TextBox)(gr.FindControl("TextBox3"));
                            TextBox TextBox4 = (TextBox)(gr.FindControl("TextBox4"));
                            TextBox TextBox5 = (TextBox)(gr.FindControl("TextBox5"));
                            TextBox TextBox6 = (TextBox)(gr.FindControl("TextBox6"));
                            Label Label1 = (Label)(gr.FindControl("lblTotalPysical"));
                            Label Label2 = (Label)(gr.FindControl("Label2"));
                            Label Label4 = (Label)(gr.FindControl("lblTotalPysicalNumber"));
                            Label Label3 = (Label)(gr.FindControl("Label3"));
                            HiddenField ActivityId = (HiddenField)(gr.FindControl("hfActivityID"));
                            if (TextBox1.Text.Trim().Length > 0)
                            {
                                Physical = Physical + Convert.ToDouble(TextBox1.Text.Trim());
                                a = a + Convert.ToDouble(TextBox1.Text.Trim());
                            }
                            if (TextBox2.Text.Trim().Length > 0)
                            {
                                Amount = Amount + Convert.ToDouble(TextBox2.Text.Trim());
                                b = b + Convert.ToDouble(TextBox2.Text.Trim());
                            }
                            if (TextBox3.Text.Trim().Length > 0)
                            {
                                Physical = Physical + Convert.ToDouble(TextBox3.Text.Trim());
                                c = c + Convert.ToDouble(TextBox3.Text.Trim());
                            }
                            if (TextBox4.Text.Trim().Length > 0)
                            {
                                Amount = Amount + Convert.ToDouble(TextBox4.Text.Trim());
                                d = d + Convert.ToDouble(TextBox4.Text.Trim());
                            }
                            if (TextBox5.Text.Trim().Length > 0)
                            {
                                Physical = Physical + Convert.ToDouble(TextBox5.Text.Trim());
                                e1 = e1 + Convert.ToDouble(TextBox5.Text.Trim());
                            }
                            if (TextBox6.Text.Trim().Length > 0)
                            {
                                Amount = Amount + Convert.ToDouble(TextBox6.Text.Trim());
                                f = f + Convert.ToDouble(TextBox6.Text.Trim());
                            }
                            Label2.Text = Amount.ToString("0.00");
                            if (Label3.Text.ToString() == "1")
                            {
                                Label1.Text = Physical.ToString("0.00");
                                Label4.Text = "0.00";
                            }
                            else
                            {
                                Label4.Text = Physical.ToString("0.00");
                                Label1.Text = "0.00";
                            }


                            if (Label4.Text.Trim().Length > 0)
                            {
                                i = i + Convert.ToDouble(Label2.Text.Trim());
                            }
                            if (Label2.Text.Trim().Length > 0)
                            {
                                g = g + Convert.ToDouble(Label2.Text.Trim());
                            }
                            if (Label1.Text.Trim().Length > 0)
                            {
                                h = h + Convert.ToDouble(Label1.Text.Trim());
                            }
                        }
                        GrdWorkDetails.FooterRow.Cells[2].Text = "एकूण एकंदर ";
                        GrdWorkDetails.FooterRow.Cells[3].Text = a.ToString();
                        GrdWorkDetails.FooterRow.Cells[4].Text = b.ToString();
                        GrdWorkDetails.FooterRow.Cells[5].Text = c.ToString();
                        GrdWorkDetails.FooterRow.Cells[6].Text = d.ToString();
                        GrdWorkDetails.FooterRow.Cells[7].Text = e1.ToString();
                        GrdWorkDetails.FooterRow.Cells[8].Text = f.ToString();
                        GrdWorkDetails.FooterRow.Cells[9].Text = h.ToString();
                        GrdWorkDetails.FooterRow.Cells[10].Text = i.ToString();
                        GrdWorkDetails.FooterRow.Cells[11].Text = g.ToString();
                        #endregion

                        ViewState["IsFromMLP"] = "YES";
                        SetReadonly(this, true);
                        txtCom_DCCApprovalDate.ReadOnly = false;
                    }
                    else
                    {
                        Util.ShowMessageBox(this.Page, "Error", "सर्व झोन डेटा MLP अॅपमध्ये अपडेट केलेला नाही, कृपया MLP मध्ये डेटा एंट्री पूर्ण करा आणि नंतर पुन्हा प्रयत्न करा.", "error");
                    }

                }

            }
            catch (Exception ex)
            {
                Util.LogError(ex);
                Util.ShowMessageBox(this.Page, "Error", "Data is Not coming from the MLP app, please click on Get Data From MLP APP again. if the problem persists please contact to Pocra Office.", "error");
            }

        }

        private void SetReadonly(Control c, Boolean Status)
        {
            if (c == null)
            {
                return;
            }
            foreach (Control item in c.Controls)
            {

                if (item is TextBox)
                {
                    ((TextBox)item).ReadOnly = Status;
                }

                else if (item.HasControls())
                {
                    SetReadonly(item, Status);
                }

            }
        }

        protected void btnUPLOAD_Click(object sender, EventArgs e)
        {
            ViewState["SanctionedVDP"] = "";
            LabelAnyOtherCertificate.Text = "";
            if (FileUpload1.HasFile == true)
            {
                string FileError = MyCommanClass.CheckAllowedFileExtension(System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName.ToString()));
                if (FileError.Length > 0)
                {
                    Util.ShowMessageBox(this.Page, "Error", FileError, "error");

                    return;
                }
                FileError = Util.CheckAllowedFileName(FileUpload1);
                if (FileError.Length > 0)
                {
                    Util.ShowMessageBox(this.Page, "Error", FileError, "error");

                    return;
                }

                int RegistrationID = cla.TableID("Tbl_M_RegistrationDetails", "RegistrationID");
                String PathUp = "DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "";
                String SanctionedVDP = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "/SanctionedVDP" + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName.ToString());
                String fileName = "SanctionedVDP" + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName.ToString());
                byte[] bin = FileUpload1.FileBytes;
                String ret = fileRet.UploadData(PathUp, fileName, bin);
                if (ret.Length == 0)
                {
                    ViewState["SanctionedVDP"] = SanctionedVDP;
                    LabelAnyOtherCertificate.Text = "<a href='" + clsSettings.BaseUrl + "" + SanctionedVDP + "' > View & Download </a> ";//SanctionedVDP;
                }
                else
                {
                    Util.ShowMessageBox(this.Page, "Error", "Error is File Uploading.", "error");

                    return;
                }
            }
            else
            {
                Util.ShowMessageBox(this.Page, "Error", "Required ! मंजूर व्हीडीपी / सविस्तर प्रकल्प अहवाल अपलोड करण्याची पीडीएफ प्रत अपलोड करा  ", "error");

                return;
            }

        }
    }


    public class MLPData
    {

        public String response
        {
            get;
            set;
        }
        public string status
        {
            get;
            set;
        }
        public clsdata data { get; set; }

    }

    public class proposed_structure
    {
        public string code
        {
            get;
            set;
        }
        public string unit
        {
            get;
            set;
        }
        public string anudan
        {
            get;
            set;
        }
        public string t_structure_count
        {
            get;
            set;
        }
        public string t_structure_amt
        {
            get;
            set;
        }
    }

    public class clsdata
    {

        public string from
        {
            get;
            set;
        }
        public string to
        {
            get;
            set;
        }
        public string tharav_date
        {
            get;
            set;
        }
        public string total_1
        {
            get;
            set;
        }
        public string total_2
        {
            get;
            set;
        }
        public string total_3
        {
            get;
            set;
        }
        public string total_4
        {
            get;
            set;
        }
        public string total_5
        {
            get;
            set;
        }
        public string total_6
        {
            get;
            set;
        }
        public string total_7
        {
            get;
            set;
        }
        public string total_8
        {
            get; set;
        }

        public string total_9
        {
            get;
            set;
        }
        public string total_10
        {
            get;
            set;
        }
        public string total_11
        {
            get;
            set;
        }
        public string total_12
        {
            get;
            set;
        }
        public string total_13
        {
            get;
            set;
        }
        public string total_14
        {
            get;
            set;
        }
        public string total_15
        {
            get;
            set;
        }
        public string total_16
        {
            get; set;
        }
        public string total_17
        {
            get; set;
        }
        public string total_18
        {
            get; set;
        }
        public string total_19
        {
            get; set;
        }
        public string total_20
        {
            get; set;
        }
        public string total_21
        {
            get; set;
        }
        public string total_22
        {
            get; set;
        }

        public List<proposed_structure> proposed_structure { get; set; }
    }
}