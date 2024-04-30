using CommanClsLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace DBTPoCRA.AdminTrans.Popup.PreLetters
{
    public partial class Well : System.Web.UI.Page
    {
        MyClass cla = new MyClass();
        private bool startConversion = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FillDetails(Request.QueryString["ID"].ToString());


                    // instantiate the html to pdf converter
                    //HtmlToPdf converter = new HtmlToPdf();
                    //// convert the url to pdf
                    //PdfDocument doc = converter.ConvertUrl("https://dbt.mahapocra.gov.in/admintrans/Popup/PreSanctionletter.aspx?ID=" + Request.QueryString["ID"].ToString() + "");
                    //// save pdf document
                    //String path = Server.MapPath("/admintrans/Popup/pdfTemp");
                    //path = path + "/" + cla.GetSqlUnikNO("3") + ".pdf";
                    //doc.Save(path);
                    //// close pdf document
                    //HyperLink1.NavigateUrl = path.Trim();
                    //doc.Close();

                }
            }
            catch
            {

            }
        }
        private void FillDetails(String ApplicationID)
        {

            List<String> lst = new List<String>();
            lst.Add(ApplicationID);
            if (Session["Lang"].ToString().Trim() == "mr-IN")
            {
                lst.Add("E");
            }
            else
            {
                lst.Add("M");
            }

            DataTable dt = cla.GetDtByProcedure("SP_GetDetailForApplicationPrint", lst);
            if (dt.Rows.Count > 0)
            {
                String RegistrationID = dt.Rows[0]["RegistrationID"].ToString();
                 Literal7.Text = dt.Rows[0]["ApplicationCode"].ToString();
              
                Literal6.Text = cla.SvrDate();// dt.Rows[0]["ApplicationDate"].ToString();
                Literal11.Text = dt.Rows[0]["ApplicationDate"].ToString();
                Literal12.Text = dt.Rows[0]["RegisterName"].ToString();
                Literal16.Text = dt.Rows[0]["ApplicationCode"].ToString();
                // Literal13.Text = cla.SvrDate();// dt.Rows[0]["ApplicationDate"].ToString();

                //Literal7.Text = dt.Rows[0]["ActivityName"].ToString();

                String adds = "";

                adds = "HouseNo : " + dt.Rows[0]["Address1HouseNo"].ToString();
                adds += "<br> Street Name : " + dt.Rows[0]["Address1StreetName"].ToString();
                adds += "<br> Village : " + dt.Rows[0]["VillageName"].ToString();
                adds += "<br> PostName : " + dt.Rows[0]["PostName"].ToString();
                adds += "<br> Districts  : " + dt.Rows[0]["Cityname"].ToString() + " <br> Pincode - " + dt.Rows[0]["Address1PinCode"].ToString();

                Literal2.Text = dt.Rows[0]["Subdivisions"].ToString();
                Literal8.Text = dt.Rows[0]["Subdivisions"].ToString();
                Literal9.Text = dt.Rows[0]["Subdivisions"].ToString();
                Literal15.Text = dt.Rows[0]["Cityname"].ToString();
                Literal10.Text = dt.Rows[0]["Cityname"].ToString();
                Literal3.Text = dt.Rows[0]["Cityname"].ToString();
                //Literal6.Text = dt.Rows[0]["Cityname"].ToString();
                Literal14.Text = dt.Rows[0]["Taluka"].ToString();
                Literal13.Text = dt.Rows[0]["PostName"].ToString();

            }
        }


        protected override void Render(HtmlTextWriter writer)
        {
            if (startConversion)
            {
                //DivPrint.Visible = false;

                //// get html of the page
                //TextWriter myWriter = new StringWriter();
                //HtmlTextWriter htmlWriter = new HtmlTextWriter(myWriter);
                //base.Render(htmlWriter);

                //// instantiate a html to pdf converter object
                //HtmlToPdf converter = new HtmlToPdf();
                //converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                //converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;

                //// create a new pdf document converting the html string of the page
                //PdfDocument doc = converter.ConvertHtmlString(
                //    myWriter.ToString(), Request.Url.AbsoluteUri);
                //DivPrint.Visible = true;
                //// save pdf document
                //doc.Save(Response, false, "PreSanctionLetter.pdf");

                //// close pdf document
                //doc.Close();
            }
            else
            {
                // render web page in browser
                base.Render(writer);
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            startConversion = true;
        }
    }
}