using CommanClsLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBTPoCRA.TESTDATA
{
    public partial class MahaBulekhData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                MyClass cla = new MyClass();
                rptCustomers.DataSource = cla.GetDataTable("Select R.RegisterName,R.RegistrationID , R.LandStatus,V.VillageName,V.Taluka,V.VillageCode,v.Subdivision from Tbl_M_RegistrationDetails as R inner join Tbl_M_VillageMaster V on V.VillageID=R.Work_VillageID  where R.IsDeleted is null and R.RegistrationID in (Select Tbl_M_RegistrationLand.RegistrationID from Tbl_M_RegistrationLand inner join Tbl_M_VillageMaster V on V.VillageID=Tbl_M_RegistrationLand.VillageID  where v.VillageID=54 and Tbl_M_RegistrationLand.AccountNumber8A is not null and v.IsDeleted is null and Tbl_M_RegistrationLand.IsDeleted is null) order by R.RegisterName ");
                rptCustomers.DataBind();
            }
        }



        protected void OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                MyClass cla = new MyClass();
                string customerId = (e.Item.FindControl("hfCustomerId") as HiddenField).Value;
                Repeater rptOrders = e.Item.FindControl("rptOrders") as Repeater;
                rptOrders.DataSource = cla.GetDataTable("SELECT *  FROM MahaBhulekh_Details where LandID in (Select Tbl_M_RegistrationLand.LandID from Tbl_M_RegistrationLand where RegistrationID="+ customerId + " and IsDeleted is null) order by MahaBuleskID ");
                rptOrders.DataBind();

                Repeater rptCrap = e.Item.FindControl("Repeater1") as Repeater;
                rptCrap.DataSource = cla.GetDataTable("SELECT *  FROM MahaBhulekh_crops where LandID in (Select Tbl_M_RegistrationLand.LandID from Tbl_M_RegistrationLand where RegistrationID=" + customerId + " and IsDeleted is null)");
                rptCrap.DataBind();



            }
        }












        protected void btnUpload_Click(object sender, EventArgs e)
        {

            if (FileUpload1.HasFile)

            {

                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string FolderPath = "DocMasters";// Server.MapPath("~/DocMasters");
                string FilePath = Server.MapPath(FolderPath + FileName);
                FileUpload1.SaveAs(FilePath);
                Import_To_Grid(FilePath, Extension, rbHDR.SelectedItem.Text);

            }

        }
        private void Import_To_Grid(string FilePath, string Extension, string isHDR)
        {

            string conStr = "";

            switch (Extension)
            {

                case ".xls": //Excel 97-03

                    conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}; Extended Properties ='Excel 8.0;HDR={1}'";

                    break;

                case ".xlsx": //Excel 07

                    conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties ='Excel 8.0;HDR={1}'";

                    break;

            }

            conStr = String.Format(conStr, FilePath, isHDR);

            OleDbConnection connExcel = new OleDbConnection(conStr);

            OleDbCommand cmdExcel = new OleDbCommand();

            OleDbDataAdapter oda = new OleDbDataAdapter();

            DataTable dt = new DataTable();

            cmdExcel.Connection = connExcel;



            //Get the name of First Sheet

            connExcel.Open();

            DataTable dtExcelSchema;

            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

            connExcel.Close();



            //Read Data from First Sheet

            connExcel.Open();

            cmdExcel.CommandText = "SELECT * From [" + SheetName + "] ";

            oda.SelectCommand = cmdExcel;

            oda.Fill(dt);

            connExcel.Close();



            //Bind Data to GridView

            GridView1.Caption = Path.GetFileName(FilePath);

            // DataView dv = new DataView(dt);
            //dv.RowFilter = "'district_code'='3' ";
            GridView1.DataSource = dt;

            GridView1.DataBind();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MyClass cla = new MyClass();
            foreach (GridViewRow gr in GridView1.Rows)
            {
                String Taluka_name = gr.Cells[3].Text.Trim().Replace("&#39;", "");
                String CityIDCode = gr.Cells[0].Text.Trim().Replace("&#39;", "");
                String CityID = cla.GetExecuteScalar("Select City_ID from Tbl_M_City where MahabhulekhCode='"+CityIDCode.Trim()+"' and IsDeleted is null");
                String VillageNameMr = gr.Cells[5].Text.Trim().Replace("&#39;", "");
                String MahaBhulekhVillageCode = gr.Cells[4].Text.Trim().Replace("&#39;", "");
                String TalukaID = cla.GetExecuteScalar("Select TalukaID from Tbl_M_TalukaMaster where TalukaMR=N'" + Taluka_name.Trim() + "'");
                if (TalukaID.Trim().Length > 0)
                {
                    //String TalukaID = cla.GetExecuteScalar("Select TalukaID from Tbl_M_TalukaMaster where TalukaMR=N'" + Taluka_name.Trim() + "'");
                    String s = cla.GetExecuteScalar("Select ISNULL(Count(VillageID),0) from Tbl_M_VillageMaster  where IsDeleted is null and VillageNameMr=N'" + VillageNameMr.Trim() + "' and City_ID=" + CityID + " and TalukaID=" + TalukaID + " ");
                    if (s.Length == 0) s = "0";
                    if (Convert.ToInt32(s) > 0)
                    {
                        cla.ExecuteCommand("update Tbl_M_VillageMaster set MahaBhulekhVillageCode='" + MahaBhulekhVillageCode + "' where IsDeleted is null and VillageNameMr=N'" + VillageNameMr.Trim() + "' and City_ID=" + CityID + " and TalukaID=" + TalukaID + " ");

                    }
                    else
                    {
                        gr.Cells[5].BackColor = System.Drawing.Color.OrangeRed;
                    }
                }
                else
                {
                    gr.Cells[3].BackColor = System.Drawing.Color.LightPink;

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
        protected void Button2_Click(object sender, EventArgs e)
        {
            //MyClass cla = new MyClass();

            //DataTable dt = cla.GetDataTable("Select LandID,AccountNumber8A,v.MahaBhulekhVillageCode from Tbl_M_RegistrationLand inner join Tbl_M_VillageMaster V on V.VillageID=Tbl_M_RegistrationLand.VillageID where v.VillageID=54 and Tbl_M_RegistrationLand.AccountNumber8A is not null and v.IsDeleted is null and Tbl_M_RegistrationLand.IsDeleted is null and LandID not in (Select MahaBhulekh_Details.LandID from MahaBhulekh_Details) order by Tbl_M_RegistrationLand.RegistrationID");
            //Mahabhulekh.WebService_POCRASoapClient s = new Mahabhulekh.WebService_POCRASoapClient();
            //int cpount = dt.Rows.Count;
            //int x = 0;
            //foreach (DataRow dr in dt.Rows)
            //{
            //    x = 0;
            //    String AccountNumber8A = dr["AccountNumber8A"].ToString();
            //    String VillageCode = dr["MahaBhulekhVillageCode"].ToString();
            //    String LandID = dr["LandID"].ToString();
            //    String ServerNo = "";
            //    String a = s.GetSubSurvey(2, "POCRA_Admin", "admin@123", "5", "4", VillageCode.Trim(), "2", "", AccountNumber8A.Trim(), "", "", "");
            //    if (a.Length == 0) a = " Responce not found from Mahabhulekh";
            //    if (a.Length > 0)
            //    {
            //        if (a.EndsWith(","))
            //        {
            //            a = a.Substring(0, a.Length - 1);
            //            a = a + "}";
            //        }
            //        try
            //        {
            //            DataTable dt1 = GetDataTableFromJsonString(a);
            //            ServerNo = dt1.Rows[0]["Pin"].ToString();
            //        }
            //        catch
            //        {

            //            String str = " INSERT INTO MahaBhulekh_Details(LandID ,ErrorsLog)";
            //            str += " VALUES(" + LandID + ",N'" + a.Trim().Replace("'", "") + "')";
            //            cla.ExecuteCommand(str);

            //        }



            //    }


            //    if (ServerNo.Length > 0)
            //    {
            //        NOTFound:
            //        DataSet dataSet = new DataSet();
            //        a = s.GetLandDetailsBySurveyNo(2, "POCRA_Admin", "admin@123", "5", "4", VillageCode.Trim(), ServerNo.Trim(), "", "", "", "", "", "", "", "");
            //        x++;
            //        if (a == null)
            //        {
            //            a = "";
            //            if (x <= 3)
            //            {
            //                goto NOTFound;
            //            }
            //        }

            //        if (a.Length == 0) a = "Data Not Available....!!!!";

            //        if (a == " Data Not Available....!!!! Crop Data Not Available....!!!!")
            //        {
            //            String str = " INSERT INTO MahaBhulekh_Details(LandID ,ErrorsLog)";
            //            str += " VALUES(" + LandID + ",N'" + a.Trim().Replace("'", "") + "')";
            //            cla.ExecuteCommand(str);

            //            str = " INSERT INTO MahaBhulekh_crops(LandID ,ErrorsLog)";
            //            str += " VALUES(" + LandID + ",N'Crop Data Not Available....!!!!')";
            //            cla.ExecuteCommand(str);

            //        }


            //        try
            //        {
            //            if (a.Trim().Length > 0)
            //            {
            //                if (a.EndsWith(","))
            //                {
            //                    a = a.Substring(0, a.Length - 1);
            //                    a = a + "}";
            //                }
            //                dataSet = JsonConvert.DeserializeObject<DataSet>(a);
            //                DataTable dtland = dataSet.Tables[0];
            //                DataTable dtCrap = dataSet.Tables[1];
            //                //
            //                string expression;
            //                expression = "khata_no ='" + AccountNumber8A + "'";

            //                DataRow[] foundRows;

            //                foundRows = dtland.Select(expression);
            //                for (int i = 0; i < foundRows.Length; i++)
            //                {
            //                    //Console.WriteLine(foundRows[""][i]);
            //                    String owner = foundRows[i]["owner"].ToString();
            //                    String khata_description = foundRows[i]["khata_description"].ToString();
            //                    String survey_number = foundRows[i]["survey_number"].ToString();
            //                    String khata_no = foundRows[i]["khata_no"].ToString();
            //                    String culti_area_in_khata = foundRows[i]["culti_area_in_khata"].ToString();
            //                    String non_culti_area_in_khata = foundRows[i]["non_culti_area_in_khata"].ToString();
            //                    String str = " INSERT INTO MahaBhulekh_Details(LandID ,owner ,khata_description,survey_number ,khata_no ,culti_area_in_khata ,non_culti_area_in_khata)";
            //                    str += " VALUES(" + LandID + ",N'" + owner + "' ,N'" + khata_description + "',N'" + survey_number + "' ,N'" + khata_no + "' ,N'" + culti_area_in_khata + "' ,N'" + non_culti_area_in_khata + "')";
            //                    cla.ExecuteCommand(str);
            //                }

            //                //foundRows = dtCrap.Select(expression);
            //                for (int i = 0; i != dtCrap.Rows.Count; i++)
            //                {
            //                    //Console.WriteLine(foundRows[""][i]);
            //                    String survey_number = dtCrap.Rows[i]["Survey_no"].ToString();
            //                    String khata_no = "";// foundRows[i]["khata_no"].ToString();
            //                    String area = dtCrap.Rows[i]["mix_unirr_area_h"].ToString()+" "+ dtCrap.Rows[i]["const_unirr_area_h"].ToString();
            //                    String Culti_Year = dtCrap.Rows[i]["Culti_Year"].ToString();
            //                    String season_name = dtCrap.Rows[i]["Season_Name"].ToString();
            //                    String crop_name = dtCrap.Rows[i]["Mix_Crop_Name"].ToString()+" "+ dtCrap.Rows[i]["Const_Crop_Name"].ToString();

            //                    String str = " INSERT INTO MahaBhulekh_crops(LandID ,survey_number ,khata_no,AreaUsedForCrop ,year_culti ,season_name ,crop_name)";
            //                    str += " VALUES(" + LandID + ",N'" + survey_number + "' ,N'" + khata_no + "',N'" + area + "' ,N'" + Culti_Year + "' ,N'" + season_name + "' ,N'" + crop_name + "')";
            //                    cla.ExecuteCommand(str);
            //                }

            //                if(dtCrap.Rows.Count==0)
            //                {
            //                    String str = " INSERT INTO MahaBhulekh_crops(LandID ,ErrorsLog)";
            //                    str += " VALUES(" + LandID + ",N'Data Not Available....!!!!')";
            //                    cla.ExecuteCommand(str);
            //                }


            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            String str = " INSERT INTO MahaBhulekh_Details(LandID ,ErrorsLog)";
            //            str += " VALUES(" + LandID + ",N'" + a.Trim().Replace("'", "") + "')";
            //            cla.ExecuteCommand(str);
            //        }
            //    }



           // }









        }
    }
}