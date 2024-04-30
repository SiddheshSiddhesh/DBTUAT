using CommanClsLibrary;
using DBTPoCRA.AdminTrans.UserControls;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace DBTPoCRA
{

    public partial class TestFarmerLogin : System.Web.UI.Page
    {
        MyClass cla = new MyClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 
                //ExpireAllCookies();

                //if (Request.QueryString.Count > 0)
                //{
                    Literal3.Text = "Individual LOGIN ";
                    // CreateRepot();
                //}
                //else
                //{
                //    Response.Redirect("Default.aspx", false);
                //}
            } 
        }


        //private void RunBiometric()
        //{
        //    StorageCredentials storageCredentials = new StorageCredentials("dbtpocradata", "7ZY9ok0uGgwnepFwe6iEsW+0bHZ3skcRa3ctc7R8PQQaCble8a3o+QrzzKDhdOcjvT2ZFLvaZhlKOdVlss1pAg==");
        //    CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, useHttps: true);
        //    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

        //    CloudBlobContainer container = blobClient.GetContainerReference("quickstartblobs" + Guid.NewGuid().ToString());

        //    // Retrieve reference to a blob named "myblob".
        //    CloudBlockBlob blockBlob = container.GetBlockBlobReference("myblob");

        //    // Create or overwrite the "myblob" blob with contents from a local file.
        //    using (var fileStream = System.IO.File.OpenRead(@"C:\DATA\a.txt"))
        //    {
        //        blockBlob.UploadFromStream(fileStream);
        //    }


        //    //await CloudBlobContainer.CreateAsync();

        //    ////CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

        //    //// Create a container called 'quickstartblobs' and append a GUID value to it to make the name unique. 
        //    //cloudBlobContainer = cloudBlobClient.GetContainerReference("quickstartblobs" + Guid.NewGuid().ToString());
        //    //await cloudBlobContainer.CreateAsync();

        //    //// Set the permissions so the blobs are public. 
        //    //BlobContainerPermissions permissions = new BlobContainerPermissions
        //    //{
        //    //    PublicAccess = BlobContainerPublicAccessType.Blob
        //    //};
        //    //await cloudBlobContainer.SetPermissionsAsync(permissions);
        //    ////CloudBlobDirectory directory = container.GetDirectoryReference(strDirectoryName)
        //}

        private void ExpireAllCookies()
        {
            if (HttpContext.Current != null)
            {
                int cookieCount = HttpContext.Current.Request.Cookies.Count;
                for (var i = 0; i < cookieCount; i++)
                {
                    var cookie = HttpContext.Current.Request.Cookies[i];
                    if (cookie != null)
                    {
                        var expiredCookie = new HttpCookie(cookie.Name)
                        {
                            Expires = DateTime.Now.AddDays(-1),
                            Domain = cookie.Domain
                        };
                        HttpContext.Current.Response.Cookies.Add(expiredCookie); // overwrite it
                    }
                }

                // clear cookies server side
                HttpContext.Current.Request.Cookies.Clear();
            }
        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {

            Literal2.Text = "";





            try
            {
                cla.OpenReturenDs("SELECT top 1 UserId, UserName, UPass, FullName, RegistrationID , LoginAs ,UserRoleID,Desig_ID FROM Tbl_M_LoginDetails WHERE (IsDeleted is null) AND (Username = '" + txtname.Value.Trim() + "') AND UPass='" + txtPassword.Value.Trim() + "' order by UserId desc ");
                if (cla.Ds.Tables[0].Rows.Count > 0)
                {


                    if (txtPassword.Value.Trim() == cla.Ds.Tables[0].Rows[0]["UPass"].ToString())
                    {
                        Session["LoginAs"] = cla.Ds.Tables[0].Rows[0]["LoginAs"].ToString();
                        Session["UserId"] = cla.Ds.Tables[0].Rows[0]["UserId"].ToString();
                        Session["RegistrationID"] = cla.Ds.Tables[0].Rows[0]["RegistrationID"].ToString();
                        Session["Comp_ID"] = "1";
                        Session["Lang"] = "en-IN";
                        Session["UserRoleID"] = cla.Ds.Tables[0].Rows[0]["UserRoleID"].ToString();
                        Session["UsersName"] = cla.Ds.Tables[0].Rows[0]["FullName"].ToString();
                        Session["BeneficiaryTypesID"] = cla.GetExecuteScalar("SELECT  BeneficiaryTypesID FROM Tbl_M_RegistrationDetails where RegistrationID=" + Session["RegistrationID"].ToString() + "");

                        if (cla.Ds.Tables[0].Rows[0]["Desig_ID"].ToString().Trim() != "")
                        {
                            Session["LevelType_ID"] = cla.GetExecuteScalar("select LevelType_ID from Tbl_M_Designation where Desig_ID=" + cla.Ds.Tables[0].Rows[0]["Desig_ID"].ToString() + "");
                        }
                        string strURL = "";

                        //if(Session["BeneficiaryTypesID"].ToString().Trim().Length>0)
                        //{

                        //    if(Request.QueryString["D"].Trim()=="1")

                        //}


                        string guid = Guid.NewGuid().ToString();
                        Session["AuthToken"] = guid;
                        // now create a new cookie with this guid value  
                        Response.Cookies.Add(new HttpCookie("AuthToken", guid));


                        strURL = "~/UsersTrans/UserDashBoard.aspx";




                        if (strURL.Length > 0)
                            Response.Redirect(strURL, false);
                        else
                        {
                            clsMessages.Errormsg(Literal2, "Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator.");
                        }

                    }
                    else
                    {
                        clsMessages.Errormsg(Literal2, "Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator.");

                    }

                }
                else
                {
                    clsMessages.Errormsg(Literal2, "Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator.");

                }
            }
            catch
            {
                //Label1.Text = ex.ToString();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MsgBox", "<script>alert('Either Your User Name & Password is Wrong or Your Account has been deactivated by Administrator !!!')</script>", false);
            }
        }


        private void LoadString()
        {


            Label3.Text = "पासवर्ड";
            if (Request.QueryString["D"].ToString().Trim() == "1")
            {
                Literal3.Text = " शेतकरी लॉग इन ";
                Label2.Text = "आधार क्रमांक";
                Label3.Text = "ओटीपी";

            }

            // btnOtp.Text = "ओटीपी पाठवा";
            btnLogin.Text = "लॉग इन";

            //Literal1.Text = "<p class='forget-pass text-white'>आपण आपला पासवर्ड विसरलात का? ? <a href='#'style='text-decoration: underline'>रीसेट करा </a></p>";
        }


        protected void btnMarathi_Click(object sender, EventArgs e)
        {

            LinkButton btn = (LinkButton)sender;
            if (btn.Text.Trim() == "मराठी")
            {
                Session["Lang"] = "mr-IN";
                btn.Text = "English";
                LoadString();
            }
            else
            {
                Session["Lang"] = "en-IN";
                btn.Text = "मराठी";
                string pageName = "UserLogin.aspx";
                String qstring = this.Page.ClientQueryString.ToString();
                if (qstring.Length > 0)
                    pageName = pageName + "?" + qstring;
                Response.Redirect("~/" + pageName);
            }

        }
         
        private void CreateRepot()
        {

            //String txtFileDestination = Server.MapPath("~/REPORTBACKUP");
            //string[] txtFiles;
            //txtFiles = Directory.GetFiles(txtFileDestination, "*.txt");
            //using (StreamWriter writer = new StreamWriter(txtFileDestination + @"\allfiles.txt"))
            //{
            //    for (int i = 0; i < txtFiles.Length; i++)
            //    {
            //        using (StreamReader reader = File.OpenText(txtFiles[i]))
            //        {
            //            writer.Write(reader.ReadToEnd());
            //        }
            //    }
            //}



            //DataTable dt = cla.GetDataTable("SELECT distinct ReportName , datediff(day,Max(TransDate),Getdate()) FROM Tbl_M_ReportGenrationStatus group by ReportName having (datediff(day,Max(TransDate),Getdate())) <=0");
            //DataTable dt = cla.GetDataTable("SELECT distinct ReportName  FROM Tbl_M_ReportGenrationStatus group by ReportName");
            //for (int x = 0; x != dt.Rows.Count; x++)
            //{
            //    String ReportName = dt.Rows[x]["ReportName"].ToString();
            //    if (ReportName.Trim() == "DashBoardSubDevesion")
            //    {
            DashBoardSubDevesion();
            //    }

            //}


            //dt = cla.GetDataTable("SELECT RegistrationID FROM Tbl_M_RegistrationDetails where isdeleted is null  and Work_VillageID <> (Select top 1 VillageID from Tbl_M_RegistrationLand where RegistrationID=Tbl_M_RegistrationDetails.RegistrationID and IsDeleted is null and VillageID is not null AND (ParentLandID IS NULL) and VillageID<>0  order by LandID)");
            //for (int x1 = 0; x1 != dt.Rows.Count; x1++)
            //{
            //    String RegistrationID = dt.Rows[x1]["RegistrationID"].ToString();
            //    DataTable dtLand = cla.GetDataTable("SELECT  TOP (1) L.VillageID, V.City_ID, V.ClustersMasterID, V.TalukaID, V.SubdivisionID FROM Tbl_M_RegistrationLand AS L INNER JOIN Tbl_M_VillageMaster AS V ON V.VillageID = L.VillageID WHERE (L.RegistrationID = " + RegistrationID.Trim() + ") AND (L.ParentLandID IS NULL) and L.isdeleted is null and L.VillageID <> 0 ORDER BY L.LandID");
            //    if (dtLand.Rows.Count > 0)
            //    {
            //        for (int x = 0; x != dtLand.Rows.Count; x++)
            //        {
            //            String str = " UPDATE  Tbl_M_RegistrationDetails SET Work_City_ID=" + dtLand.Rows[x]["City_ID"].ToString() + ", Work_TalukaID=" + dtLand.Rows[x]["TalukaID"].ToString() + ", Work_VillageID=" + dtLand.Rows[x]["VillageID"].ToString() + " ";
            //            if (dtLand.Rows[x]["ClustersMasterID"].ToString().Trim().Length > 0)
            //            {
            //                str += " , Work_ClustersMasterID=" + dtLand.Rows[x]["ClustersMasterID"].ToString() + "";
            //            }
            //            if (dtLand.Rows[x]["SubdivisionID"].ToString().Trim().Length > 0)
            //            {
            //                str += " , Work_SubdivisionID=" + dtLand.Rows[x]["SubdivisionID"].ToString().Trim() + "";
            //            }
            //            str += " WHERE (RegistrationID = " + RegistrationID.Trim() + ")";
            //            cla.ExecuteCommand(str);
            //        }
            //    }

            //}
        }

        private void DashBoardSubDevesion()
        {
            cla.ExecuteCommand(" update Tbl_T_Application_WorkReport Set TotalAmtByBen=1000  where Isdeleted is null  and  ApplicationStatusID=1 AND ApprovalStageID=6 and TotalAmtByBen is null ");

            cla.ExecuteCommand("UPDATE Tbl_M_RegistrationDetails SET   Work_SubdivisionID =(Select Tbl_M_VillageMaster.SubdivisionID from Tbl_M_VillageMaster where Tbl_M_VillageMaster.IsDeleted is null and Tbl_M_VillageMaster.VillageID=Work_VillageID) WHERE Work_SubdivisionID is null and IsDeleted is null");
            DataTable dtReport = new DataTable("DashBoardSubDevesion");
            DataTable dtC = cla.GetDataTable("Select * from GetSubDevWiseStageAppCount(1,99999)");
            dtReport = dtC.Copy();
            dtReport.TableName = "DashBoardSubDevesion";

            DataTable dt = cla.GetDataTable("SELECT City_ID FROM Tbl_M_City where IsDeleted is null  and UserInPocra is not null");
            for (int x = 0; x != dt.Rows.Count; x++)
            {
                String City_ID = dt.Rows[x]["City_ID"].ToString();
                DataTable dtD = cla.GetDataTable("Select * from GetSubDevWiseStageAppCount(1," + City_ID + ")");
                dtReport.Merge(dtD);
            }

            DataRow row1 = dtReport.NewRow();
            // row1["ItemCode"] = gvr.Cells[1].Text.Trim();
            for (int x = 0; x != dtReport.Columns.Count; x++)
            {
                String c = dtReport.Columns[x].ColumnName.Trim();
                c = c.Trim().Replace("[", "");
                c = c.Trim().Replace("]", "");

                try
                {
                    if (x != 0)
                    {
                        int sum = Convert.ToInt32(dtReport.Compute("SUM(" + c.Trim() + ")", ""));
                        row1[c.Trim()] = sum.ToString();
                    }
                    else
                    {
                        row1[c.Trim()] = "TOTAL";
                    }
                }
                catch
                {
                    // 
                }
            }
            dtReport.Rows.Add(row1);

            try
            {
                String Path = @"F:\Office\TESTDATA";// Server.MapPath("~/TESTDATA");
                Path = Path + "/" + "DashBoardSubDevesion.xml";
                Literal1.Text = Path;
                dtReport.WriteXml(@"" + Path);
            }
            catch (Exception ex)
            {
                Literal1.Text = ex.ToString();
            }



            //DataTable dtReport2 = new DataTable("ApplicationDetails");
            //dtReport2.TableName = "ApplicationDetails";
            //dtReport2 = cla.GetDataTable(" SELECT *  FROM GetOfflineApplicationReport ");
            //Path = Server.MapPath("~/TESTDATA");
            //Path = Path + "/" + "ApplicationDetails.xml";
            //dtReport2.WriteXml(@"" + Path);



        }



    }
}