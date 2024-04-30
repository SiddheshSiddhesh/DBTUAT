using CommanClsLibrary;
using CommanClsLibrary.Repository.Classes;
using CommanClsLibrary.Repository.Classes.StaticClasses;
using CommanClsLibrary.Repository.Models;
using DBTPoCRA.AdminTrans.UserControls;
using DBTPoCRA.APPData;
using MOLCryptoEngine;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using static CommanClsLibrary.Repository.Enums;

namespace DBTPoCRA
{

    public partial class FarmerLogin : System.Web.UI.Page
    {
        MyClass cla = new MyClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ViewState["Img"] = "";
                    Label5.Text = "";
                    ExpireAllCookies();
                    Session.Clear();
                    //if (Request.QueryString.Count > 0)
                    //{
                        //ddlUserType.SelectedValue = Request.QueryString["T"].Trim();
                        //UpdateLoginType(Request.QueryString["T"].Trim());
                        Literal3.Text = "INDIVIDUAL FARMER LOGIN ";
                        RadioButtonList1_SelectedIndexChanged(sender, null);
                        if (Session["Lang"] != null)
                        {
                            if (Session["Lang"].ToString().Trim() == "mr-IN")
                            {
                                btnMarathi.Text = "English";
                                LoadString();
                            }
                        }
                    //}
                    //else
                    //{
                    //    Response.Redirect("Default.aspx", false);
                    //}

