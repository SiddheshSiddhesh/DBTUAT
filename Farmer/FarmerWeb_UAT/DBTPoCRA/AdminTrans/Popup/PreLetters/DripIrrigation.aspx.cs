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
    public partial class DripIrrigation : System.Web.UI.Page
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
                    //String s = cla.GetExecuteScalar("Select isnull(Desig_NameMr,Desig_Name) from Tbl_M_Designation where Desig_ID=" + Session["Desig_ID"].ToString().Trim() + " and IsDeleted is null");
                    //litName.Text = s;

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
                litName.Text = dt.Rows[0]["ApprovedBy"].ToString();
                try
                {
                    if (litName.Text.Trim().Length == 0)
                    {
                        String s = cla.GetExecuteScalar("Select isnull(Desig_NameMr,Desig_Name) from Tbl_M_Designation where Desig_ID=" + Session["Desig_ID"].ToString().Trim() + " and IsDeleted is null");
                        litName.Text = s;
                    }
                }
                catch { }
                Literal7.Text = dt.Rows[0]["ApplicationCode"].ToString();
               // Literal1.Text = dt.Rows[0]["Are8A"].ToString();
                Literal4.Text = dt.Rows[0]["ApplicationDate"].ToString();
                Literal5.Text = dt.Rows[0]["MeetingDate"].ToString();
                Literal6.Text = cla.SvrDate();
                Literal11.Text = dt.Rows[0]["SurveyNo712"].ToString();
                Literal12.Text = dt.Rows[0]["RegisterName"].ToString();
                Literal16.Text = dt.Rows[0]["ApplicationCode"].ToString();
                Literal17.Text = dt.Rows[0]["Are712"].ToString();
                Literal18.Text= dt.Rows[0]["ActivityName"].ToString();
                Literal19.Text = dt.Rows[0]["UpdateOnDate"].ToString();
                Literal20.Text = dt.Rows[0]["Hectare8A"].ToString();
                
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