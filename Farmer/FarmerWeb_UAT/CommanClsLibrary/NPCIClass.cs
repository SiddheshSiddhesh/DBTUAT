using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;

/// <summary>
/// Summary description for NPCIClass
/// </summary>
public class NPCIClass
{

    public string InvokeService(string Aadhaar, string mobile)
    {
        //Calling CreateSOAPWebRequest method 
        string Response = "";
        HttpWebRequest request = CreateSOAPWebRequest();

        XmlDocument SOAPReqBody = new XmlDocument();
        //SOAP Body Request  
        SOAPReqBody.LoadXml(@"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:aad='http://aadhaar.npci.org/'>
   <soapenv:Header/> <soapenv:Body> <aad:getAadhaarStatus> <!--Optional:--> <arg0> <!--Optional:--> <aadhaarNumber>"+Aadhaar +"</aadhaarNumber> <!--Optional:--> <mobileNumber>"+ mobile+"</mobileNumber> <!--Optional:--> <requestNumber>PUNB000212</requestNumber> <!--Optional:--> <requestedDateTimeStamp>"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.10")+"</requestedDateTimeStamp> </arg0> </aad:getAadhaarStatus> </soapenv:Body> </soapenv:Envelope>");


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
        return Response;
    }

    private HttpWebRequest CreateSOAPWebRequest()
    {
        //Making Web Request  
        // HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"https://103.14.162.34/CMAadhaar/AadhaarStatusService/AadhaarStatusService.wsdl");

        ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });
        HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"https://103.14.162.34/CMAadhaar/AadhaarStatusService");
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
