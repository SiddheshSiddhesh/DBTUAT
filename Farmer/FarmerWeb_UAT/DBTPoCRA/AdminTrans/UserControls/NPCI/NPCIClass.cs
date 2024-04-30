using CommanClsLibrary;
using CommanClsLibrary.Repository.Classes;
using CommanClsLibrary.Repository.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using static CommanClsLibrary.Repository.Enums;

/// <summary>
/// Summary description for NPCIClass
/// </summary>
public class NPCIClass
{

    public static void UpdateAdharStatusDetails(String RegistrationID)
    {
        MyClass cla = new MyClass();
        DataTable dt = cla.GetDataTable("SELECT  B.ADVRefrenceID,B.AaDharNumber ,B.BoltID,R.MobileNumber FROM Tbl_M_RegistrationDetails_Bolt as B INNER JOIN Tbl_M_RegistrationDetails R on R.BoltID=B.BoltID where B.IsDeleted is null and R.RegistrationID=" + RegistrationID + " ");
        if (dt.Rows.Count > 0)
        {
            try
            {
                //string ADVRefrenceID = dt.Rows[0]["ADVRefrenceID"].ToString();
                //AdharVaultAPICalls api = new AdharVaultAPICalls();
                //string AdharNumber = api.GetAdharFromReference(ADVRefrenceID);

                string AdharNumber = dt.Rows[0]["AaDharNumber"].ToString(); 

                String BoltID = dt.Rows[0]["BoltID"].ToString();
                NPCIResponce cl = InvokeService(AdharNumber, dt.Rows[0]["MobileNumber"].ToString());
                String s = "update Tbl_M_RegistrationDetails_Bolt set Last_Updated_Date='" + cl.lastUpdatedDate + "', A_Status='" + cl.status + "', A_Error='" + cl.error + "' , LastUpdateOn=GETDATE()   where BoltID=" + BoltID + " ";
                cla.ExecuteCommand(s);

                String A_Status = "Not-Linked";
                if (cl.status.Trim() == "A")
                {
                    A_Status = "Linked";
                }
                cla.ExecuteCommand("update Tbl_M_RegistrationDetails set AdStatus='" + A_Status + "' where RegistrationID=" + RegistrationID + " ");


            }
            catch (Exception ex)
            {
                Util.LogErrorNNPCI("NPCI-" + ex.ToString());

                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "NPCIClass:UpdateAdharStatusDetails";
                err.ProjectName = "POCRA WEBSITE";
                err.ErrorDescription = "Exception : " + ex.Message + " \n ,Logger RegistrationID : " + RegistrationID;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);
            }

        }

    }

    public static String UpdateAdharStatusDetailsAPI(String RegistrationID)
    {
        String Str = "";
        MyClass cla = new MyClass();
        DataTable dt = cla.GetDataTable("SELECT  B.ADVRefrenceID,B.AaDharNumber ,B.BoltID,R.MobileNumber FROM Tbl_M_RegistrationDetails_Bolt as B INNER JOIN Tbl_M_RegistrationDetails R on R.BoltID=B.BoltID where B.IsDeleted is null and R.RegistrationID=" + RegistrationID + " ");
        if (dt.Rows.Count > 0)
        {
            try
            { 
                //string ADVRefrenceID = dt.Rows[0]["ADVRefrenceID"].ToString();
                //AdharVaultAPICalls api = new AdharVaultAPICalls();
                //string AdharNumber = api.GetAdharFromReference(ADVRefrenceID);
                 
                string AdharNumber = dt.Rows[0]["AaDharNumber"].ToString();

                String BoltID = dt.Rows[0]["BoltID"].ToString();
                NPCIResponce cl = InvokeService(AdharNumber, dt.Rows[0]["MobileNumber"].ToString());
                Str = cl.status;
                String s = "update Tbl_M_RegistrationDetails_Bolt set Last_Updated_Date='" + cl.lastUpdatedDate + "', A_Status='" + cl.status + "', A_Error='" + cl.error + "' , LastUpdateOn=GETDATE()   where BoltID=" + BoltID + " ";
                cla.ExecuteCommand(s);

                String A_Status = "Not-Linked";
                if (cl.status.Trim() == "A")
                {
                    A_Status = "Linked";
                }
                cla.ExecuteCommand("update Tbl_M_RegistrationDetails set AdStatus='" + A_Status + "' where RegistrationID=" + RegistrationID + " ");
                Str = A_Status;
            }
            catch (Exception ex)
            {
                Util.LogErrorNNPCI("NPCI-" + ex.ToString());

                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "NPCIClass:UpdateAdharStatusDetailsAPI";
                err.ProjectName = "POCRA WEBSITE";
                err.ErrorDescription = "Exception : " + ex.Message + " \n ,Logger RegistrationID : " + RegistrationID;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);
            }

        }
        return Str;

    }


    public static string GetAdharStatusDetails(string AaDharNumber,string ADVRefrenceID)
    {
        string ret = "";
        MyClass cla = new MyClass();

        //DataTable dt = cla.GetDataTable("SELECT B.BoltID,R.MobileNumber FROM Tbl_M_RegistrationDetails_Bolt as B INNER JOIN Tbl_M_RegistrationDetails R on R.BoltID=B.BoltID where B.IsDeleted is null and R.IsDeleted is null  " +
        //   "and B.ADVRefrenceID='" + ADVRefrenceID + "' ");

        DataTable dt = cla.GetDataTable("SELECT B.BoltID,R.MobileNumber FROM Tbl_M_RegistrationDetails_Bolt as B INNER JOIN Tbl_M_RegistrationDetails R on R.BoltID=B.BoltID where B.IsDeleted is null and R.IsDeleted is null  " +
            "and B.AaDharNumber='" + AaDharNumber + "' ");  
        if (dt.Rows.Count > 0)
        {
            try
            {
                string BoltID = dt.Rows[0]["BoltID"].ToString();
                NPCIResponce cl = InvokeService(AaDharNumber, dt.Rows[0]["MobileNumber"].ToString());
                string s = "update Tbl_M_RegistrationDetails_Bolt set Last_Updated_Date='" + cl.lastUpdatedDate + "', A_Status='" + cl.status + "', A_Error='" + cl.error + "' , LastUpdateOn=GETDATE()   where BoltID=" + BoltID + " ";
                cla.ExecuteCommand(s);
                ret = "Last_Updated_Date ::" + cl.lastUpdatedDate + "   Status :: " + cl.status + "  Error :: " + cl.error;
            }
            catch (Exception ex)
            {
                Util.LogErrorNNPCI("NPCI-" + ex.ToString());

                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "NPCIClass:GetAdharStatusDetails";
                err.ProjectName = "POCRA WEBSITE";
                err.ErrorDescription = "Exception : " + ex.Message + " \n ,Logged Adhar : " + AaDharNumber;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);
            }

        }
        return ret;
    }

    public static NPCIResponce InvokeService(string Aadhaar, string mobile)
    {
        System.Net.ServicePointManager.Expect100Continue = false;

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
       | SecurityProtocolType.Tls11
       | SecurityProtocolType.Tls12
       | SecurityProtocolType.Ssl3;

        //Calling CreateSOAPWebRequest method 
        string Response = "";
        HttpWebRequest request = CreateSOAPWebRequest();
        NPCIResponce npci = new NPCIResponce();
        XmlDocument SOAPReqBody = new XmlDocument();
        //SOAP Body Request  
        //SOAPReqBody.LoadXml(@"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:aad='http://aadhaar.npci.org/'> <soapenv:Header/> <soapenv:Body> <aad:getAadhaarStatus> <!--Optional:--> <arg0> <!--Optional:--> <aadhaarNumber>"+Aadhaar +"</aadhaarNumber> <!--Optional:--> <mobileNumber>"+ mobile+ "</mobileNumber> <!--Optional:--> <requestNumber>HDFC000001</requestNumber> <!--Optional:--> <requestedDateTimeStamp>" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")+"</requestedDateTimeStamp> </arg0> </aad:getAadhaarStatus> </soapenv:Body> </soapenv:Envelope>");

        String str = "<soapenv:Envelope xmlns:soapenv=" + MyClass.PutIntoQuotes("http://schemas.xmlsoap.org/soap/envelope/") + " xmlns:aad=" + MyClass.PutIntoQuotes("http://aadhaar.npci.org/") + ">";
        str += "<soapenv:Header/>";
        str += "<soapenv:Body>";
        str += "<aad:getAadhaarStatus>";
        str += "<!--Optional:-->";
        str += "<arg0>";
        str += "<!--Optional:-->";
        str += "<aadhaar_Number>" + Aadhaar.Trim() + "</aadhaar_Number>";
        str += "<!--Optional:-->";
        str += "<mobile_Number>" + mobile.Trim() + "</mobile_Number>";
        str += "<!--Optional:-->";
        str += "<request_Number>MAHB000001</request_Number>";
        str += "<!--Optional:-->";
        str += "<requested_date_TimeStamp>" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + ".10</requested_date_TimeStamp>";
        str += "</arg0></aad:getAadhaarStatus></soapenv:Body>";
        str += "</soapenv:Envelope>";

        SOAPReqBody.LoadXml(str);



        using (Stream stream = request.GetRequestStream())
        {
            SOAPReqBody.Save(stream);
        }
        //Geting response from request  
        using (WebResponse Serviceres = request.GetResponse())
        {
            using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
            {
                //reading stream  
                var ServiceResult = rd.ReadToEnd();
                //writting stream result on console  
                //Console.WriteLine(ServiceResult);
                //Console.ReadLine();
                Response = ServiceResult.ToString();

            }
        }

        XDocument doc = XDocument.Parse(Response);
        XNamespace ns = "";
        IEnumerable<XElement> responses = doc.Descendants(ns + "return");
        //foreach (XElement response1 in responses)
        //{
        //    Response = (string)response1.Element(ns + "status");

        //}
        foreach (XElement response1 in responses)
        {
            npci.bankName = (string)response1.Element(ns + "bankName");
            npci.lastUpdatedDate = (string)response1.Element(ns + "lastUpdatedDate");
            npci.mandateFlag = (string)response1.Element(ns + "mandateFlag");
            npci.requestReceivedDateTime = (string)response1.Element(ns + "requestReceivedDateTime");
            npci.status = (string)response1.Element(ns + "status");
            npci.error = (string)response1.Element(ns + "error");
            if (npci.status.Trim().Length == 0)
            {
                npci.status = npci.error;
            }
        }

        return npci;
    }

    private static HttpWebRequest CreateSOAPWebRequest()
    {        
        //Making Web Request  
        // HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"https://103.14.162.34/CMAadhaar/AadhaarStatusService/AadhaarStatusService.wsdl");

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
        | SecurityProtocolType.Tls11
        | SecurityProtocolType.Tls12
        | SecurityProtocolType.Ssl3;
        ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });
        //HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"https://103.14.161.34/Aadhaar/AadhaarQueryService");
        HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"https://nach.npci.org.in/Aadhaar/AadhaarQueryService");
        //SOAPAction  
        Req.Headers.Add(@"SOAPAction:http://tempuri.org/Addition");
        //Content_type  
        Req.ContentType = "text/xml;charset=\"utf-8\"";
        Req.Accept = "text/xml";
        //HTTP method  
        Req.Method = "POST";
        //return HttpWebRequest  
        return Req;
    }
}

public class NPCIResponce
{
    public String bankName { get; set; }
    public String lastUpdatedDate { get; set; }
    public String mandateFlag { get; set; }
    public String requestReceivedDateTime { get; set; }
    public String status { get; set; }
    public String error { get; set; }
}
