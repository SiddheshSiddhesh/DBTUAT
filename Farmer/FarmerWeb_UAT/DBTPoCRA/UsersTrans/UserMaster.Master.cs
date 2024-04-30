using CommanClsLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBTPoCRA.AdminTrans
{
    public partial class UserMaster : System.Web.UI.MasterPage
    {
        MyClass cla = new MyClass();


        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if (Session["UserId"] != null)
                //{
                //    lblUser.Text = "Hi , " + Session["UsersName"].ToString();
                //    //Label1.Text = Session["LoginAs"].ToString().Trim();
                //}
                //else
                //{
                //    Response.Redirect("~/UserLogin.aspx", true);
                //}

                //if (Session["UserId"] == null)
                //{
                //    Response.Redirect("~/UserLogin.aspx", true);
                //}

                if (Session["UserId"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
                {
                    if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                    {
                        // redirect to the login page in real application
                        Response.Redirect("~/LogOut.aspx", true);
                    }
                    else
                    {
                        //" you are logged in.";
                        lblUser.Text = "Hi , " + Session["UsersName"].ToString();

                        String IsMobileVerifyed = cla.GetExecuteScalar("SELECT  IsMobileVerifyed FROM Tbl_M_RegistrationDetails where RegistrationID=" + Session["RegistrationID"].ToString() + "");
                        if (IsMobileVerifyed.Trim().Length == 0)
                        {
                            //strURL = "";
                            Response.Redirect("~/UsersTrans/MobileVerification.aspx", true);
                        }

                        try
                        {
                            Convert.ToInt32(Session["RegistrationID"].ToString());//RegistrationID
                        }
                        catch
                        {
                            Response.Redirect("~/LogOut.aspx", true);
                        }
                    }
                }
                else
                {
                    // redirect to the login page in real application
                    Response.Redirect("~/LogOut.aspx", true);
                }


                if (!IsPostBack)
                {
                    // CreateMenu();
                    try
                    {
                        cla.ExecuteCommand("UPDATE Tbl_M_RegistrationDetails SET PriorityLevelID=((Select  case when R.CategoryMasterID=3 then (case when R.Gender='Male' and R.PhysicallyHandicap='No' then '7' when R.Gender='Male' and R.PhysicallyHandicap='YES' then '6' when R.Gender<>'Male' and R.PhysicallyHandicap='NO' then '5' when R.Gender<>'Male' and R.PhysicallyHandicap='YES' then '4' end   )  when R.CategoryMasterID=2 then (case when R.Gender='Male' and R.PhysicallyHandicap='No' then '11' when R.Gender='Male' and R.PhysicallyHandicap='YES' then '10' when R.Gender<>'Male' and R.PhysicallyHandicap='NO' then '9' when R.Gender<>'Male' and R.PhysicallyHandicap='YES' then '8' end   )   else (case when R.Gender='Male' and R.PhysicallyHandicap='No' then '15' when R.Gender='Male' and R.PhysicallyHandicap='YES' then '14' when R.Gender<>'Male' and R.PhysicallyHandicap='NO' then '13' when R.Gender<>'Male' and R.PhysicallyHandicap='YES' then '12' end   )   end  from Tbl_M_RegistrationDetails As R where R.BeneficiaryTypesID=1 and R.isdeleted is null and R.RegistrationID=Tbl_M_RegistrationDetails.RegistrationID) ) where BeneficiaryTypesID=1 and isDeleted is null and RegistrationID=" + Session["RegistrationID"].ToString() + "");
                    }
                    catch { }
                    //<a href="#"><i class="icon icon-sailing-boat-water purple-text s-18"></i><span>View Edit Profile</span> </a>
                    String s = "View & Edit Profile";
                    enul.Visible = true;
                    Marathisul.Visible = false;
                    btnMarathi.Text = "मराठी";
                    if (Session["Lang"].ToString().Trim() == "mr-IN")
                    {
                        s = "प्रोफाइल पहा आणि संपादित करा";
                        enul.Visible = false;
                        Marathisul.Visible = true;
                        btnMarathi.Text = "English";
                    }

                    cpm.Visible = false;
                    if (Session["BeneficiaryTypesID"].ToString() == "1")
                    {
                        // farmer

                        Literal1.Text = "<a  href='IndividualRegistrationEdit.aspx'><i class='icon icon-sailing-boat-water purple-text s-18'></i><span>" + s + "</span> </a>";
                    }
                    else if (Session["BeneficiaryTypesID"].ToString() == "2")
                    {
                        // comini..
                        cpm.Visible = true;
                        Literal1.Text = "<a href='CommunityRegistrationProfile.aspx'><i class='icon icon-sailing-boat-water purple-text s-18'></i><span>" + s + "</span> </a>";
                    }
                    else
                    {
                        // others..
                        cpm.Visible = true;
                        Literal1.Text = "<a href='FpoFpcProfileEdit.aspx'><i class='icon icon-sailing-boat-water purple-text s-18'></i><span>" + s + "</span> </a>";
                    }
                    Literal3.Text = Literal1.Text;
                }

                String Alerts = "";
                if (cla.GetExecuteScalar("SELECT  ApprovalStatus FROM Tbl_M_RegistrationDetails WHERE (RegistrationID =" + Session["RegistrationID"].ToString() + ") and ApprovalStatus='Hold (On Beneficiary )'").Length > 0)
                {
                    Alerts = " <div class='alert alert-danger alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'></span></button> <strong>Pending!</strong> Profile Update is required to resolved the hold resons.</div>";
                }
                if (cla.GetExecuteScalar(" SELECT  top 1 ApplicationID FROM Tbl_T_ApplicationDetails WHERE (RegistrationID =" + Session["RegistrationID"].ToString() + ") and  ApprovalStageID=5 and Tbl_T_ApplicationDetails.ApplicationID is not null and Tbl_T_ApplicationDetails.isDeleted is null and Tbl_T_ApplicationDetails.ApplicationID not in (Select w.ApplicationID from Tbl_T_Application_WorkReport w where w.IsDeleted is null and w.ApplicationStatusID<>2 and w.ApplicationID=Tbl_T_ApplicationDetails.ApplicationID) ").Length > 0)
                {
                    Alerts += " <div class='alert alert-danger alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'></span></button> <strong>Pending!</strong> Documents to be prepared & shared with PoCRA officials.</div>";
                }

                if (cla.GetExecuteScalar("SELECT  top 1 ApplicationID FROM Tbl_T_ApplicationDetails WHERE (RegistrationID =" + Session["RegistrationID"].ToString() + ") and  ApplicationStatusID=4 and ApplicationID is not null and isDeleted is null ").Length > 0)
                {
                    Alerts += " <div class='alert alert-danger alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'></span></button> <strong>Pending!</strong> Please check your application log by clicking on plus button and resolved the reasons of Back To Beneficiary.</div>";
                }

                String s1 = cla.GetExecuteScalar("SELECT B.A_Status FROM Tbl_M_RegistrationDetails R INNER JOIN Tbl_M_RegistrationDetails_Bolt B on B.BoltID=R.BoltID where R.RegistrationID=" + Session["RegistrationID"].ToString() + " ");
                if(s1.Length>0)
                {
                    if(s1.Trim()!="A")
                    {
                        Alerts += " <div class='alert alert-danger alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'></span></button> <strong>Alert!</strong>  <b> Aadhaar is not linked with your Bank Account  </div>";
                    }
                }
                Literal2.Text = Alerts;
            }
            catch { }
        }

        protected void btnMarathi_Click(object sender, EventArgs e)
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("mr-IN");
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
            string pageName = this.ContentPlaceHolder1.Page.GetType().FullName;
            if (pageName.Trim().Contains("_"))
            {
                string[] s = pageName.Trim().Split('_');
                pageName = s[1].Trim() + "." + s[2].Trim();
            }
            String qstring = this.ContentPlaceHolder1.Page.ClientQueryString.ToString();
            if (qstring.Length > 0)
                pageName = pageName + "?" + qstring;
            Response.Redirect("~/UsersTrans/" + pageName);
            // LoadString(Thread.CurrentThread.CurrentCulture);
        }

    }
}