                    btnLogin.Visible = false;
                }
            }
            catch { }
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


        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string txn = "";
            if (Session["AdharTxn"] != null)
            {
                txn = Session["AdharTxn"].ToString();
            }

            if (txn != "")
            {
                try
                {
                    Convert.ToDouble(txtname.Value.Trim());
                }
                catch
                {
                    Util.ShowMessageBox(this.Page, "Error", "Invalid Input", "error");
                    return;
                }

                Literal2.Text = "";
                String Pht = "";
                try
                {
                    cla.OpenReturenDs("SELECT top 1 UserId, UserName, UPass, FullName, RegistrationID , LoginAs ,UserRoleID,Desig_ID FROM Tbl_M_LoginDetails WHERE (IsDeleted is null) AND (Username = '" + txtname.Value.Trim() + "') order by UserId desc ");
                    if (cla.Ds.Tables[0].Rows.Count > 0)
                    {
                        //Boolean CanLogin = false;
                        String name = "";
                        String RegisterNameMr = "";
                        if (Request.QueryString["D"].Trim() == "1")
                        {
                            if (RadioButtonList1.SelectedValue.Trim() == "OTP")
                            {
                                // in case of otp
                                #region "in case of otp"

                                /*

                                //string keyPath = Server.MapPath("~/admintrans/UserControls/uidai_auth_encrypt_preprod.cer");
                                string keyPath = Server.MapPath("~/admintrans/UserControls/Production/uidai_auth_prod.cer");

                                String UID = txtname.Value.Trim();
                                String Txn = "UKC:" + ViewState["Txn"].ToString().Trim();

                                com.auth.AuthPacketCreator objAuthPacket = new com.auth.AuthPacketCreator();



                                string authxml = objAuthPacket.createOtpKYCPacket(UID, txtPassword.Value.Trim(), keyPath, "PNDKS23054", Txn, "2018-11-02T11:11:46.027+05:30", "UDC-AGRIGOM-0001", "");

                                XmlDocument xdocl = new XmlDocument();
                                xdocl.LoadXml(authxml);

                                String Skey = "", Data = "", Hmac = "";

                                foreach (XmlNode xNode in xdocl)
                                {

                                    foreach (XmlNode CNode in xNode)
                                    {
                                        if (CNode.Name.Trim() == "Skey")
                                        {
                                            Skey = CNode.InnerText;
                                        }
                                        if (CNode.Name.Trim() == "Data")
                                        {
                                            Data = CNode.InnerText;
                                        }
                                        if (CNode.Name.Trim() == "Hmac")
                                        {
                                            Hmac = CNode.InnerText;
                                        }
                                    }
                                }



                                // new aadhar feathure
                                clsCrypto cls = new global::clsCrypto();
                                String Password = "testndks@123";

                                byte[] key = MOLCryptoEngine.MOLSecurity.GenerateKey(256);
                                byte[] IV = MOLSecurity.GenerateIV(128, "uidaimaharashtra");
                                string EncryptedUID = MOLSecurity.AESEncrypt(UID, IV, key);// Convert.ToBase64String(encrypted);
                                string PasswordUID = MOLSecurity.AESEncrypt(Password, IV, key);// Convert.ToBase64String(encryptedPassword);
                                String Certificate = System.Web.Hosting.HostingEnvironment.MapPath("~/AdminTrans/UserControls/Production/JME.cer");
                                string incodedkey = Convert.ToBase64String(key);
                                String AUASkey = MOLSecurity.EncryptWithPublicKey(incodedkey, Certificate);// EncryptCertificate(Encoding.ASCII.GetBytes(incodedkey), cert.GetRawCertData());
                                string HA256KeyValue = cls.generateSha256Hash(incodedkey);

                                string HA256saTxn = cls.generateSha256Hash(Txn);
                                String aa = PasswordUID + "~" + EncryptedUID + "~" + HA256KeyValue + "~" + HA256saTxn;
                                aa = aa.ToLower();

                                String Token = cls.generateSha256Hash(aa);


                                String str = "<KUAData xmlns=" + PutIntoQuotes("http://kua.maharashtra.gov.in/kyc/gom-kyc-request") + ">";
                                str += "<uid>" + EncryptedUID + "</uid>";
                                str += "<appCode>KYCApp</appCode>";
                                str += "<Token>" + Token.Trim() + "</Token>";
                                str += "<KUASkey>" + AUASkey.Trim() + "</KUASkey>";
                                str += "<sa>PNDKS23054</sa>";
                                str += "<saTxn>" + Txn + "</saTxn>";
                                str += "<Data type =" + PutIntoQuotes("X") + ">" + Data + "</Data>";
                                str += "<Hmac>" + Hmac + "</Hmac>";
                                str += "<Skey ci =" + PutIntoQuotes("20250923") + ">" + Skey + "</Skey>";
                                str += "<Uses pi =" + PutIntoQuotes("n") + " pa =" + PutIntoQuotes("n") + " pfa =" + PutIntoQuotes("n") + " bio =" + PutIntoQuotes("n") + " pin=" + PutIntoQuotes("n") + " otp=" + PutIntoQuotes("y") + "/>";
                                str += "<Meta rdsId =" + PutIntoQuotes("") + " rdsVer =" + PutIntoQuotes("") + " dpId =" + PutIntoQuotes("") + " dc =" + PutIntoQuotes("") + " mi =" + PutIntoQuotes("") + " mc =" + PutIntoQuotes("") + " />";
                                str += "<type>A</type>";
                                str += "<rc>Y</rc>";
                                str += "<lr>Y</lr>";
                                str += "<pfr>N</pfr>";
                                str += "</KUAData>";

                                string url = clsSettings.KUA;



                                String Conn = str;
                                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                                byte[] requestBytes = System.Text.Encoding.ASCII.GetBytes(Conn);
                                req.Method = "POST";
                                req.ContentType = "text/xml";
                                req.ContentLength = requestBytes.Length;
                                Stream requestStream = req.GetRequestStream();
                                requestStream.Write(requestBytes, 0, requestBytes.Length);
                                requestStream.Close();
                                //HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                                string responseXml = "";
                                String OtpErrorCode = "", OtpRet = "";
                                using (HttpWebResponse res = (HttpWebResponse)req.GetResponse())
                                {
                                    // Do your processings here....
                                    StreamReader sr = new StreamReader(res.GetResponseStream());
                                    responseXml = sr.ReadToEnd();
                                    XmlDocument xml = new XmlDocument();
                                    xml.LoadXml(responseXml); // suppose that myXmlString contains "<Names>...</Names>"
                                    XmlNodeList xdocls = xml.SelectNodes("/KuaRes");

                                    string AdharDataLog = "";

                                    foreach (XmlNode xNode in xdocls)
                                    {
                                        OtpRet = xNode.Attributes["ret"].InnerText;
                                        if (xNode.Attributes["err"] != null)
                                            OtpErrorCode = xNode.Attributes["err"].InnerText;

                                        AdharDataLog += "\n" + CommonUtility.XMLToString(xNode, 1);
                                    }

                                    try
                                    {

                                        XmlNodeList xnList = xml.SelectNodes("/KuaRes/UidData");

                                        foreach (XmlNode xn in xnList)
                                        {
                                            AdharDataLog += "\n" + CommonUtility.XMLToString(xn, 1);

                                            foreach (XmlNode Cxn in xn)
                                            {
                                                if (Cxn.Name == "Poi")
                                                {

                                                    if (Cxn.Attributes["name"] != null)
                                                        if (name.Trim().Length == 0)
                                                            name = Cxn.Attributes["name"].InnerText;
                                                }
                                                if (Cxn.Name == "LData")
                                                {
                                                    if (Cxn.Attributes["name"] != null)
                                                        if (RegisterNameMr.Trim().Length == 0)
                                                            RegisterNameMr = Cxn.Attributes["name"].InnerText;
                                                }
                                                if (Cxn.Name == "Pht")
                                                {
                                                    Pht = Cxn.InnerText;
                                                    if (Pht.Length > 0)
                                                    {
                                                        //byte[] bytes = Convert.FromBase64String(Pht);

                                                    }
                                                }
                                            }

                                        }
                                    }
                                    catch { }


                                    ErrorLogModel err = new ErrorLogModel();
                                    err.ErrorTitle = "FarmerLogin Adhar BtnLoginClick";
                                    err.ProjectName = "POCRA WEBSITE";
                                    err.ErrorDescription = "UID : " + UID + "\n , Input :" + str + " \n ,Response : " + responseXml + " \n AdharDataLog" + AdharDataLog;
                                    err.ErrorSeverity = (int)ErrorSeverity.Information;
                                    new ErrorLogManager().InsertErrorLog(err);
                                }

                                if (OtpRet.Trim().ToUpper() == "Y")
                                {
                                    txtPassword.Value = cla.GetExecuteScalar("SELECT  UPass FROM Tbl_M_LoginDetails where UserName='" + txtname.Value.Trim() + "'");
                                }
                                else
                                {
                                    String error = UidiErrorCodes.GetErrorDetails(OtpErrorCode.Trim());
                                    Util.ShowMessageBox(this.Page, "Error", error, "error");
                                    return;

                                }
                                */
                                #endregion

                                //----------------------------------------------------------------------------------

                                //txn
                                //txtname.Value
                                //Shareddbtapi/Aadhar/AuthrizedUsingOtp
                                string adhar = txtname.Value;
                                string otp = txtPassword.Value.Trim();
                                AdharOTP_Response obj = new AdharVaultAPICalls().VerifyAadharOTP(adhar, txn, otp);


                                string Status = obj.MessageCode;

                                if (Status == "1")
                                {
                                    txtPassword.Value = cla.GetExecuteScalar("SELECT  UPass FROM Tbl_M_LoginDetails where UserName='" + txtname.Value.Trim() + "'");
                                }
                                else
                                {
                                    String error = obj.Message;
                                    Util.ShowMessageBox(this.Page, "Error", error, "error");
                                    return;
                                }


                            }
                            else if (RadioButtonList1.SelectedValue.Trim() == "Biometric")
                            {
                                #region "in case of otp"
                                //String strxml ="<PidOptions ver="+PutIntoQuotes("1.0") +"> <Opts fCount="+PutIntoQuotes("1")+"  format=" + PutIntoQuotes("0") + " pidVer="2.0" timeout="20000" env="PP" wadh="RZ+k4w9ySTzOibQdDHPzCFqrKScZ74b3EibKYy1WyGw=" posh="UNKNOWN" />";
                                //strxml +="</PidOptions>";
                                #endregion
                            }
                        }

                        if (name.Trim().Length > 0)
                        {
                            string AdharPhoto = "https://dbtpocradata.blob.core.windows.net/admintrans/DocMasters/Adhar/Photo/" + txtname.Value.Trim() + "/ProfileImage.jpg";

                            cla.ExecuteCommand("UPDATE Tbl_M_RegistrationDetails SET RegisterName='" + name + "' , RegisterNameMr=N'" + RegisterNameMr + "',AdharPhoto='" + AdharPhoto + "' where RegistrationID=" + cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString() + "  ");
                        }

                        if (txtPassword.Value.Trim() == cla.Ds.Tables[0].Rows[0]["UPass"].ToString())
                        {
                            Session["LoginAs"] = cla.Ds.Tables[0].Rows[0]["LoginAs"].ToString();
                            Session["UserId"] = cla.Ds.Tables[0].Rows[0]["UserId"].ToString();
                            Session["RegistrationID"] = cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString();
                            Session["Comp_ID"] = "1";
                            Session["Lang"] = "mr-IN";
                            Session["UserRoleID"] = cla.Ds.Tables[0].Rows[0]["UserRoleID"].ToString();
                            Session["UsersName"] = cla.GetExecuteScalar("SELECT  RegisterName FROM Tbl_M_RegistrationDetails where RegistrationID=" + Session["RegistrationID"].ToString() + ""); //cla.Ds.Tables[0].Rows[0]["FullName"].ToString();
                            Session["BeneficiaryTypesID"] = cla.GetExecuteScalar("SELECT  BeneficiaryTypesID FROM Tbl_M_RegistrationDetails where RegistrationID=" + Session["RegistrationID"].ToString() + "");

                            if (cla.Ds.Tables[0].Rows[0]["Desig_ID"].ToString() != "")
                            {

                                string DesigId = cla.Ds.Tables[0].Rows[0]["Desig_ID"].ToString();
                                if (DesigId != "")
                                {
                                    Session["LevelType_ID"] = cla.GetExecuteScalar("select LevelType_ID from Tbl_M_Designation where Desig_ID=" + DesigId + "");
                                }
                            }
                            else
                            {
                                Session["LevelType_ID"] = "";
                            }
                            string strURL = "";

                            //UpdatePhoto(Pht, cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString());


                            NPCIClass.UpdateAdharStatusDetails(cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString());
                            String IsMobileVerifyed = cla.GetExecuteScalar("SELECT  IsMobileVerifyed FROM Tbl_M_RegistrationDetails where RegistrationID=" + Session["RegistrationID"].ToString() + "");
                            if (IsMobileVerifyed.Trim().Length == 0)
                            {
                                strURL = "~/UsersTrans/MobileVerification.aspx";
                            }
                            else
                            {
                                strURL = "~/UsersTrans/UserDashBoard.aspx";
                            }



                            //An adversary can hijack user sessions by session fixation
                            string guid = Guid.NewGuid().ToString();
                            Session["AuthToken"] = guid;
                            // now create a new cookie with this guid value  
                            Response.Cookies.Add(new HttpCookie("AuthToken", guid));


                            if (strURL.Length > 0)
                            {
                                ErrorLogModel err = new ErrorLogModel();
                                err.ErrorTitle = "Farmer Logged From Regular Farmer Portal";
                                err.ProjectName = "POCRA WEBSITE";
                                err.ErrorDescription = "Logged Through : " + txtname.Value.Trim();
                                err.ErrorSeverity = (int)ErrorSeverity.Information;
                                new ErrorLogManager().InsertErrorLog(err);

                                Response.Redirect(strURL, false);
                            }
                            else
                            {
                                clsMessages.Errormsg(Literal2, "Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator.");
                            }

                        }
                        else
                        {
                            clsMessages.Errormsg(Literal2, "Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator.");
                        }
                    }
                    else
                    {
                        clsMessages.Errormsg(Literal2, "आपण प्रकल्पामध्ये नोंदणी केलेली नाही.");
                    }
                }
                catch (Exception ex)
                {
                    Util.LogError(ex);
                    //Label1.Text = ex.ToString();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MsgBox", "<script>alert('Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator !!!')</script>", false);
                }
            }


        }

        private static void UpdatePhoto(String BaseImge, String RegistrationID)
        {
            //Img
            try
            {
                if (BaseImge.Trim().Length > 0)
                {
                    byte[] imageBytes = Convert.FromBase64String(BaseImge);
                    AzureBlobHelper fileRet = new AzureBlobHelper();
                    String PathUp = "DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "";
                    string imageName = "ProfileImage.jpg";// + MyCommanClassAPI.GetFileExtension(BaseImge);
                    String ret = fileRet.UploadData(PathUp, imageName, imageBytes);
                }
            }
            catch { }
        }

        #region Adhar Biometric using Ajax         
        [WebMethod]
        public static string CallSessionVariables(String result, String ANumber)
        {
            try
            {
                Convert.ToDouble(ANumber);
            }
            catch
            {

                return "Invalid Input";
            }
            String ret = "", OtpErrorCode = "";
            String Pht = "";
            try
            {

                XmlDocument xdocl = new XmlDocument();
                xdocl.LoadXml(result);
                String Skey = "", Data = "", Hmac = "", rdsId = "", rdsVer = "", dpId = "", dc = "", mi = "", mc = "", OtpRet = "", keyExp = "";

                XmlNodeList xResp = xdocl.SelectNodes("/PidData/Resp");
                foreach (XmlNode xNode in xResp)
                {
                    OtpRet = xNode.Attributes["errInfo"].InnerText;

                }

                if (OtpRet.Trim() == "Device not ready")
                {
                    return "";
                }

                foreach (XmlNode xNode in xdocl)
                {

                    foreach (XmlNode CNode in xNode)
                    {

                        if (CNode.Name.Trim() == "Skey")
                        {
                            Skey = CNode.InnerText;
                            try
                            {
                                keyExp = CNode.Attributes["ci"].InnerText;//20250923
                            }
                            catch { }
                        }
                        //if (CNode.Name.Trim() == "Skey")
                        //{
                        //    Skey = CNode.InnerText;
                        //}
                        if (CNode.Name.Trim() == "Data")
                        {
                            Data = CNode.InnerText;
                        }
                        if (CNode.Name.Trim() == "Hmac")
                        {
                            Hmac = CNode.InnerText;
                        }
                    }
                }
                XmlNodeList xnDeviceInfo = xdocl.SelectNodes("/PidData/DeviceInfo");
                foreach (XmlNode Cxn in xnDeviceInfo)
                {

                    if (Cxn.Attributes["dpId"] != null)
                        dpId = Cxn.Attributes["dpId"].InnerText;

                    if (Cxn.Attributes["rdsId"] != null)
                        rdsId = Cxn.Attributes["rdsId"].InnerText;

                    if (Cxn.Attributes["rdsVer"] != null)
                        rdsVer = Cxn.Attributes["rdsVer"].InnerText;

                    if (Cxn.Attributes["dc"] != null)
                        dc = Cxn.Attributes["dc"].InnerText;

                    if (Cxn.Attributes["mi"] != null)
                        mi = Cxn.Attributes["mi"].InnerText;

                    if (Cxn.Attributes["mc"] != null)
                        mc = Cxn.Attributes["mc"].InnerText;

                }


                String UID = ANumber.Trim();
                String s = new MyClass().GetSqlUnikNO("3");
                String Txn = "UKC:" + s + DateTime.Now.ToString("yyyyMMddHHmmssfff");

                clsCrypto cls = new global::clsCrypto();
                String Password = "testndks@123";

                byte[] key = MOLCryptoEngine.MOLSecurity.GenerateKey(256);
                byte[] IV = MOLSecurity.GenerateIV(128, "uidaimaharashtra");
                string EncryptedUID = MOLSecurity.AESEncrypt(UID, IV, key);// Convert.ToBase64String(encrypted);
                string PasswordUID = MOLSecurity.AESEncrypt(Password, IV, key);// Convert.ToBase64String(encryptedPassword);
                String Certificate = System.Web.Hosting.HostingEnvironment.MapPath("~/AdminTrans/UserControls/Production/JME.cer");
                string incodedkey = Convert.ToBase64String(key);
                String AUASkey = MOLSecurity.EncryptWithPublicKey(incodedkey, Certificate);// EncryptCertificate(Encoding.ASCII.GetBytes(incodedkey), cert.GetRawCertData());
                string HA256KeyValue = cls.generateSha256Hash(incodedkey);
                string HA256saTxn = cls.generateSha256Hash(Txn);
                String aa = PasswordUID + "~" + EncryptedUID + "~" + HA256KeyValue + "~" + HA256saTxn;
                aa = aa.ToLower();

                String Token = cls.generateSha256Hash(aa);// ComputeSha256Hash(aa);





                String str = "<KUAData xmlns=" + MyCommanClass.PutIntoQuotes("http://kua.maharashtra.gov.in/kyc/gom-kyc-request") + ">";
                str += "<uid>" + EncryptedUID + "</uid>";
                str += "<appCode>KYCApp</appCode>";
                str += "<Token>" + Token.Trim() + "</Token>";
                str += "<KUASkey>" + AUASkey.Trim() + "</KUASkey>";
                str += "<sa>PNDKS23054</sa>";
                str += "<saTxn>" + Txn + "</saTxn>";
                str += "<Data type =" + MyCommanClass.PutIntoQuotes("X") + ">" + Data + "</Data>";
                str += "<Hmac>" + Hmac + "</Hmac>";
                //str += "<Skey ci =" + MyCommanClass.PutIntoQuotes("20191230") + ">" + Skey + "</Skey>";
                //str += "<Skey ci =" + MyCommanClass.PutIntoQuotes("20250923") + ">" + Skey + "</Skey>";
                if (keyExp.Trim() == "20191230")
                {
                    str += "<Skey ci =" + MyClass.PutIntoQuotes("20191230") + ">" + Skey + "</Skey>";
                }
                else
                {
                    str += "<Skey ci =" + MyClass.PutIntoQuotes("20250923") + ">" + Skey + "</Skey>";
                }
                str += "<Uses pi =" + MyCommanClass.PutIntoQuotes("n") + " pa =" + MyCommanClass.PutIntoQuotes("n") + " pfa =" + MyCommanClass.PutIntoQuotes("n") + "  bio =" + MyCommanClass.PutIntoQuotes("y") + " bt=" + MyCommanClass.PutIntoQuotes("FMR,FIR") + " pin=" + MyCommanClass.PutIntoQuotes("n") + " otp=" + MyCommanClass.PutIntoQuotes("n") + "/>";
                str += "<Meta rdsId =" + MyCommanClass.PutIntoQuotes(rdsId) + " rdsVer =" + MyCommanClass.PutIntoQuotes(rdsVer) + " dpId =" + MyCommanClass.PutIntoQuotes(dpId) + " dc =" + MyCommanClass.PutIntoQuotes(dc) + " mi =" + MyCommanClass.PutIntoQuotes(mi) + " mc =" + MyCommanClass.PutIntoQuotes(mc) + " />";
                str += "<type>A</type>";
                str += "<rc>Y</rc>";
                str += "<lr>Y</lr>";
                str += "<pfr>N</pfr>";
                str += "</KUAData>";



                //string url = "https://kuaqa.maharashtra.gov.in/KUA/rest/kycreq";// 
                //string url = "https://kua25.maharashtra.gov.in/kua25/KUA/rest/kycreq";
                string url = clsSettings.KUA;

                String Conn = str;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                byte[] requestBytes = System.Text.Encoding.ASCII.GetBytes(Conn);
                req.Method = "POST";
                req.ContentType = "text/xml";
                req.ContentLength = requestBytes.Length;
                Stream requestStream = req.GetRequestStream();
                requestStream.Write(requestBytes, 0, requestBytes.Length);
                requestStream.Close();
                //HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                string responseXml = "";
                using (HttpWebResponse res = (HttpWebResponse)req.GetResponse())
                {
                    // Do your processings here....
                    StreamReader sr = new StreamReader(res.GetResponseStream());
                    responseXml = sr.ReadToEnd();
                    res.Dispose();

                }


                XmlDocument xml = new XmlDocument();
                xml.LoadXml(responseXml); // suppose that myXmlString contains "<Names>...</Names>"
                XmlNodeList xdocls = xml.SelectNodes("/KuaRes");
                foreach (XmlNode xNode in xdocls)
                {
                    OtpRet = xNode.Attributes["ret"].InnerText;
                    if (xNode.Attributes["err"] != null)
                        OtpErrorCode = xNode.Attributes["err"].InnerText;
                }

                String name = "";
                String RegisterNameMr = "";
                try
                {

                    XmlNodeList xnList = xml.SelectNodes("/KuaRes/UidData");
                    foreach (XmlNode xn in xnList)
                    {
                        foreach (XmlNode Cxn in xn)
                        {
                            if (Cxn.Name == "Poi")
                            {

                                if (Cxn.Attributes["name"] != null)
                                    if (name.Trim().Length == 0)
                                        name = Cxn.Attributes["name"].InnerText;
                            }
                            if (Cxn.Name == "LData")
                            {

                                if (Cxn.Attributes["name"] != null)
                                    if (RegisterNameMr.Trim().Length == 0)
                                        RegisterNameMr = Cxn.Attributes["name"].InnerText;
                            }
                            if (Cxn.Name == "Pht")
                            {
                                Pht = Cxn.InnerText;
                                if (Pht.Length > 0)
                                {

                                }
                            }
                        }

                    }

                }
                catch { }

                if (OtpRet.Trim().ToUpper() == "Y")
                {

                    MyClass cla = new MyClass();
                    String txtPassword = cla.GetExecuteScalar("SELECT  UPass FROM Tbl_M_LoginDetails where UserName='" + UID.Trim() + "'");

                    cla.OpenReturenDs("SELECT top 1 UserId, UserName, UPass, FullName, RegistrationID , LoginAs ,UserRoleID,Desig_ID FROM Tbl_M_LoginDetails WHERE (IsDeleted is null) AND (Username = '" + UID.Trim() + "') order by UserId desc ");
                    if (cla.Ds.Tables[0].Rows.Count > 0)
                    {

                        if (name.Trim().Length > 0)
                            cla.ExecuteCommand("UPDATE Tbl_M_RegistrationDetails SET RegisterName='" + name + "', RegisterNameMr=N'" + RegisterNameMr + "' where RegistrationID=" + cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString() + "  ");


                        if (txtPassword.Trim() == cla.Ds.Tables[0].Rows[0]["UPass"].ToString())
                        {
                            HttpContext.Current.Session["LoginAs"] = cla.Ds.Tables[0].Rows[0]["LoginAs"].ToString();
                            HttpContext.Current.Session["UserId"] = cla.Ds.Tables[0].Rows[0]["UserId"].ToString();
                            HttpContext.Current.Session["RegistrationID"] = cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString();
                            HttpContext.Current.Session["Comp_ID"] = "1";
                            HttpContext.Current.Session["Lang"] = "en-IN";
                            HttpContext.Current.Session["UserRoleID"] = cla.Ds.Tables[0].Rows[0]["UserRoleID"].ToString();
                            HttpContext.Current.Session["UsersName"] = cla.GetExecuteScalar("SELECT  RegisterName FROM Tbl_M_RegistrationDetails where RegistrationID=" + HttpContext.Current.Session["RegistrationID"].ToString() + ""); //cla.Ds.Tables[0].Rows[0]["FullName"].ToString();
                            HttpContext.Current.Session["BeneficiaryTypesID"] = cla.GetExecuteScalar("SELECT  BeneficiaryTypesID FROM Tbl_M_RegistrationDetails where RegistrationID=" + HttpContext.Current.Session["RegistrationID"].ToString() + "");



                            if (cla.Ds.Tables[0].Rows[0]["Desig_ID"].ToString() != "")
                            {

                                string DesigId = cla.Ds.Tables[0].Rows[0]["Desig_ID"].ToString().Trim();
                                if (DesigId != "")
                                {
                                    HttpContext.Current.Session["LevelType_ID"] = cla.GetExecuteScalar("select LevelType_ID from Tbl_M_Designation where Desig_ID=" + DesigId + "");
                                }
                            }
                            else
                            {
                                ErrorLogModel err = new ErrorLogModel();
                                err.ErrorTitle = "CallSessionVariables ,FarmerLogin.aspx : Bug";
                                err.ProjectName = "POCRA WEBSITE";
                                err.ErrorDescription = "UserId :" + HttpContext.Current.Session["UserId"] + " , \n DesigId not available";
                                err.ErrorSeverity = (int)ErrorSeverity.Medium;
                                new ErrorLogManager().InsertErrorLog(err);

                                HttpContext.Current.Session["LevelType_ID"] = "";
                            }



                            //An adversary can hijack user sessions by session fixation
                            string guid = Guid.NewGuid().ToString();
                            HttpContext.Current.Session["AuthToken"] = guid;
                            // now create a new cookie with this guid value  
                            HttpContext.Current.Response.Cookies.Add(new HttpCookie("AuthToken", guid));
                            ret = "";

                            UpdatePhoto(Pht, cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString());

                        }
                        else
                        {
                            ret = "Either you have not registered with Pocra or Your Account has been deactivated by the Administrator..";

                        }

                    }
                    else
                    {
                        ret = "Either you have not registered with Pocra or Your Account has been deactivated by the Administrator.";

                    }

                }
                else
                {
                    ret = UidiErrorCodes.GetErrorDetails(OtpErrorCode.Trim());



                }


            }
            catch (Exception ex)
            {
                ret = ex.ToString();  //Session["error"]


            }

            return ret;

        }
        #endregion



        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Literal2.Text = "";
            btnLogin.Visible = true;
            if (RadioButtonList1.SelectedValue.Trim() == "OTP")
            {
                //divotp.Visible = true;
                txtPassword.Visible = false;
                Label3.Text = "";
                btnbio.Visible = false;
                btnOtp.Visible = true;
                divBioType.Visible = false;
            }
            else if (RadioButtonList1.SelectedValue.Trim() == "Biometric")
            {
                btnLogin.Visible = false;
                btnOtp.Visible = false;
                txtPassword.Visible = false;
                Label3.Text = "";
                btnbio.Visible = true;
                divBioType.Visible = true;
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> RunBiometric();  </script>", false);
            }
        }

        protected void btnOtp_Click(object sender, EventArgs e)
        {

            try
            {
                Convert.ToDouble(txtname.Value);
            }
            catch
            {
                Util.ShowMessageBox(this.Page, "Error", "Invalid Input", "error");
                return;
            }

            cla.OpenReturenDs("SELECT top 1 UserId, UserName, UPass, FullName, RegistrationID , LoginAs ,UserRoleID,Desig_ID FROM Tbl_M_LoginDetails WHERE (IsDeleted is null) AND (Username = '" + txtname.Value.Trim() + "') order by UserId desc ");

            if (cla.Ds.Tables[0].Rows.Count > 0)
            {
                Literal2.Text = "";
                txtPassword.Visible = true;
                if (btnMarathi.Text.Trim() == "मराठी")
                {
                    Label3.Text = "OTP";
                }
                else
                {
                    Label3.Text = "ओटीपी";
                }

                try
                {
                    String s = new MyClass().GetSqlUnikNO("3");
                    ViewState["Txn"] = "" + s + DateTime.Now.ToString("yyyyMMddHHmmssfff");// "2018103130051212";

                    SendForAuthentication();
                }
                catch (Exception ex)
                {
                    btnOtp.Visible = true;
                    txtPassword.Visible = false;
                    Util.ShowMessageBox(this.Page, "Error", "Response is not coming from uidai kindly try again::" + ex.Message, "error");
                }

            }
            else
            {
                clsMessages.Errormsg(Literal2, "आपण प्रकल्पामध्ये नोंदणी केलेली नाही.");
            }

        }
        public string PutIntoQuotes(string value)
        {
            return "\"" + value + "\"";
        }
        public void SendForAuthentication()
        {
            String Txn = ViewState["Txn"].ToString().Trim();

            string UID = txtname.Value.Trim();

            txtPassword.Visible = false;
            btnOtp.Visible = false;


            AdharOTP_Response obj = new AdharVaultAPICalls().GetAdharOTP(UID);


            if (obj.MessageCode == "0")
            {
                string error = obj.Message;
                btnOtp.Visible = true;
                Util.ShowMessageBox(this.Page, "Error", error, "error");
                //btnLogin.Visible = true;
            }
            else
            {
                txtPassword.Visible = true;
                btnOtp.Visible = false;
                string error = obj.Message;
                string txn = obj.Txn;
                Session["AdharTxn"] = txn;
                btnLogin.Visible = true;
                Util.ShowMessageBox(this.Page, "Success", error, "success");
            }
        }

        private void LoadString()
        {
            RadioButtonList1.Visible = false;
            RadioButtonList2.Visible = true;

            Label3.Text = "पासवर्ड";
            //if (Request.QueryString["D"].ToString().Trim() == "1")
            //{
            Literal3.Text = " शेतकरी लॉग इन ";
            Label2.Text = "आधार क्रमांक";
            //Label3.Text = "ओटीपी";
            //}

            btnOtp.Text = "ओटीपी पाठवा";
            btnLogin.Text = "लॉग इन";

            //Literal1.Text = "<p class='forget-pass text-white'>आपण आपला पासवर्ड विसरलात का? ? <a href='#'style='text-decoration: underline'>रीसेट करा </a></p>";
        }


        protected void btnMarathi_Click(object sender, EventArgs e)
        {

            LinkButton btn = (LinkButton)sender;
            if (btn.Text.Trim() == "मराठी")
            {
                Session["Lang"] = "mr-IN";
                btn.Text = "English";
                LoadString();
            }
            else
            {
                Session["Lang"] = "en-IN";
                btn.Text = "मराठी";
                string pageName = "UserLogin.aspx";
                String qstring = this.Page.ClientQueryString.ToString();
                if (qstring.Length > 0)
                    pageName = pageName + "?" + qstring;
                Response.Redirect("~/" + pageName);
            }

        }


        [WebMethod]
        public static string LogIP(string IPDetails)
        {

            if (IPDetails != "")
            {
                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "FarmerLogin IP Log";
                err.ProjectName = "POCRA WEBSITE";
                err.ErrorDescription = IPDetails;
                err.ErrorSeverity = (int)ErrorSeverity.Information;
                new ErrorLogManager().InsertErrorLog(err);
            }

            return "Success";
        }


    }
}