using CommanClsLibrary;
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

namespace DBTPoCRA
{

    public partial class AdharTestAPI : System.Web.UI.Page
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
                    //    //ddlUserType.SelectedValue = Request.QueryString["T"].Trim();
                    //    //UpdateLoginType(Request.QueryString["T"].Trim());
                    //    Literal3.Text = "Individual LOGIN ";
                    //    RadioButtonList1_SelectedIndexChanged(sender, null);
                    //    if (Session["Lang"] != null)
                    //    {
                    //        if (Session["Lang"].ToString().Trim() == "mr-IN")
                    //        {
                    //            btnMarathi.Text = "English";
                    //            LoadString();
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    Response.Redirect("Default.aspx", false);
                    //}


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

            Literal2.Text = "";
            String Pht = "";
            try
            {
                Boolean CanLogin = false;
                if (Request.QueryString["D"].Trim() == "1")
                {
                    if (RadioButtonList1.SelectedValue.Trim() == "OTP")
                    {
                        // in case of otp
                        

                        //----------------------------------------------------------------------------------

                    }
                    else if (RadioButtonList1.SelectedValue.Trim() == "Boimetric")
                    {
                        #region "in case of otp"
                        //String strxml ="<PidOptions ver="+PutIntoQuotes("1.0") +"> <Opts fCount="+PutIntoQuotes("1")+"  format=" + PutIntoQuotes("0") + " pidVer="2.0" timeout="20000" env="PP" wadh="RZ+k4w9ySTzOibQdDHPzCFqrKScZ74b3EibKYy1WyGw=" posh="UNKNOWN" />";
                        //strxml +="</PidOptions>";

                        #endregion
                    }
                }


                cla.OpenReturenDs("SELECT top 1 UserId, UserName, UPass, FullName, RegistrationID , LoginAs ,UserRoleID,Desig_ID FROM Tbl_M_LoginDetails WHERE (IsDeleted is null) AND (Username = '" + txtname.Value.Trim() + "') order by UserId desc ");
                if (cla.Ds.Tables[0].Rows.Count > 0)
                {


                    if (txtPassword.Value.Trim() == cla.Ds.Tables[0].Rows[0]["UPass"].ToString())
                    {
                        Session["LoginAs"] = cla.Ds.Tables[0].Rows[0]["LoginAs"].ToString();
                        Session["UserId"] = cla.Ds.Tables[0].Rows[0]["UserId"].ToString();
                        Session["RegistrationID"] = cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString();
                        Session["Comp_ID"] = "1";
                        Session["Lang"] = "mr-IN";
                        Session["UserRoleID"] = cla.Ds.Tables[0].Rows[0]["UserRoleID"].ToString();
                        Session["UsersName"] = cla.Ds.Tables[0].Rows[0]["FullName"].ToString();
                        Session["BeneficiaryTypesID"] = cla.GetExecuteScalar("SELECT  BeneficiaryTypesID FROM Tbl_M_RegistrationDetails where RegistrationID=" + Session["RegistrationID"].ToString() + "");
                        Session["LevelType_ID"] = cla.GetExecuteScalar("select LevelType_ID from Tbl_M_Designation where Desig_ID=" + cla.Ds.Tables[0].Rows[0]["Desig_ID"].ToString() + "");
                        string strURL = "";

                       


                     


                        strURL = "~/UsersTrans/UserDashBoard.aspx";

                        //An adversary can hijack user sessions by session fixation
                        string guid = Guid.NewGuid().ToString();
                        Session["AuthToken"] = guid;
                        // now create a new cookie with this guid value  
                        Response.Cookies.Add(new HttpCookie("AuthToken", guid));


                        if (strURL.Length > 0)
                            Response.Redirect(strURL, false);
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
                    clsMessages.Errormsg(Literal2, "Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator.");

                }
            }
            catch(Exception ex)
            {
                //Label1.Text = ex.ToString();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MsgBox", "<script>alert('Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator !!!')</script>", false);
            }
        }

