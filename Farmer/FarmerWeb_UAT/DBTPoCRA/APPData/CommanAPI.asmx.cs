using CommanClsLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.IO;
using System.Net;

namespace DBTPoCRA.APPData
{
    /// <summary>
    /// Summary description for CommanAPI
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class CommanAPI : System.Web.Services.WebService
    {
        // //API  Deprecated on 15 March 2024

        #region API  Deprecated on 15 March 2024
        [WebMethod]
        public void CheckUserExistance(String SecurityKey, String UserName, String UserPassword)
        {

            String UName = UserName.Trim();
            if (!String.IsNullOrEmpty(UName))
            {
                if (UName.Contains("="))
                    UserName = UName.Replace('=', ' ');
                else
             if (UName.Contains("-"))
                    UserName = UName.Replace('-', ' ');
            }


            DataTable dt = new DataTable();
            MyClass cla = new MyClass();

            List<clsLoginCheck> lst = new List<clsLoginCheck>();

            if (MyCommanClassAPI.CheckApiAuthrization("1", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsLoginCheck d = new clsLoginCheck();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {


                //Boolean ffsLogin = false;
                //String FFSID = cla.GetExecuteScalar("SELECT UserId FROM Tbl_M_LoginDetails where UserName='" + UserName.Trim() + "' and IsDeleted is null ");
                Boolean ffsLogin = false;
                String FFSID = "YES";// cla.GetExecuteScalar("SELECT UserId FROM Tbl_M_LoginDetails where UserName='" + UserName.Trim() + "' and IsDeleted is null ");
                //if (FFSID.Trim().Length == 0)
                //{
                //    FFSID = cla.GetExecuteScalar("SELECT Max(UserId) FROM Tbl_M_LoginDetails where UserName='" + UserName.Trim() + "'  ");
                //}
                if (FFSID.Trim().Length > 0)
                {
                    if (getTokenId(UserName.Trim(), UserPassword.Trim()) == "200")
                    {
                        ffsLogin = true;
                    }

                }




                //cla.OpenReturenDs("SELECT UserId, UserName, UPass, FullName, RegistrationID ,Desig_ID ,LoginAs FROM Tbl_M_LoginDetails WHERE (IsDeleted is null) AND (Username = '" + UserName.Trim() + "') AND UPass='" + UserPassword.Trim() + "'  ");
                String strQuery = "";
                if (ffsLogin == true)
                {
                    strQuery = "SELECT top 1  UserId, UserName, UPass, IsOtpRequired,Mobile, FullName, RegistrationID , LoginAs ,UserRoleID,Desig_ID FROM Tbl_M_LoginDetails WHERE (IsDeleted is null) AND (Username = '" + UserName.Trim() + "')  order by UserId desc  ";
                }
                else
                {
                    strQuery = "SELECT top 1  UserId, UserName, UPass, IsOtpRequired,Mobile, FullName, RegistrationID , LoginAs ,UserRoleID,Desig_ID FROM Tbl_M_LoginDetails WHERE (IsDeleted is null) AND (Username = '" + UserName.Trim() + "') AND UPass='" + UserPassword.Trim() + "'  order by UserId desc ";
                }
                cla.OpenReturenDs(strQuery);

                if (cla.Ds.Tables[0].Rows.Count > 0)
                {
                    //API to be Deprecated on 15 March 2024
                    //For Forcefully Disable Login

                    #region MyRegion

                    //clsLoginCheck d = new clsLoginCheck();
                    //string guid = Guid.NewGuid().ToString();

                    //if (cla.Ds.Tables[0].Rows[0]["UserId"].ToString() != "")
                    //{
                    //    cla.ExecuteCommand("update Tbl_M_LoginDetails set MobileTokenID='" + guid + "' where UserId=" + cla.Ds.Tables[0].Rows[0]["UserId"].ToString() + " ");
                    //}

                    //d.FullName = cla.Ds.Tables[0].Rows[0]["FullName"].ToString();
                    //d.Desig_ID = cla.Ds.Tables[0].Rows[0]["Desig_ID"].ToString();
                    //d.LoginAs = cla.Ds.Tables[0].Rows[0]["LoginAs"].ToString();
                    //d.UserId = cla.Ds.Tables[0].Rows[0]["UserId"].ToString();
                    //d.RegistrationID = cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString();
                    //if (cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString() != "")
                    //{
                    //    d.BeneficiaryTypesID = cla.GetExecuteScalar("SELECT  BeneficiaryTypesID FROM Tbl_M_RegistrationDetails where RegistrationID=" + cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString() + "");
                    //}


                    //if (d.BeneficiaryTypesID == null)
                    //{
                    //    d.BeneficiaryTypesID = "";
                    //}

                    //d.Message = "SUCCESS";
                    //d.AuthTokenID = guid;
                    //d.Error = "";

                    //String ismaligao = cla.GetExecuteScalar("SELECT UserId  FROM dbo.View_MaligaoCA_AA_Creation where  UserId=" + cla.Ds.Tables[0].Rows[0]["UserId"].ToString() + " ");
                    //if (ismaligao.Trim().Length > 0)
                    //{
                    //    d.Desig_ID = "111";
                    //} 
                    //lst.Add(d);
                    #endregion


                    clsLoginCheck d = new clsLoginCheck();
                    d.LoginAs = "";
                    d.UserId = "";
                    d.RegistrationID = "";
                    d.BeneficiaryTypesID = "";
                    d.Message = "This APP Version is disabled by PoCRA Administrator.Please Download LATEST Updated APP From \n https://mahapocra.gov.in/home/apk_file";
                    d.Error = "";
                    lst.Add(d);


                }
                else
                {
                    clsLoginCheck d = new clsLoginCheck();
                    d.LoginAs = "";
                    d.UserId = "";
                    d.RegistrationID = "";
                    d.BeneficiaryTypesID = "";
                    d.Message = "Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator.";
                    d.Error = "";
                    lst.Add(d);

                }
            }
            catch (Exception ex)
            {
                //Label1.Text = ex.ToString();
                clsLoginCheck d = new clsLoginCheck();
                d.LoginAs = "";
                d.UserId = "";
                d.RegistrationID = "";
                d.BeneficiaryTypesID = "";
                d.Message = "Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator.";
                d.Error = ex.ToString();
                lst.Add(d);
                // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MsgBox", "<script>alert('Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator !!!')</script>", false);
            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }
        #endregion

        [WebMethod]
        public void CheckUserExistanceWithOTP(String SecurityKey, String UserName, String UserPassword)
        {

            String UName = UserName.Trim();
            if (!String.IsNullOrEmpty(UName))
            {
                if (UName.Contains("="))
                    UserName = UName.Replace('=', ' ');
                else
             if (UName.Contains("-"))
                    UserName = UName.Replace('-', ' ');
            }


            DataTable dt = new DataTable();
            MyClass cla = new MyClass();

            List<clsLoginCheckWithOTP> lst = new List<clsLoginCheckWithOTP>();

            if (MyCommanClassAPI.CheckApiAuthrization("1", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsLoginCheckWithOTP d = new clsLoginCheckWithOTP();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {


                //Boolean ffsLogin = false;
                //String FFSID = cla.GetExecuteScalar("SELECT UserId FROM Tbl_M_LoginDetails where UserName='" + UserName.Trim() + "' and IsDeleted is null ");
                Boolean ffsLogin = false;
                String FFSID = "YES";// cla.GetExecuteScalar("SELECT UserId FROM Tbl_M_LoginDetails where UserName='" + UserName.Trim() + "' and IsDeleted is null ");
                //if (FFSID.Trim().Length == 0)
                //{
                //    FFSID = cla.GetExecuteScalar("SELECT Max(UserId) FROM Tbl_M_LoginDetails where UserName='" + UserName.Trim() + "'  ");
                //}
                if (FFSID.Trim().Length > 0)
                {
                    if (getTokenId(UserName.Trim(), UserPassword.Trim()) == "200")
                    {
                        ffsLogin = true;
                    }

                }


                //cla.OpenReturenDs("SELECT UserId, UserName, UPass, FullName, RegistrationID ,Desig_ID ,LoginAs FROM Tbl_M_LoginDetails WHERE (IsDeleted is null) AND (Username = '" + UserName.Trim() + "') AND UPass='" + UserPassword.Trim() + "'  ");
                String strQuery = "";
                if (ffsLogin == true)
                {
                    strQuery = "SELECT top 1  UserId, UserName, UPass, IsOtpRequired,Mobile, FullName, RegistrationID , LoginAs ,UserRoleID,Desig_ID FROM Tbl_M_LoginDetails WHERE (IsDeleted is null) AND (Username = '" + UserName.Trim() + "')  order by UserId desc  ";
                }
                else
                {
                    strQuery = "SELECT top 1  UserId, UserName, UPass, IsOtpRequired,Mobile, FullName, RegistrationID , LoginAs ,UserRoleID,Desig_ID FROM Tbl_M_LoginDetails WHERE (IsDeleted is null) AND (Username = '" + UserName.Trim() + "') AND UPass='" + UserPassword.Trim() + "'  order by UserId desc ";
                }
                cla.OpenReturenDs(strQuery);

                if (cla.Ds.Tables[0].Rows.Count > 0)
                {
                    string guid = Guid.NewGuid().ToString();

                    clsLoginCheckWithOTP d = new clsLoginCheckWithOTP();
                    if (cla.Ds.Tables[0].Rows[0]["UserId"].ToString() != "")
                    {
                        cla.ExecuteCommand("update Tbl_M_LoginDetails set MobileTokenID='" + guid + "' where UserId=" + cla.Ds.Tables[0].Rows[0]["UserId"].ToString() + " ");
                    }

                    d.FullName = cla.Ds.Tables[0].Rows[0]["FullName"].ToString();
                    d.Desig_ID = cla.Ds.Tables[0].Rows[0]["Desig_ID"].ToString();
                    d.LoginAs = cla.Ds.Tables[0].Rows[0]["LoginAs"].ToString();
                    d.UserId = cla.Ds.Tables[0].Rows[0]["UserId"].ToString();
                    d.RegistrationID = cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString();
                    if (cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString() != "")
                    {
                        d.BeneficiaryTypesID = cla.GetExecuteScalar("SELECT  BeneficiaryTypesID FROM Tbl_M_RegistrationDetails where RegistrationID=" + cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString() + "");
                    }


                    if (d.BeneficiaryTypesID == null)
                    {
                        d.BeneficiaryTypesID = "";
                    }

                    d.Message = "SUCCESS";
                    d.AuthTokenID = guid;
                    d.Error = "";


                    Random a = new Random(Guid.NewGuid().GetHashCode());
                    int MyNumber = a.Next(000000, 999999);
                    String strOTP = MyNumber.ToString();
                    int SmsSend = 0;

                    String sms = "OTP for login to the DBT portal is " + strOTP + ". It will be valid for 5 min. Team PoCRA";// "This is your one-time password " + strOTP + " Thank you. You";
                    SmsSend = SMS.SendSMS(sms, cla.Ds.Tables[0].Rows[0]["Mobile"].ToString().Trim(), "1407161960968273289");

                    d.OTP = strOTP;

                    lst.Add(d);
                }
                else
                {
                    clsLoginCheckWithOTP d = new clsLoginCheckWithOTP();
                    d.LoginAs = "";
                    d.UserId = "";
                    d.RegistrationID = "";
                    d.BeneficiaryTypesID = "";
                    d.Message = "Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator.";
                    d.Error = "";
                    d.OTP = "";
                    lst.Add(d);

                }
            }
            catch (Exception ex)
            {
                //Label1.Text = ex.ToString();
                clsLoginCheckWithOTP d = new clsLoginCheckWithOTP();
                d.LoginAs = "";
                d.UserId = "";
                d.RegistrationID = "";
                d.BeneficiaryTypesID = "";
                d.Message = "Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator.";
                d.Error = ex.ToString();
                d.OTP = "";
                lst.Add(d);
                // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MsgBox", "<script>alert('Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator !!!')</script>", false);
            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }


        [WebMethod]
        public void CheckUserExistanceWithOTPWithLoginLogs(String SecurityKey, String UserName, String UserPassword, string LogDetails)
        { 
            String UName = UserName.Trim();
            if (!String.IsNullOrEmpty(UName))
            {
                if (UName.Contains("="))
                    UserName = UName.Replace('=', ' ');
                else
             if (UName.Contains("-"))
                    UserName = UName.Replace('-', ' ');
            }


            DataTable dt = new DataTable();
            MyClass cla = new MyClass();

            List<clsLoginCheckWithOTP> lst = new List<clsLoginCheckWithOTP>();

            if (MyCommanClassAPI.CheckApiAuthrization("1", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsLoginCheckWithOTP d = new clsLoginCheckWithOTP();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {


                //Boolean ffsLogin = false;
                //String FFSID = cla.GetExecuteScalar("SELECT UserId FROM Tbl_M_LoginDetails where UserName='" + UserName.Trim() + "' and IsDeleted is null ");
                Boolean ffsLogin = false;
                String FFSID = "YES";// cla.GetExecuteScalar("SELECT UserId FROM Tbl_M_LoginDetails where UserName='" + UserName.Trim() + "' and IsDeleted is null ");
                //if (FFSID.Trim().Length == 0)
                //{
                //    FFSID = cla.GetExecuteScalar("SELECT Max(UserId) FROM Tbl_M_LoginDetails where UserName='" + UserName.Trim() + "'  ");
                //}
                if (FFSID.Trim().Length > 0)
                {
                    if (getTokenId(UserName.Trim(), UserPassword.Trim()) == "200")
                    {
                        ffsLogin = true;
                    }

                }


                //cla.OpenReturenDs("SELECT UserId, UserName, UPass, FullName, RegistrationID ,Desig_ID ,LoginAs FROM Tbl_M_LoginDetails WHERE (IsDeleted is null) AND (Username = '" + UserName.Trim() + "') AND UPass='" + UserPassword.Trim() + "'  ");
                String strQuery = "";
                if (ffsLogin == true)
                {
                    strQuery = "SELECT top 1  UserId, UserName, UPass, IsOtpRequired,Mobile, FullName, RegistrationID , LoginAs ,UserRoleID,Desig_ID FROM Tbl_M_LoginDetails WHERE (IsDeleted is null) AND (Username = '" + UserName.Trim() + "')  order by UserId desc  ";
                }
                else
                {
                    strQuery = "SELECT top 1  UserId, UserName, UPass, IsOtpRequired,Mobile, FullName, RegistrationID , LoginAs ,UserRoleID,Desig_ID FROM Tbl_M_LoginDetails WHERE (IsDeleted is null) AND (Username = '" + UserName.Trim() + "') AND UPass='" + UserPassword.Trim() + "'  order by UserId desc ";
                }
                cla.OpenReturenDs(strQuery);

                if (cla.Ds.Tables[0].Rows.Count > 0)
                {
                    string guid = Guid.NewGuid().ToString();

                    clsLoginCheckWithOTP d = new clsLoginCheckWithOTP();
                    if (cla.Ds.Tables[0].Rows[0]["UserId"].ToString() != "")
                    {
                        cla.ExecuteCommand("update Tbl_M_LoginDetails set MobileTokenID='" + guid + "' where UserId=" + cla.Ds.Tables[0].Rows[0]["UserId"].ToString() + " ");
                    }

                    d.FullName = cla.Ds.Tables[0].Rows[0]["FullName"].ToString();
                    d.Desig_ID = cla.Ds.Tables[0].Rows[0]["Desig_ID"].ToString();
                    d.LoginAs = cla.Ds.Tables[0].Rows[0]["LoginAs"].ToString();
                    d.UserId = cla.Ds.Tables[0].Rows[0]["UserId"].ToString();
                    d.RegistrationID = cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString();
                    if (cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString() != "")
                    {
                        d.BeneficiaryTypesID = cla.GetExecuteScalar("SELECT  BeneficiaryTypesID FROM Tbl_M_RegistrationDetails where RegistrationID=" + cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString() + "");
                    }


                    if (d.BeneficiaryTypesID == null)
                    {
                        d.BeneficiaryTypesID = "";
                    }

                    d.Message = "SUCCESS";
                    d.AuthTokenID = guid;
                    d.Error = "";


                    Random a = new Random(Guid.NewGuid().GetHashCode());
                    int MyNumber = a.Next(000000, 999999);
                    String strOTP = MyNumber.ToString();
                    int SmsSend = 0;


                    cla.ExecuteCommand("INSERT INTO Tbl_M_LoginDetails_Log (UserID,Username ,FromSource ,From_IP )  VALUES  ('" + d.UserId + "','" + UserName.Trim() + "','MobileApp Login', '" + LogDetails.Trim() + "') ");


                    String sms = "OTP for login to the DBT portal is " + strOTP + ". It will be valid for 5 min. Team PoCRA";// "This is your one-time password " + strOTP + " Thank you. You";
                    SmsSend = SMS.SendSMS(sms, cla.Ds.Tables[0].Rows[0]["Mobile"].ToString().Trim(), "1407161960968273289");

                    d.OTP = strOTP;

                    lst.Add(d);
                }
                else
                {

                    cla.ExecuteCommand("INSERT INTO Tbl_M_LoginDetails_Log (Username ,FromSource ,From_IP )  VALUES  ('" + UserName.Trim() + "','MobileApp Login', 'Wrong Password | " + LogDetails.Trim() + "') ");


                    clsLoginCheckWithOTP d = new clsLoginCheckWithOTP();
                    d.LoginAs = "";
                    d.UserId = "";
                    d.RegistrationID = "";
                    d.BeneficiaryTypesID = "";
                    d.Message = "Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator.";
                    d.Error = "";
                    d.OTP = "";
                    lst.Add(d);

                }
            }
            catch (Exception ex)
            {
                //Label1.Text = ex.ToString();
                clsLoginCheckWithOTP d = new clsLoginCheckWithOTP();
                d.LoginAs = "";
                d.UserId = "";
                d.RegistrationID = "";
                d.BeneficiaryTypesID = "";
                d.Message = "Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator.";
                d.Error = ex.ToString();
                d.OTP = "";
                lst.Add(d);
                // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MsgBox", "<script>alert('Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator !!!')</script>", false);
            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }




        [WebMethod]
        public void CheckUserExistanceWithAuthTokenID(String SecurityKey, String UserName, String UserPassword, String AuthTokenID)
        {

            String UName = UserName.Trim();
            if (!String.IsNullOrEmpty(UName))
            {
                if (UName.Contains("="))
                    UserName = UName.Replace('=', ' ');
                else
             if (UName.Contains("-"))
                    UserName = UName.Replace('-', ' ');
            }


            DataTable dt = new DataTable();
            MyClass cla = new MyClass();

            List<clsLoginCheck> lst = new List<clsLoginCheck>();

            if (MyCommanClassAPI.CheckApiAuthrization("1", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsLoginCheck d = new clsLoginCheck();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {


                Boolean ffsLogin = false;
                String FFSID = "YES";// cla.GetExecuteScalar("SELECT UserId FROM Tbl_M_LoginDetails where UserName='" + UserName.Trim() + "' and IsDeleted is null ");
                if (FFSID.Trim().Length > 0)
                {
                    if (getTokenId(UserName.Trim(), UserPassword.Trim()) == "200")
                    {
                        ffsLogin = true;
                    }

                }




                //cla.OpenReturenDs("SELECT UserId, UserName, UPass, FullName, RegistrationID ,Desig_ID ,LoginAs FROM Tbl_M_LoginDetails WHERE (IsDeleted is null) AND (Username = '" + UserName.Trim() + "') AND UPass='" + UserPassword.Trim() + "'  ");
                String strQuery = "";
                if (ffsLogin == true)
                {
                    strQuery = "SELECT top 1  UserId, UserName, UPass, IsOtpRequired,Mobile, FullName, RegistrationID , LoginAs ,UserRoleID,Desig_ID FROM Tbl_M_LoginDetails WHERE (IsDeleted is null) AND (Username = '" + UserName.Trim() + "')   order by UserId desc  ";
                }
                else
                {
                    strQuery = "SELECT top 1  UserId, UserName, UPass, IsOtpRequired,Mobile, FullName, RegistrationID , LoginAs ,UserRoleID,Desig_ID FROM Tbl_M_LoginDetails WHERE (IsDeleted is null) AND (Username = '" + UserName.Trim() + "') AND UPass='" + UserPassword.Trim() + "'    order by UserId desc ";
                }
                cla.OpenReturenDs(strQuery);

                if (cla.Ds.Tables[0].Rows.Count > 0)
                {


                    //if (UserPassword.Trim() == cla.Ds.Tables[0].Rows[0]["UPass"].ToString())
                    //{
                    //Session["LoginAs"] = cla.Ds.Tables[0].Rows[0]["LoginAs"].ToString();
                    //Session["UserId"] = cla.Ds.Tables[0].Rows[0]["UserId"].ToString();
                    //Session["RegistrationID"] = cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString();
                    //Session["Comp_ID"] = "1";
                    //Session["UsersName"] = cla.Ds.Tables[0].Rows[0]["FullName"].ToString();
                    //Session["BeneficiaryTypesID"] = cla.GetExecuteScalar("SELECT  BeneficiaryTypesID FROM Tbl_M_RegistrationDetails where RegistrationID=" + Session["RegistrationID"].ToString() + "");
                    string guid = Guid.NewGuid().ToString();

                    clsLoginCheck d = new clsLoginCheck();
                    if (cla.Ds.Tables[0].Rows[0]["UserId"].ToString().Trim() != "")
                    {
                        cla.ExecuteCommand("update Tbl_M_LoginDetails set MobileTokenID='" + guid + "' where UserId=" + cla.Ds.Tables[0].Rows[0]["UserId"].ToString() + " ");
                    }

                    d.FullName = cla.Ds.Tables[0].Rows[0]["FullName"].ToString();
                    d.Desig_ID = cla.Ds.Tables[0].Rows[0]["Desig_ID"].ToString();
                    d.LoginAs = cla.Ds.Tables[0].Rows[0]["LoginAs"].ToString();
                    d.UserId = cla.Ds.Tables[0].Rows[0]["UserId"].ToString();
                    d.RegistrationID = cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString();

                    if (cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString() != "")
                    {
                        d.BeneficiaryTypesID = cla.GetExecuteScalar("SELECT  BeneficiaryTypesID FROM Tbl_M_RegistrationDetails where RegistrationID=" + cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString() + "");
                    }

                    d.Message = "SUCCESS";
                    d.AuthTokenID = guid;
                    d.Error = "";
                    lst.Add(d);


                    //}
                    //else
                    //{
                    //    // clsMessages.Errormsg(Literal2, "Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator.");
                    //    clsLoginCheck d = new clsLoginCheck();
                    //    d.LoginAs = "";
                    //    d.UserId = "";
                    //    d.RegistrationID = "";
                    //    d.BeneficiaryTypesID = "";
                    //    d.Message = "Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator.";
                    //    d.Error = "";
                    //    lst.Add(d);
                    //}

                }
                else
                {
                    clsLoginCheck d = new clsLoginCheck();
                    d.LoginAs = "";
                    d.UserId = "";
                    d.RegistrationID = "";
                    d.BeneficiaryTypesID = "";
                    d.Message = "Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator.";
                    d.Error = "";
                    lst.Add(d);

                }
            }
            catch (Exception ex)
            {
                //Label1.Text = ex.ToString();
                clsLoginCheck d = new clsLoginCheck();
                d.LoginAs = "";
                d.UserId = "";
                d.RegistrationID = "";
                d.BeneficiaryTypesID = "";
                d.Message = "Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator.";
                d.Error = ex.ToString();
                lst.Add(d);
                // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MsgBox", "<script>alert('Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator !!!')</script>", false);
            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }


        [WebMethod]
        public void CheckUserExistanceWithImage(String SecurityKey, String UserName, String UserPassword, String ProfileImageInBase)
        {
            DataTable dt = new DataTable();
            MyClass cla = new MyClass();

            List<clsLoginCheck> lst = new List<clsLoginCheck>();

            if (MyCommanClassAPI.CheckApiAuthrization("1", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsLoginCheck d = new clsLoginCheck();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {


                Boolean ffsLogin = false;
                //String FFSID = "YES";// cla.GetExecuteScalar("SELECT UserId FROM Tbl_M_LoginDetails where UserName='" + UserName.Trim() + "' and IsDeleted is null ");
                //if (FFSID.Trim().Length > 0)
                //{
                if (getTokenId(UserName.Trim(), UserPassword.Trim()) == "200")
                {
                    ffsLogin = true;
                }

                //}




                //cla.OpenReturenDs("SELECT UserId, UserName, UPass, FullName, RegistrationID ,Desig_ID ,LoginAs FROM Tbl_M_LoginDetails WHERE (IsDeleted is null) AND (Username = '" + UserName.Trim() + "') AND UPass='" + UserPassword.Trim() + "'  ");
                String strQuery = "";
                if (ffsLogin == true)
                {
                    strQuery = "SELECT top 1  UserId, UserName, UPass, IsOtpRequired,Mobile, FullName, RegistrationID , LoginAs ,UserRoleID,Desig_ID FROM Tbl_M_LoginDetails WHERE (IsDeleted is null) AND (Username = '" + UserName.Trim() + "')  order by UserId desc  ";
                }
                else
                {
                    strQuery = "SELECT top 1  UserId, UserName, UPass, IsOtpRequired,Mobile, FullName, RegistrationID , LoginAs ,UserRoleID,Desig_ID FROM Tbl_M_LoginDetails WHERE (IsDeleted is null) AND (Username = '" + UserName.Trim() + "') AND UPass='" + UserPassword.Trim() + "'  order by UserId desc ";
                }
                cla.OpenReturenDs(strQuery);

                if (cla.Ds.Tables[0].Rows.Count > 0)
                {


                    //if (UserPassword.Trim() == cla.Ds.Tables[0].Rows[0]["UPass"].ToString())
                    //{
                    //Session["LoginAs"] = cla.Ds.Tables[0].Rows[0]["LoginAs"].ToString();
                    //Session["UserId"] = cla.Ds.Tables[0].Rows[0]["UserId"].ToString();
                    //Session["RegistrationID"] = cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString();
                    //Session["Comp_ID"] = "1";
                    //Session["UsersName"] = cla.Ds.Tables[0].Rows[0]["FullName"].ToString();
                    //Session["BeneficiaryTypesID"] = cla.GetExecuteScalar("SELECT  BeneficiaryTypesID FROM Tbl_M_RegistrationDetails where RegistrationID=" + Session["RegistrationID"].ToString() + "");

                    clsLoginCheck d = new clsLoginCheck();
                    d.FullName = cla.Ds.Tables[0].Rows[0]["FullName"].ToString();
                    d.Desig_ID = cla.Ds.Tables[0].Rows[0]["Desig_ID"].ToString();
                    d.LoginAs = cla.Ds.Tables[0].Rows[0]["LoginAs"].ToString();
                    d.UserId = cla.Ds.Tables[0].Rows[0]["UserId"].ToString();
                    d.RegistrationID = cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString();
                    if (cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString() != "")
                    {
                        d.BeneficiaryTypesID = cla.GetExecuteScalar("SELECT  BeneficiaryTypesID FROM Tbl_M_RegistrationDetails where RegistrationID=" + cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString() + "");
                    }
                    d.Message = "SUCCESS";
                    d.Error = "";



                    try
                    {
                        if (ProfileImageInBase.Trim().Length > 0)
                        {
                            byte[] imageBytes = Convert.FromBase64String(ProfileImageInBase);
                            AzureBlobHelper fileRet = new AzureBlobHelper();
                            String PathUp = "DocMasters/MemberDoc/" + cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString().ToString().Trim() + "";
                            string imageName = "ProfileImage.jpg";// + MyCommanClassAPI.GetFileExtension(BaseImge);
                            String ret = fileRet.UploadData(PathUp, imageName, imageBytes);
                            if (ret.Trim().Length == 0)
                            {
                                d.ProfilImage = PathUp + "/" + imageName;
                            }
                        }
                    }
                    catch { }

                    //AadharLinkStatus
                    String s1 = cla.GetExecuteScalar("SELECT B.A_Status FROM Tbl_M_RegistrationDetails R INNER JOIN Tbl_M_RegistrationDetails_Bolt B on B.BoltID=R.BoltID where R.RegistrationID=" + cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString().ToString().Trim().Trim() + " ");
                    if (s1.Length > 0)
                    {
                        if (s1.Trim() != "A")
                        {
                            //not linked
                            d.AadharLinkStatus = "Not Linked";
                        }
                        else
                        {
                            d.AadharLinkStatus = "Linked";
                        }
                    }

                    lst.Add(d);

                    //}
                    //else
                    //{
                    //    // clsMessages.Errormsg(Literal2, "Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator.");
                    //    clsLoginCheck d = new clsLoginCheck();
                    //    d.LoginAs = "";
                    //    d.UserId = "";
                    //    d.RegistrationID = "";
                    //    d.BeneficiaryTypesID = "";
                    //    d.Message = "Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator.";
                    //    d.Error = "";
                    //    lst.Add(d);
                    //}

                }
                else
                {
                    clsLoginCheck d = new clsLoginCheck();
                    d.LoginAs = "";
                    d.UserId = "";
                    d.RegistrationID = "";
                    d.BeneficiaryTypesID = "";
                    d.Message = "Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator.";
                    d.Error = "";
                    lst.Add(d);
                }
            }
            catch (Exception ex)
            {
                //Label1.Text = ex.ToString();
                clsLoginCheck d = new clsLoginCheck();
                d.LoginAs = "";
                d.UserId = "";
                d.RegistrationID = "";
                d.BeneficiaryTypesID = "";
                d.Message = "Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator.";
                d.Error = ex.ToString();
                lst.Add(d);
                // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MsgBox", "<script>alert('Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator !!!')</script>", false);
            } 

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
        }
         
        public string getTokenId(String mob, String pass)
        {
            String url = "https://api-ffs.mahapocra.gov.in/3rd-party/authService/sso";
            String TokenId = "";
            string jsonContent = "{\"mob\":\"" + mob.Trim() + "\",\"pass\":\"" + pass.Trim() + "\", \"key\":\"a910d2ba49ef2e4a74f8e0056749b10d\"}";
            //Util.LogErrorFFS("FFS- " + jsonContent);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(jsonContent);

            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            long length = 0;

            request.KeepAlive = false;
            //request.Timeout = 50000;




            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    length = response.ContentLength;
                    StreamReader sr = new StreamReader(response.GetResponseStream());
                    string results = sr.ReadToEnd();
                    //Util.LogErrorFFS("FFS- " + results);
                    //{"status":201,"response":"Unautherized access"}
                    // var JSONObj = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(results);
                    // TokenId = JSONObj["status"].Trim();
                    String[] s = results.Trim().Split(':');
                    String[] a = s[1].Trim().Split(',');//200,"token"
                    TokenId = a[0].Trim();
                    if (TokenId == "200")
                    {
                        FFSClass.UpdateLofinInFFS(results);
                    }
                    if (TokenId == "201")
                    {
                        // update it and make inactive ..........
                        FFSClass.MakeItInactiveInDBT(results);
                    }
                    //FFSClass.UpdateLofinInFFS(results);
                }
            }
            catch (Exception ex)
            {
                Util.LogErrorFFS("FFS- " + ex.ToString() + " -   FOR --   " + jsonContent);
                // Log exception and throw as for GET example above
            }

            return TokenId;
        }

        [WebMethod]
        public void GetRegisteredUnder(String SecurityKey, String Lang)
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();

            if (MyCommanClassAPI.CheckApiAuthrization("25", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                dt = Comcls.GetRegisteredUnder(Lang);
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = dt.Rows[x]["ID"].ToString();
                    d.Value = dt.Rows[x]["Test"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            // ////Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }

        [WebMethod]
        public void GetRegisteredThrough(String SecurityKey, String Lang)
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();
            if (MyCommanClassAPI.CheckApiAuthrization("24", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)


            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                ////Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                dt = Comcls.GetRegisteredThrough(Lang);
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = dt.Rows[x]["ID"].ToString();
                    d.Value = dt.Rows[x]["Test"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            ////Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }


        [WebMethod]
        public void GetBank(String SecurityKey, String Lang)
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();

            if (MyCommanClassAPI.CheckApiAuthrization("6", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)

            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                ////Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                dt = Comcls.GetBank(Lang);
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = dt.Rows[x]["NameOFbank"].ToString();
                    d.Value = dt.Rows[x]["NameOFbank"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            ////Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }


        [WebMethod]
        public void GetBankWiseBrach(String SecurityKey, String NameOFbank, String Lang)
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("8", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                ////Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                dt = Comcls.GetBankWiseBrach(NameOFbank, Lang);
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = dt.Rows[x]["BranchName"].ToString();
                    d.Value = dt.Rows[x]["BranchName"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }

        [WebMethod]
        public void GetBankBranchWiseIFSC(String SecurityKey, String NameOFbank, String BranchNamek)
        {
            DataTable dt = new DataTable();

            //  MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MobAppComClass Appcls = new MobAppComClass();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("7", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                // dt = Comcls.GetBankBranchWiseIFSC(NameOFbank, BranchNamek);
                dt = Appcls.GetBankBranchWiseIFSC(NameOFbank, BranchNamek);
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = dt.Rows[x]["RBIBankID"].ToString();
                    d.Value = dt.Rows[x]["IFSCCode"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }


        [WebMethod]
        public void GetCastCategory(String SecurityKey, String Lang)
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("9", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                dt = Comcls.GetCostCategoryMaster(Lang);
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = dt.Rows[x]["CategoryMasterID"].ToString();
                    d.Value = dt.Rows[x]["CategoryMaster"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }


        [WebMethod]
        public void GetDistrict(String SecurityKey, String State_ID, String LevelType_ID, String Lang)
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("13", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                dt = Comcls.GetCityAll(State_ID, LevelType_ID, Lang);
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = dt.Rows[x]["City_ID"].ToString();
                    d.Value = dt.Rows[x]["Cityname"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            // //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }
        //
        [WebMethod]
        public void GetCityPoCRA(String SecurityKey, String State_ID, String Lang)
        {
            DataTable dt = new DataTable();

            MobAppComClass Appcls = new MobAppComClass();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("10", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                dt = Appcls.GetCityPocra(State_ID, "", Lang);
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = dt.Rows[x]["City_ID"].ToString();
                    d.Value = dt.Rows[x]["Cityname"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            //Context.Response.Clear();
            //Context.Response.ContentType = "application/json";
            ////Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            //Context.Response.Flush();
            //Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

            //int x1 = JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length;

            try
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", (x1 + 9).ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
            }
            catch
            {

            }


        }


        //
        [WebMethod]
        public void GetTaluka(String SecurityKey, String City_ID, String Lang)
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            //// MobAppComClass Appcls = new MobAppComClass();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("28", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                dt = Comcls.GetTalukaMaster(City_ID, Lang);
                ////   dt = Appcls.GetTalukaMaster(City_ID);
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = dt.Rows[x]["TalukaID"].ToString();
                    d.Value = dt.Rows[x]["Taluka"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }

            //string s = JsonConvert.SerializeObject(lst).ToString();


            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst).ToString());




        }

        [WebMethod]
        public void GetVillage(String SecurityKey, String City_ID, String TalukaID, String Lang)
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            // MobAppComClass Appcls = new MobAppComClass();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("29", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                dt = Comcls.GetVillage(City_ID, TalukaID, Lang);
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = dt.Rows[x]["VillageID"].ToString();
                    d.Value = dt.Rows[x]["VillageName"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }

        [WebMethod]
        public void GetPostMaster(String SecurityKey, String City_ID, String Lang, String TalukaID)
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("21", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                dt = Comcls.GetPostMaster(City_ID, Lang, TalukaID);
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = dt.Rows[x]["Post_ID"].ToString();
                    d.Value = dt.Rows[x]["PostName"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }

        [WebMethod]
        public void GetPostWisePin(String SecurityKey, String Post_ID)
        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<clsValueFromID> lst = new List<clsValueFromID>();


            if (MyCommanClassAPI.CheckApiAuthrization("22", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsValueFromID d = new clsValueFromID();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {

                clsValueFromID d = new clsValueFromID();

                d.Value = cla.GetExecuteScalar("SELECT  PinCode FROM Tbl_M_CityWisePost where Post_ID=" + Post_ID.Trim() + " and IsDeleted is null");
                d.Message = "";
                d.Error = "";
                lst.Add(d);



            }
            catch (Exception ex)
            {

                clsValueFromID d = new clsValueFromID();

                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }

        [WebMethod]
        public void GetVillageWiseCluster(String SecurityKey, String VillageID)
        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<clsValueFromID> lst = new List<clsValueFromID>();

            if (MyCommanClassAPI.CheckApiAuthrization("30", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsValueFromID d = new clsValueFromID();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {

                clsValueFromID d = new clsValueFromID();
                d.Value = cla.GetExecuteScalar("SELECT   Tbl_M_ClustersMaster.Clusters FROM   dbo.Tbl_M_VillageMaster INNER JOIN  dbo.Tbl_M_ClustersMaster ON dbo.Tbl_M_VillageMaster.ClustersMasterID = dbo.Tbl_M_ClustersMaster.ClustersMasterID WHERE  (dbo.Tbl_M_VillageMaster.VillageID =" + VillageID.Trim() + ")");
                d.Message = "";
                d.Error = "";
                lst.Add(d);



            }
            catch (Exception ex)
            {

                clsValueFromID d = new clsValueFromID();

                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }
        //
        [WebMethod]

        public void GetComponent(String SecurityKey, String Lang)
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();

            if (MyCommanClassAPI.CheckApiAuthrization("11", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                dt = Comcls.GetComponent(Lang);
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = dt.Rows[x]["ComponentID"].ToString();
                    d.Value = dt.Rows[x]["ComponentName"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }


        [WebMethod]
        public void GetComponentWiseSubComponent(String SecurityKey, String ComponentID)
        {
            DataTable dt = new DataTable();

            //   MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MobAppComClass MobAppCls = new MobAppComClass();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("12", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                //  dt = Comcls.SubGetComponent();

                dt = MobAppCls.SubGetComponent(ComponentID);
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = dt.Rows[x]["SubComponentID"].ToString();
                    d.Value = dt.Rows[x]["SubComponentName"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }


        [WebMethod]
        public void GetComponentWiseSubComponentMr(String SecurityKey, String ComponentID)
        {
            DataTable dt = new DataTable();

            //   MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MobAppComClass MobAppCls = new MobAppComClass();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("12", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                //  dt = Comcls.SubGetComponent();

                dt = MobAppCls.SubGetComponentMr(ComponentID);
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = dt.Rows[x]["SubComponentID"].ToString();
                    d.Value = dt.Rows[x]["SubComponentName"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }

        [WebMethod]
        public void GetApplication(String SecurityKey, String UserID)
        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("4", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                List<String> lst2 = new List<String>();
                lst2.Add("");
                lst2.Add("");
                lst2.Add("");
                lst2.Add("");
                lst2.Add("");
                lst2.Add("");
                lst2.Add("");
                lst2.Add("");
                lst2.Add("3");
                lst2.Add(UserID);
                lst2.Add("");//ddlGPCode
                lst2.Add("");//txtBeneficiaryName
                dt = cla.GetDtByProcedure("SP_Get_ApplicationGrdData", lst2);

                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = dt.Rows[x]["ApplicationID"].ToString();
                    d.Value = dt.Rows[x]["RegisterName"].ToString() + " / " + dt.Rows[x]["ActivityName"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }

        [WebMethod]

        public void GetImageTitle(String SecurityKey, String Lang)
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("17", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                dt = Comcls.GetImageTitle(Lang);
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = dt.Rows[x]["ImageTitleID"].ToString();
                    d.Value = dt.Rows[x]["ImageTitleName"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }


            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());


        }


        [WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string[] GetSearchTerm(string prefix, String ID, String Lan)
        {
            List<string> customers = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = clsSettings.strCoonectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    if (Lan.Trim() == "en-IN")
                    {
                        cmd.CommandText = "SELECT   distinct   SearchTerm FROM Tbl_M_ActivityMaster WHERE  (IsDeleted IS NULL) and SearchTerm is not null  " +
                        " AND Tbl_M_ActivityMaster.ActivityID in (select distinct ActivityID from Tbl_M_ActivityBeneficiary where  BeneficiaryTypesID=" + ID + ") " +
                    " AND SearchTerm like @SearchText + '%'";
                        cmd.Parameters.AddWithValue("@SearchText", prefix);
                    }
                    else
                    {
                        cmd.CommandText = "SELECT   distinct   SearchTerm FROM Tbl_M_ActivityMaster WHERE  (IsDeleted IS NULL) and SearchTerm is not null  " +
                        " AND Tbl_M_ActivityMaster.ActivityID in (select distinct ActivityID from Tbl_M_ActivityBeneficiary where  BeneficiaryTypesID=" + ID + ") " +
                    " AND SearchTerm like @SearchText + '%'";
                        cmd.Parameters.AddWithValue("@SearchText", prefix);
                    }

                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(sdr["SearchTerm"].ToString());
                        }
                    }
                    conn.Close();
                }
                return customers.ToArray();
            }
        }





        [WebMethod]
        public void GetPreSanctionApplicationStatus(String SecurityKey, String Lang)
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("23", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                dt = Comcls.GetApplicationStatus("AA", Lang); //ApprovalStageID="2",
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = dt.Rows[x]["ApplicationStatusID"].ToString();
                    d.Value = dt.Rows[x]["ApplicationStatus"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }



        [WebMethod]
        public void GetDesk4ApplicationStatus(String SecurityKey, String Lang,string ApplicationID)
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("23", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                dt = Comcls.GetApplicationStatusListForDesk4("AA", Lang, ApplicationID); //ApprovalStageID="2",
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = dt.Rows[x]["ApplicationStatusID"].ToString();
                    d.Value = dt.Rows[x]["ApplicationStatus"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());             
        }


        [WebMethod]
        public void GetApplicationStatusWiseReason(String SecurityKey, String ApprovalStageID, String ApplicationStatusID, String Lang)
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("5", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                dt = Comcls.GetReasonVerifecation(ApprovalStageID, ApplicationStatusID, Lang); //ApprovalStageID="2",
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = dt.Rows[x]["ReasonID"].ToString();
                    d.Value = dt.Rows[x]["Reasons"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());


        }
        [WebMethod]
        public void GetFeasibility(String SecurityKey, String ApplicationID, String Lang)//, D
                                                                                         ////  public void GetFeasibility(String SecurityKey, String ActivityID, String Lang)//, D
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("15", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                MyClass cla = new MyClass();
                if (ApplicationID != "")
                {
                    dt = Comcls.GetFeasibility(cla.GetExecuteScalar("SELECT ActivityID from Tbl_T_ApplicationDetails where ApplicationID=" + ApplicationID.Trim() + ""), Lang);

                    ////  dt = Comcls.GetFeasibility(ActivityID, Lang); // ActivityID,
                    for (int x = 0; x != dt.Rows.Count; x++)
                    {
                        clsDropDownBind d = new clsDropDownBind();
                        d.KeyID = dt.Rows[x]["FeasibilityRptID"].ToString();
                        d.Value = dt.Rows[x]["FeasibilityRpt"].ToString();
                        d.Message = "";
                        d.Error = "";
                        lst.Add(d);

                    }
                }
            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);
            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }

        [WebMethod]
        public void GetDocumentType(String SecurityKey)
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("14", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "Procurement Documents";
                d.Value = "Procurement Documents";

                d.Message = "";
                d.Error = "";
                lst.Add(d);
                clsDropDownBind dd2 = new clsDropDownBind();
                dd2.KeyID = "Pre-Sanction Letter";
                dd2.Value = "Pre-Sanction Letter";

                dd2.Message = "";
                dd2.Error = "";
                lst.Add(dd2);


            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }

        [WebMethod]
        public void GetLevels(String SecurityKey)
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("20", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "ProcurementBill";
                d.Value = "ProcurementBill";

                d.Message = "";
                d.Error = "";
                lst.Add(d);
                clsDropDownBind dd2 = new clsDropDownBind();
                dd2.KeyID = "Pre-Sanction Letter";
                dd2.Value = "Pre-Sanction Letter";

                dd2.Message = "";
                dd2.Error = "";
                lst.Add(dd2);


            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }


            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());


        }
        //
        [WebMethod]
        public void GetInspectionDocumentType(String SecurityKey)
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("18", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "Progress Photographs";
                d.Value = "Progress Photographs";

                d.Message = "";
                d.Error = "";
                lst.Add(d);
                clsDropDownBind d3 = new clsDropDownBind();
                d3.KeyID = "MB";
                d3.Value = "MB";

                d3.Message = "";
                d3.Error = "";
                lst.Add(d3);
                clsDropDownBind d4 = new clsDropDownBind();
                d4.KeyID = "Estimate";
                d4.Value = "Estimate";

                d4.Message = "";
                d4.Error = "";
                lst.Add(d4);
                clsDropDownBind d5 = new clsDropDownBind();
                d5.KeyID = "Inspection Report";
                d5.Value = "Inspection Report";

                d5.Message = "";
                d5.Error = "";
                lst.Add(d5);

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }

        [WebMethod]
        public void GetInspectionLevels(String SecurityKey)
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("19", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {

                // clsDropDownBind d = new clsDropDownBind();
                //d.KeyID = "Before starting of work";
                //d.Value = "Before starting of work";
                //d.Message = "";
                //d.Error = "";
                //lst.Add(d);
                //clsDropDownBind d1 = new clsDropDownBind();
                //d1.KeyID = "During the processing of work";
                //d1.Value = "During the processing of work";

                //d1.Message = "";
                //d1.Error = "";
                //lst.Add(d1);

                clsDropDownBind d2 = new clsDropDownBind();
                d2.KeyID = "Photograph of the site after completion of work";
                d2.Value = "Photograph of the site after completion of work";
                d2.Message = "";
                d2.Error = "";
                lst.Add(d2);


                clsDropDownBind d3 = new clsDropDownBind();
                d3.KeyID = "MB";
                d3.Value = "MB";
                d3.Message = "";
                d3.Error = "";
                lst.Add(d3);

                clsDropDownBind d4 = new clsDropDownBind();
                d4.KeyID = "Estimate";
                d4.Value = "Estimate";
                d4.Message = "";
                d4.Error = "";
                lst.Add(d4);

                clsDropDownBind d5 = new clsDropDownBind();
                d5.KeyID = "Inspection Report";
                d5.Value = "Inspection Report";
                d5.Message = "";
                d5.Error = "";
                lst.Add(d5);


                clsDropDownBind d6 = new clsDropDownBind();
                d6.KeyID = "Training certificate";
                d6.Value = "Training certificate";
                d6.Message = "";
                d6.Error = "";
                lst.Add(d6);


                clsDropDownBind d7 = new clsDropDownBind();
                d7.KeyID = "Spot Verification Report";
                d7.Value = "Spot Verification Report";
                d7.Message = "";
                d7.Error = "";
                lst.Add(d7);

                clsDropDownBind d8 = new clsDropDownBind();
                d8.KeyID = "Other Report/Documents";
                d8.Value = "Other Report/Documents";
                d8.Message = "";
                d8.Error = "";
                lst.Add(d8);


                clsDropDownBind d9 = new clsDropDownBind();
                d9.KeyID = " कंपनी प्रतिनिधीने तयार केलेला सूक्ष्म सिंचन आराखडा व प्रमाणपत्र";
                d9.Value = " कंपनी प्रतिनिधीने तयार केलेला सूक्ष्म सिंचन आराखडा व प्रमाणपत्र";
                d9.Message = "";
                d9.Error = "";
                lst.Add(d9);
                //
                clsDropDownBind d10 = new clsDropDownBind();
                d10.KeyID = "पाणी व मृद तपासणी अहवाल";
                d10.Value = "पाणी व मृद तपासणी अहवाल";
                d10.Message = "";
                d10.Error = "";
                lst.Add(d10);
                //
                clsDropDownBind d11 = new clsDropDownBind();
                d11.KeyID = "मापनपुस्तिका";
                d11.Value = "मापनपुस्तिका";
                d11.Message = "";
                d11.Error = "";
                lst.Add(d11);
                //
                clsDropDownBind d12 = new clsDropDownBind();
                d12.KeyID = "मोका तपासणी अहवाल तुषार सिंचन संच(प्रपत्र -५ ब)";
                d12.Value = "मोका तपासणी अहवाल तुषार सिंचन संच(प्रपत्र -५ ब)";
                d12.Message = "";
                d12.Error = "";
                lst.Add(d12);
                //
                clsDropDownBind d13 = new clsDropDownBind();
                d13.KeyID = "लाभार्थी अनुदान मागणी पत्र (प्रपत्र -४ ब)";
                d13.Value = "लाभार्थी अनुदान मागणी पत्र (प्रपत्र -४ ब)";
                d13.Message = "";
                d13.Error = "";
                lst.Add(d13);
                //
                clsDropDownBind d14 = new clsDropDownBind();
                d14.KeyID = "लाभार्थीने अपलोड केलेली देयके";
                d14.Value = "लाभार्थीने अपलोड केलेली देयके";
                d14.Message = "";
                d14.Error = "";
                lst.Add(d14);
                //
                clsDropDownBind d15 = new clsDropDownBind();
                d15.KeyID = "शेतकऱ्याने द्यावयाचे हमीपत्र(प्रपत्र -१ ब)";
                d15.Value = "शेतकऱ्याने द्यावयाचे हमीपत्र(प्रपत्र -१ ब)";
                d15.Message = "";
                d15.Error = "";
                lst.Add(d15);
                //
                clsDropDownBind d16 = new clsDropDownBind();
                d16.KeyID = "संकल्प आराखडा व प्रमाणपत्र (Annexure - I)";
                d16.Value = "संकल्प आराखडा व प्रमाणपत्र (Annexure - I)";
                d16.Message = "";
                d16.Error = "";
                lst.Add(d16);
            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }


            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());


        }
        [WebMethod]
        public void GetApplicantArrangeFund(String SecurityKey, String Lang)
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("3", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                if (Lang.Trim() == "en-IN")
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = "Self";
                    d.Value = "Self";

                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);
                    clsDropDownBind dd2 = new clsDropDownBind();
                    dd2.KeyID = "Bank Loan";
                    dd2.Value = "Bank Loan";

                    dd2.Message = "";
                    dd2.Error = "";
                    lst.Add(dd2);
                }
                else
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = "स्वतः";
                    d.Value = "स्वतः";

                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);
                    clsDropDownBind dd2 = new clsDropDownBind();
                    dd2.KeyID = "बँक कर्ज";
                    dd2.Value = "बँक कर्ज";

                    dd2.Message = "";
                    dd2.Error = "";
                    lst.Add(dd2);
                }


            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }
        [WebMethod]
        public void GetGPCodeVCRMCMeetingDate(String SecurityKey, String ApplicationID)//, D
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("16", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                MyClass cla = new MyClass();



                dt = Comcls.GetApplicationMeeting(ApplicationID);
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = dt.Rows[x]["ID"].ToString();
                    d.Value = dt.Rows[x]["Names"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }
        [WebMethod]

        public void GetActivityCategory(String SecurityKey, String Lang)
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("2", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                dt = Comcls.GetActivityCategory(Lang);
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = dt.Rows[x]["ActivityCategoryID"].ToString();
                    d.Value = dt.Rows[x]["ActivityCategory"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }


            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());


        }
        [WebMethod]
        public void GetSearchTermSJson(String SecurityKey, String prefix, String ID, String Lan)
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("27", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {

                MyClass cla = new MyClass();
                dt = cla.GetDataTable("SELECT   distinct   SearchTerm FROM Tbl_M_ActivityMaster WHERE  (IsDeleted IS NULL) and SearchTerm is not null   AND Tbl_M_ActivityMaster.ActivityID in (select distinct ActivityID from Tbl_M_ActivityBeneficiary where  BeneficiaryTypesID=" + ID + ") " + "  AND SearchTerm like '" + prefix + "%'");
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = dt.Rows[x]["SearchTerm"].ToString();
                    d.Value = dt.Rows[x]["SearchTerm"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());


        }
        [WebMethod]
        public void GetAssignToEmployee(String SecurityKey, String RegistrationID)
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();

            if (MyCommanClassAPI.CheckApiAuthrization("80", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                dt = Comcls.GetPageWiseUseList(RegistrationID.ToString().Trim(), "101");
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = dt.Rows[x]["UserId"].ToString();
                    d.Value = dt.Rows[x]["FullName"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }


        [WebMethod]
        public string checkUserNameAvail(string id, string val)
        {
            string s = "";
            try
            {
                if (id.Length > 0)
                {
                    MyClass cla = new MyClass();
                    cla.ExecuteCommand("UPDATE Tbl_M_RegistrationDetails SET RegisterNameMr=N'" + val + "' where RegistrationID=" + id + "");
                }

                return s;
            }
            catch
            {
                return null;
            }
        }



        [WebMethod]
        public void TestUserLogin(String SecurityKey, String UID, String TestPassword)
        {
            DataTable dt = new DataTable();
            MyClass cla = new MyClass();

            List<clsLoginCheck> lst = new List<clsLoginCheck>();

            if (MyCommanClassAPI.CheckApiAuthrization("1", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsLoginCheck d = new clsLoginCheck();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                if (TestPassword.Trim() == "")
                {
                    //Label1.Text = ex.ToString();
                    clsLoginCheck d = new clsLoginCheck();
                    d.LoginAs = "";
                    d.UserId = "";
                    d.RegistrationID = "";
                    d.BeneficiaryTypesID = "";
                    d.Message = "Please Enter Valid Credentials";
                    d.Error = "Please Enter Valid Credentials";
                    lst.Add(d);
                }
                else
                {

                    //cla.OpenReturenDs("SELECT UserId, UserName, UPass, FullName, RegistrationID ,Desig_ID ,LoginAs FROM Tbl_M_LoginDetails WHERE (IsDeleted is null) AND (Username = '" + UserName.Trim() + "') AND UPass='" + UserPassword.Trim() + "'  ");
                    String strQuery = "";
                    strQuery = "SELECT top 1  UserId, UserName, UPass, IsOtpRequired,Mobile, FullName, RegistrationID , LoginAs ,UserRoleID,Desig_ID FROM Tbl_M_LoginDetails WHERE (IsDeleted is null) AND (Username = '" + UID.Trim() + "') AND TestKey='" + TestPassword + "'  order by UserId desc  ";

                    cla.OpenReturenDs(strQuery);

                    if (cla.Ds.Tables[0].Rows.Count > 0)
                    {

                        clsLoginCheck d = new clsLoginCheck();
                        d.FullName = cla.Ds.Tables[0].Rows[0]["FullName"].ToString();
                        d.Desig_ID = cla.Ds.Tables[0].Rows[0]["Desig_ID"].ToString();
                        d.LoginAs = cla.Ds.Tables[0].Rows[0]["LoginAs"].ToString();
                        d.UserId = cla.Ds.Tables[0].Rows[0]["UserId"].ToString();
                        d.RegistrationID = cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString();
                        if (cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString() != "")
                        {
                            d.BeneficiaryTypesID = cla.GetExecuteScalar("SELECT  BeneficiaryTypesID FROM Tbl_M_RegistrationDetails where RegistrationID=" + cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString() + "");
                        }
                        d.Message = "SUCCESS";
                        d.Error = "";


                        String s1 = cla.GetExecuteScalar("SELECT B.A_Status FROM Tbl_M_RegistrationDetails R INNER JOIN Tbl_M_RegistrationDetails_Bolt B on B.BoltID=R.BoltID where R.RegistrationID=" + cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString().ToString().Trim().Trim() + " ");
                        if (s1.Length > 0)
                        {
                            if (s1.Trim() != "A")
                            {
                                //not linked
                                d.AadharLinkStatus = "Not Linked";
                            }
                            else
                            {
                                d.AadharLinkStatus = "Linked";
                            }
                        }

                        lst.Add(d);
                    }
                    else
                    {
                        clsLoginCheck d = new clsLoginCheck();
                        d.LoginAs = "";
                        d.UserId = "";
                        d.RegistrationID = "";
                        d.BeneficiaryTypesID = "";
                        d.Message = "Invalid Login";
                        d.Error = "";
                        lst.Add(d);

                    }
                }
            }
            catch (Exception ex)
            {
                //Label1.Text = ex.ToString();
                clsLoginCheck d = new clsLoginCheck();
                d.LoginAs = "";
                d.UserId = "";
                d.RegistrationID = "";
                d.BeneficiaryTypesID = "";
                d.Message = "Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator.";
                d.Error = ex.ToString();
                lst.Add(d);
                // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MsgBox", "<script>alert('Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator !!!')</script>", false);
            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
        }
    }
}



class clsLoginCheck
{

    public string RegistrationID { get; set; }
    public string BeneficiaryTypesID { get; set; }
    public String UserId { get; set; }
    public String Desig_ID { get; set; }
    public String LoginAs { get; set; }//FullName
    public String FullName { get; set; }
    public String Message { get; set; }
    public string Error { get; set; }
    public String ProfilImage { get; set; }
    public String AadharLinkStatus { get; set; }
    public String AuthTokenID { get; set; }

}


class clsLoginCheckWithOTP
{

    public string RegistrationID { get; set; }
    public string BeneficiaryTypesID { get; set; }
    public String UserId { get; set; }
    public String Desig_ID { get; set; }
    public String LoginAs { get; set; }//FullName
    public String FullName { get; set; }
    public String Message { get; set; }
    public string Error { get; set; }
    public String ProfilImage { get; set; }
    public String AadharLinkStatus { get; set; }
    public String AuthTokenID { get; set; }

    public String OTP { get; set; }

}
public class clsDropDownBind
{

    public string KeyID { get; set; }
    public string Value { get; set; }
    public String Message { get; set; }
    public string Error { get; set; }

}
public class clsValueFromID
{


    public string Value { get; set; }
    public String Message { get; set; }
    public string Error { get; set; }

}

public class clsCommanReturns
{
    public String Message { get; set; }
    public string Error { get; set; }

}