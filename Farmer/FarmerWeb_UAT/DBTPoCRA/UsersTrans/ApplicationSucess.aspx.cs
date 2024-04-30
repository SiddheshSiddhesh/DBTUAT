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
using System.Globalization;
using System.Resources;
using System.Threading;

namespace DBTPoCRA.UsersTrans
{
    public partial class ApplicationSucess : System.Web.UI.Page
    {
        MyClass cla = new MyClass();
        ResourceManager rm;
        CultureInfo ci;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["T"].ToString().Trim().Length > 0)
                    {
                        Literal1.Text = cla.GetExecuteScalar("Select ActivityName from Tbl_M_ActivityMaster where ActivityID=" + Request.QueryString["T"].ToString().Trim() + "");
                        Literal5.Text = "<a  class='btn btn-danger btn-sm mt-3' target=_blank href='../admintrans/Popup/PreLetters/ApplicationPrint.aspx?ID=" + Request.QueryString["A"].ToString() + "'>Print</a>";


                    }

                    if (Session["Lang"].ToString().Trim() == "mr-IN")
                    {
                        Literal1.Text = cla.GetExecuteScalar("Select ActivityNameMr from Tbl_M_ActivityMaster where ActivityID=" + Request.QueryString["T"].ToString().Trim() + "");
                        rm = new ResourceManager("Resources.ApplyForNewScheme", System.Reflection.Assembly.Load("App_GlobalResources")); ci = Thread.CurrentThread.CurrentCulture;
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("mr-IN");
                        LoadString(Thread.CurrentThread.CurrentCulture);

                    }

                }
                else
                {

                    if (Session["Lang"].ToString().Trim() == "mr-IN")
                    {
                        rm = new ResourceManager("Resources.ApplyForNewScheme", System.Reflection.Assembly.Load("App_GlobalResources")); ci = Thread.CurrentThread.CurrentCulture;
                        Thread.CurrentThread.CurrentCulture = new CultureInfo("mr-IN");
                        LoadString(Thread.CurrentThread.CurrentCulture);

                    }
                }
            }
            catch { }
        }

        private void LoadString(CultureInfo ci)
        {
            Literal2.Text = rm.GetString("You_have_successfully_applied_for", ci);
            Literal3.Text = rm.GetString("scheme_under_Nanaji_Deshmukh_Krishi_Sanjivani_Prakalp_Your_Application_is_pending_for_Approvals", ci);
            Literal4.Text = rm.GetString("Your_application_has_been_successfully_submitted", ci);
            Literal6.Text = "आपण दुसर्या योजनेसाठी अर्ज करू इच्छित आहात, ";
        }
    }
}