using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CommanClsLibrary;

namespace UgoTransfer.Data
{
    /// <summary>
    /// Summary description for ExportToExcel
    /// </summary>
    public class ExportToExcel : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {


            DataTable table = new DataTable();


            if (context.Request.QueryString["T"].ToString().Trim() == "Session")
            {
                table = (DataTable)context.Session["ExportDt"];
                //context.Session["ExportDt"] = null;
            }

            if (context.Request.QueryString["T"].ToString().Trim() == "Desk1Dt")
            {
                table = (DataTable)context.Session["Desk1Dt"];
                //context.Session["ExportDt"] = null;
            }

            if (HttpContext.Current.Session["Headers"] == null)
            {
                HttpContext.Current.Session["Headers"] = "";
            }


            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + context.Request.QueryString["SID"].ToString().Trim().ToUpper() + ".xls");

            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR>");
            //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
              "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");

            HttpContext.Current.Response.Write("<Td align='center' colspan=" + table.Columns.Count + "> <h1> " + context.Request.QueryString["SID"].ToString().Trim().ToUpper() + " </h1> " + HttpContext.Current.Session["Headers"].ToString() + " <BR> </Td>  <TR>");

            //am getting my grid's column headers
            int columnscount = table.Columns.Count;

            for (int j = 0; j < columnscount; j++)
            {      //write in new column
                HttpContext.Current.Response.Write("<Td>");
                //Get column headers  and make it as bold in excel columns
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(table.Columns[j].ColumnName.ToString());
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in table.Rows)
            {//write in new row
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }

                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}