using CommanClsLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace DBTPoCRA.APPData
{
    /// <summary>
    /// Summary description for ApplicationDetailSD
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ApplicationDetailSD : System.Web.Services.WebService
    {

        #region 'Data Close Application'
        [WebMethod]
        public void GetClosedApplication(String SecurityKey, String RegistrationID)
        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<CloseAppl> lst = new List<CloseAppl>();
            if (MyCommanClassAPI.CheckApiAuthrization("77", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                CloseAppl d = new CloseAppl();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
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
                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_Get_ApplicationGrdDataClosed", apl);


                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    CloseAppl Sts = new CloseAppl();

                    Sts.ActivityName = dt.Rows[z]["ActivityName"].ToString().Trim();

                    Sts.ActivityCode = dt.Rows[z]["ActivityCode"].ToString().Trim();
                    Sts.ComponentName = dt.Rows[z]["ComponentName"].ToString().Trim();
                    Sts.Date = dt.Rows[z]["ApplicationDate"].ToString().Trim();
                    Sts.ApplicationID = dt.Rows[z]["ApplicationID"].ToString().Trim();
                    List<AppReport> AppRep = new List<AppReport>();

                    List<PaymentReq> PayReq = new List<PaymentReq>();

                    apl.Add(dt.Rows[z]["ApplicationID"].ToString());


                    DataTable dtL = cla.GetDtByProcedure("SP_Application_Log", apl);

                    for (int y = 0; y != dtL.Rows.Count; y++)
                    {
                        AppReport Al = new AppReport();



                        Al.Stage = dtL.Rows[y]["ApprovalStages"].ToString().Trim();
                        Al.UpdatedBy = dtL.Rows[y]["FullName"].ToString().Trim();
                        Al.Level = dtL.Rows[y]["Desig_Name"].ToString().Trim();
                        Al.Status = dtL.Rows[y]["ApplicationStatus"].ToString().Trim();
                        Al.Reason = dtL.Rows[y]["Reason"].ToString().Trim();
                        Al.Remark = dtL.Rows[y]["Remark"].ToString().Trim();
                        AppRep.Add(Al);
                    }

                    apl.Add(dt.Rows[z]["ApplicationID"].ToString());

                    DataTable dtC = cla.GetDtByProcedure("SP_GetWorkCompletionRequests", apl);
                    for (int x = 0; x != dtC.Rows.Count; x++)
                    {
                        PaymentReq C = new PaymentReq();

                        C.RequestNo = dtC.Rows[x]["RequestNo"].ToString().Trim();
                        C.RequestAmount = dtC.Rows[x]["TotalAmtByBen"].ToString().Trim();
                        C.TotalAmtByBen = dtC.Rows[x]["TotalAmtByVCRMC"].ToString().Trim();
                        C.FinalAmtApproved = dtC.Rows[x]["FinalAmtApproved"].ToString().Trim();
                        C.ApprovalStagesFarmer = dtC.Rows[x]["ApprovalStagesFarmer"].ToString().Trim();
                        C.ApprovalStage = dtC.Rows[x]["ApprovalStage"].ToString().Trim();
                        PayReq.Add(C);
                    }
                    Sts.AppRep = AppRep;
                    Sts.PayReq = PayReq;

                    lst.Add(Sts);
                }
            }

            catch (Exception ex)
            {
                CloseAppl Sts = new CloseAppl();
                Sts.Message = ex.ToString();

                lst.Add(Sts);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }
        #endregion

        [WebMethod]
        public void SendForReVerification(String SecurityKey, String RegistrationID, String ReasonID)//, String Remarkany//UpdateByRegID
        {
            List<clsRtnMessage> lst = new List<clsRtnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            String UpdateOnDate = cla.SvrDate();
            if (MyCommanClassAPI.CheckApiAuthrization("75", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsRtnMessage d = new clsRtnMessage();
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



            if (RegistrationID.Trim().Length == 0)
            {

                clsRtnMessage d = new clsRtnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill RegistrationID";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
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


                    Int32 x1 = 0;
                    String[] Reas = ReasonID.Trim().Split('/');
                    for (int x = 0; x != Reas.Length; x++)
                    {

                        if (ReasonID == "")
                        {
                            x1++;
                        }

                    }

                    if (x1 == 0)
                    {
                        String str = " UPDATE Tbl_M_RegistrationDetails SET ApprovalStatus='Pending'  WHERE RegistrationID=" + RegistrationID.ToString() + "";
                        if (cla.ExecuteCommand(str).Length == 0)
                        {
                            clsRtnMessage d = new clsRtnMessage();
                            d.MessageType = "Sucess";
                            d.RegistrationID = RegistrationID.ToString();
                            d.Message = "Record Saved Sucessfully";
                            lst.Add(d);
                        }
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

                    clsRtnMessage d = new clsRtnMessage();
                    d = new clsRtnMessage();
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

        
        #region " Application for PreSanctionDesk1 (1PreDeskOne) with Paging"

        [WebMethod]
        public void GetDataPreSanctionDesk1Paging(String SecurityKey, String UserID, String ApprovalStatus, String FromRegistrationDate, String ToRegistrationDate, String ComponentID, String SubComponentID, String ActivityCategoryID, String ActivityID, String GPCOde, String BeneficiaryName, String PageSize, String PageNumber)

        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<PreDeskData> lst = new List<PreDeskData>();

            if (MyCommanClassAPI.CheckApiAuthrization("76", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                PreDeskData d = new PreDeskData();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
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
                apl.Add("");
                apl.Add(cla.mdy(FromRegistrationDate.Trim()));
                apl.Add(cla.mdy(ToRegistrationDate.Trim()));
                apl.Add(ApprovalStatus.Trim());
                apl.Add(ComponentID.Trim());
                apl.Add(SubComponentID.Trim());
                apl.Add(ActivityCategoryID.Trim());
                apl.Add(ActivityID.Trim());
                apl.Add("2");
                apl.Add(UserID.Trim());
                apl.Add(GPCOde.Trim());//
                apl.Add(BeneficiaryName.Trim());//
                apl.Add(intPageSize.ToString());
                apl.Add(intPageNumber.ToString());
                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_Get_ApplicationGrdData_Paging", apl);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    PreDeskData Sts = new PreDeskData();

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
                    Sts.RegistrationID = dt.Rows[z]["RegistrationID"].ToString().Trim();
                    Sts.LandHect = dt.Rows[z]["TotalLand"].ToString().Trim();
                    Sts.AssignTo = dt.Rows[z]["AssignTo"].ToString();
                    Sts.AadharlinkStatus = dt.Rows[z]["AadharlinkStatus"].ToString();
                    Sts.BtnStatus = dt.Rows[z]["BtnStatus"].ToString();
                    lst.Add(Sts);
                }
            }

            catch (Exception ex)
            {
                PreDeskData Sts = new PreDeskData();
                Sts.Message = ex.ToString();

                lst.Add(Sts);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }

        #endregion


        //
        [WebMethod]
        public void UpdatePreDeskOne(String SecurityKey, String UserId, String ApplicationID, String ReasonID, String ApplicationStatusID, String LogDetails, String ApplicationMeetingID)//, String Remarkany//UpdateByRegID
        {
            List<clsRtnMessage> lst = new List<clsRtnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            String UpdateOnDate = cla.SvrDate();

            //clsRtnMessage dd = new clsRtnMessage();
            //dd.MessageType = "Error";
            //dd.Message = clsSettings.StrCommanMessgae;
            //lst.Add(dd);

            //Context.Response.Clear();
            //Context.Response.ContentType = "application/json";
            ////Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            //Context.Response.Flush();
            //Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
            //return;

            if (MyCommanClassAPI.CheckApiAuthrization("74", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsRtnMessage d = new clsRtnMessage();
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



            if (ApplicationID.Trim().Length == 0)
            {

                clsRtnMessage d = new clsRtnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill ApplicationID";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            String str = "";
            int ApplicationLogID = cla.TableID("Tbl_T_ApplicationDetails_Log", "ApplicationLogID");
            int ApplicationLogReasonID = cla.TableID("Tbl_T_ApplicationDetails_LogReason", "ApplicationLogReasonID");

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


                    // UPDATE
                    str = " INSERT INTO Tbl_T_ApplicationDetails_Log (ApplicationLogID, ApplicationID, LogDetails, ApplicationStatusID, ApprovalStageID, UpdateOnDate, UpdateByRegID,ApplicationMeetingID)";
                    str += " VALUES(" + ApplicationLogID + "," + ApplicationID.Trim() + ",'" + LogDetails.Trim() + "'," + ApplicationStatusID.Trim() + ",2,'" + cla.mdy(cla.SvrDate()) + "'," + UserId.Trim() + "," + ApplicationMeetingID.Trim() + ")";
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

                            str = " INSERT INTO Tbl_T_ApplicationDetails_LogReason (ApplicationLogReasonID, ApplicationLogID, ReasonID)";
                            str += " VALUES(" + ApplicationLogReasonID + "," + ApplicationLogID + "," + ReasonID.Trim() + ")";
                            cla.ExecuteCommand(str, command);
                            ApplicationLogReasonID++;
                        }
                    }

                    if (ApplicationStatusID.Trim() == "5")
                    {
                        str = " UPDATE Tbl_T_ApplicationDetails SET ApprovalStageID=3   , ApplicationStatusID=1  WHERE ApplicationID=" + ApplicationID.Trim() + "";
                    }
                    else
                    {
                        str = " UPDATE Tbl_T_ApplicationDetails SET ApprovalStageID=2   , ApplicationStatusID='" + ApplicationStatusID.Trim() + "'  WHERE ApplicationID=" + ApplicationID.Trim() + "";
                    }

                    cla.ExecuteCommand(str, command);

                    transaction.Commit();
                    clsRtnMessage d = new clsRtnMessage();
                    d.MessageType = "Sucess";
                    d.RegistrationID = UserId.ToString();
                    d.Message = "Record Saved Sucessfully";
                    lst.Add(d);
                    // }
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

                    clsRtnMessage d = new clsRtnMessage();
                    d = new clsRtnMessage();
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

        [WebMethod]
        public void UpdateBackToBenPreSanctionDesk2(String SecurityKey, String ApplicationID, String LogDetails, String ApplicationStatusID, String BackToStageID, String AssignTo, String ApprovalStageID, String UpdateByRegID)//, String Remarkany
        {
            List<clsReturnMessage> lst = new List<clsReturnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            String UpdateOnDate = cla.SvrDate();
            if (MyCommanClassAPI.CheckApiAuthrization("81", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
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



            if (ApplicationStatusID.Trim().Length == 0)
            {

                clsReturnMessage d = new clsReturnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Select  ApplicationStatus";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
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
                    ////

                    // UPDATE
                    if (AssignTo.ToString().Length > 0)
                    {
                        str = " INSERT INTO Tbl_T_ApplicationDetails_Log (ApplicationLogID, ApplicationID, LogDetails, ApplicationStatusID, ApprovalStageID, UpdateOnDate, UpdateByRegID,IsBackToPrevious,AssignToUserID,AssignToUser)";
                        str += " VALUES(" + ApplicationLogID + "," + ApplicationID.ToString() + ",'" + LogDetails.Trim() + "',8," + BackToStageID.Trim() + ",'" + cla.mdy(cla.SvrDate().Trim()) + "'," + UpdateByRegID.Trim() + ",'1'," + AssignTo.Trim() + ",'" + AssignTo.Trim() + "')";
                        cla.ExecuteCommand(str, command);
                    }
                    else
                    {
                        str = " INSERT INTO Tbl_T_ApplicationDetails_Log (ApplicationLogID, ApplicationID, LogDetails, ApplicationStatusID, ApprovalStageID, UpdateOnDate, UpdateByRegID,IsBackToPrevious)";//" + ddlBackToStage.SelectedValue.Trim() + "
                        str += " VALUES(" + ApplicationLogID + "," + ApplicationID.ToString() + ",'" + LogDetails.Trim() + "',8," + BackToStageID.Trim() + ",'" + cla.mdy(cla.SvrDate().Trim()) + "'," + UpdateByRegID.Trim() + ",'1')";
                        cla.ExecuteCommand(str, command);
                    }



                    str = " UPDATE Tbl_T_ApplicationDetails SET ApprovalStageID=" + BackToStageID.Trim() + " , ApplicationStatusID=8 WHERE ApplicationID=" + ApplicationID.ToString() + "";
                    cla.ExecuteCommand(str, command);



                    ////
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
            Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
        }
        
        #region' Remove Upload Image(Geo-fencing)'
        [WebMethod]
        public void RemoveGeofencingImage(String SecurityKey, String GeofencingID)
        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<RemoveUploadImag> lst = new List<RemoveUploadImag>();

            if (MyCommanClassAPI.CheckApiAuthrization("82", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                RemoveUploadImag d = new RemoveUploadImag();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {

                List<String> apl = new List<String>();
                apl.Add(GeofencingID.Trim());

                bool str = cla.ExecuteByProcedure("SP_T_Remove_T_ApplicationGeofencing", apl);
                if (str == true)
                {
                    RemoveUploadImag w = new RemoveUploadImag();
                    clsRtnMessage d = new clsRtnMessage();
                    d.MessageType = "Sucess";
                    // d.WorkReportID = WorkReportID.ToString();//
                    d.Message = "Your details have been Deleted successfully.";
                    lst.Add(w);

                }


            }
            catch (Exception ex)
            {
                RemoveUploadImag w = new RemoveUploadImag();
                w.Message = ex.ToString();

                lst.Add(w);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }

        #endregion

        #region " Application for Approval (Approval Log)"

        [WebMethod]
        public void GetDataApprovalLog(String SecurityKey, String ApplicationID)

        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<ApprovalLog> lst = new List<ApprovalLog>();

            if (MyCommanClassAPI.CheckApiAuthrization("83", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                ApprovalLog d = new ApprovalLog();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {

                List<String> apl = new List<String>();
                apl.Add(ApplicationID);

                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_Application_Log", apl);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    ApprovalLog Sts = new ApprovalLog();

                    Sts.ApplicationID = dt.Rows[z]["ApplicationID"].ToString().Trim();
                    Sts.Date = dt.Rows[z]["Date"].ToString();
                    Sts.UpdatedBy = dt.Rows[z]["FullName"].ToString().Trim();//Date UpdatedBy Hierarchy   Stage Status  Reason Remark ApplicationID
                    Sts.Hierarchy = dt.Rows[z]["Desig_Name"].ToString().Trim();
                    Sts.Stage = dt.Rows[z]["ApprovalStages"].ToString();
                    Sts.Status = dt.Rows[z]["ApplicationStatus"].ToString().Trim();
                    Sts.Reason = dt.Rows[z]["Reason"].ToString();
                    Sts.Remark = dt.Rows[z]["Remark"].ToString();

                    //Sts.AssignTo = dt.Rows[z]["AssignTo"].ToString();
                    lst.Add(Sts);
                }
            }

            catch (Exception ex)
            {
                ApprovalLog Sts = new ApprovalLog();
                Sts.Message = ex.ToString();

                lst.Add(Sts);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }

        #endregion

        #region " Application for Approval (Approval Log)"

        [WebMethod]
        public void GetDataDesk4ApprovalLog(String SecurityKey, String WorkCompletionID)

        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<Desk4ApprovalLog> lst = new List<Desk4ApprovalLog>();

            if (MyCommanClassAPI.CheckApiAuthrization("84", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                Desk4ApprovalLog d = new Desk4ApprovalLog();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {

                List<String> apl = new List<String>();
                apl.Add(WorkCompletionID.Trim());

                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_WorkCompletion_Log", apl);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    Desk4ApprovalLog Sts = new Desk4ApprovalLog();

                    // Sts.ApplicationID = dt.Rows[z]["ApplicationID"].ToString().Trim();
                    Sts.Date = dt.Rows[z]["Date"].ToString();
                    Sts.UpdatedBy = dt.Rows[z]["FullName"].ToString().Trim();//Date UpdatedBy Hierarchy   Stage Status  Reason Remark ApplicationID
                    Sts.Hierarchy = dt.Rows[z]["Desig_Name"].ToString().Trim();
                    Sts.Stage = dt.Rows[z]["ApprovalStages"].ToString();
                    Sts.Status = dt.Rows[z]["ApplicationStatus"].ToString().Trim();
                    Sts.Reason = dt.Rows[z]["Reason"].ToString();
                    Sts.Remark = dt.Rows[z]["Remark"].ToString();

                    lst.Add(Sts);
                }
            }

            catch (Exception ex)
            {
                Desk4ApprovalLog Sts = new Desk4ApprovalLog();
                Sts.Message = ex.ToString();

                lst.Add(Sts);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }

        #endregion

        //#region''
        //[WebMethod]
        //public void GetVCRMCGPCodeByUser(String SecurityKey)
        //{
        //    DataTable dt = new DataTable();

        //    //   MyCommanClassAPI Comcls = new MyCommanClassAPI();
        //    MobAppComClass Mobapp = new MobAppComClass();
        //    List<DropDownBind> lst = new List<DropDownBind>();


        //    if (MyCommanClassAPI.CheckApiAuthrization("90", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
        //    {
        //        DropDownBind d = new DropDownBind();
        //        d.Message = "";
        //        d.Error = "Authorization Failed";
        //        lst.Add(d);

        //        Context.Response.Clear();
        //        Context.Response.ContentType = "application/json";
        //        //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
        //        Context.Response.Flush();

        //        Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
        //        return;
        //    }
        //    try
        //    {
        //        MyClass cla = new MyClass();
        //        //  dt = Comcls.GetApplicationMeeting(ApplicationID);
        //        //   dt = Mobapp.GetGPCodeByUser(UpdateByRegID);
        //        dt = Mobapp.GetPoCRAGPCode();
        //        for (int x = 0; x != dt.Rows.Count; x++)
        //        {
        //            DropDownBind d = new DropDownBind();
        //            d.KeyID = dt.Rows[x]["VillageID"].ToString();//ID
        //            d.Value = dt.Rows[x]["VillageName"].ToString();//Names
        //            d.Message = "";
        //            d.Error = "";
        //            lst.Add(d);

        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        DropDownBind d = new DropDownBind();
        //        d.KeyID = "";
        //        d.Value = "";
        //        d.Message = "";
        //        d.Error = ex.ToString();
        //        lst.Add(d);

        //    }

        //    Context.Response.Clear();
        //    Context.Response.ContentType = "application/json";
        //    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
        //    Context.Response.Flush();
        //    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        //}

        //#endregion

        #region' Pending Data for VCRMC Meeting Search with GPCode'
        [WebMethod]
        public void GetDataPendingVCRMCMeeting(String SecurityKey, String UserID, String GPCode, String ApprovalStatus, String FromRegistrationDate, String ToRegistrationDate, String ComponentID, String SubComponentID, String ActivityCategoryID, String ActivityID)

        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<PendingVCRMCMeetingData> lst = new List<PendingVCRMCMeetingData>();

            if (MyCommanClassAPI.CheckApiAuthrization("88", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                PendingVCRMCMeetingData d = new PendingVCRMCMeetingData();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {

                List<String> apl = new List<String>();
                apl.Add("");
                apl.Add(cla.mdy(FromRegistrationDate.Trim()));
                apl.Add(cla.mdy(ToRegistrationDate.Trim()));
                apl.Add(ApprovalStatus.Trim());
                apl.Add(ComponentID.Trim());
                apl.Add(SubComponentID.Trim());
                apl.Add(ActivityCategoryID.Trim());
                apl.Add(ActivityID.Trim());
                apl.Add("");
                apl.Add(UserID.Trim());
                apl.Add(GPCode.Trim());
                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_Get_ApplicationGrdDataVCRMCMetting", apl);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    PendingVCRMCMeetingData Sts = new PendingVCRMCMeetingData();

                    Sts.ApplicationID = dt.Rows[z]["ApplicationID"].ToString().Trim();
                    Sts.Date = dt.Rows[z]["ApplicationDate"].ToString();
                    Sts.RegisterName = dt.Rows[z]["RegisterName"].ToString().Trim();
                    Sts.Activity = dt.Rows[z]["ActivityName"].ToString();
                    Sts.ActivityCode = dt.Rows[z]["ActivityCode"].ToString().Trim();
                    Sts.GPCode = dt.Rows[z]["VillageCode"].ToString();
                    Sts.Status = dt.Rows[z]["ApplicationStatus"].ToString();
                    lst.Add(Sts);
                }
            }

            catch (Exception ex)
            {
                PendingVCRMCMeetingData Sts = new PendingVCRMCMeetingData();
                Sts.Message = ex.ToString();

                lst.Add(Sts);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }

        #endregion

        #region'Data VCRMC MeetingDate'
        [WebMethod]
        public void GetDataVCRMCMeetingDate(String SecurityKey, String UserID, String PageSize, String PageNo)
        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<DatawithMeetingDate> lst = new List<DatawithMeetingDate>();

            if (MyCommanClassAPI.CheckApiAuthrization("89", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                DatawithMeetingDate d = new DatawithMeetingDate();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {

                List<String> apl = new List<String>();
                apl.Add("");
                apl.Add("S");
                apl.Add(UserID.Trim());
                apl.Add(PageSize);
                apl.Add(PageNo);
                dt = new DataTable();
                //  dt = cla.GetDtByProcedure("SP_T_ApplicationVCRMC_Meeting", apl);//ApplicationMeetingID,MeetingnDate,VCRMCGPCode,FileMinuteOfmeeting,ListOfbeneficiary
                dt = cla.GetDtByProcedure("SP_T_ApplicationVCRMC_Meeting_WithPaging", apl);
                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    DatawithMeetingDate Sts = new DatawithMeetingDate();
                    Sts.ApplicationMeetingID = dt.Rows[z]["ApplicationMeetingID"].ToString().Trim();
                    Sts.MeetingnDate = dt.Rows[z]["MeetingnDate"].ToString();
                    Sts.VCRMCGPCode = dt.Rows[z]["VCRMCGPCode"].ToString().Trim();
                    Sts.FileMinuteOfmeeting = dt.Rows[z]["FileMinuteOfmeeting"].ToString();
                    Sts.ListOfbeneficiary = dt.Rows[z]["ListOfbeneficiaryApp"].ToString();
                    Sts.FilePhotographsVCRMC = dt.Rows[z]["FilePhotographsVCRMC"].ToString();
                    lst.Add(Sts);
                }
            }

            catch (Exception ex)
            {
                DatawithMeetingDate Sts = new DatawithMeetingDate();
                Sts.Message = ex.ToString();

                lst.Add(Sts);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }

        #endregion


        #region'Save ApplicationData With VCRMC Meeting Date'
        [WebMethod]
        public void SaveDataVCRMCMeetingDate(String SecurityKey, String ApplicationID, String GPCode, String VCRMCGPCode, String MeetingDate, String UserId)//String MinuteOfmeeting
        {
            List<clsRtnMessage> lst = new List<clsRtnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            String UpdateOnDate = cla.SvrDate();

            ////---------
            //clsRtnMessage dd = new clsRtnMessage();
            //dd.MessageType = "Error";
            //dd.Message = clsSettings.StrCommanMessgae;
            //lst.Add(dd);

            //Context.Response.Clear();
            //Context.Response.ContentType = "application/json";
            ////Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            //Context.Response.Flush();
            //Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
            //return;
            ////--------



            if (MyCommanClassAPI.CheckApiAuthrization("91", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsRtnMessage d = new clsRtnMessage();
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



            if (ApplicationID.Trim().Length == 0)
            {

                clsRtnMessage d = new clsRtnMessage();
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
            int ApplicationMeetingID = cla.TableID("Tbl_T_ApplicationVCRMC_Meeting", "ApplicationMeetingID");
            int ApplicationApplicantID = cla.TableID("Tbl_T_ApplicationVCRMC_MeetingChild", "ApplicationApplicantID");

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
                    String MinuteOfmeeting = "";
                    //   String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + "RegistrationID".ToString().Trim() + "");

                    str = " INSERT INTO Tbl_T_ApplicationVCRMC_Meeting (ApplicationMeetingID, MeetingnDate, VCRMCGPCode, UpdateByRegID, FileMinuteOfmeeting,VillageID)";//
                    str += " VALUES(" + ApplicationMeetingID + ",'" + cla.mdy(MeetingDate.Trim()) + "','" + VCRMCGPCode.ToString() + "'," + UserId.ToString() + ",'" + MinuteOfmeeting.ToString() + "'," + GPCode.Trim() + ")";
                    cla.ExecuteCommand(str, command);

                    if (ApplicationID.ToString().Length == 0)
                    {
                        str = "";
                    }
                    else
                    {

                        String[] Apli = ApplicationID.Trim().Split('/');
                        for (int x = 0; x != Apli.Length; x++)
                        {
                            ApplicationID = Apli[x];
                            {
                                str = " INSERT INTO Tbl_T_ApplicationVCRMC_MeetingChild (ApplicationApplicantID, ApplicationMeetingID, ApplicationID)";
                                str += " VALUES(" + ApplicationApplicantID + "," + ApplicationMeetingID + "," + ApplicationID.Trim() + ")";
                                cla.ExecuteCommand(str, command);
                                ApplicationApplicantID++;
                            }

                        }

                    }


                    transaction.Commit();

                    clsRtnMessage d = new clsRtnMessage();
                    d.MessageType = "Sucess";
                    d.ApplicationMeetingID = ApplicationMeetingID.ToString();
                    //  d.RegistrationID = UpdateByRegID.ToString();
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

                    clsRtnMessage d = new clsRtnMessage();
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

        #region'Save UPLOAD Menute of Meeting'
        [WebMethod]
        public void SaveUploadMenuteofMeeting(String SecurityKey, String ApplicationMeetingID, String FileMinuteOfmeeting, String FilePhotographsVCRMC)
        {

            List<clsRtnMessage> lst = new List<clsRtnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            //String RegistrationDate = cla.SvrDate();


            //clsRtnMessage dd = new clsRtnMessage();
            //dd.MessageType = "Error";
            //dd.Message = clsSettings.StrCommanMessgae;
            //lst.Add(dd);

            //Context.Response.Clear();
            //Context.Response.ContentType = "application/json";
            ////Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            //Context.Response.Flush();
            //Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
            //return;


            if (MyCommanClassAPI.CheckApiAuthrization("92", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {

                clsRtnMessage d = new clsRtnMessage();
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

            // int ApplicationMeetingID = Convert.ToInt32(ViewState["ApplicationMeetingID"].ToString());

            // int ApplicationMeetingID = cla.TableID("Tbl_T_ApplicationVCRMC_Meeting", "ApplicationMeetingID");

            //String path = Server.MapPath("~/admintrans/DocMasters/MasterDoc/");
            String PathUp = "DocMasters/MasterDoc";
            AzureBlobHelper fileRet = new AzureBlobHelper();


            String MinuteOfmeeting = "", PhotographsOfmeeting = "";

            String imageName = "";
            string filepath = "";
            if (FileMinuteOfmeeting.Length > 0)
            {
                imageName = "MinuteOfmeeting_" + ApplicationMeetingID.ToString() + "" + MyCommanClassAPI.GetFileExtension(FileMinuteOfmeeting);
                filepath = "/admintrans/DocMasters/MasterDoc/" + imageName;
                MinuteOfmeeting = filepath;
                //set the image path
                //string imgPath = path + imageName;
                //byte[] imageBytes = Convert.FromBase64String(FileMinuteOfmeeting);
                //File.WriteAllBytes(imgPath, imageBytes);   
                byte[] imageBytes = Convert.FromBase64String(FileMinuteOfmeeting);
                if (fileRet.UploadData(PathUp, imageName, imageBytes).Trim().Length > 0)
                {
                    clsRtnMessage d = new clsRtnMessage();
                    d.MessageType = "Error";
                    d.Message = "Please upload Minute Of meeting";
                    lst.Add(d);

                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }
                //-------------------
            }

            if (FilePhotographsVCRMC.Length > 0)
            {
                // string filepath1 = "";
                imageName = "PhotographsOfmeeting_" + ApplicationMeetingID.ToString() + "" + MyCommanClassAPI.GetFileExtension(FilePhotographsVCRMC);
                filepath = "/admintrans/DocMasters/MasterDoc/" + imageName;
                PhotographsOfmeeting = filepath;
                //set the image path
                byte[] imageBytes = Convert.FromBase64String(FilePhotographsVCRMC);
                //fileRet.UploadData(PathUp, imageName, imageBytes);
                if (fileRet.UploadData(PathUp, imageName, imageBytes).Trim().Length > 0)
                {
                    clsRtnMessage d = new clsRtnMessage();
                    d.MessageType = "Error";
                    d.Message = "Please upload Minute Of meeting";
                    lst.Add(d);

                    Context.Response.Clear();
                    Context.Response.ContentType = "application/json";
                    //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                    Context.Response.Flush();
                    Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                    return;
                }
                //string imgPath1 = path + imageName;
                //byte[] image1Bytes = Convert.FromBase64String(FilePhotographsVCRMC);
                //File.WriteAllBytes(imgPath1, image1Bytes);                 
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



                    if (ApplicationMeetingID.ToString().Trim().Length > 0)
                    {
                        //  String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + ApplicationID.Trim() + "");                                                 
                        if (FileMinuteOfmeeting.Length > 0)
                        {
                            String str = "";
                            str = " UPDATE Tbl_T_ApplicationVCRMC_Meeting set FileMinuteOfmeeting='" + MinuteOfmeeting.ToString() + "' where  ApplicationMeetingID=" + ApplicationMeetingID + " ";
                            cla.ExecuteCommand(str, command);
                            //-------------------
                        }

                        if (FilePhotographsVCRMC.Length > 0)
                        {
                            String str = "";
                            str = " UPDATE Tbl_T_ApplicationVCRMC_Meeting set FilePhotographsVCRMC='" + PhotographsOfmeeting.ToString() + "' where  ApplicationMeetingID=" + ApplicationMeetingID + " ";
                            cla.ExecuteCommand(str, command);
                        }
                        transaction.Commit();


                        clsRtnMessage d = new clsRtnMessage();
                        d.MessageType = "Sucess";
                        d.ApplicationMeetingID = ApplicationMeetingID.ToString();//                      
                        d.MinuteofMeetingCont = MinuteOfmeeting;
                        d.PhotoImage = PhotographsOfmeeting;
                        d.Message = "Record Saved Sucessfully";//
                        lst.Add(d);
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

                    clsRtnMessage d = new clsRtnMessage();
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

        #region'GetVCRMCGPCodeByUser wit Paging'

        [WebMethod]
        public void GetVCRMCGPCodeByUser_WithPaging(String SecurityKey, String Lang, String UserId, String LevelType_ID)
        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            MobAppComClass Mobapp = new MobAppComClass();
            List<DropDownBind> lst = new List<DropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("93", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                DropDownBind d = new DropDownBind();
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

                dt = Mobapp.GetPocraVillageAsPerWorkArea(Lang, UserId, LevelType_ID);
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    DropDownBind d = new DropDownBind();
                    d.KeyID = dt.Rows[x]["VillageID"].ToString();//ID
                    d.Value = dt.Rows[x]["VillageName"].ToString();//Names
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                DropDownBind d = new DropDownBind();
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

        #endregion

        #region' Data For Farmer Verification with Paging'
        [WebMethod]
        public void GetDataFarmerForVerification(String SecurityKey, String UserID, String DISTRICT, String TALUKA, String VILLAGE, String CATEGORY, String LANDSTATUS, String GENDER, String PHYSICALLYHANDICAP, String FromRegistrationDate, String ToRegistrationDate, String Status, String PageSize, String PageNumber)

        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<DataforVerificationFarmer> lst = new List<DataforVerificationFarmer>();

            if (MyCommanClassAPI.CheckApiAuthrization("94", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                DataforVerificationFarmer d = new DataforVerificationFarmer();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
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

                apl.Add("");
                apl.Add(GENDER.Trim());
                apl.Add(CATEGORY.Trim());
                apl.Add(LANDSTATUS.Trim());
                apl.Add(PHYSICALLYHANDICAP.Trim());
                apl.Add(DISTRICT.Trim());
                apl.Add(TALUKA.Trim());
                apl.Add(VILLAGE.Trim());//Address1VillageID
                apl.Add(cla.mdy(FromRegistrationDate.Trim()));
                apl.Add(cla.mdy(ToRegistrationDate.Trim()));
                apl.Add(Status.Trim());
                apl.Add(UserID.Trim());
                apl.Add(intPageSize.ToString());
                apl.Add(intPageNumber.ToString());
                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_FarmarRegistration_Paging2_Details", apl);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    DataforVerificationFarmer Sts = new DataforVerificationFarmer();

                    Sts.RegistrationID = dt.Rows[z]["RegistrationID"].ToString().Trim();
                    Sts.RegistrationNo = dt.Rows[z]["RegistrationNo"].ToString().Trim();
                    Sts.Date = dt.Rows[z]["RegistrationDate"].ToString();
                    Sts.Name = dt.Rows[z]["RegisterName"].ToString().Trim();
                    Sts.GENDER = dt.Rows[z]["Gender"].ToString();
                    Sts.CATEGORY = dt.Rows[z]["CategoryMaster"].ToString().Trim();
                    Sts.LANDSTATUS = dt.Rows[z]["LandStatus"].ToString();
                    Sts.Status = dt.Rows[z]["ApprovalStatus"].ToString();
                    Sts.Totalland = dt.Rows[z]["TotalLand"].ToString();
                    Sts.FarmerType = dt.Rows[z]["FarmerType"].ToString();
                    lst.Add(Sts);
                }
            }

            catch (Exception ex)
            {
                DataforVerificationFarmer Sts = new DataforVerificationFarmer();
                Sts.Message = ex.ToString();

                lst.Add(Sts);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }

        #endregion


        #region 'Get Registration status wise Reasion'
        [WebMethod]
        public void GetRegistrationStatusWiseReason(String SecurityKey, String ApplicationStatusID)
        {
            DataTable dt = new DataTable();

            MyCommanClass Comcls = new MyCommanClass();
            List<clsDdLBind> lst = new List<clsDdLBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("95", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsDdLBind d = new clsDdLBind();
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
            {      //Comcls.GetReasonVerifecation("1", ddlApplicationStatus.SelectedValue.Trim());
                dt = Comcls.GetReasonVerifecation("1", ApplicationStatusID); //ApprovalStageID="2",
                for (int x = 0; x != dt.Rows.Count; x++)
                {
                    clsDdLBind d = new clsDdLBind();
                    d.KeyID = dt.Rows[x]["ReasonID"].ToString();
                    d.Value = dt.Rows[x]["Reasons"].ToString();
                    d.Message = "";
                    d.Error = "";
                    lst.Add(d);

                }

            }
            catch (Exception ex)
            {

                clsDdLBind d = new clsDdLBind();
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
        #endregion

        #region 'Update Registration Verification'
        [WebMethod]
        public void UpdateRegistrationVerification(String SecurityKey, String RegistrationID, String RegistrationStatus, String Remark, String ReasonID, String UpdateOnDate, String UpdateByRegID)//, String Remarkany//UpdateByRegID
        {
            List<clsRtnMessage> lst = new List<clsRtnMessage>();
            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            MyClass cla = new MyClass();
            //  String UpdateOnDate = cla.SvrDate();
            if (MyCommanClassAPI.CheckApiAuthrization("96", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                clsRtnMessage d = new clsRtnMessage();
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



            if (RegistrationID.Trim().Length == 0)
            {

                clsRtnMessage d = new clsRtnMessage();
                d.MessageType = "Warning";
                d.Message = "Please Fill RegistrationID";
                lst.Add(d);
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();
                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }

            String str = "";
            int RegistrationLogID = cla.TableID("Tbl_M_Registration_Log", "RegistrationLogID");
            int RegistrationLogReasonID = cla.TableID("Tbl_M_Registration_LogReason", "RegistrationLogReasonID");

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

                    str = " INSERT INTO Tbl_M_Registration_Log (RegistrationLogID, RegistrationID, LogDetails, RegistrationStatus, UpdateOnDate, UpdateByRegID)";
                    str += " VALUES(" + RegistrationLogID + "," + RegistrationID.ToString() + ",'" + Remark.Trim() + "','" + RegistrationStatus.ToString().Trim() + "','" + cla.mdy(UpdateOnDate.Trim()) + "'," + UpdateByRegID.ToString() + ")";
                    cla.ExecuteCommand(str, command);

                    String[] Reas = ReasonID.Trim().Split('/');
                    for (int x = 0; x != Reas.Length; x++)
                    {

                        if (ReasonID.Trim().Length > 0)
                        {

                            str = " INSERT INTO Tbl_M_Registration_LogReason (RegistrationLogReasonID, RegistrationLogID, ReasonID)";
                            str += " VALUES(" + RegistrationLogReasonID + "," + RegistrationLogID + "," + Reas[x].ToString().Trim() + ")";
                            cla.ExecuteCommand(str, command);
                            RegistrationLogReasonID++;
                        }

                    }

                    //if (x1 == 0)
                    //{

                    //    str = " INSERT INTO Tbl_M_Registration_LogReason (RegistrationLogReasonID, RegistrationLogID, ReasonID)";
                    //    str += " VALUES(" + RegistrationLogReasonID + "," + RegistrationLogID + "," + ReasonID.Trim() + ")";
                    //    cla.ExecuteCommand(str, command);
                    //    RegistrationLogReasonID++;
                    //}


                    str = " UPDATE Tbl_M_RegistrationDetails SET ApprovalStatus='" + RegistrationStatus.ToString() + "'  WHERE RegistrationID=" + RegistrationID.ToString() + "";
                    cla.ExecuteCommand(str, command);

                    transaction.Commit();
                    clsRtnMessage d = new clsRtnMessage();
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

                    clsRtnMessage d = new clsRtnMessage();
                    d = new clsRtnMessage();
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

        #endregion

        #region' Data For Farmer Verification Log'
        [WebMethod]
        public void GetFarmarVerificationLog(String SecurityKey, String RegistrationID)

        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<FarmerVerificationLog> lst = new List<FarmerVerificationLog>();

            if (MyCommanClassAPI.CheckApiAuthrization("97", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                FarmerVerificationLog d = new FarmerVerificationLog();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {

                List<String> apl = new List<String>();

                //  apl.Add("");
                apl.Add(RegistrationID.Trim());

                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_FormarRegistration_Log", apl);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    FarmerVerificationLog Sts = new FarmerVerificationLog();


                    Sts.RegistrationID = dt.Rows[z]["RegistrationID"].ToString().Trim();
                    Sts.Date = dt.Rows[z]["Date"].ToString();
                    Sts.FullName = dt.Rows[z]["FullName"].ToString().Trim();
                    Sts.Hierarchy = dt.Rows[z]["Desig_Name"].ToString();
                    Sts.Status = dt.Rows[z]["Status"].ToString().Trim();
                    Sts.Reason = dt.Rows[z]["Reason"].ToString();
                    Sts.Remark = dt.Rows[z]["Remark"].ToString();
                    lst.Add(Sts);
                }
            }

            catch (Exception ex)
            {
                FarmerVerificationLog Sts = new FarmerVerificationLog();
                Sts.Message = ex.ToString();

                lst.Add(Sts);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }

        #endregion

        #region' Data For Other Verification'
        [WebMethod]
        public void GetDataOthersForVerification(String SecurityKey, String UserID, String DISTRICT, String TALUKA, String VILLAGE, String FromRegistrationDate, String ToRegistrationDate, String Status, String PageSize, String PageNo)

        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<OtherDtVerification> lst = new List<OtherDtVerification>();

            if (MyCommanClassAPI.CheckApiAuthrization("98", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                DataforVerificationFarmer d = new DataforVerificationFarmer();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {
                List<String> apl = new List<string>();
                apl.Add("");
                apl.Add(DISTRICT.Trim());
                apl.Add(TALUKA.Trim());
                apl.Add(VILLAGE.Trim());//Address1VillageID
                apl.Add(cla.mdy(FromRegistrationDate.Trim()));
                apl.Add(cla.mdy(ToRegistrationDate.Trim()));
                apl.Add(Status.Trim());
                apl.Add(UserID.Trim());
                apl.Add(PageSize);
                apl.Add(PageNo);
                dt = new DataTable();
                // dt = cla.GetDtByProcedure("SP_FOthersRegistration_Details", apl);
                dt = cla.GetDtByProcedure("SP_FOthersRegistration_Details_WithPaging", apl);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    OtherDtVerification Sts = new OtherDtVerification();
                    Sts.RegistrationID = dt.Rows[z]["RegistrationID"].ToString().Trim();
                    Sts.RegistrationNo = dt.Rows[z]["FPORegistrationNo"].ToString().Trim();
                    Sts.RegistrationDate = dt.Rows[z]["RegistrationDate"].ToString();
                    Sts.RegisterName = dt.Rows[z]["RegisterName"].ToString().Trim();
                    Sts.RegisterUnder = dt.Rows[z]["RegisterUnder"].ToString();
                    Sts.RegisteredThrough = dt.Rows[z]["RegisteredThrough"].ToString().Trim();
                    Sts.BeneficiaryTypes = dt.Rows[z]["BeneficiaryTypes"].ToString();
                    Sts.ApprovalStatus = dt.Rows[z]["ApprovalStatus"].ToString();

                    lst.Add(Sts);
                }
            }

            catch (Exception ex)
            {
                OtherDtVerification Sts = new OtherDtVerification();
                Sts.Message = ex.ToString();

                lst.Add(Sts);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }

        #endregion

        #region' Data For Community Verification'
        [WebMethod]
        public void GetDataCommunityForVerification(String SecurityKey, String UserID, String DISTRICT, String TALUKA, String VILLAGE, String GENDER, String FromRegistrationDate, String ToRegistrationDate, String Status, String PageSize, String PageNo)

        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<CommunityDtVerification> lst = new List<CommunityDtVerification>();

            if (MyCommanClassAPI.CheckApiAuthrization("99", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                CommunityDtVerification d = new CommunityDtVerification();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {

                List<String> apl = new List<String>();

                apl.Add("");
                apl.Add(GENDER.Trim());
                apl.Add(DISTRICT.Trim());
                apl.Add(TALUKA.Trim());
                apl.Add(VILLAGE.Trim());//Address1VillageID
                apl.Add(cla.mdy(FromRegistrationDate.Trim()));
                apl.Add(cla.mdy(ToRegistrationDate.Trim()));
                apl.Add(Status.Trim());
                apl.Add(UserID.Trim());
                apl.Add(PageSize);
                apl.Add(PageNo);
                dt = new DataTable();
                // dt = cla.GetDtByProcedure("SP_CommunityRegistration_Details", apl);
                dt = cla.GetDtByProcedure("SP_CommunityRegistration_Details_WithPaging", apl);

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    CommunityDtVerification Sts = new CommunityDtVerification();

                    Sts.RegistrationID = dt.Rows[z]["RegistrationID"].ToString().Trim();
                    Sts.RegistrationNo = dt.Rows[z]["RegistrationNo"].ToString().Trim();
                    Sts.RegistrationDate = dt.Rows[z]["RegistrationDate"].ToString();
                    Sts.RegisterName = dt.Rows[z]["RegisterName"].ToString().Trim();
                    Sts.Gender = dt.Rows[z]["Gender"].ToString();
                    Sts.GramPanchayatCode = dt.Rows[z]["GramPanchayatCode"].ToString().Trim();
                    Sts.ApprovalStatus = dt.Rows[z]["ApprovalStatus"].ToString();//ApprovalStatus
                    lst.Add(Sts);
                }
            }

            catch (Exception ex)
            {
                CommunityDtVerification Sts = new CommunityDtVerification();
                Sts.Message = ex.ToString();

                lst.Add(Sts);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }

        #endregion

        #region' Pending Data for VCRMC Meeting Search with GPCode'
        [WebMethod]
        public void GetDataPendingVCRMCMeeting_WithPaging(String SecurityKey, String UserID, String GPCode, String ApprovalStatus, String FromRegistrationDate, String ToRegistrationDate, String ComponentID, String SubComponentID, String ActivityCategoryID, String ActivityID, String PageSize, String PageNo)

        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<PendingVCRMCMeetingData> lst = new List<PendingVCRMCMeetingData>();

            if (MyCommanClassAPI.CheckApiAuthrization("100", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                PendingVCRMCMeetingData d = new PendingVCRMCMeetingData();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {

                List<String> apl = new List<String>();
                apl.Add("");
                apl.Add(cla.mdy(FromRegistrationDate.Trim()));
                apl.Add(cla.mdy(ToRegistrationDate.Trim()));
                apl.Add(ApprovalStatus.Trim());
                apl.Add(ComponentID.Trim());
                apl.Add(SubComponentID.Trim());
                apl.Add(ActivityCategoryID.Trim());
                apl.Add(ActivityID.Trim());
                apl.Add("");
                apl.Add(UserID.Trim());
                apl.Add(GPCode.Trim());
                apl.Add(PageSize);
                apl.Add(PageNo);
                dt = new DataTable();
                //dt = cla.GetDtByProcedure("SP_Get_ApplicationGrdDataVCRMCMetting", apl);//
                dt = cla.GetDtByProcedure("SP_Get_ApplicationGrdDataVCRMCMetting_WithPaging", apl);//

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    PendingVCRMCMeetingData Sts = new PendingVCRMCMeetingData();

                    Sts.ApplicationID = dt.Rows[z]["ApplicationID"].ToString().Trim();
                    Sts.Date = dt.Rows[z]["ApplicationDate"].ToString();
                    Sts.RegisterName = dt.Rows[z]["RegisterName"].ToString().Trim();
                    Sts.Activity = dt.Rows[z]["ActivityName"].ToString();
                    Sts.ActivityCode = dt.Rows[z]["ActivityCode"].ToString().Trim();
                    Sts.GPCode = dt.Rows[z]["VillageCode"].ToString();
                    Sts.Status = dt.Rows[z]["ApplicationStatus"].ToString();
                    Sts.RegistrationID = dt.Rows[z]["RegistrationID"].ToString();
                    lst.Add(Sts);
                }
            }

            catch (Exception ex)
            {
                PendingVCRMCMeetingData Sts = new PendingVCRMCMeetingData();
                Sts.Message = ex.ToString();

                lst.Add(Sts);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }

        #endregion

        #region' Data District wise Registration'
        [WebMethod]
        public void GetDistrictwiseRegistration(String SecurityKey, String RegistrationDate)

        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<DistricsNoReg> lst = new List<DistricsNoReg>();

            if (MyCommanClassAPI.CheckApiAuthrization("107", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                DistricsNoReg d = new DistricsNoReg();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {

                List<String> apl = new List<String>();

                if (RegistrationDate.Length == 0)
                {

                    dt = cla.GetDataTable("SELECT    COUNT(R.RegistrationID) AS NoofRegistration, dbo.Tbl_M_City.Cityname FROM  dbo.Tbl_M_RegistrationDetails AS R INNER JOIN   dbo.Tbl_M_City ON R.Work_City_ID = dbo.Tbl_M_City.City_ID WHERE(R.IsDeleted IS NULL) AND R.Work_ClustersMasterID is not null   GROUP BY Tbl_M_City.Cityname order by NoofRegistration desc");
                }
                else
                {
                    dt = cla.GetDataTable("SELECT    COUNT(R.RegistrationID) AS NoofRegistration, dbo.Tbl_M_City.Cityname FROM  dbo.Tbl_M_RegistrationDetails AS R INNER JOIN   dbo.Tbl_M_City ON R.Work_City_ID = dbo.Tbl_M_City.City_ID WHERE(R.IsDeleted IS NULL) AND R.Work_ClustersMasterID is not null and R.RegistrationDate='" + cla.mdy(RegistrationDate) + "' GROUP BY Tbl_M_City.Cityname  order by NoofRegistration desc");
                }
                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    DistricsNoReg Sts = new DistricsNoReg();


                    Sts.NoOfRegistration = dt.Rows[z]["NoofRegistration"].ToString().Trim();
                    Sts.District = dt.Rows[z]["Cityname"].ToString();
                    lst.Add(Sts);
                }
            }

            catch (Exception ex)
            {
                DistricsNoReg Sts = new DistricsNoReg();
                Sts.Message = ex.ToString();

                lst.Add(Sts);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }

        #endregion

        #region' Data District wise Application'
        [WebMethod]
        public void GetDistrictwiseApplication(String SecurityKey, String RegistrationDate)

        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<DistricsNoOfAppl> lst = new List<DistricsNoOfAppl>();

            if (MyCommanClassAPI.CheckApiAuthrization("108", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                DistricsNoOfAppl d = new DistricsNoOfAppl();
                //   d.Message = "Authorization Failed";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
                Context.Response.Flush();

                Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());
                return;
            }
            //------------validations ---------------------// 


            try
            {

                List<String> apl = new List<String>();

                if (RegistrationDate.Length == 0)
                {

                    dt = cla.GetDataTable("SELECT  COUNT(AD.ApplicationID) AS NoofRegistration, C.Cityname FROM Tbl_T_ApplicationDetails AS AD INNER JOIN Tbl_M_RegistrationDetails AS R ON AD.RegistrationID = R.RegistrationID INNER JOIN Tbl_M_City AS C ON R.Work_City_ID = C.City_ID WHERE (AD.IsDeleted IS NULL) GROUP BY C.Cityname order by NoofRegistration desc");
                }
                else
                {
                    dt = cla.GetDataTable("SELECT  COUNT(AD.ApplicationID) AS NoofRegistration, C.Cityname FROM Tbl_T_ApplicationDetails AS AD INNER JOIN Tbl_M_RegistrationDetails AS R ON AD.RegistrationID = R.RegistrationID INNER JOIN Tbl_M_City AS C ON R.Work_City_ID = C.City_ID WHERE (AD.IsDeleted IS NULL) and AD.ApplicationDate='" + cla.mdy(RegistrationDate) + "'  GROUP BY C.Cityname order by NoofRegistration desc");
                }

                for (int z = 0; z != dt.Rows.Count; z++)
                {
                    DistricsNoOfAppl Sts = new DistricsNoOfAppl();


                    Sts.NoOfApplication = dt.Rows[z]["NoofRegistration"].ToString().Trim();
                    Sts.District = dt.Rows[z]["Cityname"].ToString();

                    lst.Add(Sts);
                }
            }

            catch (Exception ex)
            {
                DistricsNoOfAppl Sts = new DistricsNoOfAppl();
                Sts.Message = ex.ToString();

                lst.Add(Sts);
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());

        }
        #endregion

        #region
        [WebMethod]
        public void GetRelatedToBeneficiaryCriteria(String SecurityKey)
        {
            DataTable dt = new DataTable();

            MyCommanClassAPI Comcls = new MyCommanClassAPI();
            List<clsDropDownBind> lst = new List<clsDropDownBind>();


            if (MyCommanClassAPI.CheckApiAuthrization("109", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
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

                clsDropDownBind d = new clsDropDownBind();
                d.KeyID = "Divorcee woman";
                d.Value = "Divorcee woman";

                d.Message = "";
                d.Error = "";
                lst.Add(d);
                clsDropDownBind dd2 = new clsDropDownBind();
                dd2.KeyID = "Widow";
                dd2.Value = "Widow";

                dd2.Message = "";
                dd2.Error = "";
                lst.Add(dd2);
                clsDropDownBind dd3 = new clsDropDownBind();
                dd3.KeyID = "others";
                dd3.Value = "others";

                dd3.Message = "";
                dd3.Error = "";
                lst.Add(dd3);

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
        #endregion

        #region "Village wise Farmer  Registration list"
        [WebMethod]


        public void GetVillagewiseFarmersList(String SecurityKey, String VillagesID)

        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<VillageRegistrationData> lst = new List<VillageRegistrationData>();

            if (MyCommanClassAPI.CheckApiAuthrization("110", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                VillageRegistrationData d = new VillageRegistrationData();
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
                VillageRegistrationData Vrd = new VillageRegistrationData();
                Vrd.status = "200";
                Vrd.response = "FarmerList";
                // Vrd.Message = "Success";
                List<RegistrData> data = new List<RegistrData>();
                //
                String[] Vlg = VillagesID.Trim().Split(',');
                for (int x = 0; x != Vlg.Length; x++)
                {
                    String VillageID = cla.GetExecuteScalar("SELECT VillageID FROM Tbl_M_VillageMaster where VillageCode='" + Vlg[x].Trim() + "' and IsDeleted is null ");
                    if (VillageID.Trim().Length > 0)
                    {
                        //
                        dt = new DataTable();
                        dt = cla.GetDataTable("SELECT rdf.RegistrationID, rdf.RegisterName, rdf.Gender, rdf.Address1VillageID, bt.BeneficiaryTypes, rdf.MobileNumber,vm.VillageCode, vm.VillageName FROM dbo.Tbl_M_RegistrationDetails AS rdf INNER JOIN dbo.Tbl_M_BeneficiaryTypes AS bt ON rdf.BeneficiaryTypesID = bt.BeneficiaryTypesID INNER JOIN dbo.Tbl_M_VillageMaster AS vm ON rdf.Address1VillageID = vm.VillageID WHERE(rdf.IsDeleted IS NULL) AND(rdf.BeneficiaryTypesID = 1) AND rdf.Address1VillageID in (" + VillageID + ")");//VillageID

                        for (int z = 0; z != dt.Rows.Count; z++)
                        {

                            RegistrData Reg = new RegistrData();
                            Reg.ID = dt.Rows[z]["RegistrationID"].ToString().Trim();
                            Reg.village_id = dt.Rows[z]["Address1VillageID"].ToString(); //dt.Rows[z]["VillageCode"].ToString();  //   
                            Reg.village_name = dt.Rows[z]["VillageName"].ToString();
                            Reg.Name = dt.Rows[z]["RegisterName"].ToString();
                            Reg.Gender = dt.Rows[z]["Gender"].ToString().Trim();
                            Reg.designation = dt.Rows[z]["BeneficiaryTypes"].ToString().Trim();
                            Reg.mobile = dt.Rows[z]["MobileNumber"].ToString().Trim();

                            Reg.is_selected = "0";


                            data.Add(Reg);
                            // lst.Add(Reg);
                        }

                        Vrd.data = data;


                        lst.Add(Vrd);

                    }
                    //  //
                }
            }
            // //
            catch (Exception ex)
            {
                VillageRegistrationData Reg = new VillageRegistrationData();
                Reg.Message = ex.ToString();
                Reg.status = "201";
                lst.Add(Reg);


            }


            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            //Context.Response.AddHeader("content-length", JsonConvert.SerializeObject(lst, Formatting.Indented).ToString().Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(JsonConvert.SerializeObject(lst, Formatting.Indented).ToString());




        }

        #endregion

        #region "Registration wise Farmer  Registration list"
        [WebMethod]


        public void GetRegistrationwiseFarmersList(String SecurityKey, String RegistrationID)

        {
            DataTable dt = new DataTable();

            MyClass cla = new MyClass();
            List<FarmrData> lst = new List<FarmrData>();

            if (MyCommanClassAPI.CheckApiAuthrization("111", EncryptDecryptQueryString.Decrypt(SecurityKey)) == false)
            {
                FarmrData d = new FarmrData();
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
                FarmrData Vrd = new FarmrData();
                Vrd.status = "200";
                Vrd.response = "FarmerList";
                // Vrd.Message = "Success";
                List<RegistFarmerData> data = new List<RegistFarmerData>();

                dt = new DataTable();

                dt = cla.GetDataTable("SELECT rdf.RegistrationID, rdf.RegisterName, rdf.Gender, rdf.Address1VillageID, bt.BeneficiaryTypes, rdf.MobileNumber, vm.VillageName FROM dbo.Tbl_M_RegistrationDetails AS rdf INNER JOIN dbo.Tbl_M_BeneficiaryTypes AS bt ON rdf.BeneficiaryTypesID = bt.BeneficiaryTypesID INNER JOIN dbo.Tbl_M_VillageMaster AS vm ON rdf.Address1VillageID = vm.VillageID WHERE(rdf.IsDeleted IS NULL) AND(rdf.BeneficiaryTypesID = 1) AND rdf.RegistrationID in (" + RegistrationID + ")");

                for (int z = 0; z != dt.Rows.Count; z++)
                {

                    RegistFarmerData Reg = new RegistFarmerData();
                    Reg.ID = dt.Rows[z]["RegistrationID"].ToString().Trim();
                    Reg.village_id = dt.Rows[z]["Address1VillageID"].ToString();
                    Reg.village_name = dt.Rows[z]["VillageName"].ToString();
                    Reg.Name = dt.Rows[z]["RegisterName"].ToString();
                    Reg.Gender = dt.Rows[z]["Gender"].ToString().Trim();
                    Reg.designation = dt.Rows[z]["BeneficiaryTypes"].ToString().Trim();
                    Reg.mobile = dt.Rows[z]["MobileNumber"].ToString().Trim();

                    Reg.is_selected = "0";


                    string[] fullName = Reg.Name.Split(new Char[] { ' ' });
                    string firstName = fullName[0];
                    string middleName = fullName[1];
                    string lastName = fullName[2];

                    Reg.first_name = firstName;
                    Reg.middle_name = middleName;
                    Reg.last_name = lastName;

                    data.Add(Reg);
                    // lst.Add(Reg);



                }

                Vrd.data = data;


                lst.Add(Vrd);

            }
            catch (Exception ex)
            {

                FarmrData Reg = new FarmrData();
                Reg.Message = ex.ToString();
                Reg.status = "201";
                lst.Add(Reg);

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











public class clsDdLBind
{

    public string KeyID { get; set; }
    public string Value { get; set; }
    public String Message { get; set; }
    public string Error { get; set; }

}


public class clsRtnMessage
{
    public String RegistrationID { get; set; }
    public String MessageType { get; set; }
    public string Message { get; set; }
    public String WorkReportID { get; set; }
    public String ApplicationMeetingID { get; set; }
    public string MinuteofMeetingCont { get; set; }
    public String PhotoImage { get; set; }
}
public class CloseAppl
{
    public String RegistrationID { get; set; }
    public String ApplicationID { get; set; }
    public String ActivityName { get; set; }
    public String ActivityCode { get; set; }
    public String ComponentName { get; set; }
    public String Date { get; set; }
    public List<AppReport> AppRep { get; set; }
    public List<PaymentReq> PayReq { get; set; }

    public string Message { get; set; }
}
public class AppReport
{
    public String UpdatedBy { get; set; }
    public String Level { get; set; }
    public String Stage { get; set; }

    public String Status { get; set; }
    public String Reason { get; set; }
    public String Remark { get; set; }

}
public class PaymentReq
{
    public String RequestNo { get; set; }
    public String RequestAmount { get; set; }
    public String TotalAmtByBen { get; set; }

    public String FinalAmtApproved { get; set; }
    public String ApprovalStagesFarmer { get; set; }
    public String ApprovalStage { get; set; }



}
public class PreDeskData
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
public class RemoveUploadImag
{
    public string GeofencingID { get; set; }
    public string Message { get; set; }
}


public class ApprovalLog
{

    public string ApplicationID { get; set; }
    public string Date { get; set; }
    public string UpdatedBy { get; set; }
    public string Hierarchy { get; set; }
    public string Stage { get; set; }
    public string Status { get; set; }
    public string Reason { get; set; }
    public string Remark { get; set; }
    public string Message { get; set; }
}
public class Desk4ApprovalLog
{

    //  public string ApplicationID { get; set; }
    public string Date { get; set; }
    public string UpdatedBy { get; set; }
    public string Hierarchy { get; set; }
    public string Stage { get; set; }
    public string Status { get; set; }
    public string Reason { get; set; }
    public string Remark { get; set; }
    public string Message { get; set; }
}
public class PreSancLetter
{
    public string PreSenLetterUrl { get; set; }
    public string Message { get; set; }
}
public class DropDownBind
{

    public string KeyID { get; set; }
    public string Value { get; set; }
    public String Message { get; set; }
    public string Error { get; set; }

}
public class PendingVCRMCMeetingData
{
    public string RegistrationID { get; set; }
    public string ApplicationID { get; set; }
    public string Date { get; set; }
    public string RegisterName { get; set; }
    public string Activity { get; set; }
    public string ActivityCode { get; set; }
    public string GPCode { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
}
public class DatawithMeetingDate
{
    public string ApplicationMeetingID { get; set; }
    public string MeetingnDate { get; set; }
    public string VCRMCGPCode { get; set; }
    public string FileMinuteOfmeeting { get; set; }
    public string ListOfbeneficiary { get; set; }
    public string FilePhotographsVCRMC { get; set; }
    public string Message { get; set; }
}
public class DataforVerificationFarmer
{
    public string RegistrationID { get; set; }
    public string RegistrationNo { get; set; }
    public string Date { get; set; }
    public string Name { get; set; }
    public string CATEGORY { get; set; }
    public string LANDSTATUS { get; set; }
    public string GENDER { get; set; }
    public string Totalland { get; set; }
    public string FarmerType { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
}
public class FarmerVerificationLog
{
    public string RegistrationID { get; set; }
    public string Date { get; set; }
    public string FullName { get; set; }
    public string Hierarchy { get; set; }
    public string Status { get; set; }
    public string Reason { get; set; }
    public string Remark { get; set; }
    public string Message { get; set; }



}
public class OtherDtVerification
{
    public string RegistrationID { get; set; }
    public string RegistrationNo { get; set; }
    public string RegistrationDate { get; set; }
    public string RegisterName { get; set; }
    public string RegisterUnder { get; set; }
    public string RegisteredThrough { get; set; }
    public string BeneficiaryTypes { get; set; }
    public string ApprovalStatus { get; set; }
    public string Message { get; set; }
}
public class CommunityDtVerification
{
    public string RegistrationID { get; set; }
    public string RegistrationNo { get; set; }
    public string GramPanchayatCode { get; set; }
    public string RegistrationDate { get; set; }
    public string RegisterName { get; set; }
    public string Gender { get; set; }
    public string ApprovalStatus { get; set; }
    public string Message { get; set; }
}
public class DistricsNoReg
{

    public string NoOfRegistration { get; set; }
    public string District { get; set; }
    public String Message { get; set; }
    public string Error { get; set; }

}
public class DistricsNoOfAppl
{

    public string NoOfApplication { get; set; }
    public string District { get; set; }
    public String Message { get; set; }
    public string Error { get; set; }

}
public class VillageRegistrationData
{
    public String status { get; set; }
    public string response { get; set; }
    public List<RegistrData> data { get; set; }
    public String Message { get; set; }
}
public class RegistrData
{
    public string ID { get; set; }
    public string village_id { get; set; }
    public string village_name { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }
    public string designation { get; set; }
    public string mobile { get; set; }
    public string is_selected { get; set; }

    //  public string Message { get; set; }

}
public class FarmrData
{
    public String status { get; set; }
    public string response { get; set; }
    public List<RegistFarmerData> data { get; set; }
    public String Message { get; set; }
}
public class RegistFarmerData
{
    public string ID { get; set; }
    public string village_id { get; set; }
    public string village_name { get; set; }
    public string Name { get; set; }
    public string first_name { get; set; }
    public string middle_name { get; set; }
    public string last_name { get; set; }
    public string Gender { get; set; }
    public string designation { get; set; }
    public string mobile { get; set; }
    public string is_selected { get; set; }

}





