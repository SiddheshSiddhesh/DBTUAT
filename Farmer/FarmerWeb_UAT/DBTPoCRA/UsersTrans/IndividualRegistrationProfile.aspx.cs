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

namespace DBTPoCRA.Registration
{
    public partial class IndividualRegistrationProfile : System.Web.UI.Page
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

                try
                {
                    cla.ExecuteCommand("UPDATE Tbl_M_RegistrationDetails SET PriorityLevelID=((Select  case when R.CategoryMasterID=3 then (case when R.Gender='Male' and R.PhysicallyHandicap='No' then '7' when R.Gender='Male' and R.PhysicallyHandicap='YES' then '6' when R.Gender<>'Male' and R.PhysicallyHandicap='NO' then '5' when R.Gender<>'Male' and R.PhysicallyHandicap='YES' then '4' end   )  when R.CategoryMasterID=2 then (case when R.Gender='Male' and R.PhysicallyHandicap='No' then '11' when R.Gender='Male' and R.PhysicallyHandicap='YES' then '10' when R.Gender<>'Male' and R.PhysicallyHandicap='NO' then '9' when R.Gender<>'Male' and R.PhysicallyHandicap='YES' then '8' end   )   else (case when R.Gender='Male' and R.PhysicallyHandicap='No' then '15' when R.Gender='Male' and R.PhysicallyHandicap='YES' then '14' when R.Gender<>'Male' and R.PhysicallyHandicap='NO' then '13' when R.Gender<>'Male' and R.PhysicallyHandicap='YES' then '12' end   )   end  from Tbl_M_RegistrationDetails As R where R.BeneficiaryTypesID=1 and R.isdeleted is null and R.RegistrationID=Tbl_M_RegistrationDetails.RegistrationID) ) where BeneficiaryTypesID=1 and isDeleted is null and RegistrationID="+ Session["RegistrationID"].ToString() + "");
                }
                catch { }

                if (!IsPostBack)
                {
                    multiViewEmployee.ActiveViewIndex = 0;
                    HttpContext.Current.Session["dt712"] = null;
                    HttpContext.Current.Session["dt8A"] = null;
                    ViewState["LandID"] = "";
                    fillDob();
                    FillDetails();
                    Page.Form.Attributes.Add("enctype", "multipart/form-data");
                    if (Session["Lang"].ToString().Trim() == "mr-IN")
                    {
                        rm = new ResourceManager("Resources.FarmerRegistration", System.Reflection.Assembly.Load("App_GlobalResources"));
                        ci = Thread.CurrentThread.CurrentCulture;
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("mr-IN");
                        LoadString(Thread.CurrentThread.CurrentCulture);

                    }

                }
                else
                {

                    if (Session["Lang"].ToString().Trim() == "mr-IN")
                    {
                        rm = new ResourceManager("Resources.FarmerRegistration", System.Reflection.Assembly.Load("App_GlobalResources"));
                        ci = Thread.CurrentThread.CurrentCulture;
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
            Menu1.Items[1].Text = rm.GetString("BASIC_DETAILS", ci);
            Menu1.Items[2].Text = rm.GetString("LAND_DETAILS", ci);
            Menu1.Items[3].Text = rm.GetString("DECLARATION", ci);

            Literal1.Text = rm.GetString("REGISTRATION_DETAILS", ci);
            Literal2.Text = rm.GetString("AADHAR_NUMBER", ci);
            Literal3.Text = rm.GetString("AUTHENTICATION_TYPE", ci);
            Literal4.Text = rm.GetString("Name", ci);
            Literal5.Text = rm.GetString("DATE_OF_BIRTH", ci);
            Literal6.Text = rm.GetString("GENDER", ci);
            Literal7.Text = rm.GetString("Photo_as_on_Aadhar_Card", ci);

            Literal8.Text = rm.GetString("BASIC_DETAILS", ci);
            Literal9.Text = rm.GetString("HOUSE_NO", ci);
            Literal10.Text = rm.GetString("STREET_NO", ci);
            Literal11.Text = rm.GetString("DISTRICT", ci);
            Literal12.Text = rm.GetString("TALUKA", ci);
            Literal13.Text = rm.GetString("POST", ci);
            Literal14.Text = rm.GetString("PIN_CODE", ci);
            Literal15.Text = rm.GetString("VILLAGE", ci);
            Literal16.Text = rm.GetString("CLUSTER_CODE", ci);
            Literal17.Text = rm.GetString("MOBILE_1", ci);
            Literal18.Text = rm.GetString("MOBILE_2", ci);
            Literal19.Text = rm.GetString("LANDLINE_NO", ci);
            Literal20.Text = rm.GetString("EMAILID", ci);
            Literal21.Text = rm.GetString("PANNO", ci);
            Literal22.Text = rm.GetString("CATEGORY", ci);
            Literal23.Text = rm.GetString("PHYSICALLY_HANDICAP", ci);
            Literal24.Text = rm.GetString("LAND_DETAILS", ci);
            Literal25.Text = rm.GetString("Land_Status", ci);
            Literal26.Text = rm.GetString("DISTRICT", ci);
            Literal27.Text = rm.GetString("TALUKA", ci);
            Literal28.Text = rm.GetString("VILLAGE", ci);
            Literal29.Text = rm.GetString("A_KHATA_KRAMANK", ci);
            Literal30.Text = rm.GetString("Hectare", ci);
            Literal31.Text = rm.GetString("Area", ci);
            Literal32.Text = rm.GetString("FORM_8_A", ci);
            Literal33.Text = rm.GetString("A_KHATA_KRAMANK", ci);
            Literal34.Text = rm.GetString("SURVEY_NUMBER", ci);
            Literal35.Text = rm.GetString("Hectare", ci);
            Literal36.Text = rm.GetString("Area", ci);
            Literal37.Text = rm.GetString("Extracts", ci);
            Literal38.Text = rm.GetString("DECLARATION", ci);
            Literal39.Text = rm.GetString("DECLARATION", ci);
            Literal40.Text = rm.GetString("Info", ci);
            Literal41.Text = rm.GetString("Info1", ci);
            Literal42.Text = rm.GetString("Info2", ci);
            Literal43.Text = rm.GetString("Info3", ci);
            Literal44.Text = rm.GetString("Info4", ci);
            Literal45.Text = rm.GetString("Info5", ci);
            // Literal46.Text = rm.GetString("Info6", ci);
            // Literal47.Text = rm.GetString("Info7", ci);
            Literal48.Text = rm.GetString("Farmer_Registration", ci);
            //Label1.Text = rm.GetString("Info7", ci);
            //Label5.Text = rm.GetString("Info7", ci);
            // Label6.Text = rm.GetString("LandLessCertificate", ci);
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
                txtAADHARNo.Text = dt.Rows[0]["AaDharNumber"].ToString();


                txtName.Text = dt.Rows[0]["RegisterName"].ToString();//RegisterName//CategoryMaster  
                rdoAuthenticationType.SelectedValue = dt.Rows[0]["BeneficiaryTypesID"].ToString();
                string[] str = dt.Rows[0]["DateOfBirth"].ToString().Split('/');
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
                    //    LabelLandlessCert.Text = "<a href='https://dbt.mahapocra.gov.in" + dt.Rows[0]["LandLessCertificate"].ToString() + "' target=_blank >  View Certificate  </a>";
                    //}
                    //else
                    //{
                    //    LabelLandlessCert.Text = "";
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
                    btnRemoveHandi.Visible = true;
                }


                if (dt.Rows[0]["CastCategoryDoc"].ToString().Length > 0)
                    lblCATEGORY.Text = "<a href='" + clsSettings.BaseUrl + "" + dt.Rows[0]["CastCategoryDoc"].ToString() + "' target=_blank >  View Certificate  </a>";
                else
                    lblCATEGORY.Text = "";

                fillLandDetails();

            }

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
            DataTable dt = new DataTable();

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






