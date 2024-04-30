using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Net;
using System.IO;

/// <summary>
/// Summary description for SMS
/// </summary>
/// 
namespace CommanClsLibrary
{

    public class SMS
    {
        public SMS()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private static string apicall(string url)
        {
            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);

            try
            {

                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                StreamReader sr = new StreamReader(httpres.GetResponseStream());
                string results = sr.ReadToEnd();
                sr.Close();
                return results;



            }
            catch
            {

                return "0";
            }
        }



        public static int SendSMS(string message, string mobileNo, String templateID)
        {
            int x = 0;
            try
            {
                SMSHttpPostClient NewSMS = new SMSHttpPostClient();
                String s = NewSMS.sendUnicodeSMS(mobileNo, message, templateID);
                SendSMSLog(s, mobileNo);
                x = 1;

            }
            catch (Exception ex)
            {
                SMSLogError(ex, mobileNo);
                x = 0;
            }

            return x;
        }

        public static String SendSMSNew(string message, string mobileNo, String templateID)
        {
            String s = "";
            try
            {
                SMSHttpPostClient NewSMS = new SMSHttpPostClient();
                s = NewSMS.sendUnicodeSMS(mobileNo, message, templateID);
                SendSMSLog(s, mobileNo);


            }
            catch (Exception ex)
            {
                SMSLogError(ex, mobileNo);
                s = ex.Message;
            }

            return s;
        }


        public static int SendSMS(string message, string mobileNo)
        {
            int x = 0;
            try
            {
                SMSHttpPostClient NewSMS = new SMSHttpPostClient();
                String s = NewSMS.sendUnicodeSMS(mobileNo, message, "");
                if (s.Contains("402"))
                {
                    x = 1;
                }

            }
            catch (Exception ex)
            {
                SMSLogError(ex, mobileNo);
                x = 0;
            }

            return x;
        }


