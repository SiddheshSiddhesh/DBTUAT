using CommanClsLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;


namespace DBTPoCRA.APPData
{
    /// <summary>
    /// Summary description for UserMaster
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class UserMaster : System.Web.Services.WebService
    {



        #region 'ClusterLevel User'
        [WebMethod]
        public void CreateUserClusterLevel(String SecurityKey, String FullName, String UserName, String Password, String Desig_ID, String CommaSeparatedUserRoleID, String CommaSeparatedClusters, String CommaSeparatedVillage, String FFSID, String TransType)
        {
            List<clsRtnMssage> lst = new List<clsRtnMssage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            if (MyCommanClassAPI.CheckApiAuthrization("101", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsRtnMssage d = new clsRtnMssage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                d.ResponseCode = "100";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //------------validations ---------------------// 
            if (TransType.Trim() == "I")
            {

                if (Comcls.RecordExistanceChk("Tbl_M_LoginDetails", "UserName", UserName.Trim(), "UserID") == false)
                {
                    clsRtnMssage d = new clsRtnMssage();
                    d.MessageType = "Warning";
                    d.Message = "User Name  " + UserName.ToUpper() + " is already created";
                    lst.Add(d);
                    d.ResponseCode = "100";
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }

                if (UserName.Trim().Length == 0)
                {

                    clsRtnMssage d = new clsRtnMssage();
                    d.MessageType = "Warning";
                    d.Message = "Please Fill User Name";
                    lst.Add(d);
                    d.ResponseCode = "100";
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }
                else if (Password.Trim().Length == 0)
                {

                    clsRtnMssage d = new clsRtnMssage();
                    d.MessageType = "Warning";
                    d.Message = "Please Fill Password";
                    lst.Add(d);
                    d.ResponseCode = "100";
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }

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

                        str = " INSERT INTO Tbl_M_LoginDetails (UserId, Desig_ID, LoginAs, FullName, UserName, UPass,FromAPI,FFSID)";
                        str += " VALUES(" + UserId + "," + Desig_ID.ToString() + ", '1',N'" + FullName.Trim() + "','" + UserName.Trim() + "','" + Password.Trim() + "','1','" + FFSID + "')";
                        cla.ExecuteCommand(str, command);

                        cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails_Child SET IsDeleted='1' where  UserId = " + UserId.ToString() + "", command);


                        String[] Clu = CommaSeparatedClusters.Trim().Split(',');
                        //for (int x = 0; x != Clu.Length; x++)
                        //{
                        //    String ClustersMasterID = cla.GetExecuteScalar("SELECT ClustersMasterID FROM Tbl_M_ClustersMaster where Clusters='" + Clu[x].Trim() + "' and IsDeleted is null ", command);
                        //    if (ClustersMasterID.Trim().Length > 0)

                        //    {
                        //        str = " INSERT INTO Tbl_M_LoginDetails_Child (UserChildId, UserId, ClustersMasterID)";//VillageID,
                        //        str += " VALUES(" + UserChildId + "," + UserId + "," + ClustersMasterID.Trim() + ")";
                        //        cla.ExecuteCommand(str, command);
                        //        UserChildId++;
                        //    }
                        //}


                        String[] Vlg = CommaSeparatedVillage.Trim().Split(',');
                        for (int x = 0; x != Vlg.Length; x++)
                        {

                            DataTable dta = cla.GetDataTable("SELECT VillageID FROM Tbl_M_VillageMaster where VillageCode='" + Vlg[x].Trim() + "' and IsDeleted is null ", command);
                            for (int x1 = 0; x1 != dta.Rows.Count; x1++)
                            {

                                str = " INSERT INTO Tbl_M_LoginDetails_Child (UserChildId, UserId, VillageID,FromSource)";//VillageID,
                                str += " VALUES(" + UserChildId + "," + UserId + "," + dta.Rows[x1]["VillageID"].ToString().Trim() + ",'FFS')";
                                cla.ExecuteCommand(str, command);
                                UserChildId++;

                            }


                        }

                        cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails_Role SET IsDeleted='1' where  UserId = " + UserId.ToString() + "", command);
                        Clu = CommaSeparatedUserRoleID.Trim().Split(',');
                        for (int x = 0; x != Clu.Length; x++)
                        {

                            cla.ExecuteCommand(" INSERT INTO Tbl_M_LoginDetails_Role (UserWiseRoleId, UserId, UserRoleID) VALUES (" + UserWiseRoleId + "," + UserId.ToString() + "," + Clu[x].Trim().Trim() + ") ", command);
                            UserWiseRoleId++;


                        }


                        transaction.Commit();
                        clsRtnMssage d = new clsRtnMssage();
                        d.MessageType = "Sucess";
                        d.UserID = UserId.ToString();
                        d.Message = "Record Saved Sucessfully";
                        d.ResponseCode = "200";
                        lst.Add(d);
                    }
                    catch (Exception ex)
                    {


                        try
                        {
                            transaction.Rollback();

                        }
                        catch
                        {

                        }

                        clsRtnMssage d = new clsRtnMssage();
                        d.MessageType = "Error";
                        d.Message = ex.ToString();
                        lst.Add(d);

                    }
                    finally
                    {
                        if (connection != null) { connection.Close(); }
                        command.Dispose();
                    }


                }

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
            }
            else if (TransType.Trim() == "U")
            {
                String UserId = cla.GetExecuteScalar("select UserID from Tbl_M_LoginDetails where UserName='" + UserName.Trim() + "' and IsDeleted is null ");

                if (Comcls.RecordExistanceChk("Tbl_M_LoginDetails", "UserName", UserName.Trim(), "UserID", Convert.ToInt32(UserId)) == false)
                {
                    clsRtnMssage d = new clsRtnMssage();
                    d.MessageType = "Warning";
                    d.Message = "User Name  " + UserName.ToUpper() + " is not exist";
                    lst.Add(d);
                    d.ResponseCode = "100";
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }



                String str = "";


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

                        str = " UPDATE Tbl_M_LoginDetails SET Desig_ID=" + Desig_ID.ToString() + ", LoginAs=1 , FullName=N'" + FullName.Trim() + "', UPass='" + Password.Trim() + "',FromAPI='1',FFSID='" + FFSID + "'   WHERE UserId=" + UserId.ToString() + "";//UserName='" + UserName.Trim() + "',

                        cla.ExecuteCommand(str, command);

                        cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails_Child SET IsDeleted='1' where  UserId = " + UserId.ToString() + "", command);

                        //String[] Clu = CommaSeparatedClusters.Trim().Split(',');
                        //for (int x = 0; x != Clu.Length; x++)
                        //{
                        //    String ClustersMasterID = cla.GetExecuteScalar("SELECT ClustersMasterID FROM Tbl_M_ClustersMaster where Clusters='" + Clu[x].Trim() + "' and IsDeleted is null ", command);
                        //    if (ClustersMasterID.Trim().Length > 0)

                        //    {
                        //        str = " INSERT INTO Tbl_M_LoginDetails_Child (UserChildId, UserId, ClustersMasterID)";//VillageID,
                        //        str += " VALUES(" + UserChildId + "," + UserId + "," + ClustersMasterID.Trim() + ")";
                        //        cla.ExecuteCommand(str, command);
                        //        UserChildId++;
                        //    }
                        //}


                        String[] Vlg = CommaSeparatedVillage.Trim().Split(',');
                        for (int x = 0; x != Vlg.Length; x++)
                        {
                            DataTable dta = cla.GetDataTable("SELECT VillageID FROM Tbl_M_VillageMaster where VillageCode='" + Vlg[x].Trim() + "' and IsDeleted is null ", command);
                            for (int x1 = 0; x1 != dta.Rows.Count; x1++)
                            {

                                str = " INSERT INTO Tbl_M_LoginDetails_Child (UserChildId, UserId, VillageID,FromSource)";//VillageID,
                                str += " VALUES(" + UserChildId + "," + UserId + "," + dta.Rows[x1]["VillageID"].ToString().Trim() + ",'FFS')";
                                cla.ExecuteCommand(str, command);
                                UserChildId++;

                            }
                        }

                        cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails_Role SET IsDeleted='1' where  UserId = " + UserId.ToString() + "", command);
                        String[] Clu = CommaSeparatedUserRoleID.Trim().Split(',');
                        for (int x = 0; x != Clu.Length; x++)
                        {

                            cla.ExecuteCommand(" INSERT INTO Tbl_M_LoginDetails_Role (UserWiseRoleId, UserId, UserRoleID) VALUES (" + UserWiseRoleId + "," + UserId.ToString() + "," + Clu[x].Trim().Trim() + ") ", command);
                            UserWiseRoleId++;


                        }


                        transaction.Commit();
                        clsRtnMssage d = new clsRtnMssage();
                        d.MessageType = "Sucess";
                        d.UserID = UserId.ToString();
                        d.Message = "Record Updated Sucessfully";
                        d.ResponseCode = "200";
                        lst.Add(d);
                    }
                    catch (Exception ex)
                    {


                        try
                        {
                            transaction.Rollback();

                        }
                        catch
                        {

                        }

                        clsRtnMssage d = new clsRtnMssage();
                        d.MessageType = "Error";
                        d.Message = ex.ToString();
                        lst.Add(d);

                    }
                    finally
                    {
                        if (connection != null) { connection.Close(); }
                        command.Dispose();
                    }


                }

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
            }
        }

        #endregion   //SubdivisionID





        #region "CreateUserSubDivisionLevel"
        [WebMethod]
        public void CreateUserSubDivisionLevel(String SecurityKey, String FullName, String UserName, String Password, String Desig_ID, String CommaSeparatedUserRoleID, String CommaSeparatedSubDivision, String FFSID, String TransType)
        {
            List<clsRtnMssage> lst = new List<clsRtnMssage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            if (MyCommanClassAPI.CheckApiAuthrization("102", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsRtnMssage d = new clsRtnMssage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                d.ResponseCode = "100";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //------------validations ---------------------// 

            if (TransType.Trim() == "I")
            {
                if (Comcls.RecordExistanceChk("Tbl_M_LoginDetails", "UserName", UserName.Trim(), "UserID") == false)
                {
                    clsRtnMssage d = new clsRtnMssage();
                    d.MessageType = "Warning";
                    d.Message = "User Name  " + UserName.ToUpper() + " is already created";
                    lst.Add(d);
                    d.ResponseCode = "100";
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }

                if (UserName.Trim().Length == 0)
                {

                    clsRtnMssage d = new clsRtnMssage();
                    d.MessageType = "Warning";
                    d.Message = "Please Fill User Name";
                    lst.Add(d);
                    d.ResponseCode = "100";
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }
                else if (Password.Trim().Length == 0)
                {

                    clsRtnMssage d = new clsRtnMssage();
                    d.MessageType = "Warning";
                    d.Message = "Please Fill Password";
                    lst.Add(d);
                    d.ResponseCode = "100";
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }

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


                        str = " INSERT INTO Tbl_M_LoginDetails (UserId, Desig_ID, LoginAs, FullName, UserName, UPass,FromAPI,FFSID)";
                        str += " VALUES(" + UserId + "," + Desig_ID.ToString() + ", '1',N'" + FullName.Trim() + "','" + UserName.Trim() + "','" + Password.Trim() + "','1','" + FFSID + "')";
                        cla.ExecuteCommand(str, command);

                        cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails_Child SET IsDeleted='1' where  UserId = " + UserId.ToString() + "", command);

                        String[] Clu = CommaSeparatedSubDivision.Trim().Split(',');
                        for (int x = 0; x != Clu.Length; x++)
                        {
                            String SubdivisionID = cla.GetExecuteScalar("SELECT SubdivisionID FROM Tbl_M_Subdivision where Subdivisions='" + Clu[x].Trim() + "' and IsDeleted is null ", command);
                            if (SubdivisionID.Trim().Length > 0)
                            {
                                str = " INSERT INTO Tbl_M_LoginDetails_Child (UserChildId, UserId, SubdivisionID,FromSource)";//VillageID,
                                str += " VALUES(" + UserChildId + "," + UserId + "," + SubdivisionID.Trim() + ",'FFS')";
                                cla.ExecuteCommand(str, command);
                                UserChildId++;
                            }
                        }

                        cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails_Role SET IsDeleted='1' where  UserId = " + UserId.ToString() + "", command);
                        Clu = CommaSeparatedUserRoleID.Trim().Split(',');
                        for (int x = 0; x != Clu.Length; x++)
                        {

                            cla.ExecuteCommand(" INSERT INTO Tbl_M_LoginDetails_Role (UserWiseRoleId, UserId, UserRoleID) VALUES (" + UserWiseRoleId + "," + UserId.ToString() + "," + Clu[x].Trim().Trim() + ") ", command);
                            UserWiseRoleId++;


                        }


                        transaction.Commit();
                        clsRtnMssage d = new clsRtnMssage();
                        d.MessageType = "Sucess";
                        d.UserID = UserId.ToString();
                        d.Message = "Record Saved Sucessfully";
                        d.ResponseCode = "200";
                        lst.Add(d);
                    }
                    catch (Exception ex)
                    {


                        try
                        {
                            transaction.Rollback();

                        }
                        catch
                        {

                        }

                        clsRtnMssage d = new clsRtnMssage();
                        d.MessageType = "Error";
                        d.Message = ex.ToString();
                        lst.Add(d);

                    }
                    finally
                    {
                        if (connection != null) { connection.Close(); }
                        command.Dispose();
                    }

                }

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
            }

            else if (TransType.Trim() == "U")
            {
                String UserId = cla.GetExecuteScalar("select UserID from Tbl_M_LoginDetails where UserName='" + UserName.Trim() + "' and IsDeleted is null ");

                if (Comcls.RecordExistanceChk("Tbl_M_LoginDetails", "UserName", UserName.Trim(), "UserID", Convert.ToInt32(UserId)) == false)
                {

                    clsRtnMssage d = new clsRtnMssage();
                    d.MessageType = "Warning";
                    d.Message = "User Name  " + UserName.ToUpper() + " is not exist";
                    lst.Add(d);
                    d.ResponseCode = "100";
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }



                String str = "";

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

                        str = " UPDATE Tbl_M_LoginDetails SET Desig_ID=" + Desig_ID.ToString() + ", LoginAs=1 , FullName=N'" + FullName.Trim() + "', UPass='" + Password.Trim() + "',FromAPI='1',FFSID='" + FFSID + "'   WHERE UserId=" + UserId.ToString() + "";//UserName='" + UserName.Trim() + "',

                        cla.ExecuteCommand(str, command);

                        cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails_Child SET IsDeleted='1' where  UserId = " + UserId.ToString() + "", command);

                        String[] Clu = CommaSeparatedSubDivision.Trim().Split(',');
                        for (int x = 0; x != Clu.Length; x++)
                        {
                            String SubdivisionID = cla.GetExecuteScalar("SELECT SubdivisionID FROM Tbl_M_Subdivision where Subdivisions='" + Clu[x].Trim() + "' and IsDeleted is null ", command);
                            if (SubdivisionID.Trim().Length > 0)
                            {
                                str = " INSERT INTO Tbl_M_LoginDetails_Child (UserChildId, UserId, SubdivisionID,FromSource)";//VillageID,
                                str += " VALUES(" + UserChildId + "," + UserId + "," + SubdivisionID.Trim() + ",'FFS')";
                                cla.ExecuteCommand(str, command);
                                UserChildId++;
                            }
                        }

                        cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails_Role SET IsDeleted='1' where  UserId = " + UserId.ToString() + "", command);
                        Clu = CommaSeparatedUserRoleID.Trim().Split(',');
                        for (int x = 0; x != Clu.Length; x++)
                        {

                            cla.ExecuteCommand(" INSERT INTO Tbl_M_LoginDetails_Role (UserWiseRoleId, UserId, UserRoleID) VALUES (" + UserWiseRoleId + "," + UserId.ToString() + "," + Clu[x].Trim().Trim() + ") ", command);
                            UserWiseRoleId++;


                        }


                        transaction.Commit();
                        clsRtnMssage d = new clsRtnMssage();
                        d.MessageType = "Sucess";
                        d.UserID = UserId.ToString();
                        d.Message = "Record Updated Sucessfully";
                        d.ResponseCode = "200";
                        lst.Add(d);
                    }
                    catch (Exception ex)
                    {


                        try
                        {
                            transaction.Rollback();

                        }
                        catch
                        {

                        }

                        clsRtnMssage d = new clsRtnMssage();
                        d.MessageType = "Error";
                        d.Message = ex.ToString();
                        lst.Add(d);

                    }
                    finally
                    {
                        if (connection != null) { connection.Close(); }
                        command.Dispose();
                    }

                }

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
            }


        }
        #endregion

        #region 'Village Level User'
        [WebMethod]
        public void CreateUserVillageLevel(String SecurityKey, String FullName, String UserName, String Password, String Desig_ID, String CommaSeparatedUserRoleID, String CommaSeparatedVillage, String FFSID, String TransType)
        {
            List<clsRtnMssage> lst = new List<clsRtnMssage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            if (MyCommanClassAPI.CheckApiAuthrization("103", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsRtnMssage d = new clsRtnMssage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                d.ResponseCode = "100";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
           

            //------------validations ---------------------// 
            if (TransType.Trim() == "I")
            {
                if (Comcls.RecordExistanceChk("Tbl_M_LoginDetails", "UserName", UserName.Trim(), "UserID") == false)
                {
                    clsRtnMssage d = new clsRtnMssage();
                    d.MessageType = "Warning";
                    d.Message = "User Name  " + UserName.ToUpper() + " is already created";
                    lst.Add(d);
                    d.ResponseCode = "100";
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }
                if (Comcls.RecordExistanceChk("Tbl_M_LoginDetails", "FFSID", FFSID.Trim(), "UserID") == false)
                {
                    clsRtnMssage d = new clsRtnMssage();
                    d.MessageType = "Warning";
                    d.Message = " FFSID  " + FFSID.ToUpper() + " is already created";
                    d.ResponseCode = "100";
                    lst.Add(d);
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();

                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }
                if (UserName.Trim().Length == 0)
                {

                    clsRtnMssage d = new clsRtnMssage();
                    d.MessageType = "Warning";
                    d.Message = "Please Fill User Name";
                    lst.Add(d);
                    d.ResponseCode = "100";
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }
                else if (Password.Trim().Length == 0)
                {

                    clsRtnMssage d = new clsRtnMssage();
                    d.MessageType = "Warning";
                    d.Message = "Please Fill Password";
                    lst.Add(d);
                    d.ResponseCode = "100";
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }

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


                        str = " INSERT INTO Tbl_M_LoginDetails (UserId, Desig_ID, LoginAs, FullName, UserName, UPass,FromAPI,FFSID)";
                        str += " VALUES(" + UserId + "," + Desig_ID.ToString() + ", '1',N'" + FullName.Trim() + "','" + UserName.Trim() + "','" + Password.Trim() + "','1','" + FFSID + "')";
                        cla.ExecuteCommand(str, command);

                        cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails_Child SET IsDeleted='1' where  UserId = " + UserId.ToString() + "", command);

                        String[] Vlg = CommaSeparatedVillage.Trim().Split(',');
                        for (int x = 0; x != Vlg.Length; x++)
                        {
                            //String VillageID = cla.GetExecuteScalar("SELECT VillageID FROM Tbl_M_VillageMaster where VillageCode='" + Vlg[x].Trim() + "' and IsDeleted is null ", command);
                            //if (VillageID.Trim().Length > 0)
                            //{
                            //    str = " INSERT INTO Tbl_M_LoginDetails_Child (UserChildId, UserId, VillageID)";//VillageID,
                            //    str += " VALUES(" + UserChildId + "," + UserId + "," + VillageID.Trim() + ")";
                            //    cla.ExecuteCommand(str, command);
                            //    UserChildId++;
                            //}

                            DataTable dta = cla.GetDataTable("SELECT VillageID FROM Tbl_M_VillageMaster where VillageCode='" + Vlg[x].Trim() + "' and IsDeleted is null ", command);
                            for (int x1 = 0; x1 != dta.Rows.Count; x1++)
                            {

                                str = " INSERT INTO Tbl_M_LoginDetails_Child (UserChildId, UserId, VillageID,FromSource)";//VillageID,
                                str += " VALUES(" + UserChildId + "," + UserId + "," + dta.Rows[x1]["VillageID"].ToString().Trim() + ",'FFS')";
                                cla.ExecuteCommand(str, command);
                                UserChildId++;

                            }

                        }

                        cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails_Role SET IsDeleted='1' where  UserId = " + UserId.ToString() + "", command);
                        Vlg = CommaSeparatedUserRoleID.Trim().Split(',');
                        for (int x = 0; x != Vlg.Length; x++)
                        {

                            cla.ExecuteCommand(" INSERT INTO Tbl_M_LoginDetails_Role (UserWiseRoleId, UserId, UserRoleID) VALUES (" + UserWiseRoleId + "," + UserId.ToString() + "," + Vlg[x].Trim().Trim() + ") ", command);
                            UserWiseRoleId++;


                        }


                        transaction.Commit();
                        clsRtnMssage d = new clsRtnMssage();
                        d.MessageType = "Sucess";
                        d.UserID = UserId.ToString();
                        d.Message = "Record Saved Sucessfully";
                        d.ResponseCode = "200";
                        lst.Add(d);
                    }
                    catch (Exception ex)
                    {


                        try
                        {
                            transaction.Rollback();

                        }
                        catch
                        {

                        }

                        clsRtnMssage d = new clsRtnMssage();
                        d.MessageType = "Error";
                        d.Message = ex.ToString();
                        lst.Add(d);

                    }
                    finally
                    {
                        if (connection != null) { connection.Close(); }
                        command.Dispose();
                    }

                }

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
            }

            else if (TransType.Trim() == "U")
            {
                String UserId = cla.GetExecuteScalar("select UserID from Tbl_M_LoginDetails where UserName='" + UserName.Trim() + "' and IsDeleted is null ");

                if (Comcls.RecordExistanceChk("Tbl_M_LoginDetails", "UserName", UserName.Trim(), "UserID", Convert.ToInt32(UserId)) == false)
                {

                    clsRtnMssage d = new clsRtnMssage();
                    d.MessageType = "Warning";
                    d.Message = "User Name  " + UserName.ToUpper() + " is not exist";
                    lst.Add(d);
                    d.ResponseCode = "100";
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }



                String str = "";

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

                        str = " UPDATE Tbl_M_LoginDetails SET Desig_ID=" + Desig_ID.ToString() + ", LoginAs=1 , FullName=N'" + FullName.Trim() + "', UPass='" + Password.Trim() + "',FromAPI='1',FFSID='" + FFSID + "'   WHERE UserId=" + UserId.ToString() + "";// UserName='" + UserName.Trim() + "',

                        cla.ExecuteCommand(str, command);

                        cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails_Child SET IsDeleted='1' where  UserId = " + UserId.ToString() + "", command);

                        String[] Vlg = CommaSeparatedVillage.Trim().Split(',');
                        for (int x = 0; x != Vlg.Length; x++)
                        {
                            //String VillageID = cla.GetExecuteScalar("SELECT VillageID FROM Tbl_M_VillageMaster where VillageCode='" + Vlg[x].Trim() + "' and IsDeleted is null ", command);
                            //if (VillageID.Trim().Length > 0)
                            //{
                            //    str = " INSERT INTO Tbl_M_LoginDetails_Child (UserChildId, UserId, VillageID)";//VillageID,
                            //    str += " VALUES(" + UserChildId + "," + UserId + "," + VillageID.Trim() + ")";
                            //    cla.ExecuteCommand(str, command);
                            //    UserChildId++;
                            //}

                            DataTable dta = cla.GetDataTable("SELECT VillageID FROM Tbl_M_VillageMaster where VillageCode='" + Vlg[x].Trim() + "' and IsDeleted is null ", command);
                            for (int x1 = 0; x1 != dta.Rows.Count; x1++)
                            {

                                str = " INSERT INTO Tbl_M_LoginDetails_Child (UserChildId, UserId, VillageID,FromSource)";//VillageID,
                                str += " VALUES(" + UserChildId + "," + UserId + "," + dta.Rows[x1]["VillageID"].ToString().Trim() + ",'FFS')";
                                cla.ExecuteCommand(str, command);
                                UserChildId++;

                            }
                        }

                        cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails_Role SET IsDeleted='1' where  UserId = " + UserId.ToString() + "", command);
                        Vlg = CommaSeparatedUserRoleID.Trim().Split(',');
                        for (int x = 0; x != Vlg.Length; x++)
                        {

                            cla.ExecuteCommand(" INSERT INTO Tbl_M_LoginDetails_Role (UserWiseRoleId, UserId, UserRoleID) VALUES (" + UserWiseRoleId + "," + UserId.ToString() + "," + Vlg[x].Trim().Trim() + ") ", command);
                            UserWiseRoleId++;


                        }


                        transaction.Commit();
                        clsRtnMssage d = new clsRtnMssage();
                        d.MessageType = "Sucess";
                        d.UserID = UserId.ToString();
                        d.Message = "Record Updated Sucessfully";
                        d.ResponseCode = "200";
                        lst.Add(d);
                    }
                    catch (Exception ex)
                    {


                        try
                        {
                            transaction.Rollback();

                        }
                        catch
                        {

                        }

                        clsRtnMssage d = new clsRtnMssage();
                        d.MessageType = "Error";
                        d.Message = ex.ToString();
                        lst.Add(d);

                    }
                    finally
                    {
                        if (connection != null) { connection.Close(); }
                        command.Dispose();
                    }

                }

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
            }

        }
        #endregion

        #region 'District Level user'
        [WebMethod]
        public void CreateUserDistrictsLevel(String SecurityKey, String FullName, String UserName, String Password, String Desig_ID, String CommaSeparatedUserRoleID, String CommaSeparatedDistricts, String FFSID, String TransType)
        {
            List<clsRtnMssage> lst = new List<clsRtnMssage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            if (MyCommanClassAPI.CheckApiAuthrization("104", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsRtnMssage d = new clsRtnMssage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                d.ResponseCode = "100";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //------------validations ---------------------// 
            if (TransType.Trim() == "I")
            {
                if (Comcls.RecordExistanceChk("Tbl_M_LoginDetails", "UserName", UserName.Trim(), "UserID") == false)
                {
                    clsRtnMssage d = new clsRtnMssage();
                    d.MessageType = "Warning";
                    d.Message = "User Name  " + UserName.ToUpper() + " is already created";
                    lst.Add(d);
                    d.ResponseCode = "100";
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }

                if (UserName.Trim().Length == 0)
                {

                    clsRtnMssage d = new clsRtnMssage();
                    d.MessageType = "Warning";
                    d.Message = "Please Fill User Name";
                    lst.Add(d);
                    d.ResponseCode = "100";
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }
                else if (Password.Trim().Length == 0)
                {

                    clsRtnMssage d = new clsRtnMssage();
                    d.MessageType = "Warning";
                    d.Message = "Please Fill Password";
                    lst.Add(d);
                    d.ResponseCode = "100";
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }

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


                        str = " INSERT INTO Tbl_M_LoginDetails (UserId, Desig_ID, LoginAs, FullName, UserName, UPass,FromAPI,FFSID)";
                        str += " VALUES(" + UserId + "," + Desig_ID.ToString() + ", '1',N'" + FullName.Trim() + "','" + UserName.Trim() + "','" + Password.Trim() + "','1','" + FFSID + "')";
                        cla.ExecuteCommand(str, command);

                        cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails_Child SET IsDeleted='1' where  UserId = " + UserId.ToString() + "", command);

                        String[] Dist = CommaSeparatedDistricts.Trim().Split(',');
                        for (int x = 0; x != Dist.Length; x++)
                        {
                            String City_ID = cla.GetExecuteScalar("SELECT City_ID FROM Tbl_M_City where CityName='" + Dist[x].Trim() + "' and IsDeleted is null ", command);
                            if (City_ID.Trim().Length > 0)
                            {
                                str = " INSERT INTO Tbl_M_LoginDetails_Child (UserChildId, UserId, City_ID,FromSource)";//VillageID,
                                str += " VALUES(" + UserChildId + "," + UserId + "," + City_ID.Trim() + ",'FFS')";
                                cla.ExecuteCommand(str, command);
                                UserChildId++;
                            }
                        }

                        cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails_Role SET IsDeleted='1' where  UserId = " + UserId.ToString() + "", command);
                        Dist = CommaSeparatedUserRoleID.Trim().Split(',');
                        for (int x = 0; x != Dist.Length; x++)
                        {

                            cla.ExecuteCommand(" INSERT INTO Tbl_M_LoginDetails_Role (UserWiseRoleId, UserId, UserRoleID) VALUES (" + UserWiseRoleId + "," + UserId.ToString() + "," + Dist[x].Trim().Trim() + ") ", command);
                            UserWiseRoleId++;


                        }


                        transaction.Commit();
                        clsRtnMssage d = new clsRtnMssage();
                        d.MessageType = "Sucess";
                        d.UserID = UserId.ToString();
                        d.Message = "Record Saved Sucessfully";
                        d.ResponseCode = "200";
                        lst.Add(d);
                    }
                    catch (Exception ex)
                    {


                        try
                        {
                            transaction.Rollback();

                        }
                        catch
                        {

                        }

                        clsRtnMssage d = new clsRtnMssage();
                        d.MessageType = "Error";
                        d.Message = ex.ToString();
                        lst.Add(d);

                    }
                    finally
                    {
                        if (connection != null) { connection.Close(); }
                        command.Dispose();
                    }

                }

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
            }

            else if (TransType.Trim() == "U")
            {
                String UserId = cla.GetExecuteScalar("select UserID from Tbl_M_LoginDetails where UserName='" + UserName.Trim() + "' and IsDeleted is null ");

                if (Comcls.RecordExistanceChk("Tbl_M_LoginDetails", "UserName", UserName.Trim(), "UserID", Convert.ToInt32(UserId)) == false)
                {

                    clsRtnMssage d = new clsRtnMssage();
                    d.MessageType = "Warning";
                    d.Message = "User Name  " + UserName.ToUpper() + " is not exist";
                    lst.Add(d);
                    d.ResponseCode = "100";
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }



                String str = "";

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

                        str = " UPDATE Tbl_M_LoginDetails SET Desig_ID=" + Desig_ID.ToString() + ", LoginAs=1 , FullName=N'" + FullName.Trim() + "', UPass='" + Password.Trim() + "',FromAPI='1',FFSID='" + FFSID + "'   WHERE UserId=" + UserId.ToString() + ""; // UserName='" + UserName.Trim() + "',

                        cla.ExecuteCommand(str, command);
                        cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails_Child SET IsDeleted='1' where  UserId = " + UserId.ToString() + "", command);

                        String[] Dist = CommaSeparatedDistricts.Trim().Split(',');
                        for (int x = 0; x != Dist.Length; x++)
                        {
                            String City_ID = cla.GetExecuteScalar("SELECT City_ID FROM Tbl_M_City where CityName='" + Dist[x].Trim() + "' and IsDeleted is null ", command);
                            if (City_ID.Trim().Length > 0)
                            {
                                str = " INSERT INTO Tbl_M_LoginDetails_Child (UserChildId, UserId, City_ID,FromSource)";//VillageID,
                                str += " VALUES(" + UserChildId + "," + UserId + "," + City_ID.Trim() + ",'FFS')";
                                cla.ExecuteCommand(str, command);
                                UserChildId++;
                            }
                        }

                        cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails_Role SET IsDeleted='1' where  UserId = " + UserId.ToString() + "", command);
                        Dist = CommaSeparatedUserRoleID.Trim().Split(',');
                        for (int x = 0; x != Dist.Length; x++)
                        {

                            cla.ExecuteCommand(" INSERT INTO Tbl_M_LoginDetails_Role (UserWiseRoleId, UserId, UserRoleID) VALUES (" + UserWiseRoleId + "," + UserId.ToString() + "," + Dist[x].Trim().Trim() + ") ", command);
                            UserWiseRoleId++;


                        }


                        transaction.Commit();
                        clsRtnMssage d = new clsRtnMssage();
                        d.MessageType = "Sucess";
                        d.UserID = UserId.ToString();
                        d.Message = "Record Updated Sucessfully";
                        d.ResponseCode = "200";
                        lst.Add(d);
                    }
                    catch (Exception ex)
                    {


                        try
                        {
                            transaction.Rollback();

                        }
                        catch
                        {

                        }

                        clsRtnMssage d = new clsRtnMssage();
                        d.MessageType = "Error";
                        d.Message = ex.ToString();
                        lst.Add(d);

                    }
                    finally
                    {
                        if (connection != null) { connection.Close(); }
                        command.Dispose();
                    }

                }

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
            }
        }
        #endregion


        #region 'Taluka Level User'
        [WebMethod]
        public void CreateUserTalukaLevel(String SecurityKey, String FullName, String UserName, String Password, String Desig_ID, String CommaSeparatedUserRoleID, String CommaSeparatedFFSTalukaID, String FFSUSerID, String TransType)
        {
            List<clsRtnMssage> lst = new List<clsRtnMssage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            if (MyCommanClassAPI.CheckApiAuthrization("103", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsRtnMssage d = new clsRtnMssage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                d.ResponseCode = "100";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //------------validations ---------------------// 
            if (TransType.Trim() == "I")
            {
                if (Comcls.RecordExistanceChk("Tbl_M_LoginDetails", "UserName", UserName.Trim(), "UserID") == false)
                {
                    clsRtnMssage d = new clsRtnMssage();
                    d.MessageType = "Warning";
                    d.Message = "User Name  " + UserName.ToUpper() + " is already created";
                    lst.Add(d);
                    d.ResponseCode = "100";
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }

                if (UserName.Trim().Length == 0)
                {

                    clsRtnMssage d = new clsRtnMssage();
                    d.MessageType = "Warning";
                    d.Message = "Please Fill User Name";
                    lst.Add(d);
                    d.ResponseCode = "100";
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }
                else if (Password.Trim().Length == 0)
                {

                    clsRtnMssage d = new clsRtnMssage();
                    d.MessageType = "Warning";
                    d.Message = "Please Fill Password";
                    lst.Add(d);
                    d.ResponseCode = "100";
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }

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


                        str = " INSERT INTO Tbl_M_LoginDetails (UserId, Desig_ID, LoginAs, FullName, UserName, UPass,FromAPI,FFSID)";
                        str += " VALUES(" + UserId + "," + Desig_ID.ToString() + ", '1',N'" + FullName.Trim() + "','" + UserName.Trim() + "','" + Password.Trim() + "','1','" + FFSUSerID + "')";
                        cla.ExecuteCommand(str, command);

                        cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails_Child SET IsDeleted='1' where  UserId = " + UserId.ToString() + "", command);

                        String[] Vlg = CommaSeparatedFFSTalukaID.Trim().Split(',');
                        for (int x = 0; x != Vlg.Length; x++)
                        {
                            DataTable dta = cla.GetDataTable("SELECT TalukaID FROM Tbl_M_TalukaMaster where FFSTalukaID=" + Vlg[x].Trim() + " and IsDeleted is null ", command);
                            for (int x1 = 0; x1 != dta.Rows.Count; x1++)
                            {

                                str = " INSERT INTO Tbl_M_LoginDetails_Child (UserChildId, UserId, TalukaID,FromSource)";//VillageID,
                                str += " VALUES(" + UserChildId + "," + UserId + "," + dta.Rows[x1]["TalukaID"].ToString().Trim() + ",'FFS')";
                                cla.ExecuteCommand(str, command);
                                UserChildId++;

                            }

                        }

                        cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails_Role SET IsDeleted='1' where  UserId = " + UserId.ToString() + "", command);
                        Vlg = CommaSeparatedUserRoleID.Trim().Split(',');
                        for (int x = 0; x != Vlg.Length; x++)
                        {

                            cla.ExecuteCommand(" INSERT INTO Tbl_M_LoginDetails_Role (UserWiseRoleId, UserId, UserRoleID) VALUES (" + UserWiseRoleId + "," + UserId.ToString() + "," + Vlg[x].Trim().Trim() + ") ", command);
                            UserWiseRoleId++;


                        }


                        transaction.Commit();
                        clsRtnMssage d = new clsRtnMssage();
                        d.MessageType = "Sucess";
                        d.UserID = UserId.ToString();
                        d.Message = "Record Saved Sucessfully";
                        d.ResponseCode = "200";
                        lst.Add(d);
                    }
                    catch (Exception ex)
                    {


                        try
                        {
                            transaction.Rollback();

                        }
                        catch
                        {

                        }

                        clsRtnMssage d = new clsRtnMssage();
                        d.MessageType = "Error";
                        d.Message = ex.ToString();
                        lst.Add(d);

                    }
                    finally
                    {
                        if (connection != null) { connection.Close(); }
                        command.Dispose();
                    }

                }

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
            }

            else if (TransType.Trim() == "U")
            {
                String UserId = cla.GetExecuteScalar("select UserID from Tbl_M_LoginDetails where UserName='" + UserName.Trim() + "' and IsDeleted is null ");

                if (Comcls.RecordExistanceChk("Tbl_M_LoginDetails", "UserName", UserName.Trim(), "UserID", Convert.ToInt32(UserId)) == false)
                {

                    clsRtnMssage d = new clsRtnMssage();
                    d.MessageType = "Warning";
                    d.Message = "User Name  " + UserName.ToUpper() + " is not exist";
                    lst.Add(d);
                    d.ResponseCode = "100";
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }



                String str = "";

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

                        str = " UPDATE Tbl_M_LoginDetails SET Desig_ID=" + Desig_ID.ToString() + ", LoginAs=1 , FullName=N'" + FullName.Trim() + "', UPass='" + Password.Trim() + "',FromAPI='1',FFSID='" + FFSUSerID + "'   WHERE UserId=" + UserId.ToString() + "";// UserName='" + UserName.Trim() + "',

                        cla.ExecuteCommand(str, command);

                        cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails_Child SET IsDeleted='1' where  UserId = " + UserId.ToString() + "", command);

                        String[] Vlg = CommaSeparatedFFSTalukaID.Trim().Split(',');
                        for (int x = 0; x != Vlg.Length; x++)
                        {
                            DataTable dta = cla.GetDataTable("SELECT TalukaID FROM Tbl_M_TalukaMaster where FFSTalukaID=" + Vlg[x].Trim() + " and IsDeleted is null ", command);
                            for (int x1 = 0; x1 != dta.Rows.Count; x1++)
                            {

                                str = " INSERT INTO Tbl_M_LoginDetails_Child (UserChildId, UserId, TalukaID,FromSource)";//VillageID,
                                str += " VALUES(" + UserChildId + "," + UserId + "," + dta.Rows[x1]["TalukaID"].ToString().Trim() + ",'FFS')";
                                cla.ExecuteCommand(str, command);
                                UserChildId++;

                            }
                        }

                        cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails_Role SET IsDeleted='1' where  UserId = " + UserId.ToString() + "", command);
                        Vlg = CommaSeparatedUserRoleID.Trim().Split(',');
                        for (int x = 0; x != Vlg.Length; x++)
                        {

                            cla.ExecuteCommand(" INSERT INTO Tbl_M_LoginDetails_Role (UserWiseRoleId, UserId, UserRoleID) VALUES (" + UserWiseRoleId + "," + UserId.ToString() + "," + Vlg[x].Trim().Trim() + ") ", command);
                            UserWiseRoleId++;


                        }


                        transaction.Commit();
                        clsRtnMssage d = new clsRtnMssage();
                        d.MessageType = "Sucess";
                        d.UserID = UserId.ToString();
                        d.Message = "Record Updated Sucessfully";
                        d.ResponseCode = "200";
                        lst.Add(d);
                    }
                    catch (Exception ex)
                    {


                        try
                        {
                            transaction.Rollback();

                        }
                        catch
                        {

                        }

                        clsRtnMssage d = new clsRtnMssage();
                        d.MessageType = "Error";
                        d.Message = ex.ToString();
                        lst.Add(d);

                    }
                    finally
                    {
                        if (connection != null) { connection.Close(); }
                        command.Dispose();
                    }

                }

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
            }

        }
        #endregion


        #region' Change Password'
        [WebMethod]
        public void ChangePassword(String SecurityKey, String UserName, String Password, String FFSID)
        {
            List<clsRtnMssage> lst = new List<clsRtnMssage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            if (MyCommanClassAPI.CheckApiAuthrization("105", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsRtnMssage d = new clsRtnMssage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                d.ResponseCode = "100";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //------------validations ---------------------// 

            if (UserName.Trim().Length == 0)
            {

                clsRtnMssage d = new clsRtnMssage();
                d.MessageType = "Warning";
                d.Message = "Please Fill User Name";
                lst.Add(d);
                d.ResponseCode = "100";
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (Password.Trim().Length == 0)
            {

                clsRtnMssage d = new clsRtnMssage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Password";
                lst.Add(d);
                d.ResponseCode = "100";
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }


            //------------validations ---------------------// 

            String str = "";
            //  int UserId = cla.TableID("Tbl_M_LoginDetails", "UserId");

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


                    if (UserName.ToString().Trim().Length > 0)
                    {
                        // UPDATE
                        str = " UPDATE  Tbl_M_LoginDetails SET  UPass='" + Password.Trim() + "' , FromAPI='1' ,FFSID='" + FFSID + "' ";//, IsDeleted=NULL
                        str += " WHERE(UserName = '" + UserName.Trim() + "')";
                        cla.ExecuteCommand(str, command);


                    }

                    transaction.Commit();
                    clsRtnMssage d = new clsRtnMssage();
                    d.MessageType = "Sucess";
                    d.UserID = UserName.Trim();
                    d.Message = "Your Password has been changed successfully!";
                    lst.Add(d);
                }
                catch (Exception ex)
                {


                    try
                    {
                        transaction.Rollback();

                    }
                    catch
                    {

                    }

                    clsRtnMssage d = new clsRtnMssage();
                    d.MessageType = "Error";
                    d.Message = ex.ToString();
                    lst.Add(d);

                }
                finally
                {
                    if (connection != null) { connection.Close(); }
                    command.Dispose();
                }

            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
        }

        #endregion


        #region' MakeIt In Active'
        [WebMethod]
        public void MakeItInActive(String SecurityKey, String UserName, String FFSID)
        {
            List<clsRtnMssage> lst = new List<clsRtnMssage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            if (MyCommanClassAPI.CheckApiAuthrization("106", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsRtnMssage d = new clsRtnMssage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                d.ResponseCode = "100";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //------------validations ---------------------// 

            if (UserName.Trim().Length == 0)
            {

                clsRtnMssage d = new clsRtnMssage();
                d.MessageType = "Warning";
                d.Message = "Please Fill User Name";
                lst.Add(d);
                d.ResponseCode = "100";
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }



            //------------validations ---------------------// 

            String str = "";
            //  int UserId = cla.TableID("Tbl_M_LoginDetails", "UserId");

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


                    if (UserName.ToString().Trim().Length > 0)
                    {
                        // UPDATE
                        str = " UPDATE  Tbl_M_LoginDetails SET  IsDeleted='1' , FromAPI='1' ,FFSID='" + FFSID + "', DeleteFromSource='FFS'  ";//, IsDeleted=NULL
                        str += " WHERE(UserName = '" + UserName.Trim() + "')";
                        cla.ExecuteCommand(str, command);


                    }

                    transaction.Commit();
                    clsRtnMssage d = new clsRtnMssage();
                    d.MessageType = "Sucess";
                    d.UserID = UserName.Trim();
                    d.Message = "Status changed successfully!";
                    lst.Add(d);
                }
                catch (Exception ex)
                {


                    try
                    {
                        transaction.Rollback();

                    }
                    catch
                    {

                    }

                    clsRtnMssage d = new clsRtnMssage();
                    d.MessageType = "Error";
                    d.Message = ex.ToString();
                    lst.Add(d);

                }
                finally
                {
                    if (connection != null) { connection.Close(); }
                    command.Dispose();
                }

            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
        }

        #endregion
    }



}









public class clsRtnMssage
{
    public String UserID { get; set; }
    public String ResponseCode { get; set; }
    public String MessageType { get; set; }
    public string Message { get; set; }


}