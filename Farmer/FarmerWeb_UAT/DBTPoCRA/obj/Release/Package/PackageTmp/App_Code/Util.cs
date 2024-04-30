using System;
using System.Web.UI;
using System.IO;
using System.Web;

/// <summary>
/// Summary description for Util
/// </summary>


public class Util
{
   

    public Util()
    {

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
            path = path + "/ErroLog.txt";
            string seperator = "-------------------------------------------\n\r";
            File.AppendAllText(path,
                seperator + DateTime.Now.ToString() + "\n\r" + message + "\n\r" + seperator);


        }
        catch { }

    }
    public static void LogError(String ex, String UserID, String PageName)
    {
        try
        {
            String PathUp = "~/DocMasters/ErroLog";
            String path = System.Web.HttpContext.Current.Server.MapPath(PathUp);
            if (!Directory.Exists(path))
            {
                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            path = path + "/ErroLog.txt";
            string seperator = "---------------UserID - " + UserID + "--------------------------PageName --" + PageName + "----------------------------\n\r";
            File.AppendAllText(path,
                seperator + DateTime.Now.ToString() + "\n\r" + ex.ToString() + "\n\r" + seperator);
        }
        catch { }

    }


   

    public static void ShowMessageBox(Control control, string Title, string msg, string msgtype)
    {
        ShowMessageBox(control, Title, msg, msgtype, "");

    }
    public static void ShowMessageBox(Control control, string Title, string msg, string msgtype, string redirect)
    {
        if (redirect != string.Empty)
        {
            redirect = "window.location='" + redirect + "';";
        }
        Title = Title.Replace("'", "`");
        ScriptManager.RegisterClientScriptBlock(control, HttpContext.Current.GetType(), "Popup","sweetAlert('" + Title + "', '" + msg.Replace("'", "`") + "', '" + msgtype + "');" + redirect, true);
    }
}