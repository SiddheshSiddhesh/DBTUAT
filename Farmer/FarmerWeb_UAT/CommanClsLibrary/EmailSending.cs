using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Text;

/// <summary>
/// Summary description for EmailSending
/// </summary>
public class EmailSending
{
    public EmailSending()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public static String SendOTP(String OTP, String SendToEmailID)
    {
        //EmailSending email = new global::EmailSending();
        //String str = "<HTML> <HEAD> </HEAD> <BODY> <div style='font-family: Helvetica,Arial,sans-serif;min-width:1000px;overflow:auto;line-height:2'>";
        //str += "<div style='margin:50px auto;width:70%;padding:20px 0'>";
        //str += "<div style='border-bottom:1px solid #eee'>";
        //str += " <a href='' style='font-size:1.4em;color: #00466a;text-decoration:none;font-weight:600'><img src='https://dbt.mahapocra.gov.in/Office/assets/img/pocra.png' alt=''  style='height:70px;weight:70px;'> POCRA - नानाजी देशमुख कृषि संजीवनी प्रकल्प </a>";
        //str += "</div>";
        //str += "<p style='font-size:1.1em'>Hi,</p>";
        //str += "<p>Use the following OTP to complete your Sign in procedures. OTP is valid for 5 minutes</p>";
        //str += "<h2 style='background: #00466a;margin: 0 auto;width: max-content;padding: 0 10px;color: #fff;border-radius: 4px;'>" + OTP + "</h2>";
        //str += "<p style='font-size:0.9em;'>Regards,<br />Nanaji Deshmukh Krushi Sanjivani Prakalp <br> Phone - 022 2216 3351 </p>";
        //str += "<hr style='border:none;border-top:1px solid #eee' />";
        //str += "<div style='float:right;padding:8px 0;color:#aaa;font-size:0.8em;line-height:1;font-weight:300'>";
        //str += "<p>POCRA</p>";
        //str += "<p>30B, Arcade, World Trade Centre, Cuffe Parade, Mumbai</p>";
        //str += " <p>Maharashtra 400005</p>";
        //str += "</div>";
        //str += "</div>";
        //str += "</div> </BODY> </HTML> ";
        //return email.SendMail(SendToEmailID, "Notification from Pocra", str);

        return "";
    }





    public static String SendReportLink(String ReportName, String FilePath, String SendToEmailID, String SendToName)
    {
        EmailSending email = new global::EmailSending();
        String str = "<HTML> <HEAD> </HEAD> <BODY> <div style='font-family: Helvetica,Arial,sans-serif;min-width:1000px;overflow:auto;line-height:2'>";
        str += "<div style='margin:50px auto;width:70%;padding:20px 0'>";
        str += "<div style='border-bottom:1px solid #eee'>";
        str += " <a href='' style='font-size:1.4em;color: #00466a;text-decoration:none;font-weight:600'><img src='https://dbt.mahapocra.gov.in/Office/assets/img/pocra.png' alt=''  style='height:70px;weight:70px;'> POCRA - नानाजी देशमुख कृषि संजीवनी प्रकल्प </a>";
        str += "</div>";
        str += "<p style='font-size:1.1em'>Hi, " + SendToName + "</p>";
        str += "<p>Please use below link to download " + ReportName + " report. </p>";
        str += "<p> Generated Date and Time : " + DateTime.Now.ToString() + "  </p>";

        str += "<h2 style='background: #00466a;margin: 0 auto;width: max-content;padding: 0 10px;color: #fff;border-radius: 4px;'> <a href='" + FilePath + "'> Click here to download </a> </h2>";

        str += "<p style='font-size:0.9em;'>Regards,<br />Nanaji Deshmukh Krushi Sanjivani Prakalp <br> Phone - 022 2216 3351 </p>";
        str += "<hr style='border:none;border-top:1px solid #eee' />";
        str += "<div style='float:right;padding:8px 0;color:#aaa;font-size:0.8em;line-height:1;font-weight:300'>";
        str += "<p>POCRA</p>";
        str += "<p>30B, Arcade, World Trade Centre, Cuffe Parade, Mumbai</p>";
        str += " <p>Maharashtra 400005</p>";
        str += "</div>";
        str += "</div>";
        str += "</div> </BODY> </HTML> ";
        return email.SendMail(SendToEmailID, "Notification from Pocra", str);
    }

