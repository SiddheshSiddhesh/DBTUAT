using CommanClsLibrary;
using DBTPoCRA.APPData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBTPoCRA.AdminTrans.UserControls
{
    /// <summary>
    /// Summary description for FileUploadHandler
    /// </summary>
    public class FileUploadHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        AzureBlobHelper fileRet = new AzureBlobHelper();
        MyClass cla = new MyClass();

        public void ProcessRequest(HttpContext context)
        {
            String Message = "";
            String RegistrationID = context.Request.Params["RID"].ToString();
            String CommunityPaymentVoucherID = context.Request.Params["PID"].ToString();
            String Name = context.Request.Params["Name"].ToString();
            if (! HttpContext.Current.Session["AuthToken"].ToString().Equals(context.Request.Params["AuthToken"].ToString()))
            {
                // redirect to the login page in real application
                return;

            }

            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile fu = files[i];

                    string FileError = MyCommanClass.CheckAllowedFileExtension(System.IO.Path.GetExtension(fu.FileName.ToString()));
                    if (FileError.Length > 0)
                    {
                        Message = "" + FileError;                                            
                    }
                    FileError = Util.CheckAllowedFileName(fu);
                    if (FileError.Length > 0)
                    {
                        Message = "" + FileError;
                    }
                    String PathUp = "DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "";
                    String SanctionedVDP = "/admintrans/DocMasters/MemberDoc/" + RegistrationID.ToString().Trim() + "/"+Name + System.IO.Path.GetExtension(fu.FileName.ToString());
                    String fileName = Name+ System.IO.Path.GetExtension(fu.FileName.ToString());
                    byte[] bin = new byte[fu.ContentLength];
                    String ret = fileRet.UploadData(PathUp, fileName, bin);
                    if(ret.Length==0)
                    {
                        //insert into data
                        if (Name.Trim() == "Work_Completion_Certificate")
                        {
                            ret= cla.ExecuteCommand("UPDATE Tbl_T_CommunityPaymentVoucher SET   Work_Completion_Certificate_Path = '"+ SanctionedVDP + "' WHERE CommunityPaymentVoucherID=" + CommunityPaymentVoucherID + " ");
                        }
                        else
                        {
                            ret= cla.ExecuteCommand("UPDATE Tbl_T_CommunityPaymentVoucher SET  NOC_By_SDAO_Path = '"+ SanctionedVDP + "'  WHERE CommunityPaymentVoucherID=" + CommunityPaymentVoucherID + " ");
                        }
                    }
                    if(ret.Length==0)
                    {
                        Message = "File Uploaded Successfully. ~ "+clsSettings.BaseUrl+""+SanctionedVDP;
                    }
                    else
                    {
                        Message = "Error in Data Uploading.Please try again.";
                    }
                }
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(Message);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}