using CommanClsLibrary;
using DBTPoCRA.APPData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBTPoCRA.UsersTrans
{
    public partial class UpdateLandCerti : System.Web.UI.Page
    {
        MyClass cla = new MyClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Count > 0)
                {

                    if (Request.QueryString.Count == 2)
                    {
                        String LandID = Request.QueryString["I"].ToString();

                        String RegID = cla.GetExecuteScalar("Select RegistrationID from Tbl_M_RegistrationLand where LandID=" + LandID + " and IsDeleted is null ");
                        if (RegID.Trim() != Session["RegistrationID"].ToString())
                        {
                            btnUpload.Visible = false;
                        }

                    }
                    else if (Request.QueryString.Count > 2)
                    {

                        String LandID = EncryptDecryptQueryString.Decrypt(Request.QueryString["I"].ToString());

                        String RegID = cla.GetExecuteScalar("Select RegistrationID from Tbl_M_RegistrationLand where LandID=" + LandID + " and IsDeleted is null ");
                        if (RegID.Trim() != EncryptDecryptQueryString.Decrypt(Request.QueryString["R"].ToString()))
                        {
                            btnUpload.Visible = false;
                        }
                        else
                        {
                            Session["RegistrationID"] = EncryptDecryptQueryString.Decrypt(Request.QueryString["R"].ToString());
                        }
                    }
                }
                else
                {
                    btnUpload.Visible = false;
                }

            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            String LandID = "";
            if (Request.QueryString.Count == 2)
            {
                LandID = Request.QueryString["I"].ToString();
            }
            else
            {
                LandID = EncryptDecryptQueryString.Decrypt(Request.QueryString["I"].ToString());
            }
            String PathUp = "DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "";
            String ParentLandID = cla.GetExecuteScalar("Select ParentLandID from Tbl_M_RegistrationLand where LandID=" + LandID + " and IsDeleted is null ");

            AzureBlobHelper fileRet = new AzureBlobHelper();

            if (File712.HasFile)
            {
                string filepath = "";

                if (File712.PostedFile.ContentLength < 3145728)
                {
                    String fileName = "";
                    String str = "";
                    if (ParentLandID.Length > 0)
                    {
                        fileName = "SURVEYNUMBER712_" + LandID.ToString() + "" + System.IO.Path.GetExtension(File712.PostedFile.FileName.ToString());
                        filepath = "/admintrans/DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "/" + fileName;
                        str = " UPDATE Tbl_M_RegistrationLand SET Extracts712Doc='" + filepath + "' , UpdatedOn='" + cla.mdy(cla.SvrDate()) + "'  WHERE  LandID=" + LandID + " ";

                    }
                    else
                    {
                        fileName = "FORM8A_" + LandID.ToString() + "" + System.IO.Path.GetExtension(File712.PostedFile.FileName.ToString());
                        filepath = "/admintrans/DocMasters/MemberDoc/" + Session["RegistrationID"].ToString().Trim() + "/" + fileName;
                        str = " UPDATE Tbl_M_RegistrationLand SET Form8ADoc='" + filepath + "' , UpdatedOn='" + cla.mdy(cla.SvrDate()) + "'  WHERE  LandID=" + LandID + " ";
                    }


                    byte[] bin = File712.FileBytes;
                    String ret = fileRet.UploadData(PathUp, fileName, bin);
                    if (ret.Trim().Length > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> alert('Please upload again');  </script>", false);
                        return;
                    }
                    else
                    {
                        String s = cla.ExecuteCommand(str);
                        if (s.Length == 0)
                        {
                            //Util.ShowMessageBox(this.Page, "Success", "Document Uploaded Successfully", "success");
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> alert('Document Uploaded Successfully');  </script>", false);
                        }
                        else
                        {
                            //Util.ShowMessageBox(this.Page, "Error", "Please upload again", "error");
                            ScriptManager.RegisterStartupScript(Page, this.GetType(), "MsgBox", "<script> alert('Please upload again');  </script>", false);

                        }
                    }
                }
            }
        }
    }
}