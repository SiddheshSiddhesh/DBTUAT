using CommanClsLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace DBTPoCRA.APPData
{
    /// <summary>
    /// Summary description for GeographicAPI
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class GeographicAPI : System.Web.Services.WebService
    {

        [WebMethod]
        public void SaveGeofencing(String SecurityKey, String UserID, String ApplicationID, String LatitudeMap, String LongitudeMap, String CoordinateAddress, String CoordinateImage,String Remark,String ImageTitleID)
        {
            CoordinateAddress = CoordinateAddress.Trim().Replace("'", "");
            List<clsReturnMessage> lst = new List<clsReturnMessage>();
            MyCommanClass Comcls = new MyCommanClass();
            MyClass cla = new MyClass();

            if (EncryptDecryptQueryString.Decrypt(SecurityKey) != clsSettings.APIKEY)
            {
                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Error";
                d.Message = "Authorization Failed";
                lst.Add(d);

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            //------------validations ---------------------// 
            int GeofencingID = cla.TableID("Tbl_T_ApplicationGeofencing", "GeofencingID");
            String RegistrationID = cla.GetExecuteScalar("select RegistrationID from Tbl_T_ApplicationDetails where ApplicationID=" + ApplicationID.ToString() + "").ToUpper();

            //String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "");
            String PathUp = "DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "";
            AzureBlobHelper fileRet = new AzureBlobHelper();

            String LocationImg = "";
            if (RegistrationID.ToString().Trim().Length > 0)
            {
                if (CoordinateImage.Length > 0)
                {
                    LocationImg = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "/LocationImg_" + GeofencingID.ToString() + MyCommanClass.GetFileExtension(CoordinateImage);
                    string imageName = "LocationImg_" + GeofencingID.ToString() + MyCommanClass.GetFileExtension(CoordinateImage);
                    //set the image path
                    //string imgPath = path + imageName;
                    //byte[] imageBytes = Convert.FromBase64String(CoordinateImage);
                    //File.WriteAllBytes(imgPath, imageBytes);

                    byte[] imageBytes = Convert.FromBase64String(CoordinateImage);
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


                    if (RegistrationID.ToString().Trim().Length > 0)
                    { 
                        //cla.ExecuteCommand(" Update Tbl_T_ApplicationGeofencing Set IsDeleted='1'  where ApplicationID="+ ApplicationID + " ",command);

                        String str = "INSERT  INTO Tbl_T_ApplicationGeofencing (GeofencingID, ApplicationID, LatitudeMap, LongitudeMap, CoordinateAddress, CoordinateImage, UpdateOnDate, UpdateByRegID, Remarks,ImageTitleID)";
                        str += " VALUES("+ GeofencingID + ", " + ApplicationID+", '"+LatitudeMap+"', '"+LongitudeMap + "', '" + CoordinateAddress + "', '" + LocationImg + "', '" + cla.mdy(cla.SvrDate()) + "', '" + UserID + "', N'" + Remark + "',"+ ImageTitleID + ")";
                        cla.ExecuteCommand(str, command);
                    }







                    transaction.Commit();
                    //SendSmsOnPrePostSpotVerification
                    String Mob = cla.GetExecuteScalar("SELECT   MobileNumber  FROM Tbl_M_RegistrationDetails where RegistrationID=" + RegistrationID.ToString() + "");
                    String ApplicationNo = cla.GetExecuteScalar("select ApplicationCode from Tbl_T_ApplicationDetails where ApplicationID=" + ApplicationID.ToString() + "").ToUpper();
                    SMS.SendSmsOnPrePostSpotVerification(Mob, ApplicationNo.Trim());
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
            Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
        }
    }

    public class clsReturnMessage2
    {
        public String RegistrationID { get; set; }
        public String MessageType { get; set; }
        public string Message { get; set; }

    }
}
