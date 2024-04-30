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

namespace DBTPoCRA.Registration
{
    public partial class IndividualRegistrationEdit : System.Web.UI.Page
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

                    FillDetails();


                    Page.Form.Attributes.Add("enctype", "multipart/form-data");


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



        private void FillDetails()
        {

            //Session["RegistrationID"] = Session["RegistrationID"].ToString();
            ddlCATEGORY.DataSource = Comcls.GetCostCategoryMaster();
            ddlCATEGORY.DataTextField = "CategoryMaster";
            ddlCATEGORY.DataValueField = "CategoryMasterID";
            ddlCATEGORY.DataBind();
            ddlCATEGORY.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlCATEGORY.SelectedIndex = 0;
            //

            List<String> lst = new List<String>();
            lst.Add(Session["RegistrationID"].ToString());

            dt = new DataTable();
            dt = cla.GetDtByProcedure("SP_Farmer_IndividualDetails_View", lst);

            if (dt.Rows.Count > 0)
            {
                // ddlBENEFICIARY.SelectedValue = dt.Rows[0]["BeneficiaryTypesID"].ToString();               
                txtName.Text = dt.Rows[0]["RegisterName"].ToString();//RegisterName//CategoryMaster  
                                                                     // rdoAuthenticationType.SelectedValue = dt.Rows[0]["BeneficiaryTypesID"].ToString();
                string[] str = dt.Rows[0]["DateOfBirth"].ToString().Split('/');

                ImageTagId.ImageUrl = "https://dbtpocradata.blob.core.windows.net/admintrans/DocMasters/MemberDoc/" + Session["RegistrationID"].ToString() + "/ProfileImage.jpg";

                int d = 0, m = 0, y = 0;
                int.TryParse(str[0], out d);
                int.TryParse(str[1], out m);
                int.TryParse(str[2], out y);
                ddlDay.SelectedValue = d.ToString();
                ddlMonth.SelectedValue = m.ToString();
                ddlYear.SelectedValue = y.ToString();
                //  String DOB = ddlDay.SelectedValue.Trim() + "/" + ddlMonth.SelectedValue.Trim() + "/" + ddlYear.SelectedValue.Trim();
                rdoLandStatus.SelectedValue = dt.Rows[0]["LandStatus"].ToString();
                rdoLandStatus_SelectedIndexChanged(null, null);
                //Land less Certificate
                if (dt.Rows[0]["LandLessCertificate"].ToString().Length > 0)
                {
                    LabelLandlessCert.Text = "<a target=_blank  href='" + clsSettings.BaseUrl + "" + dt.Rows[0]["LandLessCertificate"].ToString() + "'> View Certificate </a>";

                }

                if (dt.Rows[0]["AnyOtherDocType"].ToString().Length > 0)
                {
                    ddlAnyOtherCertificate.SelectedValue = dt.Rows[0]["AnyOtherDocType"].ToString();
                    if (dt.Rows[0]["AnyOtherDoc"].ToString().Length > 0)
                    {
                        LabelAnyOtherCertificate.Text = "<a target=_blank  href='" + clsSettings.BaseUrl + "" + dt.Rows[0]["AnyOtherDoc"].ToString() + "'> View Certificate </a>";

                    }
                }
                //

                FillDropDowns();


                rdoGender.SelectedValue = dt.Rows[0]["Gender"].ToString();
                txtHouseNo.Text = dt.Rows[0]["Address1HouseNo"].ToString();
                txtStreetNo.Text = dt.Rows[0]["Address1StreetName"].ToString();
                ddlDISTRICT.SelectedValue = dt.Rows[0]["Address1City_ID"].ToString();
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
                txtEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                txtPAN.Text = dt.Rows[0]["PanNumber"].ToString();
                ddlCATEGORY.SelectedValue = dt.Rows[0]["CategoryMasterID"].ToString();//FPORegistrationDate
                rdoHANDICAP.SelectedValue = dt.Rows[0]["PhysicallyHandicap"].ToString();
                txtDISABILITYPer.Text = dt.Rows[0]["DisabilityPer"].ToString();
                ////FileUpload2   
                if (dt.Rows[0]["PhysicallyHandicapDoc"].ToString().Length > 0)
                {
                    lblHandiChetificate.Text = "<a target=_blank  href='" + clsSettings.BaseUrl + "" + dt.Rows[0]["PhysicallyHandicapDoc"].ToString() + "'> View Certificate </a>";
                    //btnRemoveHandi.Visible = true;
                }


                if (dt.Rows[0]["CastCategoryDoc"].ToString().Length > 0)
                    lblCATEGORY.Text = "<a href='" + clsSettings.BaseUrl + "" + dt.Rows[0]["CastCategoryDoc"].ToString() + "' target=_blank >  View Certificate  </a>";
                else
                    lblCATEGORY.Text = "";

                fillLandDetails();

            }

        }

