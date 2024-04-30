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
using System.Net;
using System.Xml;
using DBTPoCRA.AdminTrans.UserControls;
using System.Web.Services;
using DBTPoCRA.APPData;
using RSACryptography;
using MOLCryptoEngine;
using CommanClsLibrary.Repository.Classes;
using CommanClsLibrary.Repository.Models;
using static CommanClsLibrary.Repository.Enums;

namespace DBTPoCRA.Registration
{
    public partial class IndividualRegistrationNew : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;
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

                if (!IsPostBack)
                {

                    DivIndividual.Visible = false;
                    DivDisableRegistration.Visible = true;
                    //DivIndividual.Visible = true;
                    //DivDisableRegistration.Visible = false;



                    multiViewEmployee.ActiveViewIndex = 0;
                    HttpContext.Current.Session["dt712"] = null;
                    HttpContext.Current.Session["dt8A"] = null;

                    ddlCATEGORY.DataSource = Comcls.GetCostCategoryMaster();
                    ddlCATEGORY.DataTextField = "CategoryMaster";
                    ddlCATEGORY.DataValueField = "CategoryMasterID";
                    ddlCATEGORY.DataBind();
                    ddlCATEGORY.Items.Insert(0, new ListItem("--Select--", "0"));
                    ddlCATEGORY.SelectedIndex = 0;

                    rdoGender.DataSource = Comcls.GetGenderMaster();
                    rdoGender.DataTextField = "Text";
                    rdoGender.DataValueField = "ID";
                    rdoGender.DataBind();
                    rdoGender.Items.Insert(0, new ListItem("--Select--", "0"));
                    rdoGender.SelectedIndex = 0;

                    fillDob();
                    div7A.Visible = false;
                    ViewState["RegistrationID"] = "";

                    Random a = new Random(Guid.NewGuid().GetHashCode());
                    int firstNumber = a.Next(1, 9);
                    int secondNumber = a.Next(1, 9);
                    ViewState["spam"] = firstNumber + secondNumber;
                    LiteralCapcha.Text = firstNumber.ToString() + " + " + secondNumber.ToString();


                    if (Session["Lang"] != null)
                    {
                        if (Session["Lang"].ToString().Trim() == "mr-IN")
                        {
                            btnMarathi.Text = "English";
                        }
                        else
                        {
                            btnMarathi.Text = "मराठी";
                        }
                    }
                    else
                    {
                        Session["Lang"] = "mr-IN";
                        if (btnMarathi.Text.Trim() == "मराठी")
                        {
                            Session["Lang"] = "mr-IN";

                        }
                        else
                        {
                            Session["Lang"] = "en-IN";

                        }
                    }

                    Page.Form.Attributes.Add("enctype", "multipart/form-data");
                    SetCaptchaText();
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> GetUrl();  </script>", false);

                    if (Session["Lang"].ToString().Trim() == "mr-IN")
                    {
                        rm = new ResourceManager("Resources.FarmerRegistration", System.Reflection.Assembly.Load("App_GlobalResources")); ci = Thread.CurrentThread.CurrentCulture;
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("mr-IN");
                        LoadString(Thread.CurrentThread.CurrentCulture);

                    }

                    //String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/1");              
                }
                else
                {
                    if (Session["Lang"].ToString().Trim() == "mr-IN")
                    {
                        rm = new ResourceManager("Resources.FarmerRegistration", System.Reflection.Assembly.Load("App_GlobalResources")); ci = Thread.CurrentThread.CurrentCulture;
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("mr-IN");
                        LoadString(Thread.CurrentThread.CurrentCulture);

                    }
                }
            }
            catch
            {

            }

        }

        private void SetCaptchaText()
        {
            Random oRandom = new Random();
            int iNumber = oRandom.Next(100000, 999999);
            Session["Captcha"] = iNumber.ToString();
            imgCaptcha.ImageUrl = "~/Registration/CaptchaT.ashx?C=" + EncryptDecryptQueryString.EncryptString(iNumber.ToString());
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            SetCaptchaText();
        }

        private void LoadString(CultureInfo ci)
        {
            Menu1.Items[0].Text = rm.GetString("REGISTRATION_DETAILS", ci);
            Menu1.Items[1].Text = rm.GetString("BASIC_DETAILS", ci);
            Menu1.Items[2].Text = rm.GetString("LAND_DETAILS", ci);
            Menu1.Items[3].Text = rm.GetString("DECLARATION", ci);

            Literal9.Text = rm.GetString("Farmer_Registration", ci);
            Literal3.Text = rm.GetString("REGISTRATION_DETAILS", ci);
            Literal2.Text = rm.GetString("AADHAR_NUMBER", ci);
            Literal1.Text = rm.GetString("AUTHENTICATION_TYPE", ci);
            Literal8.Text = rm.GetString("Name", ci);
            Literal7.Text = rm.GetString("DATE_OF_BIRTH", ci);
            Literal6.Text = rm.GetString("GENDER", ci);
            Literal5.Text = rm.GetString("Photo_as_on_Aadhar_Card", ci);
            Literal4.Text = rm.GetString("BASIC_DETAILS", ci);
            Literal10.Text = rm.GetString("HOUSE_NO", ci);
            Literal11.Text = rm.GetString("STREET_NO", ci);
            Literal12.Text = rm.GetString("DISTRICT", ci);
            Literal13.Text = rm.GetString("TALUKA", ci);
            Literal14.Text = rm.GetString("POST", ci);
            Literal15.Text = rm.GetString("PIN_CODE", ci);
            Literal16.Text = rm.GetString("VILLAGE", ci);
            Literal17.Text = rm.GetString("CLUSTER_CODE", ci);
            Literal18.Text = rm.GetString("MOBILE_1", ci);
            Literal19.Text = rm.GetString("MOBILE_2", ci);
            Literal20.Text = rm.GetString("LANDLINE_NO", ci);
            Literal21.Text = rm.GetString("EMAILID", ci);
            Literal22.Text = rm.GetString("PANNO", ci);
            Literal23.Text = rm.GetString("CATEGORY", ci);
            Literal24.Text = rm.GetString("PHYSICALLY_HANDICAP", ci);
            Literal25.Text = rm.GetString("LAND_DETAILS", ci);
            Literal26.Text = rm.GetString("Land_Status", ci);
            Literal27.Text = rm.GetString("DISTRICT", ci);
            Literal28.Text = rm.GetString("TALUKA", ci);
            Literal29.Text = rm.GetString("VILLAGE", ci);
            Literal30.Text = rm.GetString("A_KHATA_KRAMANK", ci);
            Literal31.Text = rm.GetString("Hectare", ci);
            Literal32.Text = rm.GetString("Area", ci);
            Literal33.Text = rm.GetString("FORM_8_A", ci);
            Literal34.Text = rm.GetString("A_KHATA_KRAMANK", ci);
            Literal35.Text = rm.GetString("SURVEY_NUMBER", ci);
            Literal36.Text = rm.GetString("Hectare", ci);
            Literal37.Text = rm.GetString("Area", ci);
            Literal38.Text = rm.GetString("Extracts", ci);
            Literal39.Text = rm.GetString("DECLARATION", ci);
            Literal40.Text = rm.GetString("Declarations", ci);
            Literal41.Text = rm.GetString("Disability_with_40_or_more_will_be_considered_for_scheme_benefit_Kindly_attached_certificate_from_Government_Medical_Practitioner_displaying_the_same", ci);
            Literal42.Text = rm.GetString("Info", ci);
            Literal43.Text = rm.GetString("Info1", ci);
            Literal44.Text = rm.GetString("Info2", ci);
            Literal45.Text = rm.GetString("Info3", ci);
            Literal46.Text = rm.GetString("Info4", ci);
            Literal47.Text = rm.GetString("Info5", ci);
            //Literal48.Text = rm.GetString("Info7", ci);
            Literal49.Text = rm.GetString("if_you_dont_have_an_aadhaar_number_please_click_here_to_enroll", ci);
            Label5.Text = rm.GetString("DISABILITY_PERCENTAGE", ci);
            Label1.Text = rm.GetString("UPLOAD_CERITIFICATE", ci);
            // Label6.Text = rm.GetString("LandLessCertificate", ci);
            btnSaveAAudhar.Text = rm.GetString("Continue", ci);
            btnBasic.Text = rm.GetString("Continue", ci);
            btnADD.Text = rm.GetString("ADD_8A", ci);
            btnAdd712.Text = rm.GetString("ADD_", ci);
            btnBasicBack0.Text = rm.GetString("Previous", ci);
            btnLandNext.Text = rm.GetString("Continue", ci);
            btnFinalSave.Text = rm.GetString("Submit", ci);
            btnOtp.Text = rm.GetString("SEND_OTP", ci);
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
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
        private void fillDob()
        {

            for (int x = 31; x != 0; x--)
            {
                if (x < 10)
                {
                    ddlDay.Items.Insert(0, new ListItem("0" + x.ToString(), x.ToString()));
                }
                else
                    ddlDay.Items.Insert(0, new ListItem(x.ToString(), x.ToString()));
            }
            ddlDay.Items.Insert(0, new ListItem("--Date--", "0"));


            for (int x = 12; x != 0; x--)
            {
                if (x < 10)
                {
                    ddlMonth.Items.Insert(0, new ListItem("0" + x.ToString(), x.ToString()));
                }
                else
                {
                    ddlMonth.Items.Insert(0, new ListItem(x.ToString(), x.ToString()));
                }

            }
            ddlMonth.Items.Insert(0, new ListItem("--Month--", "0"));


            for (int x = 1930; x != DateTime.Now.Year; x++)
            {
                ddlYear.Items.Insert(0, new ListItem(x.ToString(), x.ToString()));
            }
            ddlYear.Items.Insert(0, new ListItem("--Year--", "0"));

        }

        private void FillDropDowns()
        {
            DataTable dt = new DataTable();// 

            if (rdoLandStatus.SelectedValue.Trim() == "YES")
            {
                ddlLANDDISTRICT.DataSource = Comcls.GetCityPocra("27");
                ddlLANDDISTRICT.DataTextField = "Cityname";
                ddlLANDDISTRICT.DataValueField = "City_ID";
                ddlLANDDISTRICT.DataBind();
                ddlLANDDISTRICT.Items.Insert(0, new ListItem("--Select--", "0"));
                ddlLANDDISTRICT.SelectedIndex = 0;

                ddlDISTRICT.DataSource = Comcls.GetCityAll("27");
                ddlDISTRICT.DataTextField = "Cityname";
                ddlDISTRICT.DataValueField = "City_ID";
                ddlDISTRICT.DataBind();
                ddlDISTRICT.Items.Insert(0, new ListItem("--Select--", "0"));
                ddlDISTRICT.SelectedIndex = 0;

            }
            else
            {
                ddlLANDDISTRICT.DataSource = Comcls.GetCityPocra("27"); ;
                ddlLANDDISTRICT.DataTextField = "Cityname";
                ddlLANDDISTRICT.DataValueField = "City_ID";
                ddlLANDDISTRICT.DataBind();
                ddlLANDDISTRICT.Items.Insert(0, new ListItem("--Select--", "0"));
                ddlLANDDISTRICT.SelectedIndex = 0;

                ddlDISTRICT.DataSource = Comcls.GetCityPocra("27"); ;
                ddlDISTRICT.DataTextField = "Cityname";
                ddlDISTRICT.DataValueField = "City_ID";
                ddlDISTRICT.DataBind();
                ddlDISTRICT.Items.Insert(0, new ListItem("--Select--", "0"));
                ddlDISTRICT.SelectedIndex = 0;
            }
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

            ddlCATEGORY.SelectedIndex = 0;
            rdoHANDICAP.SelectedIndex = 1;

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);

        }

        protected void ddlLANDDISTRICT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLANDVILLAGE.Items.Count > 0)
                ddlLANDVILLAGE.Items.Clear();

            ddlLANDTALUKA.DataSource = Comcls.GetTalukaMaster(ddlLANDDISTRICT.SelectedValue.Trim());
            ddlLANDTALUKA.DataTextField = "Taluka";
            ddlLANDTALUKA.DataValueField = "TalukaID";
            ddlLANDTALUKA.DataBind();
            ddlLANDTALUKA.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlLANDTALUKA.SelectedIndex = 0;


        }

        protected void ddlTALUKA_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlVILLAGE.DataSource = Comcls.GetVillage(ddlDISTRICT.SelectedValue.Trim(), ddlTALUKA.SelectedValue.Trim());
            ddlVILLAGE.DataTextField = "VillageName";
            ddlVILLAGE.DataValueField = "VillageID";
            ddlVILLAGE.DataBind();
            ddlVILLAGE.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlVILLAGE.SelectedIndex = 0;


            ddlPOST.DataSource = Comcls.GetPostTalukaWiseMaster(ddlTALUKA.SelectedValue.Trim(), ddlDISTRICT.SelectedValue.Trim());
            ddlPOST.DataTextField = "PostName";
            ddlPOST.DataValueField = "Post_ID";
            ddlPOST.DataBind();
            ddlPOST.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlPOST.SelectedIndex = 0;



            ddlCATEGORY.SelectedIndex = 0;
            rdoHANDICAP.SelectedIndex = 1;
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
        }

        protected void ddlLANDTALUKA_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlLANDVILLAGE.DataSource = Comcls.GetVillage(ddlLANDDISTRICT.SelectedValue.Trim(), ddlLANDTALUKA.SelectedValue.Trim());
            ddlLANDVILLAGE.DataTextField = "VillageName";
            ddlLANDVILLAGE.DataValueField = "VillageID";
            ddlLANDVILLAGE.DataBind();
            ddlLANDVILLAGE.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlLANDVILLAGE.SelectedIndex = 0;
        }
        //protected void ddlCATEGORY_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    FileCATEGORYCERITIFICATE.Visible = false;
        //    Label1.Visible = false;
        //    if (cla.GetExecuteScalar("SELECT  DocRequired FROM  Tbl_M_CategoryMaster where DocRequired is not null  AND CategoryMasterID=" + ddlCATEGORY.SelectedValue.Trim() + "").Length > 0)
        //    {
        //        FileCATEGORYCERITIFICATE.Visible = true;
        //        Label1.Visible = true;
        //    }
        //}

        //protected void rdoHANDICAP_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //    txtDISABILITYPer.Visible = false;
        //    Label5.Visible = false;
        //    if (rdoHANDICAP.SelectedValue.Trim() == "YES")
        //    {
        //        txtDISABILITYPer.Visible = true;
        //        Label5.Visible = true;
        //    }



        //}

        protected void ddlVILLAGE_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCLUSTARCODE.Text = "";
            txtCLUSTARCODE.Text = cla.GetExecuteScalar("SELECT   Tbl_M_ClustersMaster.Clusters FROM   dbo.Tbl_M_VillageMaster INNER JOIN  dbo.Tbl_M_ClustersMaster ON dbo.Tbl_M_VillageMaster.ClustersMasterID = dbo.Tbl_M_ClustersMaster.ClustersMasterID WHERE  (dbo.Tbl_M_VillageMaster.VillageID =" + ddlVILLAGE.SelectedValue.Trim() + ")");

            ddlCATEGORY.SelectedIndex = 0;
            rdoHANDICAP.SelectedIndex = 1;
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
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
            try
            {
                Convert.ToDouble(txtAADHARNo.Text);
            }
            catch
            {
                Util.ShowMessageBox(this.Page, "Error", "Invalid Input", "error");
                return;
            }

            //AdharVaultAPICalls api = new AdharVaultAPICalls();
            //string ReferenceNumber = api.GetReferenceFromAdhar(txtAADHARNo.Text.Trim());
            //String RegExId = cla.GetExecuteScalar("Select top 1 RegistrationID  from Tbl_M_RegistrationDetails where IsDeleted is null and  BoltID in (select BoltID from Tbl_M_RegistrationDetails_Bolt where ADVRefrenceID='" + ReferenceNumber + "')");

            String RegExId = cla.GetExecuteScalar("Select top 1 RegistrationID  from Tbl_M_RegistrationDetails where IsDeleted is null and  BoltID in (select BoltID from Tbl_M_RegistrationDetails_Bolt where AaDharNumber='" + txtAADHARNo.Text.Trim() + "')");
            if (RegExId.Trim().Length > 0)
            {
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG26", Session["Lang"].ToString()), "error");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                return;
            }

            if (ViewState["RegistrationID"].ToString().Trim().Length == 0)
            {
                if (RegExId.Trim().Length > 0)
                {
                    Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG26", Session["Lang"].ToString()), "error");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                    return;
                }


                if (Comcls.RecordExistanceChk("Tbl_M_RegistrationDetails_Bolt", "AaDharNumber", txtAADHARNo.Text.Trim(), "BoltID") == false)
                {

                    Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG27", Session["Lang"].ToString()), "error");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                    return;
                }



            }
            String BeneficiaryTypesID = "1";


            if (txtAADHARNo.Text.Trim().Length == 0)
            {
                Util.ShowMessageBox(this.Page, "Required", MyCommanClass.GetMsgInEnForDB("MSG12", Session["Lang"].ToString()), "error");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                return;
            }
            else if (txtName.Text.Trim().Length == 0)
            {

                Util.ShowMessageBox(this.Page, "Required", MyCommanClass.GetMsgInEnForDB("MSG3", Session["Lang"].ToString()), "error");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                return;
            }




            if (FileAnyOtherCertificate.HasFile == true)
            {


                if (FileAnyOtherCertificate.PostedFile.ContentLength > 3145728)
                {

                    Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG29", Session["Lang"].ToString()), "error");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                    return;
                }

                string FileError = MyCommanClass.CheckAllowedFileExtension(System.IO.Path.GetExtension(FileAnyOtherCertificate.PostedFile.FileName.ToString()));
                if (FileError.Length > 0)
                {
                    Util.ShowMessageBox(this.Page, "Error", FileError, "error");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                    return;
                }
                FileError = Util.CheckAllowedFileName(FileAnyOtherCertificate);
                if (FileError.Length > 0)
                {
                    Util.ShowMessageBox(this.Page, "Error", FileError, "error");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                    return;
                }


            }






            String DOB = ddlDay.SelectedValue.Trim() + "/" + ddlMonth.SelectedValue.Trim() + "/" + ddlYear.SelectedValue.Trim();
            //------------End validations ---------------------// 

            //List<String> lst = new List<string>();
            String str = "";
            int RegistrationID = cla.TableID("Tbl_M_RegistrationDetails", "RegistrationID");
            int BoltID = cla.TableID("Tbl_M_RegistrationDetails_Bolt", "BoltID");

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
                        //AdharVaultAPICalls api = new AdharVaultAPICalls();
                        //ReferenceNumber = api.GetReferenceFromAdhar(txtAADHARNo.Text.Trim());
                        //cla.ExecuteCommand("INSERT INTO Tbl_M_RegistrationDetails_Bolt(BoltID, ADVRefrenceID) VALUES(" + BoltID + ",'" + ReferenceNumber.Trim().ToUpper() + "')", command);                         
                        cla.ExecuteCommand("INSERT INTO Tbl_M_RegistrationDetails_Bolt(BoltID, AaDharNumber) VALUES(" + BoltID + ",'" + txtAADHARNo.Text.Trim().ToUpper() + "')", command);

                        // ADD NEW 
                        str = " INSERT INTO Tbl_M_RegistrationDetails (RegistrationID, BeneficiaryTypesID, RegisterName, DateOfBirth, Gender,IsDeleted,BoltID,RegisterNameMr)";
                        str += " VALUES(" + RegistrationID + "," + BeneficiaryTypesID + ",N'" + txtName.Text.Trim() + "','" + cla.mdy(DOB.Trim()) + "','" + rdoGender.SelectedValue.Trim() + "','1'," + BoltID + ",N'" + txtNameInMarathi.Text.Trim() + "')";
                        cla.ExecuteCommand(str, command);

                        ViewState["RegistrationID"] = RegistrationID;
                        LiteralName.Text = txtName.Text.Trim().ToUpper();

                        transaction.Commit();
                    }
                    else
                    {
                        // EDIT 
                        transaction.Commit();
                    }

                    if (ViewState["RegistrationID"].ToString().Trim().Length > 0)
                    {
                        String PathUp = "DocMasters/MemberDoc/" + ViewState["RegistrationID"].ToString().Trim() + "";



                        String AnyOtherCertificate = "";
                        if (FileAnyOtherCertificate.HasFile)
                        {
                            if (FileAnyOtherCertificate.PostedFile.ContentLength < 3145728)
                            {
                                //String Uppath = path + "/AnyOtherCertificate" + System.IO.Path.GetExtension(FileAnyOtherCertificate.PostedFile.FileName.ToString());
                                // FileAnyOtherCertificate.PostedFile.SaveAs(Uppath.Trim()); //File is saved in the Physical folder 
                                AnyOtherCertificate = "/admintrans/DocMasters/MemberDoc/" + ViewState["RegistrationID"].ToString().Trim() + "/AnyOtherCertificate" + System.IO.Path.GetExtension(FileAnyOtherCertificate.PostedFile.FileName.ToString());

                                byte[] bin = FileAnyOtherCertificate.FileBytes;
                                String fileName = "AnyOtherCertificate" + System.IO.Path.GetExtension(FileAnyOtherCertificate.PostedFile.FileName.ToString());
                                //fileRet.UploadData(PathUp, fileName, bin);

                                String ret = fileRet.UploadData(PathUp, fileName, bin);
                                if (ret.Trim().Length > 0)
                                {
                                    Util.ShowMessageBox(this.Page, "Error", "Please upload Any Other Certificate", "error");
                                    return;
                                }

                                LabelAnyOtherCertificate.Text = "<a href='" + clsSettings.BaseUrl + "" + AnyOtherCertificate + "' target=_blank >  VIEW UPLOADED CERITIFICATE  </a>";
                            }
                        }

                        str = " UPDATE  Tbl_M_RegistrationDetails SET  AnyOtherDocType=N'" + ddlAnyOtherCertificate.SelectedValue.Trim() + "' , AnyOtherDoc='" + AnyOtherCertificate.Trim() + "'  ";
                        str += " WHERE(RegistrationID = " + ViewState["RegistrationID"].ToString().Trim() + ")";
                        cla.ExecuteCommand(str);


                        txtAADHARNo.ReadOnly = true;
                        txtName.ReadOnly = true;


                        multiViewEmployee.ActiveViewIndex = 1;
                        Menu1.Items[1].Enabled = true;
                        FillDropDowns();
                    }
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBasic_Click(object sender, EventArgs e)
        {
            LiteralMsg.Text = "";
            //------------validations ---------------------// 

            if (ddlCATEGORY.SelectedValue.Trim() == "5")
            {
                lblCATEGORY.Text = "";
            }
            if (rdoHANDICAP.SelectedValue.Trim() == "NO")
            {
                lblHandiChetificate.Text = "";
                txtDISABILITYPer.Text = "";
            }

            if (txtAADHARNo.Text.Trim().Length == 0)
            {

                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG12", Session["Lang"].ToString()), "error");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                return;
            }
            else if (txtName.Text.Trim().Length == 0)
            {

                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG3", Session["Lang"].ToString()), "error");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                return;
            }
            if (ddlCATEGORY.SelectedIndex == 0)
            {

                Util.ShowMessageBox(this.Page, "Required", MyCommanClass.GetMsgInEnForDB("MSG30", Session["Lang"].ToString()), "error");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                return;
            }
            else if (ddlCATEGORY.SelectedValue.Trim() != "5")
            {
                if (lblCATEGORY.Text.Trim().Length == 0)
                {
                    if (FileCATEGORYCERITIFICATE.HasFile == false)
                    {

                        Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG31", Session["Lang"].ToString()), "error");
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                        return;

                    }
                    else
                    {
                        if (FileCATEGORYCERITIFICATE.PostedFile.ContentLength > 3145728)
                        {

                            Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG32", Session["Lang"].ToString()), "error");
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                            return;
                        }
                        string FileError = MyCommanClass.CheckAllowedFileExtension(System.IO.Path.GetExtension(FileCATEGORYCERITIFICATE.PostedFile.FileName.ToString()));
                        if (FileError.Length > 0)
                        {
                            Util.ShowMessageBox(this.Page, "Error", FileError, "error");
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                            return;
                        }
                        FileError = Util.CheckAllowedFileName(FileCATEGORYCERITIFICATE);
                        if (FileError.Length > 0)
                        {
                            Util.ShowMessageBox(this.Page, "Error", FileError, "error");
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                            return;
                        }
                    }
                }


            }
            else if (rdoHANDICAP.SelectedValue.Trim() == "YES")
            {

                if (lblHandiChetificate.Text.Trim().Length == 0)
                {
                    if (txtDISABILITYPer.Text.Trim().Length > 0)
                    {
                        if (FileHANDICAPCERITIFICATE.HasFile == false)
                        {

                            Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG33", Session["Lang"].ToString()), "error");
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                            return;

                        }
                        else
                        {
                            if (FileHANDICAPCERITIFICATE.PostedFile.ContentLength > 3145728)
                            {

                                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG32", Session["Lang"].ToString()), "error");
                                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                                return;
                            }

                            string FileError = MyCommanClass.CheckAllowedFileExtension(System.IO.Path.GetExtension(FileHANDICAPCERITIFICATE.PostedFile.FileName.ToString()));
                            if (FileError.Length > 0)
                            {
                                Util.ShowMessageBox(this.Page, "Error", FileError, "error");
                                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                                return;
                            }
                            FileError = Util.CheckAllowedFileName(FileHANDICAPCERITIFICATE);
                            if (FileError.Length > 0)
                            {
                                Util.ShowMessageBox(this.Page, "Error", FileError, "error");
                                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                                return;
                            }
                        }
                    }
                    else
                    {

                        Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG34", Session["Lang"].ToString()), "error");
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                        return;
                    }
                }


            }

            //if (rdoLandStatus.SelectedValue.Trim() != "YES")// else 
            //{

            //    if (FileLandLessCertificate.HasFile == false)
            //    {

            //        Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG28", Session["Lang"].ToString()), "error");
            //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
            //        return;

            //    }
            //    else
            //    {
            //        if (FileLandLessCertificate.PostedFile.ContentLength > 3145728)
            //        {

            //            Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG29", Session["Lang"].ToString()), "error");
            //            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
            //            return;
            //        }

            //        string FileError = MyCommanClass.CheckAllowedFileExtension(System.IO.Path.GetExtension(FileLandLessCertificate.PostedFile.FileName.ToString()));
            //        if (FileError.Length > 0)
            //        {
            //            Util.ShowMessageBox(this.Page, "Error", FileError, "error");
            //            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
            //            return;
            //        }

            //    }
            //}
            ////------------End validations ---------------------// 


            String str = "", Work_CircleID = "", Work_ClustersMasterID = "";
            Work_ClustersMasterID = cla.GetExecuteScalar("Select ClustersMasterID from Tbl_M_VillageMaster where VillageID=" + ddlVILLAGE.SelectedValue.Trim() + "");
            Work_CircleID = cla.GetExecuteScalar("Select CircleID from Tbl_M_VillageMaster where VillageID=" + ddlVILLAGE.SelectedValue.Trim() + "");


            String CastCategoryDoc = "";
            String PhysicallyHandicapDoc = "";

            if (ViewState["RegistrationID"].ToString().Trim().Length > 0)
            {

                String PathUp = "DocMasters/MemberDoc/" + ViewState["RegistrationID"].ToString().Trim() + "";


                if (FileCATEGORYCERITIFICATE.HasFile)
                {
                    if (FileCATEGORYCERITIFICATE.PostedFile.ContentLength < 3145728)
                    {
                        CastCategoryDoc = "/admintrans/DocMasters/MemberDoc/" + ViewState["RegistrationID"].ToString().Trim() + "/CasteCategoryCertificate" + System.IO.Path.GetExtension(FileCATEGORYCERITIFICATE.PostedFile.FileName.ToString());
                        byte[] bin = FileCATEGORYCERITIFICATE.FileBytes;
                        String fileName = "CasteCategoryCertificate" + System.IO.Path.GetExtension(FileCATEGORYCERITIFICATE.PostedFile.FileName.ToString());
                        String ret = fileRet.UploadData(PathUp, fileName, bin);
                        if (ret.Trim().Length > 0)
                        {
                            Util.ShowMessageBox(this.Page, "Error", "Please upload Caste Category Certificate", "error");
                            return;
                        }
                        lblCATEGORY.Text = "<a href='" + clsSettings.BaseUrl + "" + CastCategoryDoc + "' target=_blank >  VIEW UPLOADED CERITIFICATE  </a>";
                    }
                }


                if (FileHANDICAPCERITIFICATE.HasFile)
                {
                    if (FileHANDICAPCERITIFICATE.PostedFile.ContentLength < 3145728)
                    {
                        PhysicallyHandicapDoc = "/admintrans/DocMasters/MemberDoc/" + ViewState["RegistrationID"].ToString().Trim() + "/PhysicallyHandicapCertificate" + System.IO.Path.GetExtension(FileHANDICAPCERITIFICATE.PostedFile.FileName.ToString());
                        byte[] bin = FileHANDICAPCERITIFICATE.FileBytes;
                        String fileName = "PhysicallyHandicapCertificate" + System.IO.Path.GetExtension(FileHANDICAPCERITIFICATE.PostedFile.FileName.ToString());
                        String ret = fileRet.UploadData(PathUp, fileName, bin);
                        if (ret.Trim().Length > 0)
                        {
                            Util.ShowMessageBox(this.Page, "Error", "Please upload Physically Handicap Certificate", "error");
                            return;
                        }
                        lblHandiChetificate.Text = "<a href='" + clsSettings.BaseUrl + "" + PhysicallyHandicapDoc + "' target=_blank >  VIEW UPLOADED CERITIFICATE  </a>";
                    }
                }



            }


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
                        str = " UPDATE  Tbl_M_RegistrationDetails SET   CategoryMasterID =" + ddlCATEGORY.SelectedValue.Trim() + ", MobileNumber ='" + txtMobile1.Text.Trim() + "', ";
                        str += " MobileNumber2 ='" + txtMobile2.Text.Trim() + "', LandLineNumber ='" + txtLandLine.Text.Trim() + "', EmailID ='" + txtEmailID.Text.Trim() + "', PanNumber ='" + txtPAN.Text.Trim() + "', PhysicallyHandicap ='" + rdoHANDICAP.SelectedValue.Trim() + "', DisabilityPer ='" + txtDISABILITYPer.Text.Trim() + "', ";
                        str += " Address1HouseNo =N'" + txtHouseNo.Text.Trim() + "', Address1StreetName =N'" + txtStreetNo.Text.Trim() + "', Address1City_ID =" + ddlDISTRICT.SelectedValue.Trim() + ", Address1TalukaID =" + ddlTALUKA.SelectedValue.Trim() + ",  ";
                        str += " Address1VillageID =" + ddlVILLAGE.SelectedValue.Trim() + ", Address1PinCode ='" + txtPostPin.Text.Trim() + "', IsBothAddressSame ='0' ";
                        if (rdoLandStatus.SelectedValue.Trim() == "NO")
                        {
                            str += " , Work_City_ID=" + ddlDISTRICT.SelectedValue.Trim() + ", Work_TalukaID=" + ddlTALUKA.SelectedValue.Trim() + ", Work_VillageID=" + ddlVILLAGE.SelectedValue.Trim() + " ";
                            if (Work_ClustersMasterID.Trim().Length > 0)
                            {
                                str += " , Work_ClustersMasterID=" + Work_ClustersMasterID.Trim() + "";
                            }
                            if (Work_CircleID.Trim().Length > 0)
                            {
                                str += " , Work_CircleID=" + Work_CircleID.Trim() + "";
                            }
                        }

                        if (CastCategoryDoc.Trim().Length > 0)
                        {
                            str += " ,CastCategoryDoc ='" + CastCategoryDoc + "' ";
                        }
                        if (PhysicallyHandicapDoc.Trim().Trim().Length > 0)
                        {
                            str += " ,PhysicallyHandicapDoc ='" + PhysicallyHandicapDoc.Trim() + "' ";
                        }
                        if (ddlPOST.SelectedIndex > 0)
                        {
                            str += " , Address1Post_ID =" + ddlPOST.SelectedValue.Trim() + "";
                        }


                        str += " WHERE(RegistrationID = " + ViewState["RegistrationID"].ToString().Trim() + ")";
                        cla.ExecuteCommand(str, command);
                        txtAADHARNo.ReadOnly = true;
                        txtName.ReadOnly = true;
                    }


                    transaction.Commit();
                    //clsMessages.Sucessmsg(LiteralMsg, "S");


                    multiViewEmployee.ActiveViewIndex = 2;
                    Menu1.Items[2].Enabled = true;
                    Literal51.Visible = true;

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
                    // clsMessages.Errormsg(LiteralMsg, "Please provide correct input." + ex.Message.Trim());
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

        #region " Save Land"

        protected void rdoLandStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //divLandLess
            if (rdoLandStatus.SelectedValue.Trim() == "YES")
            {
                divLand.Visible = true;
                divLandLess.Visible = false;
            }
            else
            {
                divLand.Visible = false;
                divLandLess.Visible = true;

                cla.ExecuteCommand("UPDATE Tbl_M_RegistrationLand SET IsDeleted='1'  where  RegistrationID = " + ViewState["RegistrationID"].ToString().Trim() + "");
                String str = " UPDATE  Tbl_M_RegistrationDetails SET   LandStatus ='" + rdoLandStatus.SelectedItem.Value.Trim() + "' , LandLessCertificate=NULL ";
                str += " WHERE (RegistrationID = " + ViewState["RegistrationID"].ToString().Trim() + ")";
                cla.ExecuteCommand(str);
            }



        }
        protected void btnADD_Click(object sender, EventArgs e)
        {
            LiteralMsg.Text = "";

            if (ddlLANDVILLAGE.SelectedValue.Trim() == "0")
            {
                // clsMessages.Warningmsg(LiteralMsg, "Please Add 8-A KHATA KRAMANK.");
                Util.ShowMessageBox(this.Page, "Error", "Please Select Village from list", "error");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                return;
            }

            if (txtSURVEYNo8A.Text.Trim().Length == 0)
            {
                // clsMessages.Warningmsg(LiteralMsg, "Please Add 8-A KHATA KRAMANK.");
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG35", Session["Lang"].ToString()), "error");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                return;
            }


            //" + txtLANDAREA8AH.Text.Trim() + "," + txtLANDAREA8AA.Text.Trim() + "
            if (txtLANDAREA8AH.Text.Trim().Length == 0)
            {
                // clsMessages.Warningmsg(LiteralMsg, "Please Add 8-A KHATA KRAMANK.");
                Util.ShowMessageBox(this.Page, "Error", "जमीन मोजणी आवश्यक आहे", "error");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                return;
            }
            if (txtLANDAREA8AA.Text.Trim().Length == 0)
            {
                // clsMessages.Warningmsg(LiteralMsg, "Please Add 8-A KHATA KRAMANK.");
                Util.ShowMessageBox(this.Page, "Error", "जमीन मोजणी आवश्यक आहे", "error");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                return;
            }

            if (Convert.ToDouble(txtLANDAREA8AH.Text.Trim()) + Convert.ToDouble(txtLANDAREA8AA.Text.Trim()) == 0)
            {
                Util.ShowMessageBox(this.Page, "Error", "जमीन मोजणी आवश्यक आहे", "error");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                return;
            }

            if (FileFORM8A.HasFile == false)
            {
                //clsMessages.Warningmsg(LiteralMsg, "Please attach Form 8A.");
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG36", Session["Lang"].ToString()), "error");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                return;

            }
            else
            {
                if (FileFORM8A.PostedFile.ContentLength > 3145728)
                {
                    //clsMessages.Warningmsg(LiteralMsg, "Please attach max 3 MB file for Form 8A.");
                    Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG37", Session["Lang"].ToString()), "error");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                    return;
                }

                string FileError = MyCommanClass.CheckAllowedFileExtension(System.IO.Path.GetExtension(FileFORM8A.PostedFile.FileName.ToString()));
                if (FileError.Length > 0)
                {
                    Util.ShowMessageBox(this.Page, "Error", FileError, "error");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                    return;
                }
                FileError = Util.CheckAllowedFileName(FileFORM8A);
                if (FileError.Length > 0)
                {
                    Util.ShowMessageBox(this.Page, "Error", FileError, "error");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                    return;
                }
            }

            String s = cla.GetExecuteScalar("SELECT top 1 LandID FROM Tbl_M_RegistrationLand where RegistrationID=" + ViewState["RegistrationID"].ToString().Trim() + " and City_ID=" + ddlLANDDISTRICT.SelectedValue.Trim() + " and TalukaID=" + ddlLANDTALUKA.SelectedValue.Trim() + "  and VillageID=" + ddlLANDVILLAGE.SelectedValue.Trim() + " and IsDeleted is null");
            if (s.Length > 0)
            {
                // clsMessages.Warningmsg(LiteralMsg, "Only one 8-A is allowed per village.");
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG38", Session["Lang"].ToString()), "error");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                return;
            }

            String str = "";
            int LandID = cla.TableID("Tbl_M_RegistrationLand", "LandID");


            string filepath = "";
            if (FileFORM8A.HasFile == true)
            {
                if (ViewState["RegistrationID"].ToString().Trim().Length > 0)
                {
                    //String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + ViewState["RegistrationID"].ToString().Trim() + "");
                    String PathUp = "DocMasters/MemberDoc/" + ViewState["RegistrationID"].ToString().Trim() + "";

                    if (FileFORM8A.HasFile)
                    {
                        if (FileFORM8A.PostedFile.ContentLength < 3145728)
                        {
                            String fileName = "FORM8A_" + LandID.ToString() + "" + System.IO.Path.GetExtension(FileFORM8A.PostedFile.FileName.ToString());
                            // String Uppath = path + fileName;
                            //FileFORM8A.PostedFile.SaveAs(Uppath.Trim()); //File is saved in the Physical folder 
                            filepath = "/admintrans/DocMasters/MemberDoc/" + ViewState["RegistrationID"].ToString().Trim() + "/" + fileName;

                            byte[] bin = FileFORM8A.FileBytes;

                            //fileRet.UploadData(PathUp, fileName, bin);
                            String ret = fileRet.UploadData(PathUp, fileName, bin);
                            if (ret.Trim().Length > 0)
                            {
                                Util.ShowMessageBox(this.Page, "Error", "Please upload FORM8A", "error");
                                return;
                            }
                        }
                    }
                }

            }


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
                        str = " INSERT INTO Tbl_M_RegistrationLand (LandID, RegistrationID, City_ID, TalukaID, VillageID, AccountNumber8A, Hectare8A, Are8A, Form8ADoc)";
                        str += " VALUES(" + LandID + "," + ViewState["RegistrationID"].ToString().Trim() + "," + ddlLANDDISTRICT.SelectedValue.Trim() + "," + ddlLANDTALUKA.SelectedValue.Trim() + "," + ddlLANDVILLAGE.SelectedValue.Trim() + ",'" + txtSURVEYNo8A.Text.Trim() + "'," + txtLANDAREA8AH.Text.Trim() + "," + txtLANDAREA8AA.Text.Trim() + ",'" + filepath + "')";
                        cla.ExecuteCommand(str, command);
                        div7A.Visible = true;
                        btnADD.Enabled = false;
                        rdoLandStatus.Enabled = false;

                        str = " UPDATE  Tbl_M_RegistrationDetails SET   LandStatus ='" + rdoLandStatus.SelectedItem.Value.Trim() + "' ";
                        str += " WHERE(RegistrationID = " + ViewState["RegistrationID"].ToString().Trim() + ")";
                        cla.ExecuteCommand(str, command);


                    }


                    transaction.Commit();

                    DataTable dt = cla.GetDataTable("SELECT LandID, AccountNumber8A FROM Tbl_M_RegistrationLand where RegistrationID=" + ViewState["RegistrationID"].ToString() + " and IsDeleted is null  and AccountNumber8A is not null  Order by LandID");
                    ddl8A.DataSource = dt;
                    ddl8A.DataTextField = "AccountNumber8A";
                    ddl8A.DataValueField = "LandID";
                    ddl8A.DataBind();
                    ddl8A.Items.Insert(0, new ListItem("--Select--", "0"));
                    if (ddl8A.Items.Count == 1)
                    {
                        ddl8A.SelectedIndex = 1;
                    }
                    else
                        ddl8A.SelectedIndex = 0;

                    //multiViewEmployee.ActiveViewIndex = 3;
                    //Menu1.Items[3].Enabled = true;

                    fillLandDetails();
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

        protected void btnAdd712_Click(object sender, EventArgs e)
        {
            LiteralMsg.Text = "";
            if (ddl8A.Items.Count == 0)
            {
                //clsMessages.Warningmsg(LiteralMsg, "Please Add Form 8A first then you can add multiple 7/12 under Form 8A");
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG39", Session["Lang"].ToString()), "error");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                return;
            }
            else if (ddl8A.SelectedIndex == 0)
            {
                //clsMessages.Warningmsg(LiteralMsg, "Please Select Form 8A first then you can add multiple 7/12 under Form 8A");
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG40", Session["Lang"].ToString()), "error");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                return;
            }

            if (File712.HasFile == false)
            {
                //clsMessages.Warningmsg(LiteralMsg, "Please attach 7 / 12.");
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG41", Session["Lang"].ToString()), "error");

            }
            else
            {
                if (File712.PostedFile.ContentLength > 3145728)
                {
                    // clsMessages.Warningmsg(LiteralMsg, "Please attach max 3 MB file for 7 / 12.");
                    Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG42", Session["Lang"].ToString()), "error");
                }

                string FileError = MyCommanClass.CheckAllowedFileExtension(System.IO.Path.GetExtension(File712.PostedFile.FileName.ToString()));
                if (FileError.Length > 0)
                {
                    Util.ShowMessageBox(this.Page, "Error", FileError, "error");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                    return;
                }
                FileError = Util.CheckAllowedFileName(FileFORM8A);
                if (FileError.Length > 0)
                {
                    Util.ShowMessageBox(this.Page, "Error", FileError, "error");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                    return;
                }
            }

            int LandID = cla.TableID("Tbl_M_RegistrationLand", "LandID");
            //String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + ViewState["RegistrationID"].ToString().Trim() + "");
            String PathUp = "DocMasters/MemberDoc/" + ViewState["RegistrationID"].ToString().Trim() + "";

            if (File712.HasFile)
            {
                string filepath = "";

                if (File712.PostedFile.ContentLength < 3145728)
                {


                    String fileName = "SURVEYNUMBER712_" + LandID.ToString() + "" + System.IO.Path.GetExtension(File712.PostedFile.FileName.ToString());
                    // String Uppath = path + fileName;
                    //File712.PostedFile.SaveAs(Uppath.Trim()); //File is saved in the Physical folder 
                    filepath = "/admintrans/DocMasters/MemberDoc/" + ViewState["RegistrationID"].ToString().Trim() + "/" + fileName;

                    byte[] bin = File712.FileBytes;
                    // String fileName = "InspectionImage" + WorkCompletionID.ToString() + "" + System.IO.Path.GetExtension(File712.PostedFile.FileName.ToString());
                    //fileRet.UploadData(PathUp, fileName, bin);

                    String ret = fileRet.UploadData(PathUp, fileName, bin);
                    if (ret.Trim().Length > 0)
                    {
                        Util.ShowMessageBox(this.Page, "Error", "Please upload 712", "error");
                        return;
                    }


                    int ErrorCount = 0;

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
                            LandID = cla.TableID("Tbl_M_RegistrationLand", "LandID", command);
                            String str = " INSERT INTO Tbl_M_RegistrationLand ( LandID, ParentLandID , SurveyNo712, Hectare712, Are712, Extracts712Doc,RegistrationID)";
                            str += " VALUES(" + LandID + "," + ddl8A.SelectedValue.Trim() + ",'" + txtSURVEYNo712.Text.Trim() + "','" + txtLANDAREA712H.Text.Trim() + "','" + txtLANDAREA712A.Text.Trim() + "','" + filepath + "'," + ViewState["RegistrationID"].ToString().Trim() + ")";
                            cla.ExecuteCommand(str, command);

                            String s = cla.GetExecuteScalar("SELECT dbo.fn_GetRegisteredLandDeff(" + ViewState["RegistrationID"].ToString().Trim() + ")", command);
                            if (s.Length > 0)
                            {
                                if (Convert.ToDouble(s) > 0)
                                {
                                    ErrorCount++;
                                }
                            }



                            if (ErrorCount == 0)
                            {
                                transaction.Commit();
                                txtSURVEYNo712.Text = "";
                                txtLANDAREA712H.Text = "";
                                txtLANDAREA712A.Text = "";

                                div7A.Visible = true;
                                btnADD.Enabled = true;
                                fillLandDetails();
                                btnLandNext.Visible = true;

                                ddlLANDVILLAGE.SelectedIndex = 0;
                                ddlLANDTALUKA.SelectedIndex = 0;
                                ddlLANDDISTRICT.SelectedIndex = 0;
                            }
                            else
                            {
                                try
                                {
                                    transaction.Rollback();

                                }
                                catch
                                {

                                }
                                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG43", Session["Lang"].ToString()), "error");
                                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);


                            }



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
                            // clsMessages.Errormsg(LiteralMsg, "Please provide correct input." + ex.Message.Trim());
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
            }



        }


        private void fillLandDetails()
        {
            List<String> lst = new List<string>();
            lst.Clear();
            lst.Add(ViewState["RegistrationID"].ToString().Trim());
            DataTable dt = cla.GetDtByProcedure("SP_GetLandParentDetails", lst);
            grdSubject.DataSource = dt;
            grdSubject.DataBind();
        }

        protected void btnLandNext_Click(object sender, EventArgs e)
        {
            LiteralMsg.Text = "";
            int ErrorCount = 0;
            if (rdoLandStatus.SelectedValue.Trim() == "YES")
            {
                if (grdSubject.Rows.Count == 0)
                {
                    Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG35", Session["Lang"].ToString()), "error");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                    return;
                }

                String s = cla.GetExecuteScalar("SELECT dbo.fn_GetRegisteredLandDeff(" + ViewState["RegistrationID"].ToString().Trim() + ")");
                if (s.Length > 0)
                {
                    if (Convert.ToDouble(s) > 0)
                    {
                        ErrorCount++;
                    }
                }
            }


            //Sum of hectare & are from 7 / 12 will be equal to hectare & are of 8A respectively.



            if (rdoLandStatus.SelectedValue.Trim() != "YES")// else 
            {

                if (FileLandLessCertificate.HasFile == false)
                {

                    Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG28", Session["Lang"].ToString()), "error");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                    return;

                }
                else
                {
                    if (FileLandLessCertificate.PostedFile.ContentLength > 3145728)
                    {

                        Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG29", Session["Lang"].ToString()), "error");
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                        return;
                    }

                    string FileError = MyCommanClass.CheckAllowedFileExtension(System.IO.Path.GetExtension(FileLandLessCertificate.PostedFile.FileName.ToString()));
                    if (FileError.Length > 0)
                    {
                        Util.ShowMessageBox(this.Page, "Error", FileError, "error");
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                        return;
                    }

                }
            }

            String PathUp = "DocMasters/MemberDoc/" + ViewState["RegistrationID"].ToString().Trim() + "";

            String LandLessCertificate = "";
            if (FileLandLessCertificate.HasFile)
            {
                if (FileLandLessCertificate.PostedFile.ContentLength < 3145728)
                {
                    LandLessCertificate = "/admintrans/DocMasters/MemberDoc/" + ViewState["RegistrationID"].ToString().Trim() + "/LandLessCertificate" + System.IO.Path.GetExtension(FileLandLessCertificate.PostedFile.FileName.ToString());

                    byte[] bin = FileLandLessCertificate.FileBytes;
                    String fileName = "LandLessCertificate" + System.IO.Path.GetExtension(FileLandLessCertificate.PostedFile.FileName.ToString());

                    String ret = fileRet.UploadData(PathUp, fileName, bin);
                    if (ret.Trim().Length > 0)
                    {
                        Util.ShowMessageBox(this.Page, "Error", "Please upload Land Less Certificate", "error");
                        return;
                    }

                    LabelLandlessCert.Text = "<a href='" + clsSettings.BaseUrl + "" + LandLessCertificate + "' target=_blank >  VIEW UPLOADED CERITIFICATE  </a>";
                }
            }


            if (ErrorCount == 0)
            {




                String str = "";

                DataTable dtLand = cla.GetDataTable("SELECT  TOP (1) L.VillageID, V.City_ID, V.ClustersMasterID, V.TalukaID, V.CircleID FROM Tbl_M_RegistrationLand AS L INNER JOIN Tbl_M_VillageMaster AS V ON V.VillageID = L.VillageID WHERE (L.RegistrationID = " + ViewState["RegistrationID"].ToString().Trim() + ") AND (L.ParentLandID IS NULL) and L.isdeleted is null AND L.VillageID <> 0 ORDER BY L.LandID");
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





                        str = " UPDATE  Tbl_M_RegistrationDetails SET   LandStatus ='" + rdoLandStatus.SelectedItem.Value.Trim() + "' ";
                        str += " WHERE (RegistrationID = " + ViewState["RegistrationID"].ToString().Trim() + ")";
                        cla.ExecuteCommand(str, command);


                        if (rdoLandStatus.SelectedValue.Trim() == "YES")
                        {
                            if (dtLand.Rows.Count > 0)
                            {
                                for (int x = 0; x != dtLand.Rows.Count; x++)
                                {
                                    str = " UPDATE  Tbl_M_RegistrationDetails SET Work_City_ID=" + dtLand.Rows[x]["City_ID"].ToString() + ", Work_TalukaID=" + dtLand.Rows[x]["TalukaID"].ToString() + ", Work_VillageID=" + dtLand.Rows[x]["VillageID"].ToString() + " ";
                                    if (dtLand.Rows[x]["ClustersMasterID"].ToString().Trim().Length > 0)
                                    {
                                        str += " , Work_ClustersMasterID=" + dtLand.Rows[x]["ClustersMasterID"].ToString() + "";
                                    }
                                    if (dtLand.Rows[x]["CircleID"].ToString().Trim().Length > 0)
                                    {
                                        str += " , Work_CircleID=" + dtLand.Rows[x]["CircleID"].ToString().Trim() + "";
                                    }
                                    str += " WHERE (RegistrationID = " + ViewState["RegistrationID"].ToString().Trim() + ")";
                                    cla.ExecuteCommand(str, command);
                                }
                            }
                            else
                            {
                                //str = " UPDATE  Tbl_M_RegistrationDetails SET  Work_City_ID =NULL, Work_TalukaID =NULL, Work_VillageID =NULL, Work_CircleID =NULL, Work_ClustersMasterID =NULL ";
                                //str += " WHERE (RegistrationID = " + ViewState["RegistrationID"].ToString().Trim() + ")";
                            }
                        }



                        transaction.Commit();
                        multiViewEmployee.ActiveViewIndex = 3;
                        Menu1.Items[3].Enabled = true;
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
                        // clsMessages.Errormsg(LiteralMsg, "Please provide correct input." + ex.Message.Trim());
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
            else
            {
                //clsMessages.Warningmsg(LiteralMsg, "Sum of hectare & are from 7/12 will be equal to hectare & are of 8A respectively.");
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG43", Session["Lang"].ToString()), "error");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                return;
            }
        }


        protected void grdSubject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string ParentLandID = grdSubject.DataKeys[e.Row.RowIndex]["LandID"].ToString();
                GridView SC = (GridView)e.Row.FindControl("grdChild");


                DataTable dt = cla.GetDataTable("SELECT   LandID , SurveyNo712, Hectare712, Are712,  '<a target=_blank href=''https://dbtpocradata.blob.core.windows.net'+Extracts712Doc+'''> View Extracts  </a>' as Extracts712Doc FROM  Tbl_M_RegistrationLand WHERE  (ParentLandID = " + ParentLandID + ") AND (IsDeleted IS NULL) AND RegistrationID=" + ViewState["RegistrationID"].ToString().Trim() + " ");
                SC.DataSource = dt;
                SC.DataBind();
            }
        }

        protected void grdSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LandID
            List<string> lst = new List<string>();
            lst.Add(grdSubject.SelectedDataKey["LandID"].ToString());
            cla.ExecuteByProcedure("SP_Remove_LandDetails", lst);
            fillLandDetails();
            LiteralMsg.Text = "";

            DataTable dtLandCheck = cla.GetDataTable("SELECT  TOP (1) L.VillageID, V.City_ID, V.ClustersMasterID, V.TalukaID, V.CircleID FROM Tbl_M_RegistrationLand AS L INNER JOIN Tbl_M_VillageMaster AS V ON V.VillageID = L.VillageID WHERE (L.RegistrationID = " + ViewState["RegistrationID"].ToString().Trim() + ") AND (L.ParentLandID IS NULL) and L.IsDeleted is null ORDER BY L.LandID");
            if (dtLandCheck.Rows.Count == 0)
            {
                rdoLandStatus.SelectedValue = "NO";
                rdoLandStatus_SelectedIndexChanged(null, null);
            }

        }

        protected void grdChild_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LandID
            GridView gv = (GridView)sender;
            List<string> lst = new List<string>();
            lst.Add(gv.SelectedDataKey["LandID"].ToString());
            cla.ExecuteByProcedure("SP_Remove_LandDetails", lst);
            fillLandDetails();
            LiteralMsg.Text = "";

            DataTable dtLandCheck = cla.GetDataTable("SELECT  TOP (1) L.VillageID, V.City_ID, V.ClustersMasterID, V.TalukaID, V.CircleID FROM Tbl_M_RegistrationLand AS L INNER JOIN Tbl_M_VillageMaster AS V ON V.VillageID = L.VillageID WHERE (L.RegistrationID = " + ViewState["RegistrationID"].ToString().Trim() + ") AND (L.ParentLandID IS NULL) and L.IsDeleted is null ORDER BY L.LandID");
            if (dtLandCheck.Rows.Count == 0)
            {
                rdoLandStatus.SelectedValue = "NO";
                rdoLandStatus_SelectedIndexChanged(null, null);
            }

        }


        #endregion

        #region "Save Declretion"

        protected void btnFinalSave_Click(object sender, EventArgs e)
        {
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
            if (CheckBox4.Checked == false)
            {
                IsAcept = false;
            }
            if (CheckBox5.Checked == false)
            {
                IsAcept = false;
            }
            if (CheckBox6.Checked == false)
            {
                IsAcept = false;
            }

            DataTable dtLandCheck = cla.GetDataTable("SELECT  TOP (1) L.VillageID, V.City_ID, V.ClustersMasterID, V.TalukaID, V.CircleID FROM Tbl_M_RegistrationLand AS L INNER JOIN Tbl_M_VillageMaster AS V ON V.VillageID = L.VillageID WHERE (L.RegistrationID = " + ViewState["RegistrationID"].ToString().Trim() + ") AND (L.ParentLandID IS NULL) ORDER BY L.LandID");
            if (rdoLandStatus.SelectedValue.Trim() == "YES")
            {
                if (dtLandCheck.Rows.Count == 0)
                {
                    Util.ShowMessageBox(this.Page, "Error", "Please Add Land Details", "error");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                    return;
                }

            }


            //if (CheckBox7.Checked == false)
            //{
            //    IsAcept = false;
            //}
            //if (CheckBox8.Checked == false)
            //{
            //    IsAcept = false;
            //}

            if (IsAcept == false)
            {
                //clsMessages.Warningmsg(LiteralMsg, "Please accept all Declarations to register under Nanaji Deshmukh Krishi Sanjivani Prakalp");
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG9", Session["Lang"].ToString()), "error");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                return;
            }


            //------------End validations ---------------------// 


            String str = "", RegistrationNo = "";
            int UserId = cla.TableID("Tbl_M_LoginDetails", "UserId");
            if (ViewState["RegistrationID"].ToString().Trim().Length > 0)
            {
                String GPCode = cla.GetExecuteScalar("SELECT TOP 1 V.VillageCode FROM Tbl_M_RegistrationDetails AS R INNER JOIN Tbl_M_VillageMaster AS V ON R.Work_VillageID = V.VillageID WHERE (R.RegistrationID = " + ViewState["RegistrationID"].ToString().Trim() + ") ");
                String RuningNo = cla.GetExecuteScalar("Select Max(RegistrationNo) from Tbl_M_RegistrationDetails where Tbl_M_RegistrationDetails.Work_VillageID =(SELECT Work_VillageID FROM Tbl_M_RegistrationDetails AS R  WHERE (R.RegistrationID =" + ViewState["RegistrationID"].ToString().Trim() + "))");
                if (RuningNo.Trim().Length > 0)
                {
                    RegistrationNo = (Convert.ToDouble(RuningNo) + 1).ToString();
                }
                else
                {
                    RegistrationNo = GPCode + "0001";
                }

            }
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
                        str = " UPDATE  Tbl_M_RegistrationDetails SET   IAgree ='1' , IsDeleted=NULL ,UserID='" + txtAADHARNo.Text.Trim() + "' , RegistrationDate='" + cla.mdy(cla.SvrDate()) + "'  ";
                        str += " WHERE (RegistrationID = " + ViewState["RegistrationID"].ToString().Trim() + ")";
                        cla.ExecuteCommand(str, command);


                        str = " INSERT INTO Tbl_M_LoginDetails (UserId, RegistrationID, UserName, UPass, LoginAs, FullName)";
                        str += " VALUES(" + UserId + "," + ViewState["RegistrationID"].ToString().Trim() + ",'" + txtAADHARNo.Text.Trim() + "','" + txtAADHARNo.Text.Trim() + "','Beneficiary',N'" + txtName.Text.Trim() + "')";
                        cla.ExecuteCommand(str, command);
                    }


                    transaction.Commit();
                    try
                    {

                        Session["RegistrationIDPass"] = ViewState["RegistrationID"].ToString().Trim();
                        if (txtMobile1.Text.Trim().Length > 0)
                        {
                            SMS.SendSmsOnRegistration(txtMobile1.Text.Trim());
                        }

                        clsMessages.Sucessmsg2(LiteralMsg, MyCommanClass.GetMsgInEnForDB("MSG10", Session["Lang"].ToString()));
                    }
                    catch { }
                    Response.Redirect("RegisterSucess.aspx?" + EncryptDecryptQueryString.encrypt(string.Format("ID={0}", ViewState["RegistrationID"].ToString().Trim())), true);
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

        protected void rdoAuthenticationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtOtp.Visible = false;
            divBioType.Visible = false;

            LiteralCapcha.Visible = false;
            txtCapcha.Visible = false;

            if (rdoAuthenticationType.SelectedValue.Trim() == "OTP")
            {
                btnOtp.Visible = true;
                btnbio.Visible = false;

                LiteralCapcha.Visible = true;
                txtCapcha.Visible = true;

                Random a = new Random(Guid.NewGuid().GetHashCode());
                int firstNumber = a.Next(1, 9);
                int secondNumber = a.Next(1, 9);
                ViewState["spam"] = firstNumber + secondNumber;
                LiteralCapcha.Text = firstNumber.ToString() + " + " + secondNumber.ToString();
            }
            else
            {
                btnOtp.Visible = false;
                btnbio.Visible = true;
                divBioType.Visible = true;
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> Test();  </script>", false);
            }
        }





        [WebMethod]
        public static String GetData(String result, String ANumber)
        {
            HttpContext.Current.Session["UDIxml"] = result;

            return "";
        }
        protected void btnBioKyc_Click(object sender, EventArgs e)
        {
            String result = HttpContext.Current.Session["UDIxml"].ToString();
            if (cla.ExecuteCommand("update Tbl_M_SMSCount set UserNo='BIO-" + txtAADHARNo.Text.Trim() + "' where SMSCountID in (Select top 1 Tbl_M_SMSCount.SMSCountID from Tbl_M_SMSCount where Tbl_M_SMSCount.MobileNo='" + txtMobileOtp.Text.Trim() + "' order by Tbl_M_SMSCount.SMSCountID desc)").Trim().Length != 0)
            {
                cla.ExecuteCommand("INSERT INTO Tbl_M_SMSCount (UserNo) VALUES ('AJAX CALL')");
            }
            String ret = "", OtpErrorCode = "";
            try
            {

                XmlDocument xdocl = new XmlDocument();
                xdocl.LoadXml(result);
                String Skey = "", Data = "", Hmac = "", rdsId = "", rdsVer = "", dpId = "", dc = "", mi = "", mc = "", OtpRet = "", keyExp = "";

                XmlNodeList xResp = xdocl.SelectNodes("/PidData/Resp");
                foreach (XmlNode xNode in xResp)
                {
                    OtpRet = xNode.Attributes["errInfo"].InnerText;

                }

                foreach (XmlNode xNode in xdocl)
                {

                    foreach (XmlNode CNode in xNode)
                    {
                        //if (CNode.Name.Trim() == "Skey")
                        //{
                        //    Skey = CNode.InnerText;
                        //}
                        if (CNode.Name.Trim() == "Skey")
                        {
                            Skey = CNode.InnerText;
                            try
                            {
                                keyExp = CNode.Attributes["ci"].InnerText;//20250923
                            }
                            catch { }
                        }
                        if (CNode.Name.Trim() == "Data")
                        {
                            Data = CNode.InnerText;
                        }
                        if (CNode.Name.Trim() == "Hmac")
                        {
                            Hmac = CNode.InnerText;
                        }
                    }
                }
                XmlNodeList xnDeviceInfo = xdocl.SelectNodes("/PidData/DeviceInfo");
                foreach (XmlNode Cxn in xnDeviceInfo)
                {

                    if (Cxn.Attributes["dpId"] != null)
                        dpId = Cxn.Attributes["dpId"].InnerText;

                    if (Cxn.Attributes["rdsId"] != null)
                        rdsId = Cxn.Attributes["rdsId"].InnerText;

                    if (Cxn.Attributes["rdsVer"] != null)
                        rdsVer = Cxn.Attributes["rdsVer"].InnerText;

                    if (Cxn.Attributes["dc"] != null)
                        dc = Cxn.Attributes["dc"].InnerText;

                    if (Cxn.Attributes["mi"] != null)
                        mi = Cxn.Attributes["mi"].InnerText;

                    if (Cxn.Attributes["mc"] != null)
                        mc = Cxn.Attributes["mc"].InnerText;

                }



                String UID = txtAADHARNo.Text.Trim();
                String s1 = new MyClass().GetSqlUnikNO("3");
                String Txn = "UKC:" + s1 + DateTime.Now.ToString("yyyyMMddHHmmssfff");


                clsCrypto cls = new global::clsCrypto();
                String Password = "testndks@123";

                byte[] key = MOLCryptoEngine.MOLSecurity.GenerateKey(256);
                byte[] IV = MOLSecurity.GenerateIV(128, "uidaimaharashtra");
                string EncryptedUID = MOLSecurity.AESEncrypt(UID, IV, key);// Convert.ToBase64String(encrypted);
                string PasswordUID = MOLSecurity.AESEncrypt(Password, IV, key);// Convert.ToBase64String(encryptedPassword);
                String Certificate = System.Web.Hosting.HostingEnvironment.MapPath("~/AdminTrans/UserControls/Production/JME.cer");
                string incodedkey = Convert.ToBase64String(key);
                String AUASkey = MOLSecurity.EncryptWithPublicKey(incodedkey, Certificate);// EncryptCertificate(Encoding.ASCII.GetBytes(incodedkey), cert.GetRawCertData());
                string HA256KeyValue = cls.generateSha256Hash(incodedkey);
                string HA256saTxn = cls.generateSha256Hash(Txn);
                String aa = PasswordUID + "~" + EncryptedUID + "~" + HA256KeyValue + "~" + HA256saTxn;
                aa = aa.ToLower();

                String Token = cls.generateSha256Hash(aa);// ComputeSha256Hash(aa);


                String str = "<KUAData xmlns=" + MyCommanClass.PutIntoQuotes("http://kua.maharashtra.gov.in/kyc/gom-kyc-request") + ">";
                str += "<uid>" + EncryptedUID + "</uid>";
                str += "<appCode>KYCApp</appCode>";
                str += "<Token>" + Token.Trim() + "</Token>";
                str += "<KUASkey>" + AUASkey.Trim() + "</KUASkey>";
                str += "<sa>PNDKS23054</sa>";
                str += "<saTxn>" + Txn + "</saTxn>";
                str += "<Data type =" + MyCommanClass.PutIntoQuotes("X") + ">" + Data + "</Data>";
                str += "<Hmac>" + Hmac + "</Hmac>";
                //str += "<Skey ci =" + MyCommanClass.PutIntoQuotes("20191230") + ">" + Skey + "</Skey>";
                //str += "<Skey ci =" + MyCommanClass.PutIntoQuotes("20250923") + ">" + Skey + "</Skey>";
                if (keyExp.Trim() == "20191230")
                {
                    str += "<Skey ci =" + MyClass.PutIntoQuotes("20191230") + ">" + Skey + "</Skey>";
                }
                else
                {
                    str += "<Skey ci =" + MyClass.PutIntoQuotes("20250923") + ">" + Skey + "</Skey>";
                }
                str += "<Uses pi =" + MyCommanClass.PutIntoQuotes("n") + " pa =" + MyCommanClass.PutIntoQuotes("n") + " pfa =" + MyCommanClass.PutIntoQuotes("n") + "  bio =" + MyCommanClass.PutIntoQuotes("y") + " bt=" + MyCommanClass.PutIntoQuotes("FMR,FIR") + " pin=" + MyCommanClass.PutIntoQuotes("n") + " otp=" + MyCommanClass.PutIntoQuotes("n") + "/>";
                str += "<Meta rdsId =" + MyCommanClass.PutIntoQuotes(rdsId) + " rdsVer =" + MyCommanClass.PutIntoQuotes(rdsVer) + " dpId =" + MyCommanClass.PutIntoQuotes(dpId) + " dc =" + MyCommanClass.PutIntoQuotes(dc) + " mi =" + MyCommanClass.PutIntoQuotes(mi) + " mc =" + MyCommanClass.PutIntoQuotes(mc) + " />";
                str += "<type>A</type>";
                str += "<rc>Y</rc>";
                str += "<lr>Y</lr>";
                str += "<pfr>N</pfr>";
                str += "</KUAData>";



                //string url = "https://kuaqa.maharashtra.gov.in/KUA/rest/kycreq";// 
                string url = "https://kua25.maharashtra.gov.in/kua25/KUA/rest/kycreq";

                String Conn = str;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                byte[] requestBytes = System.Text.Encoding.ASCII.GetBytes(Conn);
                req.Method = "POST";
                req.ContentType = "text/xml";
                req.ContentLength = requestBytes.Length;
                Stream requestStream = req.GetRequestStream();
                requestStream.Write(requestBytes, 0, requestBytes.Length);
                requestStream.Close();
                //HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                string responseXml = "";
                using (HttpWebResponse res = (HttpWebResponse)req.GetResponse())
                {
                    // Do your processings here....
                    StreamReader sr = new StreamReader(res.GetResponseStream());
                    responseXml = sr.ReadToEnd();
                    res.Dispose();
                }


                XmlDocument xml = new XmlDocument();
                xml.LoadXml(responseXml); // suppose that myXmlString contains "<Names>...</Names>"
                XmlNodeList xdocls = xml.SelectNodes("/KuaRes");
                foreach (XmlNode xNode in xdocls)
                {
                    OtpRet = xNode.Attributes["ret"].InnerText;
                    if (xNode.Attributes["err"] != null)
                        OtpErrorCode = xNode.Attributes["err"].InnerText;
                }

                if (OtpRet.Trim().ToUpper() == "Y")
                {

                    txtAADHARNo.ReadOnly = true;
                    txtOtp.Visible = false;
                    btnAuthAudhar.Visible = false;
                    btnbio.Visible = false;
                    string email = "";
                    string phone = "";
                    string gender = "";
                    string dob = "";
                    string name = "";
                    String Pht = "";

                    XmlNodeList xnList = xml.SelectNodes("/KuaRes/UidData");
                    foreach (XmlNode xn in xnList)
                    {
                        foreach (XmlNode Cxn in xn)
                        {
                            if (Cxn.Name == "Poi")
                            {
                                if (Cxn.Attributes["email"] != null)
                                    email = Cxn.Attributes["email"].InnerText;
                                if (Cxn.Attributes["phone"] != null)
                                    phone = Cxn.Attributes["phone"].InnerText;
                                if (Cxn.Attributes["gender"] != null)
                                    gender = Cxn.Attributes["gender"].InnerText;
                                if (Cxn.Attributes["dob"] != null)
                                    dob = Cxn.Attributes["dob"].InnerText;
                                if (Cxn.Attributes["name"] != null)
                                    if (name.Trim().Length == 0)
                                        name = Cxn.Attributes["name"].InnerText;
                            }
                            if (Cxn.Name == "LData")
                            {
                                if (Cxn.Attributes["name"] != null)
                                    if (txtNameInMarathi.Text.Trim().Length == 0)
                                        txtNameInMarathi.Text = Cxn.Attributes["name"].InnerText;
                            }
                            if (Cxn.Name == "Pht")
                            {
                                Pht = Cxn.InnerText;
                                if (Pht.Length > 0)
                                {
                                    byte[] bytes = Convert.FromBase64String(Pht);
                                    ImageTagId.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(bytes);
                                    Literal5.Text = "";
                                }
                            }
                        }

                    }

                    // show records 
                    txtName.Text = name;
                    if (dob.Length > 0)
                    {
                        if (dob.Contains("-"))
                        {
                            try
                            {
                                String[] s = dob.Split('-');
                                ddlDay.SelectedValue = Convert.ToInt32(s[0].ToString()).ToString();
                                ddlMonth.SelectedValue = Convert.ToInt32(s[1].ToString()).ToString();
                                ddlYear.SelectedValue = Convert.ToInt32(s[2].ToString()).ToString();
                                ddlDay.Enabled = false;
                                ddlMonth.Enabled = false;
                                ddlYear.Enabled = false;
                            }
                            catch { }
                        }
                    }
                    if (gender.Length > 0)
                    {
                        if (gender.Trim() == "M")
                        {
                            rdoGender.SelectedValue = "Male";
                            rdoGender.SelectedIndex = 1;
                        }
                        if (gender.Trim() == "F")
                        {
                            rdoGender.SelectedValue = "Female";
                            rdoGender.SelectedIndex = 2;
                        }
                        else if (gender.Trim() == "T")
                        {
                            rdoGender.SelectedValue = "Other";
                            rdoGender.SelectedIndex = 3;
                        }
                        rdoGender.Enabled = false;
                    }

                    try
                    {
                        NPCIResponce cl = NPCIClass.InvokeService(txtAADHARNo.Text.Trim(), "");
                        if (cl.status != "A")
                        {
                            //Adhar not link
                            Literal51.Text = " <div class='alert alert-danger alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'></span></button> <strong>Alert!</strong>  <b> Aadhaar is not linked with your Bank Account  </div>";
                        }
                        else if (cl.status == "A")
                        {
                            //Adhar not link
                            Literal51.Text = " <div class='alert alert-danger alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'></span></button> <strong>Alert!</strong>  <b> Aadhaar is linked with your Bank Account  </div>";
                        }

                    }
                    catch (Exception ex)
                    {
                        Util.LogErrorNNPCI(ex.ToString());
                    }

                    Util.ShowMessageBox(this.Page, "Success", "Aadhar verified Successfully, Please complete your registration.", "success");

                }
                else
                {
                    ret = UidiErrorCodes.GetErrorDetails(OtpErrorCode.Trim());



                }


            }
            catch (Exception ex)
            {
                ret = ex.ToString();  //Session["error"]


            }


        }




        public string PutIntoQuotes(string value)
        {
            return "\"" + value + "\"";
        }


        public void SendForAuthentication()
        {

            try
            {


                // SingXML(); "">
                String Txn = ViewState["Txn"].ToString().Trim();

                clsCrypto cls = new global::clsCrypto();
                String Password = "testndks@123";
                string UID = txtAADHARNo.Text.Trim();
                byte[] key = MOLCryptoEngine.MOLSecurity.GenerateKey(256);
                byte[] IV = MOLSecurity.GenerateIV(128, "uidaimaharashtra");
                string EncryptedUID = MOLSecurity.AESEncrypt(UID, IV, key);// Convert.ToBase64String(encrypted);
                string PasswordUID = MOLSecurity.AESEncrypt(Password, IV, key);// Convert.ToBase64String(encryptedPassword);
                String Certificate = System.Web.Hosting.HostingEnvironment.MapPath("~/AdminTrans/UserControls/Production/JME.cer");
                string incodedkey = Convert.ToBase64String(key);
                String AUASkey = MOLSecurity.EncryptWithPublicKey(incodedkey, Certificate);// EncryptCertificate(Encoding.ASCII.GetBytes(incodedkey), cert.GetRawCertData());
                string HA256KeyValue = cls.generateSha256Hash(incodedkey);

                string HA256saTxn = cls.generateSha256Hash(Txn);



                String aa = PasswordUID + "~" + EncryptedUID + "~" + HA256KeyValue + "~" + HA256saTxn;
                aa = aa.ToLower();

                String Token = cls.generateSha256Hash(aa);// ComputeSha256Hash(aa);



                String str = "<Auth xmlns=" + PutIntoQuotes("http://aua.maharashtra.gov.in/auth/gom-auth-request") + ">";
                str += "<Txn>" + Txn + "</Txn>";
                str += "<Ver>2.5</Ver>";
                str += "<SubAUACode>PNDKS23054</SubAUACode>";
                str += "<AUAToken>" + Token.Trim() + "</AUAToken>";
                str += "<AUASkey>" + AUASkey.Trim() + "</AUASkey>";
                str += "<ReqType>otp</ReqType>";
                str += "<DeviceId>UDC-AGRIGOM-0001</DeviceId>";
                str += "<UID>" + EncryptedUID + "</UID>";
                str += "<type>A</type>";
                str += "<Ch>01</Ch>";
                str += "</Auth> ";

                string url = "https://aua25.maharashtra.gov.in/aua25/aua/rest/authreqv2";// "https://auaqa.maharashtra.gov.in/aua/rest/authreqv2";


                //StreamReader sr1 = new StreamReader(Server.MapPath("~/TESTDATA/test-signed.xml").ToString());
                String Conn = str;// sr1.ReadToEnd();
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                byte[] requestBytes = System.Text.Encoding.ASCII.GetBytes(Conn);
                req.Method = "POST";
                req.ContentType = "text/xml";
                req.ContentLength = requestBytes.Length;
                Stream requestStream = req.GetRequestStream();
                requestStream.Write(requestBytes, 0, requestBytes.Length);
                requestStream.Close();
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                StreamReader sr = new StreamReader(res.GetResponseStream());
                string backstr = sr.ReadToEnd();
                sr.Close();
                res.Close();


                XmlDocument xdocl = new XmlDocument();
                xdocl.LoadXml(backstr);
                String OtpErrorCode = "", OtpRet = "", ResponseMsg = "";

                foreach (XmlNode xNode in xdocl)
                {
                    foreach (XmlNode CNode in xNode)
                    {
                        if (CNode.Name.Trim() == "OtpErrorCode")
                        {
                            OtpErrorCode = CNode.InnerText;
                        }
                        if (CNode.Name.Trim() == "OtpRet")
                        {
                            OtpRet = CNode.InnerText;
                        }
                        if (CNode.Name.Trim() == "ResponseMsg")
                        {
                            ResponseMsg = CNode.InnerText;
                        }
                    }
                }

                btnAuthAudhar.Visible = false;
                txtOtp.Visible = false;
                btnOtp.Visible = false;
                String error = "";
                if (OtpRet.Trim().ToUpper() == "N")
                {
                    // error
                    if (OtpErrorCode.Trim().Length > 0)
                    {
                        error = UidiErrorCodes.GetErrorDetails(OtpErrorCode.Trim());
                    }
                    btnOtp.Visible = true;
                    Util.ShowMessageBox(this.Page, "Error", error, "error");
                }
                else if (OtpRet.Trim().ToUpper() == "")
                {
                    try
                    {
                        String[] ss = ResponseMsg.Trim().Split(',');
                        ResponseMsg = ss[6].ToString();

                    }
                    catch { }

                    error = "Unable to Send OTP, Response : " + ResponseMsg + "";
                    Util.ShowMessageBox(this.Page, "error", error, "error");
                }
                else
                {
                    try
                    {
                        String[] ss = ResponseMsg.Trim().Split(',');
                        ResponseMsg = ss[6].ToString();

                    }
                    catch { }

                    try
                    {
                        NPCIResponce cl = NPCIClass.InvokeService(txtAADHARNo.Text.Trim(), "");
                        if (cl.status != "A")
                        {
                            //Adhar not link
                            Literal51.Text = " <div class='alert alert-danger alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'></span></button> <strong>Alert!</strong>  <b> Aadhaar is not linked with your Bank Account  </div>";
                        }
                        else if (cl.status == "A")
                        {
                            //Adhar not link
                            Literal51.Text = " <div class='alert alert-danger alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'></span></button> <strong>Alert!</strong>  <b> Aadhaar is linked with your Bank Account  </div>";
                        }
                    }
                    catch (Exception ex)
                    {
                        Util.LogErrorNNPCI(ex.ToString());
                    }

                    btnAuthAudhar.Visible = true;
                    txtOtp.Visible = true;
                    btnOtp.Visible = false;
                    error = "OTP send to registered mobile number " + ResponseMsg + "  and will valid for next 5 min.";
                    Util.ShowMessageBox(this.Page, "Success", error, "success");



                    ErrorLogModel err = new ErrorLogModel();
                    err.ErrorTitle = "Farmer Adhar Individual Registration Log";
                    err.ProjectName = "POCRA WEBSITE";
                    err.ErrorDescription = "UID : " + UID + "\n URL :" + url + " , Input :" + str + " \n ,Response : " + backstr;
                    err.ErrorSeverity = (int)ErrorSeverity.Information;
                    new ErrorLogManager().InsertErrorLog(err);


                }

            }
            catch (Exception ex)
            {
                txtOtp.Visible = true;
                Util.ShowMessageBox(this.Page, "Error", "Response is not coming from uidai kindly try again " + ex.Message, "error");
            }

        }


        protected void btnOtp_Click(object sender, EventArgs e)
        {

            if (txtAADHARNo.Text.Trim().Length == 0)
            {

                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG12", Session["Lang"].ToString()), "error");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                return;
            }

            //AdharVaultAPICalls api = new AdharVaultAPICalls();
            //string ReferenceNumber = api.GetReferenceFromAdhar(txtAADHARNo.Text.Trim());

            //String RegExId = cla.GetExecuteScalar("Select top 1 RegistrationID  from Tbl_M_RegistrationDetails where IsDeleted is null and  BoltID in (select BoltID from Tbl_M_RegistrationDetails_Bolt where ADVRefrenceID='" + ReferenceNumber.Trim() + "')");

            String RegExId = cla.GetExecuteScalar("Select top 1 RegistrationID  from Tbl_M_RegistrationDetails where IsDeleted is null and  BoltID in (select BoltID from Tbl_M_RegistrationDetails_Bolt where AaDharNumber='" + txtAADHARNo.Text.Trim() + "')");
            if (RegExId.Trim().Length > 0)
            {

                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG26", Session["Lang"].ToString()), "error");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                return;
            }

            if (txtCapcha.Text == ViewState["spam"].ToString())
            {
                // done 
            }
            else
            {

                clsMessages.Errormsg(Literal2, "You have entered invalid captcha code. Please retry.");
                Random a = new Random(Guid.NewGuid().GetHashCode());
                int firstNumber = a.Next(1, 9);
                int secondNumber = a.Next(1, 9);
                ViewState["spam"] = firstNumber + secondNumber;
                LiteralCapcha.Text = firstNumber.ToString() + " + " + secondNumber.ToString();
                return;
            }


            btnAuthAudhar.Visible = false;

            if (rdoAuthenticationType.SelectedValue.Trim() == "OTP")
            {
                btnOtp.Visible = false;
                txtOtp.Visible = true;
                btnAuthAudhar.Visible = true;
            }
            else
            {
                btnOtp.Visible = false;
                txtOtp.Visible = false;
            }






            if (rdoAuthenticationType.SelectedValue.Trim() == "OTP")
            {
                try
                {
                    cla.ExecuteCommand("update Tbl_M_SMSCount set UserNo='" + txtAADHARNo.Text.Trim() + "' where SMSCountID in (Select top 1 Tbl_M_SMSCount.SMSCountID from Tbl_M_SMSCount where Tbl_M_SMSCount.MobileNo='" + txtMobileOtp.Text.Trim() + "' order by Tbl_M_SMSCount.SMSCountID desc)");
                    //ViewState["Txn"] = "UKC:" + DateTime.Now.ToString("yyyyMMddHHmmssfff");// "2018103130051212";
                    String s = new MyClass().GetSqlUnikNO("3");
                    ViewState["Txn"] = "" + s + DateTime.Now.ToString("yyyyMMddHHmmssfff");// "2018103130051212";
                    SendForAuthentication();

                }
                catch (Exception ex)
                {
                    txtOtp.Visible = true;
                    Util.ShowMessageBox(this.Page, "Error", "Response is not coming from uidai kindly try again " + ex.Message, "error");
                }

                //txtAADHARNo_TextChanged(null, null);
            }
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> submitCheck();  </script>", false);
        }

        protected void btnAuthAudhar_Click(object sender, EventArgs e)
        {

            try
            {



                //string keyPath = Server.MapPath("~/admintrans/UserControls/uidai_auth_encrypt_preprod.cer");
                string keyPath = Server.MapPath("~/admintrans/UserControls/Production/uidai_auth_prod.cer");

                String UID = txtAADHARNo.Text.Trim();// "";
                String Txn = "UKC:" + ViewState["Txn"].ToString().Trim();

                com.auth.AuthPacketCreator objAuthPacket = new com.auth.AuthPacketCreator();
                //string authxml = objAuthPacket.createOtpAuthPacket(UID, TextBox1.Text.Trim(), keyPath, "SNDKS23054", Txn, "2018-10-31T13:11:46.027+05:30", "UDC-AGRIGOM-0001");

                string authxml = objAuthPacket.createOtpKYCPacket(UID, txtOtp.Text.Trim(), keyPath, "PNDKS23054", Txn, "2018-11-02T11:11:46.027+05:30", "UDC-AGRIGOM-0001", "");


                //Response.Write(authxml);
                XmlDocument xdocl = new XmlDocument();
                xdocl.LoadXml(authxml);

                String Skey = "", Data = "", Hmac = "", keyExp = "";

                foreach (XmlNode xNode in xdocl)
                {

                    foreach (XmlNode CNode in xNode)
                    {
                        //if (CNode.Name.Trim() == "Skey")
                        //{
                        //    Skey = CNode.InnerText;
                        //}
                        if (CNode.Name.Trim() == "Skey")
                        {
                            Skey = CNode.InnerText;
                            try
                            {
                                keyExp = CNode.Attributes["ci"].InnerText;//20250923
                            }
                            catch { }
                        }
                        if (CNode.Name.Trim() == "Data")
                        {
                            Data = CNode.InnerText;
                        }
                        if (CNode.Name.Trim() == "Hmac")
                        {
                            Hmac = CNode.InnerText;
                        }
                    }
                }


                clsCrypto cls = new global::clsCrypto();
                String Password = "testndks@123";
                byte[] key = MOLCryptoEngine.MOLSecurity.GenerateKey(256);
                byte[] IV = MOLSecurity.GenerateIV(128, "uidaimaharashtra");
                string EncryptedUID = MOLSecurity.AESEncrypt(UID, IV, key);// Convert.ToBase64String(encrypted);
                string PasswordUID = MOLSecurity.AESEncrypt(Password, IV, key);// Convert.ToBase64String(encryptedPassword);
                String Certificate = System.Web.Hosting.HostingEnvironment.MapPath("~/AdminTrans/UserControls/Production/JME.cer");
                string incodedkey = Convert.ToBase64String(key);
                String AUASkey = MOLSecurity.EncryptWithPublicKey(incodedkey, Certificate);// EncryptCertificate(Encoding.ASCII.GetBytes(incodedkey), cert.GetRawCertData());
                string HA256KeyValue = cls.generateSha256Hash(incodedkey);
                string HA256saTxn = cls.generateSha256Hash(Txn);
                String aa = PasswordUID + "~" + EncryptedUID + "~" + HA256KeyValue + "~" + HA256saTxn;
                aa = aa.ToLower();

                String Token = cls.generateSha256Hash(aa);// ComputeSha256Hash(aa);




                String str = "<KUAData xmlns=" + PutIntoQuotes("http://kua.maharashtra.gov.in/kyc/gom-kyc-request") + ">";
                str += "<uid>" + EncryptedUID + "</uid>";
                str += "<appCode>KYCApp</appCode>";
                str += "<Token>" + Token.Trim() + "</Token>";
                str += "<KUASkey>" + AUASkey.Trim() + "</KUASkey>";
                str += "<sa>PNDKS23054</sa>";
                str += "<saTxn>" + Txn + "</saTxn>";
                str += "<Data type =" + PutIntoQuotes("X") + ">" + Data + "</Data>";
                str += "<Hmac>" + Hmac + "</Hmac>";
                //str += "<Skey ci =" + PutIntoQuotes("20191230") + ">" + Skey + "</Skey>";
                if (keyExp.Trim() == "20191230")
                {
                    str += "<Skey ci =" + MyClass.PutIntoQuotes("20191230") + ">" + Skey + "</Skey>";
                }
                else
                {
                    str += "<Skey ci =" + MyClass.PutIntoQuotes("20250923") + ">" + Skey + "</Skey>";
                }
                //str += "<Skey ci =" + PutIntoQuotes("20250923") + ">" + Skey + "</Skey>";
                str += "<Uses pi =" + PutIntoQuotes("n") + " pa =" + PutIntoQuotes("n") + " pfa =" + PutIntoQuotes("n") + " bio =" + PutIntoQuotes("n") + " pin=" + PutIntoQuotes("n") + " otp=" + PutIntoQuotes("y") + "/>";
                str += "<Meta rdsId =" + PutIntoQuotes("") + " rdsVer =" + PutIntoQuotes("") + " dpId =" + PutIntoQuotes("") + " dc =" + PutIntoQuotes("") + " mi =" + PutIntoQuotes("") + " mc =" + PutIntoQuotes("") + " />";
                str += "<type>A</type>";
                str += "<rc>Y</rc>";
                str += "<lr>Y</lr>";
                str += "<pfr>N</pfr>";
                str += "</KUAData>";



                //string url = "https://kuaqa.maharashtra.gov.in/KUA/rest/kycreq";
                string url = "https://kua25.maharashtra.gov.in/kua25/KUA/rest/kycreq";

                String Conn = str;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                byte[] requestBytes = System.Text.Encoding.ASCII.GetBytes(Conn);
                req.Method = "POST";
                req.ContentType = "text/xml";
                req.ContentLength = requestBytes.Length;
                Stream requestStream = req.GetRequestStream();
                requestStream.Write(requestBytes, 0, requestBytes.Length);
                requestStream.Close();
                //HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                string responseXml = "";
                using (HttpWebResponse res = (HttpWebResponse)req.GetResponse())
                {
                    // Do your processings here....
                    StreamReader sr = new StreamReader(res.GetResponseStream());
                    responseXml = sr.ReadToEnd();

                }


                XmlDocument xml = new XmlDocument();
                xml.LoadXml(responseXml); // suppose that myXmlString contains "<Names>...</Names>"


                String OtpErrorCode = "", OtpRet = "";

                XmlNodeList xdocls = xml.SelectNodes("/KuaRes");
                foreach (XmlNode xNode in xdocls)
                {
                    OtpRet = xNode.Attributes["ret"].InnerText;
                    if (xNode.Attributes["err"] != null)
                        OtpErrorCode = xNode.Attributes["err"].InnerText;
                }

                if (OtpRet.Trim().ToUpper() == "Y")
                {
                    txtAADHARNo.ReadOnly = true;
                    txtOtp.Visible = false;
                    btnAuthAudhar.Visible = false;
                    string email = "";
                    string phone = "";
                    string gender = "";
                    string dob = "";
                    string name = "";
                    String Pht = "";
                    String state = "";
                    XmlNodeList xnList = xml.SelectNodes("/KuaRes/UidData");
                    foreach (XmlNode xn in xnList)
                    {
                        foreach (XmlNode Cxn in xn)
                        {
                            if (Cxn.Name == "Poi")
                            {
                                if (Cxn.Attributes["email"] != null)
                                    email = Cxn.Attributes["email"].InnerText;
                                if (Cxn.Attributes["phone"] != null)
                                    phone = Cxn.Attributes["phone"].InnerText;
                                if (Cxn.Attributes["gender"] != null)
                                    gender = Cxn.Attributes["gender"].InnerText;
                                if (Cxn.Attributes["state"] != null)
                                    state = Cxn.Attributes["state"].InnerText;
                                if (Cxn.Attributes["dob"] != null)
                                    dob = Cxn.Attributes["dob"].InnerText;
                                if (Cxn.Attributes["name"] != null)
                                    if (name.Trim().Length == 0)
                                        name = Cxn.Attributes["name"].InnerText;
                            }
                            if (Cxn.Name == "LData")
                            {
                                if (Cxn.Attributes["name"] != null)
                                    if (txtNameInMarathi.Text.Trim().Length == 0)
                                        txtNameInMarathi.Text = Cxn.Attributes["name"].InnerText;
                            }
                            if (Cxn.Name == "Pht")
                            {
                                Pht = Cxn.InnerText;
                                if (Pht.Length > 0)
                                {
                                    byte[] bytes = Convert.FromBase64String(Pht);
                                    ImageTagId.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(bytes);
                                    Literal5.Text = "";
                                }
                            }
                        }

                    }

                    // show records 
                    txtName.Text = name;
                    txtName.Enabled = false;
                    if (dob.Length > 0)
                    {
                        if (dob.Contains("-"))
                        {
                            try
                            {
                                String[] s = dob.Split('-');
                                ddlDay.SelectedValue = Convert.ToInt32(s[0].ToString()).ToString();
                                ddlMonth.SelectedValue = Convert.ToInt32(s[1].ToString()).ToString();
                                ddlYear.SelectedValue = Convert.ToInt32(s[2].ToString()).ToString();
                            }
                            catch { }
                        }
                    }
                    if (gender.Length > 0)
                    {
                        if (gender.Trim() == "M")
                        {
                            rdoGender.SelectedValue = "Male";
                            rdoGender.SelectedIndex = 1;
                        }
                        if (gender.Trim() == "F")
                        {
                            rdoGender.SelectedValue = "Female";
                            rdoGender.SelectedIndex = 2;
                        }
                        else if (gender.Trim() == "T")
                        {
                            rdoGender.SelectedValue = "Other";
                            rdoGender.SelectedIndex = 3;
                        }
                    }

                    Util.ShowMessageBox(this.Page, "Success", "Aadhar verified Successfully, Please complete your registration.", "success");

                }
                else
                {
                    String error = "";

                    if (OtpErrorCode.Trim().Length > 0)
                    {
                        error = UidiErrorCodes.GetErrorDetails(OtpErrorCode.Trim());
                    }
                    try
                    {
                        //string strPath = Server.MapPath("~/admintrans/UserControls/AULOG.txt");// @"D:\Rekha\Log.txt";
                        //if (!File.Exists(strPath))
                        //{
                        //    File.Create(strPath).Dispose();
                        //}
                        //using (StreamWriter sw = File.AppendText(strPath))
                        //{
                        //    sw.WriteLine("===========START============= " + txtAADHARNo.Text.Trim());

                        //    sw.WriteLine("=============API  Logging ===========");
                        //    sw.WriteLine("REQ TEXT: " + str);
                        //    sw.WriteLine("========================");

                        //    sw.WriteLine("RES TEXT: " + responseXml);
                        //    sw.WriteLine("===========End============= " + DateTime.Now);
                        //    sw.Dispose();
                        //}
                    }
                    catch
                    {

                    }

                    Util.ShowMessageBox(this.Page, "Error", error, "error");
                }
            }
            catch (Exception ex)
            {
                Util.ShowMessageBox(this.Page, "Error", ex.ToString(), "error");
                Util.LogError(ex);
            }
        }

        protected void btnMarathi_Click1(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            if (btn.Text.Trim() == "मराठी")
            {
                Session["Lang"] = "mr-IN";
                btn.Text = "English";
            }
            else
            {
                Session["Lang"] = "en-IN";
                btn.Text = "मराठी";
            }
            Response.Redirect(Request.Path);
            //rm = new ResourceManager("Resources.FarmerRegistration", System.Reflection.Assembly.Load("App_GlobalResources")); ci = Thread.CurrentThread.CurrentCulture;
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("mr-IN");
            //LoadString(Thread.CurrentThread.CurrentCulture);

        }


        protected void btnBasicBack_Click(object sender, EventArgs e)
        {
            LiteralMsg.Text = "";
            Button btn = (Button)sender;
            if (btn.ValidationGroup == "0")
            {
                multiViewEmployee.ActiveViewIndex = 0;

            }
            if (btn.ValidationGroup == "1")
            {
                multiViewEmployee.ActiveViewIndex = 1;

            }
            if (btn.ValidationGroup == "2")
            {
                if (rdoLandStatus.SelectedValue.Trim() == "YES")
                {
                    multiViewEmployee.ActiveViewIndex = 2;
                }
                else
                {
                    multiViewEmployee.ActiveViewIndex = 1;
                }

            }
            if (btn.ValidationGroup == "3")
            {
                multiViewEmployee.ActiveViewIndex = 3;

            }
            if (btn.ValidationGroup == "4")
            {
                multiViewEmployee.ActiveViewIndex = 4;

            }

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
        }

        protected void btnSendOTP_Click(object sender, EventArgs e)
        {
            // ask for otp

            if (Session["Captcha"] == null)
            {
                //clsMessages.Errormsg(Literal2, "You have entered invalid Captcha Value .");
                Util.ShowMessageBox(this.Page, "Error", "You have entered invalid Captcha Value .", "error");
                SetCaptchaText();
                return;
            }


            if (txtVerificationCode.Value.Trim() != Session["Captcha"].ToString())
            {
                // clsMessages.Errormsg(Literal2, "You have entered invalid Captcha Value .");
                Util.ShowMessageBox(this.Page, "Error", "You have entered invalid Captcha Value .", "error");
                return;
            }


            string Myipaddress = "";
            try
            {

                Myipaddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (Myipaddress == "" || Myipaddress == null)
                    Myipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            catch
            {

            }


            txtAADHARNo.Enabled = false;
            divAdhar.Visible = false;
            divAdharbtns.Visible = false;

            String abc = cla.GetExecuteScalar("Select top 1  DATEDIFF(MINUTE,DateTime_Table,GETDATE()) from Tbl_M_SMSCount where MobileNo='" + txtMobileOtp.Text.Trim() + "' order by SMSCountID desc");
            if (abc.Length == 0) abc = "10";
            if (Convert.ToInt32(abc) <= 3)
            {
                Util.ShowMessageBox(this.Page, "Error", "OTP is already Send to your mobile No. You can send new OTP after 5 Min.", "error");
                return;
            }

            String aa = cla.GetExecuteScalar("Select count(SMSCountID) from Tbl_M_SMSCount where CONVERT(date, DateTime_Table,103)=CONVERT(date,GETDATE(),103) and MobileNo='" + txtMobileOtp.Text.Trim() + "' ");
            if (aa.Length == 0) aa = "0";
            if (Convert.ToInt32(aa) > 3)
            {
                Util.ShowMessageBox(this.Page, "Error", "Your mobile number has been inactive for 24 hrs. Please try after 24 Hrs.", "error");
                return;
            }


            aa = cla.GetExecuteScalar("Select count(SMSCountID) from Tbl_M_SMSCount where MobileNo='" + txtMobileOtp.Text.Trim() + "' ");
            if (aa.Length == 0) aa = "0";
            if (Convert.ToInt32(aa) > 6)
            {
                String b = cla.GetExecuteScalar("Select top 1 RegistrationID from Tbl_M_RegistrationDetails where MobileNumber='" + txtMobileOtp.Text.Trim() + "' ");
                if (b.Length == 0)
                {
                    cla.ExecuteCommand("update Tbl_M_SMSCount Set IsBlocked='1' ,IPAddress='" + Myipaddress + "' where MobileNo='" + txtMobileOtp.Text.Trim() + "' ");
                    Util.ShowMessageBox(this.Page, "Error", "Your mobile number has been inactive , Please call us on 022-22153351", "error");
                    return;
                }
            }

            Random a = new Random(Guid.NewGuid().GetHashCode());
            int MyNumber = a.Next(000000, 999999);
            String strOTP = MyNumber.ToString();
            String s = "OTP for login to the DBT portal is " + strOTP + ". It will be valid for 5 min. Team PoCRA"; // "This is your one-time password " +MyNumber+" for PoCRA DBT Registration.";
            SMS.SendSMS(s, txtMobileOtp.Text.Trim(), "1407161960968273289");
            ViewState["OTP"] = EncryptDecryptQueryString.encrypt(strOTP);
            cla.ExecuteCommand("INSERT INTO Tbl_M_SMSCount (MobileNo,IPAddress,FromLastURL) VALUES ('" + txtMobileOtp.Text.Trim() + "','" + Myipaddress + "','" + txtPrev.Text.Trim() + "')");
            divOTP.Visible = true;
            String msg = "OTP send to your mobile number " + txtMobileOtp.Text.Trim() + "  and will valid for next 5 min. You can send max. 3 OTP to this number. ";
            Util.ShowMessageBox(this.Page, "Success", msg, "success");
        }

        protected void btnVerifyOTP_Click(object sender, EventArgs e)
        {
            if (EncryptDecryptQueryString.Decrypt(ViewState["OTP"].ToString().Trim()).ToUpper() != txtOTPNew.Text.Trim().ToUpper())
            {

                // invalid otp
                cla.ExecuteCommand("INSERT INTO Tbl_M_SMSCount (MobileNo,IsIncorectOTP) VALUES ('" + txtMobileOtp.Text.Trim() + "','1')");
                String a = cla.GetExecuteScalar("Select count(SMSCountID) from Tbl_M_SMSCount where CONVERT(date, DateTime_Table,103)=CONVERT(date,GETDATE(),103) and MobileNo='" + txtMobileOtp.Text.Trim() + "' ");
                if (a.Length == 0) a = "0";
                if (Convert.ToInt32(a) > 3)
                {
                    Util.ShowMessageBox(this.Page, "Error", "Your mobile number has been inactive for 24 hrs. Please try after 24 Hrs.", "error");
                }
                else
                {
                    Util.ShowMessageBox(this.Page, "Error", "You have entered invalid OTP. 3 incorrect attempt will make your mobile number inactive for 24 hrs.", "error");
                }
                return;
            }
            else
            {
                cla.ExecuteCommand("update Tbl_M_SMSCount set IsVarefyed='1' where SMSCountID in (Select top 1 Tbl_M_SMSCount.SMSCountID from Tbl_M_SMSCount where Tbl_M_SMSCount.MobileNo='" + txtMobileOtp.Text.Trim() + "' order by Tbl_M_SMSCount.SMSCountID desc)");
                txtMobileOtp.ReadOnly = true;
                txtAADHARNo.Enabled = true;
                txtMobile1.Text = txtMobileOtp.Text.Trim();
                txtMobile1.Enabled = false;
                btnSendOTP.Visible = false;
                divOTP.Visible = false;
                divCapcha.Visible = false;
                divAdhar.Visible = true;
                divAdharbtns.Visible = true;
                Util.ShowMessageBox(this.Page, "Success", "Mobile Number verified Successfully, Please complete your registration.", "success");
            }

        }
    }
}