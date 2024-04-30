using CommanClsLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace DBTPoCRA.APPData
{
    /// <summary>
    /// Summary description for Reports
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Reports : System.Web.Services.WebService
    {

        [WebMethod]
        public void CheckUserExistance(String SecurityKey, String UserID)
        {
            DataTable dt = new DataTable();
            MyClass cla = new MyClass();

            List<clsLoginCheck> lst = new List<clsLoginCheck>();

            if (MyCommanClassAPI.CheckApiAuthrization("1", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsLoginCheck d = new clsLoginCheck();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {


                int AA = 0, CA = 0;

                try
                {
                    String DashBoardType = cla.GetExecuteScalar("select DashBoardType from Tbl_M_Designation where Desig_ID=(select Desig_ID from Tbl_M_LoginDetails where UserId=" + UserID + " and IsDeleted is null ) ");
                    if (DashBoardType.Trim() == "PMU")
                    {
                        AA = Convert.ToInt32(GetCAandAA("8", "", ""));//AA
                        CA = Convert.ToInt32(GetCAandAA("9", "", ""));//CA
                    }
                    if (DashBoardType.Trim() == "SAO")
                    {
                        //District
                        //DataTable dtt = cla.GetDataTable("Select Tbl_M_TalukaMaster.FFS_TalukaID from Tbl_M_LoginDetails_Child Inner join Tbl_M_TalukaMaster On Tbl_M_TalukaMaster.TalukaID=Tbl_M_LoginDetails_Child.TalukaID where UserId=543195 and Tbl_M_LoginDetails_Child.IsDeleted is null");
                        foreach (DataRow dr in dt.Rows)
                        {
                            AA = AA + Convert.ToInt32(GetCAandAA("8", dr["Working_Area"].ToString().Trim(), ""));//AA
                            CA = CA + Convert.ToInt32(GetCAandAA("9", dr["Working_Area"].ToString().Trim(), ""));//CA
                        }

                    }
                    if (DashBoardType.Trim() == "SDAO")
                    {
                        //Taluka
                        DataTable dtt = cla.GetDataTable("Select Tbl_M_TalukaMaster.FFS_TalukaID from Tbl_M_LoginDetails_Child Inner join Tbl_M_TalukaMaster On Tbl_M_TalukaMaster.TalukaID=Tbl_M_LoginDetails_Child.TalukaID where UserId=" + UserID + " and Tbl_M_LoginDetails_Child.IsDeleted is null");
                        foreach (DataRow dr in dtt.Rows)
                        {
                            AA = AA + Convert.ToInt32(GetCAandAA("8", "", dr["FFS_TalukaID"].ToString().Trim()));//AA
                            CA = CA + Convert.ToInt32(GetCAandAA("9", "", dr["FFS_TalukaID"].ToString().Trim()));//CA
                        }
                    }
                }
                catch (Exception ex)
                {
                    Util.LogErrorFFS(" FFS DASHBOARD APP ==  " + ex.ToString());
                }


                List<string> parm = new List<string>();
                parm.Add(UserID);
                dt = cla.GetDtByProcedure("SP_Rpt_GetDashBoardData", parm);

                Dictionary<String, String> Values = new Dictionary<string, string>();

                for (int x = 0; x != dt.Columns.Count; x++)
                {
                    try
                    {
                        String s = dt.Compute("Sum([" + dt.Columns[x].ColumnName + "])", string.Empty).ToString();
                        if (dt.Columns[x].ColumnName.Trim().ToUpper() == "No. of Cluster Assistants (Active)".ToUpper())
                        {
                            
                            s = AA.ToString();
                        }
                        if (dt.Columns[x].ColumnName.Trim().ToUpper() == "No. of Agri Assistants (Active)".ToUpper())
                        {
                            
                            s = CA.ToString();
                        }
                        Values.Add(dt.Columns[x].ColumnName, s);

                    }
                    catch (Exception ex)
                    {
                        Util.LogError(ex);
                    }
                }


                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(Values, Formatting.Indented).ToString());


            }
            catch (Exception ex)
            {

                Util.LogError(ex);
            }
        }


        private String GetCAandAA(String DisgID, String disitrict_name, String taluka_id)
        {
            String ret = "0";

            //String disitrict_name = "", taluka_id = "";
            String url = "https://api-ffs.mahapocra.gov.in/v27/caService/ca-aa-count";
            //String responce = "";
            string jsonContent = "{\"disitrict_name\":\"" + disitrict_name.Trim() + "\",\"taluka_id\":\"" + taluka_id.Trim() + "\", \"role_id\":\"" + DisgID + "\"}";
            //Util.LogErrorFFS("FFS- " + jsonContent);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(jsonContent);

            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            //request.Timeout = 100000;// System.Threading.Timeout.Infinite;
            //request.KeepAlive = false;
            //request.ReadWriteTimeout = 100000;
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            long length = 0;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    length = response.ContentLength;
                    StreamReader sr = new StreamReader(response.GetResponseStream());
                    string results = sr.ReadToEnd();
                    String[] s = results.Trim().Split(':');
                    String[] a = s[3].Trim().Split(',');//200,"token"
                    ret = a[0].Trim().Replace("}", "");
                }
            }
            catch (Exception ex)
            {
                Util.LogErrorFFS("FFS CA/AA- " + ex.ToString());
                // Log exception and throw as for GET example above
            }

            if (ret.Trim().Length == 0) ret = "0";

            return ret;
        }



        public string DataTableToJSONWithStringBuilder(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;



        }



        [WebMethod]
        public void GetPreSanIssueList(String SecurityKey, String UserID, String VillageID, String FromDate, String ToDate, String PageNo)
        {
            DataTable dt = new DataTable();
            MyClass cla = new MyClass();

            List<clsLoginCheck> lsts = new List<clsLoginCheck>();

            if (MyCommanClassAPI.CheckApiAuthrization("1", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsLoginCheck d = new clsLoginCheck();
                d.Message = "";
                d.Error = "Authorization Failed";
                lsts.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lsts, Formatting.Indented).ToString());
                return;
            }
            try
            {


                List<String> lst = new List<String>();
                lst.Add("");
                lst.Add("");

                if (VillageID.Length > 0)
                {
                    lst.Add(VillageID.Trim());
                }
                else
                {
                    lst.Add("");
                }


                lst.Add("");

                lst.Add("");

                lst.Add("");


                if (FromDate.Trim().Length > 0)
                {
                    lst.Add(cla.mdy(FromDate.Trim()));
                }
                else
                {
                    lst.Add("");
                }
                if (ToDate.Trim().Length > 0)
                {
                    lst.Add(cla.mdy(ToDate.Trim()));
                }
                else
                {
                    lst.Add("");
                }
                lst.Add(UserID);
                lst.Add(PageNo);
                dt = cla.GetDtByProcedure("SP_Rpt_GetPreSanctionListPaging", lst);


                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(dt, Formatting.None).ToString());


            }
            catch (Exception ex)
            {
                Util.LogError(ex);

            }

        }



        [WebMethod]
        public void GetWillingnesList(String SecurityKey, String UserID, String VillageID, String FromDate, String ToDate, String PageNo, String RegisterName)
        {
            DataTable dt = new DataTable();
            MyClass cla = new MyClass();

            List<clsLoginCheck> lsts = new List<clsLoginCheck>();

            if (MyCommanClassAPI.CheckApiAuthrization("1", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsLoginCheck d = new clsLoginCheck();
                d.Message = "";
                d.Error = "Authorization Failed";
                lsts.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lsts, Formatting.Indented).ToString());
                return;
            }
            try
            {


                List<String> lst = new List<String>();
                lst.Add("");
                lst.Add("");

                if (VillageID.Length > 0)
                {
                    lst.Add(VillageID.Trim());
                }
                else
                {
                    lst.Add("");
                }


                lst.Add("");

                lst.Add("");

                lst.Add("");


                if (FromDate.Trim().Length > 0)
                {
                    lst.Add(cla.mdy(FromDate.Trim()));
                }
                else
                {
                    lst.Add("");
                }
                if (ToDate.Trim().Length > 0)
                {
                    lst.Add(cla.mdy(ToDate.Trim()));
                }
                else
                {
                    lst.Add("");
                }
                lst.Add(UserID);
                lst.Add(PageNo);
                lst.Add(RegisterName);
                dt = cla.GetDtByProcedure("SP_Rpt_GetPreSanctionListPagingFinal", lst);


                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(dt, Formatting.None).ToString());


            }
            catch (Exception ex)
            {

                Util.LogError(ex);
            }

        }



        [WebMethod]
        public void UpdateWillingness(String SecurityKey, String UserID, String ApplicationID, String IsInterested, String ExpectedStartWeek, String ReasonofNotInterested, String SelfiImageString, String LatitudeMap, String LongitudeMap)
        {
            DataTable dt = new DataTable();
            MyClass cla = new MyClass();
            AzureBlobHelper fileRet = new AzureBlobHelper();
            List<clsCommanReturns> lst = new List<clsCommanReturns>();
            if (MyCommanClassAPI.CheckApiAuthrization("1", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsCommanReturns d = new clsCommanReturns();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {


                String PathUp = "DocMasters/MemberDoc/Willingness/" + ApplicationID.ToString().Trim() + "";

                String str = "";
                String selfieofUser = "/admintrans/DocMasters/MemberDoc/Willingness/" + ApplicationID.ToString().Trim() + "/selfie" + MyCommanClassAPI.GetFileExtension(SelfiImageString);
                string imageName = "selfie" + MyCommanClassAPI.GetFileExtension(SelfiImageString);
                byte[] imageBytes = Convert.FromBase64String(SelfiImageString);
                String ret = fileRet.UploadData(PathUp, imageName, imageBytes);
                if (ret.Trim().Length == 0)
                {
                    str = " INSERT INTO Tbl_T_ApplicationWillingness ( ApplicationID,TransDate ,ExpectedStartWeek,IsInterested ,ReasonofNotInterested ,UserID,SelfieofUser,LatitudeMap,LongitudeMap)";
                    str += " VALUES (" + ApplicationID + ",'" + cla.mdy(cla.SvrDate()) + "','" + ExpectedStartWeek.Trim() + "',N'" + IsInterested + "',N'" + ReasonofNotInterested + "'," + UserID + ",'" + selfieofUser.Trim() + "','" + LatitudeMap + "','" + LongitudeMap + "')";



                    if (cla.ExecuteCommand(str).Length == 0)
                    {
                        clsCommanReturns d = new clsCommanReturns();
                        d.Message = "";
                        d.Error = "Saved Sucessfully";
                        lst.Add(d);
                    }
                    else
                    {
                        clsCommanReturns d = new clsCommanReturns();
                        d.Message = "";
                        d.Error = "Error";
                        lst.Add(d);
                    }
                }
                else
                {
                    clsCommanReturns d = new clsCommanReturns();
                    d.Message = "";
                    d.Error = "Please take your selfie ";
                    lst.Add(d);
                }

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());


            }
            catch (Exception ex)
            {

                Util.LogError(ex);
            }
        }



        [WebMethod]
        public void GetPreSanIssueListFFS(String SecurityKey, String FromApplicationDate, String TOApplicationDate, String District, String SubDivision, String Village, String Activity, String ActivityGroup)
        {
            DataTable dt = new DataTable();
            MyClass cla = new MyClass();

            List<clsLoginCheck> lsts = new List<clsLoginCheck>();

            if (MyCommanClassAPI.CheckApiAuthrization("101", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsLoginCheck d = new clsLoginCheck();
                d.Message = "";
                d.Error = "Authorization Failed";
                lsts.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lsts, Formatting.Indented).ToString());
                return;
            }
            try
            {


                List<String> lst = new List<String>();

                if (FromApplicationDate.Trim().Length > 0)
                {
                    lst.Add(cla.mdy(FromApplicationDate.Trim()));
                }
                else
                {
                    lst.Add("");
                }
                if (TOApplicationDate.Trim().Length > 0)
                {
                    lst.Add(cla.mdy(TOApplicationDate.Trim()));
                }
                else
                {
                    lst.Add("");
                }


                if (District.Trim().Length > 0)
                {
                    lst.Add(District);

                }
                else
                {
                    lst.Add("");
                }

                if (SubDivision.Trim().Length > 0)
                {
                    lst.Add(SubDivision);

                }
                else
                {
                    lst.Add("");
                }

                if (Village.Trim().Length > 0)
                {
                    lst.Add(Village);

                }
                else
                {
                    lst.Add("");
                }


                if (Activity.Trim().Length > 0)
                {
                    lst.Add(Activity);

                }
                else
                {
                    lst.Add("");
                }
                if (ActivityGroup.Trim().Length > 0)
                {
                    lst.Add(ActivityGroup);

                }
                else
                {
                    lst.Add("");
                }
                dt = cla.GetDtByProcedure("SP_Rpt_GetPreSanctionListForFFS", lst);


                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(dt, Formatting.None).ToString());


            }
            catch (Exception ex)
            {
                Util.LogError(ex);

            }

        }


        [WebMethod]
        public void GetDisbursementDetails(String SecurityKey, String Month, String Year)
        {
            DataTable dt = new DataTable();
            MyClass cla = new MyClass();

            List<clsLoginCheck> lsts = new List<clsLoginCheck>();

            if (MyCommanClassAPI.CheckApiAuthrization("101", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsLoginCheck d = new clsLoginCheck();
                d.Message = "";
                d.Error = "Authorization Failed";
                lsts.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lsts, Formatting.Indented).ToString());
                return;
            }

            if(Month.Length==0)
            {
                clsLoginCheck d = new clsLoginCheck();
                d.Message = "";
                d.Error = "Month is Required";
                lsts.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lsts, Formatting.Indented).ToString());
                return;

            }
            if (Year.Length == 0)
            {
                clsLoginCheck d = new clsLoginCheck();
                d.Message = "";
                d.Error = "Year is Required";
                lsts.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lsts, Formatting.Indented).ToString());
                return;
            }

            try
            {


                List<String> lst = new List<String>();
                lst.Add(Month);
                lst.Add(Year);
                dt = cla.GetDtByProcedure("SP_Rpt_GetPaymentDetailsFORFFS", lst);

                if (dt.Rows.Count > 0)
                {
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(dt, Formatting.None).ToString());
                }
                else
                {
                    clsLoginCheck d = new clsLoginCheck();
                    d.Message = "";
                    d.Error = "No Record Found";
                    lsts.Add(d);
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    Context.Response.Flush();

                    Context.Response.Write(JsonConvert.SerializeObject(lsts, Formatting.Indented).ToString());
                }


            }
            catch (Exception ex)
            {
                clsLoginCheck d = new clsLoginCheck();
                d.Message = "ERROR";
                d.Error = ex.Message;
                lsts.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lsts, Formatting.Indented).ToString());
             
                Util.LogError(ex);

            }

        }



        [WebMethod]
        public string[] AutoCompleteAjaxRequest(string prefixText, int count)
        {
            MyClass cla1 = new MyClass();
            List<string> ajaxDataCollection = new List<string>();
            DataTable _objdt = new DataTable();
            _objdt = cla1.GetDataTable("SELECT  ProjectCostElements FROM FPO_M_ProjectCostElements where IsDeleted is null and ProjectCostElements like '%"+ prefixText + "%' order by ProjectCostElements");// GetDataFromDataBase(prefixText);
            if (_objdt.Rows.Count > 0)
            {
                for (int i = 0; i < _objdt.Rows.Count; i++)
                {
                    ajaxDataCollection.Add(_objdt.Rows[i]["ProjectCostElements"].ToString());
                }
            }
            return ajaxDataCollection.ToArray();
        }

    }
}