            //dt = Comcls.GetTalukaMaster();
            //ddlUnit.DataSource = Comcls.GetUnit();
            //ddlUnit.DataTextField = "UnitofMes";
            //ddlUnit.DataValueField = "UnitMasterID";
            //ddlUnit.DataBind();
            //ddlUnit.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlUnit.SelectedIndex = 0;




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
        //protected void txtDISABILITYPer_TextChanged(object sender, EventArgs e)
        //{
        //    FileHANDICAPCERITIFICATE.Visible = false;
        //    Label3.Visible = false;
        //    if (rdoHANDICAP.SelectedValue.Trim() == "YES")
        //    {
        //        if (Convert.ToDouble(txtDISABILITYPer.Text.Trim()) >= 40)
        //        {
        //            FileHANDICAPCERITIFICATE.Visible = true;
        //            Label3.Visible = true;
        //        }
        //        else
        //        {
        //            rdoHANDICAP.SelectedValue = "NO";
        //        }
        //    }
        //}

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
            if (Comcls.RecordExistanceChk("Tbl_M_RegistrationDetails", "AaDharNumber", txtAADHARNo.Text, "RegistrationID", Convert.ToInt32(Session["RegistrationID"].ToString())) == false)
            {
                //clsMessages.Errormsg(LiteralMsg, "Aadhaar number " + txtAADHARNo.Text.ToUpper() + "  is already registered");
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG26", Session["Lang"].ToString()), "error");
                return;
            }




