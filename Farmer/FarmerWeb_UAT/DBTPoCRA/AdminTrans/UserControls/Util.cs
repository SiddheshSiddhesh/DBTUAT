using System;
using System.Web.UI;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Net;
using Newtonsoft.Json;
using System.Globalization;
using AjaxControlToolkit;

/// <summary>
/// Summary description for Util
/// </summary>


public class Util
{


    public Util()
    {

    }

    //public static void LogErrorAadhar(String error)
    //{
    //    try
    //    {

    //        string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
    //        message += Environment.NewLine;
    //        message += "-----------------------------------------------------------";
    //        message += Environment.NewLine;
    //        message += string.Format("Message: {0}", error);
    //        message += Environment.NewLine;

    //        try
    //        {
    //            message += Environment.NewLine;
    //            message += string.Format("UserID: {0}", HttpContext.Current.Session["UserId"].ToString());
    //            message += Environment.NewLine;
    //        }
    //        catch { }

    //        message += "-----------------------------------------------------------";
    //        message += Environment.NewLine;

    //        try
    //        {
    //            // IPAddress =GetIPAddress(HttpContext.Current.Request);
    //            //if (HttpContext.Current.Request.Headers["CF-CONNECTING-IP"] != null) IPAddress= HttpContext.Current.Request.Headers["CF-CONNECTING-IP"].ToString();
    //            //if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null) IPAddress= HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();

    //            string ipaddress;
    //            ipaddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
    //            if (ipaddress == "" || ipaddress == null)
    //                ipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];


    //            message += string.Format("IPAddress: {0}", ipaddress.ToString());
    //            message += Environment.NewLine;
    //            message += "-----------------------------------------------------------";
    //            message += Environment.NewLine;
    //        }
    //        catch
    //        {

    //        }


    //        String PathUp = "~/DocMasters/ErroLog";
    //        String path = System.Web.HttpContext.Current.Server.MapPath(PathUp);
    //        if (!Directory.Exists(path))
    //        {
    //            // Try to create the directory.
    //            DirectoryInfo di = Directory.CreateDirectory(path);
    //        }
    //        path = path + "/LogErrorAadhar.txt";
    //        string seperator = "-------------------------------------------\n\r";
    //        File.AppendAllText(path,
    //            seperator + DateTime.Now.ToString() + "\n\r" + message + "\n\r" + seperator);


    //    }
    //    catch { }

    //}

    public static void LogError(Exception ex, String StrPageName, String Query)
    {
        try
        {

            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("PageName: {0}", StrPageName);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
            message += Environment.NewLine;
            message += string.Format("Exception: {0}", ex.ToString());
            message += Environment.NewLine;

            message += string.Format("Query: {0}", Query.ToString());
            message += Environment.NewLine;

            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            try
            {
                message += Environment.NewLine;
                message += string.Format("UserID: {0}", HttpContext.Current.Session["UserId"].ToString());
                message += Environment.NewLine;
            }
            catch { }

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


            String PathUp = "~/DocMasters/ErroLog";
            String path = System.Web.HttpContext.Current.Server.MapPath(PathUp);
            if (!Directory.Exists(path))
            {
                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            path = path + "/ErroLog" + DateTime.Now.ToString("MM/dd/yyyy").Replace("/", "") + ".txt";

            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                string seperator = "-------------------------------------------\n\r";
                File.AppendAllText(path,
                    seperator + DateTime.Now.ToString() + "\n\r" + message + "\n\r" + seperator);

            }
            else
            {
                using (StreamWriter sw = fi.CreateText())
                {
                    sw.WriteLine(message);
                }
            }

        }
        catch { }

    }

    public static void FPOLogError(Exception ex)
    {
        try
        {

            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", ex.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
            message += Environment.NewLine;
            message += string.Format("Exception: {0}", ex.ToString());
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            try
            {
                message += Environment.NewLine;
                message += string.Format("UserID: {0}", HttpContext.Current.Session["UserId"].ToString());
                message += Environment.NewLine;
            }
            catch { }

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


            String PathUp = "~/DocMasters/FPOErroLog";
            String path = System.Web.HttpContext.Current.Server.MapPath(PathUp);
            if (!Directory.Exists(path))
            {
                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            path = path + "/ErroLog" + DateTime.Now.ToString("MM/dd/yyyy").Replace("/", "") + ".txt";

            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                string seperator = "-------------------------------------------\n\r";
                File.AppendAllText(path,
                    seperator + DateTime.Now.ToString() + "\n\r" + message + "\n\r" + seperator);

            }
            else
            {
                using (StreamWriter sw = fi.CreateText())
                {
                    sw.WriteLine(message);
                }
            }

        }
        catch { }

    }

