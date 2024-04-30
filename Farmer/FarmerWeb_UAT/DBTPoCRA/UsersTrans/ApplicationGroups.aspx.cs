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
    public partial class ApplicationGroups : System.Web.UI.Page
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
                    hfCustomerId.Value = Session["BeneficiaryTypesID"].ToString();
                    FillDll();
                    FillActivety();
                    ((Literal)Master.FindControl("lblHeadings")).Text = "Application Form";
                    HiddenFieldLan.Value = Session["Lang"].ToString().Trim();
                    if (Session["Lang"].ToString().Trim() != "en-IN")
                    {
                        Literal1.Text = "सूचीमधून शोध क्रियाकलाप";
                        //Literal2.Text = "घटक";
                        //Literal3.Text = "उपघटक";
                        Literal4.Text = "योजना गट";
                        ((Literal)Master.FindControl("lblHeadings")).Text = "अर्ज";
                        btnSearch.Text = "शोध";
                        btnShowAll.Text = "सगळं दाखवा";
                    }


                }
            }
            catch { }
        }

        private void FillDll()
        { 
            List<String> lst = new List<string>();

            lst.Add("");

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

            lst.Add(cla.GetExecuteScalar("Select Tbl_M_VillageMaster.KharpanOrOther from Tbl_M_VillageMaster where VillageID in (Select R.Work_VillageID from Tbl_M_RegistrationDetails R where R.RegistrationID=" + Session["RegistrationID"].ToString().Trim() + " and R.IsDeleted is null )"));
            lst.Add(Session["Lang"].ToString().Trim());
            lst.Add(Session["RegistrationID"].ToString());
            DataTable dt = cla.GetDtByProcedure("SP_GetActivetyGroupSearchNew", lst);



            ddlActivityGroups.DataSource = dt;
            ddlActivityGroups.DataTextField = "ActivityGroupName";
            ddlActivityGroups.DataValueField = "ActivityGroupID";
            ddlActivityGroups.DataBind();
            ddlActivityGroups.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlActivityGroups.SelectedIndex = 0;


        }

        private void FillActivety()
        {
            List<String> lst = new List<string>();
            if (ddlActivityGroups.SelectedIndex > 0)
            {
                lst.Add(ddlActivityGroups.SelectedValue.Trim());
            }
            else
            {
                lst.Add("");
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

            lst.Add(cla.GetExecuteScalar("Select Tbl_M_VillageMaster.KharpanOrOther from Tbl_M_VillageMaster where VillageID in (Select R.Work_VillageID from Tbl_M_RegistrationDetails R where R.RegistrationID=" + Session["RegistrationID"].ToString().Trim() + " and R.IsDeleted is null )"));
            lst.Add(Session["Lang"].ToString().Trim());
            lst.Add(Session["RegistrationID"].ToString());
            DataTable dt = cla.GetDtByProcedure("SP_GetActivetyGroupSearchNew", lst);
            DataList1.DataSource = dt;
            DataList1.DataBind();




            int count = DataList1.Items.Count;
            for (int i = 0; i < count; i++)
            {
                //Label lblActivityName = DataList1.Items[i].FindControl("ActivityName") as Label;
                //Label lblActivityNameMr = DataList1.Items[i].FindControl("ActivityNameMr") as Label;
                Label Label1 = DataList1.Items[i].FindControl("Label1") as Label;

                //lblActivityNameMr.Visible = false;
                //lblActivityName.Visible = false;
                if (Session["Lang"].ToString().Trim() == "en-IN")
                {
                    //lblActivityName.Visible = true;
                    Label1.Text = "SELECT";
                }
                else
                {
                    //lblActivityNameMr.Visible = true;
                    Label1.Text = "निवडा";
                }
            }

        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FillActivety();
        }

        protected void btnShowAll_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Path, false);
        }
    }
}