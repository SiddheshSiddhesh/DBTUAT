using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Xml.Linq;

namespace DBTPoCRA.AdminTrans.UserControls
{
    public partial class WebForm1T : System.Web.UI.Page
    {

        //public static String Txn = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["Txn"] = "UKC:" + DateTime.Now.ToString("yyyyMMddHHmmssfff");// "2018103130051212";
                                                                                       // SendForAuthentication();

                //id.Decode(ResponseMsg);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Server.MapPath("~/TESTDATA/pid.xml").ToString());
                xmlDoc.PreserveWhitespace = true;

                String OtpRet = "";

                XmlNodeList xdocl = xmlDoc.SelectNodes("/KuaRes");
                foreach (XmlNode xNode in xdocl)
                {
                    OtpRet = xNode.Attributes["ret"].InnerText;
                    //OtpErrorCode = xNode.Attributes["err"].InnerText;
                }


                string email = "";
                string phone = "";
                string gender = "";
                string dob = "";
                string name = "";
                String Pht = "";

                XmlNodeList xnList = xmlDoc.SelectNodes("/KuaRes/UidData");
                foreach (XmlNode xn in xnList)
                {
                    foreach (XmlNode Cxn in xn)
                    {
                        if (Cxn.Name == "Poi")
                        {
                            if(Cxn.Attributes["email"]!=null)
                            email = Cxn.Attributes["email"].InnerText;
                            if(Cxn.Attributes["phone"]!=null)
                            phone = Cxn.Attributes["phone"].InnerText;
                            if(Cxn.Attributes["gender"]!=null)
                            gender = Cxn.Attributes["gender"].InnerText;
                            if(Cxn.Attributes["dob"]!=null)
                            dob = Cxn.Attributes["dob"].InnerText;
                            if(Cxn.Attributes["name"]!=null)
                            name = Cxn.Attributes["name"].InnerText;
                        }
                        if (Cxn.Name == "Pht")
                        {
                            Pht = Cxn.InnerText;
                          
                        }
                    }

                }

            }
        }

        public string PutIntoQuotes(string value)
        {
            return "\"" + value + "\"";
        }
        public void SendForAuthentication()
        {

            // SingXML(); "">

            String Txn = ViewState["Txn"].ToString().Trim();// "UKC:public:" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

            String str = "<Auth xmlns=" + PutIntoQuotes("http://aua.maharashtra.gov.in/auth/gom-auth-request") + ">";
            str += "<Txn>" + Txn + "</Txn>";
            str += "<Ver>2.5</Ver>";
            str += "<SubAUACode>SNDKS23054</SubAUACode>";
            str += "<ReqType>otp</ReqType>";
            str += "<DeviceId>UDC-AGRIGOM-0001</DeviceId>";
            str += "<UID>899348693919</UID>";
            str += "<type>A</type>";
            str += "<Ch>01</Ch>";
            str += "</Auth> ";

            string url ="https://auaqa.maharashtra.gov.in/aua/rest/authreqv2";

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

            Button1.Visible = false;
            String error = "";
            if(OtpRet.Trim().ToUpper()=="N")
            {
                // error
                if(OtpErrorCode.Trim().Length>0)
                {
                    UidiErrorCodes ecode = new UidiErrorCodes();
                   // Dictionary<Int32, String> ErrorList = UidiErrorCodes.ErrorList;
                    //ErrorList.TryGetValue(Convert.ToInt32(OtpErrorCode), out error);

                }
                if(error.Length==0)
                {
                    error = "response is not coming from uidai kindly try again";
                }

            }
            else
            {
                Button1.Visible = true;
                error = "OTP send to registered mobile number and will valid for next 5 min.";
            }

         
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string keyPath = Server.MapPath("~/admintrans/UserControls/uidai_auth_encrypt_preprod.cer");// "E:\\uidai_auth_encrypt_preprod.cer";
            String UID = "899348693919";
            String Txn = ViewState["Txn"].ToString().Trim();

            com.auth.AuthPacketCreator objAuthPacket = new com.auth.AuthPacketCreator();
            //string authxml = objAuthPacket.createOtpAuthPacket(UID, TextBox1.Text.Trim(), keyPath, "SNDKS23054", Txn, "2018-10-31T13:11:46.027+05:30", "UDC-AGRIGOM-0001");

            string authxml = objAuthPacket.createOtpKYCPacket(UID, TextBox1.Text.Trim(), keyPath, "SNDKS23054", Txn, "2018-10-31T13:11:46.027+05:30", "UDC-AGRIGOM-0001","");

            //Response.Write(authxml);
            XmlDocument xdocl = new XmlDocument();
            xdocl.LoadXml(authxml);

            String Skey = "", Data="", Hmac="";

            foreach (XmlNode xNode in xdocl)
            {
                
                foreach (XmlNode CNode in xNode)
                {
                    if(CNode.Name.Trim()== "Skey")
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

            //String str = "<Auth xmlns=" + PutIntoQuotes("http://aua.maharashtra.gov.in/auth/gom-auth-request") + ">";
            //str += "<Txn>" + Txn + "</Txn>";
            //str += "<Ver>2.5</Ver>";
            //str += "<SubAUACode>SNDKS23054</SubAUACode>";
            //str += "<ReqType>auth</ReqType>";
            //str += "<DeviceId>UDC-AGRIGOM-0001</DeviceId>";
            //str += "<UID>"+ UID + "</UID>";
            //str += "<Uses pi =" + PutIntoQuotes("n") + " pa =" + PutIntoQuotes("n") + " pfa =" + PutIntoQuotes("n") + " bio =" + PutIntoQuotes("n") + " pin=" + PutIntoQuotes("n") + " otp=" + PutIntoQuotes("y") + "/>";

            //str += "<Meta rdsId =" + PutIntoQuotes("") + " rdsVer =" + PutIntoQuotes("") + " dpId =" + PutIntoQuotes("") + " dc =" + PutIntoQuotes("") + " mi =" + PutIntoQuotes("") + " mc =" + PutIntoQuotes("") + " />";
            //str += "<Skey ci =" + PutIntoQuotes("20201030") + ">" + Skey + "</Skey>";
            //str += "<Data type =" + PutIntoQuotes("X") + ">" + Data + "</Data>";
            //str += "<Hmac>" + Hmac + "</Hmac> <type>A</type>";
            //str += "<rc>Y</rc>";
            //str += "</Auth>";

            String str = "<KUAData xmlns=" + PutIntoQuotes("http://kua.maharashtra.gov.in/kyc/gom-kyc-request") + ">";
            str += "<UID>" + UID + "</UID>";
            str += "<appCode>KYCApp</appCode>";
            str += "<sa>SNDKS23054</sa>";
            str += "<saTxn>" + Txn + "</saTxn>";
            str += "<Data type =" + PutIntoQuotes("X") + ">" + Data + "</Data>";
            str += "<Hmac>" + Hmac + "</Hmac>";
            str += "<Skey ci =" + PutIntoQuotes("20201030") + ">" + Skey + "</Skey>";
           
            str += "<Uses pi =" + PutIntoQuotes("n") + " pa =" + PutIntoQuotes("n") + " pfa =" + PutIntoQuotes("n") + " bio =" + PutIntoQuotes("n") + " pin=" + PutIntoQuotes("n") + " otp=" + PutIntoQuotes("y") + "/>";

            str += "<Meta rdsId =" + PutIntoQuotes("") + " rdsVer =" + PutIntoQuotes("") + " dpId =" + PutIntoQuotes("") + " dc =" + PutIntoQuotes("") + " mi =" + PutIntoQuotes("") + " mc =" + PutIntoQuotes("") + " />";


            str += "<type>A</type>";
            str += "<rc>Y</rc>";
            str += "<lr>N</lr>";
            str += "<pfr>N</pfr>";
            str += "</KUAData>";



            string url = "https://kuaqa.maharashtra.gov.in/KUA/rest/kycreq";// "https://auaqa.maharashtra.gov.in/aua/rest/authreqv2";
            String Conn = str;
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
            string responseXml = sr.ReadToEnd();


            //DeserializeResponseXml(responseXml);
            //var str = Encoding.UTF8.GetString(stream.ToArray());
            //String ResponseMsg = "04{01111562h/wSphY5CJ2k5+tTUed1p9KgjNEXWpFOsCiouYVIzF35EaavdkeSNbx2wnj6Knqv,A,e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855,0100000100000010,2.0,20181031165425,0,0,0,0,2.5,a695e5c2f15d22af8e0c72a0627b1f99755870fbcc1d9cb37c1d67c638e5e95d,85bd3fd04c13b2b4dfe9e3bfdb116d9a8e9b55d6a1c68dd2da2b63b73a19ebfa,875a032173f3aa6aae0b80d3ad8939549310600fe6ad47f010f77008089809a4,NA,NA,NA,NA,NA,NA,NA,NA,NA,NA,,NA,NA,NA,NA,NA,NA}";

        }

        //protected  void DeserializeResponseXml(XElement responseXml)
        //{
        //    //ValidateNull(Response, nameof(responseXml));

        //    String ErrorCode = responseXml.Attribute("err")?.Value;

        //    // Catch all exceptions arising from API error condition due to absence of mandatory XML elements and attributes.
        //    try { Response.FromXml(responseXml); }
        //    finally
        //    {
        //        //if (!string.IsNullOrWhiteSpace(Response.ErrorCode))
        //        //    throw new ApiException(Response.ErrorCode);
        //    }
        //}
    }
}