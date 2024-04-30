using CommanClsLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBTPoCRA.TESTDATA
{
    public partial class LandMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    UATFarmerWeb.MahaBulekh.WebService_RORSoapClient s = new UATFarmerWeb.MahaBulekh.WebService_RORSoapClient();
                    String a = s.GetDistricts_Taluka_Village(2, "POCRA_Admin", "POCRA@Admin", "1", "", "", "");

                    //String a = s.GetDistricts_Taluka_Village(29, "WSHDFC_Admin", "WSHDFC@Admin", "1", "", "", "");


                    if (a.Length > 0)
                    {
                        if (a.EndsWith(","))
                        {
                            a = a.Substring(0, a.Length - 1);
                        }
                        a = a + "}";


                        DataTable dt = GetDataTableFromJsonString(a);

                        ddlD.DataSource = dt;
                        ddlD.DataTextField = "div_desc";
                        ddlD.DataValueField = "div_code";
                        ddlD.DataBind();
                        ddlD.Items.Insert(0, new ListItem("--Select--", "0"));

                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<p style='color:red;font-weight:800;font-size:20px;'>"+ ex.Message +"</p>");
                }
            }
        }

        public DataTable GetDataTableFromJsonString(string json)
        {
            var jsonLinq = JObject.Parse(json);

            // Find the first array using Linq
            var srcArray = jsonLinq.Descendants().Where(d => d is JArray).First();
            var trgArray = new JArray();
            foreach (JObject row in srcArray.Children<JObject>())
            {
                var cleanRow = new JObject();
                foreach (JProperty column in row.Properties())
                {
                    // Only include JValue types
                    if (column.Value is JValue)
                    {
                        cleanRow.Add(column.Name, column.Value);
                    }
                    else
                    {
                        Response.Write(column.Name);
                    }

                }
                trgArray.Add(cleanRow);
            }

            return JsonConvert.DeserializeObject<DataTable>(trgArray.ToString());
        }


        protected void ddlD_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                UATFarmerWeb.MahaBulekh.WebService_RORSoapClient s = new UATFarmerWeb.MahaBulekh.WebService_RORSoapClient();
                String a = s.GetDistricts_Taluka_Village(2, "POCRA_Admin", "POCRA@Admin", "2", ddlD.SelectedValue.Trim(), "", "");
                if (a.Length > 0)
                {
                    if (a.EndsWith(","))
                    {
                        a = a.Substring(0, a.Length - 1);
                    }
                    a = a + "}";


                    DataTable dt = GetDataTableFromJsonString(a);

                    ddlC.DataSource = dt;
                    ddlC.DataTextField = "district_name";
                    ddlC.DataValueField = "district_code";
                    ddlC.DataBind();
                    ddlC.Items.Insert(0, new ListItem("--Select--", "0"));

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlC_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                UATFarmerWeb.MahaBulekh.WebService_RORSoapClient s = new UATFarmerWeb.MahaBulekh.WebService_RORSoapClient();
                String a = s.GetDistricts_Taluka_Village(2, "POCRA_Admin", "POCRA@Admin", "3", ddlD.SelectedValue.Trim(), ddlC.SelectedValue.Trim(), "");
                if (a.Length > 0)
                {
                    if (a.EndsWith(","))
                    {
                        a = a.Substring(0, a.Length - 1);
                    }
                    a = a + "}";


                    DataTable dt = GetDataTableFromJsonString(a);

                    ddlT.DataSource = dt;
                    ddlT.DataTextField = "taluka_name";
                    ddlT.DataValueField = "taluka_code";
                    ddlT.DataBind();
                    ddlT.Items.Insert(0, new ListItem("--Select--", "0"));

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlT_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                UATFarmerWeb.MahaBulekh.WebService_RORSoapClient s = new UATFarmerWeb.MahaBulekh.WebService_RORSoapClient();
                String a = s.GetDistricts_Taluka_Village(2, "POCRA_Admin", "POCRA@Admin", "4", ddlD.SelectedValue.Trim(), ddlC.SelectedValue.Trim(), ddlT.SelectedValue.Trim());
                if (a.Length > 0)
                {
                    if (a.EndsWith(","))
                    {
                        a = a.Substring(0, a.Length - 1);
                    }
                    a = a + "}";


                    DataTable dt = GetDataTableFromJsonString(a);

                    ddlV.DataSource = dt;
                    ddlV.DataTextField = "village_name";
                    ddlV.DataValueField = "ccode";
                    ddlV.DataBind();
                    //ddlV.Items.Insert(0, new ListItem("--Select--", "0"));

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnTaluka_Click(object sender, EventArgs e)
        {
            MyClass cla = new MyClass();
            String CityCode = ddlC.SelectedValue.Trim();
            String CityID = cla.GetExecuteScalar("Select City_ID from Tbl_M_City where MahabhulekhCode=" + CityCode.Trim() + " and IsDeleted is null ");
            if (CityID.Length > 0)
            {

                for (int x = 0; x != ddlT.Items.Count; x++)
                {
                    String MahaBhulekhTalukaCode = ddlT.Items[x].Value.Trim();
                    String TalukaName = ddlT.Items[x].Text.Trim();
                    String s = cla.GetExecuteScalar("Select ISNULL(Count(TalukaID),0) from Tbl_M_VillageMaster  where IsDeleted is null and TalukaMr=N'" + TalukaName.Trim() + "' and City_ID=" + CityID + "");
                    if (s.Length == 0) s = "0";
                    if (Convert.ToInt32(s) > 0)
                    {
                        cla.ExecuteCommand(" update Tbl_M_VillageMaster set MahaBhulekhTalukaCode='" + MahaBhulekhTalukaCode + "' where IsDeleted is null and TalukaMr=N'" + TalukaName.Trim() + "' and City_ID=" + CityID + "");

                    }
                    else
                    {

                    }
                }
            }
        }

        protected void btnVillage_Click(object sender, EventArgs e)
        {
            MyClass cla = new MyClass();
            String CityCode = ddlC.SelectedValue.Trim();
            String CityID = cla.GetExecuteScalar("Select City_ID from Tbl_M_City where MahabhulekhCode=" + CityCode.Trim() + " and IsDeleted is null ");
            if (CityID.Length > 0)
            {


                String talukaCode = ddlT.SelectedValue.Trim();
                String TalukaID = cla.GetExecuteScalar("Select TalukaID from Tbl_M_VillageMaster where MahaBhulekhTalukaCode=" + talukaCode.Trim() + " and IsDeleted is null and City_ID=" + CityID + " ");
                if (TalukaID.Length > 0)
                {
                    for (int x = 0; x != ddlV.Items.Count; x++)
                    {
                        String MahaBhulekhVillageCode = ddlV.Items[x].Value.Trim();
                        String VillageNameMr = ddlV.Items[x].Text.Trim();
                        String s = cla.GetExecuteScalar("Select ISNULL(Count(VillageID),0) from Tbl_M_VillageMaster  where IsDeleted is null and VillageNameMr=N'" + VillageNameMr.Trim() + "' and City_ID=" + CityID + " and TalukaID=" + TalukaID + " ");
                        if (s.Length == 0) s = "0";
                        if (Convert.ToInt32(s) > 0)
                        {
                            cla.ExecuteCommand(" update Tbl_M_VillageMaster set MahaBhulekhVillageCode='" + MahaBhulekhVillageCode + "' where IsDeleted is null and VillageNameMr=N'" + VillageNameMr.Trim() + "' and City_ID=" + CityID + " and TalukaID=" + TalukaID + " ");

                        }
                        else
                        {

                        }
                    }
                }
            }
        }

        protected void btnGet8A_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlV.SelectedIndex > 0)
                {
                    txtKhataNo.Text = ddlDbt.SelectedValue.Trim();
                }
                GridView1.DataSource = null;
                GridView1.DataBind();

                UATFarmerWeb.MahaBulekh.WebService_RORSoapClient s = new UATFarmerWeb.MahaBulekh.WebService_RORSoapClient();
                String a = s.DisplayROR8A(2, "POCRA_Admin", "POCRA@Admin", "1", ddlC.SelectedValue.Trim(), ddlT.SelectedValue.Trim(), ddlV.SelectedValue.Trim(), txtKhataNo.Text.Trim());
                if (a.Length > 0)
                {

                    Label1.Text = a;

                    // a = "<?xml version='1.0' encoding='UTF-8'?>" + a;
                    //DataTable newTable = new DataTable();
                    //newTable.ReadXml(a);

                    System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(new System.IO.StringReader(a));
                    reader.Read();

                    System.Data.DataSet ds = new System.Data.DataSet();
                    ds.ReadXml(reader, System.Data.XmlReadMode.Auto);

                    GridView1.DataSource = ds.Tables["row"];
                    GridView1.DataBind();


                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlV_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyClass cla = new MyClass();
            DataTable dt = cla.GetDataTable("Select distinct r.RegisterName +' - Khata No.  '+ l.AccountNumber8A +' - Area - '+Convert(nvarchar(20),l.Hectare8A)+' - '+Convert(nvarchar(20),l.Are8A) as Names , l.AccountNumber8A from Tbl_M_RegistrationDetails r inner join Tbl_M_VillageMaster v on v.VillageID=r.Work_VillageID  inner join Tbl_M_RegistrationLand l on l.RegistrationID=r.RegistrationID where r.IsDeleted is null and l.IsDeleted is null and l.AccountNumber8A is not null and v.MahaBhulekhVillageCode='" + ddlV.SelectedValue.Trim() + "'");
            ddlDbt.DataSource = dt;
            ddlDbt.DataTextField = "Names";
            ddlDbt.DataValueField = "AccountNumber8A";
            ddlDbt.DataBind();
            ddlDbt.Items.Insert(0, new ListItem("--Select--", "0"));

        }

        protected void btnGetVillage_Click(object sender, EventArgs e)
        {
            MyClass cla = new MyClass();


            for (int m = 0; m != ddlV.Items.Count; m++)
            {

                String VillageCode1 = ddlV.Items[m].Value.Trim();
                String VillageName = ddlV.Items[m].Text.Trim();
                int check = 0;
                  

                for (int x = 0; x != 2000; x++)
                {
                    try
                    {

                        check++;

                        if (check >= 10)
                        {
                            break;
                        }


                        UATFarmerWeb.MahaBulekh.WebService_RORSoapClient s = new UATFarmerWeb.MahaBulekh.WebService_RORSoapClient();
                        String a = s.DisplayNEW8A(2, "POCRA_Admin", "POCRA@Admin", "1", ddlC.SelectedValue.Trim(), ddlT.SelectedValue.Trim(), VillageCode1.Trim(), x.ToString());

                        a = a.Contains("Error : 103") ? "" : a;

                        if (a.Length > 0)
                        {

                            check = 0;
                            System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(new System.IO.StringReader(a));
                            reader.Read();

                            System.Data.DataSet ds = new System.Data.DataSet();
                            ds.ReadXml(reader, System.Data.XmlReadMode.Auto);

                            //GridView1.DataSource = ds.Tables["row"];
                            //GridView1.DataBind();

                            string Query = "";

                            for (int b = 0; b != ds.Tables["row"].Rows.Count; b++)
                            {
                                String owner = ds.Tables["row"].Rows[b]["owner"].ToString().Trim();
                                String khata_description = ds.Tables["row"].Rows[b]["khata_description"].ToString().Trim();
                                String culti_area_in_khata = ds.Tables["row"].Rows[b]["culti_area_in_khata"].ToString().Trim();
                                String non_culti_area_in_khata = ds.Tables["row"].Rows[b]["non_culti_area_in_khata"].ToString().Trim();
                                String khata_no = x.ToString();
                                String District = ds.Tables["row"].Rows[b]["district_name"].ToString().Trim();
                                String Taluka = ddlT.SelectedItem.Text;
                                String Village = VillageName;// ddlV.SelectedItem.Text;
                                String VillageCode = VillageCode1;// ddlV.SelectedValue.Trim();
                                String survey_number = ds.Tables["row"].Rows[b]["pin"].ToString().Trim();

                                String ss = " INSERT INTO [dbo].[MahaBhulekh_Details] (  [owner] ,[khata_description] ,[khata_no] ,survey_number ,[culti_area_in_khata] ,[non_culti_area_in_khata] ,[District] ,[Taluka] ,[Village] ,[VillageCode])  ";
                                ss += " VALUES (N'" + owner + "',N'" + khata_description + "',N'" + khata_no + "','" + survey_number + "' ,N'" + culti_area_in_khata + "',N'" + non_culti_area_in_khata + "',N'" + District + "',N'" + Taluka + "',N'" + Village + "',N'" + VillageCode + "'); ";

                                Query += ss;
                                //if (cla.ExecuteCommand(ss).Length > 0)
                                //{

                                //}
                            }
                            if (cla.ExecuteCommand(Query).Length > 0)
                            {

                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }


                }




            }

        }



        //GetSurveyWiseLandDetails
    }
}