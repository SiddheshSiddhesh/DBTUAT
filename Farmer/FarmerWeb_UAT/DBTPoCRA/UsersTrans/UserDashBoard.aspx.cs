using CommanClsLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBTPoCRA.UsersTrans
{
    public partial class UserDashBoard : System.Web.UI.Page
    {
        MyClass cla = new MyClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["UserId"] != null)
                {
                    try
                    {
                        Convert.ToInt32(Session["RegistrationID"].ToString());
                    }
                    catch
                    {
                        Response.Redirect("~/LogOut.aspx", true);
                    }
                }
                else
                {
                    Response.Redirect("~/LogOut.aspx", true);
                }
                if (!IsPostBack)
                {

                    String s = "View & Edit Profile";


                    if (Session["Lang"].ToString().Trim() == "mr-IN")
                    {
                        s = "प्रोफाइल पहा आणि संपादित करा";
                        Literal6.Text = "नाव";
                        Literal7.Text = "मोबाइल नंबर";
                        Literal8.Text = "नोंदणी दिनांक";
                        Literal9.Text = "नोंदणी स्थिती";
                        Literal10.Text = "नोंदणी तपशील";
                        Literal11.Text = "अर्ज उघडा";

                        grdSubject.Columns[0].HeaderText = "क्रमांक";
                        grdSubject.Columns[2].HeaderText = "अर्जाचा सांकेतांक";
                        grdSubject.Columns[3].HeaderText = "तारीख";
                        grdSubject.Columns[4].HeaderText = "क्रियाकलाप";
                        grdSubject.Columns[6].HeaderText = "क्रियाकलाप कोड";
                        grdSubject.Columns[7].HeaderText = "घटक";

                        grdSubject.Columns[8].HeaderText = "अवस्था";
                        //grdSubject.Columns[6].HeaderText = "";

                    }


                    if (cla.GetExecuteScalar("Select AdStatus from Tbl_M_RegistrationDetails R where R.RegistrationID=" + Session["RegistrationID"].ToString() + " ").Trim() != "Linked")
                    {
                        Util.ShowMessageBox(this.Page, "Message", "The Aadhaar number is not linked with Bank Account, kindly link for easy disbursement", "error");
                    }


                    FillData(s);
                    FillGrid();
                }
            }
            catch { }
        }
        private void FillData(String EditText)
        {
            DataTable dt = cla.GetDataTable("SELECT        ApprovalStatus, RegisterName,Convert(nvarchar(20), RegistrationDate,103) as RegistrationDate, MobileNumber FROM Tbl_M_RegistrationDetails WHERE (RegistrationID = " + Session["RegistrationID"].ToString() + ")");
            if (dt.Rows.Count > 0)
            {
                Literal1.Text = dt.Rows[0]["RegisterName"].ToString().ToUpper();
                Literal2.Text = dt.Rows[0]["MobileNumber"].ToString().ToUpper();
                Literal3.Text = dt.Rows[0]["RegistrationDate"].ToString().ToUpper();
                Literal4.Text = dt.Rows[0]["ApprovalStatus"].ToString().ToUpper();


                string RegID = Session["RegistrationID"].ToString();
                LiteralImage.Text = " <img alt='Profile Image' width='80' height='80' src='https://dbtpocradata.blob.core.windows.net/admintrans/DocMasters/MemberDoc/" + RegID + "/ProfileImage.jpg' />";


                if (Session["BeneficiaryTypesID"].ToString() == "1")
                {
                    // farmer
                    Literal5.Text = "<a  href='IndividualRegistrationEdit.aspx'><i class='icon icon-sailing-boat-water purple-text s-18'></i><span>" + EditText + "</span> </a>";
                }
                else if (Session["BeneficiaryTypesID"].ToString() == "2")
                {
                    // comini..
                    Literal5.Text = "<a href='CommunityRegistrationProfile.aspx'><i class='icon icon-sailing-boat-water purple-text s-18'></i><span>" + EditText + "</span> </a>";
                }
                else
                {
                    // others..
                    Literal5.Text = "<a href='FpoFpcProfileEdit.aspx'><i class='icon icon-sailing-boat-water purple-text s-18'></i><span>" + EditText + "</span> </a>";
                }

            }
            LiteralResion.Text = "";
            if (Session["Lang"].ToString().Trim() == "mr-IN")
            {
                dt = cla.GetDataTable(" SELECT R_1.ReasonsMr as Reasons FROM  Tbl_M_RegistrationDetails AS R INNER JOIN Tbl_M_Registration_Log AS L ON R.RegistrationID = L.RegistrationID INNER JOIN Tbl_M_Registration_LogReason AS S ON L.RegistrationLogID = S.RegistrationLogID INNER JOIN Tbl_M_Reason AS R_1 ON S.ReasonID = R_1.ReasonID WHERE(R.RegistrationID =" + Session["RegistrationID"].ToString() + ") AND(R.ApprovalStatus = 'Back To Beneficiary') and L.RegistrationLogID = (select top 1 RegistrationLogID from Tbl_M_Registration_Log where RegistrationID =" + Session["RegistrationID"].ToString() + " order by RegistrationLogID desc)");
            }
            else
            {
                dt = cla.GetDataTable(" SELECT R_1.Reasons as Reasons FROM  Tbl_M_RegistrationDetails AS R INNER JOIN Tbl_M_Registration_Log AS L ON R.RegistrationID = L.RegistrationID INNER JOIN Tbl_M_Registration_LogReason AS S ON L.RegistrationLogID = S.RegistrationLogID INNER JOIN Tbl_M_Reason AS R_1 ON S.ReasonID = R_1.ReasonID WHERE(R.RegistrationID =" + Session["RegistrationID"].ToString() + ") AND(R.ApprovalStatus = 'Back To Beneficiary') and L.RegistrationLogID = (select top 1 RegistrationLogID from Tbl_M_Registration_Log where RegistrationID =" + Session["RegistrationID"].ToString() + " order by RegistrationLogID desc)");
            }
            if (dt.Rows.Count > 0)
            {
                LiteralResion.Text = " <b> Your Registration back to you for below reasons. </b>  <br> ";
                TrResion.Visible = true;
                //for(int x=0;x!=dt.Rows.Count;x++)
                //{
                //    LiteralResion.Text = LiteralResion.Text+ (x+1).ToString()+ ". <a href='#'> " +dt.Rows[x]["Reasons"].ToString()+" </a>  <br> ";
                //}
                CheckBoxList1.DataSource = dt;
                CheckBoxList1.DataTextField = "Reasons";
                CheckBoxList1.DataValueField = "Reasons";
                CheckBoxList1.DataBind();
            }
        }
        private void FillGrid()
        {
            List<String> lst = new List<String>();
            lst.Add(Session["RegistrationID"].ToString());
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");//ddlGPCode
            lst.Add("");//txtBeneficiaryName
            grdSubject.DataSource = cla.GetDtByProcedure("SP_Get_ApplicationGrdData", lst); ;
            grdSubject.DataBind();


            lst.Clear();
            lst.Add(Session["RegistrationID"].ToString());
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            GridView1.DataSource = cla.GetDtByProcedure("SP_Get_ApplicationGrdDataClosed", lst);
            GridView1.DataBind();
        }

        protected void grdSubject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string id = grdSubject.DataKeys[e.Row.RowIndex]["ApplicationID"].ToString();
                GridView SC = (GridView)e.Row.FindControl("grdChild");
                List<String> lst = new List<string>();
                lst.Add(id.ToString());
                DataTable dt = cla.GetDtByProcedure("SP_Application_Log", lst);
                SC.DataSource = dt;
                SC.DataBind();

                GridView SChild = (GridView)e.Row.FindControl("grdChildPay");
                lst.Clear();
                lst.Add(id.ToString());
                dt = cla.GetDtByProcedure("SP_GetWorkCompletionRequests", lst);
                SChild.DataSource = dt;
                SChild.DataBind();

            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string id = GridView1.DataKeys[e.Row.RowIndex]["ApplicationID"].ToString();
                GridView SC = (GridView)e.Row.FindControl("grdChild");
                List<String> lst = new List<string>();
                lst.Add(id.ToString());
                DataTable dt = cla.GetDtByProcedure("SP_Application_Log", lst);
                SC.DataSource = dt;
                SC.DataBind();

                GridView SChild = (GridView)e.Row.FindControl("grdChildPay");
                lst.Clear();
                lst.Add(id.ToString());
                dt = cla.GetDtByProcedure("SP_GetWorkCompletionRequests", lst);
                SChild.DataSource = dt;
                SChild.DataBind();

            }
        }
        //protected void grdChild_RowDataBound(object sender, GridViewRowEventArgs e)
        ////{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        GridView grid = (GridView)sender;
        //        string id = grid.DataKeys[e.Row.RowIndex]["ApplicationID"].ToString();
        //        GridView SC = (GridView)e.Row.FindControl("grdChildPay");
        //        List<String> lst = new List<string>();
        //        lst.Add(id.ToString());
        //        DataTable dt = cla.GetDtByProcedure("SP_GetWorkCompletionRequests", lst);
        //        SC.DataSource = dt;
        //        SC.DataBind();



        //    }
        //}



        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Int32 x1 = 0;
            for (int x = 0; x != CheckBoxList1.Items.Count; x++)
            {
                if (CheckBoxList1.Items[x].Selected == false)
                {
                    x1++;
                }

            }

            if (x1 == 0)
            {
                String str = " UPDATE Tbl_M_RegistrationDetails SET ApprovalStatus='Pending'  WHERE RegistrationID=" + Session["RegistrationID"].ToString() + "";
                if (cla.ExecuteCommand(str).Length == 0)
                {
                    // clsMessages.Sucessmsg2(LiteralMsg, "Record Sucessfully Updated.");
                    Util.ShowMessageBox(this.Page, "Success", MyCommanClass.GetMsgInEnForDB("MSG17", Session["Lang"].ToString()), "success");
                }
            }
            else
            {
                // clsMessages.Errormsg(LiteralMsg, "Please select resolved resions from list .");
                Util.ShowMessageBox(this.Page, "Success", MyCommanClass.GetMsgInEnForDB("MSG45", Session["Lang"].ToString()), "success");
            }
        }
    }
}