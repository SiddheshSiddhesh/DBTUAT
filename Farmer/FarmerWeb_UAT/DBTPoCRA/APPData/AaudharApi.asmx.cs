using CommanClsLibrary;
using CommanClsLibrary.Repository.Classes;
using CommanClsLibrary.Repository.Models;
using DBTPoCRA.AdminTrans.UserControls;
using MOLCryptoEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Services;
using System.Xml;
using static CommanClsLibrary.Repository.Enums;
namespace DBTPoCRA.APPData
{
    /// <summary>
    /// Summary description for AaudharApi
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AaudharApi : System.Web.Services.WebService
    {
        public string PutIntoQuotes(string value)
        {
            return "\"" + value + "\"";
        }


        [WebMethod]
        public void GetOtp(String SecurityKey, String AudharNumber)
        {

            List<clsAudharMessage> lst = new List<clsAudharMessage>();

            if (MyCommanClass.CheckApiAuthrization("86", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {

                clsAudharMessage d = new clsAudharMessage();
                d.Message = "Authorization Failed";
                d.MessageCode = "0";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Newtonsoft.Json.Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Newtonsoft.Json.Formatting.Indented).ToString());
                return;
            }
            try
            {
                String Error = "", Txn = "";
                clsAudharMessage d = new clsAudharMessage();
                try
                {

                    Txn = "" + DateTime.Now.ToString("yyyyMMddHHmmssfff");// "2018103130051212";
                    d.Txn = Txn;

                    clsCrypto cls = new global::clsCrypto();
                    String Password = "testndks@123";
                    string UID = AudharNumber.Trim();
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




                    String str = "<Auth xmlns=" + PutIntoQuotes("http://aua.maharashtra.gov.in/auth/gom-auth-request") + ">";
                    str += "<Txn>" + Txn + "</Txn>";
                    str += "<Ver>2.5</Ver>";
                    str += "<SubAUACode>PNDKS23054</SubAUACode>";
                    str += "<AUAToken>" + Token.Trim() + "</AUAToken>";
                    str += "<AUASkey>" + AUASkey.Trim() + "</AUASkey>";
                    str += "<ReqType>otp</ReqType>";
                    str += "<DeviceId>UDC-AGRIGOM-0001</DeviceId>";
                    str += "<UID>" + EncryptedUID + "</UID>";
                    str += "<type>A</type>";
                    str += "<Ch>01</Ch>";
                    str += "</Auth> ";

                    //string url = "https://aua25.maharashtra.gov.in/aua25/aua/rest/authreqv2";// "https://auaqa.maharashtra.gov.in/aua/rest/authreqv2";
                    string url = clsSettings.AUA;
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
                    StreamReader sr = new StreamReader(res.GetResponseStream());
                    string backstr = sr.ReadToEnd();
                    sr.Close();
                    res.Close();

                    //Util.LogErrorAadhar(str + " APP .............................." + backstr);

                    ErrorLogModel err = new ErrorLogModel();
                    err.ErrorTitle = "Farmer Adhar Login Log";
                    err.ProjectName = "POCRA AaudharAPI.asmx";
                    err.ErrorDescription = "AudharNumber : " + AudharNumber + " \n, Input :" + str + " \n ,Response : " + backstr;
                    err.ErrorSeverity = (int)ErrorSeverity.Information;
                    new ErrorLogManager().InsertErrorLog(err);

                    XmlDocument xdocl = new XmlDocument();
                    xdocl.LoadXml(backstr);
                    String OtpErrorCode = "", OtpRet = "";

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
                        }
                    }


                    String error = "";
                    if (OtpRet.Trim().ToUpper() == "N")
                    {
                        // error
                        if (OtpErrorCode.Trim().Length > 0)
                        {
                            error = UidiErrorCodes.GetErrorDetails(OtpErrorCode.Trim());
                        }
                        d.Message = error;
                        d.MessageCode = "0";
                    }
                    else
                    {

                        error = "OTP send to registered mobile number and will valid for next 5 min.";
                        d.Message = error;
                        d.MessageCode = "1";
                    }
                }
                catch (Exception ex)
                {

                    Error = "Response is not coming from uidai kindly try again  :: " + ex.Message;
                    d.Message = Error;
                    d.MessageCode = "0";
                }


                lst.Add(d);

            }
            catch (Exception ex)
            {
                clsAudharMessage d = new clsAudharMessage();
                d.Message = ex.ToString();
                d.MessageCode = "0";
                lst.Add(d);
            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Newtonsoft.Json.Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Newtonsoft.Json.Formatting.Indented).ToString());




        }


        [WebMethod]
        public void AuthrizedUsingOtp(String SecurityKey, String AudharNumber, String Txn, String Otp)
        {

            List<clsAudharMessage> lst = new List<clsAudharMessage>();

            if (MyCommanClass.CheckApiAuthrization("87", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {

                clsAudharMessage d = new clsAudharMessage();
                d.Message = "Authorization Failed";
                d.MessageCode = "0";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Newtonsoft.Json.Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Newtonsoft.Json.Formatting.Indented).ToString());
                return;
            }
            try
            {
                String Error = "", MessageExp = "";
                clsAudharMessage d = new clsAudharMessage();
                try
                {



                    //string keyPath = Server.MapPath("~/admintrans/UserControls/uidai_auth_encrypt_preprod.cer");
                    string keyPath = Server.MapPath("~/admintrans/UserControls/Production/uidai_auth_prod.cer");
                    String UID = AudharNumber.Trim();
                    Txn = "UKC:" + Txn.Trim();

                    com.auth.AuthPacketCreator objAuthPacket = new com.auth.AuthPacketCreator();
                    //string authxml = objAuthPacket.createOtpAuthPacket(UID, TextBox1.Text.Trim(), keyPath, "SNDKS23054", Txn, "2018-10-31T13:11:46.027+05:30", "UDC-AGRIGOM-0001");

                    string authxml = objAuthPacket.createOtpKYCPacket(UID, Otp.Trim(), keyPath, "PNDKS23054", Txn, "2018-11-02T11:11:46.027+05:30", "UDC-AGRIGOM-0001", "");

                    //Response.Write(authxml);
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



                    String str = "<KUAData xmlns=" + PutIntoQuotes("http://kua.maharashtra.gov.in/kyc/gom-kyc-request") + ">";
                    str += "<uid>" + EncryptedUID + "</uid>";
                    str += "<appCode>KYCApp</appCode>";
                    str += "<Token>" + Token.Trim() + "</Token>";
                    str += "<KUASkey>" + AUASkey.Trim() + "</KUASkey>";
                    str += "<sa>PNDKS23054</sa>";
                    str += "<saTxn>" + Txn + "</saTxn>";
                    str += "<Data type =" + PutIntoQuotes("X") + ">" + Data + "</Data>";
                    str += "<Hmac>" + Hmac + "</Hmac>";
                    //str += "<Skey ci =" + PutIntoQuotes("20191230") + ">" + Skey + "</Skey>";
                    str += "<Skey ci =" + PutIntoQuotes("20250923") + ">" + Skey + "</Skey>";
                    str += "<Uses pi =" + PutIntoQuotes("n") + " pa =" + PutIntoQuotes("n") + " pfa =" + PutIntoQuotes("n") + " bio =" + PutIntoQuotes("n") + " pin=" + PutIntoQuotes("n") + " otp=" + PutIntoQuotes("y") + "/>";
                    str += "<Meta rdsId =" + PutIntoQuotes("") + " rdsVer =" + PutIntoQuotes("") + " dpId =" + PutIntoQuotes("") + " dc =" + PutIntoQuotes("") + " mi =" + PutIntoQuotes("") + " mc =" + PutIntoQuotes("") + " />";
                    str += "<type>A</type>";
                    str += "<rc>Y</rc>";
                    str += "<lr>Y</lr>";
                    str += "<pfr>N</pfr>";
                    str += "</KUAData>";



                    //string url = "https://kuaqa.maharashtra.gov.in/KUA/rest/kycreq";
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
                        //StreamReader sr = new StreamReader(res.GetResponseStream());
                        StreamReader sr = new StreamReader(res.GetResponseStream());
                        responseXml = sr.ReadToEnd();

                        try
                        {
                            string strPath = Server.MapPath("~/admintrans/UserControls/AULOG789.txt");// @"D:\Rekha\Log.txt";
                            if (!File.Exists(strPath))
                            {
                                File.Create(strPath).Dispose();
                            }
                            using (StreamWriter sw = File.AppendText(strPath))
                            {
                                sw.WriteLine("===========START============= " + AudharNumber);

                                sw.WriteLine("=============API  Logging ===========");
                                sw.WriteLine("REQ TEXT: " + str);
                                sw.WriteLine("========================");

                                sw.WriteLine("RES TEXT: " + responseXml);
                                sw.WriteLine("===========End============= " + DateTime.Now);
                                sw.Dispose();
                            }

                        }
                        catch { }
                    }


                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(responseXml); // suppose that myXmlString contains "<Names>...</Names>"


                    String OtpErrorCode = "", OtpRet = "";

                    XmlNodeList xdocls = xml.SelectNodes("/KuaRes");
                    foreach (XmlNode xNode in xdocls)
                    {
                        OtpRet = xNode.Attributes["ret"].InnerText;
                        if (xNode.Attributes["err"] != null)
                            OtpErrorCode = xNode.Attributes["err"].InnerText;
                    }

                    if (OtpRet.Trim().ToUpper() == "Y")
                    {

                        string email = "";
                        string phone = "";
                        string gender = "";
                        string dob = "";
                        string name = "";
                        string RegisterNameMr = "";
                        String Pht = "";

                        XmlNodeList xnList = xml.SelectNodes("/KuaRes/UidData");
                        foreach (XmlNode xn in xnList)
                        {
                            foreach (XmlNode Cxn in xn)
                            {
                                if (Cxn.Name == "Poi")
                                {
                                    if (Cxn.Attributes["email"] != null)
                                        email = Cxn.Attributes["email"].InnerText;
                                    if (Cxn.Attributes["phone"] != null)
                                        phone = Cxn.Attributes["phone"].InnerText;
                                    if (Cxn.Attributes["gender"] != null)
                                        gender = Cxn.Attributes["gender"].InnerText;
                                    if (Cxn.Attributes["dob"] != null)
                                        dob = Cxn.Attributes["dob"].InnerText;
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
                                        byte[] bytes = Convert.FromBase64String(Pht);
                                        d.ImageUrl = Convert.ToBase64String(bytes);

                                    }
                                }
                            }

                        }

                        // show records 
                        d.name = name;
                        d.nameInMarathi = RegisterNameMr;
                        if (dob.Length > 0)
                        {
                            if (dob.Contains("-"))
                            {
                                d.dob = dob;
                            }
                            else
                            {
                                d.dob = dob;
                            }
                        }
                        if (gender.Length > 0)
                        {
                            if (gender.Trim() == "M")
                            {
                                d.gender = "Male";

                            }
                            if (gender.Trim() == "F")
                            {
                                d.gender = "Female";

                            }
                            else if (gender.Trim() == "T")
                            {
                                d.gender = "Other";

                            }
                        }
                        d.MessageCode = "1";
                    }
                    else
                    {
                        String error = "";
                        if (OtpErrorCode.Trim().Length > 0)
                        {
                            error = UidiErrorCodes.GetErrorDetails(OtpErrorCode.Trim());
                        }
                        d.Message = error;
                        d.MessageCode = "0";
                    }
                }
                catch (Exception ex)
                {
                    MessageExp = ex.ToString();
                    Error = "aadhaar verification service is not working right now. please try after some time.";// ex.ToString();
                    d.Message = Error;
                    d.MessageCode = "0";
                    d.ImageUrl = "";

                }

                d.Txn = Txn;
                lst.Add(d);
            }
            catch (Exception ex)
            {
                clsAudharMessage d = new clsAudharMessage();
                d.MessageExp = ex.ToString();
                d.Message = "aadhaar verification service is not working right now. please try after some time.";// ex.ToString();
                //d.Message = ex.ToString();
                d.MessageCode = "0";
                lst.Add(d);
            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Newtonsoft.Json.Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Newtonsoft.Json.Formatting.Indented).ToString());  
        }




        [WebMethod]
        public String AuthrizedUsingMobileOtp(String SecurityKey, String AudharNumber, String Txn, String Otp)
        {

            List<clsAudharMessage> lst = new List<clsAudharMessage>();

            if (MyCommanClass.CheckApiAuthrization("87", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {

                clsAudharMessage d = new clsAudharMessage();
                d.Message = "Authorization Failed";
                d.MessageCode = "0";
                lst.Add(d);
            }
            try
            {
                String Error = "", MessageExp = "";
                clsAudharMessage d = new clsAudharMessage();
                try
                {



                    //string keyPath = Server.MapPath("~/admintrans/UserControls/uidai_auth_encrypt_preprod.cer");
                    string keyPath = Server.MapPath("~/admintrans/UserControls/Production/uidai_auth_prod.cer");
                    String UID = AudharNumber.Trim();
                    Txn = "UKC:" + Txn.Trim();

                    com.auth.AuthPacketCreator objAuthPacket = new com.auth.AuthPacketCreator();
                    //string authxml = objAuthPacket.createOtpAuthPacket(UID, TextBox1.Text.Trim(), keyPath, "SNDKS23054", Txn, "2018-10-31T13:11:46.027+05:30", "UDC-AGRIGOM-0001");

                    string authxml = objAuthPacket.createOtpKYCPacket(UID, Otp.Trim(), keyPath, "PNDKS23054", Txn, "2018-11-02T11:11:46.027+05:30", "UDC-AGRIGOM-0001", "");

                    //Response.Write(authxml);
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



                    String str = "<KUAData xmlns=" + PutIntoQuotes("http://kua.maharashtra.gov.in/kyc/gom-kyc-request") + ">";
                    str += "<uid>" + EncryptedUID + "</uid>";
                    str += "<appCode>KYCApp</appCode>";
                    str += "<Token>" + Token.Trim() + "</Token>";
                    str += "<KUASkey>" + AUASkey.Trim() + "</KUASkey>";
                    str += "<sa>PNDKS23054</sa>";
                    str += "<saTxn>" + Txn + "</saTxn>";
                    str += "<Data type =" + PutIntoQuotes("X") + ">" + Data + "</Data>";
                    str += "<Hmac>" + Hmac + "</Hmac>";
                    //str += "<Skey ci =" + PutIntoQuotes("20191230") + ">" + Skey + "</Skey>";
                    str += "<Skey ci =" + PutIntoQuotes("20250923") + ">" + Skey + "</Skey>";
                    str += "<Uses pi =" + PutIntoQuotes("n") + " pa =" + PutIntoQuotes("n") + " pfa =" + PutIntoQuotes("n") + " bio =" + PutIntoQuotes("n") + " pin=" + PutIntoQuotes("n") + " otp=" + PutIntoQuotes("y") + "/>";
                    str += "<Meta rdsId =" + PutIntoQuotes("") + " rdsVer =" + PutIntoQuotes("") + " dpId =" + PutIntoQuotes("") + " dc =" + PutIntoQuotes("") + " mi =" + PutIntoQuotes("") + " mc =" + PutIntoQuotes("") + " />";
                    str += "<type>A</type>";
                    str += "<rc>Y</rc>";
                    str += "<lr>Y</lr>";
                    str += "<pfr>N</pfr>";
                    str += "</KUAData>";



                    //string url = "https://kuaqa.maharashtra.gov.in/KUA/rest/kycreq";
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
                        //StreamReader sr = new StreamReader(res.GetResponseStream());
                        StreamReader sr = new StreamReader(res.GetResponseStream());
                        responseXml = sr.ReadToEnd();

                        try
                        {
                            string strPath = Server.MapPath("~/admintrans/UserControls/AULOG789.txt");// @"D:\Rekha\Log.txt";
                            if (!File.Exists(strPath))
                            {
                                File.Create(strPath).Dispose();
                            }
                            using (StreamWriter sw = File.AppendText(strPath))
                            {
                                sw.WriteLine("===========START============= " + AudharNumber);

                                sw.WriteLine("=============API  Logging ===========");
                                sw.WriteLine("REQ TEXT: " + str);
                                sw.WriteLine("========================");

                                sw.WriteLine("RES TEXT: " + responseXml);
                                sw.WriteLine("===========End============= " + DateTime.Now);
                                sw.Dispose();
                            }

                        }
                        catch { }
                    }


                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(responseXml); // suppose that myXmlString contains "<Names>...</Names>"


                    String OtpErrorCode = "", OtpRet = "";

                    XmlNodeList xdocls = xml.SelectNodes("/KuaRes");
                    foreach (XmlNode xNode in xdocls)
                    {
                        OtpRet = xNode.Attributes["ret"].InnerText;
                        if (xNode.Attributes["err"] != null)
                            OtpErrorCode = xNode.Attributes["err"].InnerText;
                    }

                    if (OtpRet.Trim().ToUpper() == "Y")
                    {

                        string email = "";
                        string phone = "";
                        string gender = "";
                        string dob = "";
                        string name = "";
                        string RegisterNameMr = "";
                        String Pht = "";

                        XmlNodeList xnList = xml.SelectNodes("/KuaRes/UidData");
                        foreach (XmlNode xn in xnList)
                        {
                            foreach (XmlNode Cxn in xn)
                            {
                                if (Cxn.Name == "Poi")
                                {
                                    if (Cxn.Attributes["email"] != null)
                                        email = Cxn.Attributes["email"].InnerText;
                                    if (Cxn.Attributes["phone"] != null)
                                        phone = Cxn.Attributes["phone"].InnerText;
                                    if (Cxn.Attributes["gender"] != null)
                                        gender = Cxn.Attributes["gender"].InnerText;
                                    if (Cxn.Attributes["dob"] != null)
                                        dob = Cxn.Attributes["dob"].InnerText;
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
                                        byte[] bytes = Convert.FromBase64String(Pht);
                                        d.ImageUrl = Convert.ToBase64String(bytes);

                                    }
                                }
                            }

                        }

                        // show records 
                        d.name = name;
                        d.nameInMarathi = RegisterNameMr;
                        if (dob.Length > 0)
                        {
                            if (dob.Contains("-"))
                            {
                                d.dob = dob;
                            }
                            else
                            {
                                d.dob = dob;
                            }
                        }
                        if (gender.Length > 0)
                        {
                            if (gender.Trim() == "M")
                            {
                                d.gender = "Male";

                            }
                            if (gender.Trim() == "F")
                            {
                                d.gender = "Female";

                            }
                            else if (gender.Trim() == "T")
                            {
                                d.gender = "Other";

                            }
                        }
                        d.MessageCode = "1";
                    }
                    else
                    {
                        String error = "";
                        if (OtpErrorCode.Trim().Length > 0)
                        {
                            error = UidiErrorCodes.GetErrorDetails(OtpErrorCode.Trim());
                        }
                        d.Message = error;
                        d.MessageCode = "0";
                    }
                }
                catch (Exception ex)
                {
                    MessageExp = ex.ToString();
                    Error = "aadhaar verification service is not working right now. please try after some time.";// ex.ToString();
                    d.Message = Error;
                    d.MessageCode = "0";
                    d.ImageUrl = "";

                }

                d.Txn = Txn;
                lst.Add(d);
            }
            catch (Exception ex)
            {
                clsAudharMessage d = new clsAudharMessage();
                d.MessageExp = ex.ToString();
                d.Message = "aadhaar verification service is not working right now. please try after some time.";// ex.ToString();
                //d.Message = ex.ToString();
                d.MessageCode = "0";
                lst.Add(d);
            }




            //Context.Response.Clear();
            //Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Newtonsoft.Json.Formatting.Indented).ToString().Length.ToString());
            //Context.Response.Flush();
            //Context.Response.Write(JsonConvert.SerializeObject(lst, Newtonsoft.Json.Formatting.Indented).ToString());


            return JsonConvert.SerializeObject(lst, Newtonsoft.Json.Formatting.Indented).ToString();

        }


        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        [WebMethod]
        public void AuthrizedUsingBio(String SecurityKey, String AudharNumber, String Base64StringEncodedPid)
        {

            List<clsAudharMessage> lst = new List<clsAudharMessage>();
            clsAudharMessage d = new clsAudharMessage();
            if (MyCommanClass.CheckApiAuthrization("85", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {


                d.Message = "Authorization Failed";
                d.MessageCode = "0";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Newtonsoft.Json.Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Newtonsoft.Json.Formatting.Indented).ToString());
                return;
            }
            try
            {
                String Error = "", MessageExp = ""; ;
                try
                {

                    String xmlPid = Base64Decode(Base64StringEncodedPid);
                    XmlDocument xdocl = new XmlDocument();
                    xdocl.LoadXml(xmlPid);
                    String Skey = "", Data = "", Hmac = "", rdsId = "", rdsVer = "", dpId = "", dc = "", mi = "", mc = "", OtpRet = "", keyExp = "";

                    XmlNodeList xResp = xdocl.SelectNodes("/PidData/Resp");
                    foreach (XmlNode xNode in xResp)
                    {
                        try
                        {
                            OtpRet = xNode.Attributes["errInfo"].InnerText;
                        }
                        catch { }
                        try
                        {
                            OtpRet = xNode.Attributes["errCode"].InnerText;
                        }
                        catch { }

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



                    String UID = AudharNumber.Trim();
                    String Txn = "UKC:" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

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

                    String Token = cls.generateSha256Hash(aa);// ComputeSha256Hash(aa)



                    String str = "<KUAData xmlns=" + PutIntoQuotes("http://kua.maharashtra.gov.in/kyc/gom-kyc-request") + ">";
                    str += "<uid>" + EncryptedUID + "</uid>";
                    str += "<appCode>KYCApp</appCode>";
                    str += "<Token>" + Token.Trim() + "</Token>";
                    str += "<KUASkey>" + AUASkey.Trim() + "</KUASkey>";
                    str += "<sa>PNDKS23054</sa>";
                    str += "<saTxn>" + Txn + "</saTxn>";
                    str += "<Data type =" + PutIntoQuotes("X") + ">" + Data + "</Data>";
                    str += "<Hmac>" + Hmac + "</Hmac>";
                    if (keyExp.Trim() == "20191230")
                    {
                        str += "<Skey ci =" + PutIntoQuotes("20191230") + ">" + Skey + "</Skey>";
                    }
                    else
                    {
                        str += "<Skey ci =" + PutIntoQuotes("20250923") + ">" + Skey + "</Skey>";
                    }
                    str += "<Uses pi =" + PutIntoQuotes("n") + " pa =" + PutIntoQuotes("n") + " pfa =" + PutIntoQuotes("n") + "  bio =" + PutIntoQuotes("y") + " bt=" + PutIntoQuotes("FMR,FIR") + " pin=" + PutIntoQuotes("n") + " otp=" + PutIntoQuotes("n") + "/>";
                    str += "<Meta rdsId =" + PutIntoQuotes(rdsId) + " rdsVer =" + PutIntoQuotes(rdsVer) + " dpId =" + PutIntoQuotes(dpId) + " dc =" + PutIntoQuotes(dc) + " mi =" + PutIntoQuotes(mi) + " mc =" + PutIntoQuotes(mc) + " />";
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

                        try
                        {
                            string strPath = Server.MapPath("~/admintrans/UserControls/AULOG.txt");// @"D:\Rekha\Log.txt";
                            if (!File.Exists(strPath))
                            {
                                File.Create(strPath).Dispose();
                            }
                            using (StreamWriter sw = File.AppendText(strPath))
                            {
                                sw.WriteLine("===========START BIO ============= " + AudharNumber);

                                sw.WriteLine("=============API  Logging ===========");
                                sw.WriteLine("REQ TEXT: " + str);
                                sw.WriteLine("========================");

                                sw.WriteLine("RES TEXT: " + responseXml);
                                sw.WriteLine("===========End============= " + DateTime.Now);
                                sw.Dispose();
                            }
                        }
                        catch
                        {

                        }
                        Util.LogError(str);
                        Util.LogError(responseXml);
                    }


                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(responseXml); // suppose that myXmlString contains "<Names>...</Names>"
                                              // d.xmlReturn = xml;

                    String OtpErrorCode = "";

                    XmlNodeList xdocls = xml.SelectNodes("/KuaRes");
                    foreach (XmlNode xNode in xdocls)
                    {
                        OtpRet = xNode.Attributes["ret"].InnerText;
                        if (xNode.Attributes["err"] != null)
                            OtpErrorCode = xNode.Attributes["err"].InnerText;
                    }

                    if (OtpRet.Trim().ToUpper() == "Y")
                    {

                        string email = "";
                        string phone = "";
                        string gender = "";
                        string dob = "";
                        string name = "";
                        string nameInMarathi = "";
                        String Pht = "";

                        XmlNodeList xnList = xml.SelectNodes("/KuaRes/UidData");
                        foreach (XmlNode xn in xnList)
                        {
                            foreach (XmlNode Cxn in xn)
                            {
                                if (Cxn.Name == "Poi")
                                {
                                    if (Cxn.Attributes["email"] != null)
                                        email = Cxn.Attributes["email"].InnerText;
                                    if (Cxn.Attributes["phone"] != null)
                                        phone = Cxn.Attributes["phone"].InnerText;
                                    if (Cxn.Attributes["gender"] != null)
                                        gender = Cxn.Attributes["gender"].InnerText;
                                    if (Cxn.Attributes["dob"] != null)
                                        dob = Cxn.Attributes["dob"].InnerText;
                                    if (Cxn.Attributes["name"] != null)
                                        if (name.Trim().Length == 0)
                                            name = Cxn.Attributes["name"].InnerText;
                                }
                                if (Cxn.Name == "LData")
                                {
                                    if (Cxn.Attributes["name"] != null)
                                        if (nameInMarathi.Trim().Length == 0)
                                            nameInMarathi = Cxn.Attributes["name"].InnerText;
                                }
                                if (Cxn.Name == "Pht")
                                {
                                    Pht = Cxn.InnerText;
                                    if (Pht.Length > 0)
                                    {
                                        byte[] bytes = Convert.FromBase64String(Pht);
                                        d.ImageUrl = Convert.ToBase64String(bytes);

                                    }
                                }
                            }

                        }

                        // show records 
                        d.name = name;
                        d.nameInMarathi = nameInMarathi;
                        if (dob.Length > 0)
                        {
                            if (dob.Contains("-"))
                            {
                                d.dob = dob;
                            }
                        }
                        if (gender.Length > 0)
                        {
                            if (gender.Trim() == "M")
                            {
                                d.gender = "Male";

                            }
                            if (gender.Trim() == "F")
                            {
                                d.gender = "Female";

                            }
                            else if (gender.Trim() == "T")
                            {
                                d.gender = "Other";

                            }
                        }
                        d.MessageCode = "1";
                    }
                    else
                    {
                        String error = "";

                        if (OtpErrorCode.Trim().Length > 0)
                        {
                            error = UidiErrorCodes.GetErrorDetails(OtpErrorCode.Trim());
                        }
                        d.Message = error;
                        d.MessageCode = "0";
                    }
                }
                catch (Exception ex)
                {
                    //Error = ex.ToString();
                    MessageExp = ex.ToString();
                    Error = "aadhaar verification service is not working right now. please try after some time.";// ex.ToString();
                    d.Message = Error;
                    d.MessageCode = "0";
                }



            }
            catch (Exception ex)
            {
                d.MessageExp = ex.ToString();
                d.Message = "aadhaar verification service is not working right now. please try after some time.";// ex.ToString();
                d.MessageCode = "0";
            }


            lst.Add(d);

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Newtonsoft.Json.Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Newtonsoft.Json.Formatting.Indented).ToString());




        }




    }

    public class clsAudharMessage
    {
        public String Txn { get; set; }
        public string Message { get; set; }
        public string MessageCode { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public string name { get; set; }
        public string nameInMarathi { get; set; }
        public string ImageUrl { get; set; }
        public string MessageExp { get; set; }
        public XmlDocument xmlReturn { get; set; }

    }
}