        [WebMethod]
        public static string CallSessionVariables(String result, String ANumber)
        {
            String ret = "", OtpErrorCode = "";
            String Pht = "";
            try
            {
                

                clsCrypto cls = new global::clsCrypto();
                String Password = "testndks@123";
                string UID = "979387549678";
                byte[] key = MOLCryptoEngine.MOLSecurity.GenerateKey(256);
                byte[] IV = MOLSecurity.GenerateIV(128, "uidaimaharashtra");
                string EncryptedUID = MOLSecurity.AESEncrypt(UID, IV, key);// Convert.ToBase64String(encrypted);
                string PasswordUID = MOLSecurity.AESEncrypt(Password, IV, key);// Convert.ToBase64String(encryptedPassword);
                String Certificate = System.Web.Hosting.HostingEnvironment.MapPath("~/AdminTrans/UserControls/Production/JME.cer");
                string incodedkey = Convert.ToBase64String(key);
                String AUASkey = MOLSecurity.EncryptWithPublicKey(incodedkey, Certificate);// EncryptCertificate(Encoding.ASCII.GetBytes(incodedkey), cert.GetRawCertData());
                string HA256KeyValue = cls.generateSha256Hash(incodedkey);
                String Txn = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string HA256saTxn = cls.generateSha256Hash(Txn);



                String aa = PasswordUID + "~" + EncryptedUID + "~" + HA256KeyValue + "~" + HA256saTxn;
                aa = aa.ToLower();

                String Token = cls.generateSha256Hash(aa);// ComputeSha256Hash(aa);



                XmlDocument xdocl = new XmlDocument();
                xdocl.LoadXml(result);
                String Skey = "", Data = "", Hmac = "", rdsId = "", rdsVer = "", dpId = "", dc = "", mi = "", mc = "", OtpRet = "", keyExp = "";

                XmlNodeList xResp = xdocl.SelectNodes("/PidData/Resp");
                foreach (XmlNode xNode in xResp)
                {
                    OtpRet = xNode.Attributes["errInfo"].InnerText;

                }

                if(OtpRet.Trim()== "Device not ready")
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
                                keyExp = CNode.Attributes["ci"].InnerText;//20221021
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



                //String UID = ANumber.Trim();
                //String Txn = "UKC:" + DateTime.Now.ToString("yyyyMMddHHmmssfff");



                String str = "<KUAData xmlns=" + MyCommanClass.PutIntoQuotes("http://kua.maharashtra.gov.in/kyc/gom-kyc-request") + ">";
                str += "<uid>" + EncryptedUID + "</uid>";
                str += "<appCode>KYCApp</appCode>";
                str += "<Token>" + Token.Trim()+ "</Token>";
                str += "<KUASkey>" + AUASkey.Trim()+ "</KUASkey>";
                str += "<sa>SNDKS23054</sa>";
                str += "<saTxn>" + Txn + "</saTxn>";
                str += "<Data type =" + MyCommanClass.PutIntoQuotes("X") + ">" + Data + "</Data>";
                str += "<Hmac>" + Hmac + "</Hmac>";
                //str += "<Skey ci =" + MyCommanClass.PutIntoQuotes("20191230") + ">" + Skey + "</Skey>";
                //str += "<Skey ci =" + MyCommanClass.PutIntoQuotes("20221021") + ">" + Skey + "</Skey>";
                if (keyExp.Trim() == "20191230")
                {
                    str += "<Skey ci =" + MyClass.PutIntoQuotes("20191230") + ">" + Skey + "</Skey>";
                }
                else
                {
                    str += "<Skey ci =" + MyClass.PutIntoQuotes("20221021") + ">" + Skey + "</Skey>";
                }
                str += "<Uses pi =" + MyCommanClass.PutIntoQuotes("n") + " pa =" + MyCommanClass.PutIntoQuotes("n") + " pfa =" + MyCommanClass.PutIntoQuotes("n") + "  bio =" + MyCommanClass.PutIntoQuotes("y") + " bt=" + MyCommanClass.PutIntoQuotes("FMR") + " pin=" + MyCommanClass.PutIntoQuotes("n") + " otp=" + MyCommanClass.PutIntoQuotes("n") + "/>";
                str += "<Meta rdsId =" + MyCommanClass.PutIntoQuotes(rdsId) + " rdsVer =" + MyCommanClass.PutIntoQuotes(rdsVer) + " dpId =" + MyCommanClass.PutIntoQuotes(dpId) + " dc =" + MyCommanClass.PutIntoQuotes(dc) + " mi =" + MyCommanClass.PutIntoQuotes(mi) + " mc =" + MyCommanClass.PutIntoQuotes(mc) + " />";
                str += "<type>A</type>";
                str += "<rc>Y</rc>";
                str += "<lr>Y</lr>";
                str += "<pfr>N</pfr>";
                str += "</KUAData>";



               
                string url = "https://kuaqa.maharashtra.gov.in/kua25/KUA/rest/kycreq";//;

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
                    StreamReader sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.Default);
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


                try
                {
                    
                    XmlNodeList xnList = xml.SelectNodes("/KuaRes/UidData");
                    foreach (XmlNode xn in xnList)
                    {
                        foreach (XmlNode Cxn in xn)
                        {
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


    


        

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Literal2.Text = "";
            if (RadioButtonList1.SelectedValue.Trim() == "OTP")
            {
                //divotp.Visible = true;
                txtPassword.Visible = false;
                Label3.Text = "";
                btnbio.Visible = false;
                btnOtp.Visible = true;
                divBioType.Visible = false;
            }
            else if (RadioButtonList1.SelectedValue.Trim() == "Boimetric")
            {
                btnOtp.Visible = false;
                txtPassword.Visible = false;
                Label3.Text = "";
                btnbio.Visible = true;
                divBioType.Visible = true;
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> Test();  </script>", false);
            }
        }

        protected void btnOtp_Click(object sender, EventArgs e)
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
                //ViewState["Txn"] = "UKC:" + DateTime.Now.ToString("yyyyMMddHHmmssfff");// "2018103130051212";
                ViewState["Txn"] = "" + DateTime.Now.ToString("yyyyMMddHHmmssfff");// "2018103130051212";
                SendForAuthentication();

            }
            catch (Exception ex)
            {
                btnOtp.Visible = true;
                txtPassword.Visible = false;
                Util.ShowMessageBox(this.Page, "Error", "Response is not coming from uidai kindly try again::" + ex.Message, "error");
            }

        }
        public string PutIntoQuotes(string value)
        {
            return "\"" + value + "\"";
        }
        public void SendForAuthentication()
        {

            // SingXML(); "">

            String Txn = ViewState["Txn"].ToString().Trim();

            String str = "<Auth xmlns=" + PutIntoQuotes("http://aua.maharashtra.gov.in/auth/gom-auth-request") + ">";
            str += "<Txn>" + Txn + "</Txn>";
            str += "<Ver>2.5</Ver>";
            str += "<SubAUACode>PNDKS23054</SubAUACode>";
            str += "<ReqType>otp</ReqType>";
            str += "<DeviceId>UDC-AGRIGOM-0001</DeviceId>";
            str += "<UID>" + txtname.Value.Trim() + "</UID>";
            str += "<type>A</type>";
            str += "<Ch>01</Ch>";
            str += "</Auth> ";

            string url = "https://aua25.maharashtra.gov.in/aua25/aua/rest/authreqv2";// "https://auaqa.maharashtra.gov.in/aua/rest/authreqv2";

            //StreamReader sr1 = new StreamReader(Server.MapPath("~/TESTDATA/test-signed.xml").ToString());
            String Conn = str;// sr1.ReadToEnd();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            byte[] requestBytes = System.Text.Encoding.ASCII.GetBytes(Conn);
            req.Method = "POST";
            req.ContentType = "text/xml";
            req.ContentLength = requestBytes.Length;
            Stream requestStream = req.GetRequestStream();
            requestStream.Write(requestBytes, 0, requestBytes.Length);
            requestStream.Close();
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.Default);
            string backstr = sr.ReadToEnd();
            sr.Close();
            res.Close();


            XmlDocument xdocl = new XmlDocument();
            xdocl.LoadXml(backstr);
            String OtpErrorCode = "", OtpRet = "", ResponseMsg = "";

            foreach (XmlNode xNode in xdocl)
            {
                foreach (XmlNode CNode in xNode)
                {
                    if (CNode.Name.Trim() == "OtpErrorCode")
                    {
                        OtpErrorCode = CNode.InnerText;
                    }
                    if (CNode.Name.Trim() == "OtpRet")
                    {
                        OtpRet = CNode.InnerText;
                    }
                    if (CNode.Name.Trim() == "ResponseMsg")
                    {
                        ResponseMsg = CNode.InnerText;
                    }
                }
            }

            txtPassword.Visible = false;
            btnOtp.Visible = false;
            String error = "";
            if (OtpRet.Trim().ToUpper() == "N")
            {
                // error
                if (OtpErrorCode.Trim().Length > 0)
                {

                    if (OtpErrorCode.Trim().Length > 0)
                    {
                        error = UidiErrorCodes.GetErrorDetails(OtpErrorCode.Trim());
                    }
                }
                if (error == null)
                {
                    error = "Response is not coming from uidai kindly try again ::" + OtpErrorCode;
                }
                else if (error.Length == 0)
                {
                    error = "Response is not coming from uidai kindly try again ::" + OtpErrorCode;
                }

                btnOtp.Visible = true;
                Util.ShowMessageBox(this.Page, "Error", error, "error");
            }
            else
            {
                try
                {
                    String[] ss = ResponseMsg.Trim().Split(',');
                    ResponseMsg = ss[6].ToString();

                }
                catch { }

                txtPassword.Visible = true;
                btnOtp.Visible = false;
                error = "OTP send to registered mobile number " + ResponseMsg + "  and will valid for next 5 min.";
                Util.ShowMessageBox(this.Page, "Success", error, "success");
            }



        }
        private void LoadString()
        {
            RadioButtonList1.Visible = false;
            RadioButtonList2.Visible = true;

            Label3.Text = "पासवर्ड";
            if (Request.QueryString["D"].ToString().Trim() == "1")
            {
                Literal3.Text = " शेतकरी लॉग इन ";
                Label2.Text = "आधार क्रमांक";
                Label3.Text = "ओटीपी";

            }

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




     


    }
}