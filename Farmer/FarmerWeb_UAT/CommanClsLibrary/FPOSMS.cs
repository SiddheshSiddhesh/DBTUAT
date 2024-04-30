using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanClsLibrary
{
    public class FPOSMS
    {

        

        public static int FPCRegScrutinyBacktoApplicant(String MobileNumber, String FPORegistrationID)
        {
            String message = "शेतकरी गटाच्या डीबीटी पोर्टलवर आपल्या नोंदणी तपशीलामधील त्रुटींची पूर्तता करून अद्ययावत करा. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003588499197");
            return 1;
        }
        public static int FPCRegScrutinyRejection(String MobileNumber, String FPORegistrationID)
        {
            String message = "शेतकरी गटाच्या डीबीटी पोर्टलवर अपरिहार्य तपशिलाच्या अभावामुळे आपली नोंदणी रद्द करण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003594878695");
            return 1;
        }

        public static int SendSmsOnApplication(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID="+ FPOApplicationID + "");

            String message = "प्रकल्पांतर्गत शेतकरी गटाच्या डीबीटी पोर्टलवर "+ str + " या घटकाकरिता आपला अर्ज यशस्वीरीत्या प्राप्त झालेला आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003575431102");
            return 1;
        }



        



        //--------ApplScrutiny-------------------------------------------------------------------------------
        public static int FPCApplScrutinySuccessful(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "प्रकल्पांतर्गत शेतकरी गटाच्या डीबीटी पोर्टलवर " + str + " या घटकाकरिता आपण केलेल्या अर्जाची पडताळणी यशस्वीरीत्या झालेली आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003603264735");
            return 1;
        }

        public static int FPCApplScrutinyBacktoApplicant(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "शेतकरी गटाच्या डीबीटी पोर्टलवर आपण  " + str + " या घटकाकरिता केलेल्या अर्जातील तपशीलामधील त्रुटींची पूर्तता करून अद्ययावत करा. ना.दे.कृ.सं.प्र";
            SMS.SendSMS(message, MobileNumber, "1407162003611780180");
            return 1;
        }

        public static int FPCApplScrutinyRejected(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "शेतकरी गटाच्या डीबीटी पोर्टलवर आपण " + str + " या घटकाकरिता केलेला अर्ज अपरिहार्य तपशिल नसल्याकारणाने रद्द करण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003624885854");
            return 1;
        }
        //------------------------------------------------------------------------------------------------





        //--------Pre-Sanction desk-------------------------------------------------------------------------------
        public static int FPCPreSancBacktostage(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = ""+ str + " या घटकाकरिता केलेला अर्ज त्रुटी पूर्ततेकरिता  पडताळणी  डेस्कवर पुनश्च पाठविण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003639256278");
            return 1;
        }

        public static int FPCPreSancRejection(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "शेतकरी गटाच्या डीबीटी पोर्टलवर आपण " + str + " या घटकाकरिता केलेला अर्ज  राबविण्याकरिता अयोग्य असल्याकारणाने रद्द करण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003649064723");
            return 1;
        }

        public static int FPCPreSancApproved(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता केलेल्या अर्जास तत्वत: पुर्वसंमतीस शिफारस करण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003658939764");
            return 1;
        }
        //------------------------------------------------------------------------------------------------




        //--------Pre-Sanction desk AgriBusiness-------------------------------------------------------------------------------
        public static int FPCPreSancAgriBusinessBacktostage(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता केलेला अर्ज त्रुटी पूर्ततेकरिता प्रकल्प संचालक, आत्मा यांच्या डेस्कवर पडताळणीकरिता पाठविण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003691771425");
            return 1;
        }

        public static int FPCPreSancAgriBusinessRejection(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "शेतकरी गटाच्या डीबीटी पोर्टलवर आपण " + str + " या घटकाकरिता केलेला अर्ज प्रकल्प  राबविण्याकरिता अयोग्य असल्याकारणाने रद्द करण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003705108669");
            return 1;
        }

        public static int FPCPreSancAgriBusinessApproved(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता केलेल्या अर्जास तत्वत: पुर्वसंमतीस शिफारस देण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003715047589");
            return 1;
        }
        //------------------------------------------------------------------------------------------------



        //--------Pre-Sanction PD-------------------------------------------------------------------------------
        public static int FPCPreSancPDBacktostage(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता केलेला अर्ज त्रुटी पूर्ततेकरिता  कृषी व्यवसाय विशेषज्ञ यांच्या डेस्कवर पडताळणीकरिता पुनश्च  पाठविण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003734836873");
            return 1;
        }

        public static int FPCPreSancPDRejection(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "शेतकरी गटाच्या डीबीटी पोर्टलवर आपण " + str + " या घटकाकरिता केलेला अर्ज प्रकल्प  राबविण्याकरिता अयोग्य असल्याकारणाने रद्द करण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003705108669");
            return 1;
        }

        public static int FPCPrePDApproved(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता केलेल्या अर्जास तत्वत: पुर्वसंमती देण्यात येत असून त्रुटी नमूद केल्या असल्यास त्रुटींची पूर्तता पूर्ण करून मार्गदर्शक सूचनेनेनुसार आवश्यक कागदपत्रे अपलोड करावीत जेणेकरून आपणास कार्यारंभ आदेश डेटा येईल . ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003822299128");
            return 1;
        }
        //------------------------------------------------------------------------------------------------



        //--------Commencement desk (SDAO /PD ATMA)-------------------------------------------------------------------------------
        public static int FPCCommencementBacktostage(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता अर्ज केलेला असून पूर्वसंमती पत्रातील त्रुटींची तसेच नमूद केलेली आवश्यक कागदपत्रे अपलोड करण्याकरिता आपल्या स्तरावर अर्ज पाठविण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003836597702");
            return 1;
        }

        public static int FPCCommencementRejection(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "शेतकरी गटाच्या डीबीटी पोर्टलवर आपण " + str + " या घटकाकरिता अर्ज केलेला असून  पूर्वसंमती पत्रातील त्रुटींची पूर्तता न केल्यामुळे  आणि /किंवा  कागदपत्रांच्या अभावामुळे आपला अर्ज रद्द करण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003845483815");
            return 1;
        }

        public static int FPCCommencementpproved(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता केलेल्या अर्जास कार्यारंभ आदेश देण्यात येत असून मार्गदर्शक सूचनेनेनुसार व जागतिक बँकेच्या प्रापण नियमानुसार घटकाची अंमलबजावणी करावी. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003853257242");
            return 1;
        }
        //------------------------------------------------------------------------------------------------


        //--------ScrutinyDesk3-------------------------------------------------------------------------------
        public static int FPCScrutinyDesk3Backtostage(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता अनुदान मागणी करण्यात आलेली असून त्यामध्ये नमूद त्रुटींची पूर्तता करून सदर अनुदान मागणी अद्ययावत करण्याकरिता आपल्या स्तरावर अर्ज पाठविण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003861614080");
            return 1;
        }

        public static int FPCScrutinyDesk3Rejection(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "शेतकरी गटाच्या डीबीटी पोर्टलवर आपण " + str + " या घटकाकरिता अर्ज केलेला असून  प्रापण प्रक्रियेमधील विसंगती आणि/किंवा  चुकीच्या देयकांमुळे आपला अर्ज रद्द करण्यात येत आहे. ना.दे.कृ.सं.प्र";
            SMS.SendSMS(message, MobileNumber, "1407162003868533139");
            return 1;
        }

        public static int FPCScrutinyDesk3aproved(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता अर्ज केलेला असून सदर अर्जास पुढील कार्यवाहीकरिता शिफारस करण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003875885644");
            return 1;
        }
        //------------------------------------------------------------------------------------------------



        //--------Postworksiteinspection-------------------------------------------------------------------------------
        public static int FPCPostworksiteinspectionBacktostage(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता अनुदान मागणी मधील नमूद केलेल्या त्रुटींची पडताळणी करण्याकरिता प्रकल्प विशेषज्ञ-प्रापण यांच्याकडे सदर अर्ज पुनश्च पाठविण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003861614080");
            return 1;
        }

        public static int FPCPostworksiteinspectionRejection(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता अर्ज केलेला असून प्रकल्पाची उभारणी मार्गदर्शक सुचनेनुसार झाली नसल्यामुळे आपला अर्ज रद्द करण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003902689418");
            return 1;
        }

        public static int FPCPostworksiteinspectionaproved(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता अर्ज केलेला असून देय अनुदानास शिफारस करण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003910108631");
            return 1;
        }
        //------------------------------------------------------------------------------------------------



        //--------Accountofficer-5 -------------------------------------------------------------------------------
        public static int FPCAccountofficerBacktostage(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता अनुदान परिगणनेमधील नमूद त्रुटींची पडताळणी करण्याकरिता मोका तपासणी अधिकारी यांच्याकडे सदर अर्ज पुनश्च पाठविण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003924613712");
            return 1;
        }

        public static int FPCAccountofficerRejection(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता अर्ज केलेला असून  अनुदान मागणी अयोग्य असल्यामुळे  सदर अनुदान मागणी रद्द करण्यात येत आहे. आपण नव्याने अनुदान मागणी करावी. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003937576470");
            return 1;
        }

        public static int FPCAccountofficeraproved(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता अर्ज केलेला असून देय अनुदानास पुढील कार्यवाहीकरिता शिफारस करण्यात येत आहे. ना.दे.कृ.सं.प्र";
            SMS.SendSMS(message, MobileNumber, "1407162003944457796");
            return 1;
        }
        //------------------------------------------------------------------------------------------------



        //--------PaymentapprovalATMA -5 -------------------------------------------------------------------------------
        public static int FPCPaymentapprovalATMABacktostage(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता अनुदान परिगणनेमधील नमूद त्रुटींची पडताळणी करण्याकरिता लेखाधिकारी यांच्याकडे सदर अर्ज पुनश्च पाठविण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162004016965777");
            return 1;
        }

        public static int FPCPaymentapprovalATMARejection(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता अर्ज केलेला असून देयकांमध्ये तफावत असल्यामुळे सदर अनुदान मागणी रद्द करण्यात येत आहे. आपण नव्याने अनुदान मागणी करावी. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003997776435");
            return 1;
        }

        public static int FPCPaymentapprovalATMAaproved(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता अर्ज केलेला असून देय अनुदानास पुढील कार्यवाहीकरिता शिफारस करण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003944457796");
            return 1;
        }
        //------------------------------------------------------------------------------------------------



        //--------PaymentAgriBusiness -5 -------------------------------------------------------------------------------
        public static int FPCPaymentAgriBusinessBacktostage(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता अनुदान परिगणनेमधील नमूद त्रुटींची पडताळणी करण्याकरिता प्रकल्प संचालक, आत्मा यांच्याकडे सदर अर्ज पुनश्च पाठविण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162004060622844");
            return 1;
        }

        public static int FPCPaymentAgriBusinessRejection(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता अर्ज केलेला असून  अनुदान मागणी चुकीची  असल्यामुळे सदर अनुदान मागणी रद्द करण्यात येत आहे. आपण नव्याने अनुदान मागणी करावी. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162004067774821");
            return 1;
        }

        public static int FPCPaymentAgriBusinessAaproved(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता अर्ज केलेला असून देय अनुदानास पुढील कार्यवाहीकरिता शिफारस करण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003944457796");
            return 1;
        }
        //------------------------------------------------------------------------------------------------



        //--------FinanceSpecialist -5 -------------------------------------------------------------------------------
        public static int FPCPaymentFinanceSpecialistBacktostage(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता अनुदान परिगणनेमधील नमूद त्रुटींची पडताळणी करण्याकरिता कृषी व्यवसाय विशेषज्ञ यांच्याकडे सदर अर्ज पुनश्च पाठविण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162004137881229");
            return 1;
        }

        public static int FPCFinanceSpecialistRejection(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता अर्ज केलेला असून  अनुदान मागणी चुकीची  असल्यामुळे सदर अनुदान मागणी रद्द करण्यात येत आहे. आपण नव्याने अनुदान मागणी करावी. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162004067774821");
            return 1;
        }

        public static int FPCFinanceSpecialistAaproved(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता अर्ज केलेला असून देय अनुदानास पुढील कार्यवाहीकरिता शिफारस करण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162003944457796");
            return 1;
        }
        //------------------------------------------------------------------------------------------------



        //--------PaymentPD -5 -------------------------------------------------------------------------------
        public static int FPCPaymentPDBacktostage(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता अनुदान परिगणनेमधील नमूद त्रुटींची पडताळणी करण्याकरिता वित्त विशेषज्ञ यांच्याकडे सदर अर्ज पुनश्च  पाठविण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162004205433012");
            return 1;
        }

        public static int FPCPaymentPDRejection(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता अर्ज केलेला असून  अनुदान मागणी चुकीची  असल्यामुळे सदर अनुदान मागणी रद्द करण्यात येत आहे. आपण नव्याने अनुदान मागणी करावी. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162004067774821");
            return 1;
        }

        public static int FPCPaymentPDAaproved(String MobileNumber, String FPOApplicationID)
        {
            MyClass cla = new MyClass();
            String str = cla.GetExecuteScalar("Select top 1 t.ActivityNameMr+' & '+t.ActivityCode +'..'  from FPO_A_FPOApplication_Activity as A inner join Tbl_M_ActivityMaster t on t.ActivityID=A.ActivityID  where A.IsDeleted is null and A.FPOApplicationID=" + FPOApplicationID + "");

            String message = "" + str + " या घटकाकरिता अर्ज केलेला असून देय अनुदान अदायगीस मंजुरी देण्यात येत आहे. ना.दे.कृ.सं.प्र.";
            SMS.SendSMS(message, MobileNumber, "1407162004231598337");
            return 1;
        }
        //------------------------------------------------------------------------------------------------




    }




}