    public static void LogError(Exception ex)
    {
        try
        {

            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", ex.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
            message += Environment.NewLine;
            message += string.Format("Exception: {0}", ex.ToString());
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            try
            {
                if (HttpContext.Current.Session["UserId"] != null)
                {
                    message += Environment.NewLine;
                    message += string.Format("UserID: {0}", HttpContext.Current.Session["UserId"].ToString());
                    message += Environment.NewLine;
                }
            }
            catch { }

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


            String PathUp = "~/DocMasters/ErroLog";
            String path = System.Web.HttpContext.Current.Server.MapPath(PathUp);
            if (!Directory.Exists(path))
            {
                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            path = path + "/ErroLog" + DateTime.Now.ToString("MM/dd/yyyy").Replace("/", "") + ".txt";

            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                string seperator = "-------------------------------------------\n\r";
                File.AppendAllText(path,
                    seperator + DateTime.Now.ToString() + "\n\r" + message + "\n\r" + seperator);

            }
            else
            {
                using (StreamWriter sw = fi.CreateText())
                {
                    sw.WriteLine(message);
                }
            }

        }
        catch { }

    }


    public static void LogErrorFFS(String error)
    {
        try
        {

            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", error);
            message += Environment.NewLine;

            try
            {
                message += Environment.NewLine;
                message += string.Format("UserID: {0}", HttpContext.Current.Session["UserId"].ToString());
                message += Environment.NewLine;
            }
            catch { }

            message += "-----------------------------------------------------------";
            message += Environment.NewLine;

            try
            {
                // IPAddress =GetIPAddress(HttpContext.Current.Request);
                //if (HttpContext.Current.Request.Headers["CF-CONNECTING-IP"] != null) IPAddress= HttpContext.Current.Request.Headers["CF-CONNECTING-IP"].ToString();
                //if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null) IPAddress= HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();

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


            String PathUp = "~/DocMasters/ErroLog";
            String path = System.Web.HttpContext.Current.Server.MapPath(PathUp);
            if (!Directory.Exists(path))
            {
                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            path = path + "/FFS.txt";
            string seperator = "-------------------------------------------\n\r";
            File.AppendAllText(path,
                seperator + DateTime.Now.ToString() + "\n\r" + message + "\n\r" + seperator);


        }
        catch { }

    }



    public static void LogError(String error)
    {
        try
        {

            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", error);
            message += Environment.NewLine;

            try
            {
                if (HttpContext.Current.Session["UserId"] != null)
                {
                    message += Environment.NewLine;
                    message += string.Format("UserID: {0}", HttpContext.Current.Session["UserId"].ToString());
                    message += Environment.NewLine;
                }
            }
            catch { }

            message += "-----------------------------------------------------------";
            message += Environment.NewLine;

            try
            {
                // IPAddress =GetIPAddress(HttpContext.Current.Request);
                //if (HttpContext.Current.Request.Headers["CF-CONNECTING-IP"] != null) IPAddress= HttpContext.Current.Request.Headers["CF-CONNECTING-IP"].ToString();
                //if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null) IPAddress= HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();

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


            String PathUp = "~/DocMasters/ErroLog";
            String path = System.Web.HttpContext.Current.Server.MapPath(PathUp);
            if (!Directory.Exists(path))
            {
                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            path = path + "/ErroLog.txt";
            string seperator = "-------------------------------------------\n\r";
            File.AppendAllText(path,
                seperator + DateTime.Now.ToString() + "\n\r" + message + "\n\r" + seperator);


        }
        catch { }

    }

    public static void LogErrorNNPCI(String error)
    {
        try
        {

            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", error);
            message += Environment.NewLine;

            try
            {
                message += Environment.NewLine;
                message += string.Format("UserID: {0}", HttpContext.Current.Session["UserId"].ToString());
                message += Environment.NewLine;
            }
            catch { }

            message += "-----------------------------------------------------------";
            message += Environment.NewLine;

            try
            {
                // IPAddress =GetIPAddress(HttpContext.Current.Request);
                //if (HttpContext.Current.Request.Headers["CF-CONNECTING-IP"] != null) IPAddress= HttpContext.Current.Request.Headers["CF-CONNECTING-IP"].ToString();
                //if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null) IPAddress= HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();

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


            String PathUp = "~/DocMasters/ErroLog";
            String path = System.Web.HttpContext.Current.Server.MapPath(PathUp);
            if (!Directory.Exists(path))
            {
                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            path = path + "/NPCIErroLog.txt";
            string seperator = "-------------------------------------------\n\r";
            File.AppendAllText(path,
                seperator + DateTime.Now.ToString() + "\n\r" + message + "\n\r" + seperator);


        }
        catch { }

    }


    public static void ShowMessageBox(Control control, string Title, string msg, string msgtype)
    {

        if (msgtype.Trim().ToUpper() == "ERROR")
        {
            try
            {
                string strPath = HttpContext.Current.Server.MapPath("~/admintrans/UserControls/APPLOG.txt");// @"D:\Rekha\Log.txt";
                if (!File.Exists(strPath))
                {
                    File.Create(strPath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(strPath))
                {
                    sw.WriteLine("===========ERROR HEAD ============= " + Title.Trim());
                    sw.WriteLine("Details: " + msg);
                    sw.WriteLine("===========End============= " + DateTime.Now);
                    sw.WriteLine("========================");
                    sw.Dispose();
                }
            }
            catch
            {

            }
        }


        ShowMessageBox(control, Title, msg, msgtype, "");

    }


    public static void ShowMessageBox(Control control, string Title, string msg, string msgtype, string redirect)
    {
        if (redirect != string.Empty)
        {
            //redirect = "window.location='" + redirect + "';";
            redirect = "function Redirect() { window.location ='" + redirect + "'; }  setTimeout('Redirect()', 6000); ";
        }
        Title = Title.Replace("'", "`");
        ScriptManager.RegisterClientScriptBlock(control, HttpContext.Current.GetType(), "Popup", "sweetAlert('" + Title + "', '" + msg.Replace("'", "`") + "', '" + msgtype + "');" + redirect, true);
    }


    /// <summary>
    /// Takes the specified file extension and checks if it's acceptable. If not, an error message is returned. 
    /// </summary>
    /// <param name="FileUpload"></param>
    /// <returns>string</returns>
    public static string CheckAllowedFileName(FileUpload FileUpload1)
    {
        bool isValidFile = true;
        int count = FileUpload1.PostedFile.FileName.Split('.').Length - 1;
        if (count > 1)
        {
            isValidFile = false;
        }
        bool isExecutableFile = FileUpload1.FileBytes.Length > 1 &&
                        FileUpload1.FileBytes[0] == 0x4D &&
                        FileUpload1.FileBytes[1] == 0x5A;

        if (isExecutableFile == true)
        {
            isValidFile = false;
        }
        if (!isValidFile)
        {
            return "Invalid File name.";
        }
        else
        {
            return "";

        }
    }


    public static string CheckAllowedFileName(HttpPostedFile FileUpload1)
    {
        bool isValidFile = true;
        int count = FileUpload1.FileName.Split('.').Length - 1;
        if (count > 1)
        {
            isValidFile = false;
        }
        byte[] buffer = new byte[FileUpload1.ContentLength];
        bool isExecutableFile = FileUpload1.ContentLength > 1 &&
                        buffer[0] == 0x4D &&
                        buffer[1] == 0x5A;

        if (isExecutableFile == true)
        {
            isValidFile = false;
        }
        if (!isValidFile)
        {
            return "Invalid File name.";
        }
        else
        {
            return "";

        }
    }


    /// <summary>
    /// Takes the specified file extension and checks if it's acceptable. If not, an error message is returned. 
    /// </summary>
    /// <param name="FileUpload"></param>
    /// <returns>string</returns>
    public static string CheckAllowedFileName(AsyncFileUpload FileUpload1)
    {
        bool isValidFile = true;
        int count = FileUpload1.PostedFile.FileName.Split('.').Length - 1;
        if (count > 1)
        {
            isValidFile = false;
        }
        bool isExecutableFile = FileUpload1.FileBytes.Length > 1 &&
                        FileUpload1.FileBytes[0] == 0x4D &&
                        FileUpload1.FileBytes[1] == 0x5A;

        if (isExecutableFile == true)
        {
            isValidFile = false;
        }
        if (!isValidFile)
        {
            return "Invalid File name.";
        }
        else
        {
            return "";

        }
    }



    //public static string GetUserCountryByIp()
    //{
    //    string ip = "";
    //    try
    //    {

    //        ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
    //        if (ip == "" || ip == null)
    //            ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
    //    }
    //    catch
    //    {

    //    }

    //    IpInfo ipInfo = new IpInfo();
    //    try
    //    {
    //        string info = new WebClient().DownloadString("http://ipinfo.io/" + ip);
    //        ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
    //        RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
    //        ipInfo.Country = myRI1.EnglishName;
    //    }
    //    catch (Exception)
    //    {
    //        ipInfo.Country = null;
    //    }

    //    return ipInfo.Country;
    //}

}

//public class IpInfo
//{


//    public string Ip { get; set; }


//    public string Hostname { get; set; }


//    public string City { get; set; }


//    public string Region { get; set; }


//    public string Country { get; set; }


//    public string Loc { get; set; }


//    public string Org { get; set; }


//    public string Postal { get; set; }
//}