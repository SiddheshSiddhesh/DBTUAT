using CommanClsLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBTPoCRA.Registration
{
    public partial class RegisterSucess : System.Web.UI.Page
    {
        MyClass cla = new MyClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString.Count > 0)
                    {
                        string strReq = "";
                        strReq = Request.RawUrl;
                        strReq = strReq.Substring(strReq.IndexOf('?') + 1);
                        ViewState["BeneficiaryTypesID"] = "";
                        if (!strReq.Equals(""))
                        {
                            strReq = EncryptDecryptQueryString.Decrypt(strReq);
                            string[] arrMsgs = strReq.Split('&');
                            string[] arrIndMsg;
                            arrIndMsg = arrMsgs[0].Split('='); //Get the Name
                            String RegistrationID = arrIndMsg[1].ToString().Trim();
                            //Session["RegistrationIDPass"]
                            Log.Visible = false;
                            DataTable dt = cla.GetDataTable("SELECT  RegisterName, BeneficiaryTypesID,GramPanchayatCode ,FPORegistrationNo,isnull(IsRegDraft,0) as IsRegDraft FROM Tbl_M_RegistrationDetails where RegistrationID=" + Session["RegistrationIDPass"].ToString() + " and IsDeleted is null ");
                            if (dt.Rows.Count > 0)
                            {
                                ViewState["BeneficiaryTypesID"] = dt.Rows[0]["BeneficiaryTypesID"].ToString().Trim();
                                if (dt.Rows[0]["BeneficiaryTypesID"].ToString().Trim() == "1")
                                {
                                    //Literal1.Text = "You have been registered under <b> Nanaji Deshmukh Krishi Sanjivani Prakalp.</b>  Your canlogin using your AADHAAR Number";
                                    Literal1.Text = "Registration completed successfully and details of the same are sent on registered mobile number. You can login using your AADHAAR Number";
                                    btnCreatePass.Text = "Go to Login";
                                    // cla.ExecuteCommand("UPDATE Tbl_M_RegistrationDetails SET    FarmerType =(Select case when ISNULL(TotalLand,0) =0 then 'LandLess' when TotalLand <=1 then 'Marginal' when TotalLand >1 and TotalLand <=2 then 'Small' else 'Other' end  from dbo.GetDataToSetFarmerType(RegistrationID)) WHERE (IsDeleted IS NULL) and RegistrationID=" + Session["RegistrationIDPass"].ToString() + " ");
                                }
                                else if (dt.Rows[0]["BeneficiaryTypesID"].ToString().Trim() == "2" || dt.Rows[0]["BeneficiaryTypesID"].ToString().Trim() == "99")
                                {
                                    Literal1.Text = "You have been registered under<b> Nanaji Deshmukh Krishi Sanjivani Prakalp.</ b >";
                                    strl.Visible = false;
                                    Log.Visible = false;
                                    //Literal2.Text = dt.Rows[0]["GramPanchayatCode"].ToString().Trim();
                                    //Literal3.Text = dt.Rows[0]["RegisterName"].ToString().Trim();
                                    btnLogin.Visible = false;
                                    btnHome.Visible = false;
                                }
                                else
                                {
                                    Literal1.Text = "You have been registered under<b> Nanaji Deshmukh Krishi Sanjivani Prakalp.</ b > Your login ID will be your Registration Number ";
                                    Log.Visible = true;
                                    Literal2.Text = dt.Rows[0]["FPORegistrationNo"].ToString().Trim();
                                    Literal3.Text = dt.Rows[0]["RegisterName"].ToString().Trim();
                                    // btnCreatePass.Text = "Create Password";
                                }

                                if (dt.Rows[0]["BeneficiaryTypesID"].ToString().Trim() == "99" && dt.Rows[0]["IsRegDraft"].ToString().Trim() == "1")
                                {
                                    Literal1.Text = "Your registration have Save now .Please completed it Using Updated registration Section.</ b >";
                                    strl.Visible = false;
                                    Log.Visible = false;
                                    //Literal2.Text = dt.Rows[0]["GramPanchayatCode"].ToString().Trim();
                                    //Literal3.Text = dt.Rows[0]["RegisterName"].ToString().Trim();
                                    btnLogin.Visible = false;
                                    btnHome.Visible = false;
                                }


                                MyCommanClass.UpdateRegistrationNo();
                                cla.ExecuteByProcedure("SP_CommanProCall", new List<string>());

                            }
                            else
                            {
                                clsMessages.Errormsg(LiteralMsgBox, "Invalid Registration Details , kindly contect to office for more details .");
                            }
                        }
                    }
                }
            }
            catch { }
        }

        protected void btnCreatePass_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim().Length == 0)
            {
                clsMessages.Warningmsg(LiteralMsgBox, "Please Fill Password");
                return;
            }
            else if (txtRepassword.Text.Trim().Length == 0)
            {
                clsMessages.Warningmsg(LiteralMsgBox, "Please Fill confirm Password");
                return;
            }

            if (cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails SET UPass='" + txtPassword.Text.Trim() + "' where RegistrationID=" + Session["RegistrationIDPass"].ToString() + "  ").Trim().Length == 0)
            {
                Log.Visible = false;
                clsMessages.Sucessmsg(LiteralMsgBox, "Your Password is sucessfully created.");

            }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //~/Default.aspx

            if (ViewState["BeneficiaryTypesID"].ToString().Trim() == "1")
            {
                Response.Redirect("~/FarmerLogin.aspx?T=I&D=1", false);

            }
            else if (ViewState["BeneficiaryTypesID"].ToString().Trim() == "2")
            {
                //?T = C & D = 2
                Response.Redirect("~/FarmerLogin.aspx?T=C&D=2", false);
            }
            else
            {
                Response.Redirect("~/FarmerLogin.aspx?T=F&D=3", false);
            }
        }
    }
}