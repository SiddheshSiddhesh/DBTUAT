using CommanClsLibrary;
using CommanClsLibrary.Repository.Classes;
using CommanClsLibrary.Repository.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;
using static CommanClsLibrary.Repository.Enums;

namespace DBTPoCRA.APPData
{
    /// <summary>
    /// Summary description for Registration
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Registration : System.Web.Services.WebService
    {


        #region "Farmer"

        [WebMethod]
        public void SaveAAudharDetails(String SecurityKey, String UpRegistrationID, String AADHARNo, String Name, String DOB, String Gender, String LandStatus, String FileLandLessCertificate)
        {
            List<clsReturnMessage> lst = new List<clsReturnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            AzureBlobHelper fileRet = new AzureBlobHelper();


            ////---------
            //clsReturnMessage d1 = new clsReturnMessage();
            //d1.MessageType = "Error";
            //d1.Message = clsSettings.StrCommanMessgae;
            //lst.Add(d1);

            //Context.Response.Clear();
            //Context.Response.ContentType = "application/json";
            ////Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            //Context.Response.Flush();
            //Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

            ////--------



            if (MyCommanClassAPI.CheckApiAuthrization("42", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //------------validations ---------------------// 


            //AdharVaultAPICalls api = new AdharVaultAPICalls();
            //string ReferenceNumber = api.GetReferenceFromAdhar(AADHARNo.Trim());
            //if (Comcls.RecordExistanceChk("Tbl_M_RegistrationDetails_Bolt", "ADVRefrenceID", ReferenceNumber.Trim(), "BoltID") == false)
            if (Comcls.RecordExistanceChk("Tbl_M_RegistrationDetails_Bolt", "AaDharNumber", AADHARNo.Trim(), "BoltID") == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Aadhaar number " + AADHARNo.ToUpper() + " is already registered";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //String RegExId = cla.GetExecuteScalar("Select top 1 RegistrationID  from Tbl_M_RegistrationDetails where IsDeleted is null and  BoltID in (select top 1 BoltID from Tbl_M_RegistrationDetails_Bolt where ADVRefrenceID='" + ReferenceNumber.Trim() + "' order by BoltID desc)");

            String RegExId = cla.GetExecuteScalar("Select top 1 RegistrationID  from Tbl_M_RegistrationDetails where IsDeleted is null and  BoltID in (select top 1 BoltID from Tbl_M_RegistrationDetails_Bolt where AaDharNumber='" + AADHARNo.Trim() + "' order by BoltID desc)");
            if (RegExId.Trim().Length > 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Aadhaar number " + AADHARNo.ToUpper() + " is already registered";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            String BeneficiaryTypesID = "1";


            if (AADHARNo.Trim().Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Aadhaar Number";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (Name.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Name";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            String str = "";
            int RegistrationID = cla.TableID("Tbl_M_RegistrationDetails", "RegistrationID");
            int BoltID = cla.TableID("Tbl_M_RegistrationDetails_Bolt", "BoltID");


            String LandLessCertificate = "";
            if (UpRegistrationID.Trim().Length == 0)
            {
                if (RegistrationID.ToString().Trim().Length > 0)
                {
                    //String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString() + "");
                    String PathUp = "DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "";

                    //if (!Directory.Exists(path))
                    //{
                    //    // Try to create the directory.
                    //    DirectoryInfo di = Directory.CreateDirectory(path);
                    //}
                    if (FileLandLessCertificate.Length > 0)
                    {

                        LandLessCertificate = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "/LandLessCertificate" + MyCommanClassAPI.GetFileExtension(FileLandLessCertificate);
                        string imageName = "LandLessCertificate" + MyCommanClassAPI.GetFileExtension(FileLandLessCertificate);
                        //set the image path
                        //string imgPath = path + imageName;
                        byte[] imageBytes = Convert.FromBase64String(FileLandLessCertificate);
                        String ret = fileRet.UploadData(PathUp, imageName, imageBytes);
                        if (ret.Trim().Length > 0)
                        {
                            clsReturnMessage d = new clsReturnMessage();
                            d.MessageType = "Warning";
                            d.Message = "Please upload LandLessCertificate";
                            lst.Add(d);
                            Context.Response.Clear();
                            Context.Response.ContentType = "application/json";
                            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                            Context.Response.Flush();
                            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                            return;
                        }
                        //File.WriteAllBytes(imgPath, imageBytes);

                    }
                }
            }

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


                    if (UpRegistrationID.Trim().Length == 0)
                    {

                        //cla.ExecuteCommand("INSERT INTO Tbl_M_RegistrationDetails_Bolt(BoltID, ADVRefrenceID) VALUES (" + BoltID + ",'" + ReferenceNumber.Trim().ToUpper() + "')", command);

                        cla.ExecuteCommand("INSERT INTO Tbl_M_RegistrationDetails_Bolt(BoltID, AaDharNumber) VALUES (" + BoltID + ",'" + AADHARNo.Trim().ToUpper() + "')", command);


                        // ADD NEW 
                        str = " INSERT INTO Tbl_M_RegistrationDetails (RegistrationID, BeneficiaryTypesID, RegisterName, DateOfBirth, Gender,IsDeleted,BoltID,RegSource)";
                        str += " VALUES(" + RegistrationID + "," + BeneficiaryTypesID + ",N'" + Name.Trim() + "','" + cla.mdy(DOB.Trim()) + "','" + Gender.Trim() + "','1'," + BoltID + ",'Mobile')";
                        cla.ExecuteCommand(str, command);



                        if (RegistrationID.ToString().Trim().Length > 0)
                        {
                            if (LandStatus.Trim().ToUpper() == "NO")
                            {
                                str = " UPDATE  Tbl_M_RegistrationDetails SET   LandStatus ='" + LandStatus.Trim().ToUpper() + "', LandLessCertificate='" + LandLessCertificate + "' ";
                                str += " WHERE(RegistrationID = " + RegistrationID.ToString().Trim() + ")";
                                cla.ExecuteCommand(str, command);
                            }
                            else
                            {
                                str = " UPDATE  Tbl_M_RegistrationDetails SET   LandStatus ='" + LandStatus.Trim().ToUpper() + "' ";
                                str += " WHERE(RegistrationID = " + RegistrationID.ToString().Trim() + ")";
                                cla.ExecuteCommand(str, command);
                            }
                        }

                        clsReturnMessage d = new clsReturnMessage();
                        d.MessageType = "Sucess";
                        d.RegistrationID = RegistrationID.ToString();
                        d.Message = "Record Saved Sucessfully";
                        lst.Add(d);
                    }
                    else
                    {
                        // EDIT 

                    }



                    transaction.Commit();

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

                    clsReturnMessage d = new clsReturnMessage();
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
        //
        #region "Farmer registration"

        [WebMethod]
        public void SaveAAudharDetail(String SecurityKey, String UpRegistrationID, String AADHARNo, String Name, String DOB, String Gender, String LandStatus, String FileLandLessCertificate, String RelatedToBeneficiaryCriteria, String BENEFICIARYCriteriaCertificates)
        {
            List<clsReturnMessage> lst = new List<clsReturnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            AzureBlobHelper fileRet = new AzureBlobHelper();

            if (MyCommanClassAPI.CheckApiAuthrization("42", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //------------validations ---------------------// 

            //AdharVaultAPICalls api = new AdharVaultAPICalls();
            //string ReferenceNumber = api.GetReferenceFromAdhar(AADHARNo.Trim());
            //if (Comcls.RecordExistanceChk("Tbl_M_RegistrationDetails_Bolt", "ADVRefrenceID", ReferenceNumber.Trim(), "BoltID") == false)
            if (Comcls.RecordExistanceChk("Tbl_M_RegistrationDetails_Bolt", "AaDharNumber", AADHARNo.Trim(), "BoltID") == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Aadhaar number " + AADHARNo.ToUpper() + " is already registered";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //string RegExId = cla.GetExecuteScalar("Select top 1 RegistrationID  from Tbl_M_RegistrationDetails where IsDeleted is null and  BoltID in (select top 1 BoltID from Tbl_M_RegistrationDetails_Bolt where ADVRefrenceID='" + ReferenceNumber.Trim() + "' order by BoltID desc)");
            string RegExId = cla.GetExecuteScalar("Select top 1 RegistrationID  from Tbl_M_RegistrationDetails where IsDeleted is null and  BoltID in (select top 1 BoltID from Tbl_M_RegistrationDetails_Bolt where AaDharNumber='" + AADHARNo.Trim() + "' order by BoltID desc)");
            if (RegExId.Trim().Length > 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Aadhaar number " + AADHARNo.ToUpper() + " is already registered";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            String BeneficiaryTypesID = "1";


            if (AADHARNo.Trim().Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Aadhaar Number";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (Name.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Name";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }


            String str = "";
            int RegistrationID = cla.TableID("Tbl_M_RegistrationDetails", "RegistrationID");
            int BoltID = cla.TableID("Tbl_M_RegistrationDetails_Bolt", "BoltID");

            String OtherCriteriaCertificates = "";
            String LandLessCertificate = "";




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
                    //cla.ExecuteCommand("INSERT INTO Tbl_M_RegistrationDetails_Bolt(BoltID, ADVRefrenceID) VALUES (" + BoltID + ",'" + ReferenceNumber.Trim().ToUpper() + "')", command);
                    cla.ExecuteCommand("INSERT INTO Tbl_M_RegistrationDetails_Bolt(BoltID, AaDharNumber) VALUES (" + BoltID + ",'" + AADHARNo.Trim().ToUpper() + "')", command);
                    // ADD NEW 
                    str = " INSERT INTO Tbl_M_RegistrationDetails (RegistrationID, BeneficiaryTypesID, RegisterName, DateOfBirth, Gender,IsDeleted,BoltID,RegSource)";
                    str += " VALUES(" + RegistrationID + "," + BeneficiaryTypesID + ",N'" + Name.Trim() + "','" + cla.mdy(DOB.Trim()) + "','" + Gender.Trim() + "','1'," + BoltID + ",'Mobile')";
                    cla.ExecuteCommand(str, command);



                    if (RegistrationID.ToString().Trim().Length > 0)
                    {
                        if (LandStatus.Trim().ToUpper() == "NO")
                        {
                            str = " UPDATE  Tbl_M_RegistrationDetails SET   LandStatus ='" + LandStatus.Trim().ToUpper() + "', LandLessCertificate='" + LandLessCertificate + "' ";
                            str += " WHERE(RegistrationID = " + RegistrationID.ToString().Trim() + ")";
                            cla.ExecuteCommand(str, command);
                        }
                        else
                        {
                            str = " UPDATE  Tbl_M_RegistrationDetails SET   LandStatus ='" + LandStatus.Trim().ToUpper() + "' ";
                            str += " WHERE(RegistrationID = " + RegistrationID.ToString().Trim() + ")";
                            cla.ExecuteCommand(str, command);
                        }
                        str = " UPDATE  Tbl_M_RegistrationDetails SET   AnyOtherDocType ='" + RelatedToBeneficiaryCriteria.Trim().ToUpper() + "', AnyOtherDoc='" + OtherCriteriaCertificates + "' ";
                        str += " WHERE(RegistrationID = " + RegistrationID.ToString().Trim() + ")";
                        cla.ExecuteCommand(str, command);
                    }

                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Sucess";
                    d.RegistrationID = RegistrationID.ToString();
                    d.Message = "Record Saved Sucessfully";
                    lst.Add(d);

                    transaction.Commit();


                    if (RegistrationID.ToString().Trim().Length > 0)
                    {
                        // String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString() + "");
                        String PathUp = "DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "";
                        if (FileLandLessCertificate.Length > 0)
                        {

                            LandLessCertificate = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "/LandLessCertificate" + MyCommanClassAPI.GetFileExtension(FileLandLessCertificate);
                            string imageName = "LandLessCertificate" + MyCommanClassAPI.GetFileExtension(FileLandLessCertificate);
                            byte[] imageBytes = Convert.FromBase64String(FileLandLessCertificate);
                            String ret = fileRet.UploadData(PathUp, imageName, imageBytes);
                            if (ret.Trim().Length > 0)
                            {
                                clsReturnMessage d1 = new clsReturnMessage();
                                d1.MessageType = "Warning";
                                d1.Message = "Please upload LandLessCertificate";
                                lst.Add(d1);
                                Context.Response.Clear();
                                Context.Response.ContentType = "application/json";
                                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                                Context.Response.Flush();
                                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                                return;
                            }

                        }
                        if (BENEFICIARYCriteriaCertificates.Length > 0)
                        {
                            OtherCriteriaCertificates = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "/AnyOtherCertificate" + MyCommanClassAPI.GetFileExtension(BENEFICIARYCriteriaCertificates);
                            string imageName = "AnyOtherCertificate" + MyCommanClassAPI.GetFileExtension(BENEFICIARYCriteriaCertificates);
                            byte[] imageBytes = Convert.FromBase64String(BENEFICIARYCriteriaCertificates);
                            String ret = fileRet.UploadData(PathUp, imageName, imageBytes);
                            if (ret.Trim().Length > 0)
                            {
                                clsReturnMessage d2 = new clsReturnMessage();
                                d2.MessageType = "Warning";
                                d2.Message = "Please upload AnyOtherCertificate";
                                lst.Add(d2);
                                Context.Response.Clear();
                                Context.Response.ContentType = "application/json";
                                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                                Context.Response.Flush();
                                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                                return;
                            }
                        }

                        if (RegistrationID.ToString().Trim().Length > 0)
                        {
                            if (LandStatus.Trim().ToUpper() == "NO")
                            {
                                if (LandLessCertificate.Trim().Length > 0)
                                {
                                    str = " UPDATE  Tbl_M_RegistrationDetails SET   LandLessCertificate='" + LandLessCertificate + "' ";
                                    str += " WHERE(RegistrationID = " + RegistrationID.ToString().Trim() + ")";
                                    cla.ExecuteCommand(str);
                                }
                            }

                            if (OtherCriteriaCertificates.Trim().Length > 0)
                            {
                                str = " UPDATE  Tbl_M_RegistrationDetails SET   AnyOtherDocType ='" + RelatedToBeneficiaryCriteria.Trim().ToUpper() + "', AnyOtherDoc='" + OtherCriteriaCertificates + "' ";
                                str += " WHERE(RegistrationID = " + RegistrationID.ToString().Trim() + ")";
                                cla.ExecuteCommand(str);
                            }
                        }
                    }


                    MyCommanClass.UpdateLandStatusWiseWorkVillage(RegistrationID.ToString().Trim());





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

                    clsReturnMessage d = new clsReturnMessage();
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

        #region "Farmer registration"

        [WebMethod]
        public void SaveAAudharDetailWithMarathi(String SecurityKey, String UpRegistrationID, String AADHARNo, String Name, String DOB, String Gender, String LandStatus, String FileLandLessCertificate, String RelatedToBeneficiaryCriteria, String BENEFICIARYCriteriaCertificates, String NameInMarathi)
        {
            List<clsReturnMessage> lst = new List<clsReturnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            AzureBlobHelper fileRet = new AzureBlobHelper();


            if (MyCommanClassAPI.CheckApiAuthrization("42", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }


            //AdharVaultAPICalls api = new AdharVaultAPICalls();
            //string ReferenceNumber = api.GetReferenceFromAdhar(AADHARNo.Trim());

            //------------validations ---------------------// 
            //if (Comcls.RecordExistanceChk("Tbl_M_RegistrationDetails_Bolt", "ADVRefrenceID", ReferenceNumber.Trim(), "BoltID") == false)

            if (Comcls.RecordExistanceChk("Tbl_M_RegistrationDetails_Bolt", "AaDharNumber", AADHARNo.Trim(), "BoltID") == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Aadhaar number " + AADHARNo.ToUpper() + " is already registered";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //String RegExId = cla.GetExecuteScalar("Select top 1 RegistrationID  from Tbl_M_RegistrationDetails where IsDeleted is null and  BoltID in (select top 1 BoltID from Tbl_M_RegistrationDetails_Bolt where ADVRefrenceID='" + ReferenceNumber.Trim() + "' order by BoltID desc)");
            String RegExId = cla.GetExecuteScalar("Select top 1 RegistrationID  from Tbl_M_RegistrationDetails where IsDeleted is null and  BoltID in (select top 1 BoltID from Tbl_M_RegistrationDetails_Bolt where AaDharNumber='" + AADHARNo.Trim() + "' order by BoltID desc)");
            if (RegExId.Trim().Length > 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Aadhaar number " + AADHARNo.ToUpper() + " is already registered";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            String BeneficiaryTypesID = "1";


            if (AADHARNo.Trim().Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Aadhar Number";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (Name.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Name";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }


            String str = "";
            int RegistrationID = cla.TableID("Tbl_M_RegistrationDetails", "RegistrationID");
            int BoltID = cla.TableID("Tbl_M_RegistrationDetails_Bolt", "BoltID");

            String OtherCriteriaCertificates = "";
            String LandLessCertificate = "";




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
                    //cla.ExecuteCommand("INSERT INTO Tbl_M_RegistrationDetails_Bolt(BoltID, ADVRefrenceID) VALUES (" + BoltID + ",'" + ReferenceNumber.Trim().ToUpper() + "')", command);

                    cla.ExecuteCommand("INSERT INTO Tbl_M_RegistrationDetails_Bolt(BoltID, ADVRefrenceID) VALUES (" + BoltID + ",'" + AADHARNo.Trim().ToUpper() + "')", command);

                    // ADD NEW 
                    str = " INSERT INTO Tbl_M_RegistrationDetails (RegistrationID, BeneficiaryTypesID, RegisterName, DateOfBirth, Gender,IsDeleted,BoltID,RegSource,RegisterNameMr)";
                    str += " VALUES(" + RegistrationID + "," + BeneficiaryTypesID + ",N'" + Name.Trim() + "','" + cla.mdy(DOB.Trim()) + "','" + Gender.Trim() + "','1'," + BoltID + ",'Mobile',N'" + NameInMarathi + "')";
                    cla.ExecuteCommand(str, command);



                    if (RegistrationID.ToString().Trim().Length > 0)
                    {
                        if (LandStatus.Trim().ToUpper() == "NO")
                        {
                            str = " UPDATE  Tbl_M_RegistrationDetails SET   LandStatus ='" + LandStatus.Trim().ToUpper() + "', LandLessCertificate='" + LandLessCertificate + "' ";
                            str += " WHERE(RegistrationID = " + RegistrationID.ToString().Trim() + ")";
                            cla.ExecuteCommand(str, command);
                        }
                        else
                        {
                            str = " UPDATE  Tbl_M_RegistrationDetails SET   LandStatus ='" + LandStatus.Trim().ToUpper() + "' ";
                            str += " WHERE(RegistrationID = " + RegistrationID.ToString().Trim() + ")";
                            cla.ExecuteCommand(str, command);
                        }
                        str = " UPDATE  Tbl_M_RegistrationDetails SET   AnyOtherDocType ='" + RelatedToBeneficiaryCriteria.Trim().ToUpper() + "', AnyOtherDoc='" + OtherCriteriaCertificates + "' ";
                        str += " WHERE(RegistrationID = " + RegistrationID.ToString().Trim() + ")";
                        cla.ExecuteCommand(str, command);
                    }

                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Sucess";
                    d.RegistrationID = RegistrationID.ToString();
                    d.Message = "Record Saved Sucessfully";
                    lst.Add(d);




                    transaction.Commit();


                    if (RegistrationID.ToString().Trim().Length > 0)
                    {
                        // String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString() + "");
                        String PathUp = "DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "";
                        if (FileLandLessCertificate.Length > 0)
                        {

                            LandLessCertificate = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "/LandLessCertificate" + MyCommanClassAPI.GetFileExtension(FileLandLessCertificate);
                            string imageName = "LandLessCertificate" + MyCommanClassAPI.GetFileExtension(FileLandLessCertificate);
                            byte[] imageBytes = Convert.FromBase64String(FileLandLessCertificate);
                            String ret = fileRet.UploadData(PathUp, imageName, imageBytes);
                            if (ret.Trim().Length > 0)
                            {
                                clsReturnMessage d1 = new clsReturnMessage();
                                d1.MessageType = "Warning";
                                d1.Message = "Please upload LandLessCertificate";
                                lst.Add(d1);
                                Context.Response.Clear();
                                Context.Response.ContentType = "application/json";
                                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                                Context.Response.Flush();
                                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                                return;
                            }

                        }
                        if (BENEFICIARYCriteriaCertificates.Length > 0)
                        {
                            OtherCriteriaCertificates = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "/AnyOtherCertificate" + MyCommanClassAPI.GetFileExtension(BENEFICIARYCriteriaCertificates);
                            string imageName = "AnyOtherCertificate" + MyCommanClassAPI.GetFileExtension(BENEFICIARYCriteriaCertificates);
                            byte[] imageBytes = Convert.FromBase64String(BENEFICIARYCriteriaCertificates);
                            String ret = fileRet.UploadData(PathUp, imageName, imageBytes);
                            if (ret.Trim().Length > 0)
                            {
                                clsReturnMessage d2 = new clsReturnMessage();
                                d2.MessageType = "Warning";
                                d2.Message = "Please upload AnyOtherCertificate";
                                lst.Add(d2);
                                Context.Response.Clear();
                                Context.Response.ContentType = "application/json";
                                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                                Context.Response.Flush();
                                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                                return;
                            }
                        }

                        if (RegistrationID.ToString().Trim().Length > 0)
                        {
                            if (LandStatus.Trim().ToUpper() == "NO")
                            {
                                if (LandLessCertificate.Trim().Length > 0)
                                {
                                    str = " UPDATE  Tbl_M_RegistrationDetails SET   LandLessCertificate='" + LandLessCertificate + "' ";
                                    str += " WHERE(RegistrationID = " + RegistrationID.ToString().Trim() + ")";
                                    cla.ExecuteCommand(str);
                                }
                            }

                            if (OtherCriteriaCertificates.Trim().Length > 0)
                            {
                                str = " UPDATE  Tbl_M_RegistrationDetails SET   AnyOtherDocType ='" + RelatedToBeneficiaryCriteria.Trim().ToUpper() + "', AnyOtherDoc='" + OtherCriteriaCertificates + "' ";
                                str += " WHERE(RegistrationID = " + RegistrationID.ToString().Trim() + ")";
                                cla.ExecuteCommand(str);
                            }
                        }
                    }


                    MyCommanClass.UpdateLandStatusWiseWorkVillage(RegistrationID.ToString().Trim());





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

                    clsReturnMessage d = new clsReturnMessage();
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
        //
        #region
        [WebMethod]
        public void SaveBasicDetails(String SecurityKey, String RegistrationID, String CATEGORY, String FileCATEGORYCERITIFICATE, String HANDICAP, String FileHANDICAPCERITIFICATE, String MobileNumber, String MobileNumber2, String LandLineNumber, String EmailID, String PanNumber, String PhysicallyHandicap, String DisabilityPer, String Address1VillageID, String Address1PinCode, String Address1HouseNo, String Address1StreetName, String Address1City_ID, String Address1TalukaID, String Address1Post_ID)
        {
            List<clsReturnMessage> lst = new List<clsReturnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            AzureBlobHelper fileRet = new AzureBlobHelper();

            if (MyCommanClassAPI.CheckApiAuthrization("43", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }


            if (CATEGORY.Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select CATEGORY";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;

            }
            else if (CATEGORY.Trim() != "5")
            {
                if (FileCATEGORYCERITIFICATE.Length == 0)
                {

                    //clsReturnMessage d = new clsReturnMessage();
                    //d.MessageType = "Warning";
                    //d.Message = "Please attach Caste Certificate using UPLOAD CERITIFICATE.";
                    //lst.Add(d);
                    //Context.Response.Clear();
                    //Context.Response.ContentType = "application/json";
                    ////Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    //Context.Response.Flush();
                    //Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    //return;


                }
                else
                {
                    //if (FileCATEGORYCERITIFICATE.ContentLength > 3145728)
                    //{
                    //    clsMessages.Warningmsg(LiteralMsg, "Please attach max 3 MB file for  Caste Certificate");
                    //    return;
                    //}
                }
            }
            else if (HANDICAP.Trim() == "YES")
            {
                if (DisabilityPer.Trim().Length > 0)
                {
                    if (FileHANDICAPCERITIFICATE.Length == 0)
                    {

                        clsReturnMessage d = new clsReturnMessage();
                        d.MessageType = "Warning";
                        d.Message = "Please attach PHYSICALLY HANDICAP CERITIFICATE.";
                        lst.Add(d);
                        Context.Response.Clear();
                        Context.Response.ContentType = "application/json";
                        //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                        Context.Response.Flush();
                        Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                        return;


                    }
                    else
                    {
                        //if (FileHANDICAPCERITIFICATE.PostedFile.ContentLength > 3145728)
                        //{
                        //    clsMessages.Warningmsg(LiteralMsg, "Please attach max 3 MB file for  Caste Certificate");
                        //    return;
                        //}
                    }
                }
                else
                {

                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Warning";
                    d.Message = "Please fill DISABILITY PERCENTAGE ";
                    lst.Add(d);
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;

                }
            }
            //------------End validations ---------------------// 


            String str = "";
            String LandStatus = cla.GetExecuteScalar("SELECT   LandStatus FROM Tbl_M_RegistrationDetails where RegistrationID=" + RegistrationID.Trim() + "");

            String Work_ClustersMasterID = "", Work_CircleID = "";

            if (Address1Post_ID.Trim() != "")
            {
                cla.GetExecuteScalar("Select top 1 ClustersMasterID from Tbl_M_VillageMaster where VillageID=" + Address1Post_ID.Trim() + "");
            }

            if (Address1Post_ID.Trim() != "")
            {
                cla.GetExecuteScalar("Select top 1 CircleID from Tbl_M_VillageMaster where VillageID=" + Address1Post_ID.Trim() + "");
            }

            // String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "");
            String PathUp = "DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "";

            String CastCategoryDoc = "";
            String PhysicallyHandicapDoc = "";
            if (RegistrationID.ToString().Trim().Length > 0)
            {
                if (FileCATEGORYCERITIFICATE.Length > 0)
                {

                    CastCategoryDoc = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "/CasteCategoryCertificate" + MyCommanClassAPI.GetFileExtension(FileCATEGORYCERITIFICATE);
                    string imageName = "CasteCategoryCertificate" + MyCommanClassAPI.GetFileExtension(FileCATEGORYCERITIFICATE);
                    //set the image path
                    //string imgPath = path + imageName;
                    //byte[] imageBytes = Convert.FromBase64String(FileCATEGORYCERITIFICATE);
                    //File.WriteAllBytes(imgPath, imageBytes);

                    byte[] imageBytes = Convert.FromBase64String(FileCATEGORYCERITIFICATE);
                    //fileRet.UploadData(PathUp, imageName, imageBytes);
                    String ret = fileRet.UploadData(PathUp, imageName, imageBytes);
                    if (ret.Trim().Length > 0)
                    {
                        clsReturnMessage d = new clsReturnMessage();
                        d.MessageType = "Warning";
                        d.Message = "Please upload Caste Category Certificate";
                        lst.Add(d);
                        Context.Response.Clear();
                        Context.Response.ContentType = "application/json";
                        //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                        Context.Response.Flush();
                        Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                        return;
                    }
                }

                if (FileHANDICAPCERITIFICATE.Length > 0)
                {
                    PhysicallyHandicapDoc = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "/PhysicallyHandicapCertificate" + MyCommanClassAPI.GetFileExtension(FileHANDICAPCERITIFICATE);
                    string imageName = "PhysicallyHandicapCertificate" + MyCommanClassAPI.GetFileExtension(FileHANDICAPCERITIFICATE);
                    //set the image path
                    //string imgPath = path + imageName;
                    //byte[] imageBytes = Convert.FromBase64String(FileHANDICAPCERITIFICATE);
                    //File.WriteAllBytes(imgPath, imageBytes);
                    byte[] imageBytes = Convert.FromBase64String(FileHANDICAPCERITIFICATE);
                    // fileRet.UploadData(PathUp, imageName, imageBytes);
                    String ret = fileRet.UploadData(PathUp, imageName, imageBytes);
                    if (ret.Trim().Length > 0)
                    {
                        clsReturnMessage d = new clsReturnMessage();
                        d.MessageType = "Warning";
                        d.Message = "Please upload Physically Handicap Certificate";
                        lst.Add(d);
                        Context.Response.Clear();
                        Context.Response.ContentType = "application/json";
                        //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                        Context.Response.Flush();
                        Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                        return;
                    }
                }
            }


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


                    if (RegistrationID.ToString().Trim().Length > 0)
                    {
                        // UPDATE
                        str = " UPDATE  Tbl_M_RegistrationDetails SET   CategoryMasterID =" + CATEGORY.Trim() + ", MobileNumber ='" + MobileNumber.Trim() + "', ";
                        str += " MobileNumber2 ='" + MobileNumber2.Trim() + "', LandLineNumber ='" + LandLineNumber.Trim() + "', EmailID ='" + EmailID.Trim() + "', PanNumber ='" + PanNumber.Trim() + "', PhysicallyHandicap ='" + PhysicallyHandicap.Trim() + "', DisabilityPer ='" + DisabilityPer.Trim() + "', ";
                        str += "  Address1HouseNo =N'" + Address1HouseNo.Trim() + "', Address1StreetName =N'" + Address1StreetName.Trim() + "', Address1City_ID =" + Address1City_ID.Trim() + ", Address1TalukaID =" + Address1TalukaID.Trim() + ",  ";
                        str += " Address1VillageID =" + Address1VillageID.Trim() + ", Address1PinCode ='" + Address1PinCode.Trim() + "', IsBothAddressSame ='0' ";


                        if (PhysicallyHandicapDoc.Trim().Length > 0)
                        {
                            str += " , PhysicallyHandicapDoc ='" + PhysicallyHandicapDoc.Trim() + "' ";
                        }
                        if (CastCategoryDoc.Trim().Length > 0)
                        {
                            str += " , CastCategoryDoc ='" + CastCategoryDoc.Trim() + "' ";
                        }

                        if (LandStatus.Trim().ToUpper() == "NO")
                        {
                            str += " , Work_City_ID=" + Address1City_ID.Trim() + ", Work_TalukaID=" + Address1TalukaID.Trim() + ", Work_VillageID=" + Address1VillageID.Trim() + " ";
                            if (Work_ClustersMasterID.Trim().Length > 0)
                            {
                                str += " , Work_ClustersMasterID=" + Work_ClustersMasterID.Trim() + "";
                            }
                            if (Work_CircleID.Trim().Length > 0)
                            {
                                str += " , Work_CircleID=" + Work_CircleID.Trim() + "";
                            }
                        }

                        if (Address1Post_ID.Trim().Length > 0)
                        {
                            str += " , Address1Post_ID =" + Address1Post_ID.Trim() + " ";
                        }

                        str += " WHERE(RegistrationID = " + RegistrationID.Trim() + ")";
                        cla.ExecuteCommand(str, command);

                    }


                    transaction.Commit();
                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Sucess";
                    d.RegistrationID = RegistrationID.ToString();
                    d.Message = "Record Saved Sucessfully";
                    lst.Add(d);

                }
                catch (Exception ex)
                {
                    //String error = "Error in Add Journey Save button Click " + ex.ToString();
                    //WriteError(error, Session["UserEmailID"].ToString());
                    try
                    {
                        transaction.Rollback();

                    }
                    catch
                    {

                    }
                    clsReturnMessage d = new clsReturnMessage();
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



        [WebMethod]
        public void SaveLandStatus(String SecurityKey, String RegistrationID, String LandStatus, String FileLandLessCertificate, String RelatedToBeneficiaryCriteria, String BENEFICIARYCriteriaCertificates)
        {
            List<clsReturnMessage> lst = new List<clsReturnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            AzureBlobHelper fileRet = new AzureBlobHelper();
            if (MyCommanClassAPI.CheckApiAuthrization("48", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //------------validations ---------------------// 




            if (LandStatus.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill  LandStatus";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }


            if (LandStatus.Trim().ToUpper().Trim() == "NO")
            {
                if (cla.GetExecuteScalar("Select top 1 landID from Tbl_M_RegistrationLand where RegistrationID=" + RegistrationID + " and IsDeleted is null ").Length > 0)
                {
                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Warning";
                    d.Message = "Please remove your land to change land Status";
                    lst.Add(d);
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }

            }




            // String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "");
            String PathUp = "DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "";

            String LandLessCertificate = "";
            String OtherCriteriaCertificates = "";
            if (RegistrationID.ToString().Trim().Length > 0)
            {

                if (FileLandLessCertificate.Length > 0)
                {

                    LandLessCertificate = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "/LandLessCertificate" + MyCommanClassAPI.GetFileExtension(FileLandLessCertificate);
                    string imageName = "LandLessCertificate" + MyCommanClassAPI.GetFileExtension(FileLandLessCertificate);
                    //set the image path
                    //string imgPath = path + imageName;
                    //byte[] imageBytes = Convert.FromBase64String(FileLandLessCertificate);
                    //File.WriteAllBytes(imgPath, imageBytes);
                    byte[] imageBytes = Convert.FromBase64String(FileLandLessCertificate);
                    //fileRet.UploadData(PathUp, imageName, imageBytes);
                    String ret = fileRet.UploadData(PathUp, imageName, imageBytes);
                    if (ret.Trim().Length > 0)
                    {
                        clsReturnMessage d = new clsReturnMessage();
                        d.MessageType = "Warning";
                        d.Message = "Please upload Land Less Certificate";
                        lst.Add(d);
                        Context.Response.Clear();
                        Context.Response.ContentType = "application/json";
                        //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                        Context.Response.Flush();
                        Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                        return;
                    }
                }
                if (BENEFICIARYCriteriaCertificates.Length > 0)
                {
                    OtherCriteriaCertificates = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "/AnyOtherCertificate" + MyCommanClassAPI.GetFileExtension(BENEFICIARYCriteriaCertificates);
                    string imageName = "AnyOtherCertificate" + MyCommanClassAPI.GetFileExtension(BENEFICIARYCriteriaCertificates);
                    //set the image path
                    //string imgPath = path + imageName;
                    //byte[] imageBytes = Convert.FromBase64String(BENEFICIARYCriteriaCertificates);
                    //File.WriteAllBytes(imgPath, imageBytes);
                    byte[] imageBytes = Convert.FromBase64String(BENEFICIARYCriteriaCertificates);
                    //fileRet.UploadData(PathUp, imageName, imageBytes);
                    String ret = fileRet.UploadData(PathUp, imageName, imageBytes);
                    if (ret.Trim().Length > 0)
                    {
                        clsReturnMessage d = new clsReturnMessage();
                        d.MessageType = "Warning";
                        d.Message = "Please upload Any Other Certificate";
                        lst.Add(d);
                        Context.Response.Clear();
                        Context.Response.ContentType = "application/json";
                        //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                        Context.Response.Flush();
                        Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                        return;
                    }
                }
            }


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


                    if (RegistrationID.ToString().Trim().Length > 0)
                    {
                        if (LandLessCertificate.Length > 0)
                        {
                            String str = " UPDATE  Tbl_M_RegistrationDetails SET   LandStatus ='" + LandStatus.Trim().ToUpper() + "', LandLessCertificate='" + LandLessCertificate + "' ";
                            str += " WHERE(RegistrationID = " + RegistrationID.ToString().Trim() + ")";
                            cla.ExecuteCommand(str, command);
                        }
                        if (OtherCriteriaCertificates.Length > 0)
                        {
                            String str = " UPDATE  Tbl_M_RegistrationDetails SET  AnyOtherDocType ='" + RelatedToBeneficiaryCriteria.Trim().ToUpper() + "', AnyOtherDoc='" + OtherCriteriaCertificates + "' ";
                            str += " WHERE(RegistrationID = " + RegistrationID.ToString().Trim() + ")";
                            cla.ExecuteCommand(str, command);
                        }
                        else
                        {
                            String str = " UPDATE  Tbl_M_RegistrationDetails SET   LandStatus ='" + LandStatus.Trim().ToUpper() + "',   AnyOtherDocType ='" + RelatedToBeneficiaryCriteria.Trim().ToUpper() + "' ";
                            str += " WHERE(RegistrationID = " + RegistrationID.ToString().Trim() + ")";
                            cla.ExecuteCommand(str, command);
                        }

                        if (LandStatus.Trim().ToUpper().Trim() == "YES")
                        {
                            String str = " UPDATE  Tbl_M_RegistrationDetails SET   LandLessCertificate=NULL , LandStatus ='" + LandStatus.Trim().ToUpper() + "',   AnyOtherDocType ='" + RelatedToBeneficiaryCriteria.Trim().ToUpper() + "' ";
                            str += " WHERE(RegistrationID = " + RegistrationID.ToString().Trim() + ")";
                            cla.ExecuteCommand(str, command);
                        }


                    }


                    transaction.Commit();
                    MyCommanClass.UpdateLandStatusWiseWorkVillage(RegistrationID.ToString().Trim());
                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Sucess";
                    d.RegistrationID = RegistrationID.ToString();
                    d.Message = "Record Saved Sucessfully";
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

                    clsReturnMessage d = new clsReturnMessage();
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

        [WebMethod]
        public void Save8ADetail(String SecurityKey, String RegistrationID, String LandStatus, String LANDDISTRICT, String LANDTALUKA, String LANDVILLAGE, String SURVEYNo8A, String Hectare8A, String Are8A, String FileFORM8A)
        {
            List<clsReturnMessage> lst = new List<clsReturnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();

            if (MyCommanClassAPI.CheckApiAuthrization("44", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //------------validations ---------------------// 




            if (FileFORM8A.Trim().Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please attach Form 8A. file";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (SURVEYNo8A.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill  Number";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            String s = cla.GetExecuteScalar("SELECT top 1 LandID FROM Tbl_M_RegistrationLand where RegistrationID=" + RegistrationID.ToString().Trim() + " and City_ID=" + LANDDISTRICT.Trim() + " and TalukaID=" + LANDTALUKA.Trim() + "  and VillageID=" + LANDVILLAGE.Trim() + " and IsDeleted is null");
            if (s.Length > 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Only one 8-A is allowed per village.";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            String str = "";
            int LandID = cla.TableID("Tbl_M_RegistrationLand", "LandID");
            AzureBlobHelper fileRet = new AzureBlobHelper();
            String PathUp = "DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "";
            //String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + RegistrationID.Trim() + "");
            string filepath = "";
            if (RegistrationID.ToString().Trim().Length > 0)
            {
                if (FileFORM8A.Length > 0)
                {
                    string imageName = "FORM8A_" + LandID.ToString() + "" + MyCommanClassAPI.GetFileExtension(FileFORM8A);
                    filepath = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "/" + imageName;
                    //set the image path
                    //string imgPath = path + imageName;
                    //byte[] imageBytes = Convert.FromBase64String(FileFORM8A);
                    //File.WriteAllBytes(imgPath, imageBytes);

                    byte[] imageBytes = Convert.FromBase64String(FileFORM8A);
                    // fileRet.UploadData(PathUp, imageName, imageBytes);
                    String ret = fileRet.UploadData(PathUp, imageName, imageBytes);
                    if (ret.Trim().Length > 0)
                    {
                        clsReturnMessage d = new clsReturnMessage();
                        d.MessageType = "Warning";
                        d.Message = "Please upload FORM8A";
                        lst.Add(d);
                        Context.Response.Clear();
                        Context.Response.ContentType = "application/json";
                        //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                        Context.Response.Flush();
                        Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                        return;
                    }
                }
            }

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
                    if (RegistrationID.ToString().Trim().Length > 0)
                    {
                        if (FileFORM8A.Length > 0)
                        {
                            str = " INSERT INTO Tbl_M_RegistrationLand (LandID, RegistrationID, City_ID, TalukaID, VillageID, AccountNumber8A, Hectare8A, Are8A, Form8ADoc)";
                            str += " VALUES(" + LandID + "," + RegistrationID.ToString().Trim() + "," + LANDDISTRICT.Trim() + "," + LANDTALUKA.Trim() + "," + LANDVILLAGE.Trim() + ",'" + SURVEYNo8A.Trim() + "'," + Hectare8A.Trim() + "," + Are8A.Trim() + ",'" + filepath + "')";
                            cla.ExecuteCommand(str, command);


                            str = " UPDATE  Tbl_M_RegistrationDetails SET   LandStatus ='" + LandStatus.Trim() + "' ";
                            str += " WHERE(RegistrationID = " + RegistrationID.Trim() + ")";
                            cla.ExecuteCommand(str, command);

                        }
                    }


                    transaction.Commit();
                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Sucess";
                    d.RegistrationID = RegistrationID.ToString();
                    d.Message = "Record Saved Sucessfully";
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

                    clsReturnMessage d = new clsReturnMessage();
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

        [WebMethod]
        public void Get8A(String SecurityKey, String RegistrationID, String Lang)
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();

            if (MyCommanClassAPI.CheckApiAuthrization("51", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                dt = Comcls.GetForm8A(RegistrationID, Lang);
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = dt.Rows[x]["LandID"].ToString();
                    d.Value = dt.Rows[x]["Names"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }

        [WebMethod]
        public void Save712Detail(String SecurityKey, String RegistrationID, String SURVEYNo8A, String SurveyNo712, String Hectare712, String Are712, String File712)
        {
            List<clsReturnMessage> lst = new List<clsReturnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();

            if (MyCommanClassAPI.CheckApiAuthrization("45", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //------------validations ---------------------// 




            if (File712.Trim().Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please attach Form 7-12. file";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (SURVEYNo8A.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select 8A Number";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }



            String str = "";
            int LandID = cla.TableID("Tbl_M_RegistrationLand", "LandID");

            //String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + RegistrationID.Trim() + "");
            AzureBlobHelper fileRet = new AzureBlobHelper();
            String PathUp = "DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "";

            string filepath = "";
            if (RegistrationID.ToString().Trim().Length > 0)
            {
                if (File712.Length > 0)
                {
                    string imageName = "SURVEYNUMBER712_" + LandID.ToString() + "" + MyCommanClassAPI.GetFileExtension(File712);
                    filepath = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "/" + imageName;
                    //set the image path
                    //string imgPath = path + imageName;
                    //byte[] imageBytes = Convert.FromBase64String(File712);
                    //File.WriteAllBytes(imgPath, imageBytes);     
                    byte[] imageBytes = Convert.FromBase64String(File712);
                    // fileRet.UploadData(PathUp, imageName, imageBytes);
                    String ret = fileRet.UploadData(PathUp, imageName, imageBytes);
                    if (ret.Trim().Length > 0)
                    {
                        clsReturnMessage d = new clsReturnMessage();
                        d.MessageType = "Warning";
                        d.Message = "Please upload 712";
                        lst.Add(d);
                        Context.Response.Clear();
                        Context.Response.ContentType = "application/json";
                        //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                        Context.Response.Flush();
                        Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                        return;
                    }
                }
            }

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

                    if (RegistrationID.ToString().Trim().Length > 0)
                    {
                        if (File712.Length > 0)
                        {
                            str = " INSERT INTO Tbl_M_RegistrationLand ( LandID, ParentLandID , SurveyNo712, Hectare712, Are712, Extracts712Doc,RegistrationID)";
                            str += " VALUES(" + LandID + "," + SURVEYNo8A.Trim() + ",'" + SurveyNo712.Trim() + "','" + Hectare712.Trim() + "','" + Are712.Trim() + "','" + filepath + "'," + RegistrationID.ToString().Trim() + ")";
                            cla.ExecuteCommand(str, command);
                        }
                    }


                    transaction.Commit();
                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Sucess";
                    d.RegistrationID = RegistrationID.ToString();
                    d.Message = "Record Saved Sucessfully";
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

                    clsReturnMessage d = new clsReturnMessage();
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


        [WebMethod]
        public void GetLandDetails(String SecurityKey, String RegistrationID)
        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<cls8A> lst = new List<cls8A>();
            MyCommanClass.UpdateLandStatusWiseWorkVillage(RegistrationID.ToString().Trim());
            if (MyCommanClassAPI.CheckApiAuthrization("49", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                cls8A d = new cls8A();
                d.Message = "Authorization Failed";


                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(d, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(d, Formatting.Indented).ToString());
                return;
            }
            try
            {
                String UpDateLink = "";

                String s = cla.GetExecuteScalar("( (Select top 1 RegistrationID from Tbl_T_ApplicationDetails AP where AP.RegistrationID=" + RegistrationID.Trim() + " and IsDeleted is null and AP.ApplicationStatusID in (4,10,20) union all Select top 1 RegistrationID from Tbl_M_RegistrationDetails AP where AP.RegistrationID=" + RegistrationID.Trim() + " and IsDeleted is null and AP.ApprovalStatus='Back To Beneficiary') union all Select top 1 WR.WorkReportID as RegistrationID from Tbl_T_Application_WorkReport WR where WR.IsDeleted is null and WR.ApplicationStatusID in (4, 10, 20) and WR.ApplicationID in (Select ApplicationID from Tbl_T_ApplicationDetails where Tbl_T_ApplicationDetails.RegistrationID =" + RegistrationID.Trim() + " and Tbl_T_ApplicationDetails.IsDeleted is null))");



                List<String> lsts = new List<string>();
                lsts.Add(RegistrationID.Trim());
                dt = cla.GetDtByProcedure("SP_GetLandParentDetails", lsts);

                for (int x = 0; x != dt.Rows.Count; x++)
                {


                    cls8A d = new cls8A();

                    d.LANDDISTRICT = dt.Rows[x]["Cityname"].ToString();
                    d.City_ID = dt.Rows[x]["City_ID"].ToString();
                    d.LANDTALUKA = dt.Rows[x]["TALUKA"].ToString();
                    d.TalukaID = dt.Rows[x]["TalukaID"].ToString();
                    d.LANDVILLAGE = dt.Rows[x]["VillageName"].ToString();
                    d.VillageID = dt.Rows[x]["VillageID"].ToString();
                    d.LandID = dt.Rows[x]["LandID"].ToString();
                    d.SURVEYNo8A = dt.Rows[x]["AccountNumber8A"].ToString();
                    d.Hectare8A = dt.Rows[x]["Hectare8A"].ToString();
                    d.Are8A = dt.Rows[x]["Are8A"].ToString();
                    d.FileFORM8A = dt.Rows[x]["Form8ADoc2"].ToString();
                    d.Message = "";
                    if (s.Length > 0)
                    {
                        String a = EncryptDecryptQueryString.encrypt(dt.Rows[x]["LandID"].ToString().Trim());
                        String b = EncryptDecryptQueryString.encrypt(RegistrationID);
                        UpDateLink = "https://dbt-api.mahapocra.gov.in/app/UsersTrans/UpdateLandCerti.aspx?I=" + a + "&R=" + b + "&A=S";
                    }
                    d.UpDateLink = UpDateLink;

                    List<cls712> l = new List<cls712>();
                    String ParentLandID = dt.Rows[x]["LandID"].ToString();

                    DataTable dt2 = cla.GetDataTable("SELECT  LandID, SurveyNo712, Hectare712, Are712,  Extracts712Doc as Extracts712Doc FROM  Tbl_M_RegistrationLand WHERE  (ParentLandID = " + ParentLandID + ") AND (IsDeleted IS NULL) AND RegistrationID=" + RegistrationID.Trim() + " ");
                    for (int y = 0; y != dt2.Rows.Count; y++)
                    {
                        cls712 c = new cls712();
                        c.SURVEYNo8A = dt.Rows[x]["AccountNumber8A"].ToString();
                        c.SurveyNo712 = dt2.Rows[y]["SurveyNo712"].ToString();
                        c.Hectare712 = dt2.Rows[y]["Hectare712"].ToString();
                        c.Are712 = dt2.Rows[y]["Are712"].ToString();
                        c.File712 = dt2.Rows[y]["Extracts712Doc"].ToString();
                        c.LandID = dt2.Rows[y]["LandID"].ToString();
                        if (s.Length > 0)
                        {
                            String a = EncryptDecryptQueryString.encrypt(c.LandID);
                            String b = EncryptDecryptQueryString.encrypt(RegistrationID);
                            UpDateLink = "https://dbt-api.mahapocra.gov.in/app/UsersTrans/UpdateLandCerti.aspx?I=" + a + "&R=" + b + "&A=S";
                        }
                        c.UpDateLink = UpDateLink;
                        l.Add(c);
                    }
                    d.cls712 = l;
                    lst.Add(d);

                }


            }
            catch (Exception ex)
            {
                cls8A d = new cls8A();
                d.Message = ex.ToString();
                lst.Add(d);


            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }


        [WebMethod]
        public void SaveDeclretion(String SecurityKey, String RegistrationID, String AADHARNo, String Name)
        {
            List<clsReturnMessage> lst = new List<clsReturnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();

            if (MyCommanClassAPI.CheckApiAuthrization("46", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //------------validations ---------------------// 






            int UserId = cla.TableID("Tbl_M_LoginDetails", "UserId");
            String LandStatus = cla.GetExecuteScalar("SELECT   LandStatus FROM Tbl_M_RegistrationDetails where RegistrationID=" + RegistrationID.Trim() + "");
            DataTable dtLand = cla.GetDataTable("SELECT  TOP (1) L.VillageID, V.City_ID, V.ClustersMasterID, V.TalukaID, V.CircleID FROM Tbl_M_RegistrationLand AS L INNER JOIN Tbl_M_VillageMaster AS V ON V.VillageID = L.VillageID WHERE (L.RegistrationID = " + RegistrationID.Trim() + ") AND (L.ParentLandID IS NULL) ORDER BY L.LandID");

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


                    if (RegistrationID.ToString().Trim().Length > 0)
                    {





                        if (RegistrationID.Trim().Length > 0)
                        {
                            // UPDATE
                            String str = " UPDATE  Tbl_M_RegistrationDetails SET   IAgree ='1' , IsDeleted=NULL ,UserID='" + AADHARNo.Trim() + "' , RegistrationDate='" + cla.mdy(cla.SvrDate()) + "' ";
                            str += " WHERE(RegistrationID = " + RegistrationID.Trim() + ")";
                            cla.ExecuteCommand(str, command);


                            str = " INSERT INTO Tbl_M_LoginDetails (UserId, RegistrationID, UserName, UPass, LoginAs, FullName)";
                            str += " VALUES(" + UserId + "," + RegistrationID.ToString().Trim() + ",'" + AADHARNo.Trim() + "','" + AADHARNo.Trim() + "','Beneficiary','" + Name.Trim() + "')";
                            cla.ExecuteCommand(str, command);
                        }


                        if (LandStatus.Trim().ToUpper() == "YES")
                        {
                            if (dtLand.Rows.Count > 0)
                            {
                                for (int x = 0; x != dtLand.Rows.Count; x++)
                                {
                                    String str = " UPDATE  Tbl_M_RegistrationDetails SET Work_City_ID=" + dtLand.Rows[x]["City_ID"].ToString() + ", Work_TalukaID=" + dtLand.Rows[x]["TalukaID"].ToString() + ", Work_VillageID=" + dtLand.Rows[x]["VillageID"].ToString() + " ";
                                    if (dtLand.Rows[x]["ClustersMasterID"].ToString().Trim().Length > 0)
                                    {
                                        str += " , Work_ClustersMasterID=" + dtLand.Rows[x]["ClustersMasterID"].ToString() + "";
                                    }
                                    if (dtLand.Rows[x]["CircleID"].ToString().Trim().Length > 0)
                                    {
                                        str += " , Work_CircleID=" + dtLand.Rows[x]["CircleID"].ToString().Trim() + "";
                                    }
                                    str += " WHERE (RegistrationID = " + RegistrationID.ToString().Trim() + ")";
                                    cla.ExecuteCommand(str, command);
                                }
                            }

                        }





                    }







                    transaction.Commit();
                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Sucess";
                    d.RegistrationID = RegistrationID.ToString();
                    d.Message = "You have been registered under Nanaji Deshmukh Krishi Sanjivani Prakalp. Your login ID will be your AADHAAR Number";
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

                    clsReturnMessage d = new clsReturnMessage();
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




        #region "FPO/FPC"

        [WebMethod]
        public void OtherRegistrationDetails(String SecurityKey, String PoRegistrationID, String RegisterAs, String Name, String RegistrationNo, String PORegistrationDate, String RegistrationCertificate, String RegistrationUnder, String RegistrationThrough, String PramoterName, String Gender, String MobileNo, String LandlineNo, String Email, String CeoName, String CeoGender, String CeoMobileNo, String CeoLandlineNo, String CeoEmail, String AuthorisedPersonName, String Disignation, String ProofAuthorisation, String AuthorizedGender, String AuthorisedMobileNo, String AuthorisedEmailId)
        {

            List<clsReturnMessage> lst = new List<clsReturnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            AzureBlobHelper fileRet = new AzureBlobHelper();
            //String RegistrationDate = cla.SvrDate();
            if (MyCommanClassAPI.CheckApiAuthrization("36", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 
            if (Comcls.RecordExistanceChk("Tbl_M_RegistrationDetails", "FPORegistrationNo", RegistrationNo.Trim(), "RegistrationID") == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "FPO Registration number " + RegistrationNo.ToUpper() + " is already registered";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //  String BeneficiaryTypesID = "3";

            if (RegisterAs.Trim().Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select Register As";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (Name.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Name";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (RegistrationNo.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Registration Number";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (PORegistrationDate.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill FPO Registration Date";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (RegistrationCertificate.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please attach Registration Certificate";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (RegistrationUnder.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select Registration Under";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (RegistrationThrough.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select Registration Through";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (PramoterName.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Name";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (Gender.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select Gender";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (MobileNo.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Mobile Number";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }


            //--------Ceo----//
            else if (CeoName.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Name";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (CeoGender.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select Ceo Gender";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (CeoMobileNo.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Ceo Mobile Number";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            else if (AuthorisedPersonName.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Authorised Person Name";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (Disignation.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Disignation";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (ProofAuthorisation.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please attach Proof Of Authorisation";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (AuthorizedGender.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select Authorized Gender";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (AuthorisedMobileNo.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Authorised Mobile Number";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (AuthorisedEmailId.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Authorised Email Id";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //------------validations ---------------------// 


            String str = "";
            int RegistrationID = cla.TableID("Tbl_M_RegistrationDetails", "RegistrationID");

            String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString() + "");
            if (!Directory.Exists(path))
            {
                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
            }

            //   String Uppath = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "");

            //String CastCategoryDoc = "";
            //if (RegistrationCertificate.Length > 0)
            //{

            //    CastCategoryDoc = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "/RegistrationCertifecate" + MyCommanClassAPI.GetFileExtension(RegistrationCertificate);
            //    string imageName = "/RegistrationCertifecate" + MyCommanClassAPI.GetFileExtension(RegistrationCertificate);
            //    //set the image path
            //    string imgPath = path + imageName;
            //    byte[] imageBytes = Convert.FromBase64String(RegistrationCertificate);
            //    File.WriteAllBytes(imgPath, imageBytes);

            //}

            //ProofAuthorisation = "";
            //if (ProofAuthorisation.Length > 0)
            //{
            //    ProofAuthorisation = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "/ProofAuthorisation" + MyCommanClassAPI.GetFileExtension(ProofAuthorisation);
            //    string imageName = "/ProofAuthorisation" + MyCommanClassAPI.GetFileExtension(ProofAuthorisation);
            //    //set the image path
            //    string imgPath = path + imageName;
            //    byte[] imageBytes = Convert.FromBase64String(ProofAuthorisation);
            //    File.WriteAllBytes(imgPath, imageBytes);

            //}
            //

            String PathUp = "DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "";

            String Certificate = "";
            String AuthorisationCertificates = "";
            //if (RegistrationID.ToString().Trim().Length > 0)
            //{

            if (RegistrationCertificate.Length > 0)
            {

                Certificate = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "/RegistrationCertifecate" + MyCommanClassAPI.GetFileExtension(RegistrationCertificate);
                string imageName = "RegistrationCertifecate" + MyCommanClassAPI.GetFileExtension(RegistrationCertificate);
                //set the image path
                //string imgPath = path + imageName;
                //byte[] imageBytes = Convert.FromBase64String(FileLandLessCertificate);
                //File.WriteAllBytes(imgPath, imageBytes);
                byte[] imageBytes = Convert.FromBase64String(RegistrationCertificate);
                fileRet.UploadData(PathUp, imageName, imageBytes);
            }
            if (ProofAuthorisation.Length > 0)
            {
                AuthorisationCertificates = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "/AuthorisationCertificates" + MyCommanClassAPI.GetFileExtension(ProofAuthorisation);
                string imageName = "AuthorisationCertificates" + MyCommanClassAPI.GetFileExtension(ProofAuthorisation);
                //set the image path
                //string imgPath = path + imageName;
                //byte[] imageBytes = Convert.FromBase64String(BENEFICIARYCriteriaCertificates);
                //File.WriteAllBytes(imgPath, imageBytes);
                byte[] imageBytes = Convert.FromBase64String(ProofAuthorisation);
                fileRet.UploadData(PathUp, imageName, imageBytes);
            }


            //
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


                    if (PoRegistrationID.Trim().Length == 0)
                    {
                        // ADD NEW                          
                        str = " INSERT INTO Tbl_M_RegistrationDetails";
                        str += "  (RegistrationID, BeneficiaryTypesID, RegisterName, FPORegistrationNo, FPORegistrationDate, RegisterUnderID, RegisteredThroughID, PromoterName, Promotermobile,";
                        str += "  Promoterlandline, PromoterEmail, CeoName, CeoMobile, CeoLandline, CeoEmail, CeoDesignation, CeoAuthorisedPerson,PromoterGender,CeoGender,CeoAuthorisedPersonGen,CeoAuthorisedPersonMob,CeoAuthorisedPersonEmail,IsDeleted)";
                        str += " VALUES(" + RegistrationID + "," + RegisterAs.Trim().ToUpper() + ",N'" + Name.Trim() + "','" + RegistrationNo.Trim() + "','" + cla.mdy(PORegistrationDate.Trim()).Trim() + "',";
                        str += " " + RegistrationUnder.Trim() + "," + RegistrationThrough.Trim() + ",'" + PramoterName.Trim() + "','" + MobileNo.Trim() + "','" + LandlineNo.Trim() + "','" + Email.Trim() + "',";
                        str += " '" + CeoName.Trim() + "','" + CeoMobileNo.Trim() + "','" + CeoLandlineNo.Trim() + "','" + CeoEmail.Trim() + "','" + Disignation.Trim() + "','" + AuthorisedPersonName.Trim() + "','" + Gender.Trim() + "','" + CeoGender.Trim() + "','" + AuthorizedGender.Trim() + "','" + AuthorisedMobileNo.Trim() + "',N'" + AuthorisedEmailId.Trim() + "','1')  ";
                        cla.ExecuteCommand(str, command);

                        cla.ExecuteCommand("UPDATE Tbl_M_RegistrationDetails SET RegistrationCertifecate = '" + Certificate.Trim() + "', CeoProofOfAuthorisation ='" + AuthorisationCertificates.Trim() + "' WHERE  (RegistrationID = " + RegistrationID + ")", command);//CastCategoryDoc..ProofAuthorisation
                    }
                    else
                    {
                        // EDIT 

                        if (PoRegistrationID.ToString().Trim().Length > 0)
                        {
                            // ADD NEW 
                            //String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + ViewState["RegistrationID"].ToString() + "");                            
                            str = " UPDATE Tbl_M_RegistrationDetails SET ";
                            str += " RegisterUnderID=" + RegistrationUnder.Trim() + ", RegisteredThroughID=" + RegistrationThrough.Trim() + ", PromoterName='" + PramoterName.Trim() + "', Promotermobile='" + MobileNo.Trim() + "', Promoterlandline='" + LandlineNo.Trim() + "', ";
                            str += " CeoName='" + CeoName.Trim() + "',CeoMobile='" + CeoMobileNo.Trim() + "',CeoLandline='" + CeoLandlineNo.Trim() + "',CeoEmail='" + CeoEmail.Trim() + "',CeoDesignation='" + Disignation.Trim() + "',CeoAuthorisedPerson='" + AuthorisedPersonName.Trim() + "',PromoterGender='" + Gender.Trim() + "',CeoGender='" + CeoGender.Trim() + "',CeoAuthorisedPersonGen='" + AuthorizedGender.Trim() + "',CeoAuthorisedPersonMob ='" + AuthorisedMobileNo.Trim() + "', CeoAuthorisedPersonEmail='" + AuthorisedEmailId.Trim() + "' ";
                            str += " WHERE(RegistrationID = " + PoRegistrationID.ToString().Trim() + ")";
                            cla.ExecuteCommand(str, command);

                            //
                            cla.ExecuteCommand("UPDATE Tbl_M_RegistrationDetails SET RegistrationCertifecate = '" + Certificate.Trim() + "', CeoProofOfAuthorisation ='" + AuthorisationCertificates.Trim() + "' WHERE  (RegistrationID = " + RegistrationID + ")", command);//RegistrationCertificate..ProofAuthorisation



                        }


                    }

                    transaction.Commit();
                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Sucess";
                    d.RegistrationID = RegistrationID.ToString();
                    d.Message = "Record Saved Sucessfully";
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

                    clsReturnMessage d = new clsReturnMessage();
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

        [WebMethod]

        public void OtherRegisteredAddress(String SecurityKey, String PoRegistrationID, String OfficeNo, String StreetNo, String District, String Taluka, String Post, String PinCode, String Village, String CLUSTERCODE, String Mobile1, String Mobile2, String LandlineNo, String EmailId, String PanNo)
        {

            List<clsReturnMessage> lst = new List<clsReturnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            String RegistrationDate = cla.SvrDate();
            if (MyCommanClassAPI.CheckApiAuthrization("37", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 

            if (OfficeNo.Trim().Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Office Number";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (District.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select District";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (Taluka.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select Taluka";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (Post.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select Post";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (PinCode.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Pin Code";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (Village.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select Village";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (CLUSTERCODE.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Cluster Code";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (Mobile1.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Mobile Number";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            String str = "";

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


                    if (PoRegistrationID.Trim().Length > 0)
                    {


                        // UPDATE
                        str = " UPDATE  Tbl_M_RegistrationDetails SET  MobileNumber ='" + Mobile1.Trim() + "', ";
                        str += " MobileNumber2 ='" + Mobile2.Trim() + "', LandLineNumber ='" + LandlineNo.Trim() + "', EmailID ='" + EmailId.Trim() + "', PanNumber ='" + PanNo.Trim() + "',  ";
                        str += " Address1HouseNo =N'" + OfficeNo.Trim() + "', Address1StreetName =N'" + StreetNo.Trim() + "', Address1City_ID =" + District.Trim() + ", Address1TalukaID =" + Taluka.Trim() + ", Address1Post_ID =" + Post.Trim() + ", ";
                        str += " Address1VillageID =" + Village.Trim() + ", Address1PinCode ='" + PinCode.Trim() + "' ";
                        str += " WHERE(RegistrationID = " + PoRegistrationID.Trim() + ")";

                        cla.ExecuteCommand(str, command);

                    }


                    transaction.Commit();
                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Sucess";
                    d.RegistrationID = PoRegistrationID.ToString();
                    d.Message = "Record Saved Sucessfully";
                    lst.Add(d);

                }
                catch (Exception ex)
                {
                    //String error = "Error in Add Journey Save button Click " + ex.ToString();
                    //WriteError(error, Session["UserEmailID"].ToString());
                    try
                    {
                        transaction.Rollback();

                    }
                    catch
                    {

                    }
                    clsReturnMessage d = new clsReturnMessage();
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

        [WebMethod]
        public void OtherCorrespondenceAddress(String SecurityKey, String PoRegistrationID, String SameAsAddress, String CorresOfficeNo, String CorresStreetNo, String CorresDistrict, String CorresTaluka, String CorresPost, String CorresPinCode, String CorresVillage, String CorresCLUSTERCODE, String CorresMobile1, String CorresMobile2, String CorresLandlineNo)
        {

            List<clsReturnMessage> lst = new List<clsReturnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            String RegistrationDate = cla.SvrDate();
            if (MyCommanClassAPI.CheckApiAuthrization("38", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            if (CorresOfficeNo.Trim().Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Office Number";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (CorresDistrict.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select District";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (CorresTaluka.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select Taluka";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (CorresPost.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select Post";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (CorresPinCode.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Pin Code";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (CorresVillage.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select Village";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (CorresCLUSTERCODE.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Cluster Code";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (CorresMobile1.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Mobile Number";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //String IsBothAddressSame = "False";

            //if(Convert.ToBoolean(SameAsAddress.Trim()))
            //{
            //    IsBothAddressSame = "True";
            //}



            //------------End validations ---------------------// 


            String str = "";

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


                    if (PoRegistrationID.ToString().Trim().Length > 0)
                    {


                        // UPDATE
                        str = " UPDATE  Tbl_M_RegistrationDetails SET  Address2Mob1 ='" + CorresMobile1.Trim() + "', ";
                        str += " Address2Mob2 ='" + CorresMobile2.Trim() + "', Address2LandLine ='" + CorresLandlineNo.Trim() + "',   ";
                        str += " Address2HouseNo =N'" + CorresOfficeNo.Trim() + "', Address2StreetName =N'" + CorresStreetNo.Trim() + "', Address2City_ID =" + CorresDistrict.Trim() + ", Address2TalukaID =" + CorresTaluka.Trim() + ", Address2Post_ID =" + CorresPost.Trim() + ", ";
                        str += " Address2VillageID =" + CorresVillage.Trim() + ", Address2PinCode ='" + CorresPinCode.Trim() + "', IsBothAddressSame ='" + SameAsAddress.Trim() + "' ";
                        str += " WHERE(RegistrationID = " + PoRegistrationID.ToString().Trim() + ")";
                        cla.ExecuteCommand(str, command);
                    }
                    transaction.Commit();
                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Sucess";
                    d.RegistrationID = PoRegistrationID.ToString();
                    d.Message = "Record Saved Sucessfully";
                    lst.Add(d);

                }
                catch (Exception ex)
                {
                    //String error = "Error in Add Journey Save button Click " + ex.ToString();
                    //WriteError(error, Session["UserEmailID"].ToString());
                    try
                    {
                        transaction.Rollback();

                    }
                    catch
                    {

                    }
                    clsReturnMessage d = new clsReturnMessage();
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



        [WebMethod]
        public void OtherDeclaration(String SecurityKey, String PoRegistrationID, String RegisterAs, String RegistrationNo, String Name)
        {
            List<clsReturnMessage> lst = new List<clsReturnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();

            if (MyCommanClassAPI.CheckApiAuthrization("39", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //------------validations ---------------------// 


            String str = "";


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


                    if (PoRegistrationID.ToString().Trim().Length > 0)
                    {
                        //UPDATE
                        str = " UPDATE  Tbl_M_RegistrationDetails SET   IAgree ='1' , IsDeleted=NULL , RegistrationDate='" + cla.mdy(cla.SvrDate()) + "'  ";
                        str += " WHERE(RegistrationID = " + PoRegistrationID.ToString().Trim() + ")";
                        cla.ExecuteCommand(str, command);

                    }

                    transaction.Commit();
                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Sucess";
                    d.RegistrationID = PoRegistrationID.ToString();
                    d.Message = "You have been registered under Nanaji Deshmukh Krishi Sanjivani Prakalp. Your login ID will be your Registration Number";
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

                    clsReturnMessage d = new clsReturnMessage();
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



        #region "Community"



        [WebMethod]
        public void CommunityRegistrationDetails(String SecurityKey, String ComRegistrationID, String GramPanchayatCode, String AuthorisedPersonName, String Gender, String AuthorisedMobileNo, String AuthorisedEmailId)
        {

            List<clsReturnMessage> lst = new List<clsReturnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            // String RegistrationDate = cla.SvrDate();
            if (MyCommanClassAPI.CheckApiAuthrization("31", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 
            if (Comcls.RecordExistanceChk("Tbl_M_RegistrationDetails", "GramPanchayatCode", GramPanchayatCode.Trim(), "RegistrationID") == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Gram Panchayat Code " + GramPanchayatCode.ToUpper() + " is already registered";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            String BeneficiaryTypesID = "2";

            if (GramPanchayatCode.Trim().Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select Gram Panchayat Code";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (AuthorisedPersonName.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Name";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (AuthorisedMobileNo.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Authorised Mobile Number";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (AuthorisedEmailId.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Authorised Email Id";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //----------validation------------//
            String str = "";
            int RegistrationID = cla.TableID("Tbl_M_RegistrationDetails", "RegistrationID");

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


                    if (ComRegistrationID.ToString().Trim().Length == 0)
                    {
                        // ADD NEW 
                        str = " INSERT INTO Tbl_M_RegistrationDetails (RegistrationID, BeneficiaryTypesID, GramPanchayatCode, VillageID , RegisterName, Gender , IsDeleted, GramPanchayatMobile , GramPanchayatEmail)";
                        str += " VALUES(" + RegistrationID + "," + BeneficiaryTypesID + ",'" + GramPanchayatCode.Trim().ToUpper() + "'," + GramPanchayatCode.Trim() + ",'" + AuthorisedPersonName.Trim() + "','" + Gender.Trim() + "','1','" + AuthorisedMobileNo.Trim() + "','" + AuthorisedEmailId.Trim() + "' )";
                        cla.ExecuteCommand(str, command);

                        //String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString() + "");
                        //if (!Directory.Exists(path))
                        //{
                        //    // Try to create the directory.
                        //    DirectoryInfo di = Directory.CreateDirectory(path);
                        //}
                    }
                    else
                    {
                        // EDIT 
                        if (ComRegistrationID.ToString().Trim().Length > 0)
                        {
                            //UPDATE
                            str = " UPDATE  Tbl_M_RegistrationDetails SET";
                            str += " RegisterName=N'" + AuthorisedPersonName.Trim() + "',Gender='" + Gender.Trim() + "',GramPanchayatMobile='" + AuthorisedMobileNo.Trim() + "', GramPanchayatEmail=N'" + AuthorisedEmailId.Trim() + "' ";
                            str += " WHERE (RegistrationID = " + ComRegistrationID.ToString().Trim() + ")";
                            cla.ExecuteCommand(str, command);

                        }

                    }


                    transaction.Commit();
                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Sucess";
                    d.RegistrationID = RegistrationID.ToString();
                    d.Message = "Record Saved Sucessfully";
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

                    clsReturnMessage d = new clsReturnMessage();
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

        [WebMethod]
        public void CommunityRegisteredAddress(String SecurityKey, String ComRegistrationID, String GramPanchayatAddress, String StreetNo, String District, String Taluka, String Post, String PinCode, String Village, String CLUSTERCODE, String Mobile1, String Mobile2, String LandlineNo, String EmailId)
        {

            List<clsReturnMessage> lst = new List<clsReturnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            String RegistrationDate = cla.SvrDate();
            if (MyCommanClassAPI.CheckApiAuthrization("32", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 

            if (GramPanchayatAddress.Trim().Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Gram Panchayat Address";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (District.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select District";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (Taluka.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select Taluka";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (Post.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select Post";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (PinCode.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Pin Code";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (Village.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select Village";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (CLUSTERCODE.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Cluster Code";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (Mobile1.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Mobile Number";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            String str = "";

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


                    if (ComRegistrationID.ToString().Trim().Length > 0)
                    {


                        // UPDATE
                        str = " UPDATE  Tbl_M_RegistrationDetails SET  MobileNumber ='" + Mobile1.Trim() + "', ";
                        str += " MobileNumber2 ='" + Mobile2.Trim() + "', LandLineNumber ='" + LandlineNo.Trim() + "', EmailID ='" + EmailId.Trim() + "',  ";
                        str += " Address1HouseNo =N'" + GramPanchayatAddress.Trim() + "', Address1StreetName =N'" + StreetNo.Trim() + "', Address1City_ID =" + District.Trim() + ", Address1TalukaID =" + Taluka.Trim() + ", Address1Post_ID =" + Post.Trim() + ", ";
                        str += " Address1VillageID =" + Village.Trim() + ", Address1PinCode ='" + PinCode.Trim() + "', IsBothAddressSame ='0' ";
                        str += " WHERE(RegistrationID = " + ComRegistrationID.ToString().Trim() + ")";
                        cla.ExecuteCommand(str, command);

                    }


                    transaction.Commit();
                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Sucess";
                    d.RegistrationID = ComRegistrationID.ToString();
                    d.Message = "Record Saved Sucessfully";
                    lst.Add(d);

                }
                catch (Exception ex)
                {
                    //String error = "Error in Add Journey Save button Click " + ex.ToString();
                    //WriteError(error, Session["UserEmailID"].ToString());
                    try
                    {
                        transaction.Rollback();

                    }
                    catch
                    {

                    }
                    clsReturnMessage d = new clsReturnMessage();
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

        [WebMethod]
        public void CommunityVCRMCBankDetails(String SecurityKey, String ComRegistrationID, String BankAccoNo, String AccoHolderName, String RBIBankID)
        {

            List<clsReturnMessage> lst = new List<clsReturnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            String RegistrationDate = cla.SvrDate();
            if (MyCommanClassAPI.CheckApiAuthrization("33", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 

            if (BankAccoNo.Trim().Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Bank Account Number";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (AccoHolderName.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Account Holder Name";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            else if (RBIBankID.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select RBI Bank ID";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //-------------validation--------//
            String str = "";

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


                    if (ComRegistrationID.ToString().Trim().Length > 0)
                    {


                        // UPDATE
                        str = " UPDATE  Tbl_M_RegistrationDetails SET  BankAccountNo ='" + BankAccoNo.Trim() + "', BankAccountHolder ='" + AccoHolderName.Trim() + "', RBIBankID =" + RBIBankID + "  ";
                        str += " WHERE (RegistrationID = " + ComRegistrationID.ToString().Trim() + ")";
                        cla.ExecuteCommand(str, command);

                    }


                    transaction.Commit();
                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Sucess";
                    d.RegistrationID = ComRegistrationID.ToString();
                    d.Message = "Record Saved Sucessfully";
                    lst.Add(d);

                }
                catch (Exception ex)
                {
                    //String error = "Error in Add Journey Save button Click " + ex.ToString();
                    //WriteError(error, Session["UserEmailID"].ToString());
                    try
                    {
                        transaction.Rollback();

                    }
                    catch
                    {

                    }
                    clsReturnMessage d = new clsReturnMessage();
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

        [WebMethod]
        public void CommunityDeclaration(String SecurityKey, String ComRegistrationID, String GramPanchayatCode)
        {
            List<clsReturnMessage> lst = new List<clsReturnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();

            if (MyCommanClassAPI.CheckApiAuthrization("34", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            if (GramPanchayatCode.Trim().Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select Gram Panchayat Code";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }




            //------------validations ---------------------// 

            String str = "";
            int UserId = cla.TableID("Tbl_M_LoginDetails", "UserId");

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


                    if (ComRegistrationID.ToString().Trim().Length > 0)
                    {
                        // UPDATE
                        str = " UPDATE  Tbl_M_RegistrationDetails SET   IAgree ='1' , IsDeleted=NULL ,UserID='" + GramPanchayatCode.Trim() + "' , RegistrationDate='" + cla.mdy(cla.SvrDate()) + "' ";
                        str += " WHERE(RegistrationID = " + ComRegistrationID.ToString().Trim() + ")";
                        cla.ExecuteCommand(str, command);


                    }

                    transaction.Commit();
                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Sucess";
                    d.RegistrationID = ComRegistrationID.ToString();
                    d.Message = "You have been registered under Nanaji Deshmukh Krishi Sanjivani Prakalp. Your login ID will be your Gram Panchayat Code";
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

                    clsReturnMessage d = new clsReturnMessage();
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


        #region "Individual Registration"
        [WebMethod]


        public void GetRegistrationData(String SecurityKey, String RegistrationID)

        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<RegistrationData> lst = new List<RegistrationData>();

            if (MyCommanClassAPI.CheckApiAuthrization("47", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                Registration d = new Registration();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            try
            {


                List<String> frs = new List<String>();
                frs.Add(RegistrationID.Trim());
                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_Farmer_IndividualDetails_View", frs);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    RegistrationData Reg = new RegistrationData();


                    //string ADVRefrenceID = dt.Rows[z]["ADVRefrenceID"].ToString().Trim();
                    //AdharVaultAPICalls api = new AdharVaultAPICalls();
                    //string AdharNumber = api.GetAdharFromReference(ADVRefrenceID);
                    //Reg.AadharNo = AdharNumber; //dt.Rows[z]["AaDharNumber"].ToString().Trim();

                    Reg.AadharNo = dt.Rows[z]["AaDharNumber"].ToString().Trim();

                    Reg.Name = dt.Rows[z]["RegisterName"].ToString();
                    Reg.DOB = dt.Rows[z]["DateOfBirth"].ToString();
                    Reg.Gender = dt.Rows[z]["Gender"].ToString().Trim();
                    Reg.LandStatus = dt.Rows[z]["LandStatus"].ToString().Trim();
                    Reg.LandLessCertificate = dt.Rows[z]["LandLessCertificate"].ToString();
                    Reg.BeneficiaryTypesID = dt.Rows[z]["BeneficiaryTypesID"].ToString().Trim();
                    Reg.CATEGORY = dt.Rows[z]["CategoryMasterID"].ToString();
                    Reg.FileCATEGORYCERITIFICATE = dt.Rows[z]["CastCategoryDoc"].ToString();
                    Reg.HANDICAP = dt.Rows[z]["PhysicallyHandicap"].ToString().Trim();
                    Reg.DISABILITYPer = dt.Rows[z]["DisabilityPer"].ToString();

                    Reg.FileHANDICAPCERITIFICATE = dt.Rows[z]["PhysicallyHandicapDoc"].ToString();

                    Reg.MobileNumber = dt.Rows[z]["MobileNumber"].ToString().Trim();
                    Reg.MobileNumber2 = dt.Rows[z]["MobileNumber2"].ToString();
                    Reg.LandLineNumber = dt.Rows[z]["LandLineNumber"].ToString();
                    Reg.EmailId = dt.Rows[z]["EmailID"].ToString().Trim();
                    Reg.PanNumber = dt.Rows[z]["PanNumber"].ToString();
                    Reg.Address1VillageID = dt.Rows[z]["Address1VillageID"].ToString();
                    Reg.Address1PinCode = dt.Rows[z]["Address1PinCode"].ToString().Trim();
                    Reg.Address1HouseNo = dt.Rows[z]["Address1HouseNo"].ToString();
                    Reg.Address1StreetName = dt.Rows[z]["Address1StreetName"].ToString();
                    Reg.Address1City_ID = dt.Rows[z]["Address1City_ID"].ToString().Trim();
                    Reg.Address1TalukaID = dt.Rows[z]["Address1TalukaID"].ToString();
                    Reg.Address1Post_ID = dt.Rows[z]["Address1Post_ID"].ToString();
                    Reg.CLUSTERCODE = dt.Rows[z]["Clusters"].ToString();
                    Reg.ApprovalStatus = dt.Rows[z]["ApprovalStatus"].ToString();
                    Reg.RegistrationDate = dt.Rows[z]["RegistrationDate"].ToString();
                    Reg.RelatedToBeneficiaryCriteria = dt.Rows[z]["AnyOtherDocType"].ToString();
                    Reg.BeneficiaryCriteriaCertificate = dt.Rows[z]["AnyOtherDoc"].ToString();
                    //Reg.Hectare8A = dt.Rows[z]["Hectare8A"].ToString();
                    //Reg.Are8A = dt.Rows[z]["Are8A"].ToString();
                    //Reg.FileFORM8A = dt.Rows[z]["Form8ADoc2"].ToString();
                    //Reg.SURVEYNo8A = dt.Rows[z]["AccountNumber8A"].ToString();
                    //Reg.SurveyNo712 = dt.Rows[z]["SurveyNo712"].ToString();
                    //Reg.Hectare712 = dt.Rows[z]["Hectare712"].ToString();
                    //Reg.Are712 = dt.Rows[z]["Are712"].ToString();
                    //Reg.File712 = dt.Rows[z]["Extracts712Doc"].ToString();

                    lst.Add(Reg);
                }
            }
            catch (Exception ex)
            {
                RegistrationData Reg = new RegistrationData();
                Reg.Message = ex.ToString();

                lst.Add(Reg);


            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }

        #endregion


        #region "GET ACTIVITY"

        [WebMethod]
        public void GetActivity(String SecurityKey, String ActivityID, String BeneficiaryTypesID, String ActivityName, String ComponentID, String SubComponentID, String LandRequired)

        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<Activity> lst = new List<Activity>();

            if (MyCommanClassAPI.CheckApiAuthrization("59", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                Activity d = new Activity();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 

            //if (BeneficiaryTypesID.Trim().Trim().Length == 0)
            //{

            //    clsReturnMessage d = new clsReturnMessage();
            //    d.MessageType = "Warning";
            //    d.Message = "Please Select Beneficiary Types ID";
            //    //  lst.Add(d);
            //    Context.Response.Clear();
            //    Context.Response.ContentType = "application/json";
            //    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            //    Context.Response.Flush();
            //    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
            //    return;
            //}
            //-----------------validation----------------------------//
            try
            {


                List<String> actv = new List<String>();
                actv.Add(ActivityName.Trim());
                actv.Add(ComponentID.Trim());
                actv.Add(SubComponentID.Trim());
                actv.Add(ActivityID.Trim());
                actv.Add(LandRequired.Trim());
                actv.Add(BeneficiaryTypesID.Trim());
                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_GetActivetySearch", actv);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    Activity Ser = new Activity();

                    Ser.ActivityID = dt.Rows[z]["ActivityID"].ToString().Trim();
                    Ser.ComponentID = dt.Rows[z]["ComponentID"].ToString();
                    Ser.SubComponentID = dt.Rows[z]["SubComponentID"].ToString().Trim();
                    Ser.ActivityName = dt.Rows[z]["ActivityName"].ToString();
                    Ser.ActivityCode = dt.Rows[z]["ActivityCode"].ToString().Trim();
                    Ser.ActivityImagePath = dt.Rows[z]["ActivityImagePath"].ToString();
                    Ser.ComponentName = dt.Rows[z]["ComponentName"].ToString().Trim();
                    Ser.SubComponentName = dt.Rows[z]["SubComponentName"].ToString();
                    Ser.MaxSubsidyAmt = dt.Rows[z]["MaxSubsidyAmt"].ToString().Trim();
                    Ser.SubsidyPer = dt.Rows[z]["SubsidyPer"].ToString();
                    Ser.SubsidyAmt = dt.Rows[z]["SubsidyAmt"].ToString().Trim();

                    lst.Add(Ser);
                }
            }
            catch (Exception ex)
            {
                Activity Ser = new Activity();
                Ser.Message = ex.ToString();

                lst.Add(Ser);


            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }

        #region "GET ACTIVITY With Paging"

        [WebMethod]
        public void GetActivityWithPaging(String SecurityKey, String BeneficiaryTypesID, String ActivityName, String LandRequired, String PageSize, String PageNumber)

        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<Activity> lst = new List<Activity>();

            if (MyCommanClassAPI.CheckApiAuthrization("59", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                Activity d = new Activity();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 

            try
            {


                List<String> actv = new List<String>();
                actv.Add(ActivityName.Trim());
                actv.Add(LandRequired.Trim());
                actv.Add(BeneficiaryTypesID.Trim());
                actv.Add(PageSize);
                actv.Add(PageNumber);
                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_GetActivitySearch_WithPaging", actv);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    Activity Ser = new Activity();

                    Ser.ActivityID = dt.Rows[z]["ActivityID"].ToString().Trim();
                    Ser.ComponentID = dt.Rows[z]["ComponentID"].ToString();
                    Ser.SubComponentID = dt.Rows[z]["SubComponentID"].ToString().Trim();
                    Ser.ActivityName = dt.Rows[z]["ActivityName"].ToString();
                    Ser.ActivityCode = dt.Rows[z]["ActivityCode"].ToString().Trim();
                    Ser.ActivityImagePath = dt.Rows[z]["ActivityImagePath"].ToString();
                    Ser.ComponentName = dt.Rows[z]["ComponentName"].ToString().Trim();
                    Ser.SubComponentName = dt.Rows[z]["SubComponentName"].ToString();
                    Ser.MaxSubsidyAmt = dt.Rows[z]["MaxSubsidyAmt"].ToString().Trim();
                    Ser.SubsidyPer = dt.Rows[z]["SubsidyPer"].ToString();
                    Ser.SubsidyAmt = dt.Rows[z]["SubsidyAmt"].ToString().Trim();

                    lst.Add(Ser);
                }
            }
            catch (Exception ex)
            {
                Activity Ser = new Activity();
                Ser.Message = ex.ToString();

                lst.Add(Ser);


            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }




        [WebMethod]
        public void GetActivityWithPagingByGroupID(String SecurityKey, String BeneficiaryTypesID, String ActivityName, String LandRequired, String PageSize, String PageNumber, String ActivityGroupID)

        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<Activity> lst = new List<Activity>();

            if (MyCommanClassAPI.CheckApiAuthrization("59", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                Activity d = new Activity();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 

            try
            {


                List<String> actv = new List<String>();
                actv.Add(ActivityName.Trim());
                actv.Add(LandRequired.Trim());
                actv.Add(BeneficiaryTypesID.Trim());
                actv.Add(PageSize);
                actv.Add(PageNumber);
                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_GetActivitySearch_WithPaging", actv);



                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    Activity Ser = new Activity();

                    if (ActivityGroupID.Trim().Length > 0)
                    {
                        if (ActivityGroupID.Trim() == dt.Rows[z]["ActivityGroupID"].ToString().Trim())
                        {
                            Ser.ActivityID = dt.Rows[z]["ActivityID"].ToString().Trim();
                            Ser.ComponentID = dt.Rows[z]["ComponentID"].ToString();
                            Ser.SubComponentID = dt.Rows[z]["SubComponentID"].ToString().Trim();
                            Ser.ActivityName = dt.Rows[z]["ActivityName"].ToString();
                            Ser.ActivityCode = dt.Rows[z]["ActivityCode"].ToString().Trim();
                            Ser.ActivityImagePath = dt.Rows[z]["ActivityImagePath"].ToString();
                            Ser.ComponentName = dt.Rows[z]["ComponentName"].ToString().Trim();
                            Ser.SubComponentName = dt.Rows[z]["SubComponentName"].ToString();
                            Ser.MaxSubsidyAmt = dt.Rows[z]["MaxSubsidyAmt"].ToString().Trim();
                            Ser.SubsidyPer = dt.Rows[z]["SubsidyPer"].ToString();
                            Ser.SubsidyAmt = dt.Rows[z]["SubsidyAmt"].ToString().Trim();
                            lst.Add(Ser);
                        }
                    }
                    else
                    {
                        Ser.ActivityID = dt.Rows[z]["ActivityID"].ToString().Trim();
                        Ser.ComponentID = dt.Rows[z]["ComponentID"].ToString();
                        Ser.SubComponentID = dt.Rows[z]["SubComponentID"].ToString().Trim();
                        Ser.ActivityName = dt.Rows[z]["ActivityName"].ToString();
                        Ser.ActivityCode = dt.Rows[z]["ActivityCode"].ToString().Trim();
                        Ser.ActivityImagePath = dt.Rows[z]["ActivityImagePath"].ToString();
                        Ser.ComponentName = dt.Rows[z]["ComponentName"].ToString().Trim();
                        Ser.SubComponentName = dt.Rows[z]["SubComponentName"].ToString();
                        Ser.MaxSubsidyAmt = dt.Rows[z]["MaxSubsidyAmt"].ToString().Trim();
                        Ser.SubsidyPer = dt.Rows[z]["SubsidyPer"].ToString();
                        Ser.SubsidyAmt = dt.Rows[z]["SubsidyAmt"].ToString().Trim();
                        lst.Add(Ser);
                    }


                }
            }
            catch (Exception ex)
            {
                Activity Ser = new Activity();
                Ser.Message = ex.ToString();

                lst.Add(Ser);


            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }



        [WebMethod]
        public void GetActivityGroupWithPaging(String SecurityKey, String BeneficiaryTypesID, String ActivityName, String LandRequired, String PageSize, String PageNumber, String lang)
        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<ActivityGroup> lst = new List<ActivityGroup>();

            if (MyCommanClassAPI.CheckApiAuthrization("59", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                ActivityGroup d = new ActivityGroup();
                d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 

            try
            {


                List<String> actv = new List<String>();
                actv.Add(ActivityName.Trim());
                actv.Add(LandRequired.Trim());
                actv.Add(BeneficiaryTypesID.Trim());
                actv.Add(PageSize);
                actv.Add(PageNumber);
                actv.Add(lang);
                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_GetActivityGroupSearch_WithPaging", actv);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    ActivityGroup Ser = new ActivityGroup();
                    Ser.ActivityGroupCode = dt.Rows[z]["ActivityGroupCode"].ToString().Trim();
                    Ser.ActivityGroupID = dt.Rows[z]["ActivityGroupID"].ToString();
                    Ser.ActivityGroupImagePath = dt.Rows[z]["ImageOfActivityGroup"].ToString().Trim();
                    Ser.ActivityGroupName = dt.Rows[z]["ActivityGroupName"].ToString();
                    Ser.Message = "";
                    lst.Add(Ser);
                }
            }
            catch (Exception ex)
            {
                ActivityGroup Ser = new ActivityGroup();
                Ser.Message = ex.ToString();

                lst.Add(Ser);


            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }


        #endregion





        [WebMethod]
        public void GetActivityMr(String SecurityKey, String ActivityID, String BeneficiaryTypesID, String ActivityName, String ComponentID, String SubComponentID, String LandRequired)
        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<Activity> lst = new List<Activity>();

            if (MyCommanClassAPI.CheckApiAuthrization("59", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                Activity d = new Activity();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 

            //if (BeneficiaryTypesID.Trim().Trim().Length == 0)
            //{

            //    clsReturnMessage d = new clsReturnMessage();
            //    d.MessageType = "Warning";
            //    d.Message = "Please Select Beneficiary Types ID";
            //    //  lst.Add(d);
            //    Context.Response.Clear();
            //    Context.Response.ContentType = "application/json";
            //    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            //    Context.Response.Flush();
            //    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
            //    return;
            //}
            //-----------------validation----------------------------//
            try
            {


                List<String> actv = new List<String>();
                actv.Add(ActivityName.Trim());
                actv.Add(ComponentID.Trim());
                actv.Add(SubComponentID.Trim());
                actv.Add(ActivityID.Trim());
                actv.Add(LandRequired.Trim());
                actv.Add(BeneficiaryTypesID.Trim());
                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_GetActivetySearchMr", actv);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    Activity Ser = new Activity();

                    Ser.ActivityID = dt.Rows[z]["ActivityID"].ToString().Trim();
                    Ser.ComponentID = dt.Rows[z]["ComponentID"].ToString();
                    Ser.SubComponentID = dt.Rows[z]["SubComponentID"].ToString().Trim();
                    Ser.ActivityName = dt.Rows[z]["ActivityName"].ToString();
                    Ser.ActivityCode = dt.Rows[z]["ActivityCode"].ToString().Trim();
                    Ser.ActivityImagePath = dt.Rows[z]["ActivityImagePath"].ToString();
                    Ser.ComponentName = dt.Rows[z]["ComponentName"].ToString().Trim();
                    Ser.SubComponentName = dt.Rows[z]["SubComponentName"].ToString();
                    Ser.MaxSubsidyAmt = dt.Rows[z]["MaxSubsidyAmt"].ToString().Trim();
                    Ser.SubsidyPer = dt.Rows[z]["SubsidyPer"].ToString();
                    Ser.SubsidyAmt = dt.Rows[z]["SubsidyAmt"].ToString().Trim();

                    lst.Add(Ser);
                }
            }
            catch (Exception ex)
            {
                Activity Ser = new Activity();
                Ser.Message = ex.ToString();

                lst.Add(Ser);


            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }


        #endregion
        #region


        [WebMethod]
        public void GetActivityMrWithPaging(String SecurityKey, String BeneficiaryTypesID, String ActivityName, String LandRequired, String PageSize, String PageNumber)
        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<Activity> lst = new List<Activity>();

            if (MyCommanClassAPI.CheckApiAuthrization("59", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                Activity d = new Activity();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {


                List<String> actv = new List<String>();
                actv.Add(ActivityName.Trim());
                actv.Add(LandRequired.Trim());
                actv.Add(BeneficiaryTypesID.Trim());
                actv.Add(PageSize);
                actv.Add(PageNumber);
                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_GetActivitySearchMr_WithPage", actv);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    Activity Ser = new Activity();

                    Ser.ActivityID = dt.Rows[z]["ActivityID"].ToString().Trim();
                    Ser.ComponentID = dt.Rows[z]["ComponentID"].ToString();
                    Ser.SubComponentID = dt.Rows[z]["SubComponentID"].ToString().Trim();
                    Ser.ActivityName = dt.Rows[z]["ActivityName"].ToString();
                    Ser.ActivityCode = dt.Rows[z]["ActivityCode"].ToString().Trim();
                    Ser.ActivityImagePath = dt.Rows[z]["ActivityImagePath"].ToString();
                    Ser.ComponentName = dt.Rows[z]["ComponentName"].ToString().Trim();
                    Ser.SubComponentName = dt.Rows[z]["SubComponentName"].ToString();
                    Ser.MaxSubsidyAmt = dt.Rows[z]["MaxSubsidyAmt"].ToString().Trim();
                    Ser.SubsidyPer = dt.Rows[z]["SubsidyPer"].ToString();
                    Ser.SubsidyAmt = dt.Rows[z]["SubsidyAmt"].ToString().Trim();

                    lst.Add(Ser);
                }
            }
            catch (Exception ex)
            {
                Activity Ser = new Activity();
                Ser.Message = ex.ToString();

                lst.Add(Ser);


            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }



        [WebMethod]
        public void GetActivityMrWithPagingByGroupID(String SecurityKey, String BeneficiaryTypesID, String ActivityName, String LandRequired, String PageSize, String PageNumber, String ActivityGroupID)
        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<Activity> lst = new List<Activity>();

            if (MyCommanClassAPI.CheckApiAuthrization("59", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                Activity d = new Activity();
                d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {


                List<String> actv = new List<String>();
                actv.Add(ActivityName.Trim());
                actv.Add(LandRequired.Trim());
                actv.Add(BeneficiaryTypesID.Trim());
                actv.Add(PageSize);
                actv.Add(PageNumber);
                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_GetActivitySearchMr_WithPage", actv);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    Activity Ser = new Activity();
                    if (ActivityGroupID.Trim().Length > 0)
                    {
                        if (ActivityGroupID.Trim() == dt.Rows[z]["ActivityGroupID"].ToString().Trim())
                        {
                            Ser.ActivityID = dt.Rows[z]["ActivityID"].ToString().Trim();
                            Ser.ComponentID = dt.Rows[z]["ComponentID"].ToString();
                            Ser.SubComponentID = dt.Rows[z]["SubComponentID"].ToString().Trim();
                            Ser.ActivityName = dt.Rows[z]["ActivityName"].ToString();
                            Ser.ActivityCode = dt.Rows[z]["ActivityCode"].ToString().Trim();
                            Ser.ActivityImagePath = dt.Rows[z]["ActivityImagePath"].ToString();
                            Ser.ComponentName = dt.Rows[z]["ComponentName"].ToString().Trim();
                            Ser.SubComponentName = dt.Rows[z]["SubComponentName"].ToString();
                            Ser.MaxSubsidyAmt = dt.Rows[z]["MaxSubsidyAmt"].ToString().Trim();
                            Ser.SubsidyPer = dt.Rows[z]["SubsidyPer"].ToString();
                            Ser.SubsidyAmt = dt.Rows[z]["SubsidyAmt"].ToString().Trim();
                            lst.Add(Ser);
                        }
                    }
                    else
                    {
                        Ser.ActivityID = dt.Rows[z]["ActivityID"].ToString().Trim();
                        Ser.ComponentID = dt.Rows[z]["ComponentID"].ToString();
                        Ser.SubComponentID = dt.Rows[z]["SubComponentID"].ToString().Trim();
                        Ser.ActivityName = dt.Rows[z]["ActivityName"].ToString();
                        Ser.ActivityCode = dt.Rows[z]["ActivityCode"].ToString().Trim();
                        Ser.ActivityImagePath = dt.Rows[z]["ActivityImagePath"].ToString();
                        Ser.ComponentName = dt.Rows[z]["ComponentName"].ToString().Trim();
                        Ser.SubComponentName = dt.Rows[z]["SubComponentName"].ToString();
                        Ser.MaxSubsidyAmt = dt.Rows[z]["MaxSubsidyAmt"].ToString().Trim();
                        Ser.SubsidyPer = dt.Rows[z]["SubsidyPer"].ToString();
                        Ser.SubsidyAmt = dt.Rows[z]["SubsidyAmt"].ToString().Trim();
                        lst.Add(Ser);
                    }


                }
            }
            catch (Exception ex)
            {
                Activity Ser = new Activity();
                Ser.Message = ex.ToString();

                lst.Add(Ser);


            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }


        #endregion


        #region "FPO/FPC Other Registration"
        [WebMethod]

        public void GetOtherRegistrationData(String SecurityKey, String RegistrationID)//RegistrationNo
        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<FPOOtherRegistrationData> lst = new List<FPOOtherRegistrationData>();

            if (MyCommanClassAPI.CheckApiAuthrization("40", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                Registration d = new Registration();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            try
            {


                List<String> frs = new List<String>();
                frs.Add(RegistrationID.Trim());
                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_FOthersRegistration_View_Details", frs);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    FPOOtherRegistrationData Regfp = new FPOOtherRegistrationData();

                    Regfp.FPORegistrationNo = dt.Rows[z]["FPORegistrationNo"].ToString().Trim();
                    Regfp.RegisterName = dt.Rows[z]["RegisterName"].ToString();
                    Regfp.BeneficiaryTypesID = dt.Rows[z]["BeneficiaryTypesID"].ToString();
                    Regfp.RegistrationDate = dt.Rows[z]["RegistrationDate"].ToString().Trim();
                    Regfp.RegistrationCertifecate = dt.Rows[z]["RegistrationCertifecate"].ToString().Trim();
                    Regfp.RegisterUnderID = dt.Rows[z]["RegisterUnderID"].ToString();
                    Regfp.RegisteredThroughID = dt.Rows[z]["RegisteredThroughID"].ToString();
                    Regfp.PromoterName = dt.Rows[z]["PromoterName"].ToString().Trim();
                    Regfp.PromoterGender = dt.Rows[z]["PromoterGender"].ToString();
                    Regfp.Promotermobile = dt.Rows[z]["Promotermobile"].ToString().Trim();
                    Regfp.Promoterlandline = dt.Rows[z]["Promoterlandline"].ToString();
                    Regfp.LandLineNumber = dt.Rows[z]["LandLineNumber"].ToString();
                    Regfp.PromoterEmail = dt.Rows[z]["PromoterEmail"].ToString().Trim();
                    Regfp.CeoName = dt.Rows[z]["CeoName"].ToString();
                    Regfp.CeoGender = dt.Rows[z]["CeoGender"].ToString();
                    Regfp.CeoMobile = dt.Rows[z]["CeoMobile"].ToString().Trim();
                    Regfp.CeoLandline = dt.Rows[z]["CeoLandline"].ToString();
                    Regfp.CeoEmail = dt.Rows[z]["CeoEmail"].ToString();
                    Regfp.CeoAuthorisedPerson = dt.Rows[z]["CeoAuthorisedPerson"].ToString().Trim();
                    Regfp.CeoDesignation = dt.Rows[z]["CeoDesignation"].ToString();
                    Regfp.AuthorisedCertifecate = dt.Rows[z]["CeoProofOfAuthorisation"].ToString();
                    Regfp.CeoAuthorisedPersonGen = dt.Rows[z]["CeoAuthorisedPersonGen"].ToString();
                    Regfp.CeoAuthorisedPersonMob = dt.Rows[z]["CeoAuthorisedPersonMob"].ToString();
                    Regfp.CeoAuthorisedPersonEmail = dt.Rows[z]["CeoAuthorisedPersonEmail"].ToString();
                    Regfp.Address1HouseNo = dt.Rows[z]["Address1HouseNo"].ToString();
                    Regfp.Address1StreetName = dt.Rows[z]["Address1StreetName"].ToString();
                    Regfp.Address1City_ID = dt.Rows[z]["Address1City_ID"].ToString();
                    Regfp.Address1TalukaID = dt.Rows[z]["Address1TalukaID"].ToString();
                    Regfp.Address1Post_ID = dt.Rows[z]["Address1Post_ID"].ToString();
                    Regfp.Address1PinCode = dt.Rows[z]["Address1PinCode"].ToString();
                    Regfp.Address1VillageID = dt.Rows[z]["Address1VillageID"].ToString();
                    Regfp.Clusters = dt.Rows[z]["Clusters"].ToString();
                    Regfp.MobileNumber = dt.Rows[z]["MobileNumber"].ToString();
                    Regfp.MobileNumber2 = dt.Rows[z]["MobileNumber2"].ToString();
                    Regfp.LandLineNumber = dt.Rows[z]["LandLineNumber"].ToString();
                    Regfp.EmailID = dt.Rows[z]["EmailID"].ToString();
                    Regfp.PanNumber = dt.Rows[z]["PanNumber"].ToString();
                    Regfp.IsBothAddressSame = dt.Rows[z]["IsBothAddressSame"].ToString();
                    Regfp.Address2HouseNo = dt.Rows[z]["Address2HouseNo"].ToString();
                    Regfp.Address2StreetName = dt.Rows[z]["Address2StreetName"].ToString();
                    Regfp.Address2City_ID = dt.Rows[z]["Address2City_ID"].ToString();
                    Regfp.Address2TalukaID = dt.Rows[z]["Address2TalukaID"].ToString();
                    Regfp.Address2Post_ID = dt.Rows[z]["Address2Post_ID"].ToString();
                    Regfp.Address2PinCode = dt.Rows[z]["Address2PinCode"].ToString();
                    Regfp.Address2VillageID = dt.Rows[z]["Address2VillageID"].ToString();
                    Regfp.Address2Clusters = dt.Rows[z]["Clusters"].ToString();
                    Regfp.Address2Mob1 = dt.Rows[z]["Address2Mob1"].ToString();
                    Regfp.Address2Mob2 = dt.Rows[z]["Address2Mob2"].ToString();
                    Regfp.Address2LandLine = dt.Rows[z]["Address2LandLine"].ToString();
                    Regfp.ApprovalStatus = dt.Rows[z]["ApprovalStatus"].ToString();
                    Regfp.RegistrationID = dt.Rows[z]["RegistrationID"].ToString();

                    lst.Add(Regfp);
                }
            }
            catch (Exception ex)
            {
                FPOOtherRegistrationData Regfp = new FPOOtherRegistrationData();
                Regfp.Message = ex.ToString();

                lst.Add(Regfp);


            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());


        }

        #endregion


        #region "OtherFpoPassword"

        [WebMethod]
        public void SaveOtherPassword(String SecurityKey, String RegistrationID, String RegistationNo, String Name, String Password, String ConfirmPassword)
        {
            List<clsReturnMessage> lst = new List<clsReturnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();

            if (MyCommanClassAPI.CheckApiAuthrization("54", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }


            if (Password.Trim().Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Password";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            else if (ConfirmPassword.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill ConfirmPassword same as Password";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            int UserId = cla.TableID("Tbl_M_LoginDetails", "UserId");
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


                    if (RegistrationID.ToString().Trim().Length > 0)
                    {

                        //UPDATE
                        String str = " UPDATE  Tbl_M_RegistrationDetails SET   IAgree ='1' , IsDeleted=NULL  ";
                        str += " WHERE(RegistrationID = " + RegistrationID.ToString().Trim() + ")";
                        cla.ExecuteCommand(str, command);


                        str = " INSERT INTO Tbl_M_LoginDetails (UserId, RegistrationID, UserName, LoginAs, FullName, UPass)";
                        str += " VALUES(" + UserId + "," + RegistrationID.ToString().Trim() + ",'" + RegistationNo.Trim() + "','Beneficiary',N'" + Name.Trim() + "','" + Password + "')";
                        cla.ExecuteCommand(str, command);


                    }


                    transaction.Commit();
                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Sucess";
                    d.RegistrationID = RegistrationID.ToString();
                    d.Message = "Record Saved Sucessfully";
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

                    clsReturnMessage d = new clsReturnMessage();
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

        #region " DATA DASHBOARD"

        [WebMethod]
        public void GetDataDashboard(String SecurityKey, String RegistrationID)
        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<Dashboard> lst = new List<Dashboard>();

            if (MyCommanClassAPI.CheckApiAuthrization("58", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                Dashboard d = new Dashboard();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(d, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(d, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {

                List<String> apl = new List<String>();
                apl.Add(RegistrationID.Trim());
                apl.Add("");
                apl.Add("");
                apl.Add("");
                apl.Add("");
                apl.Add("");
                apl.Add("");
                apl.Add("");
                apl.Add("");
                apl.Add("");
                apl.Add("");//ddlGPCode
                apl.Add("");//txtBeneficiaryName
                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_Get_ApplicationGrdData", apl);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    Dashboard Sts = new Dashboard();

                    Sts.ApplicationCode = dt.Rows[z]["ApplicationCode"].ToString().Trim();
                    Sts.ApplicationID = dt.Rows[z]["ApplicationID"].ToString().Trim();
                    Sts.Date = dt.Rows[z]["ApplicationDate"].ToString();
                    Sts.RegisterName = dt.Rows[z]["RegisterName"].ToString().Trim();
                    Sts.BeneficiaryTypes = dt.Rows[z]["BeneficiaryTypes"].ToString().Trim();
                    Sts.Activity = dt.Rows[z]["ActivityName"].ToString();
                    Sts.ActivityCode = dt.Rows[z]["ActivityCode"].ToString().Trim();
                    Sts.ActivityImage = dt.Rows[z]["ActivityImagePath"].ToString().Trim();
                    Sts.Component = dt.Rows[z]["ComponentName"].ToString();

                    Sts.Stages = dt.Rows[z]["ApprovalStages"].ToString().Trim();
                    Sts.Status = dt.Rows[z]["ApplicationStatus"].ToString();




                    Sts.ApprovalStageID = dt.Rows[z]["ApprovalStageID"].ToString();

                    #region MyRegion



                    String str = cla.GetExecuteScalar("SELECT  count(WorkReportID)   FROM Tbl_T_Application_WorkReport where ApplicationID=" + Sts.ApplicationID + " and IsDeleted is null  and ApplicationStatusID not in (15,2,25)  ");
                    if (str.Trim().Length == 0)
                    {
                        Sts.ApprovalStageID = "-1";
                    }
                    else
                    {
                        if (Convert.ToInt32(str) == 0)
                        {
                            if (cla.GetExecuteScalar("SELECT  top 1 WorkReportID  FROM Tbl_T_Application_WorkReport where ApplicationID=" + Sts.ApplicationID + " and IsDeleted is null  and ApplicationStatusID=20").Length == 0)
                            {
                                // if back to beni then add new will not avilab
                                Sts.ApprovalStageID = dt.Rows[z]["ApprovalStageID"].ToString();
                            }
                            else
                            {
                                Sts.ApprovalStageID = "-1";
                            }
                        }
                    }



                    if (cla.GetExecuteScalar("SELECT  top 1 WorkReportID   FROM Tbl_T_Application_WorkReport where ApplicationID=" + Sts.ApplicationID + " and IsDeleted is null and ApplicationStatusID not in (2,25) ").Length > 0)// rejected nahi hay
                    {

                        if (cla.GetExecuteScalar("SELECT PartialPaymentAllowed FROM Tbl_M_ActivityMaster where ActivityID=(Select Tbl_T_ApplicationDetails.ActivityID from Tbl_T_ApplicationDetails where ApplicationID=" + Sts.ApplicationID + " and Tbl_T_ApplicationDetails.IsDeleted is null ) and IsDeleted is null") == "NO")
                        {
                            Sts.ApprovalStageID = "-1";
                        }
                    }

                    string PoCRAPhase = WebConfigurationManager.AppSettings["CurrentPoCRAPhase"].ToString().ToUpper();
                    if (PoCRAPhase == "PHASE1")
                    {
                        string ActivityStatus = cla.GetExecuteScalar("SELECT PartialPaymentAllowed FROM Tbl_M_ActivityMaster where IsDeleted is null and ActivityID=(Select Tbl_T_ApplicationDetails.ActivityID from Tbl_T_ApplicationDetails where ApplicationID=" + Sts.ApplicationID + " and Tbl_T_ApplicationDetails.IsDeleted is null ) and IsDeleted is null");

                        if (ActivityStatus.ToUpper() == "YES" || ActivityStatus.ToUpper() == "")
                        {
                            Sts.ApprovalStageID = "-1";
                        }
                    }
                    #endregion

                    if (dt.Rows[z]["PresanctionStatus"].ToString() == "Y")
                    {
                        Sts.ApprovalStageID = "-1";
                    }


                    if (Convert.ToInt32(Sts.ApprovalStageID) < 5)
                    {
                        Sts.ApprovalStageID = "-1";
                    }

                    //

                    if (Convert.ToInt32(Sts.ApprovalStageID) >= 5)
                    {
                        String ActivityID = cla.GetExecuteScalar("Select ActivityID from Tbl_T_ApplicationDetails  where ApplicationID=" + dt.Rows[z]["ApplicationID"].ToString() + "");
                        Sts.PreSanctionLetter = "/admintrans/Popup/PreLetters/" + cla.GetExecuteScalar("SELECT  PreSenLetterUrl FROM Tbl_M_PreSenLetterType where PreSenLetterTypeID=(Select PreSenLetterTypeID from Tbl_M_ActivityMaster  where ActivityID=" + ActivityID + ")") + "?ID=" + dt.Rows[z]["ApplicationID"].ToString().Trim() + "";
                    }
                    else
                    {
                        Sts.PreSanctionLetter = "#";
                    }

                    List<ApplicationLogDetails> l = new List<ApplicationLogDetails>();
                    apl.Add(dt.Rows[z]["ApplicationID"].ToString());//ApplicationID.Trim()

                    DataTable dt2 = cla.GetDtByProcedure("SP_Application_Log", apl);

                    for (int y = 0; y != dt2.Rows.Count; y++)
                    {
                        ApplicationLogDetails c = new ApplicationLogDetails();
                        c.PreArvDate = dt2.Rows[y]["Date"].ToString();
                        c.UpdatedBy = dt2.Rows[y]["FullName"].ToString();
                        c.Level = dt2.Rows[y]["Desig_Name"].ToString();
                        c.PreStage = dt2.Rows[y]["ApplicationStatus"].ToString();
                        c.PreStatus = dt2.Rows[y]["ApprovalStages"].ToString();
                        c.Remark = dt2.Rows[y]["Remark"].ToString();
                        c.Reason = dt2.Rows[y]["Reason"].ToString();
                        l.Add(c);
                    }

                    Sts.log = l;
                    lst.Add(Sts);
                }
            }
            catch (Exception ex)
            {
                Dashboard Sts = new Dashboard();
                Sts.Message = ex.ToString();

                lst.Add(Sts);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }


        #endregion


        #region " DATA DASHBOARD"

        [WebMethod]
        public void GetDataDashboardWithPaymentHideFlag(String SecurityKey, String RegistrationID)
        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<DashboardWithFlag> lst = new List<DashboardWithFlag>();

            if (MyCommanClassAPI.CheckApiAuthrization("58", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                Dashboard d = new Dashboard();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(d, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(d, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {

                List<String> apl = new List<String>();
                apl.Add(RegistrationID.Trim());
                apl.Add("");
                apl.Add("");
                apl.Add("");
                apl.Add("");
                apl.Add("");
                apl.Add("");
                apl.Add("");
                apl.Add("");
                apl.Add("");
                apl.Add("");//ddlGPCode
                apl.Add("");//txtBeneficiaryName
                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_Get_ApplicationGrdData", apl);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    DashboardWithFlag Sts = new DashboardWithFlag();

                    Sts.ApplicationCode = dt.Rows[z]["ApplicationCode"].ToString().Trim();
                    Sts.ApplicationID = dt.Rows[z]["ApplicationID"].ToString().Trim();
                    Sts.Date = dt.Rows[z]["ApplicationDate"].ToString();
                    Sts.RegisterName = dt.Rows[z]["RegisterName"].ToString().Trim();
                    Sts.BeneficiaryTypes = dt.Rows[z]["BeneficiaryTypes"].ToString().Trim();
                    Sts.Activity = dt.Rows[z]["ActivityName"].ToString();
                    Sts.ActivityCode = dt.Rows[z]["ActivityCode"].ToString().Trim();
                    Sts.ActivityImage = dt.Rows[z]["ActivityImagePath"].ToString().Trim();
                    Sts.Component = dt.Rows[z]["ComponentName"].ToString();

                    Sts.Stages = dt.Rows[z]["ApprovalStages"].ToString().Trim();
                    Sts.Status = dt.Rows[z]["ApplicationStatus"].ToString();




                    Sts.ApprovalStageID = dt.Rows[z]["ApprovalStageID"].ToString();


                    #region MyRegion
                    Sts.ApprovalStageID = dt.Rows[z]["ApprovalStageID"].ToString();





                    String str = cla.GetExecuteScalar("SELECT  count(WorkReportID)   FROM Tbl_T_Application_WorkReport where ApplicationID=" + Sts.ApplicationID + " and IsDeleted is null  and ApplicationStatusID not in (15,2,25)");
                    if (str.Trim().Length == 0)
                    {
                        Sts.ShowPaymentBtn = false;
                    }
                    else
                    {
                        if (Convert.ToInt32(str) == 0)
                        {
                            if (cla.GetExecuteScalar("SELECT  top 1 WorkReportID  FROM Tbl_T_Application_WorkReport where ApplicationID=" + Sts.ApplicationID + " and IsDeleted is null  and ApplicationStatusID=20").Length == 0)
                            {
                                // if back to beni then add new will not avilab
                                Sts.ShowPaymentBtn = true;
                            }
                            else
                            {
                                Sts.ShowPaymentBtn = false;
                            }
                        }
                    }



                    if (cla.GetExecuteScalar("SELECT  top 1 WorkReportID   FROM Tbl_T_Application_WorkReport where ApplicationID=" + Sts.ApplicationID + " and IsDeleted is null and ApplicationStatusID not in (2,25) ").Length > 0)// rejected nahi hay
                    {

                        if (cla.GetExecuteScalar("SELECT PartialPaymentAllowed FROM Tbl_M_ActivityMaster where ActivityID=(Select Tbl_T_ApplicationDetails.ActivityID from Tbl_T_ApplicationDetails where ApplicationID=" + Sts.ApplicationID + " and Tbl_T_ApplicationDetails.IsDeleted is null ) and IsDeleted is null") == "NO")
                        {
                            Sts.ShowPaymentBtn = false;
                        }
                    }

                    string PoCRAPhase = WebConfigurationManager.AppSettings["CurrentPoCRAPhase"].ToString().ToUpper();
                    if (PoCRAPhase == "PHASE1")
                    {
                        string ActivityStatus = cla.GetExecuteScalar("SELECT PartialPaymentAllowed FROM Tbl_M_ActivityMaster where IsDeleted is null and ActivityID=(Select Tbl_T_ApplicationDetails.ActivityID from Tbl_T_ApplicationDetails where ApplicationID=" + Sts.ApplicationID + " and Tbl_T_ApplicationDetails.IsDeleted is null ) and IsDeleted is null");

                        if (ActivityStatus.ToUpper() == "YES" || ActivityStatus.ToUpper() == "")
                        {
                            Sts.ShowPaymentBtn = false;
                        }
                    }
                    #endregion

                    if (dt.Rows[z]["PresanctionStatus"].ToString() == "Y")
                    {
                        Sts.ShowPaymentBtn = false;
                    }


                    if (Convert.ToInt32(Sts.ApprovalStageID) < 5)
                    {
                        Sts.ShowPaymentBtn = false;
                    }

                    //

                    if (Convert.ToInt32(Sts.ApprovalStageID) >= 5)
                    {
                        String ActivityID = cla.GetExecuteScalar("Select ActivityID from Tbl_T_ApplicationDetails  where ApplicationID=" + dt.Rows[z]["ApplicationID"].ToString() + "");
                        Sts.PreSanctionLetter = "/admintrans/Popup/PreLetters/" + cla.GetExecuteScalar("SELECT  PreSenLetterUrl FROM Tbl_M_PreSenLetterType where PreSenLetterTypeID=(Select PreSenLetterTypeID from Tbl_M_ActivityMaster  where ActivityID=" + ActivityID + ")") + "?ID=" + dt.Rows[z]["ApplicationID"].ToString().Trim() + "";
                    }
                    else
                    {
                        Sts.PreSanctionLetter = "#";
                    }

                    List<ApplicationLogDetails> l = new List<ApplicationLogDetails>();
                    apl.Add(dt.Rows[z]["ApplicationID"].ToString());//ApplicationID.Trim()

                    DataTable dt2 = cla.GetDtByProcedure("SP_Application_Log", apl);

                    for (int y = 0; y != dt2.Rows.Count; y++)
                    {
                        ApplicationLogDetails c = new ApplicationLogDetails();
                        c.PreArvDate = dt2.Rows[y]["Date"].ToString();
                        c.UpdatedBy = dt2.Rows[y]["FullName"].ToString();
                        c.Level = dt2.Rows[y]["Desig_Name"].ToString();
                        c.PreStage = dt2.Rows[y]["ApplicationStatus"].ToString();
                        c.PreStatus = dt2.Rows[y]["ApprovalStages"].ToString();
                        c.Remark = dt2.Rows[y]["Remark"].ToString();
                        c.Reason = dt2.Rows[y]["Reason"].ToString();
                        l.Add(c);
                    }

                    Sts.log = l;
                    lst.Add(Sts);
                }
            }
            catch (Exception ex)
            {
                DashboardWithFlag Sts = new DashboardWithFlag();
                Sts.Message = ex.ToString();

                lst.Add(Sts);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }


        #endregion


        //#region " Application for Geofencing(2PreDeskTwo)"

        //[WebMethod]
        //public void GetAPPLICATIONforGeofencing(String SecurityKey, String UserID, String FromRegistrationDate, String ToRegistrationDate, String ApprovalStatus, String ComponentID, String SubComponentID, String ActivityCategoryID, String ActivityID)

        //{
        //    DataTable dt = new DataTable();

        //    MyClass cla = new MyClass();
        //    List<ApplicationSt> lst = new List<ApplicationSt>();

        //    if (MyCommanClassAPI.CheckApiAuthrization("64", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
        //    {
        //        ApplicationSt d = new ApplicationSt();
        //        //   d.Message = "Authorization Failed";

        //        Context.Response.Clear();
        //        Context.Response.ContentType = "application/json";
        //        Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(d, Formatting.Indented).ToString().Length.ToString());
        //        Context.Response.Flush();

        //        Context.Response.Write(JsonConvert.SerializeObject(d, Formatting.Indented).ToString());
        //        return;
        //    }
        //    //------------validations ---------------------// 


        //    try
        //    {

        //        List<String> apl = new List<String>();

        //        apl.Add("");
        //        apl.Add(cla.mdy(FromRegistrationDate.Trim()));
        //        apl.Add(cla.mdy(ToRegistrationDate.Trim()));
        //        apl.Add(ApprovalStatus.Trim());
        //        apl.Add(ComponentID.Trim());
        //        apl.Add(SubComponentID.Trim());
        //        apl.Add(ActivityCategoryID.Trim());
        //        apl.Add(ActivityID.Trim());
        //        apl.Add("3");
        //        apl.Add(UserID.Trim());
        //        dt = new DataTable();
        //        dt = cla.GetDtByProcedure("SP_Get_ApplicationGrdData", apl);

        //        for (int z = 0; z != dt.Rows.Count; z++)
        //        {
        //            ApplicationSt Sts = new ApplicationSt();

        //            Sts.ApplicationID = dt.Rows[z]["ApplicationID"].ToString().Trim();
        //            Sts.Date = dt.Rows[z]["ApplicationDate"].ToString();
        //            Sts.RegisterName = dt.Rows[z]["RegisterName"].ToString().Trim();
        //            Sts.RAppStatus = dt.Rows[z]["RAppStatus"].ToString().Trim();
        //            Sts.Activity = dt.Rows[z]["ActivityName"].ToString();
        //            Sts.ActivityCode = dt.Rows[z]["ActivityCode"].ToString().Trim();
        //            Sts.Component = dt.Rows[z]["ComponentName"].ToString();
        //            Sts.SubComponentName = dt.Rows[z]["SubComponentName"].ToString();
        //            Sts.BeneficiaryTypesID = dt.Rows[z]["BeneficiaryTypesID"].ToString().Trim();
        //            Sts.BeneficiaryTypes = dt.Rows[z]["BeneficiaryTypes"].ToString();
        //            Sts.ApprovalStages = dt.Rows[z]["ApprovalStages"].ToString().Trim();
        //            Sts.APPStatus = dt.Rows[z]["ApplicationStatus"].ToString();
        //            Sts.LandHect = dt.Rows[z]["TotalLand"].ToString().Trim();
        //            Sts.AssignTo = dt.Rows[z]["AssignTo"].ToString();
        //            Sts.RegistrationID = dt.Rows[z]["RegistrationID"].ToString().Trim();
        //            lst.Add(Sts);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ApplicationSt Sts = new ApplicationSt();
        //        Sts.Message = ex.ToString();

        //        lst.Add(Sts);
        //    }

        //    Context.Response.Clear();
        //    Context.Response.ContentType = "application/json";
        //    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
        //    Context.Response.Flush();
        //    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        //}


        //#endregion


        //#region " Application for Geofencing(2PreDeskTwo)"

        //[WebMethod]
        //public void GetAPPLICATIONforGeofencing_withPaging(String SecurityKey, String UserID, String FromRegistrationDate, String ToRegistrationDate, String ApprovalStatus, String ComponentID, String SubComponentID, String ActivityCategoryID, String ActivityID, String PageSize, String PageNumber)

        //{
        //    DataTable dt = new DataTable();

        //    MyClass cla = new MyClass();
        //    List<ApplicationSt> lst = new List<ApplicationSt>();

        //    if (MyCommanClassAPI.CheckApiAuthrization("64", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
        //    {
        //        ApplicationSt d = new ApplicationSt();
        //        //   d.Message = "Authorization Failed";

        //        Context.Response.Clear();
        //        Context.Response.ContentType = "application/json";
        //        Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(d, Formatting.Indented).ToString().Length.ToString());
        //        Context.Response.Flush();

        //        Context.Response.Write(JsonConvert.SerializeObject(d, Formatting.Indented).ToString());
        //        return;
        //    }
        //    //------------validations ---------------------// 


        //    try
        //    {
        //        int intPageSize = 10;
        //        int intPageNumber = 1;

        //        int.TryParse(PageSize, out intPageSize);
        //        int.TryParse(PageNumber, out intPageNumber);


        //        List<String> apl = new List<String>();

        //        apl.Add("");
        //        apl.Add(cla.mdy(FromRegistrationDate.Trim()));
        //        apl.Add(cla.mdy(ToRegistrationDate.Trim()));
        //        apl.Add(ApprovalStatus.Trim());
        //        apl.Add(ComponentID.Trim());
        //        apl.Add(SubComponentID.Trim());
        //        apl.Add(ActivityCategoryID.Trim());
        //        apl.Add(ActivityID.Trim());
        //        apl.Add("3");
        //        apl.Add(UserID.Trim());
        //        apl.Add(intPageSize.ToString());
        //        apl.Add(intPageNumber.ToString());
        //        dt = new DataTable();
        //        dt = cla.GetDtByProcedure("SP_Get_ApplicationGrdData_WithPaging", apl);

        //        for (int z = 0; z != dt.Rows.Count; z++)
        //        {
        //            ApplicationSt Sts = new ApplicationSt();

        //            Sts.ApplicationID = dt.Rows[z]["ApplicationID"].ToString().Trim();
        //            Sts.Date = dt.Rows[z]["ApplicationDate"].ToString();
        //            Sts.RegisterName = dt.Rows[z]["RegisterName"].ToString().Trim();
        //            Sts.RAppStatus = dt.Rows[z]["RAppStatus"].ToString().Trim();
        //            Sts.Activity = dt.Rows[z]["ActivityName"].ToString();
        //            Sts.ActivityCode = dt.Rows[z]["ActivityCode"].ToString().Trim();
        //            Sts.Component = dt.Rows[z]["ComponentName"].ToString();
        //            Sts.SubComponentName = dt.Rows[z]["SubComponentName"].ToString();
        //            Sts.BeneficiaryTypesID = dt.Rows[z]["BeneficiaryTypesID"].ToString().Trim();
        //            Sts.BeneficiaryTypes = dt.Rows[z]["BeneficiaryTypes"].ToString();
        //            Sts.ApprovalStages = dt.Rows[z]["ApprovalStages"].ToString().Trim();
        //            Sts.APPStatus = dt.Rows[z]["ApplicationStatus"].ToString();
        //            Sts.LandHect = dt.Rows[z]["TotalLand"].ToString().Trim();
        //            Sts.AssignTo = dt.Rows[z]["AssignTo"].ToString();
        //            Sts.RegistrationID = dt.Rows[z]["RegistrationID"].ToString().Trim();
        //            lst.Add(Sts);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ApplicationSt Sts = new ApplicationSt();
        //        Sts.Message = ex.ToString();

        //        lst.Add(Sts);
        //    }

        //    Context.Response.Clear();
        //    Context.Response.ContentType = "application/json";
        //    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
        //    Context.Response.Flush();
        //    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        //}


        //#endregion
        #region " Application for Geofencing(2PreDeskTwo)"

        [WebMethod]
        public void GetAPPLICATIONforGeofencing_Paging(String SecurityKey, String UserID, String FromRegistrationDate, String ToRegistrationDate, String ApprovalStatus, String ComponentID, String SubComponentID, String ActivityCategoryID, String ActivityID, String GPCode, String BeneficiaryName, String PageSize, String PageNumber)

        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<ApplicationSt> lst = new List<ApplicationSt>();

            if (MyCommanClassAPI.CheckApiAuthrization("64", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                ApplicationSt d = new ApplicationSt();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(d, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(d, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {
                int intPageSize = 10;
                int intPageNumber = 1;

                int.TryParse(PageSize, out intPageSize);
                int.TryParse(PageNumber, out intPageNumber);


                List<String> apl = new List<String>();

                apl.Add("");
                apl.Add(cla.mdy(FromRegistrationDate.Trim()));
                apl.Add(cla.mdy(ToRegistrationDate.Trim()));
                apl.Add(ApprovalStatus.Trim());
                apl.Add(ComponentID.Trim());
                apl.Add(SubComponentID.Trim());
                apl.Add(ActivityCategoryID.Trim());
                apl.Add(ActivityID.Trim());
                apl.Add("3");
                apl.Add(UserID.Trim());
                apl.Add(GPCode.Trim());
                apl.Add(BeneficiaryName.Trim());
                apl.Add(intPageSize.ToString());
                apl.Add(intPageNumber.ToString());
                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_Get_ApplicationGrdData_Paging", apl);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    ApplicationSt Sts = new ApplicationSt();

                    Sts.ApplicationID = dt.Rows[z]["ApplicationID"].ToString().Trim();
                    Sts.Date = dt.Rows[z]["ApplicationDate"].ToString();
                    Sts.RegisterName = dt.Rows[z]["RegisterName"].ToString().Trim();
                    Sts.RAppStatus = dt.Rows[z]["RAppStatus"].ToString().Trim();
                    Sts.Activity = dt.Rows[z]["ActivityName"].ToString();
                    Sts.ActivityCode = dt.Rows[z]["ActivityCode"].ToString().Trim();
                    Sts.Component = dt.Rows[z]["ComponentName"].ToString();
                    Sts.SubComponentName = dt.Rows[z]["SubComponentName"].ToString();
                    Sts.BeneficiaryTypesID = dt.Rows[z]["BeneficiaryTypesID"].ToString().Trim();
                    Sts.BeneficiaryTypes = dt.Rows[z]["BeneficiaryTypes"].ToString();
                    Sts.ApprovalStages = dt.Rows[z]["ApprovalStages"].ToString().Trim();
                    Sts.APPStatus = dt.Rows[z]["ApplicationStatus"].ToString();
                    Sts.LandHect = dt.Rows[z]["TotalLand"].ToString().Trim();
                    Sts.AssignTo = dt.Rows[z]["AssignTo"].ToString();
                    Sts.RegistrationID = dt.Rows[z]["RegistrationID"].ToString().Trim();
                    Sts.AadharlinkStatus = dt.Rows[z]["AadharlinkStatus"].ToString();
                    Sts.BtnStatus = dt.Rows[z]["BtnStatus"].ToString();

                    lst.Add(Sts);
                }
            }
            catch (Exception ex)
            {
                ApplicationSt Sts = new ApplicationSt();
                Sts.Message = ex.ToString();

                lst.Add(Sts);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }


        #endregion


        #region'Farmer Profile Details'
        [WebMethod]

        public void FarmerProfileDetails(String SecurityKey, String RegistrationID)

        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<FarmerProDetails> lst = new List<FarmerProDetails>();

            if (MyCommanClassAPI.CheckApiAuthrization("53", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                FarmerProDetails d = new FarmerProDetails();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            try
            {


                List<String> frs = new List<String>();
                frs.Add(RegistrationID.Trim());
                frs.Add("");
                frs.Add("");
                frs.Add("");
                frs.Add("");
                frs.Add("");
                frs.Add("");
                frs.Add("");
                frs.Add("");
                frs.Add("");
                frs.Add("");
                frs.Add("");
                frs.Add("1");
                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_FormarRegistration_Details", frs);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    FarmerProDetails Reg = new FarmerProDetails();
                    Reg.RegistrationID = dt.Rows[z]["RegistrationID"].ToString();

                    //string ADVRefrenceID = dt.Rows[z]["ADVRefrenceID"].ToString().Trim();
                    //AdharVaultAPICalls api = new AdharVaultAPICalls();
                    //string AdharNumber = api.GetAdharFromReference(ADVRefrenceID);
                    //Reg.AadharNo = AdharNumber;//

                    Reg.AadharNo = dt.Rows[z]["AaDharNumber"].ToString().Trim();

                    Reg.Name = dt.Rows[z]["RegisterName"].ToString();
                    Reg.DOB = dt.Rows[z]["DateOfBirth"].ToString();
                    Reg.Gender = dt.Rows[z]["Gender"].ToString().Trim();
                    Reg.BeneficiaryTypesID = dt.Rows[z]["BeneficiaryTypesID"].ToString().Trim();
                    Reg.CATEGORY = dt.Rows[z]["CategoryMasterID"].ToString();//
                    Reg.CategoryMaster = dt.Rows[z]["CategoryMaster"].ToString();
                    Reg.FileCATEGORYCERITIFICATE = dt.Rows[z]["CastCategoryDoc"].ToString();
                    Reg.HANDICAP = dt.Rows[z]["PhysicallyHandicap"].ToString().Trim();
                    Reg.FileHANDICAPCERITIFICATE = dt.Rows[z]["PhysicallyHandicapDoc"].ToString();
                    Reg.DISABILITYPer = dt.Rows[z]["DisabilityPer"].ToString();
                    Reg.LandStatus = dt.Rows[z]["LandStatus"].ToString().Trim();
                    Reg.LandLessCertificate = dt.Rows[z]["LandLessCertificate"].ToString();
                    Reg.MobileNumber = dt.Rows[z]["MobileNumber"].ToString().Trim();
                    Reg.MobileNumber2 = dt.Rows[z]["MobileNumber2"].ToString();
                    Reg.LandLineNumber = dt.Rows[z]["LandLineNumber"].ToString();
                    Reg.EmailId = dt.Rows[z]["EmailID"].ToString().Trim();
                    Reg.PanNumber = dt.Rows[z]["PanNumber"].ToString();
                    Reg.Address1VillageID = dt.Rows[z]["Address1VillageID"].ToString();
                    Reg.Address1PinCode = dt.Rows[z]["Address1PinCode"].ToString().Trim();
                    Reg.Address1HouseNo = dt.Rows[z]["Address1HouseNo"].ToString();
                    Reg.Address1StreetName = dt.Rows[z]["Address1StreetName"].ToString();
                    Reg.Address1City_ID = dt.Rows[z]["Address1City_ID"].ToString().Trim();
                    Reg.Address1TalukaID = dt.Rows[z]["Address1TalukaID"].ToString();
                    Reg.Address1Post_ID = dt.Rows[z]["Address1Post_ID"].ToString();
                    Reg.CLUSTERCODE = dt.Rows[z]["Clusters"].ToString();
                    Reg.ApprovalStatus = dt.Rows[z]["ApprovalStatus"].ToString();
                    Reg.RegistrationDate = dt.Rows[z]["RegistrationDate"].ToString();
                    Reg.Taluka = dt.Rows[z]["Taluka"].ToString();
                    Reg.CityName = dt.Rows[z]["Cityname"].ToString();
                    Reg.VillageName = dt.Rows[z]["VillageName"].ToString();
                    Reg.PostName = dt.Rows[z]["PostName"].ToString();
                    Reg.RelatedToBeneficiaryCriteria = dt.Rows[z]["AnyOtherDocType"].ToString();
                    Reg.BeneficiaryCriteriaCertificate = dt.Rows[z]["AnyOtherDoc"].ToString();
                    // frs.Clear();//
                    frs.Add(RegistrationID.Trim());

                    DataTable dtLand = cla.GetDtByProcedure("SP_GetLandParentDetails", frs);

                    for (int x = 0; x != dtLand.Rows.Count; x++)
                    {
                        Reg.LandID = dtLand.Rows[x]["LandID"].ToString();
                        Reg.LANDDISTRICT = dtLand.Rows[x]["Cityname"].ToString();
                        Reg.City_ID = dtLand.Rows[x]["City_ID"].ToString();
                        Reg.LANDTALUKA = dtLand.Rows[x]["Taluka"].ToString();
                        Reg.TalukaID = dtLand.Rows[x]["TalukaID"].ToString();
                        Reg.VillageID = dtLand.Rows[x]["VillageID"].ToString();
                        Reg.LANDVILLAGE = dtLand.Rows[x]["VillageName"].ToString();
                        Reg.SURVEYNo8A = dtLand.Rows[x]["AccountNumber8A"].ToString();
                        Reg.Hectare8A = dtLand.Rows[x]["Hectare8A"].ToString();
                        Reg.Are8A = dtLand.Rows[x]["Are8A"].ToString();
                        Reg.FileFORM8A = dtLand.Rows[x]["Form8ADoc2"].ToString();
                        Reg.SURVEYNo8A = dtLand.Rows[x]["AccountNumber8A"].ToString();
                        Reg.Form8ADoc = dtLand.Rows[x]["Form8ADoc"].ToString();
                        Reg.Form8ADoc2 = dtLand.Rows[x]["Form8ADoc2"].ToString();

                    }
                    lst.Add(Reg);


                }
            }
            catch (Exception ex)
            {
                FarmerProDetails Reg = new FarmerProDetails();
                Reg.Message = ex.ToString();

                lst.Add(Reg);


            }



            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }

        #endregion

        #region 'Remove Land Details'
        [WebMethod]
        public void RemoveLandDetails(String SecurityKey, String LandID, String RegistrationID)
        {

            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<RemoveLandD> lst = new List<RemoveLandD>();

            if (MyCommanClassAPI.CheckApiAuthrization("50", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                RemoveLandD d = new RemoveLandD();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {

                String S = cla.GetExecuteScalar("SELECT TOP 1 ApplicationID  FROM Tbl_T_ApplicationDetails where LandID=" + LandID.ToString() + " and IsDeleted is null  and ApplicationStatusID <> 2 ");

                if (S.Length > 0)
                {
                    //ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> alert('Land Can not be deleted because it is used in application');  </script>", false);

                    RemoveLandD d = new RemoveLandD();
                    d.Message = "Land Can not be deleted because it is used in application";
                    d.MessageType = "Error";
                    lst.Add(d);
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();

                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;

                }


                String rS = cla.GetExecuteScalar("SELECT TOP 1 ApplicationID  FROM Tbl_T_ApplicationDetails where LandID in (Select LandID From Tbl_M_RegistrationLand where ParentLandID=" + LandID.ToString() + ") and IsDeleted is null  and ApplicationStatusID <> 2 ");

                if (rS.Length > 0)
                {

                    RemoveLandD d = new RemoveLandD();
                    d.Message = "Land Can not be deleted because it is used in application";
                    d.MessageType = "Error";
                    lst.Add(d);
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();

                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;

                }

                S = cla.GetExecuteScalar("SELECT  isnull(count(L.LandID),0) FROM Tbl_M_RegistrationLand AS L INNER JOIN Tbl_M_VillageMaster AS V ON V.VillageID = L.VillageID WHERE (L.RegistrationID = " + RegistrationID + ") AND (L.ParentLandID IS NULL) and L.isdeleted is null and L.VillageID <> 0 ");
                if (S.Length > 0)
                {
                    if (Convert.ToInt32(S) <= 1)
                    {
                        if (cla.GetExecuteScalar("select top 1 VillageID from Tbl_M_VillageMaster where VillageID=(Select Address1VillageID  from Tbl_M_RegistrationDetails where  RegistrationID= " + RegistrationID + ") and Isdeleted is null and UserInPocra is not null ").Length == 0)
                        {
                            RemoveLandD d3 = new RemoveLandD();
                            d3.Message = "Please update your address and add new address under pocra village";
                            d3.MessageType = "Error";
                            lst.Add(d3);
                            Context.Response.Clear();
                            Context.Response.ContentType = "application/json";
                            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                            Context.Response.Flush();

                            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                            return;
                        }

                    }

                }



                List<String> apl = new List<String>();
                apl.Add(LandID.Trim());

                bool str = cla.ExecuteByProcedure("SP_Remove_LandDetails", apl);
                if (str == true)
                {
                    cla.ExecuteCommand("update Tbl_T_ApplicationDetails Set IsDeleted=1  where RegistrationID=" + RegistrationID.ToString() + "  and LandID=" + LandID.ToString() + " ");

                    DataTable dtLandCheck = cla.GetDataTable("SELECT  TOP (1) L.VillageID, V.City_ID, V.ClustersMasterID, V.TalukaID, V.CircleID FROM Tbl_M_RegistrationLand AS L INNER JOIN Tbl_M_VillageMaster AS V ON V.VillageID = L.VillageID WHERE (L.RegistrationID = " + RegistrationID.ToString().Trim() + ") AND (L.ParentLandID IS NULL) and L.IsDeleted is null ORDER BY L.LandID");
                    if (dtLandCheck.Rows.Count == 0)
                    {

                        String strq = " UPDATE Tbl_M_RegistrationDetails SET LandStatus ='NO' WHERE RegistrationID = " + RegistrationID.ToString() + " ";
                        cla.ExecuteCommand(strq);
                        //LandStatus = "NO";
                    }

                    RemoveLandD d = new RemoveLandD();
                    d.MessageType = "Sucess";
                    d.Message = "Your details have been Deleted successfully.";
                    lst.Add(d);
                }


                //  lst.Add(Sts);

            }
            catch (Exception ex)
            {
                RemoveLandD Sts = new RemoveLandD();
                Sts.Message = ex.ToString();

                lst.Add(Sts);

            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
        }
        #endregion

        #region "FPO/FPC Other Profile Details"
        [WebMethod]

        public void FPOFPCOtherProfileDetails(String SecurityKey, String RegistrationID)//RegistrationNo
        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<FPOFPCProfDetails> lst = new List<FPOFPCProfDetails>();

            if (MyCommanClassAPI.CheckApiAuthrization("41", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                FPOFPCProfDetails d = new FPOFPCProfDetails();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            try
            {


                List<String> fpd = new List<String>();
                fpd.Add(RegistrationID.Trim());
                fpd.Add("");
                fpd.Add("");
                fpd.Add("");
                fpd.Add("");
                fpd.Add("");
                fpd.Add("");
                fpd.Add("");
                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_FOthersRegistration_Details", fpd);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    FPOFPCProfDetails Regfp = new FPOFPCProfDetails();

                    Regfp.FPORegistrationNo = dt.Rows[z]["FPORegistrationNo"].ToString().Trim();
                    Regfp.RegisterName = dt.Rows[z]["RegisterName"].ToString();
                    Regfp.BeneficiaryTypesID = dt.Rows[z]["BeneficiaryTypesID"].ToString();
                    Regfp.RegistrationDate = dt.Rows[z]["RegistrationDate"].ToString().Trim();
                    Regfp.RegistrationCertifecate = dt.Rows[z]["RegistrationCertifecate"].ToString().Trim();
                    Regfp.RegisterUnderID = dt.Rows[z]["RegisterUnderID"].ToString();
                    Regfp.RegisteredThroughID = dt.Rows[z]["RegisteredThroughID"].ToString();
                    Regfp.PromoterName = dt.Rows[z]["PromoterName"].ToString().Trim();
                    Regfp.PromoterGender = dt.Rows[z]["PromoterGender"].ToString();
                    Regfp.Promotermobile = dt.Rows[z]["Promotermobile"].ToString().Trim();
                    Regfp.Promoterlandline = dt.Rows[z]["Promoterlandline"].ToString();
                    Regfp.LandLineNumber = dt.Rows[z]["LandLineNumber"].ToString();
                    Regfp.PromoterEmail = dt.Rows[z]["PromoterEmail"].ToString().Trim();
                    Regfp.CeoName = dt.Rows[z]["CeoName"].ToString();
                    Regfp.CeoGender = dt.Rows[z]["CeoGender"].ToString();
                    Regfp.CeoMobile = dt.Rows[z]["CeoMobile"].ToString().Trim();
                    Regfp.CeoLandline = dt.Rows[z]["CeoLandline"].ToString();
                    Regfp.CeoEmail = dt.Rows[z]["CeoEmail"].ToString();
                    Regfp.CeoAuthorisedPerson = dt.Rows[z]["CeoAuthorisedPerson"].ToString().Trim();
                    Regfp.CeoDesignation = dt.Rows[z]["CeoDesignation"].ToString();
                    Regfp.AuthorisedCertifecate = dt.Rows[z]["CeoProofOfAuthorisation"].ToString();
                    Regfp.CeoAuthorisedPersonGen = dt.Rows[z]["CeoAuthorisedPersonGen"].ToString();
                    Regfp.CeoAuthorisedPersonMob = dt.Rows[z]["CeoAuthorisedPersonMob"].ToString();
                    Regfp.CeoAuthorisedPersonEmail = dt.Rows[z]["CeoAuthorisedPersonEmail"].ToString();
                    Regfp.Address1HouseNo = dt.Rows[z]["Address1HouseNo"].ToString();
                    Regfp.Address1StreetName = dt.Rows[z]["Address1StreetName"].ToString();
                    Regfp.Address1City_ID = dt.Rows[z]["Address1City_ID"].ToString();
                    Regfp.CityName = dt.Rows[z]["Cityname"].ToString();
                    Regfp.Address1TalukaID = dt.Rows[z]["Address1TalukaID"].ToString();
                    Regfp.Taluka = dt.Rows[z]["Taluka"].ToString();
                    Regfp.Address1Post_ID = dt.Rows[z]["Address1Post_ID"].ToString();
                    Regfp.PostName = dt.Rows[z]["PostName"].ToString();
                    Regfp.Address1PinCode = dt.Rows[z]["Address1PinCode"].ToString();
                    Regfp.Address1VillageID = dt.Rows[z]["Address1VillageID"].ToString();
                    Regfp.VillageName = dt.Rows[z]["VillageName"].ToString();
                    Regfp.Clusters = dt.Rows[z]["Clusters"].ToString();
                    Regfp.MobileNumber = dt.Rows[z]["MobileNumber"].ToString();
                    Regfp.MobileNumber2 = dt.Rows[z]["MobileNumber2"].ToString();
                    Regfp.LandLineNumber = dt.Rows[z]["LandLineNumber"].ToString();
                    Regfp.EmailID = dt.Rows[z]["EmailID"].ToString();
                    Regfp.PanNumber = dt.Rows[z]["PanNumber"].ToString();
                    Regfp.IsBothAddressSame = dt.Rows[z]["IsBothAddressSame"].ToString();
                    Regfp.Address2HouseNo = dt.Rows[z]["Address2HouseNo"].ToString();
                    Regfp.Address2StreetName = dt.Rows[z]["Address2StreetName"].ToString();
                    //  Regfp.Address2City_ID = dt.Rows[z]["Address2City_ID"].ToString();
                    //  Regfp.Address2TalukaID = dt.Rows[z]["Address2TalukaID"].ToString();
                    //   Regfp.Address2Post_ID = dt.Rows[z]["Address2Post_ID"].ToString();
                    Regfp.Address2PinCode = dt.Rows[z]["Address2PinCode"].ToString();
                    Regfp.Address2VillageID = dt.Rows[z]["Address2VillageID"].ToString();
                    Regfp.Address2Clusters = dt.Rows[z]["Clusters"].ToString();
                    Regfp.Address2Mob1 = dt.Rows[z]["Address2Mob1"].ToString();
                    Regfp.Address2Mob2 = dt.Rows[z]["Address2Mob2"].ToString();
                    Regfp.Address2LandLine = dt.Rows[z]["Address2LandLine"].ToString();
                    Regfp.ApprovalStatus = dt.Rows[z]["ApprovalStatus"].ToString();
                    Regfp.RegistrationID = dt.Rows[z]["RegistrationID"].ToString();
                    Regfp.RegisterUnder = dt.Rows[z]["RegisterUnder"].ToString();
                    Regfp.RegisteredThrough = dt.Rows[z]["RegisteredThrough"].ToString();
                    Regfp.BeneficiaryTypes = dt.Rows[z]["BeneficiaryTypes"].ToString();
                    Regfp.Address2City_ID = cla.GetExecuteScalar("SELECT   Cityname FROM Tbl_M_City where City_ID=" + dt.Rows[0]["Address2City_ID"].ToString() + "");//dis
                    Regfp.Address2TalukaID = cla.GetExecuteScalar("SELECT Taluka FROM Tbl_M_TalukaMaster where TalukaID=" + dt.Rows[0]["Address2TalukaID"].ToString() + "");//talu
                    Regfp.Address2Post_ID = cla.GetExecuteScalar("SELECT PostName FROM Tbl_M_CityWisePost where Post_ID=" + dt.Rows[0]["Address2Post_ID"].ToString() + "");//POST
                                                                                                                                                                           //  Label21.Text = dt.Rows[0]["Address2PinCode"].ToString();
                    Regfp.Address2VillageID = cla.GetExecuteScalar("SELECT VillageName FROM Tbl_M_VillageMaster where VillageID=" + dt.Rows[0]["Address2VillageID"].ToString() + "");//vill
                                                                                                                                                                                     //Regfp.Cluster = cla.GetExecuteScalar("SELECT VillageCode FROM Tbl_M_VillageMaster where VillageID=" + dt.Rows[0]["Address2VillageID"].ToString() + "");//vill



                    lst.Add(Regfp);
                }
            }
            catch (Exception ex)
            {
                FPOFPCProfDetails Regfp = new FPOFPCProfDetails();
                Regfp.Message = ex.ToString();

                lst.Add(Regfp);


            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());


        }

        #endregion

        #region'Community Profile Details'
        [WebMethod]

        public void CommunityProfileDetails(String SecurityKey, String RegistrationID)

        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<CommunityProDetails> lst = new List<CommunityProDetails>();

            if (MyCommanClassAPI.CheckApiAuthrization("35", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                CommunityProDetails d = new CommunityProDetails();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            try
            {


                List<String> frs = new List<String>();
                frs.Add(RegistrationID.Trim());
                frs.Add("");
                frs.Add("");
                frs.Add("");
                frs.Add("");
                frs.Add("");
                frs.Add("");
                frs.Add("");
                frs.Add("");

                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_CommunityRegistration_Details", frs);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    CommunityProDetails Reg = new CommunityProDetails();

                    Reg.GramPanchayatCode = dt.Rows[z]["GramPanchayatCode"].ToString().Trim();
                    Reg.Name = dt.Rows[z]["RegisterName"].ToString();
                    Reg.Gender = dt.Rows[z]["Gender"].ToString().Trim();
                    Reg.BeneficiaryTypesID = dt.Rows[z]["BeneficiaryTypesID"].ToString().Trim();
                    Reg.BeneficiaryTypes = dt.Rows[z]["BeneficiaryTypes"].ToString().Trim();
                    Reg.GramPanchayatMobile = dt.Rows[z]["GramPanchayatMobile"].ToString();//
                    Reg.GramPanchayatEmail = dt.Rows[z]["GramPanchayatEmail"].ToString();
                    Reg.MobileNumber = dt.Rows[z]["MobileNumber"].ToString().Trim();
                    Reg.MobileNumber2 = dt.Rows[z]["MobileNumber2"].ToString();
                    Reg.LandLineNumber = dt.Rows[z]["LandLineNumber"].ToString();
                    Reg.EmailId = dt.Rows[z]["EmailID"].ToString().Trim();
                    Reg.PanNumber = dt.Rows[z]["PanNumber"].ToString();
                    Reg.Address1VillageID = dt.Rows[z]["Address1VillageID"].ToString();
                    Reg.Address1PinCode = dt.Rows[z]["Address1PinCode"].ToString().Trim();
                    Reg.Address1HouseNo = dt.Rows[z]["Address1HouseNo"].ToString();
                    Reg.Address1StreetName = dt.Rows[z]["Address1StreetName"].ToString();
                    Reg.Address1City_ID = dt.Rows[z]["Address1City_ID"].ToString().Trim();
                    Reg.Address1TalukaID = dt.Rows[z]["Address1TalukaID"].ToString();
                    Reg.Address1Post_ID = dt.Rows[z]["Address1Post_ID"].ToString();
                    Reg.CLUSTERCODE = dt.Rows[z]["Clusters"].ToString();
                    Reg.ApprovalStatus = dt.Rows[z]["ApprovalStatus"].ToString();
                    Reg.RegistrationDate = dt.Rows[z]["RegistrationDate"].ToString();
                    Reg.Taluka = dt.Rows[z]["Taluka"].ToString();
                    Reg.CityName = dt.Rows[z]["Cityname"].ToString();
                    Reg.VillageName = dt.Rows[z]["VillageName"].ToString();
                    Reg.PostName = dt.Rows[z]["PostName"].ToString();
                    Reg.BankAccountNo = dt.Rows[0]["BankAccountNo"].ToString();
                    Reg.BankAccountHolder = dt.Rows[0]["BankAccountHolder"].ToString();
                    Reg.NameOFbank = dt.Rows[0]["NameOFbank"].ToString();
                    Reg.BranchName = dt.Rows[0]["BranchName"].ToString();
                    Reg.IFSCCode = dt.Rows[0]["IFSCCode"].ToString();//RBIBankID
                    Reg.RBIBankID = dt.Rows[0]["RBIBankID"].ToString();
                    Reg.RegistrationID = dt.Rows[0]["RegistrationID"].ToString();
                    lst.Add(Reg);

                }
            }
            catch (Exception ex)
            {
                CommunityProDetails Reg = new CommunityProDetails();
                Reg.Message = ex.ToString();

                lst.Add(Reg);


            }



            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }

        #endregion

        #region " Application Details(2PreDeskTwo)"

        [WebMethod]
        public void GetAPPLICATIONDetails(String SecurityKey, String ApplicationID)

        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<ApplicationDetails> lst = new List<ApplicationDetails>();

            if (MyCommanClassAPI.CheckApiAuthrization("55", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                ApplicationDetails d = new ApplicationDetails();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {

                List<String> apl = new List<String>();
                apl.Add(ApplicationID.Trim());

                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_Get_ApplicationDetails", apl);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    ApplicationDetails Sts = new ApplicationDetails();

                    Sts.ApplicationID = dt.Rows[z]["ApplicationID"].ToString().Trim();
                    Sts.Date = dt.Rows[z]["ApplicationDate"].ToString();
                    Sts.RegisterName = dt.Rows[z]["RegisterName"].ToString().Trim();
                    Sts.Activity = dt.Rows[z]["ActivityName"].ToString();
                    Sts.ActivityCode = dt.Rows[z]["ActivityCode"].ToString().Trim();
                    Sts.Component = dt.Rows[z]["ComponentName"].ToString();
                    Sts.SubComponentName = dt.Rows[z]["SubComponentName"].ToString();
                    Sts.ActivityImagePath = dt.Rows[z]["ActivityImagePath"].ToString();
                    Sts.BalanceFundSource = dt.Rows[z]["BalanceFundSource"].ToString();
                    Sts.BeneficiaryTypes = dt.Rows[z]["BeneficiaryTypes"].ToString();
                    Sts.ApprovalStages = dt.Rows[z]["ApprovalStages"].ToString().Trim();
                    Sts.APPStatus = dt.Rows[z]["ApplicationStatus"].ToString();
                    Sts.RegistrationID = dt.Rows[z]["RegistrationID"].ToString().Trim();
                    Sts.PastBenefitHistory = dt.Rows[z]["PastBenefitHistory"].ToString();
                    Sts.Hectare8A = dt.Rows[z]["Hectare8A"].ToString();
                    Sts.SURVEYNo712 = dt.Rows[z]["SURVEYNo712"].ToString();
                    Sts.Hectare712 = dt.Rows[z]["Hectare712"].ToString().Trim();
                    Sts.Are712 = dt.Rows[z]["Are712"].ToString();
                    Sts.LANDVILLAGE = dt.Rows[z]["LandVillage"].ToString().Trim();

                    // if not link check it   
                    String BanklinkStatus = dt.Rows[z]["BanklinkStatus"].ToString().Trim();
                    if (dt.Rows[z]["BanklinkStatus"].ToString().Trim() != "Linked")
                    {
                        BanklinkStatus = NPCIClass.UpdateAdharStatusDetailsAPI(dt.Rows[z]["RegistrationID"].ToString().Trim());
                    }
                    Sts.BanklinkStatus = BanklinkStatus;

                    lst.Add(Sts);
                }
            }
            catch (Exception ex)
            {
                ApplicationDetails Sts = new ApplicationDetails();
                Sts.Message = ex.ToString();

                lst.Add(Sts);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }


        #endregion


        #region'BackToBenificiary PendingReasons  REGISTRATION'
        [WebMethod]
        public void GetPendingReasons(String SecurityKey, String RegistrationID)
        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<BackToBenefic> lst = new List<BackToBenefic>();

            if (MyCommanClassAPI.CheckApiAuthrization("63", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                BackToBenefic d = new BackToBenefic();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {
                List<String> fst = new List<string>();

                // DataTable dt2 = cla.GetDataTable("SELECT R_1.Reasons,R_1.ReasonID FROM  Tbl_M_RegistrationDetails AS R INNER JOIN Tbl_M_Registration_Log AS L ON R.RegistrationID = L.RegistrationID INNER JOIN Tbl_M_Registration_LogReason AS S ON L.RegistrationLogID = S.RegistrationLogID INNER JOIN Tbl_M_Reason AS R_1 ON S.ReasonID = R_1.ReasonID WHERE R.RegistrationID =" + RegistrationID.Trim() + " AND R.IsDeleted IS NULL ");//AND R.ApprovalStatus = 'Back To Beneficiary'
                DataTable dt2 = cla.GetDataTable(" SELECT R_1.Reasons ,R_1.ReasonID FROM  Tbl_M_RegistrationDetails AS R INNER JOIN Tbl_M_Registration_Log AS L ON R.RegistrationID = L.RegistrationID INNER JOIN Tbl_M_Registration_LogReason AS S ON L.RegistrationLogID = S.RegistrationLogID INNER JOIN Tbl_M_Reason AS R_1 ON S.ReasonID = R_1.ReasonID WHERE R.RegistrationID =" + RegistrationID.Trim() + " and L.RegistrationLogID = (select top 1 RegistrationLogID from Tbl_M_Registration_Log where RegistrationID =" + RegistrationID.Trim() + " order by RegistrationLogID desc)");
                for (int x = 0; x != dt2.Rows.Count; x++)
                {

                    BackToBenefic btb = new BackToBenefic();
                    btb.ReasonID = dt2.Rows[x]["ReasonID"].ToString().Trim();
                    btb.Reasons = dt2.Rows[x]["Reasons"].ToString().Trim();


                    lst.Add(btb);

                }


            }
            catch (Exception ex)
            {
                BackToBenefic btb = new BackToBenefic();
                btb.Message = ex.ToString();
                lst.Add(btb);


            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }

        #endregion

        //------------validations ---------------------// 

        [WebMethod]
        public void UpdatePreSanctionDesk2(String SecurityKey, String ApplicationID, String LogDetails, String ApplicationStatusID, String ApprovalStageID, String UpdateByRegID, String ReasonID, String FeasibilityID)//, String Remarkany
        {
            List<clsReturnMessage> lst = new List<clsReturnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            String UpdateOnDate = cla.SvrDate();
            if (MyCommanClassAPI.CheckApiAuthrization("68", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //------------validations ---------------------// 



            if (ApplicationStatusID.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select  ApplicationStatus";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            if (cla.GetExecuteScalar("Select top 1 GeofencingID from Tbl_T_ApplicationGeofencing where ApplicationID=" + ApplicationID.Trim() + " and IsDeleted is null ").Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please fill Feasibility image using mobile App";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }


            String str = "";
            int ApplicationLogID = cla.TableID("Tbl_T_ApplicationDetails_Log", "ApplicationLogID");
            int ApplicationLogReasonID = cla.TableID("Tbl_T_ApplicationDetails_LogReason", "ApplicationLogReasonID");
            int AppFeasibilityID = cla.TableID("Tbl_T_ApplicationFeasibility", "AppFeasibilityID");

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

                    //   String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + "RegistrationID".ToString().Trim() + "");


                    // UPDATE
                    str = " INSERT INTO Tbl_T_ApplicationDetails_Log (ApplicationLogID, ApplicationID, LogDetails, ApplicationStatusID, ApprovalStageID, UpdateByRegID)";
                    str += " VALUES(" + ApplicationLogID + "," + ApplicationID.Trim() + ",'" + LogDetails.Trim() + "'," + ApplicationStatusID.Trim() + ",3," + UpdateByRegID.ToString().Trim() + ")";//" + Session["UpdateByRegID"].ToString() + "
                    cla.ExecuteCommand(str, command);
                    //
                    if (ReasonID.ToString().Length == 0)
                    {
                        str = "";
                    }
                    else
                    {

                        String[] Reas = ReasonID.Trim().Split('/');
                        for (int x = 0; x != Reas.Length; x++)
                        {
                            ReasonID = Reas[x];

                            str = " INSERT INTO Tbl_T_ApplicationDetails_LogReason (ApplicationLogReasonID, ApplicationLogID, ReasonID)";
                            str += " VALUES(" + ApplicationLogReasonID + "," + ApplicationLogID + "," + ReasonID.Trim() + ")";
                            cla.ExecuteCommand(str, command);
                            ApplicationLogReasonID++;

                        }
                    }
                    if (FeasibilityID.ToString().Length == 0)
                    {
                        str = "";
                    }
                    else
                    {


                        String[] Feas = FeasibilityID.Trim().Split('/');
                        for (int x = 0; x != Feas.Length; x++)
                        {
                            FeasibilityID = Feas[x];

                            str = " INSERT INTO Tbl_T_ApplicationFeasibility (AppFeasibilityID, ApplicationLogID, FeasibilityRptID)";
                            str += " VALUES(" + AppFeasibilityID + "," + ApplicationLogID + "," + FeasibilityID.Trim() + ")";
                            cla.ExecuteCommand(str, command);
                            AppFeasibilityID++;
                        }
                    }
                    if (ApplicationStatusID.Trim() == "5")
                    {
                        str = " UPDATE Tbl_T_ApplicationDetails SET ApprovalStageID=4   , ApplicationStatusID=1  WHERE ApplicationID=" + ApplicationID.Trim() + "";
                    }
                    else
                    {
                        str = " UPDATE Tbl_T_ApplicationDetails SET ApprovalStageID=3   , ApplicationStatusID='" + ApplicationStatusID.Trim() + "'  WHERE ApplicationID=" + ApplicationID.Trim() + "";
                    }

                    cla.ExecuteCommand(str, command);

                    transaction.Commit();
                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Sucess";
                    d.RegistrationID = UpdateByRegID.ToString();
                    d.Message = "Record Saved Sucessfully";
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

                    clsReturnMessage d = new clsReturnMessage();
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

        #region'UPLOAD IMAGE Pre Sanction Desk-2'

        [WebMethod]
        public void GetUploadImagePreSanctionDesk2(String SecurityKey, String ApplicationID, String Lang)
        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<UploadImage> lst = new List<UploadImage>();

            if (MyCommanClassAPI.CheckApiAuthrization("69", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                UploadImage d = new UploadImage();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {

                List<String> apl = new List<String>();

                dt = new DataTable();
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable(" SELECT A.GeofencingID, T.ImageTitleName ,  A.LatitudeMap, A.LongitudeMap, A.Remarks , A.CoordinateAddress, A.CoordinateImage, B.FullName,'<a target=_blank href=Popup/ShowMap.aspx?A='+A.LatitudeMap+'&B='+ A.LongitudeMap+'> View On Map </a>' as onMap FROM dbo.Tbl_T_ApplicationGeofencing AS A INNER JOIN dbo.Tbl_M_LoginDetails AS B ON A.UpdateByRegID = B.UserId  Inner join Tbl_M_ImageTitle T on A.ImageTitleID = T.ImageTitleID  WHERE A.ApplicationID = " + ApplicationID.Trim() + " AND A.IsDeleted IS NULL");
                else
                {
                    dt = cla.GetDataTable(" SELECT A.GeofencingID, ISNULL(T.ImageTitleName,T.ImageTitleMr) as ImageTitleName ,  A.LatitudeMap, A.LongitudeMap, A.Remarks , A.CoordinateAddress, A.CoordinateImage, B.FullName,'<a target=_blank href=Popup/ShowMap.aspx?A='+A.LatitudeMap+'&B='+ A.LongitudeMap+'> View On Map </a>' as onMap FROM dbo.Tbl_T_ApplicationGeofencing AS A INNER JOIN dbo.Tbl_M_LoginDetails AS B ON A.UpdateByRegID = B.UserId  Inner join Tbl_M_ImageTitle T on A.ImageTitleID = T.ImageTitleID  WHERE A.ApplicationID = " + ApplicationID.Trim() + " AND A.IsDeleted IS NULL");
                }
                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    UploadImage Sts = new UploadImage();
                    Sts.GeofencingID = dt.Rows[z]["GeofencingID"].ToString().Trim();
                    Sts.ImageTitleName = dt.Rows[z]["ImageTitleName"].ToString().Trim();
                    Sts.LatitudeMap = dt.Rows[z]["LatitudeMap"].ToString().Trim();
                    Sts.LongitudeMap = dt.Rows[z]["LongitudeMap"].ToString().Trim();
                    Sts.Remarks = dt.Rows[z]["Remarks"].ToString().Trim();
                    Sts.CoordinateAddress = dt.Rows[z]["CoordinateAddress"].ToString().Trim();
                    Sts.CoordinateImage = dt.Rows[z]["CoordinateImage"].ToString().Trim();
                    Sts.FullName = dt.Rows[z]["FullName"].ToString().Trim();
                    Sts.onMap = dt.Rows[z]["onMap"].ToString().Trim();


                    lst.Add(Sts);
                }
            }
            catch (Exception ex)
            {
                UploadImage Sts = new UploadImage();
                Sts.Message = ex.ToString();

                lst.Add(Sts);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }




        #endregion

        //#region 'Sanction Desk-4'
        //[WebMethod]
        //public void GetDataSanctionDesk4(String SecurityKey, String UserId ,String FromRegistrationDate, String ToRegistrationDate ,String ApprovalStatus, String ComponentID, String SubComponentID, String ActivityCategoryID, String ActivityID)
        //{
        //    DataTable dt = new DataTable();

        //    MyClass cla = new MyClass();
        //    List<DataSanctionDesk> lst = new List<DataSanctionDesk>();
        //    if (MyCommanClassAPI.CheckApiAuthrization("79", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
        //    {
        //        DataSanctionDesk d = new DataSanctionDesk();
        //        //   d.Message = "Authorization Failed";

        //        Context.Response.Clear();
        //        Context.Response.ContentType = "application/json";
        //        //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
        //        Context.Response.Flush();

        //        Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
        //        return;
        //    }
        //    //------------validations ---------------------// 


        //    try
        //    {

        //        List<String> apl = new List<String>();
        //        apl.Add("");//RegistrationID.Trim() 
        //        apl.Add(cla.mdy(FromRegistrationDate.Trim()));
        //        apl.Add(cla.mdy(ToRegistrationDate.Trim()));
        //        apl.Add(ApprovalStatus.Trim());
        //        apl.Add(ComponentID.Trim());
        //        apl.Add(SubComponentID.Trim());
        //        apl.Add(ActivityCategoryID.Trim());
        //        apl.Add(ActivityID.Trim());
        //        apl.Add("6");
        //        apl.Add(UserId.Trim());
        //        dt = new DataTable();
        //        dt = cla.GetDtByProcedure("SP_Get_ApplicationSecGrdData", apl);

        //        for (int z = 0; z != dt.Rows.Count; z++)
        //        {
        //            DataSanctionDesk Sts = new DataSanctionDesk();

        //            Sts.RegistrationID = dt.Rows[z]["RegistrationID"].ToString().Trim();
        //            Sts.ApplicationID = dt.Rows[z]["ApplicationID"].ToString().Trim();
        //            Sts.BeneficiaryTypesID = dt.Rows[z]["BeneficiaryTypesID"].ToString().Trim();
        //            Sts.BeneficiaryTypes = dt.Rows[z]["BeneficiaryTypes"].ToString().Trim();
        //            Sts.ApplicationDate = dt.Rows[z]["ApplicationDate"].ToString().Trim();
        //            Sts.RegisterName = dt.Rows[z]["RegisterName"].ToString().Trim();
        //            Sts.Activity = dt.Rows[z]["ActivityName"].ToString().Trim();
        //            Sts.ActivityCode = dt.Rows[z]["ActivityCode"].ToString().Trim();
        //            Sts.ComponentName = dt.Rows[z]["ComponentName"].ToString().Trim();
        //            Sts.SubComponentName = dt.Rows[z]["SubComponentName"].ToString().Trim();
        //            Sts.ApplicationStatus = dt.Rows[z]["ApplicationStatus"].ToString().Trim();
        //            Sts.RequestNumber = dt.Rows[z]["RequestNo"].ToString().Trim();
        //            Sts.RequestDate = dt.Rows[z]["RequestDate"].ToString().Trim();
        //            Sts.RequestAmount = dt.Rows[z]["TotalAmtByBen"].ToString().Trim();
        //            Sts.WorkReportID = dt.Rows[z]["WorkReportID"].ToString().Trim();
        //            Sts.InspectorAmount = dt.Rows[z]["TotalAmtByVCRMC"].ToString().Trim();
        //            Sts.ApprovalStages = dt.Rows[z]["ApprovalStages"].ToString().Trim();
        //          //  lst.Add(Sts);
        //            //   }
        //            DataTable dtDestinaton = cla.GetDataTable("SELECT  isnull(Max(LatitudeMap),0) as LatitudeMap,Isnull(max(LongitudeMap),0) as LongitudeMap FROM Tbl_T_Application_WorkCompletions where ApplicationID=" + dt.Rows[z]["ApplicationID"].ToString() + " and IsInspection is not null ");
        //            DataTable dtsorce = cla.GetDataTable("SELECT  isnull(Max(LatitudeMap),0) as LatitudeMap,Isnull(max(LongitudeMap),0) as LongitudeMap FROM Tbl_T_ApplicationGeofencing where ApplicationID=" + dt.Rows[z]["ApplicationID"].ToString() + "");
        //            if (dtDestinaton.Rows.Count > 0)
        //            {
        //                if (dtsorce.Rows.Count > 0)
        //                {
        //                    var distance = new Coordinates(Convert.ToDouble(dtsorce.Rows[0]["LatitudeMap"].ToString()), Convert.ToDouble(dtsorce.Rows[0]["LongitudeMap"].ToString())).DistanceTo(new Coordinates(Convert.ToDouble(dtDestinaton.Rows[0]["LatitudeMap"].ToString()), Convert.ToDouble(dtDestinaton.Rows[0]["LongitudeMap"].ToString())), UnitOfLength.Kilometers);
        //                    if (distance.ToString().Length > 0)
        //                        Sts.CoordinatesDifference = "(Feasibility and Inspection) :: " + (Convert.ToDouble(distance) * 1000).ToString("0.00");

        //                }
        //            }
        //            ////


        //           // String 
        //                ActivityID = cla.GetExecuteScalar("Select ActivityID from Tbl_T_ApplicationDetails  where ApplicationID=" + dt.Rows[z]["ApplicationID"].ToString() + "");
        //            Sts.InsPectionDoc = "/admintrans/Popup/InspDoc/" + cla.GetExecuteScalar("SELECT  InpectionDocTypeUrl FROM Tbl_M_InpectionDocType where InpectionDocTypeID=(Select InpectionDocTypeID from Tbl_M_ActivityMaster  where ActivityID=" + ActivityID + ")") + "?ID=" + dt.Rows[z]["ApplicationID"].ToString().Trim() + "";

        //            ////
        //            lst.Add(Sts);//
        //        }//
        //    }
        //    catch (Exception ex)
        //    {
        //        DataSanctionDesk Sts = new DataSanctionDesk();
        //        Sts.Message = ex.ToString();

        //        lst.Add(Sts);
        //    }

        //    Context.Response.Clear();
        //    Context.Response.ContentType = "application/json";
        //    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
        //    Context.Response.Flush();
        //    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        //}




        //#endregion

        //#region 'Sanction Desk-4 With Paging'
        //[WebMethod]
        //public void GetDataSanctionDesk4_withPaging(String SecurityKey, String UserId, String FromRegistrationDate, String ToRegistrationDate, String ApprovalStatus, String ComponentID, String SubComponentID, String ActivityCategoryID, String ActivityID, String PageSize, String PageNumber)
        //{
        //    DataTable dt = new DataTable();

        //    MyClass cla = new MyClass();
        //    List<DataSanctionDesk> lst = new List<DataSanctionDesk>();
        //    if (MyCommanClassAPI.CheckApiAuthrization("79", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
        //    {
        //        DataSanctionDesk d = new DataSanctionDesk();
        //        //   d.Message = "Authorization Failed";

        //        Context.Response.Clear();
        //        Context.Response.ContentType = "application/json";
        //        //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
        //        Context.Response.Flush();

        //        Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
        //        return;
        //    }
        //    //------------validations ---------------------// 


        //    try
        //    {

        //        int intPageSize = 10;
        //        int intPageNumber = 1;

        //        int.TryParse(PageSize, out intPageSize);
        //        int.TryParse(PageNumber, out intPageNumber);

        //        List<String> apl = new List<String>();
        //        apl.Add("");//RegistrationID.Trim() 
        //        apl.Add(cla.mdy(FromRegistrationDate.Trim()));
        //        apl.Add(cla.mdy(ToRegistrationDate.Trim()));
        //        apl.Add(ApprovalStatus.Trim());
        //        apl.Add(ComponentID.Trim());
        //        apl.Add(SubComponentID.Trim());
        //        apl.Add(ActivityCategoryID.Trim());
        //        apl.Add(ActivityID.Trim());
        //        apl.Add("6");
        //        apl.Add(UserId.Trim());
        //        apl.Add(intPageSize.ToString());
        //        apl.Add(intPageNumber.ToString());
        //        dt = new DataTable();
        //        dt = cla.GetDtByProcedure("SP_Get_ApplicationSecGrdData_WithPaging", apl);

        //        for (int z = 0; z != dt.Rows.Count; z++)
        //        {
        //            DataSanctionDesk Sts = new DataSanctionDesk();

        //            Sts.RegistrationID = dt.Rows[z]["RegistrationID"].ToString().Trim();
        //            Sts.ApplicationID = dt.Rows[z]["ApplicationID"].ToString().Trim();
        //            Sts.BeneficiaryTypesID = dt.Rows[z]["BeneficiaryTypesID"].ToString().Trim();
        //            Sts.BeneficiaryTypes = dt.Rows[z]["BeneficiaryTypes"].ToString().Trim();
        //            Sts.ApplicationDate = dt.Rows[z]["ApplicationDate"].ToString().Trim();
        //            Sts.RegisterName = dt.Rows[z]["RegisterName"].ToString().Trim();
        //            Sts.Activity = dt.Rows[z]["ActivityName"].ToString().Trim();
        //            Sts.ActivityCode = dt.Rows[z]["ActivityCode"].ToString().Trim();
        //            Sts.ComponentName = dt.Rows[z]["ComponentName"].ToString().Trim();
        //            Sts.SubComponentName = dt.Rows[z]["SubComponentName"].ToString().Trim();
        //            Sts.ApplicationStatus = dt.Rows[z]["ApplicationStatus"].ToString().Trim();
        //            Sts.RequestNumber = dt.Rows[z]["RequestNo"].ToString().Trim();
        //            Sts.RequestDate = dt.Rows[z]["RequestDate"].ToString().Trim();
        //            Sts.RequestAmount = dt.Rows[z]["TotalAmtByBen"].ToString().Trim();
        //            Sts.WorkReportID = dt.Rows[z]["WorkReportID"].ToString().Trim();
        //            Sts.InspectorAmount = dt.Rows[z]["TotalAmtByVCRMC"].ToString().Trim();
        //            Sts.ApprovalStages = dt.Rows[z]["ApprovalStages"].ToString().Trim();
        //            //  lst.Add(Sts);
        //            //   }
        //            DataTable dtDestinaton = cla.GetDataTable("SELECT  isnull(Max(LatitudeMap),0) as LatitudeMap,Isnull(max(LongitudeMap),0) as LongitudeMap FROM Tbl_T_Application_WorkCompletions where ApplicationID=" + dt.Rows[z]["ApplicationID"].ToString() + " and IsInspection is not null ");
        //            DataTable dtsorce = cla.GetDataTable("SELECT  isnull(Max(LatitudeMap),0) as LatitudeMap,Isnull(max(LongitudeMap),0) as LongitudeMap FROM Tbl_T_ApplicationGeofencing where ApplicationID=" + dt.Rows[z]["ApplicationID"].ToString() + "");
        //            if (dtDestinaton.Rows.Count > 0)
        //            {
        //                if (dtsorce.Rows.Count > 0)
        //                {
        //                    var distance = new Coordinates(Convert.ToDouble(dtsorce.Rows[0]["LatitudeMap"].ToString()), Convert.ToDouble(dtsorce.Rows[0]["LongitudeMap"].ToString())).DistanceTo(new Coordinates(Convert.ToDouble(dtDestinaton.Rows[0]["LatitudeMap"].ToString()), Convert.ToDouble(dtDestinaton.Rows[0]["LongitudeMap"].ToString())), UnitOfLength.Kilometers);
        //                    if (distance.ToString().Length > 0)
        //                        Sts.CoordinatesDifference = "(Feasibility and Inspection) :: " + (Convert.ToDouble(distance) * 1000).ToString("0.00");

        //                }
        //            }
        //            ////


        //            // String 
        //            ActivityID = cla.GetExecuteScalar("Select ActivityID from Tbl_T_ApplicationDetails  where ApplicationID=" + dt.Rows[z]["ApplicationID"].ToString() + "");
        //            Sts.InsPectionDoc = "/admintrans/Popup/InspDoc/" + cla.GetExecuteScalar("SELECT  InpectionDocTypeUrl FROM Tbl_M_InpectionDocType where InpectionDocTypeID=(Select InpectionDocTypeID from Tbl_M_ActivityMaster  where ActivityID=" + ActivityID + ")") + "?ID=" + dt.Rows[z]["ApplicationID"].ToString().Trim() + "";

        //            ////
        //            lst.Add(Sts);//
        //        }//
        //    }
        //    catch (Exception ex)
        //    {
        //        DataSanctionDesk Sts = new DataSanctionDesk();
        //        Sts.Message = ex.ToString();

        //        lst.Add(Sts);
        //    }

        //    Context.Response.Clear();
        //    Context.Response.ContentType = "application/json";
        //    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
        //    Context.Response.Flush();
        //    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        //}
        //#endregion

        #region 'Sanction Desk-4 With Paging'
        [WebMethod]
        public void GetDataSanctionDesk4_Paging(String SecurityKey, String UserId, String FromRegistrationDate, String ToRegistrationDate, String ApprovalStatus, String ComponentID, String SubComponentID, String ActivityCategoryID, String ActivityID, String GPCode, String BeneficiaryName, String PageSize, String PageNumber)
        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<DataSanctionDesk> lst = new List<DataSanctionDesk>();
            if (MyCommanClassAPI.CheckApiAuthrization("79", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                DataSanctionDesk d = new DataSanctionDesk();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {

                int intPageSize = 10;
                int intPageNumber = 1;

                int.TryParse(PageSize, out intPageSize);
                int.TryParse(PageNumber, out intPageNumber);

                List<String> apl = new List<String>();
                apl.Add("");//RegistrationID.Trim() 
                apl.Add(cla.mdy(FromRegistrationDate.Trim()));
                apl.Add(cla.mdy(ToRegistrationDate.Trim()));
                apl.Add(ApprovalStatus.Trim());
                apl.Add(ComponentID.Trim());
                apl.Add(SubComponentID.Trim());
                apl.Add(ActivityCategoryID.Trim());
                apl.Add(ActivityID.Trim());
                apl.Add("6");
                apl.Add(UserId.Trim());
                apl.Add(GPCode.Trim());
                apl.Add(BeneficiaryName.Trim());
                apl.Add(intPageSize.ToString());
                apl.Add(intPageNumber.ToString());
                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_Get_ApplicationSecGrdData_Paging", apl);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    DataSanctionDesk Sts = new DataSanctionDesk();

                    Sts.RegistrationID = dt.Rows[z]["RegistrationID"].ToString().Trim();
                    Sts.ApplicationID = dt.Rows[z]["ApplicationID"].ToString().Trim();
                    Sts.BeneficiaryTypesID = dt.Rows[z]["BeneficiaryTypesID"].ToString().Trim();
                    Sts.BeneficiaryTypes = dt.Rows[z]["BeneficiaryTypes"].ToString().Trim();
                    Sts.ApplicationDate = dt.Rows[z]["ApplicationDate"].ToString().Trim();
                    Sts.RegisterName = dt.Rows[z]["RegisterName"].ToString().Trim();
                    Sts.Activity = dt.Rows[z]["ActivityName"].ToString().Trim();
                    Sts.ActivityCode = dt.Rows[z]["ActivityCode"].ToString().Trim();
                    Sts.ComponentName = dt.Rows[z]["ComponentName"].ToString().Trim();
                    Sts.SubComponentName = dt.Rows[z]["SubComponentName"].ToString().Trim();
                    Sts.ApplicationStatus = dt.Rows[z]["ApplicationStatus"].ToString().Trim();
                    Sts.RequestNumber = dt.Rows[z]["RequestNo"].ToString().Trim();
                    Sts.RequestDate = dt.Rows[z]["RequestDate"].ToString().Trim();
                    Sts.RequestAmount = dt.Rows[z]["TotalAmtByBen"].ToString().Trim();
                    Sts.WorkReportID = dt.Rows[z]["WorkReportID"].ToString().Trim();
                    Sts.InspectorAmount = dt.Rows[z]["TotalAmtByVCRMC"].ToString().Trim();
                    Sts.ApprovalStages = dt.Rows[z]["ApprovalStages"].ToString().Trim();
                    Sts.AadharlinkStatus = dt.Rows[z]["AadharlinkStatus"].ToString();
                    Sts.PopupLink = dt.Rows[z]["PopupLink"].ToString();
                    Sts.Notes = dt.Rows[z]["Notes"].ToString();
                    Sts.IsShoweThibak = dt.Rows[z]["IsShoweThibak"].ToString();

                    Sts.CanCulateOnPercent = dt.Rows[z]["CanCulateOnPercent"].ToString();
                    // if total land > 2 then 75 else 80

                    // if not link check it 
                    //if(Sts.AadharlinkStatus.Trim()!= "Linked")
                    //{
                    //    Sts.AadharlinkStatus=NPCIClass.UpdateAdharStatusDetailsAPI(dt.Rows[z]["RegistrationID"].ToString().Trim());
                    //}


                    DataTable dtDestinaton = cla.GetDataTable("SELECT  isnull(Max(LatitudeMap),0) as LatitudeMap,Isnull(max(LongitudeMap),0) as LongitudeMap FROM Tbl_T_Application_WorkCompletions where ApplicationID=" + dt.Rows[z]["ApplicationID"].ToString() + " and IsInspection is not null ");
                    DataTable dtsorce = cla.GetDataTable("SELECT  isnull(Max(LatitudeMap),0) as LatitudeMap,Isnull(max(LongitudeMap),0) as LongitudeMap FROM Tbl_T_ApplicationGeofencing where ApplicationID=" + dt.Rows[z]["ApplicationID"].ToString() + "");
                    if (dtDestinaton.Rows.Count > 0)
                    {
                        if (dtsorce.Rows.Count > 0)
                        {
                            try
                            {
                                var distance = new Coordinates(Convert.ToDouble(dtsorce.Rows[0]["LatitudeMap"].ToString()), Convert.ToDouble(dtsorce.Rows[0]["LongitudeMap"].ToString())).DistanceTo(new Coordinates(Convert.ToDouble(dtDestinaton.Rows[0]["LatitudeMap"].ToString()), Convert.ToDouble(dtDestinaton.Rows[0]["LongitudeMap"].ToString())), UnitOfLength.Kilometers);
                                if (distance.ToString().Length > 0)
                                    Sts.CoordinatesDifference = "(Feasibility and Inspection) :: " + (Convert.ToDouble(distance) * 1000).ToString("0.00");
                            }
                            catch
                            {
                                Sts.CoordinatesDifference = "Incorrect Data";
                            }

                        }
                    }

                    // String 
                    ActivityID = dt.Rows[z]["ActivityID"].ToString().Trim();// cla.GetExecuteScalar("Select ActivityID from Tbl_T_ApplicationDetails  where ApplicationID=" + dt.Rows[z]["ApplicationID"].ToString() + "");
                    Sts.InsPectionDoc = "/admintrans/Popup/InspDoc/" + cla.GetExecuteScalar("SELECT  InpectionDocTypeUrl FROM Tbl_M_InpectionDocType where InpectionDocTypeID=(Select InpectionDocTypeID from Tbl_M_ActivityMaster  where ActivityID=" + ActivityID + ")") + "?ID=" + dt.Rows[z]["ApplicationID"].ToString().Trim() + "";

                    List<ActivityPaymentTerm> p = new List<ActivityPaymentTerm>();
                    dtDestinaton = cla.GetDataTable("Select a.PaymentTermName , p.SubsidyAmtComm  from Tbl_M_ActivityPaymentTerm as P inner join Tbl_M_PaymentTerm a on a.PaymentTermID=p.PaymentTermID where P.IsDeleted is null and P.ActivityID=" + ActivityID + "  ");
                    if (dtDestinaton.Rows.Count > 0)
                    {
                        for (int x = 0; x != dtDestinaton.Rows.Count; x++)
                        {
                            ActivityPaymentTerm a = new ActivityPaymentTerm();
                            a.PaymentTerm = dtDestinaton.Rows[x]["PaymentTermName"].ToString();
                            a.Amount = dtDestinaton.Rows[x]["SubsidyAmtComm"].ToString();
                            a.Notes = "";
                            p.Add(a);
                        }


                    }
                    Sts.PaymentTerm = p;
                    lst.Add(Sts);//
                }//
            }
            catch (Exception ex)
            {
                DataSanctionDesk Sts = new DataSanctionDesk();
                Sts.Message = ex.ToString();

                lst.Add(Sts);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }




        #endregion

        #region 'Activity Wise Subsidy'
        [WebMethod]

        public void GetActivityWiseSubsidy(String SecurityKey, String ActivityID)
        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<ActivityDetails> lst = new List<ActivityDetails>();

            if (MyCommanClassAPI.CheckApiAuthrization("61", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                ActivityDetails d = new ActivityDetails();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            try
            {


                List<String> frs = new List<String>();
                frs.Add("");
                frs.Add(ActivityID.Trim());
                frs.Add("");
                frs.Add("");
                frs.Add("");
                frs.Add("");
                frs.Add("");
                frs.Add("");
                frs.Add("");
                frs.Add("S");

                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_ActivityPaymentTerms", frs);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    ActivityDetails Reg = new ActivityDetails();
                    Reg.PaymentTermName = dt.Rows[z]["PaymentTermName"].ToString();
                    Reg.SubsidyPerComm = dt.Rows[z]["SubsidyPerComm"].ToString().Trim();
                    Reg.SubsidyAmtComm = dt.Rows[z]["SubsidyAmtComm"].ToString();// + "  (  " + dt.Rows[z]["CommanNote"].ToString();
                    Reg.SubsidyPerScST = dt.Rows[z]["SubsidyPerScST"].ToString();
                    Reg.SubsidyAmtScST = dt.Rows[z]["SubsidyAmtScST"].ToString().Trim();// + "   ---Note : --   " + dt.Rows[z]["SCSTNote"].ToString();
                    //   Reg.SubsidyPerWoman = dt.Rows[z]["SubsidyPerWoman"].ToString().Trim();
                    //   Reg.SubsidyAmtWoman = dt.Rows[z]["SubsidyAmtWoman"].ToString();//

                    lst.Add(Reg);


                }
            }
            catch (Exception ex)
            {
                ActivityDetails Reg = new ActivityDetails();
                Reg.Message = ex.ToString();

                lst.Add(Reg);


            }



            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }


        #endregion

        #region'Activity Wise Requirements'

        //[WebMethod]
        //public void GetActivityWiseRequirementEDF(String SecurityKey, String ActivityID)

        //{
        //    DataTable dt = new DataTable();

        //    MyClass cla = new MyClass();
        //    List<ActivityRequired> lst = new List<ActivityRequired>();

        //    if (MyCommanClassAPI.CheckApiAuthrization("60", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
        //    {
        //        ActivityRequired d = new ActivityRequired();
        //        //   d.Message = "Authorization Failed";

        //        Context.Response.Clear();
        //        Context.Response.ContentType = "application/json";
        //        //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
        //        Context.Response.Flush();

        //        Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
        //        return;
        //    }
        //    //------------validations ---------------------// 


        //    try
        //    {

        //        List<String> frs = new List<String>();

        //        DataTable dtActivity = new DataTable();
        //        dtActivity = cla.GetDataTable("SELECT  ActivityID   ,ActivityName    ,ActivityCode   FROM Tbl_M_ActivityMaster  where ActivityID=" + @ActivityID.Trim() + " AND IsDeleted Is NULL");

        //        for (int q = 0; q != dtActivity.Rows.Count; q++)
        //        {
        //            ActivityRequired Reg = new ActivityRequired();
        //            Reg.ActivityName = dtActivity.Rows[q]["ActivityName"].ToString();
        //            Reg.ActivityCode = dtActivity.Rows[q]["ActivityCode"].ToString();

        //            List<Eligibility> k = new List<Eligibility>();
        //            List<Requirement> l = new List<Requirement>();
        //            List<Feasibility> m = new List<Feasibility>();


        //            frs.Add("");
        //            frs.Add(ActivityID.Trim());
        //            frs.Add("");
        //            frs.Add("S");
        //            dt = new DataTable();
        //            dt = cla.GetDtByProcedure("SP_InserUpdateActivityEligibility", frs);

        //            for (int z = 0; z != dt.Rows.Count; z++)
        //            {
        //                Eligibility Elg = new Eligibility();
        //                Elg.EligibilityCriateria = dt.Rows[z]["EligibilityCriteria"].ToString();
        //                k.Add(Elg);

        //            }
        //            // frs.Clear();//
        //            frs.Add("");
        //            frs.Add(ActivityID.Trim());
        //            frs.Add("");
        //            frs.Add("S");
        //            DataTable dtRequired = cla.GetDtByProcedure("SP_InserUpdateActivityRequired", frs);

        //            for (int x = 0; x != dtRequired.Rows.Count; x++)
        //            {
        //                Requirement req = new Requirement();
        //                req.DocumentDetails = dtRequired.Rows[x]["RequiredDoc"].ToString();


        //                l.Add(req);
        //            }


        //            frs.Add("");
        //            frs.Add(ActivityID.Trim());
        //            frs.Add("");
        //            frs.Add("S");
        //            DataTable dtFea = cla.GetDtByProcedure("SP_InserUpdateActivityFeasibility", frs);
        //            for (int y = 0; y != dtFea.Rows.Count; y++)
        //            {

        //                Feasibility btb = new Feasibility();

        //                btb.FeasibilityDetails = dtFea.Rows[y]["FeasibilityRpt"].ToString().Trim();

        //                m.Add(btb);

        //            }

        //            Reg.EligibilityCriateria = k;
        //            Reg.DocumentDetails = l;
        //            Reg.FeasibilityDetails = m;

        //            lst.Add(Reg);


        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ActivityRequired Reg = new ActivityRequired();
        //        Reg.Message = ex.ToString();

        //        lst.Add(Reg);
        //    }

        //    Context.Response.Clear();
        //    Context.Response.ContentType = "application/json";
        //    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
        //    Context.Response.Flush();
        //    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
        //}


        [WebMethod]
        public void GetActivityWiseRequirementEDF(String SecurityKey, String ActivityID, String Lang)
        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<ActivityRequired> lst = new List<ActivityRequired>();

            if (MyCommanClassAPI.CheckApiAuthrization("60", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                ActivityRequired d = new ActivityRequired();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {

                List<String> frs = new List<String>();

                DataTable dtActivity = new DataTable();
                dtActivity = cla.GetDataTable("SELECT  ActivityID   ,ActivityName    ,ActivityCode ,ActivityNameMr  FROM Tbl_M_ActivityMaster  where ActivityID=" + @ActivityID.Trim() + " AND IsDeleted Is NULL");

                for (int q = 0; q != dtActivity.Rows.Count; q++)
                {
                    ActivityRequired Reg = new ActivityRequired();
                    if (Lang.Trim() == "en-IN")
                    {
                        Reg.ActivityName = dtActivity.Rows[q]["ActivityName"].ToString();
                    }
                    else
                    {
                        Reg.ActivityName = dtActivity.Rows[q]["ActivityNameMr"].ToString();
                    }

                    Reg.ActivityCode = dtActivity.Rows[q]["ActivityCode"].ToString();

                    List<Eligibility> k = new List<Eligibility>();
                    List<Requirement> l = new List<Requirement>();
                    List<Feasibility> m = new List<Feasibility>();


                    frs.Add("");
                    frs.Add(ActivityID.Trim());
                    frs.Add("");
                    frs.Add("S");
                    dt = new DataTable();
                    dt = cla.GetDtByProcedure("SP_InserUpdateActivityEligibility", frs);

                    for (int z = 0; z != dt.Rows.Count; z++)
                    {
                        Eligibility Elg = new Eligibility();
                        if (Lang.Trim() == "en-IN")
                        {
                            Elg.EligibilityCriateria = dt.Rows[z]["EligibilityCriteria"].ToString();
                        }
                        else
                        {
                            Elg.EligibilityCriateria = dt.Rows[z]["EligibilityCriteriaMr"].ToString();
                        }
                        k.Add(Elg);

                    }
                    // frs.Clear();//
                    frs.Add("");
                    frs.Add(ActivityID.Trim());
                    frs.Add("");
                    frs.Add("S");
                    DataTable dtRequired = cla.GetDtByProcedure("SP_InserUpdateActivityRequired", frs);

                    for (int x = 0; x != dtRequired.Rows.Count; x++)
                    {
                        Requirement req = new Requirement();
                        if (Lang.Trim() == "en-IN")
                        {
                            req.DocumentDetails = dtRequired.Rows[x]["RequiredDoc"].ToString();
                        }
                        else
                        {
                            req.DocumentDetails = dtRequired.Rows[x]["RequiredDocMr"].ToString();
                        }

                        l.Add(req);
                    }


                    frs.Add("");
                    frs.Add(ActivityID.Trim());
                    frs.Add("");
                    frs.Add("S");
                    DataTable dtFea = cla.GetDtByProcedure("SP_InserUpdateActivityFeasibility", frs);
                    for (int y = 0; y != dtFea.Rows.Count; y++)
                    {

                        Feasibility btb = new Feasibility();
                        if (Lang.Trim() == "en-IN")
                        {
                            btb.FeasibilityDetails = dtFea.Rows[y]["FeasibilityRpt"].ToString().Trim();
                        }
                        else
                        {
                            btb.FeasibilityDetails = dtFea.Rows[y]["FeasibilityRptMr"].ToString().Trim();
                        }
                        m.Add(btb);

                    }

                    Reg.EligibilityCriateria = k;
                    Reg.DocumentDetails = l;
                    Reg.FeasibilityDetails = m;

                    lst.Add(Reg);


                }

            }
            catch (Exception ex)
            {
                ActivityRequired Reg = new ActivityRequired();
                Reg.Message = ex.ToString();

                lst.Add(Reg);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
        }
        #endregion


        #region'Activity Wise Execution Day'

        [WebMethod]
        public void GetActivityWiseWorkServiceExecution(String SecurityKey, String ActivityID)

        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<ActivityExecution> lst = new List<ActivityExecution>();

            if (MyCommanClassAPI.CheckApiAuthrization("62", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                ActivityExecution d = new ActivityExecution();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {

                List<String> apl = new List<String>();

                dt = new DataTable();
                dt = cla.GetDataTable(" Select awt.TimeLineDay , sa.ApprovalStages from Tbl_M_ActivityStageWiseTimeLine awt inner Join Tbl_M_ActivityApprovalStage sas On awt.ApprovalStageID = sas.ActivityApprovalStageID inner Join Tbl_M_ApprovalStages sa On awt.ApprovalStageID = sa.ApprovalStageID where awt.ActivityID = " + ActivityID.Trim() + " AND awt.IsDeleted IS NULL");

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    ActivityExecution Sts = new ActivityExecution();

                    Sts.Stages = dt.Rows[z]["ApprovalStages"].ToString().Trim();
                    Sts.ExecutionTimeinDay = dt.Rows[z]["TimeLineDay"].ToString().Trim();


                    lst.Add(Sts);
                }
            }
            catch (Exception ex)
            {
                ActivityExecution Sts = new ActivityExecution();
                Sts.Message = ex.ToString();

                lst.Add(Sts);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }


        #endregion


        #region 'Payment Request Details'
        [WebMethod]
        public void GetPaymentRequestDetails(String SecurityKey, String ApplicationID)
        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<PaymentRequest> lst = new List<PaymentRequest>();
            if (MyCommanClassAPI.CheckApiAuthrization("57", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                PaymentRequest d = new PaymentRequest();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {

                List<String> apl = new List<String>();
                apl.Add(ApplicationID.Trim());

                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_GetWorkCompletionRequests", apl);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    PaymentRequest Sts = new PaymentRequest();

                    Sts.ApplicationDate = dt.Rows[z]["ApplicationDate"].ToString().Trim();
                    Sts.Activity = dt.Rows[z]["ActivityName"].ToString().Trim();
                    Sts.ActivityCode = dt.Rows[z]["ActivityCode"].ToString().Trim();
                    Sts.FinalAmtApproved = dt.Rows[z]["FinalAmtApproved"].ToString().Trim();
                    Sts.TotalAmtByVCRMC = dt.Rows[z]["TotalAmtByVCRMC"].ToString().Trim();
                    Sts.TotalAmtByBen = dt.Rows[z]["TotalAmtByBen"].ToString().Trim();
                    Sts.WorkReportID = dt.Rows[z]["WorkReportID"].ToString().Trim();
                    Sts.RequestNumber = dt.Rows[z]["RequestNo"].ToString().Trim();
                    Sts.RequestDate = dt.Rows[z]["RequestDate"].ToString().Trim();
                    Sts.btnFarmer = dt.Rows[z]["btnFarmer"].ToString().Trim();

                    lst.Add(Sts);
                }
            }
            catch (Exception ex)
            {
                PaymentRequest Sts = new PaymentRequest();
                Sts.Message = ex.ToString();

                lst.Add(Sts);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }




        #endregion

        #region' Work Completion Updation'
        [WebMethod]
        public void WorkCompletionUpdation(String SecurityKey, String UWorkReportID, String RequestNo, String ApplicationID, String UpdateByRegID, String CompletionDate, String DocTypes, String DocLevels, String DocumentDetails, String PaymentRequestDoc)
        {
            List<clsRetnMessage> lst = new List<clsRetnMessage>();

            bool Validate_CompletionDate = true;

            #region Date Validation Urgent Fix 
            try
            {
                if (CompletionDate != "")
                {
                    int DD = Convert.ToInt32(CompletionDate.Substring(0, 2));
                    int MM = Convert.ToInt32(CompletionDate.Substring(3, 2));
                    int yyyy = Convert.ToInt32(CompletionDate.Substring(6, 4));


                    //string Server_Date = DateTime.UtcNow.Date.ToString("dd/MM/yyyy");
                    //int S_DD = Convert.ToInt32(Server_Date.Substring(0, 2));
                    //int S_MM = Convert.ToInt32(Server_Date.Substring(3, 2));
                    //int S_yyyy = Convert.ToInt32(Server_Date.Substring(6, 4));

                    if (Convert.ToDateTime(MM + "/" + DD + "/" + yyyy) > DateTime.Now.Date)
                    {
                        clsRetnMessage d = new clsRetnMessage();
                        d.MessageType = "Error";
                        d.Message = "Please enter valid Completion Date";// ex.ToString();
                        lst.Add(d);

                        Validate_CompletionDate = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clsRetnMessage d = new clsRetnMessage();
                d.MessageType = "Exception";
                d.Message = "Exception :" + ex.Message + " //" + DateTime.Now;// ex.ToString();
                lst.Add(d);
                Validate_CompletionDate = false;

                //Validate_CompletionDate = true;
            }
            #endregion



            if ((ApplicationID.ToString().Trim().Length > 0) && (Validate_CompletionDate == true))
            {

                MyCommanClassAPI Comcls = new MyCommanClassAPI();
                MyClass cla = new MyClass();
                //String RegistrationDate = cla.SvrDate();
                if (MyCommanClassAPI.CheckApiAuthrization("71", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
                {
                    clsRetnMessage d = new clsRetnMessage();

                    d.MessageType = "Error";
                    d.Message = "Authorization Failed";
                    lst.Add(d);

                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }
                //------------validations ---------------------// 

                //
                if (cla.GetExecuteScalar("Select Tbl_T_ApplicationDetails.ApplicationID from Tbl_T_ApplicationDetails where ApplicationID=" + ApplicationID + " and ApprovalStageID>=5 and ApplicationStatusID<>2").Trim().Length == 0)
                {
                    //Literal1.Text = "Not available for this Activity";
                    clsRetnMessage d9 = new clsRetnMessage();
                    d9.MessageType = "Error";
                    d9.Message = "Pre-Senction not issue ";
                    lst.Add(d9);
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }

                if (UWorkReportID.Trim().Length == 0)
                {
                    // in case of new request 
                    String str = cla.GetExecuteScalar("SELECT  top 1 WorkReportID   FROM Tbl_T_Application_WorkReport where ApplicationID=" + ApplicationID.ToString() + " and IsDeleted is null and ApplicationStatusID not in (15,2,25) ");
                    if (str.Trim().Length != 0)
                    {
                        if (Convert.ToInt32(str) > 0)
                        {
                            clsRetnMessage d9 = new clsRetnMessage();
                            d9.MessageType = "Error";
                            d9.Message = "Your Payment Request is already in process , please wait until it will complete. if your application is back to the stage please edit it and make the required changes.";
                            lst.Add(d9);
                            Context.Response.Clear();
                            Context.Response.ContentType = "application/json";
                            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                            Context.Response.Flush();
                            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                            return;
                        }
                        else
                        {
                            // not in process 

                            // Activities for which part payment is not available to be closed once the payment is done for that activity. Re-payment option to be made unavailable even if partial payment is made 
                            if (cla.GetExecuteScalar("SELECT PartialPaymentAllowed FROM Tbl_M_ActivityMaster where ActivityID=(Select Tbl_T_ApplicationDetails.ActivityID from Tbl_T_ApplicationDetails where ApplicationID=" + ApplicationID.ToString() + " and Tbl_T_ApplicationDetails.IsDeleted is null ) and IsDeleted is null") == "NO")
                            {
                                //Literal1.Text = "Not available for this Activity";
                                clsRetnMessage d9 = new clsRetnMessage();
                                d9.MessageType = "Error";
                                d9.Message = "You can make a single payment request for this activity if your application is back to the stage please edit it and make the required changes. ";
                                lst.Add(d9);
                                Context.Response.Clear();
                                Context.Response.ContentType = "application/json";
                                Context.Response.Flush();
                                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                                return;
                            }
                        }
                    }
                }




                int WorkCompletionIDData = cla.TableID("Tbl_T_Application_WorkCompletions", "WorkCompletionID");
                //int WorkReportID = cla.TableID("Tbl_T_Application_WorkReport", "WorkReportID");


                RequestNo = cla.GetSqlUnikNO("3") + "/" + ApplicationID.ToString().Trim();

                //String path = Server.MapPath("~/admintrans/DocMasters/MasterDoc");
                String RegistrationID = cla.GetExecuteScalar("SELECT   RegistrationID FROM Tbl_T_ApplicationDetails where ApplicationID=" + ApplicationID.Trim() + "");
                String PathUp = "DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "";
                AzureBlobHelper fileRet = new AzureBlobHelper();

                string imageName = "";
                string filepath = "";

                //  String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + ApplicationID.Trim() + "");
                if (PaymentRequestDoc.Length > 0)
                {
                    imageName = "PaymentRequestDoc" + WorkCompletionIDData.ToString() + "" + MyCommanClassAPI.GetFileExtension(PaymentRequestDoc);
                    filepath = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "/" + imageName;
                    //set the image path
                    byte[] imageBytes = Convert.FromBase64String(PaymentRequestDoc);
                    fileRet.UploadData(PathUp, imageName, imageBytes);
                    //string imgPath = path + imageName;
                    //byte[] imageBytes = Convert.FromBase64String(PaymentRequestDoc);
                    //File.WriteAllBytes(imgPath, imageBytes);                     
                }

                using (SqlConnection connection = new SqlConnection(clsSettings.strCoonectionString))
                {
                    connection.Open();

                    SqlCommand command = connection.CreateCommand();
                    SqlTransaction transaction;
                    transaction = connection.BeginTransaction("CTransaction");
                    command.Connection = connection;
                    command.Transaction = transaction;

                    int WorkReportID = cla.TableID("Tbl_T_Application_WorkReport", "WorkReportID", command);
                    int WorkCompletionID = cla.TableID("Tbl_T_Application_WorkCompletions", "WorkCompletionID", command);
                    try
                    {


                        if (UWorkReportID.Trim().Length == 0)
                        {
                            // add new 
                            String str1 = " INSERT INTO Tbl_T_Application_WorkReport (WorkReportID, RequestNo, ApplicationID, UpdateOnDate, UpdateByRegID, ApprovalStageID, ApplicationStatusID,DataSource)";
                            str1 += " VALUES(" + WorkReportID + ", '" + RequestNo.Trim() + "'," + ApplicationID.Trim() + ",'" + cla.mdy(cla.SvrDate()) + "'," + UpdateByRegID.Trim() + ", 6,1,'APP')";//6,1//" + ApprovalStageID.Trim() + ", " + ApplicationStatusID.Trim() + "
                            cla.ExecuteCommand(str1, command);
                        }

                        else
                        {
                            WorkReportID = Convert.ToInt32(UWorkReportID);
                            RequestNo = "";
                        }


                        if (ApplicationID.ToString().Trim().Length > 0)
                        {
                            //  String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + ApplicationID.Trim() + "");

                            if (PaymentRequestDoc.Length > 0)
                            {

                                String str = " INSERT INTO Tbl_T_Application_WorkCompletions (WorkCompletionID, ApplicationID,  DocumentDetails, DocumentUploaded, CompletionDate, DocTypes, DocLevels, WorkReportID)";
                                str += " VALUES(" + WorkCompletionID + "," + ApplicationID.Trim() + ",N'" + DocumentDetails.Trim().Replace("'", "") + "','" + filepath + "','" + cla.mdy(CompletionDate.Trim()) + "',N'" + DocTypes.Trim() + "',N'" + DocLevels.Trim() + "'," + WorkReportID + ") ";

                                cla.ExecuteCommand(str, command);
                            }
                        }
                        transaction.Commit();

                        clsRetnMessage d = new clsRetnMessage();
                        d.MessageType = "Sucess";
                        d.WorkReportID = WorkReportID.ToString();//
                        d.WorkCompletionID = WorkCompletionID.ToString();//
                        d.DocumentUploaded = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "/" + imageName;
                        d.RequestNo = RequestNo.ToString();
                        d.RequestDate = cla.mdy(cla.SvrDate());//UpdateOnDate.ToString();//cla.mdy(CompletionDate.Trim())
                        d.Message = "Record Saved Sucessfully";
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

                        Util.LogError(ex, "WorkCompletionUpdation", command.CommandText);
                        clsRetnMessage d = new clsRetnMessage();
                        d.MessageType = "Error";
                        d.Message = "Kindly reset your internet connection and try again .";// ex.ToString();
                        lst.Add(d);

                    }
                    finally
                    {
                        if (connection != null) { connection.Close(); }
                        command.Dispose();
                    }

                }
            }


            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
        }

        #endregion

        #region'Get Work Comlpetion Updated Data'

        [WebMethod]
        public void GetWorkComlpetionUpdatedData(String SecurityKey, String ApplicationID, String WorkReportID)
        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<WorkCompletionData> lst = new List<WorkCompletionData>();
            if (MyCommanClassAPI.CheckApiAuthrization("72", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                WorkCompletionData d = new WorkCompletionData();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {

                List<String> apl = new List<String>();

                //    dt = cla.GetDataTable("SELECT   WorkCompletionID, Convert(nvarchar(20),CompletionDate,103) as CompletionDate,  DocumentDetails, DocumentUploaded , DocTypes, DocLevels, IsInspection, LatitudeMap,LongitudeMap  FROM Tbl_T_Application_WorkCompletions WHERE  (ApplicationID = " + ApplicationID.ToString() + ") and  WorkReportID in (Select WorkReportID from Tbl_T_Application_WorkReport where Tbl_T_Application_WorkReport.WorkReportID=" + WorkReportID.ToString() + " AND Tbl_T_Application_WorkReport.BenWorkReportID Is Null )  AND IsDeleted is null ORDER BY CompletionDate");
                //withinspe //

                //dt = cla.GetDataTable("SELECT   WorkCompletionID, Convert(nvarchar(20),CompletionDate,103) as CompletionDate,  DocumentDetails, DocumentUploaded , DocTypes, DocLevels, IsInspection, LatitudeMap,LongitudeMap  FROM Tbl_T_Application_WorkCompletions WHERE  WorkReportID in (Select WorkReportID from Tbl_T_Application_WorkReport where Tbl_T_Application_WorkReport.WorkReportID=" + WorkReportID.ToString() + "  and (Tbl_T_Application_WorkReport.ApplicationID = " + ApplicationID.ToString() + ") )  AND IsInspection is null AND IsDeleted is null ORDER BY CompletionDate");
                dt = cla.GetDataTable("SELECT   WorkCompletionID, Convert(nvarchar(20),CompletionDate,103) as CompletionDate,  DocumentDetails, DocumentUploaded , DocTypes, DocLevels, IsInspection, LatitudeMap,LongitudeMap  FROM Tbl_T_Application_WorkCompletions WHERE WorkReportID=" + WorkReportID.ToString() + " and ApplicationID=" + ApplicationID.ToString() + " AND IsInspection is null AND IsDeleted is null ORDER BY CompletionDate");

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    WorkCompletionData Sts = new WorkCompletionData();
                    Sts.WorkCompletionID = dt.Rows[z]["WorkCompletionID"].ToString().Trim();
                    Sts.DocTypes = dt.Rows[z]["DocTypes"].ToString().Trim();
                    Sts.DocLevels = dt.Rows[z]["DocLevels"].ToString().Trim();
                    Sts.DocumentDetails = dt.Rows[z]["DocumentDetails"].ToString().Trim();
                    Sts.CompletionDate = dt.Rows[z]["CompletionDate"].ToString().Trim();
                    Sts.DocumentUploaded = dt.Rows[z]["DocumentUploaded"].ToString().Trim();
                    Sts.PMURemark = "";

                    MyCommanClass Comcls = new MyCommanClass();
                    DataTable dtApp = Comcls.GetFarmerRepeatRequestDetails(ApplicationID);
                    if (dtApp.Rows.Count > 0)
                    {
                        Sts.PMURemark = "सदरील घटकासाठी एका अर्जावरती  एकदाच अनुदान अनुज्ञेय असून अनुदान अदायगी झालेली आहे. परंतु लाभार्थीने दुबार अनुदान मागणी केल्याचे निदर्शनास येत आहे.";
                    }

                    lst.Add(Sts);
                }
            }
            catch (Exception ex)
            {
                WorkCompletionData Sts = new WorkCompletionData();
                Sts.Message = ex.ToString();

                lst.Add(Sts);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }
        #endregion

        #region'Save Subsidy Amount'

        [WebMethod]
        public void SaveSubsidyAmount(String SecurityKey, String ApplicationID, String ExpenditureAmount, String WorkReportID)
        {

            List<clsReturnMessage> lst = new List<clsReturnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();

            if (MyCommanClassAPI.CheckApiAuthrization("67", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 




            if (ExpenditureAmount.Trim().Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Expenditure Amount";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //------------validations ---------------------// 


            String ApprovalStageID = cla.GetExecuteScalar("SELECT ApprovalStageID FROM  Tbl_T_ApplicationDetails where ApplicationID=" + ApplicationID.ToString().Trim() + "");

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
                    String str = "";
                    if (ApprovalStageID == "5")
                    {
                        str = " UPDATE Tbl_T_ApplicationDetails SET  ApplicationStatusID =1, ApprovalStageID =6  WHERE   (ApplicationID =" + ApplicationID.ToString().Trim() + ") ";
                        cla.ExecuteCommand(str, command);
                    }

                    str = " UPDATE Tbl_T_Application_WorkReport SET TotalAmtByBen='" + ExpenditureAmount.Trim() + "' WHERE   (ApplicationID =" + ApplicationID.ToString().Trim() + ") AND WorkReportID=" + WorkReportID.Trim() + "  ";
                    cla.ExecuteCommand(str, command);





                    transaction.Commit();


                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Sucess";
                    d.WorkReportID = WorkReportID.ToString();//
                    d.Message = "Record Saved Sucessfully";
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

                    clsReturnMessage d = new clsReturnMessage();
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

        #region' Remove Work Completion'
        [WebMethod]
        public void RemoveCompletedWork(String SecurityKey, String WorkCompletionID)
        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<RemoveWorkUpdate> lst = new List<RemoveWorkUpdate>();

            if (MyCommanClassAPI.CheckApiAuthrization("73", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                RemoveWorkUpdate d = new RemoveWorkUpdate();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {

                List<String> apl = new List<String>();
                apl.Add(WorkCompletionID.Trim());

                bool str = cla.ExecuteByProcedure("SP_T_Remove_Application_WorkCompletions", apl);
                if (str == true)
                {
                    RemoveWorkUpdate w = new RemoveWorkUpdate();
                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Sucess";
                    // d.WorkReportID = WorkReportID.ToString();//
                    d.Message = "Your details have been Deleted successfully.";
                    lst.Add(w);

                }


                //  lst.Add(w);

            }
            catch (Exception ex)
            {
                RemoveWorkUpdate w = new RemoveWorkUpdate();
                w.Message = ex.ToString();

                lst.Add(w);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }

        #endregion

        #region'Get Uploaded Inspection Details Data'

        [WebMethod]
        public void GetDataInspectionDetails(String SecurityKey, String ApplicationID, String WorkReportID)
        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<InspectionData> lst = new List<InspectionData>();
            if (MyCommanClassAPI.CheckApiAuthrization("78", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                InspectionData d = new InspectionData();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {

                List<String> apl = new List<String>();

                dt = cla.GetDataTable("SELECT   WorkCompletionID, Convert(nvarchar(20),CompletionDate,103) as CompletionDate,  DocumentDetails, DocumentUploaded , DocTypes, DocLevels, IsInspection, LatitudeMap,LongitudeMap  FROM Tbl_T_Application_WorkCompletions WHERE  WorkReportID in (Select WorkReportID from Tbl_T_Application_WorkReport where Tbl_T_Application_WorkReport.WorkReportID=" + WorkReportID.ToString() + "  and (Tbl_T_Application_WorkReport.ApplicationID = " + ApplicationID.ToString() + ") )  AND IsInspection is not null AND IsDeleted is null ORDER BY CompletionDate");

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    InspectionData Sts = new InspectionData();
                    Sts.WorkCompletionID = dt.Rows[z]["WorkCompletionID"].ToString().Trim();
                    Sts.DocTypes = dt.Rows[z]["DocTypes"].ToString().Trim();
                    Sts.DocLevels = dt.Rows[z]["DocLevels"].ToString().Trim();
                    Sts.DocumentDetails = dt.Rows[z]["DocumentDetails"].ToString().Trim();
                    Sts.CompletionDate = dt.Rows[z]["CompletionDate"].ToString().Trim();
                    Sts.DocumentUploaded = dt.Rows[z]["DocumentUploaded"].ToString().Trim();
                    Sts.LatitudeMap = dt.Rows[z]["LatitudeMap"].ToString().Trim();
                    Sts.LongitudeMap = dt.Rows[z]["LongitudeMap"].ToString().Trim();
                    Sts.IsInspection = dt.Rows[z]["IsInspection"].ToString().Trim();

                    lst.Add(Sts);
                }
            }
            catch (Exception ex)
            {
                InspectionData Sts = new InspectionData();
                Sts.Message = ex.ToString();

                lst.Add(Sts);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }
        #endregion


        #region' Save / Edit Inspection Details'
        [WebMethod]
        public void SaveEditInspectionDetails(String SecurityKey, String UWorkReportID, String BenWorkReportID, String RequestNo, String ApplicationID, String UpdateByRegID, String DocTypes, String DocLevels, String DocumentDetails, String InspectionImage, String LatitudeMap, String LongitudeMap)//LatitudeMap]LongitudeMap
        {

            List<clsRetnMessage> lst = new List<clsRetnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            //String RegistrationDate = cla.SvrDate();
            if (MyCommanClassAPI.CheckApiAuthrization("66", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {

                clsRetnMessage d = new clsRetnMessage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 

            if (UWorkReportID.Trim().Length == 0)
            {

                String str = cla.GetExecuteScalar("SELECT  top 1 WorkReportID   FROM Tbl_T_Application_WorkReport where ApplicationID=" + ApplicationID.ToString() + " and IsDeleted is null and IsPaymentDone is null and ApplicationStatusID not in (15,2) ");
                if (str.Trim().Length != 0)
                {
                    if (Convert.ToInt32(str) > 0)
                    {
                        clsRetnMessage d9 = new clsRetnMessage();

                        d9.MessageType = "Error";
                        d9.Message = "Your Payment Request is already in process , please wait until it will complete.";
                        lst.Add(d9);
                        Context.Response.Clear();
                        Context.Response.ContentType = "application/json";
                        //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                        Context.Response.Flush();
                        Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                        return;
                    }
                }



                // Activities for which part payment is not available to be closed once the payment is done for that activity. Re-payment option to be made unavailable even if partial payment is made 
                if (cla.GetExecuteScalar("SELECT  top 1 WorkReportID   FROM Tbl_T_Application_WorkReport where ApplicationID=" + ApplicationID.ToString() + " and IsDeleted is null and ApplicationStatusID not in (2,20) ").Length > 0)
                {
                    if (cla.GetExecuteScalar("SELECT PartialPaymentAllowed FROM Tbl_M_ActivityMaster where ActivityID=(Select Tbl_T_ApplicationDetails.ActivityID from Tbl_T_ApplicationDetails where ApplicationID=" + ApplicationID.ToString() + " and Tbl_T_ApplicationDetails.IsDeleted is null ) and IsDeleted is null") == "NO")
                    {
                        //Literal1.Text = "Not available for this Activity";
                        clsRetnMessage d9 = new clsRetnMessage();
                        d9.MessageType = "Error";
                        d9.Message = "Not available for this Activity";
                        lst.Add(d9);
                        Context.Response.Clear();
                        Context.Response.ContentType = "application/json";
                        Context.Response.Flush();
                        Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                        return;
                    }
                }
            }


            int WorkCompletionID = cla.TableID("Tbl_T_Application_WorkCompletions", "WorkCompletionID");

            DBTAppAPI dbtapp = new DBTAppAPI();
            dbtapp.InsertApplication_WorkCompletionsPartial(WorkCompletionID.ToString(), ApplicationID, DocumentDetails.Trim().Replace("'", ""), "NotUpdated", cla.mdy(cla.SvrDate()), DocTypes.Trim(), DocLevels.Trim(), "", "1", LatitudeMap.Trim(), LongitudeMap.Trim());


            int WorkReportID = cla.TableID("Tbl_T_Application_WorkReport", "WorkReportID");





            String RegistrationID = cla.GetExecuteScalar("SELECT RegistrationID FROM Tbl_T_ApplicationDetails where ApplicationID=" + ApplicationID.Trim() + "");

            //String path = Server.MapPath("~/admintrans/DocMasters/MasterDoc");
            //String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + RegistrationID.Trim() + "");
            String PathUp = "DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "";
            AzureBlobHelper fileRet = new AzureBlobHelper();

            string imageName = "";
            string filepath = "";
            if (ApplicationID.ToString().Trim().Length > 0)
            {
                // String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + ApplicationID.Trim() + "");
                if (InspectionImage.Length > 0)
                {
                    imageName = "InspectionImage" + WorkCompletionID.ToString() + "" + MyCommanClassAPI.GetFileExtension(InspectionImage);
                    filepath = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.Trim() + "/" + imageName;
                    //set the image path
                    //string imgPath = path + imageName;
                    //byte[] imageBytes = Convert.FromBase64String(InspectionImage);
                    //File.WriteAllBytes(imgPath, imageBytes);            

                    byte[] imageBytes = Convert.FromBase64String(InspectionImage);
                    fileRet.UploadData(PathUp, imageName, imageBytes);
                }
            }

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

                    if (UWorkReportID.Trim().Length == 0)
                    {
                        // add new 
                        String str1 = "INSERT INTO Tbl_T_Application_WorkReport(WorkReportID, RequestNo, ApplicationID, UpdateOnDate, UpdateByRegID,BenWorkReportID,DataSource)";
                        str1 += " VALUES(" + WorkReportID + ",'" + WorkReportID.ToString() + "'," + ApplicationID.Trim() + ",'" + cla.mdy(cla.SvrDate()) + "'," + UpdateByRegID.Trim() + "," + WorkReportID + ",'APP2')";//BenWorkReportID                        
                        cla.ExecuteCommand(str1, command);
                    }
                    else
                    {
                        WorkReportID = Convert.ToInt32(UWorkReportID);
                    }


                    if (ApplicationID.ToString().Trim().Length > 0)
                    {
                        // String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + ApplicationID.Trim() + "");


                        if (InspectionImage.Length > 0)
                        {
                            //String str = " INSERT INTO Tbl_T_Application_WorkCompletions(WorkCompletionID, ApplicationID,  DocumentDetails, DocumentUploaded,CompletionDate,DocTypes,DocLevels,WorkReportID,IsInspection, LatitudeMap,LongitudeMap)";
                            //str += " VALUES(" + WorkCompletionID + "," + ApplicationID.Trim() + ",N'" + DocumentDetails.Trim().Replace("'", "") + "','" + filepath + "','" + cla.mdy(cla.SvrDate()) + "',N'" + DocTypes.Trim() + "',N'" + DocLevels.Trim() + "'," + WorkReportID + ",'1','" + LatitudeMap.Trim() + "','" + LongitudeMap.Trim() + "') ";

                            String str = "Update Tbl_T_Application_WorkCompletions set  WorkReportID='" + WorkReportID + "',DocumentUploaded='" + filepath + "' where  WorkCompletionID=" + WorkCompletionID + " and ApplicationID=" + ApplicationID.Trim();


                            //if (File.Exists(Server.MapPath(filepath)))
                            //{
                            bool res = cla.ExecuteCommandWithResult(str, command);

                            if (res)
                            {
                                transaction.Commit();

                                clsRetnMessage d = new clsRetnMessage();
                                d.MessageType = "Sucess";
                                d.WorkReportID = WorkReportID.ToString();//
                                d.WorkCompletionID = WorkCompletionID.ToString();
                                d.DocumentUploaded = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "/" + imageName;
                                d.Message = "Record Saved Sucessfully";
                                lst.Add(d);
                            }
                            else
                            {
                                String query = "Update Tbl_T_Application_WorkCompletions set IsDeleted=1 where  WorkCompletionID=" + WorkCompletionID + " and DocumentUploaded='NotUpdated'";
                                bool res1 = cla.ExecuteCommandWithResult(str, command);

                                //transaction.Rollback();
                                transaction.Commit();
                                clsRetnMessage d1 = new clsRetnMessage();
                                d1.MessageType = "Error";
                                d1.Message = "File not uploaded , kindly retry ..";
                                lst.Add(d1);
                            }

                            // }
                            //else
                            //{
                            //    try
                            //    {
                            //        transaction.Rollback();

                            //    }
                            //    catch
                            //    {

                            //    }

                            //    clsRetnMessage d1= new clsRetnMessage();
                            //    d1.MessageType = "Error";
                            //    d1.Message = "File not uploaded , kindly retry ..";
                            //    lst.Add(d1);
                            //}
                        }
                    }
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

                    clsRetnMessage d = new clsRetnMessage();
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

        #region'Inspection Amount'

        [WebMethod]
        public void SaveInspectionAmount(String SecurityKey, String ApplicationID, String ExpenditureAmount, String WorkReportID, String eThibakAmount, String eThibakMbBookPath, String IndecativeCost)
        {

            List<clsReturnMessage> lst = new List<clsReturnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();

            if (MyCommanClassAPI.CheckApiAuthrization("65", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 




            if (ExpenditureAmount.Trim().Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill Inspection Amount";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //
            String IsPopupReportField = "";
            IsPopupReportField = cla.GetExecuteScalar("SELECT top 1 ID FROM Tbl_T_ApplicationDesk4Details WHERE  WorkReportID=" + WorkReportID + "");
            if (IsPopupReportField.Trim().Length == 0)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please update inspection details. ";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //------------validations ---------------------// 
            String RegistrationID = cla.GetExecuteScalar("SELECT RegistrationID FROM  Tbl_T_ApplicationDetails where ApplicationID=" + ApplicationID.ToString().Trim() + "");
            String PathUp = "DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "";
            AzureBlobHelper fileRet = new AzureBlobHelper();

            string imageName = "";
            string filepath = "";
            if (eThibakAmount.Trim().Length == 0) eThibakAmount = "0";
            if (ApplicationID.ToString().Trim().Length > 0)
            {

                if (eThibakMbBookPath.Length > 0)
                {
                    imageName = "eThibakMbBook" + WorkReportID.ToString() + "" + MyCommanClassAPI.GetFileExtension(eThibakMbBookPath);
                    filepath = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.Trim() + "/" + imageName;
                    byte[] imageBytes = Convert.FromBase64String(eThibakMbBookPath);
                    fileRet.UploadData(PathUp, imageName, imageBytes);
                }
            }


            String ApprovalStageID = cla.GetExecuteScalar("SELECT ApprovalStageID FROM  Tbl_T_ApplicationDetails where ApplicationID=" + ApplicationID.ToString().Trim() + "");

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
                    String str = "";


                    str = " UPDATE Tbl_T_Application_WorkReport SET TotalAmtByVCRMC=" + ExpenditureAmount.Trim() + " , ApprovalStageID=6,ApplicationStatusID=1 , eThibakAmount='" + eThibakAmount + "' ,  eThibakMbBookPath='" + filepath + "' , IndecativeCost='" + IndecativeCost + "'   WHERE   (ApplicationID =" + ApplicationID.ToString().Trim() + ") AND WorkReportID=" + WorkReportID.Trim() + "  ";
                    cla.ExecuteCommand(str, command);



                    transaction.Commit();


                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Sucess";
                    d.WorkReportID = WorkReportID.ToString();//
                    d.Message = "Record Saved Sucessfully";
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

                    clsReturnMessage d = new clsReturnMessage();
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

        [WebMethod]
        public void UpdateSanctionDesk4(String SecurityKey, String ApplicationID, String LogDetails, String ApplicationStatusID, String ApprovalStageID, String UpdateByRegID, String ReasonID, String TotalAmtByVCRMC, String WorkReportID)//, String Remarkany
        {
            List<clsReturnMessage> lst = new List<clsReturnMessage>();


            if (Convert.ToInt32(ApplicationStatusID) > 15)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "ERROR";
                d.Message = " This Service is temporarily not available , please use web application to complete your transaction . Sorry for inconvenience .";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //ApplicationID
            MyCommanClass Comcls = new MyCommanClass();
            DataTable dtApp = Comcls.GetFarmerRepeatRequestDetails(ApplicationID);
            if (dtApp.Rows.Count > 0)
            {
                //Repeated Farmer Not to Proceed ahead
                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "UpdateSanctionDesk4:App Desk 4";
                err.ProjectName = "POCRA WEB";
                err.ErrorDescription = "Application :" + ApplicationID + " , Not Allowed to Proceed Ahead";
                err.ErrorSeverity = (int)ErrorSeverity.Information;
                new ErrorLogManager().InsertErrorLog(err);


                //LtPMUStatus.Text = "<span style='font-weight:bold;color:red;'>सदरील घटकासाठी एका अर्जावरती  एकदाच अनुदान अनुज्ञेय असून अनुदान अदायगी झालेली आहे. परंतु लाभार्थीने दुबार अनुदान मागणी केल्याचे निदर्शनास येत आहे.</span>";
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "ERROR";
                d.Message = " सदरील घटकासाठी एका अर्जावरती  एकदाच अनुदान अनुज्ञेय असून अनुदान अदायगी झालेली आहे. परंतु लाभार्थीने दुबार अनुदान मागणी केल्याचे निदर्शनास येत आहे.";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }


            if (ApplicationStatusID.Trim() == "20")
            {
                //Back To Beneficiary
                //Remark
                if (LogDetails.Trim().Length == 0)
                {
                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Warning";
                    d.Message = "Please fill Remark / Reason ";
                    lst.Add(d);
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }
                BackToBeneficiary(ApplicationID, LogDetails, ApplicationStatusID, ApprovalStageID, UpdateByRegID, ReasonID, TotalAmtByVCRMC, WorkReportID);

                return;


            }




            MyClass cla = new MyClass();
            String UpdateOnDate = cla.SvrDate();
            if (MyCommanClassAPI.CheckApiAuthrization("70", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            if (ApplicationStatusID.Trim() != "2")
            {
                if (cla.GetExecuteScalar("Select top 1 ID from Tbl_T_ApplicationDesk4Details where WorkReportID=" + WorkReportID.Trim() + " ").Trim().Length == 0)
                {
                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Warning";
                    d.Message = "Please complete Inspection Report details";
                    lst.Add(d);
                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }
            }

            //if (ApplicationStatusID.Trim() == "300")
            //{
            //    String aa = cla.GetExecuteScalar("Select count(WorkReportID) from Tbl_T_Application_WorkReport w where ApplicationStatusID=2 and w.ApplicationID="+ApplicationID+" and IsDeleted is null");
            //    if (Convert.ToInt32(aa)==0)
            //    {
            //        // if not req. cancelled till date then error mes.
            //        clsReturnMessage d = new clsReturnMessage();
            //        d.MessageType = "Warning";
            //        d.Message = "The 'reject and Close' option is not applicable in this case, please choose the 'Rejection' option to reject.";
            //        lst.Add(d);
            //        Context.Response.Clear();
            //        Context.Response.ContentType = "application/json";
            //        Context.Response.Flush();
            //        Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
            //        return;
            //    }
            //}
            //if (ApplicationStatusID.Trim() == "2")
            //{
            //    String aa = cla.GetExecuteScalar("Select count(WorkReportID) from Tbl_T_Application_WorkReport w where ApplicationStatusID=2 and w.ApplicationID=" + ApplicationID + " and IsDeleted is null");
            //    if (Convert.ToInt32(aa) > 0)
            //    {
            //        // if not req. cancelled till date then error mes.
            //        clsReturnMessage d = new clsReturnMessage();
            //        d.MessageType = "Warning";
            //        d.Message = "The 'Rejection' option is not applicable in this case, please choose the 'reject and Close' option to reject.";
            //        lst.Add(d);
            //        Context.Response.Clear();
            //        Context.Response.ContentType = "application/json";
            //        Context.Response.Flush();
            //        Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
            //        return;
            //    }
            //}


            //------------validations ---------------------// 



            if (ApplicationStatusID.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select  ApplicationStatus";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            String str = "";



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

                    int WorkReporLogID = cla.TableID("Tbl_T_Application_WorkReport_Log", "WorkReporLogID", command);
                    str = " INSERT INTO Tbl_T_Application_WorkReport_Log (WorkReporLogID, WorkReportID, LogDetails, ApplicationStatusID, ApprovalStageID, UpdateOnDate, UpdateByRegID)";
                    str += " VALUES(" + WorkReporLogID + "," + WorkReportID.ToString() + ",N'" + LogDetails.Trim() + "'," + ApplicationStatusID.Trim() + ",6,'" + cla.mdy(cla.SvrDate()) + "'," + UpdateByRegID.ToString() + ")";
                    cla.ExecuteCommand(str, command);


                    if (ReasonID.ToString().Length == 0)
                    {
                        str = "";
                    }
                    else
                    {
                        int WorkReportLogReasonID = cla.TableID("Tbl_T_Application_WorkReport_LogReason", "WorkReportLogReasonID", command);
                        String[] Reas = ReasonID.Trim().Split('/');
                        for (int x = 0; x != Reas.Length; x++)
                        {
                            ReasonID = Reas[x];


                            str = " INSERT INTO Tbl_T_Application_WorkReport_LogReason (WorkReportLogReasonID, WorkReporLogID, ReasonID)";
                            str += " VALUES(" + WorkReportLogReasonID + "," + WorkReporLogID + "," + ReasonID.Trim() + ")";
                            cla.ExecuteCommand(str, command);
                            WorkReportLogReasonID++;
                        }
                    }

                    if (ApplicationStatusID.Trim() == "5")
                    {
                        str = " UPDATE Tbl_T_Application_WorkReport SET ApprovalStageID=7  , ApplicationStatusID=1 , TotalAmtByVCRMC=" + TotalAmtByVCRMC.Trim() + "  WHERE WorkReportID=" + WorkReportID.ToString() + "";
                    }
                    else
                    {
                        str = " UPDATE Tbl_T_Application_WorkReport SET ApprovalStageID=6  , ApplicationStatusID='" + ApplicationStatusID.Trim() + "'  WHERE WorkReportID=" + WorkReportID.ToString() + "";
                    }

                    cla.ExecuteCommand(str, command);


                    transaction.Commit();
                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Sucess";
                    d.RegistrationID = UpdateByRegID.ToString();
                    d.Message = "Record Saved Sucessfully";
                    lst.Add(d);

                    #region Automatically Presanction cancel on Rejection on Desk 4 - Phase 1               
                    if (ApplicationStatusID == "2")
                    {
                        if (ApprovalStageID == "6" || ApprovalStageID == "5")
                        {
                            MyClass cla2 = new MyClass();
                            List<String> lst2 = new List<string>();
                            lst2.Add(ApplicationID);
                            lst2.Add(UpdateByRegID);
                            cla2.ExecuteByProcedure("Usp_PresanctionCancel_Desk4", lst2);
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    Util.LogError(ex);

                    try
                    {
                        transaction.Rollback();

                    }
                    catch
                    {

                    }

                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Error";
                    d.Message = "Internet Connection is dropped , Please check your internet connection and try again ..";// ex.ToString();
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

        private void BackToBeneficiary(String ApplicationID, String LogDetails, String ApplicationStatusID, String ApprovalStageID, String UpdateByRegID, String ReasonID, String TotalAmtByVCRMC, String WorkReportID)
        {
            List<clsReturnMessage> lst = new List<clsReturnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();

            MyClass cla = new MyClass();
            String UpdateOnDate = cla.SvrDate();


            String str = "";
            int WorkReporLogID = cla.TableID("Tbl_T_Application_WorkReport_Log", "WorkReporLogID");
            int WorkReportLogReasonID = cla.TableID("Tbl_T_Application_WorkReport_LogReason", "WorkReportLogReasonID");

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


                    str = " INSERT INTO Tbl_T_Application_WorkReport_Log (WorkReporLogID, WorkReportID, LogDetails, ApplicationStatusID, ApprovalStageID, UpdateOnDate, UpdateByRegID)";
                    str += " VALUES(" + WorkReporLogID + "," + WorkReportID.ToString() + ",N'" + LogDetails.Trim() + "'," + ApplicationStatusID.Trim() + ",6,'" + cla.mdy(cla.SvrDate()) + "'," + UpdateByRegID.ToString() + ")";
                    cla.ExecuteCommand(str, command);

                    if (ReasonID.ToString().Length == 0)
                    {
                        str = "";
                    }
                    else
                    {

                        String[] Reas = ReasonID.Trim().Split('/');
                        for (int x = 0; x != Reas.Length; x++)
                        {
                            ReasonID = Reas[x];


                            str = " INSERT INTO Tbl_T_Application_WorkReport_LogReason (WorkReportLogReasonID, WorkReporLogID, ReasonID)";
                            str += " VALUES(" + WorkReportLogReasonID + "," + WorkReporLogID + "," + ReasonID.Trim() + ")";
                            cla.ExecuteCommand(str, command);
                            WorkReportLogReasonID++;
                        }
                    }


                    str = " UPDATE Tbl_T_Application_WorkReport SET ApprovalStageID=6  , ApplicationStatusID='" + ApplicationStatusID + "'  WHERE WorkReportID=" + WorkReportID + "";
                    cla.ExecuteCommand(str, command);

                    transaction.Commit();
                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Sucess";
                    d.RegistrationID = UpdateByRegID.ToString();
                    d.Message = "Record Saved Sucessfully";
                    lst.Add(d);


                }
                catch (Exception ex)
                {
                    //String error = "Error in Add Journey Save button Click " + ex.ToString();
                    //WriteError(error, Session["UserEmailID"].ToString());
                    try
                    {
                        transaction.Rollback();

                    }
                    catch
                    {

                    }
                    clsReturnMessage d = new clsReturnMessage();
                    d.MessageType = "Error";
                    d.Message = ex.ToString();
                    lst.Add(d);
                }
                finally
                {

                    if (connection != null) { connection.Close(); }
                    command.Dispose();
                }

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
            }
        }


        [WebMethod]
        public void Get712SurveyNumber(String SecurityKey, String RegistrationID, String ParentLandID)
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();

            if (EncryptDecryptQueryString.Decrypt(SecurityKey) != clsSettings.APIKEY)
            {
                clsDropDownBind d = new clsDropDownBind();
                d.Message = "";
                d.Error = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            try
            {
                MyClass cla = new MyClass();
                DataTable dtLd = cla.GetDataTable("SELECT  LandID, 'Survey No.'+SurveyNo712+' -- '+Convert(nvarchar(20),Hectare712)+' Hectare  .  '+ Convert(nvarchar(20),Are712)+' Are ' as Names FROM Tbl_M_RegistrationLand AS L WHERE  (RegistrationID =" + RegistrationID.Trim() + ") AND (ParentLandID = " + ParentLandID.Trim() + ") AND (IsDeleted IS NULL)");
                //  dt = Comcls.GetForm712Details(RegistrationID, LandID);
                for (int x = 0; x != dtLd.Rows.Count; x++)
                {
                    clsDropDownBind d = new clsDropDownBind();
                    d.KeyID = dtLd.Rows[x]["LandID"].ToString();//Survey No
                    d.Value = dtLd.Rows[x]["Names"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "";
                d.Value = "";
                d.Message = "";
                d.Error = ex.ToString();
                lst.Add(d);

            }




            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }

        [WebMethod]
        public void SubmitApplication(String SecurityKey, String RegistrationID, String ServeNo712, String ActivityID, String BalanceFundSource, String PastBenefitHistory, String AreaOfLandUsed)//, String ApprovalStageID
        {
            List<clsRetMessage> lst = new List<clsRetMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            String UpdateOnDate = cla.SvrDate();


            //clsRetMessage d11 = new clsRetMessage();
            //d11.MessageType = "Error";
            //d11.Message = clsSettings.StrCommanMessgae;
            //lst.Add(d11);

            //Context.Response.Clear();
            //Context.Response.ContentType = "application/json";
            ////Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            //Context.Response.Flush();
            //Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
            //return;



            if (MyCommanClassAPI.CheckApiAuthrization("56", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsRetMessage d111 = new clsRetMessage();
                d111.MessageType = "Error";
                d111.Message = "Authorization Failed";
                lst.Add(d111);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //------------validations ---------------------// 


            // Removed by Sohel -29-07-19
            //String SubComponentID = cla.GetExecuteScalar("select SubComponentID from Tbl_M_ActivityMaster where ActivityID=" + ActivityID + "");
            //String a = cla.GetExecuteScalar("select top 1 A.ApplicationID from Tbl_T_ApplicationDetails A inner join  Tbl_M_ActivityMaster AC on AC.ActivityID=A.ActivityID where A.IsDeleted is null and A.RegistrationID=" + RegistrationID.Trim() + " and A.ApplicationStatusID<>2 and AC.SubComponentID=" + SubComponentID + " ");
            //if (a.Length > 0)
            //{


            //    clsRetMessage d = new clsRetMessage();
            //    d.MessageType = "Error";
            //    d.Message = "You can only apply a single activity in each sub component";
            //    lst.Add(d);
            //    Context.Response.Clear();
            //    Context.Response.ContentType = "application/json";
            //    Context.Response.Flush();
            //    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
            //    return;
            //}


            String ActivityGroupID = cla.GetExecuteScalar("Select A.ActivityGroupID from Tbl_M_ActivityMaster A where A.ActivityID=" + ActivityID.ToString() + "");
            String MultipleApplicationAlow = cla.GetExecuteScalar("Select g.MultipleApplicationAlow from Tbl_M_ActivityMaster A inner join Tbl_M_Activity_Groups G on A.ActivityGroupID=G.ActivityGroupID where a.ActivityID=" + ActivityID.ToString() + " ");
            if (MultipleApplicationAlow.Length == 0) MultipleApplicationAlow = "NO";

            if (MultipleApplicationAlow.Trim().Length > 0)
            {
                if (MultipleApplicationAlow == "NO")
                {
                    String s = cla.GetExecuteScalar("SELECT top 1 ApplicationID FROM Tbl_T_ApplicationDetails WHERE  (RegistrationID =" + RegistrationID.ToString().Trim() + ") and IsDeleted is null  and ActivityGroupID=" + ActivityGroupID.ToString() + " and ApplicationStatusID not in (2,25) ");
                    if (s.Length > 0)
                    {
                        clsRetMessage d = new clsRetMessage();
                        d.MessageType = "Error";
                        d.Message = "Either Your Application for same activity on same land is already in process or you are not allow to do more than one application . Please check status of your application on your dashboard.";
                        lst.Add(d);
                        Context.Response.Clear();
                        Context.Response.ContentType = "application/json";
                        Context.Response.Flush();
                        Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                        return;
                    }
                }

            }




            //string s = cla.GetExecuteScalar("SELECT top 1 ApplicationID FROM Tbl_T_ApplicationDetails WHERE  (RegistrationID =" + RegistrationID.Trim() + ") and IsDeleted is null  and ActivityID=" + ActivityID + "  and  ApplicationStatusID not in (2,3)  ");
            //if (s.Length > 0)
            //{
            //    clsRetMessage d = new clsRetMessage();
            //    d.MessageType = "Error";
            //    d.Message = "Your Application for same activity on same land is already in process. Please check status of your application on your dashboard.";
            //    lst.Add(d);
            //    Context.Response.Clear();
            //    Context.Response.ContentType = "application/json";
            //    Context.Response.Flush();
            //    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
            //    return;
            //}


            //if (ServeNo712.Length > 0)
            //{
            //    s = cla.GetExecuteScalar("SELECT top 1 ApplicationID FROM Tbl_T_ApplicationDetails WHERE  (RegistrationID =" + RegistrationID.ToString().Trim() + ") and IsDeleted is null   and LandID=" + ServeNo712.Trim() + " and ActivityID=" + ActivityID.ToString() + "  and ApplicationStatusID not in (2,3)");
            //    if (s.Length > 0)
            //    {
            //        //clsMessages.Warningmsg(LiteralMsg, "");
            //        clsRetMessage d = new clsRetMessage();
            //        d.MessageType = "Error";
            //        d.Message = "Your Application for same activity on same land is already in process. Please check status of your application on your dashboard.";
            //        lst.Add(d);
            //        Context.Response.Clear();
            //        Context.Response.ContentType = "application/json";
            //        //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            //        Context.Response.Flush();

            //        Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
            //        return;
            //    }
            //}
            //else
            //{
            //    s = cla.GetExecuteScalar("SELECT top 1 ApplicationID FROM Tbl_T_ApplicationDetails WHERE  (RegistrationID =" + RegistrationID.ToString().Trim() + ") and IsDeleted is null  AND ActivityID=" + ActivityID.ToString() + "  and ApplicationStatusID not in (2,3)");
            //    if (s.Length > 0)
            //    {
            //        //clsMessages.Warningmsg(LiteralMsg, "Your Application for same activity on same land is already in process. Please check status of your application on your dashboard.");
            //        clsRetMessage d = new clsRetMessage();
            //        d.MessageType = "Error";
            //        d.Message = "Your Application for same activity is already in process. Please check status of your application on your dashboard.";
            //        lst.Add(d);
            //        Context.Response.Clear();
            //        Context.Response.ContentType = "application/json";
            //        //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            //        Context.Response.Flush();

            //        Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
            //        return;
            //    }
            //}

            //////////////
            int ApplicationID = cla.TableID("Tbl_T_ApplicationDetails", "ApplicationID");
            int ApplicationLogID = cla.TableID("Tbl_T_ApplicationDetails_Log", "ApplicationLogID");
            ///////


            String ApprovalStageID = cla.GetExecuteScalar("SELECT     top 1  Tbl_M_ActivityApprovalStage.ApprovalStageID FROM Tbl_M_ActivityApprovalStage INNER JOIN Tbl_M_ApprovalStages ON Tbl_M_ActivityApprovalStage.ApprovalStageID = Tbl_M_ApprovalStages.ApprovalStageID WHERE  (Tbl_M_ActivityApprovalStage.ActivityID =" + ActivityID.ToString().Trim() + ") AND (Tbl_M_ActivityApprovalStage.IsDeleted IS NULL) order by Tbl_M_ApprovalStages.OrderNo");

            String ApplicationCode = cla.GetExecuteScalar("SELECT   Tbl_M_VillageMaster.VillageCode FROM Tbl_M_RegistrationDetails INNER JOIN Tbl_M_VillageMaster ON dbo.Tbl_M_RegistrationDetails.VillageID = dbo.Tbl_M_VillageMaster.VillageID WHERE  (Tbl_M_RegistrationDetails.RegistrationID = " + RegistrationID.ToString().Trim() + ") ");
            ApplicationCode = ApplicationCode + "/" + cla.GetExecuteScalar("select ActivityCode from Tbl_M_ActivityMaster where ActivityID=" + ActivityID.ToString() + "").ToUpper();
            ApplicationCode = ApplicationCode + "/" + ApplicationID.ToString();

            DataTable dts = new DataTable();
            if (ServeNo712.Trim().Length > 0)
            {
                dts = cla.GetDataTable(" Select L.City_ID,L.TalukaID,L.VillageID,v.SubdivisionID,v.ClustersMasterID from Tbl_M_RegistrationLand as L inner join Tbl_M_VillageMaster v on v.VillageID=l.VillageID where L.LandID=(Select  ParentLandID from Tbl_M_RegistrationLand where LandID=" + ServeNo712.Trim() + " )");
            }
            else
            {
                dts = cla.GetDataTable("Select L.Work_City_ID,L.Work_TalukaID,L.Work_VillageID,v.SubdivisionID,v.ClustersMasterID from Tbl_M_RegistrationDetails as L inner join Tbl_M_VillageMaster v on v.VillageID=l.Work_VillageID where L.RegistrationID=" + RegistrationID.ToString().Trim() + " ");
            }

            if (dts.Rows.Count == 0)
            {
                clsRetMessage d = new clsRetMessage();
                d.MessageType = "Error";
                d.Message = "Error in data.";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }


            //////
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
                    // Tbl_T_ApplicationDetails
                    List<string> param = new List<string>();
                    param.Add(ApplicationID.ToString());//ApplicationID
                    param.Add(ApplicationCode.Trim());//ApplicationCode
                    param.Add(cla.mdy(cla.SvrDate()));//ApplicationDate
                    param.Add(RegistrationID.ToString());//RegistrationID

                    //  if (ServeNo712.Trim().ToString() > 0)
                    param.Add(ServeNo712.ToString().Trim());//LandID
                    //else
                    //{
                    //    param.Add("");
                    //}

                    param.Add(ActivityID.ToString());//ActivityID
                    param.Add("");//CropID
                    param.Add("");//EstimatedCost//txtEstimatedCost.Text.Trim()
                    param.Add("");//ExpectedFinAssistance//txtExpectedCost.Text.Trim()
                    param.Add(BalanceFundSource.Trim());//BalanceFundSource
                    param.Add(PastBenefitHistory.Trim());//PastBenefitHistory
                    param.Add("1");// ApplicationStatusID                                 
                    param.Add("I");
                    cla.ExecuteByProcedure("SP_T_ApplicationDetails", param, command);

                    //Tbl_T_ApplicationDetails_Log

                    param.Add(ApplicationLogID.ToString());//ApplicationLogID
                    param.Add(ApplicationID.ToString());//ApplicationID
                    param.Add("");//LogDetails
                    param.Add("1");//ApplicationStatusID 
                    param.Add(RegistrationID.ToString());//UpdateByRegID
                    param.Add("I");
                    cla.ExecuteByProcedure("SP_T_ApplicationDetails_Log", param, command);
                    param.Clear();


                    cla.ExecuteCommand("UPDATE Tbl_T_ApplicationDetails SET  AreaOfLandUsed='" + AreaOfLandUsed.Trim() + "' WHERE (ApplicationID =" + ApplicationID.ToString().Trim() + ")", command);
                    //Set ApprovalStageID =" + ApprovalStageID + " ,

                    foreach (DataRow dr in dts.Rows)
                    {

                        cla.ExecuteCommand("UPDATE Tbl_T_ApplicationDetails SET   APP_City_ID=" + dr["City_ID"].ToString() + ",APP_TalukaID=" + dr["TalukaID"].ToString() + ",APP_VillageID=" + dr["VillageID"].ToString() + ",APP_ClustersMasterID=" + dr["ClustersMasterID"].ToString() + ",APP_SubdivisionID=" + dr["SubdivisionID"].ToString() + "  WHERE (ApplicationID =" + ApplicationID.ToString().Trim() + ")", command);
                    }

                    transaction.Commit();
                    clsRetMessage d = new clsRetMessage();
                    d.MessageType = "Sucess";
                    d.RegistrationID = RegistrationID.ToString();
                    d.Message = "Record Saved Sucessfully";
                    d.ApplicationID = ApplicationID.ToString();
                    d.ApplPrint = @"/admintrans/Popup/PreLetters/ApplicationPrint.aspx?ID=";
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

                    clsRetMessage d = new clsRetMessage();
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


        [WebMethod]
        public string[] GetFarmers(string prefix, String ID, String Lan)
        {
            List<string> customers = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = clsSettings.strCoonectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    if (Lan.Trim() == "en-IN")
                    {
                        cmd.CommandText = "Select top 20 RegisterName +' / '+ RegistrationNo+'/'+CONVERT(nvarchar(50),RegistrationID) as RegisterName from Tbl_M_RegistrationDetails where IsDeleted is null and RegisterName like @SearchText + '%' order by RegisterName ";
                        cmd.Parameters.AddWithValue("@SearchText", prefix);
                    }


                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(sdr["RegisterName"].ToString());
                        }
                    }
                    conn.Close();
                }
                return customers.ToArray();
            }
        }


        [WebMethod]
        public string[] GetFarmerRegistrationNo(string prefix, String ID, String Lan)
        {
            List<string> customers = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = clsSettings.strCoonectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    if (Lan.Trim() == "en-IN")
                    {
                        cmd.CommandText = "Select top 20 RegisterName +' / '+ RegistrationNo+'/'+CONVERT(nvarchar(50),RegistrationID) as RegisterName from Tbl_M_RegistrationDetails where IsDeleted is null and RegistrationNo like @SearchText + '%' order by RegisterName ";
                        cmd.Parameters.AddWithValue("@SearchText", prefix);
                    }


                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(sdr["RegisterName"].ToString());
                        }
                    }
                    conn.Close();
                }
                return customers.ToArray();
            }
        }
    }
}


//------------validations ---------------------// 



public class clsReturnMessage
{
    public String RegistrationID { get; set; }
    public String MessageType { get; set; }
    public string Message { get; set; }
    public String WorkReportID { get; set; }
}
//WorkCompletionUpdation
public class clsRetnMessage
{
    public String RegistrationID { get; set; }
    public String MessageType { get; set; }
    public string Message { get; set; }
    public String WorkReportID { get; set; }
    public String WorkCompletionID { get; set; }
    public String DocumentUploaded { get; set; } //cla.mdy(CompletionDate.Trim())
    public String RequestNo { get; set; }
    public String RequestDate { get; set; }
}
//SubmitApplication
public class clsRetMessage
{
    public String RegistrationID { get; set; }
    public String MessageType { get; set; }
    public string Message { get; set; }
    public String ApplicationID { get; set; }
    public String ApplPrint { get; set; }
}
public class cls8A
{

    //public string ReturnData { get; set; }
    public string LandID { get; set; }
    public string LANDDISTRICT { get; set; }
    public string City_ID { get; set; }
    public string LANDTALUKA { get; set; }
    public string TalukaID { get; set; }
    public string LANDVILLAGE { get; set; }
    public string VillageID { get; set; }
    public string SURVEYNo8A { get; set; }
    public string Hectare8A { get; set; }
    public string Are8A { get; set; }
    public string FileFORM8A { get; set; }
    public List<cls712> cls712 { get; set; }
    public string Message { get; set; }
    public String UpDateLink { get; set; }
}
public class cls712
{
    public string LandID { get; set; }
    public string SURVEYNo8A { get; set; }
    public string SurveyNo712 { get; set; }
    public string Hectare712 { get; set; }
    public string Are712 { get; set; }
    public string File712 { get; set; }
    public string Message { get; set; }
    public String UpDateLink { get; set; }
}

public class RegistrationData
{
    public string AadharNo { get; set; }
    public string Name { get; set; }
    public string DOB { get; set; }
    public string Gender { get; set; }
    public string CATEGORY { get; set; }
    public string BeneficiaryTypesID { get; set; }
    public string FileCATEGORYCERITIFICATE { get; set; }
    public string HANDICAP { get; set; }
    public string FileHANDICAPCERITIFICATE { get; set; }
    public string DISABILITYPer { get; set; }
    public string Address1HouseNo { get; set; }
    public string Address1StreetName { get; set; }
    public string Address1City_ID { get; set; }
    public string Address1TalukaID { get; set; }
    public string Address1Post_ID { get; set; }
    public string Address1PinCode { get; set; }
    public string Address1VillageID { get; set; }
    public string CLUSTERCODE { get; set; }
    public string MobileNumber { get; set; }
    public string MobileNumber2 { get; set; }
    public string LandLineNumber { get; set; }
    public string EmailId { get; set; }
    public string PanNumber { get; set; }
    public string Category { get; set; }
    public string PhysicalHandicap { get; set; }
    public string LandStatus { get; set; }
    public string LandLessCertificate { get; set; }
    public string ApprovalStatus { get; set; }
    public string RegistrationDate { get; set; }
    public string RelatedToBeneficiaryCriteria { get; set; }
    public string BeneficiaryCriteriaCertificate { get; set; }
    //public string Hectare8A { get; set; }
    //public string Are8A { get; set; }
    //public string FileFORM8A { get; set; }
    //public string SurveyNo712 { get; set; }
    //public string Hectare712 { get; set; }
    //public string Are712 { get; set; }
    //public string File712 { get; set; }
    //  public string RegistrationID { get; set; }
    public string Message { get; set; }
}
public class Activity
{

    public string ActivityName { get; set; }
    public string ComponentID { get; set; }
    public string SubComponentID { get; set; }
    public string ActivityID { get; set; }
    public string BeneficiaryTypesID { get; set; }
    public string ActivityCode { get; set; }
    public string ActivityImagePath { get; set; }
    public string ComponentName { get; set; }
    public string SubComponentName { get; set; }
    public string SubsidyAmt { get; set; }
    public string MaxSubsidyAmt { get; set; }
    public string SubsidyPer { get; set; }
    public string Message { get; set; }


}

public class ActivityGroup
{

    public string ActivityGroupName { get; set; }
    public string ActivityGroupID { get; set; }
    public string ActivityGroupCode { get; set; }
    public string ActivityGroupImagePath { get; set; }
    public string Message { get; set; }


}
public class FPOOtherRegistrationData
{
    public string FPORegistrationNo { get; set; }
    public string RegisterName { get; set; }
    public string BeneficiaryTypesID { get; set; }
    public string RegistrationDate { get; set; }
    public string RegistrationCertifecate { get; set; }
    public string RegisterUnderID { get; set; }
    public string RegisteredThroughID { get; set; }
    public string PromoterName { get; set; }
    public string PromoterGender { get; set; }
    public string Promotermobile { get; set; }
    public string Promoterlandline { get; set; }
    public string PromoterEmail { get; set; }
    public string CeoName { get; set; }
    public string CeoGender { get; set; }
    public string CeoMobile { get; set; }
    public string CeoLandline { get; set; }
    public string CeoEmail { get; set; }
    public string CeoAuthorisedPerson { get; set; }
    public string CeoDesignation { get; set; }
    public string AuthorisedCertifecate { get; set; }
    public string CeoAuthorisedPersonGen { get; set; }
    public string CeoAuthorisedPersonMob { get; set; }
    public string CeoAuthorisedPersonEmail { get; set; }
    public string Address1HouseNo { get; set; }
    public string Address1StreetName { get; set; }
    public string Address1City_ID { get; set; }
    public string Address1TalukaID { get; set; }
    public string Address1Post_ID { get; set; }
    public string Address1PinCode { get; set; }
    public string Address1VillageID { get; set; }
    public string Clusters { get; set; }
    public string MobileNumber { get; set; }
    public string MobileNumber2 { get; set; }
    public string LandLineNumber { get; set; }
    public string EmailID { get; set; }
    public string PanNumber { get; set; }
    public string IsBothAddressSame { get; set; }
    public string Address2HouseNo { get; set; }
    public string Address2StreetName { get; set; }
    public string Address2City_ID { get; set; }
    public string Address2TalukaID { get; set; }
    public string Address2Post_ID { get; set; }
    public string Address2PinCode { get; set; }
    public string Address2VillageID { get; set; }
    public string Address2Clusters { get; set; }
    public string Address2Mob1 { get; set; }
    public string Address2Mob2 { get; set; }
    public string Address2LandLine { get; set; }
    public string ApprovalStatus { get; set; }
    public string RegistrationID { get; set; }
    public string Message { get; set; }
}
public class Dashboard
{
    public string ApplicationID { get; set; }

    public string ApplicationCode { get; set; }
    public string Date { get; set; }
    public string Activity { get; set; }
    public string ActivityCode { get; set; }
    public string ActivityImage { get; set; }
    public string Component { get; set; }
    public string BeneficiaryTypes { get; set; }
    public string ApprovalStageID { get; set; }
    public string Stages { get; set; }
    public string Status { get; set; }
    public string RegisterName { get; set; }
    public string Message { get; set; }//
    public string PreSanctionLetter { get; set; }

    public List<ApplicationLogDetails> log { get; set; }

}


public class DashboardWithFlag
{
    public string ApplicationID { get; set; }

    public string ApplicationCode { get; set; }
    public string Date { get; set; }
    public string Activity { get; set; }
    public string ActivityCode { get; set; }
    public string ActivityImage { get; set; }
    public string Component { get; set; }
    public string BeneficiaryTypes { get; set; }
    public string ApprovalStageID { get; set; }
    public string Stages { get; set; }
    public string Status { get; set; }
    public string RegisterName { get; set; }
    public string Message { get; set; }//
    public string PreSanctionLetter { get; set; }

    public bool ShowPaymentBtn { get; set; }

    public List<ApplicationLogDetails> log { get; set; }

}

public class ApplicationLogDetails
{
    public string PreArvDate { get; set; }
    public string UpdatedBy { get; set; }
    public string Level { get; set; }
    public string PreStage { get; set; }
    public string PreStatus { get; set; }
    public string Remark { get; set; }
    public string Reason { get; set; }
}
public class ApplicationSt
{
    public string RegistrationID { get; set; }
    public string ApplicationID { get; set; }
    public string Date { get; set; }
    public string BeneficiaryTypesID { get; set; }
    public string BeneficiaryTypes { get; set; }
    public string RegisterName { get; set; }
    public string Activity { get; set; }
    public string ActivityCode { get; set; }
    public string Component { get; set; }
    public string SubComponentName { get; set; }
    public string ApprovalStages { get; set; }
    public string APPStatus { get; set; }
    public string RAppStatus { get; set; }
    public string LandHect { get; set; }
    public string AssignTo { get; set; }
    public string Message { get; set; }
    public String AadharlinkStatus { get; set; }

    public String BtnStatus { get; set; }

}

//FarmerProfileDetails
public class FarmerProDetails
{
    public string AadharNo { get; set; }
    public string Name { get; set; }
    public string DOB { get; set; }
    public string Gender { get; set; }
    public string CATEGORY { get; set; }
    public string CategoryMaster { get; set; }
    public string BeneficiaryTypesID { get; set; }
    public string FileCATEGORYCERITIFICATE { get; set; }
    public string HANDICAP { get; set; }
    public string FileHANDICAPCERITIFICATE { get; set; }
    public string DISABILITYPer { get; set; }
    public string Address1HouseNo { get; set; }
    public string Address1StreetName { get; set; }
    public string Address1City_ID { get; set; }
    public string CityName { get; set; }
    public string Address1TalukaID { get; set; }
    public string Taluka { get; set; }
    public string Address1Post_ID { get; set; }
    public string PostName { get; set; }
    public string Address1PinCode { get; set; }
    public string Address1VillageID { get; set; }
    public string VillageName { get; set; }
    public string CLUSTERCODE { get; set; }
    public string MobileNumber { get; set; }
    public string MobileNumber2 { get; set; }
    public string LandLineNumber { get; set; }
    public string EmailId { get; set; }
    public string PanNumber { get; set; }
    public string Category { get; set; }
    public string PhysicalHandicap { get; set; }
    public string LandStatus { get; set; }
    public string LandLessCertificate { get; set; }
    public string ApprovalStatus { get; set; }
    public string RegistrationDate { get; set; }
    //LandDetails
    public string LandID { get; set; }
    public string LANDDISTRICT { get; set; }
    public string City_ID { get; set; }
    public string LANDTALUKA { get; set; }
    public string TalukaID { get; set; }
    public string LANDVILLAGE { get; set; }
    public string VillageID { get; set; }
    public string SURVEYNo8A { get; set; }
    public string Hectare8A { get; set; }
    public string Are8A { get; set; }
    public string FileFORM8A { get; set; }
    public string Form8ADoc { get; set; }
    public string Form8ADoc2 { get; set; }
    public string RelatedToBeneficiaryCriteria { get; set; }
    public string BeneficiaryCriteriaCertificate { get; set; }
    public string RegistrationID { get; set; }
    public string Message { get; set; }
}

public class RemoveLandD
{
    public string LandID { get; set; }
    public string Message { get; set; }
    public string MessageType { get; set; }
    public String LandStatus { get; set; }
}
//Fpo profile
public class FPOFPCProfDetails
{
    public string FPORegistrationNo { get; set; }
    public string RegisterName { get; set; }
    public string BeneficiaryTypesID { get; set; }
    public string BeneficiaryTypes { get; set; }
    public string RegistrationDate { get; set; }
    public string RegistrationCertifecate { get; set; }
    public string RegisterUnderID { get; set; }
    public string RegisterUnder { get; set; }
    public string RegisteredThroughID { get; set; }
    public string RegisteredThrough { get; set; }
    public string PromoterName { get; set; }
    public string PromoterGender { get; set; }
    public string Promotermobile { get; set; }
    public string Promoterlandline { get; set; }
    public string PromoterEmail { get; set; }
    public string CeoName { get; set; }
    public string CeoGender { get; set; }
    public string CeoMobile { get; set; }
    public string CeoLandline { get; set; }
    public string CeoEmail { get; set; }
    public string CeoAuthorisedPerson { get; set; }
    public string CeoDesignation { get; set; }
    public string AuthorisedCertifecate { get; set; }
    public string CeoAuthorisedPersonGen { get; set; }
    public string CeoAuthorisedPersonMob { get; set; }
    public string CeoAuthorisedPersonEmail { get; set; }
    public string Address1HouseNo { get; set; }
    public string Address1StreetName { get; set; }
    public string Address1City_ID { get; set; }
    public string CityName { get; set; }
    public string Address1TalukaID { get; set; }
    public string Taluka { get; set; }
    public string Address1Post_ID { get; set; }
    public string PostName { get; set; }
    public string Address1PinCode { get; set; }
    public string Address1VillageID { get; set; }
    public string VillageName { get; set; }
    public string Clusters { get; set; }
    public string MobileNumber { get; set; }
    public string MobileNumber2 { get; set; }
    public string LandLineNumber { get; set; }
    public string EmailID { get; set; }
    public string PanNumber { get; set; }
    public string IsBothAddressSame { get; set; }
    public string Address2HouseNo { get; set; }
    public string Address2StreetName { get; set; }
    public string Address2City_ID { get; set; }
    public string Address2TalukaID { get; set; }
    public string Address2Post_ID { get; set; }
    public string Address2PinCode { get; set; }
    public string Address2VillageID { get; set; }
    public string Address2Clusters { get; set; }
    public string Address2Mob1 { get; set; }
    public string Address2Mob2 { get; set; }
    public string Address2LandLine { get; set; }
    public string ApprovalStatus { get; set; }
    public string RegistrationID { get; set; }
    public string Message { get; set; }
}

//CommunityProfile
public class CommunityProDetails
{

    public string GramPanchayatCode { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }
    public string GramPanchayatMobile { get; set; }
    public string GramPanchayatEmail { get; set; }
    public string BeneficiaryTypesID { get; set; }
    public string BeneficiaryTypes { get; set; }
    public string Address1HouseNo { get; set; }
    public string Address1StreetName { get; set; }
    public string Address1City_ID { get; set; }
    public string CityName { get; set; }
    public string Address1TalukaID { get; set; }
    public string Taluka { get; set; }
    public string Address1Post_ID { get; set; }
    public string PostName { get; set; }
    public string Address1PinCode { get; set; }
    public string Address1VillageID { get; set; }
    public string VillageName { get; set; }
    public string CLUSTERCODE { get; set; }
    public string MobileNumber { get; set; }
    public string MobileNumber2 { get; set; }
    public string LandLineNumber { get; set; }
    public string EmailId { get; set; }
    public string PanNumber { get; set; }
    public string ApprovalStatus { get; set; }
    public string RegistrationDate { get; set; }
    public string BankAccountNo { get; set; }
    public string BankAccountHolder { get; set; }
    public string NameOFbank { get; set; }
    public string BranchName { get; set; }
    public string IFSCCode { get; set; }
    public string RBIBankID { get; set; }
    public string RegistrationID { get; set; }
    public string Message { get; set; }
}

//ApplicationDetails
public class ApplicationDetails
{
    public string RegistrationID { get; set; }
    public string ApplicationID { get; set; }
    public string Date { get; set; }
    public string BeneficiaryTypes { get; set; }
    public string RegisterName { get; set; }
    public string Activity { get; set; }
    public string ActivityCode { get; set; }
    public string Component { get; set; }
    public string SubComponentName { get; set; }//
    public string ActivityImagePath { get; set; }
    public string BalanceFundSource { get; set; }
    public string ApprovalStages { get; set; }
    public string APPStatus { get; set; }
    public string PastBenefitHistory { get; set; }
    public string LANDVILLAGE { get; set; }
    public string Hectare8A { get; set; }
    public string SURVEYNo712 { get; set; }
    public string Hectare712 { get; set; }
    public string Are712 { get; set; }
    public string Message { get; set; }
    public string BanklinkStatus { get; set; }
}


public class BackToBenefic
{
    public string ReasonID { get; set; }
    public string Reasons { get; set; }
    public string Message { get; set; }
}
public class UploadImage
{
    public string GeofencingID { get; set; }
    public string ImageTitleName { get; set; }
    public string LatitudeMap { get; set; }
    public string LongitudeMap { get; set; }
    public string Remarks { get; set; }
    public string CoordinateAddress { get; set; }
    public string CoordinateImage { get; set; }
    public string FullName { get; set; }
    public string onMap { get; set; }

    public string Message { get; set; }
}
//SanctionDesk4
public class DataSanctionDesk
{
    public string RegistrationID { get; set; }
    public string ApplicationID { get; set; }
    public string BeneficiaryTypesID { get; set; }
    public string BeneficiaryTypes { get; set; }
    public string RegisterName { get; set; }
    public string Activity { get; set; }
    public string ActivityCode { get; set; }
    public String Notes { get; set; }
    public string ComponentName { get; set; }
    public string SubComponentName { get; set; }
    public string ApplicationDate { get; set; }
    public string ApplicationStatus { get; set; }
    public string RequestNumber { get; set; }
    public string RequestDate { get; set; }
    public string RequestAmount { get; set; }
    public string WorkReportID { get; set; }
    public string ApprovalStages { get; set; }
    public string InspectorAmount { get; set; }
    public string Message { get; set; }
    public string CoordinatesDifference { get; set; }
    public string InsPectionDoc { get; set; }
    public String AadharlinkStatus { get; set; }
    public String PopupLink { get; set; }
    public String IsShoweThibak { get; set; }
    public String CanCulateOnPercent { get; set; }
    public List<ActivityPaymentTerm> PaymentTerm { get; set; }
    // List<ActivityPaymentTerm> PaymentTerm = new List<ActivityPaymentTerm>();
}

public class ActivityPaymentTerm
{
    public string PaymentTerm { get; set; }
    public string Amount { get; set; }
    public string Notes { get; set; }
}

public class ActivityDetails
{
    public string PaymentTermName { get; set; }
    public string SubsidyPerComm { get; set; }
    public string SubsidyAmtComm { get; set; }
    public string SubsidyPerScST { get; set; }
    public string SubsidyAmtScST { get; set; }
    // public string SubsidyPerWoman { get; set; }
    // public string SubsidyAmtWoman { get; set; }

    public string Message { get; set; }
}

public class ActivityExecution
{
    public string Stages { get; set; }
    public string ExecutionTimeinDay { get; set; }
    public string Message { get; set; }
}
public class ActivityRequired
{
    public string ActivityName { get; set; }
    public string ActivityCode { get; set; }
    public string Message { get; set; }
    public List<Eligibility> EligibilityCriateria { get; set; }
    public List<Requirement> DocumentDetails { get; set; }
    public List<Feasibility> FeasibilityDetails { get; set; }
}
public class Eligibility
{
    public string EligibilityCriateria { get; set; }

}
public class Requirement
{
    public string DocumentDetails { get; set; }
}
public class Feasibility
{
    public string FeasibilityDetails { get; set; }
}
public class PaymentRequest
{

    public string Activity { get; set; }
    public string ActivityCode { get; set; }
    public string ApplicationDate { get; set; }
    public string RequestNumber { get; set; }
    public string RequestDate { get; set; }
    public string TotalAmtByBen { get; set; }
    public string TotalAmtByVCRMC { get; set; }
    public string FinalAmtApproved { get; set; }
    public string WorkReportID { get; set; }
    public string btnFarmer { get; set; }
    public string Message { get; set; }
}

public class Level
{
    public string ProcurementBill { get; set; }
    public string PreSanctionLetter { get; set; }
    public string Message { get; set; }
}

public class WorkCompletionData
{
    public string WorkCompletionID { get; set; }
    public string DocTypes { get; set; }
    public string DocLevels { get; set; }
    public string DocumentDetails { get; set; }
    public string CompletionDate { get; set; }
    public string DocumentUploaded { get; set; }

    public string Message { get; set; }


    public string PMURemark { get; set; }
}
public class RemoveWorkUpdate
{
    public string WorkReportID { get; set; }
    public string Message { get; set; }
}

public class InspectionData
{
    public string WorkCompletionID { get; set; }
    public string DocTypes { get; set; }
    public string DocLevels { get; set; }
    public string DocumentDetails { get; set; }
    public string CompletionDate { get; set; }
    public string DocumentUploaded { get; set; }//IsInspection, LatitudeMap,LongitudeMap
    public string LatitudeMap { get; set; }
    public string LongitudeMap { get; set; }
    public string IsInspection { get; set; }
    public string Message { get; set; }
}