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
    public partial class FarmPondA332BlackSoil : System.Web.UI.Page
    {
        MyClass cla = new MyClass();
        private bool startConversion = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDetails(Request.QueryString["ID"].ToString());
                
            }
        }
        private void FillDetails(String ApplicationID)
        {
           
            List<String> lst = new List<String>();
            lst.Add(ApplicationID);
            lst.Add("M");
            DataTable dt = cla.GetDtByProcedure("SP_GetDetailForApplicationPrint", lst);
            if (dt.Rows.Count > 0)
            {
                String RegistrationID = dt.Rows[0]["RegistrationID"].ToString();
                //Literal1.Text = dt.Rows[0]["ApplicationCode"].ToString();
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
                //Literal5.Text = cla.SvrDate(); 
                Literal5.Text = dt.Rows[0]["LetterUpdatedate"].ToString();
                Literal6.Text = dt.Rows[0]["RegisterName"].ToString();
                Literal10.Text = dt.Rows[0]["ApplicationDate"].ToString();
                Literal11.Text = dt.Rows[0]["ActivityName"].ToString();
                Literal12.Text = dt.Rows[0]["MeetingDate"].ToString();
                Literal13.Text = dt.Rows[0]["AccountNumber8A"].ToString();
                Literal14.Text = dt.Rows[0]["SurveyNo712"].ToString();
                Literal15.Text = dt.Rows[0]["UpdateOnDate"].ToString();
                Literal16.Text = dt.Rows[0]["TimeLineDay"].ToString();
                Literal21.Text = dt.Rows[0]["ApplicationCode"].ToString();
                Literal22.Text = dt.Rows[0]["ActivityCode"].ToString();

                Literal19.Text = dt.Rows[0]["Hectare8A"].ToString();
                Literal20.Text = dt.Rows[0]["Are712"].ToString();
                String adds = "";
               
                    adds = "HouseNo : " + dt.Rows[0]["Address1HouseNo"].ToString();
                    adds += "<br> Street Name : " + dt.Rows[0]["Address1StreetName"].ToString();
                    adds += "<br> Village : " + dt.Rows[0]["VillageName"].ToString();
                    adds += "<br> PostName : " + dt.Rows[0]["PostName"].ToString();
                    adds += "<br> Districts  : " + dt.Rows[0]["Cityname"].ToString() + " <br> Pincode - " + dt.Rows[0]["Address1PinCode"].ToString();

                Literal2.Text = dt.Rows[0]["Subdivisions"].ToString();
                Literal3.Text = dt.Rows[0]["Subdivisions"].ToString();
                Literal17.Text = dt.Rows[0]["Subdivisions"].ToString();
                Literal4.Text = dt.Rows[0]["Cityname"].ToString();
                Literal9.Text = dt.Rows[0]["Cityname"].ToString();
                Literal18.Text = dt.Rows[0]["Cityname"].ToString();
                Literal8.Text = dt.Rows[0]["Taluka"].ToString();
                Literal7.Text = dt.Rows[0]["VillageName"].ToString();
                Literal23.Text = dt.Rows[0]["PostName"].ToString();
                Literal26.Text = dt.Rows[0]["PreRemarks"].ToString();



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