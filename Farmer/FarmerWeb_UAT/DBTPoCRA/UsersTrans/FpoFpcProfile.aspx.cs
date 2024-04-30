using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommanClsLibrary;
using System.IO;
using System.Data;
using System.Data.SqlClient;


namespace DBTPoCRA.UsersTrans
{
    public partial class FpoFpcProfile : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        #region"Declarection"
        MyClass cla = new MyClass();
        MyCommanClass Comcls = new MyCommanClass();
        string strQuery = "";
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                multiViewEmployee.ActiveViewIndex = 0;
                ViewState["RegistrationID"] = "";
                FillDropDowns();
                FillDetails();

               
                Page.Form.Attributes.Add("enctype", "multipart/form-data");

                //String path = Server.MapPath("/AdminTrans/DocMasters/MemberDoc/1");

            }
        }

        private void FillDetails()
        {
            ViewState["RegistrationID"] = Session["RegistrationID"].ToString();
            List<String> lst = new List<String>();
            lst.Add(Session["RegistrationID"].ToString());
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            dt = new DataTable();
            dt = cla.GetDtByProcedure("SP_FOthersRegistration_Details", lst);

            if (dt.Rows.Count > 0)
            {
                ddlBENEFICIARY.SelectedValue = dt.Rows[0]["BeneficiaryTypesID"].ToString();
                txtName.Text = dt.Rows[0]["RegisterName"].ToString();//RegisterName//CategoryMaster          
                txtRegistationNo.Text = dt.Rows[0]["FPORegistrationNo"].ToString();
                txtRegistDate.Text = dt.Rows[0]["RegistrationDate"].ToString();//FPORegistrationDate
                //FileUpload2   
                if (dt.Rows[0]["RegistrationCertifecate"].ToString().Length > 0)
                    lblFileUpload2.Text = "<a target=_blank  href='" + dt.Rows[0]["RegistrationCertifecate"].ToString() + "'> View Certificate </a>";
                ddlRegisteredUnder.SelectedValue = dt.Rows[0]["RegisterUnderID"].ToString();
                ddlRegisteredThrough.SelectedValue = dt.Rows[0]["RegisteredThroughID"].ToString();
                txtPromoterName.Text = dt.Rows[0]["PromoterName"].ToString();
                ddlGenderPROMOTER.SelectedValue = dt.Rows[0]["PromoterGender"].ToString();
                txtMobileNo.Text = dt.Rows[0]["Promotermobile"].ToString();
                txtLandlineNo.Text = dt.Rows[0]["Promoterlandline"].ToString();
                txtEmail.Text = dt.Rows[0]["PromoterEmail"].ToString();
                txtCeoSecName.Text = dt.Rows[0]["CeoName"].ToString();
                ddlGenderCEO.SelectedValue = dt.Rows[0]["CeoGender"].ToString();
                txtCeoSecMobile.Text = dt.Rows[0]["CeoMobile"].ToString();
                txtCeoSecLandline.Text = dt.Rows[0]["CeoLandline"].ToString();
                txtCeoEmailId.Text = dt.Rows[0]["CeoEmail"].ToString();
                txtAuthorisedPerson.Text = dt.Rows[0]["CeoAuthorisedPerson"].ToString();
                txtDesignation.Text = dt.Rows[0]["CeoDesignation"].ToString();
                //fileupload3
               if (dt.Rows[0]["RegistrationCertifecate"].ToString().Length > 0)
                    lblFileUpload3.Text = "<a target=_blank  href='" + dt.Rows[0]["RegistrationCertifecate"].ToString() + "'> View Certificate </a>";// dt.Rows[0]["RegistrationCertifecate"].ToString();
                ddlAuthPerGender.SelectedValue = dt.Rows[0]["CeoAuthorisedPersonGen"].ToString();
                txtAuthPerMobile.Text = dt.Rows[0]["CeoAuthorisedPersonMob"].ToString();
                txtAuthPerEmail.Text = dt.Rows[0]["CeoAuthorisedPersonEmail"].ToString();
                txtOfficeNo.Text = dt.Rows[0]["Address1HouseNo"].ToString();
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
                txtEmailID.Text = dt.Rows[0]["EmailID"].ToString();
                txtPAN.Text = dt.Rows[0]["PanNumber"].ToString();
                if(dt.Rows[0]["IsBothAddressSame"].ToString().Trim()=="True")
                {
                    chkSameAddress.Checked = true;
                    chkSameAddress_CheckedChanged(null, null);
                   
                }
                
               
              // chkSameAddress.Checked =dt.Rows[0]["IsBothAddressSame"].ToString();
                txtOfficeCorres.Text = dt.Rows[0]["Address2HouseNo"].ToString();
                txtStreetCorres.Text = dt.Rows[0]["Address2StreetName"].ToString();
                ddlCorresCity.SelectedValue = dt.Rows[0]["Address2City_ID"].ToString();
                ddlCorresCity_SelectedIndexChanged(null, null);
                ddlCorresTaluka.SelectedValue = dt.Rows[0]["Address2TalukaID"].ToString();
                ddlCorresTaluka_SelectedIndexChanged(null, null);
                ddlCorresPost.SelectedValue = dt.Rows[0]["Address2Post_ID"].ToString();
                ddlCorresPost_SelectedIndexChanged(null, null);
                txtCorresPinCode.Text = dt.Rows[0]["Address2PinCode"].ToString();
                ddlCorresVillage.SelectedValue = dt.Rows[0]["Address2VillageID"].ToString();//Address2VillageID
                txtCorresCode.Text = dt.Rows[0]["Clusters"].ToString();
                txtCorresMobile.Text = dt.Rows[0]["Address2Mob1"].ToString();
                txtCorresMobile2.Text = dt.Rows[0]["Address2Mob2"].ToString();
                txtCorresLandLine.Text = dt.Rows[0]["Address2LandLine"].ToString();
              
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


        private void FillDropDowns()
        {
            DataTable dt = Comcls.GetCity("27");
            ddlDISTRICT.DataSource = dt;
            ddlDISTRICT.DataTextField = "Cityname";
            ddlDISTRICT.DataValueField = "City_ID";
            ddlDISTRICT.DataBind();
            ddlDISTRICT.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlDISTRICT.SelectedIndex = 0;

            ddlCorresCity.DataSource = dt;
            ddlCorresCity.DataTextField = "Cityname";
            ddlCorresCity.DataValueField = "City_ID";
            ddlCorresCity.DataBind();
            ddlCorresCity.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlCorresCity.SelectedIndex = 0;


            ddlRegisteredUnder.DataSource = Comcls.GetRegisteredUnder();
            ddlRegisteredUnder.DataTextField = "Test";
            ddlRegisteredUnder.DataValueField = "ID";
            ddlRegisteredUnder.DataBind();
            ddlRegisteredUnder.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlRegisteredUnder.SelectedIndex = 0;


            ddlRegisteredThrough.DataSource = Comcls.GetRegisteredThrough();
            ddlRegisteredThrough.DataTextField = "Test";
            ddlRegisteredThrough.DataValueField = "ID";
            ddlRegisteredThrough.DataBind();
            ddlRegisteredThrough.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlRegisteredThrough.SelectedIndex = 0;



            //ddlCATEGORY.DataSource = Comcls.GetCostCategoryMaster();
            //ddlCATEGORY.DataTextField = "CategoryMaster";
            //ddlCATEGORY.DataValueField = "CategoryMasterID";
            //ddlCATEGORY.DataBind();
            //ddlCATEGORY.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlCATEGORY.SelectedIndex = 0;


            //ddlGramPanchayatCode.DataSource = Comcls.GetVillage();
            //ddlGramPanchayatCode.DataTextField = "VillageName";
            //ddlGramPanchayatCode.DataValueField = "VillageID";
            //ddlGramPanchayatCode.DataBind();
            //ddlGramPanchayatCode.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlGramPanchayatCode.SelectedIndex = 0;




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
        protected void ddlCorresCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCorresVillage.Items.Count > 0)
                ddlCorresVillage.Items.Clear();

            ddlCorresTaluka.DataSource = Comcls.GetTalukaMaster(ddlCorresCity.SelectedValue.Trim());
            ddlCorresTaluka.DataTextField = "Taluka";
            ddlCorresTaluka.DataValueField = "TalukaID";
            ddlCorresTaluka.DataBind();
            ddlCorresTaluka.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlCorresTaluka.SelectedIndex = 0;

            ddlCorresPost.DataSource = Comcls.GetPostMaster(ddlCorresCity.SelectedValue.Trim());
            ddlCorresPost.DataTextField = "PostName";
            ddlCorresPost.DataValueField = "Post_ID";
            ddlCorresPost.DataBind();
            ddlCorresPost.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlCorresPost.SelectedIndex = 0;



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
        protected void ddlCorresTaluka_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCorresVillage.DataSource = Comcls.GetVillage(ddlCorresCity.SelectedValue.Trim(), ddlCorresTaluka.SelectedValue.Trim());
            ddlCorresVillage.DataTextField = "VillageName";
            ddlCorresVillage.DataValueField = "VillageID";
            ddlCorresVillage.DataBind();
            ddlCorresVillage.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlCorresVillage.SelectedIndex = 0;


            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
        }
        protected void ddlVILLAGE_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCLUSTARCODE.Text = "";
            txtCLUSTARCODE.Text = cla.GetExecuteScalar("SELECT   Tbl_M_ClustersMaster.Clusters FROM   dbo.Tbl_M_VillageMaster INNER JOIN  dbo.Tbl_M_ClustersMaster ON dbo.Tbl_M_VillageMaster.ClustersMasterID = dbo.Tbl_M_ClustersMaster.ClustersMasterID WHERE  (dbo.Tbl_M_VillageMaster.VillageID =" + ddlVILLAGE.SelectedValue.Trim() + ")");


            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
        }

        protected void ddlCorresVillage_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCorresCode.Text = "";
            txtCorresCode.Text = cla.GetExecuteScalar("SELECT   Tbl_M_ClustersMaster.Clusters FROM   dbo.Tbl_M_VillageMaster INNER JOIN  dbo.Tbl_M_ClustersMaster ON dbo.Tbl_M_VillageMaster.ClustersMasterID = dbo.Tbl_M_ClustersMaster.ClustersMasterID WHERE  (dbo.Tbl_M_VillageMaster.VillageID =" + ddlCorresVillage.SelectedValue.Trim() + ")");


            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
        }

     

        protected void ddlPOST_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPostPin.Text = "";
            txtPostPin.Text = cla.GetExecuteScalar("SELECT  PinCode FROM Tbl_M_CityWisePost where Post_ID=" + ddlPOST.SelectedValue.Trim() + " and IsDeleted is null");


            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
        }
        protected void ddlCorresPost_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCorresPinCode.Text = "";
            txtCorresPinCode.Text = cla.GetExecuteScalar("SELECT  PinCode FROM Tbl_M_CityWisePost where Post_ID=" + ddlCorresPost.SelectedValue.Trim() + " and IsDeleted is null");


            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
        }
        protected void ddlBENEFICIARY_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label8.Text = "NAME";
            Literal1.Text = "Registration ";
            if (ddlBENEFICIARY.SelectedIndex > 0)
            {
                Label8.Text = "NAME OF " + ddlBENEFICIARY.SelectedItem.Text.Trim();
                Literal1.Text = "Registration " + ddlBENEFICIARY.SelectedItem.Text.Trim();
            }

        }
        #endregion



        #region "Save Registration"

        protected void btnSaveAAudhar_Click(object sender, EventArgs e)
        {
            LiteralMsg.Text = "";
            //------------validations ---------------------// 
            if (Comcls.RecordExistanceChk("Tbl_M_RegistrationDetails", "FPORegistrationNo", txtRegistationNo.Text.Trim(), "RegistrationID", Convert.ToInt32 (ViewState["RegistrationID"].ToString())) == false)
            {
                clsMessages.Errormsg(LiteralMsg, "Registration Number   " + txtRegistationNo.Text.ToUpper() + "  is already registered");
                return;
            }

            if (txtRegistationNo.Text.Trim().Length == 0)
            {
                clsMessages.Warningmsg(LiteralMsg, "Please Fill txtRegistation Number");
                return;
            }
            else if (txtName.Text.Trim().Length == 0)
            {
                clsMessages.Warningmsg(LiteralMsg, "Please Fill full name");
                return;
            }
            if (ddlBENEFICIARY.SelectedIndex == 0)
            {
                clsMessages.Warningmsg(LiteralMsg, "Please Select BENEFICIARY Type");
                return;
            }

            if (FileUpload2.HasFile == false)
            {
                clsMessages.Warningmsg(LiteralMsg, "Please attach Registration Certificate.");
                return;

            }
            else
            {
                if (FileUpload2.PostedFile.ContentLength > 3145728)
                {
                    clsMessages.Warningmsg(LiteralMsg, "Please attach max 3 MB file for Registration Certificate");
                    return;
                }
            }



            if (FileUpload3.HasFile == false)
            {
                clsMessages.Warningmsg(LiteralMsg, "Please attach  PROOF OF AUTHORISATION.");
                return;

            }
            else
            {
                if (FileUpload3.PostedFile.ContentLength > 3145728)
                {
                    clsMessages.Warningmsg(LiteralMsg, "Please attach max 3 MB file for   PROOF OF AUTHORISATION");
                    return;
                }
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
                        // ADD NEW 



                        String path = Server.MapPath("/AdminTrans/DocMasters/MemberDoc/" + ViewState["RegistrationID"].ToString() + "");



                        str = " UPDATE Tbl_M_RegistrationDetails SET ";
                        str += " RegisterUnderID=" + ddlRegisteredUnder.SelectedValue.Trim() + ", RegisteredThroughID=" + ddlRegisteredThrough.SelectedValue.Trim() + ", PromoterName='" + txtPromoterName.Text.Trim() + "', Promotermobile='" + txtMobileNo.Text.Trim() + "', ";
                        str += " CeoName='" + txtCeoSecName.Text.Trim() + "',CeoMobile='" + txtCeoSecMobile.Text.Trim() + "',CeoLandline='" + txtCeoSecLandline.Text.Trim() + "',CeoEmail='" + txtCeoEmailId.Text.Trim() + "',CeoDesignation='" + txtDesignation.Text.Trim() + "',CeoAuthorisedPerson='" + txtAuthorisedPerson.Text.Trim() + "',PromoterGender='" + ddlGenderPROMOTER.SelectedValue.Trim() + "',CeoGender='" + ddlGenderCEO.SelectedValue.Trim() + "',CeoAuthorisedPersonGen='" + ddlAuthPerGender.SelectedValue.Trim() + "',CeoAuthorisedPersonMob ='" + txtAuthPerMobile.Text.Trim() + "', CeoAuthorisedPersonEmail='" + txtAuthPerEmail.Text.Trim() + "' ";
                        str += " WHERE(RegistrationID = " + Session["RegistrationID"].ToString() + ")";
                        cla.ExecuteCommand(str, command);

                      
                        String RegistrationCertifecate = "";
                        if (FileUpload2.HasFile)
                        {
                            if (FileUpload2.PostedFile.ContentLength < 3145728)
                            {
                                String Uppath = path + "/RegistrationCertifecate" + System.IO.Path.GetExtension(FileUpload2.PostedFile.FileName.ToString());
                                FileUpload2.PostedFile.SaveAs(Uppath.Trim()); //File is saved in the Physical folder 
                                RegistrationCertifecate = "/AdminTrans/DocMasters/MemberDoc/" + ViewState["RegistrationID"].ToString().Trim() + "/RegistrationCertifecate" + System.IO.Path.GetExtension(FileUpload2.PostedFile.FileName.ToString());
                                lblFileUpload2.Text = "<a href='http://pocra.dbtindia.in" + RegistrationCertifecate + "' target=_blank >  VIEW UPLOADED CERITIFICATE  </a>";
                            }

                            cla.ExecuteCommand("UPDATE   Tbl_M_RegistrationDetails SET   RegistrationCertifecate ='" + RegistrationCertifecate.Trim() + "' WHERE  (RegistrationID = " + ViewState["RegistrationID"].ToString() + ")", command);
                        }
                        String CeoProofOfAuthorisation = "";
                        if (FileUpload3.HasFile)
                        {
                            if (FileUpload3.PostedFile.ContentLength < 3145728)
                            {
                                String Uppath = path + "/CeoProofOfAuthorisation" + System.IO.Path.GetExtension(FileUpload3.PostedFile.FileName.ToString());
                                FileUpload3.PostedFile.SaveAs(Uppath.Trim()); //File is saved in the Physical folder 
                                CeoProofOfAuthorisation = "/AdminTrans/DocMasters/MemberDoc/" + ViewState["RegistrationID"].ToString().Trim() + "/CeoProofOfAuthorisation" + System.IO.Path.GetExtension(FileUpload3.PostedFile.FileName.ToString());
                                lblFileUpload3.Text = "<a href='http://pocra.dbtindia.in" + CeoProofOfAuthorisation + "' target=_blank >  VIEW UPLOADED CERITIFICATE  </a>";
                            }

                            cla.ExecuteCommand("UPDATE   Tbl_M_RegistrationDetails SET    CeoProofOfAuthorisation ='" + CeoProofOfAuthorisation.Trim() + "' WHERE  (RegistrationID = " + ViewState["RegistrationID"].ToString() + ")", command);
                        }


                        txtRegistationNo.Enabled = false;
                        //txtName.ReadOnly = true;
                       
                        // add new 

                    }
                   



                    transaction.Commit();
                    //clsMessages.Sucessmsg(LiteralMsg, "S");
                    LiteralMsg.Text = "";
                    multiViewEmployee.ActiveViewIndex = 1;
                    Menu1.Items[1].Enabled = true;
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
                    connection.Close();
                    command.Dispose();
                }

            }




        }

        #endregion

        #region "Save REGISTERED ADDRESS"
        protected void btnBasic_Click(object sender, EventArgs e)
        {
            LiteralMsg.Text = "";
            //------------validations ---------------------// 



            //------------End validations ---------------------// 


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
                        str = " UPDATE  Tbl_M_RegistrationDetails SET  MobileNumber ='" + txtMobile1.Text.Trim() + "', ";
                        str += " MobileNumber2 ='" + txtMobile2.Text.Trim() + "', LandLineNumber ='" + txtLandLine.Text.Trim() + "', EmailID ='" + txtEmailID.Text.Trim() + "', PanNumber ='" + txtPAN.Text.Trim() + "',  ";
                        str += " Address1HouseNo ='" + txtOfficeNo.Text.Trim() + "', Address1StreetName ='" + txtStreetNo.Text.Trim() + "', Address1City_ID =" + ddlDISTRICT.SelectedValue.Trim() + ", Address1TalukaID =" + ddlTALUKA.SelectedValue.Trim() + ", Address1Post_ID =" + ddlPOST.SelectedValue.Trim() + ", ";
                        str += " Address1VillageID =" + ddlVILLAGE.SelectedValue.Trim() + ", Address1PinCode ='" + txtPostPin.Text.Trim() + "' ";
                        str += " WHERE(RegistrationID = " + ViewState["RegistrationID"].ToString().Trim() + ")"; 
                      
                        cla.ExecuteCommand(str, command);

                        txtName.ReadOnly = true;
                    }


                    transaction.Commit();
                    LiteralMsg.Text = "";
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
                    clsMessages.Errormsg(LiteralMsg, "Please provide correct input." + ex.Message.Trim());
                }
                finally
                {
                    connection.Close();
                    command.Dispose();
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                }

            }

        }
        #endregion

        #region " Save CORRESPONDENCE ADDRESS"

        protected void btnLandNext_Click(object sender, EventArgs e)
        {

            LiteralMsg.Text = "";
            //------------validations ---------------------// 
            String IsBothAddressSame = "0";
            if (chkSameAddress.Checked)
            {
                IsBothAddressSame = "1";
            }


            //------------End validations ---------------------// 


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
                        str = " UPDATE  Tbl_M_RegistrationDetails SET  Address2Mob1 ='" + txtCorresMobile.Text.Trim() + "', ";
                        str += " Address2Mob2 ='" + txtCorresMobile2.Text.Trim() + "', Address2LandLine ='" + txtCorresLandLine.Text.Trim() + "',   ";
                        str += " Address2HouseNo ='" + txtOfficeCorres.Text.Trim() + "', Address2StreetName ='" + txtStreetCorres.Text.Trim() + "', Address2City_ID =" + ddlCorresCity.SelectedValue.Trim() + ", Address2TalukaID =" + ddlCorresTaluka.SelectedValue.Trim() + ", Address2Post_ID =" + ddlCorresPost.SelectedValue.Trim() + ", ";
                        str += " Address2VillageID =" + ddlCorresVillage.SelectedValue.Trim() + ", Address2PinCode ='" + txtCorresPinCode.Text.Trim() + "', IsBothAddressSame ='" + IsBothAddressSame.Trim() + "' ";
                        str += " WHERE(RegistrationID = " + ViewState["RegistrationID"].ToString().Trim() + ")";
                        cla.ExecuteCommand(str, command);

                        txtName.ReadOnly = true;
                    }


                    transaction.Commit();
                    LiteralMsg.Text = "";
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
                    clsMessages.Errormsg(LiteralMsg, "Please provide correct input." + ex.Message.Trim());
                }
                finally
                {
                    connection.Close();
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
            //if (CheckBox4.Checked == false)
            //{
            //    IsAcept = false;
            //}
            //if (CheckBox5.Checked == false)
            //{
            //    IsAcept = false;
            //}
            //if (CheckBox6.Checked == false)
            //{
            //    IsAcept = false;
            //}
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
                clsMessages.Warningmsg(LiteralMsg, "Please accept all Declarations to register under Nanaji Deshmukh Krishi Sanjivani Prakalp");
                return;
            }


            //------------End validations ---------------------// 


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
                        str = " UPDATE  Tbl_M_RegistrationDetails SET   IAgree ='1' , IsDeleted=NULL  ";
                        str += " WHERE(RegistrationID = " + ViewState["RegistrationID"].ToString().Trim() + ")";
                        cla.ExecuteCommand(str, command);


                     

                    }


                    transaction.Commit();
                    //Session["RegistrationIDPass"] = ViewState["RegistrationID"].ToString().Trim();
                    clsMessages.Sucessmsg2(LiteralMsg, "You have been Sucessfully Updated your Profile details.");
                    ////Response.Redirect("RegisterSucess.aspx?" + EncryptDecryptQueryString.encrypt(string.Format("ID={0}", ViewState["RegistrationID"].ToString().Trim())), true);
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
                    connection.Close();
                    command.Dispose();
                }

            }

        }




        #endregion

        protected void chkSameAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (ddlCorresPost.Items.Count > 0)
                ddlCorresPost.SelectedIndex = 0;
            if (ddlCorresVillage.Items.Count > 0)
                ddlCorresVillage.SelectedIndex = 0;
            if (ddlCorresTaluka.Items.Count > 0)
                ddlCorresTaluka.SelectedIndex = 0;
            if (ddlCorresCity.Items.Count > 0)
                ddlCorresCity.SelectedIndex = 0;

            txtOfficeCorres.Text = "";
            txtStreetCorres.Text = "";
            txtCorresPinCode.Text = "";
            txtCorresCode.Text = "";
            txtCorresMobile.Text = "";
            txtCorresMobile2.Text = "";
            txtCorresLandLine.Text = "";

            if (chkSameAddress.Checked)
            {
                if (ddlDISTRICT.SelectedIndex > 0)
                {
                    ddlCorresCity.SelectedValue = ddlDISTRICT.SelectedValue.Trim();
                    ddlCorresCity_SelectedIndexChanged(sender, e); 
                }
                if (ddlTALUKA.SelectedIndex > 0)
                {
                    ddlCorresTaluka.SelectedValue = ddlTALUKA.SelectedValue.Trim();
                    ddlCorresTaluka_SelectedIndexChanged(sender, e);
                }
                if (ddlVILLAGE.SelectedIndex > 0)
                {
                    ddlCorresVillage.SelectedValue = ddlVILLAGE.SelectedValue.Trim();
                    ddlCorresVillage_SelectedIndexChanged(sender, e);
                }
                if (ddlPOST.SelectedIndex > 0)
                {
                    ddlCorresPost.SelectedValue = ddlPOST.SelectedValue.Trim();
                    ddlCorresPost_SelectedIndexChanged(sender, e);
                }

                txtOfficeCorres.Text = txtOfficeNo.Text.Trim();
                txtStreetCorres.Text = txtStreetNo.Text.Trim();
                txtCorresPinCode.Text = txtPostPin.Text.Trim();
                txtCorresCode.Text = txtCLUSTARCODE.Text.Trim();
                txtCorresMobile.Text = txtMobile1.Text.Trim();
                txtCorresMobile2.Text = txtMobile2.Text.Trim();
                txtCorresLandLine.Text = txtLandLine.Text.Trim();
            }



        }

      
    }
}