    public static String SendReport(String ReportName, DataSet ds, List<String> SendToEmailID, String SendToName,String strNote)
    {
        EmailSending email = new global::EmailSending();
        String str = "<HTML> <HEAD> </HEAD> <BODY> <div style='font-family:trebuchet ms,sans-serif;min-width:1000px;overflow:auto;line-height:2;font-size: small;'>";
        str += "<div style='margin:0px auto;width:99%;padding:20px 0'>";
        str += "<div style='border-bottom:1px solid #eee'>";
        str += " <a href='' style='font-size:1.4em;color: #00466a;text-decoration:none;font-weight:600'><img src='https://dbt.mahapocra.gov.in/Office/assets/img/pocra.png' alt=''  style='height:70px;weight:70px;'> POCRA - नानाजी देशमुख कृषि संजीवनी प्रकल्प </a>";
        str += "</div>";
        str += "<p style='font-size: small;'>Hi, " + SendToName + "</p>";
        str += "<p> This is system generated report (" + ReportName + ") for your review. </p>";
        str += "<p> Generated Date and Time : " + DateTime.Now.ToString() + "  </p>";

        //str += "<h2 style='background: #00466a;margin: 0 auto;width: max-content;padding: 0 10px;color: #fff;border-radius: 4px;'> <a href='" + FilePath + "'> Click here to download </a> </h2>";
        foreach (DataTable objDt in ds.Tables)
        {
            str += "<div> <h4> " + objDt.TableName + " </h4></div>";
            str += "<div>" + DataTable_to_HTMLTable(objDt) + "</div>";
        }
        str += "<p style='font-size: x-small;'>Note-, " + strNote + "</p>";
        str += "<p style='font-size:0.9em;'>Regards,<br />Nanaji Deshmukh Krushi Sanjivani Prakalp <br> Phone - 022 2216 3351 </p>";
        str += "<hr style='border:none;border-top:1px solid #eee' />";
        str += "<div style='float:right;padding:8px 0;color:#aaa;font-size:0.8em;line-height:1;font-weight:300'>";
        str += "<p>POCRA</p>";
        str += "<p>30B, Arcade, World Trade Centre, Cuffe Parade, Mumbai</p>";
        str += " <p>Maharashtra 400005</p>";
        str += "</div>";
        str += "</div>";
        str += "</div> </BODY> </HTML> ";
        return email.SendMail(SendToEmailID, "" + ReportName + "", str);
    }

    public static String SendReport(String ReportName, String EmailData, List<String> SendToEmailID, String SendToName)
    {
        EmailSending email = new global::EmailSending();
        String str = "<HTML> <HEAD> </HEAD> <BODY> <div style='font-family:trebuchet ms,sans-serif;min-width:1000px;overflow:auto;line-height:2;font-size: small;'>";
        str += "<div style='margin:0px auto;width:99%;padding:20px 0'>";
        str += "<div style='border-bottom:1px solid #eee'>";
        str += " <a href='' style='font-size:1.4em;color: #00466a;text-decoration:none;font-weight:600'><img src='https://dbt.mahapocra.gov.in/Office/assets/img/pocra.png' alt=''  style='height:70px;weight:70px;'> POCRA - नानाजी देशमुख कृषि संजीवनी प्रकल्प </a>";
        str += "</div>";
        str += "<p style='font-size: small;'>Hi, " + SendToName + "</p>";
        str += "<p> This is system generated report (" + ReportName + ") for your review. </p>";
        str += "<p> Generated Date and Time : " + DateTime.Now.ToString() + "  </p>";

        //str += "<h2 style='background: #00466a;margin: 0 auto;width: max-content;padding: 0 10px;color: #fff;border-radius: 4px;'> <a href='" + FilePath + "'> Click here to download </a> </h2>";

        str += "<div>" + EmailData + "</div>";

        str += "<p style='font-size:0.9em;'>Regards,<br />Nanaji Deshmukh Krushi Sanjivani Prakalp <br> Phone - 022 2216 3351 </p>";
        str += "<hr style='border:none;border-top:1px solid #eee' />";
        str += "<div style='float:right;padding:8px 0;color:#aaa;font-size:0.8em;line-height:1;font-weight:300'>";
        str += "<p>POCRA</p>";
        str += "<p>30B, Arcade, World Trade Centre, Cuffe Parade, Mumbai</p>";
        str += " <p>Maharashtra 400005</p>";
        str += "</div>";
        str += "</div>";
        str += "</div> </BODY> </HTML> ";
        return email.SendMail(SendToEmailID, "" + ReportName + "", str);
    }