            if (txtAADHARNo.Text.Trim().Length == 0)
            {
                //clsMessages.Warningmsg(LiteralMsg, "Please Fill AAudhar Number");
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG12", Session["Lang"].ToString()), "error");
                return;
            }
            if (txtName.Text.Trim().Length == 0)
            {
                //clsMessages.Warningmsg(LiteralMsg, "Please Fill AAudhar Number");
                Util.ShowMessageBox(this.Page, "Required", MyCommanClass.GetMsgInEnForDB("MSG3", Session["Lang"].ToString()), "error");
                return;
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

                }
            }
            String DOB = ddlDay.SelectedValue.Trim() + "/" + ddlMonth.SelectedValue.Trim() + "/" + ddlYear.SelectedValue.Trim();
            //------------End validations ---------------------// 


            // String LandStatusActive = cla.GetExecuteScalar("Select LandStatus from Tbl_M_RegistrationDetails WHERE RegistrationID ="+ Session["RegistrationID"].ToString().Trim()+ "");


            if (rdoLandStatus.SelectedValue.Trim().ToUpper() == "NO")
            {
                String LandExist = cla.GetExecuteScalar("Select top 1 LandID from Tbl_M_RegistrationLand WHERE RegistrationID =" + Session["RegistrationID"].ToString().Trim() + " and IsDeleted is null ");
                if (LandExist.Trim().Length > 0)
                {
                    //Util.ShowMessageBox(this.Page, "Error", FileError, "error");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> alert('Please remove land from land details to register as landless farmer.');  </script>", false);
                    return;
                }
            }







            //List<String> lst = new List<string>();
            String str = "";
            // int RegistrationID = cla.TableID("Tbl_M_RegistrationDetails", "RegistrationID");


            if (Session["RegistrationID"].ToString().Trim().Length > 0)
            {

                String PathUp = "DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "";
                if (Session["RegistrationID"].ToString().Trim().Length > 0)
                {

                    String LandLessCertificate = "";
                    if (FileLandLessCertificate.HasFile)
                    {
                        if (FileLandLessCertificate.PostedFile.ContentLength < 3145728)
                        {

                            LandLessCertificate = "/admintrans/DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "/LandLessCertificate" + System.IO.Path.GetExtension(FileLandLessCertificate.PostedFile.FileName.ToString());

                            byte[] bin = FileLandLessCertificate.FileBytes;
                            String fileName = "LandLessCertificate" + System.IO.Path.GetExtension(FileLandLessCertificate.PostedFile.FileName.ToString());
                            String s = fileRet.UploadData(PathUp, fileName, bin);
                            if (s.Length > 0)
                            {
                                Util.ShowMessageBox(this.Page, "Error", "Please upload Land Less Certificate", "error");
                                return;
                            }

                            LabelLandlessCert.Text = "<a href='" + clsSettings.BaseUrl + "" + LandLessCertificate + "' target=_blank >  VIEW UPLOADED CERITIFICATE  </a>";
                        }
                    }
                    if (LandLessCertificate.Trim().Length > 0)
                    {
                        str = " UPDATE Tbl_M_RegistrationDetails SET LandStatus ='" + rdoLandStatus.SelectedItem.Value.Trim() + "', ApprovalStatus='Pending' , LandLessCertificate='" + LandLessCertificate + "' WHERE RegistrationID = " + Session["RegistrationID"].ToString() + " ";
                        cla.ExecuteCommand(str);
                    }
                    else
                    {
                        str = " UPDATE Tbl_M_RegistrationDetails SET  ApprovalStatus='Pending'  WHERE RegistrationID = " + Session["RegistrationID"].ToString() + " ";
                        cla.ExecuteCommand(str);
                    }


                    if (ddlAnyOtherCertificate.SelectedValue.Trim() != "NA")
                    {
                        String AnyOtherCertificate = "";
                        if (FileAnyOtherCertificate.HasFile)
                        {
                            if (FileAnyOtherCertificate.PostedFile.ContentLength < 3145728)
                            {
                                //String Uppath = path + "/AnyOtherCertificate" + System.IO.Path.GetExtension(FileAnyOtherCertificate.PostedFile.FileName.ToString());
                                //FileAnyOtherCertificate.PostedFile.SaveAs(Uppath.Trim()); //File is saved in the Physical folder 
                                AnyOtherCertificate = "/admintrans/DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "/AnyOtherCertificate" + System.IO.Path.GetExtension(FileAnyOtherCertificate.PostedFile.FileName.ToString());

                                byte[] bin = FileAnyOtherCertificate.FileBytes;
                                String fileName = "AnyOtherCertificate" + System.IO.Path.GetExtension(FileAnyOtherCertificate.PostedFile.FileName.ToString());
                                //fileRet.UploadData(PathUp, fileName, bin);
                                String s = fileRet.UploadData(PathUp, fileName, bin);
                                if (s.Length > 0)
                                {
                                    Util.ShowMessageBox(this.Page, "Error", "Please upload Any Other Certificate", "error");
                                    return;
                                }
                                LabelLandlessCert.Text = "<a href='" + clsSettings.BaseUrl + "" + AnyOtherCertificate + "' target=_blank >  VIEW UPLOADED CERITIFICATE  </a>";
                            }
                            str = " UPDATE  Tbl_M_RegistrationDetails SET  AnyOtherDocType=N'" + ddlAnyOtherCertificate.SelectedValue.Trim() + "' , AnyOtherDoc='" + AnyOtherCertificate.Trim() + "'  ";
                            str += " WHERE(RegistrationID =" + Session["RegistrationID"].ToString().Trim() + ")";
                            cla.ExecuteCommand(str);
                        }
                    }
                    else
                    {
                        str = " UPDATE  Tbl_M_RegistrationDetails SET  AnyOtherDocType=N'NA' , AnyOtherDoc=NULL  ";
                        str += " WHERE(RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ")";
                        cla.ExecuteCommand(str);
                    }


                    //  }

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


                    if (Session["RegistrationID"].ToString().Trim().Length == 0)
                    {


                    }
                    else
                    {
                        // EDIT 

                        str = " UPDATE Tbl_M_RegistrationDetails SET  ";
                        str += " DateOfBirth='" + cla.mdy(DOB.Trim()) + "',Gender='" + rdoGender.SelectedValue.Trim() + "'";
                        str += " WHERE RegistrationID = " + Session["RegistrationID"].ToString() + " ";
                        cla.ExecuteCommand(str, command);

                        //if (rdoLandStatus.SelectedValue.Trim() == "NO")
                        //{
                        //    //LandLess
                        //    cla.ExecuteCommand("UPDATE Tbl_M_RegistrationLand SET IsDeleted='1'  where  RegistrationID = " + Session["RegistrationID"].ToString().Trim() + "", command);

                        //}

                        transaction.Commit();
                        //
                        multiViewEmployee.ActiveViewIndex = 1;
                        Menu1.Items[1].Enabled = true;


                        if (rdoLandStatus.SelectedValue.Trim() == "YES")
                        {
                            multiViewEmployee.ActiveViewIndex = 2;
                            Menu1.Items[2].Enabled = true;
                        }
                        else
                        {
                            multiViewEmployee.ActiveViewIndex = 3;
                            Menu1.Items[3].Enabled = true;
                        }


                        MyCommanClass.UpdateLandStatusWiseWorkVillage(Session["RegistrationID"].ToString());
                        Util.ShowMessageBox(this.Page, "Success", MyCommanClass.GetMsgInEnForDB("MSG17", Session["Lang"].ToString()), "success");
                    }

                }//
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
                    Util.ShowMessageBox(this.Page, "Required", ex.Message.Trim(), "error");
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

            if (txtAADHARNo.Text.Trim().Length == 0)
            {                
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG12", Session["Lang"].ToString()), "error");
                return;
            }
            else if (txtName.Text.Trim().Length == 0)
            {                
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG3", Session["Lang"].ToString()), "error");
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
            else if (rdoHANDICAP.SelectedValue.Trim() == "YES")
            {
                if (txtDISABILITYPer.Text.Trim().Length > 0)
                {
                    if (FileHANDICAPCERITIFICATE.HasFile == false)
                    {
                        if (lblHandiChetificate.Text.Trim().Length == 0)
                        {                            
                            Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG33", Session["Lang"].ToString()), "error");
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                            return;
                        }

                    }
                    else
                    {
                        // if (lblHandiChetificate.Text.Trim().Length ==0)
                        // {
                        if (FileHANDICAPCERITIFICATE.PostedFile.ContentLength > 3145728)
                        {
                            //  clsMessages.Warningmsg(LiteralMsg, "Please attach max 3 MB file for  Caste Certificate");
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
                        //  }
                        else
                        {
                            // clsMessages.Warningmsg(LiteralMsg, "Please remove old certificate if you want to add new certificate");
                            Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG44", Session["Lang"].ToString()), "error");
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                            return;
                        }
                    }
                }
                else
                {
                    // clsMessages.Warningmsg(LiteralMsg, "Please fill DISABILITY PERCENTAGE ");
                    Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG34", Session["Lang"].ToString()), "error");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                    return;
                }
            }
            //------------End validations ---------------------// 


            String str = "", Work_CircleID = "", Work_ClustersMasterID = "";
            Work_ClustersMasterID = cla.GetExecuteScalar("Select ClustersMasterID from Tbl_M_VillageMaster where VillageID=" + ddlVILLAGE.SelectedValue.Trim() + "");
            Work_CircleID = cla.GetExecuteScalar("Select CircleID from Tbl_M_VillageMaster where VillageID=" + ddlVILLAGE.SelectedValue.Trim() + "");


            if (Session["RegistrationID"].ToString().Trim().Length > 0)
            {
                //String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "");

                //if (!Directory.Exists(path))
                //{
                //    // Try to create the directory.
                //    DirectoryInfo di = Directory.CreateDirectory(path);
                //}
                String PathUp = "DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "";


                String CastCategoryDoc = "";
                if (FileCATEGORYCERITIFICATE.HasFile)
                {
                    if (FileCATEGORYCERITIFICATE.PostedFile.ContentLength < 3145728)
                    {
                        //String Uppath = path + "/CasteCategoryCertificate" + System.IO.Path.GetExtension(FileCATEGORYCERITIFICATE.PostedFile.FileName.ToString());
                        //FileCATEGORYCERITIFICATE.PostedFile.SaveAs(Uppath.Trim()); //File is saved in the Physical folder 
                        CastCategoryDoc = "/admintrans/DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "/CasteCategoryCertificate" + System.IO.Path.GetExtension(FileCATEGORYCERITIFICATE.PostedFile.FileName.ToString());

                        byte[] bin = FileCATEGORYCERITIFICATE.FileBytes;
                        String fileName = "CasteCategoryCertificate" + System.IO.Path.GetExtension(FileCATEGORYCERITIFICATE.PostedFile.FileName.ToString());
                        //fileRet.UploadData(PathUp, fileName, bin);
                        String s = fileRet.UploadData(PathUp, fileName, bin);
                        if (s.Length > 0)
                        {
                            Util.ShowMessageBox(this.Page, "Error", "Please upload Caste Category Certificate", "error");
                        }

                        lblCATEGORY.Text = "<a href='" + clsSettings.BaseUrl + "" + CastCategoryDoc + "' target=_blank >  VIEW UPLOADED CERITIFICATE  </a>";

                        str = " UPDATE  Tbl_M_RegistrationDetails SET CastCategoryDoc ='" + CastCategoryDoc + "'";
                        str += " WHERE (RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ")";
                        cla.ExecuteCommand(str);

                    }
                }
                String PhysicallyHandicapDoc = "";
                if (FileHANDICAPCERITIFICATE.HasFile)
                {
                    if (FileHANDICAPCERITIFICATE.PostedFile.ContentLength < 3145728)
                    {
                        //String Uppath = path + "/PhysicallyHandicapCertificate" + System.IO.Path.GetExtension(FileHANDICAPCERITIFICATE.PostedFile.FileName.ToString());
                        //FileHANDICAPCERITIFICATE.PostedFile.SaveAs(Uppath.Trim()); //File is saved in the Physical folder 
                        PhysicallyHandicapDoc = "/admintrans/DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "/PhysicallyHandicapCertificate" + System.IO.Path.GetExtension(FileHANDICAPCERITIFICATE.PostedFile.FileName.ToString());

                        byte[] bin = FileHANDICAPCERITIFICATE.FileBytes;
                        String fileName = "PhysicallyHandicapCertificate" + System.IO.Path.GetExtension(FileHANDICAPCERITIFICATE.PostedFile.FileName.ToString());
                        // fileRet.UploadData(PathUp, fileName, bin);
                        String s = fileRet.UploadData(PathUp, fileName, bin);
                        if (s.Length > 0)
                        {
                            Util.ShowMessageBox(this.Page, "Error", "Please upload Physically Handicap Certificate", "error");
                        }
                        lblHandiChetificate.Text = "<a href='" + clsSettings.BaseUrl + "" + PhysicallyHandicapDoc + "' target=_blank >  VIEW UPLOADED CERITIFICATE  </a>";


                        str = " UPDATE  Tbl_M_RegistrationDetails SET PhysicallyHandicapDoc ='" + CastCategoryDoc + "'";
                        str += " WHERE (RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ")";
                        cla.ExecuteCommand(str);

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
                        str += " Address1HouseNo =N'" + txtHouseNo.Text.Trim() + "', Address1StreetName =N'" + txtStreetNo.Text.Trim() + "', Address1City_ID =" + ddlDISTRICT.SelectedValue.Trim() + ", Address1TalukaID =" + ddlTALUKA.SelectedValue.Trim() + ", Address1Post_ID =" + ddlPOST.SelectedValue.Trim() + ", ";
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

                        str += " WHERE(RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ")";
                        cla.ExecuteCommand(str, command);
                        txtAADHARNo.ReadOnly = true;
                        txtName.ReadOnly = true;
                    }

                    if (cla.GetExecuteScalar("Select ApprovalStatus from Tbl_M_RegistrationDetails WHERE RegistrationID = " + Session["RegistrationID"].ToString() + "   ", command).Trim() != "Back To Beneficiary")
                    {
                        str = " UPDATE Tbl_M_RegistrationDetails SET ApprovalStatus='Pending' WHERE RegistrationID = " + Session["RegistrationID"].ToString() + " ";
                        cla.ExecuteCommand(str, command);
                    }


                    transaction.Commit();
                    //clsMessages.Sucessmsg(LiteralMsg, "S");
                    if (rdoLandStatus.SelectedValue.Trim() == "YES")
                    {
                        multiViewEmployee.ActiveViewIndex = 2;
                        Menu1.Items[2].Enabled = true;
                    }
                    else
                    {
                        multiViewEmployee.ActiveViewIndex = 3;
                        Menu1.Items[3].Enabled = true;
                    }
                    //   clsMessages.Sucessmsg(LiteralMsg, "U");
                    Util.ShowMessageBox(this.Page, "Success", MyCommanClass.GetMsgInEnForDB("MSG17", Session["Lang"].ToString()), "success");
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
        protected void rdoLandStatus_SelectedIndexChanged1(object sender, EventArgs e)
        {
            divLand.Visible = false;
            btnLandNext.Visible = false;
            if (rdoLandStatus.SelectedValue.Trim() == "YES")
            {
                divLand.Visible = true;

            }
            else
            {
                btnLandNext.Visible = true;
            }
        }
        protected void btnADD_Click(object sender, EventArgs e)
        {
            LiteralMsg.Text = "";

            if (txtSURVEYNo8A.Text.Trim().Length == 0)
            {
                // clsMessages.Warningmsg(LiteralMsg, "Please Add 8-A KHATA KRAMANK.");
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG35", Session["Lang"].ToString()), "error");
                return;
            }

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
                return;

            }
            else
            {
                if (FileFORM8A.PostedFile.ContentLength > 3145728)
                {
                    //clsMessages.Warningmsg(LiteralMsg, "Please attach max 3 MB file for Form 8A.");
                    Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG37", Session["Lang"].ToString()), "error");
                    return;
                }
                string FileError = MyCommanClass.CheckAllowedFileExtension(System.IO.Path.GetExtension(FileFORM8A.PostedFile.FileName.ToString()));
                if (FileError.Length > 0)
                {
                    Util.ShowMessageBox(this.Page, "Error", FileError, "error");
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                    return;
                }
            }

            if (ViewState["LandID"].ToString().Trim().Length == 0)
            {
                String s = cla.GetExecuteScalar("SELECT top 1 LandID FROM Tbl_M_RegistrationLand where RegistrationID=" + Session["RegistrationID"].ToString().Trim() + " and City_ID=" + ddlLANDDISTRICT.SelectedValue.Trim() + " and TalukaID=" + ddlLANDTALUKA.SelectedValue.Trim() + "  and VillageID=" + ddlLANDVILLAGE.SelectedValue.Trim() + " and IsDeleted is null");
                if (s.Length > 0)
                {
                    //clsMessages.Warningmsg(LiteralMsg, "Only one 8-A is allowed per village.");
                    Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG38", Session["Lang"].ToString()), "error");
                    return;
                }
            }
            else
            {
                String s = cla.GetExecuteScalar("SELECT top 1 LandID FROM Tbl_M_RegistrationLand where LandID <> " + ViewState["LandID"].ToString().Trim() + "  AND  RegistrationID=" + Session["RegistrationID"].ToString().Trim() + " and City_ID=" + ddlLANDDISTRICT.SelectedValue.Trim() + " and TalukaID=" + ddlLANDTALUKA.SelectedValue.Trim() + "  and VillageID=" + ddlLANDVILLAGE.SelectedValue.Trim() + " and IsDeleted is null");
                if (s.Length > 0)
                {
                    //clsMessages.Warningmsg(LiteralMsg, "Only one 8-A is allowed per village.");
                    Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG38", Session["Lang"].ToString()), "error");
                    return;
                }
            }
            String str = "";
            int LandID = cla.TableID("Tbl_M_RegistrationLand", "LandID");

            string filepath = "";
            if (Session["RegistrationID"].ToString().Trim().Length > 0)
            {
                String PathUp = "DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "";
                if (FileFORM8A.HasFile)
                {

                    if (FileFORM8A.PostedFile.ContentLength < 3145728)
                    {
                        String fileNames = "/FORM8A_" + LandID.ToString() + "" + System.IO.Path.GetExtension(FileFORM8A.PostedFile.FileName.ToString());
                        filepath = "/admintrans/DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + fileNames;
                        byte[] bin = FileFORM8A.FileBytes;
                        String fileName = "FORM8A_" + LandID.ToString() + "" + System.IO.Path.GetExtension(FileFORM8A.PostedFile.FileName.ToString());

                        String s = fileRet.UploadData(PathUp, fileName, bin);
                        if (s.Length > 0)
                        {
                            Util.ShowMessageBox(this.Page, "Error", "Please upload FORM8A", "error");
                        }


                        if (ViewState["LandID"].ToString().Trim().Length > 0)
                        {
                            str = " UPDATE Tbl_M_RegistrationLand SET ";
                            str += " IsDeleted='1'  ";
                            str += " WHERE (LandID = " + ViewState["LandID"].ToString() + ")";
                            cla.ExecuteCommand(str);

                            str = " UPDATE Tbl_M_RegistrationLand SET";
                            str += " ParentLandID=" + LandID + "  ";
                            str += " WHERE (ParentLandID = " + ViewState["LandID"].ToString() + ")";
                            cla.ExecuteCommand(str);

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



                        LandID = cla.TableID("Tbl_M_RegistrationLand", "LandID", command);

                        str = " INSERT INTO Tbl_M_RegistrationLand (LandID, RegistrationID, City_ID, TalukaID, VillageID, AccountNumber8A, Hectare8A, Are8A, Form8ADoc)";
                        str += " VALUES(" + LandID + "," + Session["RegistrationID"].ToString().Trim() + "," + ddlLANDDISTRICT.SelectedValue.Trim() + "," + ddlLANDTALUKA.SelectedValue.Trim() + "," + ddlLANDVILLAGE.SelectedValue.Trim() + ",'" + txtSURVEYNo8A.Text.Trim() + "'," + txtLANDAREA8AH.Text.Trim() + "," + txtLANDAREA8AA.Text.Trim() + ",'" + filepath + "')";
                        cla.ExecuteCommand(str, command);

                        div7A.Visible = true;
                        btnADD.Enabled = false;
                        rdoLandStatus.Enabled = false;

                        if (cla.GetExecuteScalar("Select ApprovalStatus from Tbl_M_RegistrationDetails WHERE RegistrationID = " + Session["RegistrationID"].ToString() + "   ", command).Trim() != "Back To Beneficiary")
                        {
                            str = " UPDATE Tbl_M_RegistrationDetails SET ApprovalStatus='Pending' WHERE RegistrationID = " + Session["RegistrationID"].ToString() + " ";
                            cla.ExecuteCommand(str, command);
                        }
                    }
                    transaction.Commit();










                    //clsMessages.Sucessmsg(LiteralMsg, "U");
                    Util.ShowMessageBox(this.Page, "Success", MyCommanClass.GetMsgInEnForDB("MSG17", Session["Lang"].ToString()), "success");
                    DataTable dt = cla.GetDataTable("SELECT LandID, AccountNumber8A FROM Tbl_M_RegistrationLand where RegistrationID=" + Session["RegistrationID"].ToString() + " and IsDeleted is null  and AccountNumber8A is not null ");
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
                    MyCommanClass.UpdateLandStatusWiseWorkVillage(Session["RegistrationID"].ToString());
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
                    //  clsMessages.Errormsg(LiteralMsg, "Please provide correct input." + ex.Message.Trim());
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
                // clsMessages.Warningmsg(LiteralMsg, "Please Add Form 8A first then you can add multiple 7/12 under Form 8A");
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG39", Session["Lang"].ToString()), "error");
                return;
            }
            else if (ddl8A.SelectedIndex == 0)
            {
                // clsMessages.Warningmsg(LiteralMsg, "Please Select Form 8A first then you can add multiple 7/12 under Form 8A");
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG40", Session["Lang"].ToString()), "error");
                return;
            }

            if (File712.HasFile == false)
            {
                // clsMessages.Warningmsg(LiteralMsg, "Please attach 7 / 12.");
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG41", Session["Lang"].ToString()), "error");

            }
            else
            {
                if (File712.PostedFile.ContentLength > 3145728)
                {
                    //clsMessages.Warningmsg(LiteralMsg, "Please attach max 3 MB file for 7 / 12.");
                    Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG42", Session["Lang"].ToString()), "error");
                }
                string FileError = MyCommanClass.CheckAllowedFileExtension(System.IO.Path.GetExtension(File712.PostedFile.FileName.ToString()));
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
                String str = "";

                if (File712.PostedFile.ContentLength < 3145728)
                {


                    String fileNames = "/SURVEYNUMBER712_" + LandID.ToString() + "" + System.IO.Path.GetExtension(File712.PostedFile.FileName.ToString());
                    filepath = "/admintrans/DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + fileNames;
                    byte[] bin = File712.FileBytes;
                    String fileName = "SURVEYNUMBER712_" + LandID.ToString() + "" + System.IO.Path.GetExtension(File712.PostedFile.FileName.ToString());
                    String s = fileRet.UploadData(PathUp, fileName, bin);
                    if (s.Length > 0)
                    {
                        Util.ShowMessageBox(this.Page, "Error", "Please upload 712", "error");
                    }

                    if (ViewState["LandID"].ToString().Trim().Length > 0)
                    {
                        str = " UPDATE Tbl_M_RegistrationLand SET";
                        str += " IsDeleted='1'  ";
                        str += " WHERE(LandID = " + ViewState["LandID"].ToString() + ")";
                        cla.ExecuteCommand(str);


                    }

                    LandID = cla.TableID("Tbl_M_RegistrationLand", "LandID");
                    str = " INSERT INTO Tbl_M_RegistrationLand ( LandID, ParentLandID , SurveyNo712, Hectare712, Are712, Extracts712Doc,RegistrationID)";
                    str += " VALUES(" + LandID + "," + ddl8A.SelectedValue.Trim() + ",'" + txtSURVEYNo712.Text.Trim() + "','" + txtLANDAREA712H.Text.Trim() + "','" + txtLANDAREA712A.Text.Trim() + "','" + filepath + "'," + Session["RegistrationID"].ToString().Trim() + ")";//ViewState
                    s = cla.ExecuteCommand(str);

                    if (s.Length == 0)
                    {

                        if (cla.GetExecuteScalar("Select ApprovalStatus from Tbl_M_RegistrationDetails WHERE RegistrationID = " + Session["RegistrationID"].ToString() + "   ").Trim() != "Back To Beneficiary")
                        {
                            str = " UPDATE Tbl_M_RegistrationDetails SET ApprovalStatus='Pending' WHERE RegistrationID = " + Session["RegistrationID"].ToString() + " ";
                            cla.ExecuteCommand(str);
                        }


                        txtSURVEYNo712.Text = "";
                        txtLANDAREA712H.Text = "";
                        txtLANDAREA712A.Text = "";

                        div7A.Visible = true;
                        btnADD.Enabled = true;
                        fillLandDetails();
                        btnLandNext.Visible = true;

                        ////   ddlLANDVILLAGE.SelectedIndex = 0;
                        ////        ddlLANDTALUKA.SelectedIndex = 0;
                        ////       ddlLANDDISTRICT.SelectedIndex = 0;
                        //clsMessages.Sucessmsg(LiteralMsg, "U");
                        MyCommanClass.UpdateLandStatusWiseWorkVillage(Session["RegistrationID"].ToString());
                        Util.ShowMessageBox(this.Page, "info", MyCommanClass.GetMsgInEnForDB("MSG17", Session["Lang"].ToString()), "success");
                        //multiViewEmployee.ActiveViewIndex = 3;
                        //Menu1.Items[3].Enabled = true;
                    }

                    else
                    {
                        clsMessages.Errormsg(LiteralMsg, s.Trim());
                        //  Util.ShowMessageBox(this.Page, "Error", "Only one 8-A is allowed per village.", "error");
                    }
                }
            }
        }


        private void fillLandDetails()
        {
            List<String> lst = new List<string>();
            //lst.Add(Session["RegistrationID"].ToString().Trim());
            //DataTable dt = cla.GetDtByProcedure("SP_GetLandDetails", lst);
            //grdSubject.DataSource = dt;
            //grdSubject.DataBind();
            lst.Add(Session["RegistrationID"].ToString().Trim());
            DataTable dt = cla.GetDtByProcedure("SP_GetLandParentDetails", lst);
            grdSubject.DataSource = dt;
            grdSubject.DataBind();

            dt = cla.GetDataTable("SELECT LandID, AccountNumber8A FROM Tbl_M_RegistrationLand where RegistrationID=" + Session["RegistrationID"].ToString() + " and IsDeleted is null  and AccountNumber8A is not null ");
            ddl8A.DataSource = dt;
            ddl8A.DataTextField = "AccountNumber8A";
            ddl8A.DataValueField = "LandID";
            ddl8A.DataBind();
            ddl8A.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        protected void btnLandNext_Click(object sender, EventArgs e)
        {
            //multiViewEmployee.ActiveViewIndex = 3;
            //Menu1.Items[3].Enabled = true;

            //String str = " UPDATE  Tbl_M_RegistrationDetails SET  LandStatus ='" + rdoLandStatus.SelectedItem.Value.Trim() + "' ";
            //str += " WHERE(RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ")";
            //cla.ExecuteCommand(str);
            //clsMessages.Sucessmsg(LiteralMsg, "U");

            LiteralMsg.Text = "";


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

            //if (cla.GetExecuteScalar("select  top 1 LandID  from Tbl_M_RegistrationLand where RegistrationID=" + Session["RegistrationID"].ToString().Trim() + " and IsDeleted is null and ParentLandID is null and  Hectare8A<(select  ISNULL(Sum(LL.Hectare712),0)from Tbl_M_RegistrationLand LL where LL.RegistrationID=" + Session["RegistrationID"].ToString().Trim() + " and LL.IsDeleted is null and LL.ParentLandID=Tbl_M_RegistrationLand.LandID) ").Length > 0)
            //{
            //    ErrorCount++;
            //}
            //if (cla.GetExecuteScalar("select  top 1 LandID  from Tbl_M_RegistrationLand where RegistrationID=" + Session["RegistrationID"].ToString().Trim() + " and IsDeleted is null and ParentLandID is null and  Are8A<(select  ISNULL(Sum(LL.Are712),0)from Tbl_M_RegistrationLand LL where LL.RegistrationID=" + Session["RegistrationID"].ToString().Trim() + " and LL.IsDeleted is null and LL.ParentLandID=Tbl_M_RegistrationLand.LandID)").Trim().Length > 0)
            //{
            //    ErrorCount++;
            //}

            if (ErrorCount == 0)
            {




                String str = "";

                DataTable dtLand = cla.GetDataTable("SELECT  TOP (1) L.VillageID, V.City_ID, V.ClustersMasterID, V.TalukaID, V.CircleID FROM Tbl_M_RegistrationLand AS L INNER JOIN Tbl_M_VillageMaster AS V ON V.VillageID = L.VillageID WHERE (L.RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ") AND (L.ParentLandID IS NULL) ORDER BY L.LandID");
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
                        str += " WHERE (RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ")";
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
                                    str += " WHERE (RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ")";
                                    cla.ExecuteCommand(str, command);
                                }
                            }
                            else
                            {
                                str = " UPDATE  Tbl_M_RegistrationDetails SET  Work_City_ID =NULL, Work_TalukaID =NULL, Work_VillageID =NULL, Work_CircleID =NULL, Work_ClustersMasterID =NULL ";
                                str += " WHERE (RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ")";
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
            else
            {
                // clsMessages.Warningmsg(LiteralMsg, "Sum of hectare & are from 7/12 will be equal to hectare & are of 8A respectively.");
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG43", Session["Lang"].ToString()), "error");
                return;
            }
        }


        protected void grdSubject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string ParentLandID = grdSubject.DataKeys[e.Row.RowIndex]["LandID"].ToString();
                //String RegistrationID = grdSubject.DataKeys[e.Row.RowIndex]["RegistrationID"].ToString();
                GridView SC = (GridView)e.Row.FindControl("grdChild");


                DataTable dt = cla.GetDataTable("SELECT   LandID , SurveyNo712, Hectare712, Are712,  '<a target=_blank href=''https://dbtpocradata.blob.core.windows.net'+Extracts712Doc+'''> View Extracts  </a>' as Extracts712Doc FROM  Tbl_M_RegistrationLand WHERE  (ParentLandID = " + ParentLandID + ") AND (IsDeleted IS NULL) AND RegistrationID=" + Session["RegistrationID"].ToString().Trim() + " ");
                SC.DataSource = dt;
                SC.DataBind();
            }
        }

        protected void grdSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LandID

            String S = cla.GetExecuteScalar("SELECT TOP 1 ApplicationID  FROM Tbl_T_ApplicationDetails where  LandID in (Select LandID From Tbl_M_RegistrationLand where ParentLandID=" + grdSubject.SelectedDataKey["LandID"].ToString() + ")  and IsDeleted is null and RegistrationID=" + Session["RegistrationID"].ToString() + " and ApprovalStageID > 2 ");
            if (S.Length > 0)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> alert('Land Can not be deleted because it is used in application');  </script>", false);
                return;

            }

            cla.ExecuteCommand("update Tbl_T_ApplicationDetails Set IsDeleted=1  where RegistrationID=" + Session["RegistrationID"].ToString() + "  and LandID=" + grdSubject.SelectedDataKey["LandID"].ToString() + " and ApprovalStageID > 2 ");
            List<string> lst = new List<string>();
            lst.Add(grdSubject.SelectedDataKey["LandID"].ToString());
            cla.ExecuteByProcedure("SP_Remove_LandDetails", lst);
            fillLandDetails();
            LiteralMsg.Text = "";

            DataTable dtLandCheck = cla.GetDataTable("SELECT  TOP (1) L.VillageID, V.City_ID, V.ClustersMasterID, V.TalukaID, V.CircleID FROM Tbl_M_RegistrationLand AS L INNER JOIN Tbl_M_VillageMaster AS V ON V.VillageID = L.VillageID WHERE (L.RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ") AND (L.ParentLandID IS NULL) and L.IsDeleted is null ORDER BY L.LandID");
            if (dtLandCheck.Rows.Count == 0)
            {
                rdoLandStatus.SelectedValue = "NO";
                String str = " UPDATE Tbl_M_RegistrationDetails SET LandStatus ='NO' WHERE RegistrationID = " + Session["RegistrationID"].ToString() + " ";
                cla.ExecuteCommand(str);
            }

            MyCommanClass.UpdateLandStatusWiseWorkVillage(Session["RegistrationID"].ToString());
            Util.ShowMessageBox(this.Page, "info", MyCommanClass.GetMsgInEnForDB("MSG23", Session["Lang"].ToString()), "success");
        }

        protected void grdChild_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LandID
            GridView gv = (GridView)sender;
            String S = cla.GetExecuteScalar("SELECT TOP 1 ApplicationID  FROM Tbl_T_ApplicationDetails where LandID=" + gv.SelectedDataKey["LandID"].ToString() + " and IsDeleted is null and RegistrationID=" + Session["RegistrationID"].ToString() + " and ApprovalStageID > 2 ");
            if (S.Length > 0)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> alert('Land Can not be deleted because it is used in application');  </script>", false);
                return;

            }

            // check if he want to delete all lands then he need to check work villlage from address .
            S = cla.GetExecuteScalar("SELECT  isnull(count(L.LandID),0) FROM Tbl_M_RegistrationLand AS L INNER JOIN Tbl_M_VillageMaster AS V ON V.VillageID = L.VillageID WHERE (L.RegistrationID = " + Session["RegistrationID"].ToString() + ") AND (L.ParentLandID IS NULL) and L.isdeleted is null and L.VillageID <> 0 ");
            if (S.Length > 0)
            {
                if (Convert.ToInt32(S) <= 1)
                {
                    if (cla.GetExecuteScalar("select top 1 VillageID from Tbl_M_VillageMaster where VillageID="+ddlVILLAGE.SelectedValue.Trim()+" and Isdeleted is null and UserInPocra is not null ").Length == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> alert('Please update your address and add new address under pocra village');  </script>", false);
                        return;
                    }

                }

            }

            cla.ExecuteCommand("update Tbl_T_ApplicationDetails Set IsDeleted=1  where RegistrationID=" + Session["RegistrationID"].ToString() + "  and LandID=" + gv.SelectedDataKey["LandID"].ToString() + " and ApprovalStageID > 2 ");
            List<string> lst = new List<string>();
            lst.Add(gv.SelectedDataKey["LandID"].ToString());
            cla.ExecuteByProcedure("SP_Remove_LandDetails", lst);
            fillLandDetails();
            LiteralMsg.Text = "";
            DataTable dtLandCheck = cla.GetDataTable("SELECT  TOP (1) L.VillageID, V.City_ID, V.ClustersMasterID, V.TalukaID, V.CircleID FROM Tbl_M_RegistrationLand AS L INNER JOIN Tbl_M_VillageMaster AS V ON V.VillageID = L.VillageID WHERE (L.RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ") AND (L.ParentLandID IS NULL) and L.IsDeleted is null ORDER BY L.LandID");
            if (dtLandCheck.Rows.Count == 0)
            {
                rdoLandStatus.SelectedValue = "NO";
                String str = " UPDATE Tbl_M_RegistrationDetails SET LandStatus ='NO' WHERE RegistrationID = " + Session["RegistrationID"].ToString() + " ";
                cla.ExecuteCommand(str);
            }
            Util.ShowMessageBox(this.Page, "info", MyCommanClass.GetMsgInEnForDB("MSG23", Session["Lang"].ToString()), "success");
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
                // clsMessages.Warningmsg(LiteralMsg, "Please accept all Declarations to register under Nanaji Deshmukh Krishi Sanjivani Prakalp");
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


                    if (Session["RegistrationID"].ToString().Trim().Length > 0)
                    {
                        // UPDATE
                        str = " UPDATE  Tbl_M_RegistrationDetails SET  ApprovalStatus='Pending' , IAgree ='1' , IsDeleted=NULL ,UserID='" + txtAADHARNo.Text.Trim() + "' ";
                        str += " WHERE(RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ")";
                        cla.ExecuteCommand(str, command);


                        //str = " INSERT INTO Tbl_M_LoginDetails (UserId, RegistrationID, UserName, UPass, LoginAs, FullName)";
                        //str += " VALUES("+ UserId + ","+Session["RegistrationID"].ToString().Trim()+ ",'" + txtAADHARNo.Text.Trim() + "','" + txtAADHARNo.Text.Trim() + "','Beneficiary','" + txtName.Text.Trim()+"')";
                        //cla.ExecuteCommand(str, command);
                    }


                    transaction.Commit();
                    //clsMessages.Sucessmsg2(LiteralMsg, MyCommanClass.GetMsgInEnForDB("MSG10", Session["Lang"].ToString()));
                    Util.ShowMessageBox(this.Page, "Success", MyCommanClass.GetMsgInEnForDB("MSG10", Session["Lang"].ToString()), "success");
                    Session["RegistrationIDPass"] = Session["RegistrationID"].ToString().Trim();
                    Response.Redirect("RegisterSucess.aspx?" + EncryptDecryptQueryString.encrypt(string.Format("ID={0}", Session["RegistrationID"].ToString().Trim())), true);
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
                }

            }

        }



        #endregion

        protected void rdoAuthenticationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtOtp.Visible = false;
            if (rdoAuthenticationType.SelectedValue.Trim() == "OTP")
            {
                btnOtp.Visible = true;

            }
            else
            {
                btnOtp.Visible = false;

            }
        }

        protected void btnOtp_Click(object sender, EventArgs e)
        {

            if (rdoAuthenticationType.SelectedValue.Trim() == "OTP")
            {
                btnOtp.Visible = true;
                txtOtp.Visible = true;
            }
            else
            {
                btnOtp.Visible = false;
                txtOtp.Visible = false;
            }
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> submitCheck();  </script>", false);
        }

        protected void btnRemoveHandi_Click(object sender, EventArgs e)
        {
            // UPDATE
            String str = " UPDATE  Tbl_M_RegistrationDetails SET ";
            str += " PhysicallyHandicap ='NO', DisabilityPer =NULL , ";
            str += " PhysicallyHandicapDoc =NULL";
            str += " WHERE (RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ")";
            if (cla.ExecuteCommand(str).Length == 0)
            {
                lblHandiChetificate.Text = "";
            }
        }

        protected void btnRemoveCast_Click(object sender, EventArgs e)
        {
            String str = " UPDATE  Tbl_M_RegistrationDetails SET ";
            //str += " PhysicallyHandicap ='NO', DisabilityPer =NULL , ";
            str += " CastCategoryDoc =NULL , CategoryMasterID=NULL";
            str += " WHERE (RegistrationID = " + Session["RegistrationID"].ToString().Trim() + ")";
            if (cla.ExecuteCommand(str).Length == 0)
            {
                lblCATEGORY.Text = "";
            }
        }

        protected void rdoLandStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            divLandLess.Visible = false;
            if (rdoLandStatus.SelectedValue.Trim() == "NO")
            {
                divLandLess.Visible = true;

            }
        }


        
    }
}