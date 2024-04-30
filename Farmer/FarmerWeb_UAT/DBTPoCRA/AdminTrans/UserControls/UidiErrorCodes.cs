using CommanClsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBTPoCRA.AdminTrans.UserControls
{
    public class UidiErrorCodes
    {

       // public static Dictionary<String, String> ErrorList = new Dictionary<String, string>();


        public UidiErrorCodes()
        {
           
        }

        public static String GetErrorDetails(String ErrorCode )
        {
            String ErrorMsg = "";
            MyClass cla = new MyClass();
            ErrorMsg = cla.GetExecuteScalar("SELECT  ErrorInEng FROM Tbl_M_AadharAPIError where ErrorCode='"+ ErrorCode.Trim()+ "'");
            if(ErrorMsg.Length==0)
            {
                ErrorMsg = "Response is not coming from uidai kindly try again :: "+ ErrorCode;

            }


            return ErrorMsg;
        }
    }
}