        private void LoadString(CultureInfo ci)
        {
            Menu1.Items[0].Text = rm.GetString("REGISTRATION_DETAILS", ci);
            Menu1.Items[1].Text = rm.GetString("BASIC_DETAILS", ci);
            Menu1.Items[2].Text = rm.GetString("LAND_DETAILS", ci);
            Menu1.Items[3].Text = rm.GetString("DECLARATION", ci);


            Literal3.Text = rm.GetString("REGISTRATION_DETAILS", ci);
            //Literal2.Text = rm.GetString("AADHAR_NUMBER", ci);

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
            //Literal49.Text = rm.GetString("if_you_dont_have_an_aadhaar_number_please_click_here_to_enroll", ci);
            Label5.Text = rm.GetString("DISABILITY_PERCENTAGE", ci);
            Label1.Text = rm.GetString("UPLOAD_CERITIFICATE", ci);
            // Label6.Text = rm.GetString("LandLessCertificate", ci);
            btnSaveAAudhar.Text = rm.GetString("Continue", ci);
            btnBasic.Text = rm.GetString("Continue", ci);
            btnADD.Text = rm.GetString("ADD_8A", ci);
            btnAdd712.Text = rm.GetString("ADD_", ci);
            btnBasicBack0.Text = rm.GetString("Previous", ci);
            btnLandNext.Text = rm.GetString("Continue", ci);
            //btnFinalSave.Text = rm.GetString("Submit", ci);
            //btnOtp.Text = rm.GetString("SEND_OTP", ci);
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

            //if (txtAADHARNo.Text.Trim().Length == 0)
            //{
            //    Util.ShowMessageBox(this.Page, "Required", MyCommanClass.GetMsgInEnForDB("MSG12", Session["Lang"].ToString()), "error");
            //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
            //    return;
            //}
            //else 
            if (txtName.Text.Trim().Length == 0)
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


                    if (Session["RegistrationID"].ToString().Trim().Length > 0)
                    {

                        str = " UPDATE Tbl_M_RegistrationDetails SET  ";
                        str += " DateOfBirth='" + cla.mdy(DOB.Trim()) + "',Gender='" + rdoGender.SelectedValue.Trim() + "'";
                        str += " WHERE RegistrationID = " + Session["RegistrationID"].ToString() + " ";
                        cla.ExecuteCommand(str, command);
                        LiteralName.Text = txtName.Text.Trim().ToUpper();

                        transaction.Commit();
                    }
                    else
                    {
                        // EDIT 
                        transaction.Commit();
                    }

                    if (Session["RegistrationID"].ToString().Trim().Length > 0)
                    {
                        String PathUp = "DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "";



                        String AnyOtherCertificate = "";
                        if (FileAnyOtherCertificate.HasFile)
                        {
                            if (FileAnyOtherCertificate.PostedFile.ContentLength < 3145728)
                            {
                                //String Uppath = path + "/AnyOtherCertificate" + System.IO.Path.GetExtension(FileAnyOtherCertificate.PostedFile.FileName.ToString());
                                // FileAnyOtherCertificate.PostedFile.SaveAs(Uppath.Trim()); //File is saved in the Physical folder 
                                AnyOtherCertificate = "/admintrans/DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "/AnyOtherCertificate" + System.IO.Path.GetExtension(FileAnyOtherCertificate.PostedFile.FileName.ToString());

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
                        str += " WHERE(RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ")";
                        cla.ExecuteCommand(str);


                        Util.ShowMessageBox(this.Page, "Success", "Record Updated Sucessfully", "success");
                        //txtAADHARNo.ReadOnly = true;
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

            //if (txtAADHARNo.Text.Trim().Length == 0)
            //{

            //    Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG12", Session["Lang"].ToString()), "error");
            //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
            //    return;
            //}
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

            if (Session["RegistrationID"].ToString().Trim().Length > 0)
            {

                String PathUp = "DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "";


                if (FileCATEGORYCERITIFICATE.HasFile)
                {
                    if (FileCATEGORYCERITIFICATE.PostedFile.ContentLength < 3145728)
                    {
                        CastCategoryDoc = "/admintrans/DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "/CasteCategoryCertificate" + System.IO.Path.GetExtension(FileCATEGORYCERITIFICATE.PostedFile.FileName.ToString());
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
                        PhysicallyHandicapDoc = "/admintrans/DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "/PhysicallyHandicapCertificate" + System.IO.Path.GetExtension(FileHANDICAPCERITIFICATE.PostedFile.FileName.ToString());
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


                    if (Session["RegistrationID"].ToString().Trim().Length > 0)
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


                        str += " WHERE(RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ")";
                        cla.ExecuteCommand(str, command);
                        //txtAADHARNo.ReadOnly = true;
                        txtName.ReadOnly = true;
                    }


                    transaction.Commit();
                    Util.ShowMessageBox(this.Page, "Success", "Record Updated Sucessfully", "success");


                    multiViewEmployee.ActiveViewIndex = 2;
                    Menu1.Items[2].Enabled = true;


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


                if (grdSubject.Rows.Count > 0)
                {
                    Util.ShowMessageBox(this.Page, "Error", "Please Remove Your Land to change your land status", "error");
                    rdoLandStatus.SelectedValue = "YES";
                    //  rdoLandStatus_SelectedIndexChanged(sender, e);
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);

                    return;
                }

                divLand.Visible = false;
                divLandLess.Visible = true;



                //cla.ExecuteCommand("UPDATE Tbl_M_RegistrationLand SET IsDeleted='1'  where  RegistrationID = " + Session["RegistrationID"].ToString().Trim() + "");
                //String str = " UPDATE  Tbl_M_RegistrationDetails SET   LandStatus ='" + rdoLandStatus.SelectedItem.Value.Trim() + "' , LandLessCertificate=NULL ";
                //str += " WHERE (RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ")";
                //cla.ExecuteCommand(str);
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

            String s = cla.GetExecuteScalar("SELECT top 1 LandID FROM Tbl_M_RegistrationLand where RegistrationID=" + Session["RegistrationID"].ToString().Trim() + " and City_ID=" + ddlLANDDISTRICT.SelectedValue.Trim() + " and TalukaID=" + ddlLANDTALUKA.SelectedValue.Trim() + "  and VillageID=" + ddlLANDVILLAGE.SelectedValue.Trim() + " and IsDeleted is null");
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
                if (Session["RegistrationID"].ToString().Trim().Length > 0)
                {
                    //String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "");
                    String PathUp = "DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "";

                    if (FileFORM8A.HasFile)
                    {
                        if (FileFORM8A.PostedFile.ContentLength < 3145728)
                        {
                            String fileName = "FORM8A_" + LandID.ToString() + "" + System.IO.Path.GetExtension(FileFORM8A.PostedFile.FileName.ToString());
                            // String Uppath = path + fileName;
                            //FileFORM8A.PostedFile.SaveAs(Uppath.Trim()); //File is saved in the Physical folder 
                            filepath = "/admintrans/DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "/" + fileName;

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


                    if (Session["RegistrationID"].ToString().Trim().Length > 0)
                    {
                        str = " INSERT INTO Tbl_M_RegistrationLand (LandID, RegistrationID, City_ID, TalukaID, VillageID, AccountNumber8A, Hectare8A, Are8A, Form8ADoc)";
                        str += " VALUES(" + LandID + "," + Session["RegistrationID"].ToString().Trim() + "," + ddlLANDDISTRICT.SelectedValue.Trim() + "," + ddlLANDTALUKA.SelectedValue.Trim() + "," + ddlLANDVILLAGE.SelectedValue.Trim() + ",'" + txtSURVEYNo8A.Text.Trim() + "'," + txtLANDAREA8AH.Text.Trim() + "," + txtLANDAREA8AA.Text.Trim() + ",'" + filepath + "')";
                        cla.ExecuteCommand(str, command);
                        div7A.Visible = true;
                        btnADD.Enabled = false;
                        rdoLandStatus.Enabled = false;

                        str = " UPDATE  Tbl_M_RegistrationDetails SET   LandStatus ='" + rdoLandStatus.SelectedItem.Value.Trim() + "' ";
                        str += " WHERE(RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ")";
                        cla.ExecuteCommand(str, command);


                    }


                    transaction.Commit();

                    DataTable dt = cla.GetDataTable("SELECT LandID, AccountNumber8A FROM Tbl_M_RegistrationLand where RegistrationID=" + Session["RegistrationID"].ToString() + " and IsDeleted is null  and AccountNumber8A is not null  Order by LandID");
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
                    Util.ShowMessageBox(this.Page, "Success", "Record Updated Sucessfully", "success");
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

                FileError = Util.CheckAllowedFileName(File712);
                if (FileError.Length > 0)
                {
                    Util.ShowMessageBox(this.Page, "Error", FileError, "error");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                    return;
                }
            }

            int LandID = cla.TableID("Tbl_M_RegistrationLand", "LandID");
            //String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "");
            String PathUp = "DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "";

            if (File712.HasFile)
            {
                string filepath = "";

                if (File712.PostedFile.ContentLength < 3145728)
                {


                    String fileName = "SURVEYNUMBER712_" + LandID.ToString() + "" + System.IO.Path.GetExtension(File712.PostedFile.FileName.ToString());
                    // String Uppath = path + fileName;
                    //File712.PostedFile.SaveAs(Uppath.Trim()); //File is saved in the Physical folder 
                    filepath = "/admintrans/DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "/" + fileName;

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
                            str += " VALUES(" + LandID + "," + ddl8A.SelectedValue.Trim() + ",'" + txtSURVEYNo712.Text.Trim() + "','" + txtLANDAREA712H.Text.Trim() + "','" + txtLANDAREA712A.Text.Trim() + "','" + filepath + "'," + Session["RegistrationID"].ToString().Trim() + ")";
                            cla.ExecuteCommand(str, command);

                            String s = cla.GetExecuteScalar("SELECT dbo.fn_GetRegisteredLandDeff(" + Session["RegistrationID"].ToString().Trim() + ")", command);
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
                                Util.ShowMessageBox(this.Page, "Success", "Record Saved Sucessfully", "success");
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
                    //String str = " INSERT INTO Tbl_M_RegistrationLand ( LandID, ParentLandID , SurveyNo712, Hectare712, Are712, Extracts712Doc,RegistrationID)";
                    //str += " VALUES(" + LandID + "," + ddl8A.SelectedValue.Trim() + ",'" + txtSURVEYNo712.Text.Trim() + "','" + txtLANDAREA712H.Text.Trim() + "','" + txtLANDAREA712A.Text.Trim() + "','" + filepath + "'," + Session["RegistrationID"].ToString().Trim() + ")";
                    //String s = cla.ExecuteCommand(str);
                    //if (s.Length == 0)
                    //{
                    //    txtSURVEYNo712.Text = "";
                    //    txtLANDAREA712H.Text = "";
                    //    txtLANDAREA712A.Text = "";

                    //    div7A.Visible = true;
                    //    btnADD.Enabled = true;
                    //    fillLandDetails();
                    //    btnLandNext.Visible = true;

                    //    ddlLANDVILLAGE.SelectedIndex = 0;
                    //    ddlLANDTALUKA.SelectedIndex = 0;
                    //    ddlLANDDISTRICT.SelectedIndex = 0;

                    //    Util.ShowMessageBox(this.Page, "Success", "Record Updated Sucessfully", "success");
                    //}
                    //else
                    //{
                    //    clsMessages.Errormsg(LiteralMsg, s.Trim());
                    //    // Util.ShowMessageBox(this.Page, "Error", "Please provide correct input.", "error");
                    //}
                }
            }



        }


        private void fillLandDetails()
        {
            List<String> lst = new List<string>();
            lst.Clear();
            lst.Add(Session["RegistrationID"].ToString().Trim());
            DataTable dt = cla.GetDtByProcedure("SP_GetLandParentDetails", lst);
            grdSubject.DataSource = dt;
            grdSubject.DataBind();

            if (dt.Rows.Count > 0)
            {
                div7A.Visible = true;
                dt = cla.GetDataTable("SELECT LandID, AccountNumber8A FROM Tbl_M_RegistrationLand where RegistrationID=" + Session["RegistrationID"].ToString() + " and IsDeleted is null  and AccountNumber8A is not null  Order by LandID");
                ddl8A.DataSource = dt;
                ddl8A.DataTextField = "AccountNumber8A";
                ddl8A.DataValueField = "LandID";
                ddl8A.DataBind();
                ddl8A.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }

        protected void btnLandNext_Click(object sender, EventArgs e)
        {
            LiteralMsg.Text = "";

            if (rdoLandStatus.SelectedValue.Trim() == "YES")
            {
                if (grdSubject.Rows.Count == 0)
                {
                    Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG35", Session["Lang"].ToString()), "error");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                    return;
                }
            }

            int ErrorCount = 0;
            //Sum of hectare & are from 7 / 12 will be equal to hectare & are of 8A respectively.

            String s = cla.GetExecuteScalar("SELECT dbo.fn_GetRegisteredLandDeff(" + Session["RegistrationID"].ToString().Trim() + ")");
            if (s.Length > 0)
            {
                if (Convert.ToDouble(s) > 0)
                {
                    ErrorCount++;
                }
            }

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
                    FileError = Util.CheckAllowedFileName(FileLandLessCertificate);
                    if (FileError.Length > 0)
                    {
                        Util.ShowMessageBox(this.Page, "Error", FileError, "error");
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                        return;
                    }
                }
            }

            String PathUp = "DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "";

            String LandLessCertificate = "";
            if (FileLandLessCertificate.HasFile)
            {
                if (FileLandLessCertificate.PostedFile.ContentLength < 3145728)
                {
                    LandLessCertificate = "/admintrans/DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "/LandLessCertificate" + System.IO.Path.GetExtension(FileLandLessCertificate.PostedFile.FileName.ToString());

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

                DataTable dtLand = cla.GetDataTable("SELECT  TOP (1) L.VillageID, V.City_ID, V.ClustersMasterID, V.TalukaID, V.CircleID FROM Tbl_M_RegistrationLand AS L INNER JOIN Tbl_M_VillageMaster AS V ON V.VillageID = L.VillageID WHERE (L.RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ") AND (L.ParentLandID IS NULL) and L.isdeleted is null AND L.VillageID <> 0 ORDER BY L.LandID");
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





                        str = " UPDATE  Tbl_M_RegistrationDetails SET   LandStatus ='" + rdoLandStatus.SelectedItem.Value.Trim() + "'  ";
                        str += " WHERE (RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ")";
                        cla.ExecuteCommand(str, command);


                        if (rdoLandStatus.SelectedValue.Trim() == "YES")
                        {
                            //WITH LAND
                            str = " UPDATE  Tbl_M_RegistrationDetails SET   LandLessCertificate =NULL ";
                            str += " WHERE (RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ")";
                            cla.ExecuteCommand(str, command);

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
                                    str += " WHERE (RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ")";
                                    cla.ExecuteCommand(str, command);
                                }
                            }

                        }
                        else
                        {

                            str = " UPDATE  Tbl_M_RegistrationDetails SET   LandLessCertificate ='" + LandLessCertificate.Trim() + "'  ";
                            str += " WHERE (RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ")";
                            cla.ExecuteCommand(str, command);



                        }


                        transaction.Commit();

                        Util.ShowMessageBox(this.Page, "Success", "Record Updated Sucessfully", "success");

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

                String s = cla.GetExecuteScalar("( (Select top 1 RegistrationID from Tbl_T_ApplicationDetails AP where AP.RegistrationID=" + Session["RegistrationID"].ToString().Trim() + " and IsDeleted is null and AP.ApplicationStatusID in (4,10,20) union all Select top 1 RegistrationID from Tbl_M_RegistrationDetails AP where AP.RegistrationID=" + Session["RegistrationID"].ToString().Trim() + " and IsDeleted is null and AP.ApprovalStatus='Back To Beneficiary') union all Select top 1 WR.WorkReportID as RegistrationID from Tbl_T_Application_WorkReport WR where WR.IsDeleted is null and WR.ApplicationStatusID in (4, 10, 20) and WR.ApplicationID in (Select ApplicationID from Tbl_T_ApplicationDetails where Tbl_T_ApplicationDetails.RegistrationID =" + Session["RegistrationID"].ToString().Trim() + " and Tbl_T_ApplicationDetails.IsDeleted is null))");

                if (s.Length > 0)
                {
                    // allow to upload doc without remove
                    DataTable dt = cla.GetDataTable("SELECT   LandID , SurveyNo712, Hectare712, Are712,  '<a target=_blank href=''https://dbtpocradata.blob.core.windows.net'+Extracts712Doc+'''> View Extracts  </a>' as Extracts712Doc , '<a class=''btn btn-outline-primary btn-xs'' onclick=ShowForms(''UpdateLandCerti.aspx?I='+convert(nvarchar(20),LandID)+'&R='+convert(nvarchar(20),RegistrationID)+'TB_iframe=true&height=250&width=100'')> Add New </a>'  as AddNEW FROM  Tbl_M_RegistrationLand WHERE  (ParentLandID = " + ParentLandID + ") AND (IsDeleted IS NULL) AND RegistrationID=" + Session["RegistrationID"].ToString().Trim() + " ");
                    SC.DataSource = dt;
                    SC.DataBind();

                }
                else
                {
                    DataTable dt = cla.GetDataTable("SELECT   LandID , SurveyNo712, Hectare712, Are712,  '<a target=_blank href=''https://dbtpocradata.blob.core.windows.net'+Extracts712Doc+'''> View Extracts  </a>' as Extracts712Doc , ''   as AddNEW  FROM  Tbl_M_RegistrationLand WHERE  (ParentLandID = " + ParentLandID + ") AND (IsDeleted IS NULL) AND RegistrationID=" + Session["RegistrationID"].ToString().Trim() + " ");
                    SC.DataSource = dt;
                    SC.DataBind();

                }


            }
        }




        protected void grdSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LandID
            String S = cla.GetExecuteScalar("SELECT TOP 1 ApplicationID  FROM Tbl_T_ApplicationDetails where  LandID in (Select LandID From Tbl_M_RegistrationLand where ParentLandID=" + grdSubject.SelectedDataKey["LandID"].ToString() + ")  and IsDeleted is null and RegistrationID=" + Session["RegistrationID"].ToString() + " and ApplicationStatusID <> 2  ");
            if (S.Length > 0)
            {
                Util.ShowMessageBox(this.Page, "Error", "Land Can not be deleted because it is used in application", "error");
                // ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> alert('Land Can not be deleted because it is used in application');  </script>", false);
                return;

            }

            cla.ExecuteCommand("update Tbl_T_ApplicationDetails Set ApplicationStatusID=25  where RegistrationID=" + Session["RegistrationID"].ToString() + "  and LandID in (Select LandID From Tbl_M_RegistrationLand where ParentLandID=" + grdSubject.SelectedDataKey["LandID"].ToString() + ")  ");
            List<string> lst = new List<string>();

            lst.Add(grdSubject.SelectedDataKey["LandID"].ToString());
            if (cla.ExecuteByProcedure("SP_Remove_LandDetails", lst))
            {
                fillLandDetails();
                LiteralMsg.Text = "";

                DataTable dtLandCheck = cla.GetDataTable("SELECT  TOP (1) L.VillageID, V.City_ID, V.ClustersMasterID, V.TalukaID, V.CircleID FROM Tbl_M_RegistrationLand AS L INNER JOIN Tbl_M_VillageMaster AS V ON V.VillageID = L.VillageID WHERE (L.RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ") AND (L.ParentLandID IS NULL) and L.IsDeleted is null ORDER BY L.LandID");
                if (dtLandCheck.Rows.Count == 0)
                {
                    rdoLandStatus.SelectedValue = "NO";
                    rdoLandStatus_SelectedIndexChanged(null, null);
                }

                Util.ShowMessageBox(this.Page, "Success", "Record Updated Sucessfully", "success");
            }

        }

        protected void grdChild_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LandID
            GridView gv = (GridView)sender;
            String S = cla.GetExecuteScalar("SELECT TOP 1 ApplicationID  FROM Tbl_T_ApplicationDetails where LandID=" + gv.SelectedDataKey["LandID"].ToString() + " and IsDeleted is null and RegistrationID=" + Session["RegistrationID"].ToString() + " and ApplicationStatusID <> 2 ");
            if (S.Length > 0)
            {
                // ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> alert('Land Can not be deleted because it is used in application');  </script>", false);
                Util.ShowMessageBox(this.Page, "Error", "Land Can not be deleted because it is used in application", "error");
                return;

            }

            // check if he want to delete all lands then he need to check work villlage from address .
            S = cla.GetExecuteScalar("SELECT  isnull(count(L.LandID),0) FROM Tbl_M_RegistrationLand AS L INNER JOIN Tbl_M_VillageMaster AS V ON V.VillageID = L.VillageID WHERE (L.RegistrationID = " + Session["RegistrationID"].ToString() + ") AND (L.ParentLandID IS NULL) and L.isdeleted is null and L.VillageID <> 0 ");
            if (S.Length > 0)
            {
                if (Convert.ToInt32(S) <= 1)
                {
                    if (cla.GetExecuteScalar("select top 1 VillageID from Tbl_M_VillageMaster where VillageID=" + ddlVILLAGE.SelectedValue.Trim() + " and Isdeleted is null and UserInPocra is not null ").Length == 0)
                    {
                        Util.ShowMessageBox(this.Page, "Error", "Please update your address and add new address under pocra village", "error");
                        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> alert('Please update your address and add new address under pocra village');  </script>", false);
                        return;
                    }

                }

            }

            cla.ExecuteCommand("update Tbl_T_ApplicationDetails Set IsDeleted=1  where RegistrationID=" + Session["RegistrationID"].ToString() + "  and LandID=" + gv.SelectedDataKey["LandID"].ToString() + " and ApprovalStageID > 2 ");
            List<string> lst = new List<string>();


            lst.Add(gv.SelectedDataKey["LandID"].ToString());
            if (cla.ExecuteByProcedure("SP_Remove_LandDetails", lst))
            {
                fillLandDetails();
                LiteralMsg.Text = "";
                Util.ShowMessageBox(this.Page, "Success", "Record Updated Sucessfully", "success");
                DataTable dtLandCheck = cla.GetDataTable("SELECT  TOP (1) L.VillageID, V.City_ID, V.ClustersMasterID, V.TalukaID, V.CircleID FROM Tbl_M_RegistrationLand AS L INNER JOIN Tbl_M_VillageMaster AS V ON V.VillageID = L.VillageID WHERE (L.RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ") AND (L.ParentLandID IS NULL) and L.IsDeleted is null ORDER BY L.LandID");
                if (dtLandCheck.Rows.Count == 0)
                {
                    rdoLandStatus.SelectedValue = "NO";
                    rdoLandStatus_SelectedIndexChanged(null, null);
                }
            }

        }


        #endregion


















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

        protected void btnUpdate8a_Click(object sender, EventArgs e)
        {
            //ViewState["UpLandID"]
            if (txtUpHe.Text.Trim().Length == 0)
            {
                Util.ShowMessageBox(this.Page, "Error", "Please fill Updated Hectare area", "error");
                return;

            }
            if (txtUpAre.Text.Trim().Length == 0)
            {
                Util.ShowMessageBox(this.Page, "Error", "Please fill Are Updated area ", "error");
                return;

            }

            if (cla.ExecuteCommand("update Tbl_M_RegistrationLand set Hectare8A=" + txtUpHe.Text.Trim() + ", Are8A=" + txtUpAre.Text.Trim() + " where LandID=" + ViewState["UpLandID"].ToString().Trim() + "").Trim().Length == 0)
            {
                Util.ShowMessageBox(this.Page, "Success", "Record Updated Sucessfully", "success");
                div8aupdate.Visible = false;
                fillLandDetails();
            }
        }

        protected void grdSubject_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String LandID = grdSubject.DataKeys[e.RowIndex]["LandID"].ToString();
            ViewState["UpLandID"] = LandID;
            div8aupdate.Visible = true;


        }
    }
}