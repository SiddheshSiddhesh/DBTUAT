using CommanClsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBTPoCRA.UsersTrans
{
    public partial class MobileVerification : System.Web.UI.Page
    {
        static int otpCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if(!IsPostBack)
            {
                otpCount = 0;
                MyClass cla = new MyClass();
                Literal1.Text=cla.GetExecuteScalar("SELECT  MobileNumber FROM Tbl_M_RegistrationDetails where RegistrationID=" + Session["RegistrationID"].ToString() + "");
            }

        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            DivNew.Visible = CheckBox1.Checked;
        }

        protected void btnSendOTP_Click(object sender, EventArgs e)
        {

            String MobileNo = "";
            if (CheckBox1.Checked)
            {
                MobileNo = txtMobile1.Text.Trim();
            }
            else
            {
                MobileNo = Literal1.Text.Trim();
            }
            if (MobileNo.Trim().Length == 0)
            {
                return;
            }

            Random a = new Random(Guid.NewGuid().GetHashCode());
            int MyNumber = a.Next(000000, 999999);
            String strOTP = MyNumber.ToString();
            //String s = "This is your one-time password " + strOTP.Trim() + " for  Pocra DBT Login.";
            //SMS.SendSMS(s, MobileNo.Trim());

            String s = "OTP for login to the DBT portal is " + strOTP + ". It will be valid for 5 min. Team PoCRA";
            //String s = "OTP for login to the DBT portal is " + strOTP + ". It will be valid for 5 min. Team PoCRA";// "This is your one-time password " + strOTP + " Thank you. You";
            SMS.SendSMS(s, MobileNo, "1407161960968273289");

            ViewState["OTP"] = EncryptDecryptQueryString.encrypt(strOTP);
            ViewState["Mobile"] = MobileNo;
            String msg = "OTP send to your mobile number " + MobileNo.Trim() + "  and will valid for next 5 min.";
            Util.ShowMessageBox(this.Page, "Success", msg, "success");

            div1.Visible = true;
            div2.Visible = true;

            btnSendOTP.Enabled = false;
            string myScript = "setTimeout(\"reenable('" + btnSendOTP.ClientID + "')\",60000);";
            ClientScript.RegisterStartupScript(Page.GetType(), "reenable", myScript, true);


        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            if(otpCount>3)
            {
                Response.Redirect("~/LogOut.aspx", true);
            }

            otpCount++;
            if (EncryptDecryptQueryString.Decrypt(ViewState["OTP"].ToString().Trim()).ToUpper() != txtOTP.Text.Trim().ToUpper())
            {
                Util.ShowMessageBox(this.Page, "Error", "You have entered invalid OTP. 3 incorrect attempt can make your account inactive for 24 hrs.", "error");
            }
            else
            {
                MyClass cla = new MyClass();
                cla.ExecuteCommand("update Tbl_M_RegistrationDetails set MobileNumber='"+ ViewState["Mobile"].ToString().Trim()+ "' , IsMobileVerifyed='1' where RegistrationID=" + Session["RegistrationID"].ToString() + "");
                Util.ShowMessageBox(this.Page, "Success", "Mobile Number verified Successfully Done.", "success", "UserDashBoard.aspx");
            }
        }
    }
}