    public static string DataTable_to_HTMLTable(DataTable dt)
    {
        if (dt.Rows.Count == 0) return ""; // enter code here

        StringBuilder builder = new StringBuilder();
        //builder.Append("<html>");
        //builder.Append("<head>");
        //builder.Append("<title>");
        //builder.Append("Page-");
        //builder.Append(Guid.NewGuid());
        //builder.Append("</title>");
        //builder.Append("</head>");
        //builder.Append("<body>");
        builder.Append("<table border='1px' cellpadding='5' cellspacing='0' ");
        builder.Append("style='border: solid 1px Silver; font-size: x-small;'>");
        builder.Append("<tr align='left' valign='top'>");
        foreach (DataColumn c in dt.Columns)
        {
            builder.Append("<td align='left' valign='top'><b>");
            builder.Append(c.ColumnName);
            builder.Append("</b></td>");
        }
        builder.Append("</tr>");
        foreach (DataRow r in dt.Rows)
        {
            builder.Append("<tr align='left' valign='top'>");
            foreach (DataColumn c in dt.Columns)
            {
                builder.Append("<td align='left' valign='top'>");
                builder.Append(r[c.ColumnName]);
                builder.Append("</td>");
            }
            builder.Append("</tr>");
        }
        builder.Append("</table>");
        //builder.Append("</body>");
        //builder.Append("</html>");

        return builder.ToString();
    }





    public string SendMail(List<String> MailTo, string MailSub, string MailDetails)
    {
        try
        {

            var smtpClient = new SmtpClient()
            {
                Host = "smtp.office365.com",
                Port = 587,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
            };

            //smtpClient.Credentials = new NetworkCredential("disbursement@mahapocra.gov.in", "rrhpmxlzhvrpcrmg");
            smtpClient.Credentials = new NetworkCredential("alerts@mahapocra.gov.in", "Pocra@123");

            var message = new MailMessage
            {
                From = new MailAddress("alerts@mahapocra.gov.in", "Pocra"),
                Sender = new MailAddress("alerts@mahapocra.gov.in", "Pocra"),
                Subject = MailSub,
                IsBodyHtml = false
            };
            foreach (string s in MailTo)
            {
                message.To.Add(s);
            }
            message.IsBodyHtml = true;
            message.Body = MailDetails;
            smtpClient.Send(message);

            //  ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
            //| SecurityProtocolType.Tls11
            //| SecurityProtocolType.Tls12
            //| SecurityProtocolType.Ssl3;
            //  ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });


            return "Email Send Sucessfully";

        }
        catch (Exception ex)
        {
            return ex.ToString();

        }
    }




    public string SendMail(string MailTo, string MailSub, string MailDetails)
    {
        try
        {

            var smtpClient = new SmtpClient()
            {
                Host = "smtp.office365.com",
                Port = 587,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
            };

            //smtpClient.Credentials = new NetworkCredential("disbursement@mahapocra.gov.in", "rrhpmxlzhvrpcrmg");
            smtpClient.Credentials = new NetworkCredential("alerts@mahapocra.gov.in", "Pocra@123");

            var message = new MailMessage
            {
                From = new MailAddress("alerts@mahapocra.gov.in", "Pocra"),
                Sender = new MailAddress("alerts@mahapocra.gov.in", "Pocra"),
                Subject = MailSub,
                IsBodyHtml = false
            };
            message.To.Add(MailTo);
            message.CC.Add("alerts@mahapocra.gov.in");
            message.IsBodyHtml = true;
            message.Body = MailDetails;
            smtpClient.Send(message);

            //  ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
            //| SecurityProtocolType.Tls11
            //| SecurityProtocolType.Tls12
            //| SecurityProtocolType.Ssl3;
            //  ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });


            return "";

        }
        catch (Exception ex)
        {
            return ex.ToString();

        }
    }


    public static string SendMailToMe(string MailSub, string MailDetails)
    {
        try
        { 
            var smtpClient = new SmtpClient()
            {
                Host = "smtp.office365.com",
                Port = 587,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
            };

            //smtpClient.Credentials = new NetworkCredential("disbursement@mahapocra.gov.in", "rrhpmxlzhvrpcrmg");
            smtpClient.Credentials = new NetworkCredential("alerts@mahapocra.gov.in", "Pocra@123");

            var message = new MailMessage
            {
                From = new MailAddress("alerts@mahapocra.gov.in"),
                Sender = new MailAddress("alerts@mahapocra.gov.in"),
                Subject = MailSub,
                IsBodyHtml = false
            };
            message.To.Add("mayur.khairnar@abmindia.com");
            message.IsBodyHtml = true;
            message.Body = MailDetails;
            smtpClient.Send(message);

            //  ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
            //| SecurityProtocolType.Tls11
            //| SecurityProtocolType.Tls12
            //| SecurityProtocolType.Ssl3;
            //  ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });


            return "Email Send Sucessfully";

        }
        catch (Exception ex)
        {
            return ex.ToString();

        }
    }

}

