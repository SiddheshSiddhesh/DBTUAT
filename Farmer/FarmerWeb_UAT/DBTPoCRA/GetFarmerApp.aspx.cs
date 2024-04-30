using CommanClsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBTPoCRA
{
    public partial class GetFarmerApp : System.Web.UI.Page
    {
        MyClass cla = new MyClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count > 0)
            {
                String ID = "";
                ID = cla.GetExecuteScalar("Select top 1 RegistrationID from Tbl_M_RegistrationDetails where MobileNumber='" + Request.QueryString["Mob"].ToString().Trim() + "' and IsDeleted is null and IsMobileVerifyed is not null  order by RegistrationID desc");

                if (ID.Trim().Length > 0)
                {
                    Response.Redirect("https://dbt.mahapocra.gov.in/Office/admintrans/Popup/rptApplicationHistory.aspx?RID=" + ID + "&AID=0&BID=1&", true);
                }
                else
                {
                    //Literal1.Text = "Sorry , We have not any registration with " + Request.QueryString["Mob"].ToString().Trim() + ". Please register on dbt.mahapocra.gov.in and try again.";
                    Literal1.Text = "क्षमस्व, आमच्याकडे " + Request.QueryString["Mob"].ToString().Trim() + " वर कोणतीही नोंदणी नाही. कृपया dbt.mahapocra.gov.in वर नोंदणी करा आणि पुन्हा प्रयत्न करा.";
                }
            }
            else
            {
                //Literal1.Text = "Sorry , We have not any registration. Please register on dbt.mahapocra.gov.in and try again.";
                Literal1.Text = "क्षमस्व, आमच्याकडे कोणतीही नोंदणी नाही. कृपया dbt.mahapocra.gov.in वर नोंदणी करा आणि पुन्हा प्रयत्न करा.";
            }

        }
    }
}