        //-----------------------------
        public static void SMSLogError(Exception ex, String MobileNo)
        {
            try
            {

                string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                message += Environment.NewLine;
                message += "-------------------------Mobile No " + MobileNo + "----------------------------------";
                message += Environment.NewLine;
                message += string.Format("Message: {0}", ex.Message);
                message += Environment.NewLine;
                message += string.Format("StackTrace: {0}", ex.StackTrace);
                message += Environment.NewLine;
                message += string.Format("Source: {0}", ex.Source);
                message += Environment.NewLine;
                message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
                message += Environment.NewLine;


                message += "-----------------------------------------------------------";
                message += Environment.NewLine;

                try
                {

                    string ipaddress;
                    ipaddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (ipaddress == "" || ipaddress == null)
                        ipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];


                    message += string.Format("IPAddress: {0}", ipaddress.ToString());
                    message += Environment.NewLine;
                    message += "-----------------------------------------------------------";
                    message += Environment.NewLine;
                }
                catch
                {

                }

                String PathUp = "~/DocMasters/SMSErroLog";
                String path = System.Web.HttpContext.Current.Server.MapPath(PathUp);
                if (!Directory.Exists(path))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }
                path = path + "/SMSErroLog" + DateTime.Now.ToString("MM/dd/yyyy").Replace("/", "") + ".txt";
                string seperator = "-------------------------------------------\n\r";
                File.AppendAllText(path,
                    seperator + DateTime.Now.ToString() + "\n\r" + message + "\n\r" + seperator);


            }
            catch { }

        }
        public static void SendSMSLog(String ex, String MobileNo)
        {
            try
            {

                string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                message += Environment.NewLine;
                message += "-------------------------Mobile No " + MobileNo + "----------------------------------";

                message += Environment.NewLine;
                message += "-------------------------Responce " + ex + "----------------------------------";
                message += Environment.NewLine;

                try
                {

                    string ipaddress;
                    ipaddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (ipaddress == "" || ipaddress == null)
                        ipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];


                    message += string.Format("IPAddress: {0}", ipaddress.ToString());
                    message += Environment.NewLine;
                    message += "-----------------------------------------------------------";
                    message += Environment.NewLine;
                }
                catch
                {

                }

                String PathUp = "~/DocMasters/SMSErroLog";
                String path = System.Web.HttpContext.Current.Server.MapPath(PathUp);
                if (!Directory.Exists(path))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }
                path = path + "/SMSLog" + DateTime.Now.ToString("MM/dd/yyyy").Replace("/", "") + ".txt";
                string seperator = "-------------------------------------------\n\r";
                File.AppendAllText(path,
                    seperator + DateTime.Now.ToString() + "\n\r" + message + "\n\r" + seperator);


            }
            catch { }

        }
        //-------------------

        public static int SendSmsOnRegistration(String MobileNumber)
        {
            String message = "ना. दे. कृ. सं. प्रकल्पाच्या नोंदणीसाठी आपला अर्ज यशस्वी झाला आहे.";
            SendSMS(message, MobileNumber, "1407159162011897752");

            return 1;
        }

        public static int SendSmsOnRegistrationStatusChanged(String MobileNumber, String Status, String registrationNo)
        {
            if (MobileNumber != "")
            {
                String message = "नानाजी देशमुख कृषि संजीवनी प्रकल्पाच्या अंतर्गत आपले सत्यापन याशास्वीरीता " + Status + " आहे. तुमचा नोंदणी क्रमांक " + registrationNo + " आहे. तुमचा लॉगइन आयडी तुमचा आधार क्रमांक असेल.";
                SendSMS(message, MobileNumber, "1407161535802986285");
            }
            return 1;
        }
        public static int SendSmsOnSchemeApplication(String MobileNumber, String ApplicationNo, String activity)
        {
            MyClass cla = new MyClass();
            String a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityNameMr=N'" + activity + "' and a.IsDeleted is null )");
            if (a.Trim().Length == 0)
            {
                a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityName='" + activity + "' and a.IsDeleted is null )");
            }
            activity = a;

            String message = "आपण नानाजी देशमुख कृषि संजीवनी प्रकल्पाच्या अंतर्गत  " + activity + " घटकासाठी यशस्वीरित्या अर्ज केला आहे. " + ApplicationNo + " आपला अनुप्रयोग क्रमांक आहे.";
            SendSMS(message, MobileNumber, "1407161535733251520");
            // }
            return 1;
        }
        public static int SendSmsOnVCRMCApproval(String MobileNumber, String ApplicationNo)
        {
            String message = "तुमचा अर्ज क्रमांक " + ApplicationNo + "  ग्राम संजीवनी समिती द्वारा मंजूर झाला आहे आणि तांत्रिक पडताळणीसाठी पाठविण्यात येत आहे.";
            SendSMS(message, MobileNumber, "1407161535737624447");
            return 1;
        }
        public static int SendSmsOnPrePostSpotVerification(String MobileNumber, String ApplicationNo)
        {
            String message = "घटकाच्या प्रस्तावित स्थळ पाहणीसाठी ना.दे.कृ.सं. चे अधिकारी आपल्या शेतात येतील/भेट देतील. आपणास विनंती आहे कि त्यासाठी आपण उपस्थित राहावे आणि सहकार्य  करावे.";
            SendSMS(message, MobileNumber, "1407161519314241604");

            return 1;
        }
        public static int SendSmsOnAfterSpotVerification(String MobileNumber, String ApplicationNo)
        {

            String message = "तुमच्या अर्ज क्र. " + ApplicationNo + " ची तांत्रिक पडताळणी  यशस्वी रित्या पूर्ण झालेली आहे.  पूर्व-संमती  साठी आपला अर्ज उपविभागीय कृषि अधिकारी यांचेकडे पाठविला आहे.";
            SendSMS(message, MobileNumber, "1407161535756816935");
            return 1;
        }

        public static int SendSmsOnPaymentDisbursal(String MobileNumber, String ApplicationNo)
        {
            String message = "ना.दे.कृ.सं. प्रकल्पासाठी प्रकल्पांतर्गत संदर्भ क्रमांक  " + ApplicationNo + "  असलेल्या घटकासाठी रु. __________ च्या देय अनुदान रु. ______________ यशस्वीरित्या आपल्या बँक खात्यात हस्तांतरित केले गेले आहे.";
            SendSMS(message, MobileNumber, "1407161535768814634");

            return 1;
        }

        public static int SendSmsOnRejection(String MobileNumber, String ApplicationNo, String Reason)
        {

            String message = "ना.दे.कृ.सं. प्रकल्पांतर्गत आपला संदर्भ क्र.  " + ApplicationNo + " चा अर्ज  " + Reason + " या कारणासाठी अपात्र झालेला आहे. याबाबत आपणास काही तक्रार असल्यास आपण ७ दिवसात VCRMC अध्यक्ष/समूह सहाय्यकाकडे संपर्क साधावा.";
            SendSMS(message, MobileNumber, "1407161535776720777");
            return 1;
        }

        public static int SendSmsOnPrePostSanctionPhysicalVerification(String MobileNumber, String ApplicationNo, String Name, String Activity)
        {
            MyClass cla = new MyClass();
            //Activity = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityNameMr=N'" + Activity + "' and a.IsDeleted is null )");
            String a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityNameMr=N'" + Activity + "' and a.IsDeleted is null )");
            if (a.Trim().Length == 0)
            {
                a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityName='" + Activity + "' and a.IsDeleted is null )");
            }
            Activity = a;


            String message = "लाभार्थी (नाव) " + Name + " संदर्भ क्र. " + ApplicationNo + " ने  " + Activity + "  घटकासाठी अर्ज केला आहे. आपण त्याची स्थळ पाहणी करण्यास विनंती आहे.";
            SendSMS(message, MobileNumber, "1407161535787095027");
            return 1;
        }//String Village,
        public static int SendSmsOnPreSanctionApproval(String MobileNumber, String Activity)
        {
            MyClass cla = new MyClass();
            //Activity = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityNameMr=N'" + Activity + "' and a.IsDeleted is null )");
            String a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityNameMr=N'" + Activity + "' and a.IsDeleted is null )");
            if (a.Trim().Length == 0)
            {
                a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityName='" + Activity + "' and a.IsDeleted is null )");
            }
            Activity = a;

            String message = "ना.दे.कृ.सं.अंतर्गत  " + Activity + " घटकासाठी आपणास पूर्व - संमती मिळाली आहे.  आपण आपले काम सुरू करून विहित मुदतीत पूर्ण करावयाचे आहे आणि डीबीटी सिस्टममध्ये स्थिती अद्ययावत करू शकता.";// " ना.दे.कृ.सं.अंतर्गत  " + Activity + "  घटकासाठी आपणास पूर्व - संमती मिळाली आहे.  आपण आपले काम सुरू करून विहित मुदतीत पूर्ण करावयाचे आहे आणि डीबीटी सिस्टममध्ये स्थिती अद्ययावत करू शकता.";
            SendSMS(message, MobileNumber, "1407161535791396358");

            return 1;
        }

        public static int SendSmsOnSuccessfulPrePostSanctionPhysicalVerification(String MobileNumber, String RegistrationID, String ApplicationNo)
        {
            String message = "तुमच्या अर्ज क्र.  " + ApplicationNo + " ची तांत्रिक पडताळणी  यशस्वी रित्या पूर्ण झालेली आहे.  पूर्व-संमती  साठी आपला अर्ज उपविभागीय कृषि अधिकारी यांचेकडे पाठविला आहे.";
            SendSMS(message, MobileNumber, "1407161535756816935");
            return 1;
        }

        public static int SendSmsOnVerification(String MobileNumber, String RegistrationID, String ApplicationNo)
        {
            String message = "ना.दे.कृ.सं.प्रकल्पांतर्गत आपल्या अर्जाची पडताळणी यशस्वीरीत्या पूर्ण झाली आहे.तुमचा नोंदणी क्रमांक " + ApplicationNo + " आहे.तुमचा लॉगइन आयडी तुमचा आधार क्रमांक असेल.";// "ना.दे.कृ.सं.प्रकल्पांतर्गत आपल्या अर्जाची पडताळणी यशस्वीरीत्या पूर्ण झाली आहे.तुमचा नोंदणी क्रमांक  " + ApplicationNo + " आहे.तुमचा लॉगइन आयडी तुमचा आधार क्रमांक असेल.";
            SendSMS(message, MobileNumber, "1407161535802986285");
            return 1;
        }


        public static int SendSmsOnPreSanctionCancelation(String MobileNumber, String Activity, String subdivision)
        {
            MyClass cla = new MyClass();
            //Activity = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityNameMr=N'" + Activity + "' and a.IsDeleted is null )");
            String a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityNameMr=N'" + Activity + "' and a.IsDeleted is null )");
            if (a.Trim().Length == 0)
            {
                a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityName='" + Activity + "' and a.IsDeleted is null )");
            }
            Activity = a;

            String message = "ना. दे. कृ. सं. प्र.  ( PoCRA ) तून " + Activity + " बाबीच्या लाभाकरिता दिलेल्या पूर्वसंमती पत्रानुसार मंजूर बाब राबविण्यासाठीचा नमूद कालावधी संपून गेल्याने आपणास देण्यात आलेली पूर्वसंमती रद्द झालेली आहे.  सदर बाबीचा लाभ आवश्यक असल्यास आपण नव्याने अर्ज करू शकता. नियम , अटी व शर्तीनुसार आपल्या अर्जाचा विचार करण्यात येईल. उ. वि. कृ. अ. " + subdivision + "";
            //" ना.दे.कृ.सं.अंतर्गत  " + Activity + "  घटकासाठी आपणास पूर्व - संमती मिळाली आहे.  आपण आपले काम सुरू करून विहित मुदतीत पूर्ण करावयाचे आहे आणि डीबीटी सिस्टममध्ये स्थिती अद्ययावत करू शकता.";
            SendSMS(message, MobileNumber, "1407161535811209273");

            return 1;
        }
        public static int SendSmsOnPreSanctionHold(String MobileNumber, String Activity, String subdivision)
        {
            MyClass cla = new MyClass();
            // Activity = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityNameMr=N'" + Activity + "' and a.IsDeleted is null )");
            String a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityNameMr=N'" + Activity + "' and a.IsDeleted is null )");
            if (a.Trim().Length == 0)
            {
                a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityName='" + Activity + "' and a.IsDeleted is null )");
            }
            Activity = a;
            String message = "" + Activity + " साठी काही कारणास्तव आपला अर्ज स्थगित ठेवण्यात आला आहे.  ना.दे.कृ.सं.प्र.";
            //" ना.दे.कृ.सं.अंतर्गत  " + Activity + "  घटकासाठी आपणास पूर्व - संमती मिळाली आहे.  आपण आपले काम सुरू करून विहित मुदतीत पूर्ण करावयाचे आहे आणि डीबीटी सिस्टममध्ये स्थिती अद्ययावत करू शकता.";
            SendSMS(message, MobileNumber, "1407161535816680050");

            return 1;
        }

        //--------------------------nrm-------------------------------

        public static int SendSmsOnDesk4approval(String MobileNumber, String Activity)
        {
            MyClass cla = new MyClass();
            //Activity = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityNameMr=N'" + Activity + "' and a.IsDeleted is null )");
            String a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityNameMr=N'" + Activity + "' and a.IsDeleted is null )");
            if (a.Trim().Length == 0)
            {
                a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityName='" + Activity + "' and a.IsDeleted is null )");
            }
            Activity = a;
            String message = "आपणास " + Activity + " साठी  कार्यारंभ आदेश देण्यात येत आहे. ना.दे.कृ.सं.प्र. ";
            SendSMS(message, MobileNumber, "1407161519417657020");

            return 1;
        }

        public static int SendSmsOnDesk5approval(String MobileNumber, String Activity)
        {
            MyClass cla = new MyClass();
            //Activity = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityNameMr=N'" + Activity + "' and a.IsDeleted is null )");
            String a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityNameMr=N'" + Activity + "' and a.IsDeleted is null )");
            if (a.Trim().Length == 0)
            {
                a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityName='" + Activity + "' and a.IsDeleted is null )");
            }
            Activity = a;
            String message = " " + Activity + " साठी काम सुरु करण्यापूर्वीचा फोटो अपलोड करण्यात आलेला आहे, तरी कामास सुरुवात करण्यात यावी. ना.दे.कृ.सं.प्र.   ";
            SendSMS(message, MobileNumber, "1407161519451690222");

            return 1;
        }
        public static int SendSmsOnDesk7approval(String MobileNumber, String Activity)
        {
            MyClass cla = new MyClass();
            //Activity = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityNameMr=N'" + Activity + "' and a.IsDeleted is null )");
            String a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityNameMr=N'" + Activity + "' and a.IsDeleted is null )");
            if (a.Trim().Length == 0)
            {
                a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityName='" + Activity + "' and a.IsDeleted is null )");
            }
            Activity = a;
            String message = " " + Activity + " चे काम पूर्ण झाले असून APP मध्ये अपडेट करण्यात आले आहेत. ना.दे.कृ.सं.प्र.  ";
            SendSMS(message, MobileNumber, "1407161519462251140");

            return 1;

        }
        public static int SendSmsOnDesk9approval(String MobileNumber, String Activity)
        {
            MyClass cla = new MyClass();
            // Activity = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityNameMr=N'" + Activity + "' and a.IsDeleted is null )");
            String a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityNameMr=N'" + Activity + "' and a.IsDeleted is null )");
            if (a.Trim().Length == 0)
            {
                a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityName='" + Activity + "' and a.IsDeleted is null )");
            }
            Activity = a;
            String message = "" + Activity + " साठी सादर  कागदपत्रास लेखाधिका-यांनी  मान्यता दिलेली  आहे. ना.दे.कृ.सं.प्र.  ";
            SendSMS(message, MobileNumber, "1407161519466512075");

            return 1;
        }

        public static int SendSmsOnDesk10approval(String MobileNumber, String Activity)
        {
            MyClass cla = new MyClass();
            //Activity = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityNameMr=N'" + Activity + "' and a.IsDeleted is null )");
            String a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityNameMr=N'" + Activity + "' and a.IsDeleted is null )");
            if (a.Trim().Length == 0)
            {
                a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityName='" + Activity + "' and a.IsDeleted is null )");
            }
            Activity = a;
            String message = "" + Activity + " साठी अदा करावयाच्या रकमेस   उपविभागीय कृषी अधिकारी  यांनी  अंतिम मान्यता दिलेली  आहे. ना.दे.कृ.सं.प्र. ";
            SendSMS(message, MobileNumber, "1407161519470050580");

            return 1;
        }
        public static int SendSmsOnNRMPaymentDone(String MobileNumber, String Activity, String Amount)
        {
            MyClass cla = new MyClass();
            // Activity = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityNameMr=N'" + Activity + "' and a.IsDeleted is null )");
            String a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityNameMr=N'" + Activity + "' and a.IsDeleted is null )");
            if (a.Trim().Length == 0)
            {
                a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityName='" + Activity + "' and a.IsDeleted is null )");
            }
            Activity = a;
            String message = "" + Activity + " साठी देय रक्कम रु " + Amount + " आपण दिलेल्या बँक खात्यात हस्तांतरित केले आहे. ना.दे.कृ.सं.प्र.  ";
            SendSMS(message, MobileNumber, "1407161519473491273");

            return 1;
        }
        public static int SendSmsOnRejectionOfBill(String MobileNumber, String Activity)
        {
            MyClass cla = new MyClass();
            //Activity = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityNameMr=N'" + Activity + "' and a.IsDeleted is null )");
            String a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityNameMr=N'" + Activity + "' and a.IsDeleted is null )");
            if (a.Trim().Length == 0)
            {
                a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityName='" + Activity + "' and a.IsDeleted is null )");
            }
            Activity = a;
            String message = "" + Activity + " चे बिल नाकारण्यात आलेले आहे. कृपया याबाबत संबधित कृषी सहाय्यकाशी संपर्क साधावा. ना.दे.कृ.सं.प्र.  ";
            SendSMS(message, MobileNumber, "1407161519477888218");

            return 1;
        }
        public static int SendSmsOnreclaimingsecuritydeposit(String MobileNumber, String Activity)
        {
            MyClass cla = new MyClass();
            //Activity = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityNameMr=N'" + Activity + "' and a.IsDeleted is null )");
            String a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityNameMr=N'" + Activity + "' and a.IsDeleted is null )");
            if (a.Trim().Length == 0)
            {
                a = cla.GetExecuteScalar("Select SMSGroupName from Tbl_M_Activity_Groups where ActivityGroupID=(Select top 1 a.ActivityGroupID from Tbl_M_ActivityMaster A where a.ActivityName='" + Activity + "' and a.IsDeleted is null )");
            }
            Activity = a;
            String message = "" + Activity + " कामास एक वर्ष पूर्ण झाले असून आपण सुरक्षित ठेव मागणी करावी. ना.दे.कृ.सं.प्र. ";
            SendSMS(message, MobileNumber, "1407161519481306873");

            return 1;
        }




    }
}