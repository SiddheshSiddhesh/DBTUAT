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
    public partial class ApplicationForm : System.Web.UI.Page
    {
        #region"Declarection"
        MyClass cla = new MyClass();
        MyCommanClass Comcls = new MyCommanClass();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {



                if (!IsPostBack)
                {
                    if (Session["BeneficiaryTypesID"] != null)
                    {
                        hfCustomerId.Value = Session["BeneficiaryTypesID"].ToString();
                        FillDll();
                        FillActivety();
                        ((Literal)Master.FindControl("lblHeadings")).Text = "Application Form";
                        HiddenFieldLan.Value = Session["Lang"].ToString().Trim();
                        if (Session["Lang"].ToString().Trim() != "en-IN")
                        {
                            Literal1.Text = "सूचीमधून शोध क्रियाकलाप";
                            Literal2.Text = "घटक";
                            Literal3.Text = "उपघटक";
                            Literal4.Text = "प्रकल्पांतर्गत बाब";
                            ((Literal)Master.FindControl("lblHeadings")).Text = "अर्ज";
                            btnSearch.Text = "शोध";
                            btnShowAll.Text = "सगळं दाखवा";
                        }
                    }
                    else
                    {
                        Response.Redirect("../");
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void FillDll()
        {
            DataTable dt = Comcls.GetComponent(Session["BeneficiaryTypesID"].ToString());
            ddlComponent.DataSource = dt;
            ddlComponent.DataTextField = "ComponentName";
            ddlComponent.DataValueField = "ComponentID";
            ddlComponent.DataBind();
            ddlComponent.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlComponent.SelectedIndex = 0;



            List<String> lst = new List<string>();
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add("");
            lst.Add(Session["BeneficiaryTypesID"].ToString());
            lst.Add(Session["RegistrationID"].ToString());


            if (Session["Lang"].ToString().Trim() != "en-IN")
            {
                dt = cla.GetDtByProcedure("SP_GetActivetySearchNewMr", lst);
                if (Request.QueryString.Count > 0)
                {
                    string QrStr = Request.QueryString["T"].ToString().Replace(" ", "+");

                    //ActivityGroupID
                    DataView dv = new DataView(dt);
                    dv.RowFilter = "ActivityGroupID=" + CommanClsLibrary.EncryptDecryptQueryString.BasicDecryptString(QrStr) + " ";
                    DataTable dc = dv.ToTable();
                    dt = dc;
                }
                else
                {
                    dt = null;
                }



                ddlActivity.Items.Clear();
                ddlActivity.DataSource = dt;
                ddlActivity.DataTextField = "ActivityName";
                ddlActivity.DataValueField = "ActivityID";
                ddlActivity.DataBind();
                ddlActivity.Items.Insert(0, new ListItem("--Select--", "0"));
                ddlActivity.SelectedIndex = 0;

            }
            else
            {
                dt = cla.GetDtByProcedure("SP_GetActivetySearchNew", lst);
                //ActivityGroupID
                if (Request.QueryString.Count > 0)
                {
                    //ActivityGroupID
                    DataView dv = new DataView(dt);

                    string QrStr = Request.QueryString["T"].ToString().Replace(" ", "+");
                    dv.RowFilter = "ActivityGroupID=" + CommanClsLibrary.EncryptDecryptQueryString.BasicDecryptString(QrStr) + " ";
                    DataTable dc = dv.ToTable();
                    dt = dc;
                }
                else
                {
                    dt = null;
                }
                ddlActivity.DataSource = dt;
                ddlActivity.DataTextField = "ActivityName";
                ddlActivity.DataValueField = "ActivityID";
                ddlActivity.DataBind();
                ddlActivity.Items.Insert(0, new ListItem("--Select--", "0"));
                ddlActivity.SelectedIndex = 0;

            }
        }

        private void FillActivety()
        {
            List<String> lst = new List<string>();
            lst.Add(txtSearch.Text.Trim());
            if (ddlComponent.SelectedIndex == 0)
            {
                lst.Add("");
            }
            else
            {
                lst.Add(ddlComponent.SelectedValue.Trim());
            }
            if (ddlSubComponent.SelectedIndex == 0)
            {
                lst.Add("");
            }
            else
            {
                lst.Add(ddlSubComponent.SelectedValue.Trim());
            }
            if (ddlActivity.SelectedIndex == 0)
            {
                lst.Add("");
            }
            else
            {
                lst.Add(ddlActivity.SelectedValue.Trim());
            }

            if (Session["BeneficiaryTypesID"].ToString() != "1")
            {
                lst.Add("");
            }
            else
            {
                //farmer
                String LandStatus = cla.GetExecuteScalar("select LandStatus from Tbl_M_RegistrationDetails where RegistrationID=" + Session["RegistrationID"].ToString().Trim() + "");
                if (LandStatus.Trim().ToUpper() == "YES")
                {
                    lst.Add("");
                }
                else
                    lst.Add(LandStatus);
            }
            lst.Add(cla.GetExecuteScalar("select BeneficiaryTypesID from Tbl_M_RegistrationDetails where RegistrationID=" + Session["RegistrationID"].ToString().Trim() + ""));
            lst.Add(Session["RegistrationID"].ToString());
            DataTable dt = cla.GetDtByProcedure("SP_GetActivetySearchNew", lst);

            if (Request.QueryString.Count > 0)
            {
                //ActivityGroupID

                string QrStr = Request.QueryString["T"].ToString().Replace(" ", "+");

                DataView dv = new DataView(dt);
                dv.RowFilter = "ActivityGroupID=" + CommanClsLibrary.EncryptDecryptQueryString.BasicDecryptString(QrStr) + " ";
                DataTable dc = dv.ToTable();
                dt = dc;
            }
            else
            {
                dt = null;
            }

            DataList1.DataSource = dt;
            DataList1.DataBind();


            ddlActivity.DataSource = dt;
            ddlActivity.DataTextField = "ActivityName";
            ddlActivity.DataValueField = "ActivityID";
            ddlActivity.DataBind();
            ddlActivity.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlActivity.SelectedIndex = 0;


            int count = DataList1.Items.Count;
            for (int i = 0; i < count; i++)
            {
                Label lblActivityName = DataList1.Items[i].FindControl("ActivityName") as Label;
                Label lblActivityNameMr = DataList1.Items[i].FindControl("ActivityNameMr") as Label;
                Label Label1 = DataList1.Items[i].FindControl("Label1") as Label;

                lblActivityNameMr.Visible = false;
                lblActivityName.Visible = false;
                if (Session["Lang"].ToString().Trim() == "en-IN")
                {
                    lblActivityName.Visible = true;
                    Label1.Text = "SELECT";
                }
                else
                {
                    lblActivityNameMr.Visible = true;
                    Label1.Text = "निवडा";
                }
            }

        }


        private void FillActivityBySubDDL()
        {
            List<String> lst = new List<string>();
            lst.Add(txtSearch.Text.Trim());
            if (ddlComponent.SelectedIndex == 0)
            {
                lst.Add("");
            }
            else
            {
                lst.Add(ddlComponent.SelectedValue.Trim());
            }
            if (ddlSubComponent.SelectedIndex == 0)
            {
                lst.Add("");
            }
            else
            {
                lst.Add(ddlSubComponent.SelectedValue.Trim());
            }
            if (ddlActivity.SelectedIndex == 0)
            {
                lst.Add("");
            }
            else
            {
                lst.Add(ddlActivity.SelectedValue.Trim());
            }

            if (Session["BeneficiaryTypesID"].ToString() != "1")
            {
                lst.Add("");
            }
            else
            {
                //farmer
                String LandStatus = cla.GetExecuteScalar("select LandStatus from Tbl_M_RegistrationDetails where RegistrationID=" + Session["RegistrationID"].ToString().Trim() + "");
                if (LandStatus.Trim().ToUpper() == "YES")
                {
                    lst.Add("");
                }
                else
                    lst.Add(LandStatus);
            }
            lst.Add(cla.GetExecuteScalar("select BeneficiaryTypesID from Tbl_M_RegistrationDetails where RegistrationID=" + Session["RegistrationID"].ToString().Trim() + ""));
            lst.Add(Session["RegistrationID"].ToString());
            DataTable dt = cla.GetDtByProcedure("SP_GetActivetySearchNew", lst);
             

            ddlActivity.DataSource = dt;
            ddlActivity.DataTextField = "ActivityName";
            ddlActivity.DataValueField = "ActivityID";
            ddlActivity.DataBind();
            ddlActivity.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlActivity.SelectedIndex = 0;


            
        }

        private void FillActivityByActivityDDL()
        {
            List<String> lst = new List<string>();
            lst.Add(txtSearch.Text.Trim());
            if (ddlComponent.SelectedIndex == 0)
            {
                lst.Add("");
            }
            else
            {
                lst.Add(ddlComponent.SelectedValue.Trim());
            }
            if (ddlSubComponent.SelectedIndex == 0)
            {
                lst.Add("");
            }
            else
            {
                lst.Add(ddlSubComponent.SelectedValue.Trim());
            }
            if (ddlActivity.SelectedIndex == 0)
            {
                lst.Add("");
            }
            else
            {
                lst.Add(ddlActivity.SelectedValue.Trim());
            }

            if (Session["BeneficiaryTypesID"].ToString() != "1")
            {
                lst.Add("");
            }
            else
            {
                //farmer
                String LandStatus = cla.GetExecuteScalar("select LandStatus from Tbl_M_RegistrationDetails where RegistrationID=" + Session["RegistrationID"].ToString().Trim() + "");
                if (LandStatus.Trim().ToUpper() == "YES")
                {
                    lst.Add("");
                }
                else
                    lst.Add(LandStatus);
            }
            lst.Add(cla.GetExecuteScalar("select BeneficiaryTypesID from Tbl_M_RegistrationDetails where RegistrationID=" + Session["RegistrationID"].ToString().Trim() + ""));
            lst.Add(Session["RegistrationID"].ToString());
            DataTable dt = cla.GetDtByProcedure("SP_GetActivetySearchNew", lst);



            DataList1.DataSource = dt;
            DataList1.DataBind();


            ddlActivity.DataSource = dt;
            ddlActivity.DataTextField = "ActivityName";
            ddlActivity.DataValueField = "ActivityID";
            ddlActivity.DataBind();
            ddlActivity.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlActivity.SelectedIndex = 0;


            int count = DataList1.Items.Count;
            for (int i = 0; i < count; i++)
            {
                Label lblActivityName = DataList1.Items[i].FindControl("ActivityName") as Label;
                Label lblActivityNameMr = DataList1.Items[i].FindControl("ActivityNameMr") as Label;
                Label Label1 = DataList1.Items[i].FindControl("Label1") as Label;

                lblActivityNameMr.Visible = false;
                lblActivityName.Visible = false;
                if (Session["Lang"].ToString().Trim() == "en-IN")
                {
                    lblActivityName.Visible = true;
                    Label1.Text = "SELECT";
                }
                else
                {
                    lblActivityNameMr.Visible = true;
                    Label1.Text = "निवडा";
                }
            }
        }


     

        protected void ddlActivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlActivity.SelectedIndex == 0)
            //{
            //    ddlSubComponent.SelectedIndex = 0;
            //    ddlComponent.SelectedIndex = 0;
            //}

            //try
            //{
            //    ddlComponent.SelectedValue = cla.GetExecuteScalar("SELECT   ComponentID FROM Tbl_M_ActivityMaster WHERE IsDeleted IS NULL AND  (ActivityID = " + ddlActivity.SelectedValue.Trim() + ")");
            //}
            //catch
            //{

            //}
            //try
            //{
            //    ddlSubComponent.SelectedValue = cla.GetExecuteScalar("SELECT   SubComponentID FROM Tbl_M_ActivityMaster WHERE IsDeleted IS NULL AND (ActivityID = " + ddlActivity.SelectedValue.Trim() + ")");
            //}
            //catch
            //{

            //}

            //FillActivety();

            FillActivityByActivityDDL();
        }

        protected void ddlComponent_SelectedIndexChanged(object sender, EventArgs e)
        {

            // FillActivety();

            ddlSubComponent.DataSource = Comcls.SubGetComponent(ddlComponent.SelectedValue.Trim(), Session["BeneficiaryTypesID"].ToString());
            ddlSubComponent.DataTextField = "SubComponentName";
            ddlSubComponent.DataValueField = "SubComponentID";
            ddlSubComponent.DataBind();
            ddlSubComponent.Items.Insert(0, new ListItem("--Select--", "0"));

            //List<String> lst = new List<string>();
            //lst.Add("");
            //lst.Add(ddlComponent.SelectedValue.Trim());
            //lst.Add("");
            //lst.Add("");
            //lst.Add("");
            //lst.Add(Session["BeneficiaryTypesID"].ToString());
            //if (Session["Lang"].ToString().Trim() != "en-IN")
            //{
            //    DataTable dt = cla.GetDtByProcedure("SP_GetActivetySearchNewMr", lst);
            //    ddlActivity.DataSource = dt;
            //    ddlActivity.DataTextField = "ActivityName";
            //    ddlActivity.DataValueField = "ActivityID";
            //    ddlActivity.DataBind();
            //    ddlActivity.Items.Insert(0, new ListItem("--Select--", "0"));
            //    ddlActivity.SelectedIndex = 0;

            //}
            //else
            //{
            //    DataTable dt = cla.GetDtByProcedure("SP_GetActivetySearchNew", lst);
            //    ddlActivity.DataSource = dt;
            //    ddlActivity.DataTextField = "ActivityName";
            //    ddlActivity.DataValueField = "ActivityID";
            //    ddlActivity.DataBind();
            //    ddlActivity.Items.Insert(0, new ListItem("--Select--", "0"));
            //    ddlActivity.SelectedIndex = 0;

            //}
            //DataTable dt = cla.GetDtByProcedure("SP_GetActivetySearchNew", lst);
            //ddlActivity.DataSource = dt;
            //ddlActivity.DataTextField = "ActivityName";
            //ddlActivity.DataValueField = "ActivityID";
            //ddlActivity.DataBind();
            //ddlActivity.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlActivity.SelectedIndex = 0;
        }

        protected void ddlSubComponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillActivityBySubDDL();

            //List<String> lst = new List<string>();
            //lst.Add("");
            //lst.Add("");
            //lst.Add(ddlSubComponent.SelectedValue.Trim());
            //lst.Add("");
            //lst.Add("");
            //lst.Add(Session["BeneficiaryTypesID"].ToString());

            //if (Session["Lang"].ToString().Trim() != "en-IN")
            //{
            //    DataTable dt = cla.GetDtByProcedure("SP_GetActivetySearchNewMr", lst);
            //    ddlActivity.DataSource = dt;
            //    ddlActivity.DataTextField = "ActivityName";
            //    ddlActivity.DataValueField = "ActivityID";
            //    ddlActivity.DataBind();
            //    ddlActivity.Items.Insert(0, new ListItem("--Select--", "0"));
            //    ddlActivity.SelectedIndex = 0;

            //}
            //else
            //{
            //    DataTable dt = cla.GetDtByProcedure("SP_GetActivetySearchNew", lst);
            //    ddlActivity.DataSource = dt;
            //    ddlActivity.DataTextField = "ActivityName";
            //    ddlActivity.DataValueField = "ActivityID";
            //    ddlActivity.DataBind();
            //    ddlActivity.Items.Insert(0, new ListItem("--Select--", "0"));
            //    ddlActivity.SelectedIndex = 0;

            //}
            //DataTable dt = cla.GetDtByProcedure("SP_GetActivetySearchNew", lst);
            //ddlActivity.DataSource = dt;
            //ddlActivity.DataTextField = "ActivityName";
            //ddlActivity.DataValueField = "ActivityID";
            //ddlActivity.DataBind();
            //ddlActivity.Items.Insert(0, new ListItem("--Select--", "0"));
            //ddlActivity.SelectedIndex = 0;
        }

        //protected void txtSearch_TextChanged(object sender, EventArgs e)
        //{
        //    FillActivety();
        //}

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillActivety(); ;
        }

        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Path, false);
        }
    }
}