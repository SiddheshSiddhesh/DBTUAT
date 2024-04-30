using CommanClsLibrary;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace DBTPoCRA
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // System.Net.ServicePointManager.CertificatePolicy = new CustomCertificatePolicy();
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
            | SecurityProtocolType.Tls11
            | SecurityProtocolType.Tls12
            | SecurityProtocolType.Ssl3;
        }

        

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            Response.AddHeader("Cache-Control", "max-age=0,no-cache,no-store,must-revalidate");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Expires", "Tue, 01 Jan 1970 00:00:00 GMT");

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            
        }
        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            // Remove the "Server" HTTP Header from response
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-AspNet-Version");
            //Response.Headers.Remove("X-AspNetMvc-Version");
            try
            {
                var app = sender as HttpApplication;
                if (app != null && app.Context != null)
                {
                    app.Context.Response.Headers.Remove("Server");
                }
            }
            catch { }
            try
            {
                HttpApplication app = sender as HttpApplication;
                if (null != app)
                {
                    NameValueCollection headers = app.Context.Response.Headers;
                    if (null != headers)
                    {
                        headers.Remove("Server");
                    }
                }
            }
            catch { }
        }

        public void Application_PreRequestHandlerExecute(Object sender, EventArgs e)
        {
            try
            {
                // The code below is intended to block incoming HTTP GET requests which contains in query string parameters intended to be used in webform POST
                if (Request.HttpMethod != "GET")
                {
                    return; // Nothing to do
                }

                var hasPostParams = (Request.QueryString["__EVENTTARGET"] ??
                                       Request.QueryString["__VIEWSTATE"] ??
                                       Request.QueryString["__EVENTARGUMENT"] ??
                                       Request.QueryString["__EVENTVALIDATION"]) != null;

                if (hasPostParams)
                {
                    // TODO : log error (I suggest to also log HttpContext.Current.Request.RawUrl) and throw new exception
                    Util.LogError("method interchange attack");
                    Response.Redirect("~/ErrorLog.aspx?message=404", false);
                }
            }
            catch { }
        }


        protected void Application_Error(object sender, EventArgs e)
        {
            //var exception = Server.GetLastError();
            //Server.ClearError();

            try
            {
                Exception exception = Server.GetLastError();
                Response.Clear();
                //Util.LogError("UnHandald");
                Util.LogError(exception);
                HttpException httpException = exception as HttpException;

                if (httpException != null)
                {
                    string action;

                    switch (httpException.GetHttpCode())
                    {
                        case 404:
                            // page not found
                            action = "404";
                            break;
                        case 500:
                            // server error
                            action = "500";
                            break;
                        default:
                            action = "100";
                            break;
                    }

                    // clear error on server
                    Server.ClearError();

                    Response.Redirect("~/ErrorLog.aspx?message=" + action,false);
                }
            }
            catch
            {
                Response.Redirect("~/ErrorLog.aspx?message=001",false);
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {
           
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
      
    }

    public class CustomCertificatePolicy : ICertificatePolicy
    {
        public bool CheckValidationResult(ServicePoint sp, X509Certificate cert, WebRequest req, int problem)
        {
            //* Return "true" to force the certificate to be accepted.
            return true;
        }
    }
}