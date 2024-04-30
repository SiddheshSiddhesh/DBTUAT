using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CommanClsLibrary
{
    public class FFSClass
    {
        public static object DBTPoCRA { get; private set; }

        public static String UpdateLofinInFFS(String jsonData)
        {
            MyClass cla = new MyClass();
            try
            {
                var myDetails = JsonConvert.DeserializeObject<FFSData>(jsonData);
                int x = 0;
                if (myDetails.response.ToUpper() == "Login success".ToUpper())
                {
                    #region
                    String FFSID = myDetails.data.id;
                    String UserName = myDetails.data.username;
                    String Desig_ID = myDetails.data.dbt_role_id;
                    String emailId = myDetails.data.email;

                    if (Desig_ID == null)
                    {
                        if (myDetails.data.role_id.Trim() == "24")
                        {
                            Desig_ID = "36";
                        }
                        else
                        {
                            Desig_ID = cla.GetExecuteScalar("Select top 1 Desig_ID from Tbl_M_LoginDetails where UserName='" + UserName + "' and IsDeleted is null ");
                        }
                    }

                    if (Desig_ID != "")
                    {
                        DataTable dt = cla.GetDataTable("Select UserId,Desig_ID from Tbl_M_LoginDetails where FFSID=" + FFSID + " and IsDeleted is null and Desig_ID=" + Desig_ID + " ");
                        if (dt.Rows.Count == 0)
                        {
                            if (Desig_ID.Trim() != "")
                            {
                                dt = cla.GetDataTable("Select UserId,Desig_ID from Tbl_M_LoginDetails where UserName='" + UserName + "' and IsDeleted is null and Desig_ID=" + Desig_ID + " ");
                            }
                        }

                        if (dt.Rows.Count == 0)
                        {
                            String UserId = cla.GetExecuteScalar("Select Max(UserId) from Tbl_M_LoginDetails where FFSID=" + FFSID + " and Desig_ID=" + Desig_ID + " ");

                            if (UserId != "")
                            {
                                cla.ExecuteCommand("update Tbl_M_LoginDetails set IsDeleted=NULL where UserId=" + UserId + " and Desig_ID=" + Desig_ID + " ");
                            }
                            dt = cla.GetDataTable("Select UserId,Desig_ID from Tbl_M_LoginDetails where FFSID=" + FFSID + " and IsDeleted is null and Desig_ID=" + Desig_ID + "");
                            if (dt.Rows.Count == 0)
                            {
                                if (Desig_ID.Trim() != "")
                                {
                                    dt = cla.GetDataTable("Select UserId,Desig_ID from Tbl_M_LoginDetails where UserName='" + UserName + "' and IsDeleted is null and Desig_ID=" + Desig_ID + "");
                                }
                            }

                        }

                        //------------- if exest --
                        if (dt.Rows.Count > 0)
                        {
                            #region
                            // 
                            Desig_ID = dt.Rows[0]["Desig_ID"].ToString();
                            String UserId = dt.Rows[0]["UserId"].ToString();
                            String LevelID = cla.GetExecuteScalar("SELECT   LevelType_ID FROM Tbl_M_Designation where IsDeleted is null and Desig_ID=" + Desig_ID + " ");

                            String FullName = myDetails.data.first_name + " " + myDetails.data.middle_name + " " + myDetails.data.last_name;

                            String FFS_role_id = myDetails.data.role_id;
                            cla.ExecuteCommand("update Tbl_M_LoginDetails set UserName='" + UserName + "' , FullName='" + FullName.Trim() + "' , FFS_role_id=" + FFS_role_id + " , EmailId='" + emailId + "'  where   Desig_ID=" + Desig_ID + " and FFSID=" + FFSID + " ");



                            if (LevelID.Trim() == "5")
                            {
                                #region "Village"
                                List<Geodata> lst = myDetails.data.village_data;
                                // village Lable 
                                String StrVillageCodes = "";
                                String Query = "";
                                DataTable dtVillage = cla.GetDataTable("Select distinct C.VillageID,C.VillageCode from Tbl_M_LoginDetails_Child C where C.UserId=" + UserId.Trim() + " and C.IsDeleted is null and C.VillageID is not null");
                                foreach (Geodata cls in lst)
                                {
                                    if (StrVillageCodes.Length == 0) StrVillageCodes = "'" + cls.code + "'"; else StrVillageCodes = StrVillageCodes + "," + "'" + cls.code + "'";
                                    DataRow dr = dtVillage.AsEnumerable().SingleOrDefault(r => r.Field<String>("VillageCode") == cls.code);
                                    if (dr == null)
                                    {
                                        // if not found in DBT
                                        int UserChildId = cla.TableID("Tbl_M_LoginDetails_Child", "UserChildId");

                                        DataTable dta = cla.GetDataTable("SELECT VillageID FROM Tbl_M_VillageMaster where VillageCode='" + cls.code.Trim() + "'");
                                        for (int x1 = 0; x1 != dta.Rows.Count; x1++)
                                        {

                                            Query = " INSERT INTO Tbl_M_LoginDetails_Child (UserChildId, UserId, VillageCode,FromSource,VillageID)";
                                            Query += " VALUES(" + UserChildId + "," + UserId + ",'" + cls.code.Trim() + "','DBT'," + dta.Rows[x1]["VillageID"].ToString().Trim() + ")";
                                            cla.ExecuteCommand(Query);
                                            UserChildId++;

                                        }
                                        x++;
                                    }
                                }
                                // end insert 

                                // if not in FFS but in DBT
                                if (StrVillageCodes != "")
                                {
                                    cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails_Child SET IsDeleted='1' where  VillageCode NOT IN  (" + StrVillageCodes + ") and UserId=" + UserId.Trim() + "  ");
                                }
                                cla.ExecuteCommand("update Tbl_M_LoginDetails_Child Set VillageID=(Select top 1 VillageID  from Tbl_M_VillageMaster where IsDeleted is null and VillageCode=Tbl_M_LoginDetails_Child.VillageCode)  where VillageID is null and IsDeleted is null and UserId=" + UserId.Trim() + " ");
                                if (x > 0)
                                {
                                    LogFFS(jsonData);
                                }
                                #endregion

                            }

                            if (LevelID.Trim() == "3")
                            {
                                // taluka Lable
                                #region "Taluka"
                                List<Geodata> lst = myDetails.data.taluka_data;
                                String StrVillageCodes = "";
                                String Query = "";
                                DataTable dtVillage = cla.GetDataTable("Select distinct C.TalukaID,T.FFS_TalukaID from Tbl_M_LoginDetails_Child C  inner join Tbl_M_TalukaMaster T on T.TalukaID=C.TalukaID where C.UserId=" + UserId.Trim() + " and C.IsDeleted is null and C.TalukaID is not null");
                                foreach (Geodata cls in lst)
                                {
                                    if (StrVillageCodes.Length == 0) StrVillageCodes = "'" + cls.taluka_id + "'"; else StrVillageCodes = StrVillageCodes + "," + "'" + cls.taluka_id + "'";
                                    DataRow dr = dtVillage.AsEnumerable().SingleOrDefault(r => r.Field<String>("FFS_TalukaID") == cls.taluka_id);
                                    if (dr == null)
                                    {
                                        // if not found in DBT
                                        int UserChildId = cla.TableID("Tbl_M_LoginDetails_Child", "UserChildId");
                                        String TalukaID = cla.GetExecuteScalar(" Select top 1 TalukaID from Tbl_M_TalukaMaster where FFS_TalukaID='" + cls.taluka_id + "' and IsDeleted is null  ");
                                        if (TalukaID.Length > 0)
                                        {
                                            Query = " INSERT INTO Tbl_M_LoginDetails_Child (UserChildId, UserId, TalukaID,FromSource)";
                                            Query += " VALUES(" + UserChildId + "," + UserId + ",'" + TalukaID.Trim() + "','DBT')";
                                            cla.ExecuteCommand(Query);
                                        }
                                        x++;
                                    }
                                }
                                // end insert 

                                // if not in FFS but in DBT
                                if (StrVillageCodes.Trim() != "")
                                {
                                    cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails_Child SET IsDeleted='1' where TalukaID not in ( Select TalukaID from Tbl_M_TalukaMaster where FFS_TalukaID IN  (" + StrVillageCodes + ") and IsDeleted is null )  and UserId=" + UserId.Trim() + "  ");
                                }
                                if (x > 0)
                                {
                                    LogFFS(jsonData);
                                }
                                #endregion
                            }
                            if (LevelID.Trim() == "2")
                            {
                                #region "District"
                                List<Geodata> lst = myDetails.data.district_data;
                                //String StrVillageCodes = "";
                                String Query = "";

                                cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails_Child SET IsDeleted='1' where  UserId=" + UserId.Trim() + "  ");
                                foreach (Geodata cls in lst)
                                {

                                    int UserChildId = cla.TableID("Tbl_M_LoginDetails_Child", "UserChildId");
                                    String City_ID = cla.GetExecuteScalar(" Select C.City_ID from Tbl_M_City C where C.Cityname='" + cls.district_name + "' and IsDeleted is null  ");
                                    if (City_ID.Length > 0)
                                    {
                                        Query = " INSERT INTO Tbl_M_LoginDetails_Child (UserChildId, UserId, City_ID,FromSource)";
                                        Query += " VALUES(" + UserChildId + "," + UserId + ",'" + City_ID.Trim() + "','DBT')";
                                        cla.ExecuteCommand(Query);
                                    }
                                    x++;
                                }


                                if (x > 0)
                                {
                                    LogFFS(jsonData);
                                }
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            // if exist in FFS but not in DBT add it 

                            CreateNewUser(jsonData);


                        }


                    }

                    #endregion

                }
                else
                {
                    // if failed to login



                }


            }
            catch (Exception ex)
            {
                LogFFS(jsonData);
                LogFFS(ex.ToString());
            }

            return "";
        }


        public static String MakeItInactiveInDBT(String jsonData)
        {
            MyClass cla = new MyClass();
            try
            {
                var myDetails = JsonConvert.DeserializeObject<FFSData>(jsonData);

                LogFFS(jsonData);
                #region
                String FFSID = myDetails.data.id;
                String UserName = myDetails.data.username;
                String dbt_role_id = myDetails.data.dbt_role_id;



                cla.ExecuteCommand("update Tbl_M_LoginDetails set IsDeleted='1',DeleteFromSource='FFS-AUTO' where  UserName='" + UserName.Trim() + "' and Desig_ID=" + dbt_role_id.Trim() + " ");

                #endregion


            }
            catch (Exception ex)
            {
                LogFFS(jsonData);
                LogFFS(ex.ToString());
            }

            return "";
        }



        public static void CreateNewUser(String jsonData)
        {


            MyClass cla = new MyClass();
            var myDetails = JsonConvert.DeserializeObject<FFSData>(jsonData);

            String FFSID = myDetails.data.id;
            String UserName = myDetails.data.username;
            String Desig_ID = myDetails.data.dbt_role_id;
            String EmailId = myDetails.data.email;
            if (EmailId == null) EmailId = "";
            if (Desig_ID == null)
            {
                if (myDetails.data.role_id.Trim() == "24")
                {
                    Desig_ID = "36";
                }
            }

            String FullName = myDetails.data.first_name + " " + myDetails.data.middle_name + " " + myDetails.data.last_name;
            String Password = "123456";
            String FFS_role_id = myDetails.data.role_id;
            String UserRoleID = cla.GetExecuteScalar(" SELECT UserRoleID FROM Tbl_M_UserRole where Desig_ID=" + Desig_ID.Trim() + " ");
            String LevelID = cla.GetExecuteScalar("SELECT   LevelType_ID FROM Tbl_M_Designation where IsDeleted is null and Desig_ID=" + Desig_ID + " ");



            if (Desig_ID == "1")
            {
                return;
            }
            if (Desig_ID.Trim().Length == 0)
            {
                return;
            }


            //------------validations ---------------------// 

            String str = "";
            int UserId = cla.TableID("Tbl_M_LoginDetails", "UserId");
            int UserChildId = cla.TableID("Tbl_M_LoginDetails_Child", "UserChildId");
            int UserWiseRoleId = cla.TableID("Tbl_M_LoginDetails_Role", "UserWiseRoleId");
            using (SqlConnection connection = new SqlConnection(clsSettings.strCoonectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;
                transaction = connection.BeginTransaction("CTransaction");
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {

                    cla.ExecuteCommand("update Tbl_M_LoginDetails set IsDeleted='1' where UserName='" + UserName + "'  ", command);


                    str = " INSERT INTO Tbl_M_LoginDetails (UserId, Desig_ID, LoginAs, FullName, UserName, UPass,FromAPI,FFSID,FFS_role_id,Mobile,EmailId)";
                    str += " VALUES(" + UserId + "," + Desig_ID.ToString() + ", '1',N'" + FullName.Trim() + "','" + UserName.Trim() + "','" + Password.Trim() + "','1','" + FFSID + "'," + FFS_role_id + ",'" + UserName.Trim() + "','" + EmailId.Trim() + "')";
                    cla.ExecuteCommand(str, command);

                    cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails_Child SET IsDeleted='1' where  UserId = " + UserId.ToString() + "", command);

                    if (LevelID.Trim() == "5")
                    {
                        #region "Village"
                        List<Geodata> lst = myDetails.data.village_data;
                        // village Lable 
                        String Query = "";
                        //DataTable dtVillage = cla.GetDataTable("Select distinct C.VillageID,C.VillageCode from Tbl_M_LoginDetails_Child C where C.UserId=" + UserId.Trim() + " and C.IsDeleted is null and C.VillageID is not null");
                        foreach (Geodata cls in lst)
                        {

                            // if not found in DBT
                            DataTable dta = cla.GetDataTable("SELECT VillageID FROM Tbl_M_VillageMaster where VillageCode='" + cls.code.Trim() + "'  ", command);
                            for (int x1 = 0; x1 != dta.Rows.Count; x1++)
                            {

                                Query = " INSERT INTO Tbl_M_LoginDetails_Child (UserChildId, UserId, VillageCode,FromSource,VillageID)";
                                Query += " VALUES(" + UserChildId + "," + UserId + ",'" + cls.code.Trim() + "','DBT-AUTO'," + dta.Rows[x1]["VillageID"].ToString().Trim() + ")";
                                cla.ExecuteCommand(Query, command);
                                UserChildId++;

                            }

                        }
                        // end insert 


                        #endregion

                    }

                    if (LevelID.Trim() == "3")
                    {
                        // taluka Lable
                        #region "Taluka"
                        List<Geodata> lst = myDetails.data.taluka_data;
                        String Query = "";
                        foreach (Geodata cls in lst)
                        {
                            // if not found in DBT

                            String TalukaID = cla.GetExecuteScalar(" Select top 1 TalukaID from Tbl_M_TalukaMaster where FFS_TalukaID='" + cls.taluka_id + "' and IsDeleted is null  ", command);
                            if (TalukaID.Length > 0)
                            {
                                Query = " INSERT INTO Tbl_M_LoginDetails_Child (UserChildId, UserId, TalukaID,FromSource)";
                                Query += " VALUES(" + UserChildId + "," + UserId + ",'" + TalukaID.Trim() + "','DBT-AUTO')";
                                cla.ExecuteCommand(Query, command);
                                UserChildId++;
                            }

                        }
                        #endregion
                    }
                    if (LevelID.Trim() == "2")
                    {
                        #region "District"
                        List<Geodata> lst = myDetails.data.district_data;
                        String Query = "";
                        foreach (Geodata cls in lst)
                        {
                            String City_ID = cla.GetExecuteScalar(" Select C.City_ID from Tbl_M_City C where C.Cityname='" + cls.district_name + "' and IsDeleted is null  ", command);
                            if (City_ID.Length > 0)
                            {
                                Query = " INSERT INTO Tbl_M_LoginDetails_Child (UserChildId, UserId, City_ID,FromSource)";
                                Query += " VALUES(" + UserChildId + "," + UserId + ",'" + City_ID.Trim() + "','DBT-AUTO')";
                                cla.ExecuteCommand(Query, command);
                                UserChildId++;
                            }

                        }
                        #endregion
                    }



                    //------------------




                    cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails_Role SET IsDeleted='1' where  UserId = " + UserId.ToString() + "", command);
                    cla.ExecuteCommand(" INSERT INTO Tbl_M_LoginDetails_Role (UserWiseRoleId, UserId, UserRoleID) VALUES (" + UserWiseRoleId + "," + UserId.ToString() + "," + UserRoleID.Trim() + ") ", command);


                    transaction.Commit();

                }
                catch (Exception ex)
                {

                    LogFFS(ex.ToString());

                    try
                    {
                        transaction.Rollback();

                    }
                    catch
                    {

                    }



                }
                finally
                {
                    if (connection != null) { connection.Close(); }
                    command.Dispose();
                }

            }

        }


        public static void LogFFS(String Query)
        {
            try
            {

                string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;


                message += string.Format("Data: {0}", Query.ToString());
                message += Environment.NewLine;

                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                try
                {
                    if (HttpContext.Current.Session["UserId"] != null)
                    {
                        message += Environment.NewLine;
                        message += string.Format("UserID: {0}", HttpContext.Current.Session["UserId"].ToString());
                        message += Environment.NewLine;
                    }
                }
                catch (Exception ex) { }

                message += "-----------------------------------------------------------";
                message += Environment.NewLine;

                try
                {
                    string ipaddress;
                    ipaddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (ipaddress == "" || ipaddress == null)
                        ipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];


                    message += string.Format("IPAddress: {0}", ipaddress.ToString());
                    message += Environment.NewLine;
                    message += "-----------------------------------------------------------";
                    message += Environment.NewLine;
                }
                catch
                {

                }


                String PathUp = "~/DocMasters/ErroLog";
                String path = System.Web.HttpContext.Current.Server.MapPath(PathUp);
                if (!Directory.Exists(path))
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }
                path = path + "/FFS-" + DateTime.Now.ToString("MM/dd/yyyy").Replace("/", "") + ".txt";

                FileInfo fi = new FileInfo(path);
                if (fi.Exists)
                {
                    string seperator = "-------------------------------------------\n\r";
                    File.AppendAllText(path,
                        seperator + DateTime.Now.ToString() + "\n\r" + message + "\n\r" + seperator);

                }
                else
                {
                    using (StreamWriter sw = fi.CreateText())
                    {
                        sw.WriteLine(message);
                    }
                }

            }
            catch { }

        }
    }


    public class clsRtnMssage
    {
        public String UserID { get; set; }
        public String ResponseCode { get; set; }
        public String MessageType { get; set; }
        public string Message { get; set; }


    }

    public class FFSData
    {

        public String response
        {
            get;
            set;
        }
        public string status
        {
            get;
            set;
        }
        public clsdata data { get; set; }
    }

    public class clsdata
    {

        public string first_name
        {
            get;
            set;
        }
        public string middle_name
        {
            get;
            set;
        }
        public string last_name
        {
            get;
            set;
        }
        public string designation
        {
            get;
            set;
        }
        public string username
        {
            get;
            set;
        }
        public string password
        {
            get;
            set;
        }
        public string id
        {
            get;
            set;
        }
        public string role_id
        {
            get;
            set;
        }
        public string mobile
        {
            get;
            set;
        }
        public string dbt_role_id
        {
            get;
            set;
        }
        public string email
        {
            get; set;
        }
        public List<Geodata> village_data { get; set; }
        public List<Geodata> cluster_data { get; set; }
        public List<Geodata> grampanchayat_data { get; set; }
        public List<Geodata> taluka_data { get; set; }
        public List<Geodata> district_data { get; set; }

    }

    public class Geodata
    {

        public String id
        {
            get;
            set;
        }
        public String district_id
        {
            get;
            set;
        }
        public String district_name
        {
            get;
            set;
        }
        public String taluka_id
        {
            get; set;
        }
        public String taluka_name
        {
            get; set;
        }
        public string code
        {
            get;
            set;
        }
        public string name
        {
            get;
            set;
        }

    }
}
