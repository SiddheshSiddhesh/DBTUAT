using CommanClsLibrary;
using DBTPoCRA.APPData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBTPoCRA.UsersTrans
{
    public partial class WorkCompletionUp : System.Web.UI.Page
    {
        AzureBlobHelper fileRet = new AzureBlobHelper();
        MyClass cla = new MyClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ((Literal)Master.FindControl("lblHeadings")).Text = "Work Completion Updation";
                    if (Request.QueryString.Count > 0)
                    {

                        if (Request.QueryString.Count == 3)
                        {
                            lblWorkReportID.Text = Request.QueryString["W"].ToString();
                            // come for edit

                        }

                        ViewState["ApplicationID"] = Request.QueryString["ID"].ToString();


                        String s = cla.GetExecuteScalar("Select Tbl_T_ApplicationDetails.ApprovalStageID from  Tbl_T_ApplicationDetails where ApplicationID=" + Request.QueryString["ID"].ToString() + "");
                        if (s.Length == 0) s = "0";

                        if (Convert.ToInt32(s) < 5)
                        {
                            LiteralM.Text = "Please call to Pocra office";
                            btnAdd.Enabled = false;
                            btnSave.Enabled = false;
                            return;
                        }
                        s = cla.GetExecuteScalar("Select Tbl_T_ApplicationDetails.RegistrationID from  Tbl_T_ApplicationDetails where ApplicationID=" + Request.QueryString["ID"].ToString() + "");
                        if (s.Length == 0) s = "0";

                        if (s != Session["RegistrationID"].ToString().Trim())
                        {
                            LiteralM.Text = "Please call to Pocra office";
                            btnAdd.Enabled = false;
                            btnSave.Enabled = false;
                            return;
                        }




                        String Status = cla.GetExecuteScalar("SELECT  ApplicationStatusID FROM Tbl_T_ApplicationDetails where ApplicationID=" + ViewState["ApplicationID"].ToString().Trim() + "");

                        if (Status.Trim() == "2")
                        {
                            btnAdd.Enabled = false;
                            btnSave.Enabled = false;
                            grdSubject.Columns[6].Visible = false;
                            LiteralM.Text = " <span class='alert alert-danger'> Your Application is rejected , you can not make any change. </span>"; ;
                        }
                        else if (Status.Trim() == "3")
                        {
                            btnAdd.Enabled = false;
                            btnSave.Enabled = false;
                            grdSubject.Columns[6].Visible = false;
                            LiteralM.Text = " <span class='alert alert-danger'> Your Application is rejected , you can not make any change. </span>"; ;
                        }
                        FillDetails();



                    }
                    //   Session["Lang"] = Request.QueryString["LAN"].ToString();
                    //if (Session["Lang"].ToString().Trim() == "mr-IN")
                    //{
                    //    Thread.CurrentThread.CurrentCulture = new CultureInfo("mr-IN");
                    //    LoadString(Thread.CurrentThread.CurrentCulture);
                    //    LoadString(ci);
                    //}
                }
                //else
                //{
                //    if (Session["Lang"].ToString().Trim() == "mr-IN")
                //    {
                //        Thread.CurrentThread.CurrentCulture = new CultureInfo("mr-IN");
                //        LoadString(Thread.CurrentThread.CurrentCulture);
                //        LoadString(ci);
                //    }
                //}
            }
            catch { }
        }
        private void FillDetails()
        {
            btnSave.Visible = false;
            if (lblWorkReportID.Text.Trim().Length > 0)
            {

                // come for edit
                grdSubject.DataSource = cla.GetDataTable("SELECT   WorkCompletionID, Convert(nvarchar(20),CompletionDate,103) as CompletionDate,  DocumentDetails, DocumentUploaded , DocTypes,DocLevels  FROM Tbl_T_Application_WorkCompletions WHERE IsDeleted IS null AND   (ApplicationID = " + ViewState["ApplicationID"].ToString() + ") and WorkReportID=" + lblWorkReportID.Text.Trim() + " ORDER BY CompletionDate");
                grdSubject.DataBind();

                if (grdSubject.Rows.Count > 0)
                {
                    btnSave.Visible = true;
                }
            }

            List<String> lst = new List<string>();
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add(Request.QueryString["AID"].ToString());
            lst.Add("");
            lst.Add("");
            DataTable dt = cla.GetDtByProcedure("SP_GetActivetySearch", lst);
            FormView1.DataSource = dt;
            FormView1.DataBind();

            if (cla.GetExecuteScalar("SELECT    ApprovalStageID FROM  Tbl_T_ApplicationDetails WHERE (ApplicationID =" + ViewState["ApplicationID"].ToString() + ")") == "7")
            {
                btnAdd.Visible = false;
                btnSave.Visible = false;
            }
            if (cla.GetExecuteScalar("SELECT    ApprovalStageID FROM  Tbl_T_ApplicationDetails WHERE (ApplicationID =" + ViewState["ApplicationID"].ToString() + ")") == "8")
            {
                btnAdd.Visible = false;
                btnSave.Visible = false;
            }

            divReq.Visible = false;
            if (lblWorkReportID.Text.Trim().Length > 0)
            {
                divReq.Visible = true;
                lblReqNo.Text = cla.GetExecuteScalar("SELECT RequestNo FROM  Tbl_T_Application_WorkReport where WorkReportID=" + lblWorkReportID.Text.Trim() + " AND BenWorkReportID is null ");
                txtExpenditureAmount.Text = cla.GetExecuteScalar("SELECT TotalAmtByBen FROM  Tbl_T_Application_WorkReport where WorkReportID=" + lblWorkReportID.Text.Trim() + " AND BenWorkReportID is null ");
                lblReqDate.Text = cla.GetExecuteScalar("SELECT Convert(nvarchar(20),UpdateOnDate) as UpdateOnDate FROM  Tbl_T_Application_WorkReport where WorkReportID=" + lblWorkReportID.Text.Trim() + " and  BenWorkReportID is null ");
            }

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {

            if (lblWorkReportID.Text.Trim().Length == 0)
            {
                if (cla.GetExecuteScalar("Select top 1 WorkReportID from Tbl_T_Application_WorkReport where ApplicationID=" + ViewState["ApplicationID"].ToString().Trim() + "  and ApplicationStatusID not in (15,2,25)  and IsDeleted is null  ").Length > 0)
                {
                    // in case it is in process
                    Util.ShowMessageBox(this.Page, "Info", "Record Already exist ", "error", "UserDashBoard.aspx");
                    return;
                }
            }



            if (FileUpload1.HasFile == false)
            {
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG25", Session["Lang"].ToString()), "error");
                return;
            }


            string FileError = MyCommanClass.CheckAllowedFileExtension(System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName.ToString()));
            if (FileError.Length > 0)
            {
                // Util.ShowMessageBox(this.Page, "Error", FileError, "error");
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG25", Session["Lang"].ToString()), "error");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                return;
            }
            FileError = Util.CheckAllowedFileName(FileUpload1);
            if (FileError.Length > 0)
            {
                Util.ShowMessageBox(this.Page, "Error", FileError, "error");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> CallToAll();  </script>", false);
                return;
            }


            int WorkCompletionID = cla.TableID("Tbl_T_Application_WorkCompletions", "WorkCompletionID");
            int WorkReportID = cla.TableID("Tbl_T_Application_WorkReport", "WorkReportID");
            LiteralMsg.Text = "";
            String RequestNo = cla.GetSqlUnikNO("3") + "/" + ViewState["ApplicationID"].ToString().Trim();
            List<String> lst = new List<string>();



            String DocumentUploaded = "";
            //String path = Server.MapPath("/admintrans/DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "");
            String PathUp = "DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "";


            if (FileUpload1.HasFile)
            {

                // String Uppath = path + "/PaymentRequestDoc" + WorkCompletionID.ToString() + "" + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName.ToString());
                DocumentUploaded = "/admintrans/DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "/PaymentRequestDoc" + WorkCompletionID.ToString() + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName.ToString());

                byte[] bin = FileUpload1.FileBytes;
                String fileName = "PaymentRequestDoc" + WorkCompletionID.ToString() + "" + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName.ToString());
                //fileRet.UploadData(PathUp, fileName, bin);
                String ret = fileRet.UploadData(PathUp, fileName, bin);
                if (ret.Trim().Length > 0)
                {
                    Util.ShowMessageBox(this.Page, "Error", "Please upload Payment Request Doc.", "error");
                    return;
                }
            }

            if (DocumentUploaded.Trim().Length == 0)
            {
                Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG25", Session["Lang"].ToString()), "error");
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



                    if (lblWorkReportID.Text.Trim().Length == 0)
                    {
                        // add new 
                        String str1 = " INSERT INTO Tbl_T_Application_WorkReport (WorkReportID, RequestNo, ApplicationID, UpdateOnDate, UpdateByRegID,ApprovalStageID,ApplicationStatusID)";
                        str1 += " VALUES(" + WorkReportID + ",'" + RequestNo.ToString() + "'," + ViewState["ApplicationID"].ToString() + ",'" + cla.mdy(cla.SvrDate()) + "'," + Session["UserId"].ToString() + ",6,1)";
                        cla.ExecuteCommand(str1, command);

                    }
                    else
                    {
                        WorkReportID = Convert.ToInt32(lblWorkReportID.Text);
                    }



                    String str = " INSERT INTO Tbl_T_Application_WorkCompletions (WorkCompletionID, ApplicationID,  DocumentDetails, DocumentUploaded,CompletionDate,DocTypes,DocLevels,WorkReportID)";
                    str += " VALUES(" + WorkCompletionID + "," + ViewState["ApplicationID"].ToString() + ",'" + txtDocument.Text.Trim().Replace("'", "") + "','" + DocumentUploaded.Trim() + "','" + cla.mdy(txtCompletionDate.Text) + "','" + ddlDocType.SelectedValue.Trim() + "','" + ddlLevels.SelectedValue.Trim() + "'," + WorkReportID + ") ";
                    cla.ExecuteCommand(str, command);
                    transaction.Commit();




                    lblWorkReportID.Text = WorkReportID.ToString();
                    FillDetails();
                    // clsMessages.Sucessmsg(LiteralMsg, "S");////
                    Util.ShowMessageBox(this.Page, "Info", MyCommanClass.GetMsgInEnForDB("MSG1", Session["Lang"].ToString()), "success");
                    //MyCommanClass.GetMsgInEnForDB("MSG1", Session["Lang"].ToString());
                }
                catch (Exception ex)
                {
                    clsMessages.Errormsg(LiteralMsg, MyCommanClass.GetMsgInEnForDB("MSG22", Session["Lang"].ToString()) + ex.Message.Trim());////
                    try
                    {
                        transaction.Rollback();

                    }
                    catch
                    {
                        //  
                    }

                }
                finally
                {

                    if (connection != null) { connection.Close(); }
                    command.Dispose();
                }

            }



        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            LiteralMsg.Text = "";
            String ApprovalStageID = cla.GetExecuteScalar("SELECT ApprovalStageID FROM  Tbl_T_ApplicationDetails where ApplicationID=" + ViewState["ApplicationID"].ToString().Trim() + "");

            //if(cla.GetExecuteScalar("Select top 1 WorkReportID from Tbl_T_Application_WorkReport where ApplicationID=" + ViewState["ApplicationID"].ToString().Trim() + " and TotalAmtByBen is not null and ApplicationStatusID not in (15,2,20) ").Length>0)
            //{
            //    Util.ShowMessageBox(this.Page, "Info", "Record Already exist ", "error", "UserDashBoard.aspx");
            //    return;
            //}

            if (txtExpenditureAmount.Text.Trim() != "")
            {

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

                        str = " UPDATE Tbl_T_Application_WorkReport SET TotalAmtByBen=" + txtExpenditureAmount.Text.Trim() + " , ApplicationStatusID =1, ApprovalStageID =6 ,TotalAmtByVCRMC=0 ,FinalAmtApproved=0   WHERE   (ApplicationID =" + ViewState["ApplicationID"].ToString().Trim() + ") AND WorkReportID=" + lblWorkReportID.Text.Trim() + "  ";
                        cla.ExecuteCommand(str, command);





                        transaction.Commit();
                        //clsMessages.Sucessmsg(LiteralMsg, "S");////
                        Util.ShowMessageBox(this.Page, "Info", MyCommanClass.GetMsgInEnForDB("MSG1", Session["Lang"].ToString()), "success", "UserDashBoard.aspx");
                        FillDetails();
                        txtCompletionDate.Text = "";
                        txtDocument.Text = "";

                    }
                    catch (Exception ex)
                    {
                        clsMessages.Errormsg(LiteralMsg, MyCommanClass.GetMsgInEnForDB("MSG22", Session["Lang"].ToString()) + ex.Message.Trim());////
                        try
                        {
                            transaction.Rollback();

                        }
                        catch
                        {
                            //  
                        }

                    }
                    finally
                    {
                        if (connection != null) { connection.Close(); }
                        command.Dispose();
                    }

                }
            }
            else
            {                 
                
            }
        }

        protected void grdSubject_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String WorkCompletionID = grdSubject.DataKeys[e.RowIndex]["WorkCompletionID"].ToString();

            LiteralMsg.Text = "";
            try
            {

                String strQuery = "Update Tbl_T_Application_WorkCompletions set IsDeleted='1'  WHERE  (WorkCompletionID =" + WorkCompletionID + ")";
                if (cla.ExecuteCommand(strQuery).Trim().Length == 0)
                {
                    //clsMessages.Sucessmsg(LiteralMsg, "D");
                    // MyCommanClass.GetMsgInEnForDB("", Session["Lang"].ToString());
                    Util.ShowMessageBox(this.Page, "Info", MyCommanClass.GetMsgInEnForDB("MSG23", Session["Lang"].ToString()), "success");
                    FillDetails();
                }
                else
                    //  clsMessages.Warningmsg(LiteralMsg, "Record is in Used , Can not be deleted !! ");
                    //MyCommanClass.GetMsgInEnForDB("MSG24", Session["Lang"].ToString());
                    Util.ShowMessageBox(this.Page, "Error", MyCommanClass.GetMsgInEnForDB("MSG24", Session["Lang"].ToString()), "error");
            }
            catch (Exception ex)
            {
                clsMessages.Errormsg(LiteralMsg, ex.Message);
            }
        }
    }
}