using CommanClsLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBTPoCRA
{
    public partial class LogOut : System.Web.UI.Page
    {
        #region "Global Declaration"
        MyClass cla = new MyClass();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {


            //--------------------
            DataTable dt = cla.GetDataTable("Select v.VillageID,v.VillageCode,r.RegistrationID from Tbl_M_RegistrationDetails r inner join Tbl_M_VillageMaster v on v.VillageID=r.Work_VillageID where r.IsDeleted is null and v.IsDeleted is not null");
            foreach (DataRow dr in dt.Rows)
            {
                String RegistrationID = dr["RegistrationID"].ToString().Trim();
                String VillageID = dr["VillageID"].ToString().Trim();
                String VillageCode = dr["VillageCode"].ToString().Trim();

                String UpVillageID = cla.GetExecuteScalar("Select VillageID from Tbl_M_VillageMaster where VillageCode='" + VillageCode.Trim() + "' and IsDeleted is null");
                if (UpVillageID.Trim().Length > 0)
                {
                    cla.ExecuteCommand("UPDATE Tbl_M_RegistrationDetails SET Work_VillageID=" + UpVillageID + " where RegistrationID=" + RegistrationID + " and Work_VillageID=" + VillageID + " ");
                }

            }
            // for maligaoi CA to add AA role auto
            dt = cla.GetDataTable("SELECT UserId ,UserRoleID FROM dbo.View_MaligaoCA_AA_Creation");
            foreach (DataRow dr in dt.Rows)
            {
                String UserId = dr["UserId"].ToString().Trim();

                String UpVillageID = cla.GetExecuteScalar("SELECT UserWiseRoleId FROM Tbl_M_LoginDetails_Role where UserId=" + UserId + "  and IsDeleted is null and UserRoleID=7");
                if (UpVillageID.Trim().Length == 0)
                {

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
                            int UserWiseRoleId = cla.TableID("Tbl_M_LoginDetails_Role", "UserWiseRoleId",command);
                            cla.ExecuteCommand("INSERT INTO Tbl_M_LoginDetails_Role (UserWiseRoleId, UserId,UserRoleID) values ("+ UserWiseRoleId + "," + UserId + ",7) ",command);

                            transaction.Commit();

                        }
                        catch (Exception ex)
                        {


                            try
                            {
                                transaction.Rollback();

                            }
                            catch
                            {

                            }
                            //cla.SendMail(ex.ToString());
                        }
                        finally
                        {

                            connection.Close();
                            command.Dispose();
                        }

                    }







                }

            }



            //-----------------------

            if (Session["UserId"] != null)
            {
                cla.GetExecuteScalar("update Tbl_M_LoginDetails set IsLogged=NULL where UserId=" + Session["UserId"].ToString() + "");
            }
            if (Request.QueryString.Count > 0)
            {
                String UName = Request.QueryString["M"].ToString().Trim();
                if (!String.IsNullOrEmpty(UName))
                {
                    if (UName.Contains("="))
                        UName = UName.Replace('=', ' ');
                    else
                 if (UName.Contains("-"))
                        UName = UName.Replace('-', ' ');
                }

                String UserId = cla.GetExecuteScalar("Select UserId from  Tbl_M_LoginDetails where UserName='" + UName.Trim() + "' and IsDeleted is null ");
                if (UserId.Length > 0)
                {
                    cla.GetExecuteScalar("update Tbl_M_LoginDetails set IsLogged=NULL where UserId=" + UserId.ToString() + "");
                }
            }


            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            ExpireAllCookies();
            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }

            if (Request.Cookies["AuthToken"] != null)
            {
                Response.Cookies["AuthToken"].Value = string.Empty;
                Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
            }


            Response.Redirect("~/Default.aspx", false);
        }

        private void ExpireAllCookies()
        {
            if (HttpContext.Current != null)
            {
                int cookieCount = HttpContext.Current.Request.Cookies.Count;
                for (var i = 0; i < cookieCount; i++)
                {
                    var cookie = HttpContext.Current.Request.Cookies[i];
                    if (cookie != null)
                    {
                        var expiredCookie = new HttpCookie(cookie.Name)
                        {
                            Expires = DateTime.Now.AddDays(-1),
                            Domain = cookie.Domain
                        };
                        HttpContext.Current.Response.Cookies.Add(expiredCookie); // overwrite it
                    }
                }

                // clear cookies server side
                HttpContext.Current.Request.Cookies.Clear();
            }
        }
    }
}