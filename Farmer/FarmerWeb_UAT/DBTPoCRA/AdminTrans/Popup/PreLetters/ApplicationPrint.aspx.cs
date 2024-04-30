using CommanClsLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace DBTPoCRA.AdminTrans.Popup.PreLetters
{
    public partial class ApplicationPrint : System.Web.UI.Page
    {
        MyClass cla = new MyClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    FillDetails(Request.QueryString["ID"].ToString());

                }
            }
            catch
            {

            }
        }

        private void FillExport()
        {
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //StringWriter stringWriter = new StringWriter();
            //HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            //divExport.RenderControl(htmlTextWriter);
            //StringReader stringReader = new StringReader(stringWriter.ToString());
            //Document Doc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
            //HTMLWorker htmlparser = new HTMLWorker(Doc);
            //PdfWriter.GetInstance(Doc, Response.OutputStream);
            //Doc.Open();
            //htmlparser.Parse(stringReader);
            //Doc.Close();
            //Response.Write(Doc);
            //Response.End();
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
                Literal11.Text = dt.Rows[0]["RegisterName2"].ToString();
                Literal3.Text = dt.Rows[0]["ActivityName"].ToString();
                Literal8.Text = dt.Rows[0]["ComponentName"].ToString();
                Literal6.Text = dt.Rows[0]["Gender"].ToString();
                Literal22.Text = dt.Rows[0]["MobileNumber"].ToString();
                Literal23.Text = dt.Rows[0]["EmailID"].ToString();
                Literal24.Text = dt.Rows[0]["AaDharNumber"].ToString();
                Literal13.Text = dt.Rows[0]["PhysicallyHandicap"].ToString();
                Literal14.Text = dt.Rows[0]["BalanceFundSource"].ToString();
                Literal25.Text = dt.Rows[0]["AccountNumber8A"].ToString(); //खाते क्र
                Literal4.Text = dt.Rows[0]["Hectare8A"].ToString();//(हेक्टर) 
                Literal10.Text = dt.Rows[0]["Are8A"].ToString();//(आर) 
                Literal21.Text = dt.Rows[0]["Address1PinCode"].ToString();
                Literal12.Text = dt.Rows[0]["Address1HouseNo"].ToString();
                Literal16.Text = dt.Rows[0]["Address1StreetName"].ToString();
                Literal18.Text = dt.Rows[0]["Address1Post_ID"].ToString();

                Literal15.Text = dt.Rows[0]["Taluka"].ToString();
                Literal19.Text = dt.Rows[0]["Taluka"].ToString();
                Literal20.Text = dt.Rows[0]["Cityname"].ToString();
                Literal2.Text = dt.Rows[0]["Cityname"].ToString();
                Literal20.Text = dt.Rows[0]["Cityname"].ToString();
                Literal5.Text = dt.Rows[0]["VillageName"].ToString();
                Literal17.Text = dt.Rows[0]["PostName"].ToString();
                Literal27.Text = cla.SvrDate();
                Literal9.Text = dt.Rows[0]["Clusters"].ToString();

                Literal26.Text = dt.Rows[0]["CategoryMaster"].ToString();

            }
        }




        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }
    }
}