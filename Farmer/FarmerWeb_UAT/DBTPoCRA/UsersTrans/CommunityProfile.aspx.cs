using System.Text;
using CommanClsLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Resources;
using System.Globalization;
using System.Threading;

namespace DBTPoCRA.UsersTrans
{
    public partial class CommunityProfile : System.Web.UI.Page
    {
       
        
        DataTable dt = new DataTable();
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
                    ViewState["RegistrationID"] = Request.QueryString["ID"].ToString();
                   
                    ddlGramPanchayatCode_SelectedIndexChanged();

                 

                   
                }
                else
                {
                    
                }
            }
            catch { }
        }

        protected void ddlGramPanchayatCode_SelectedIndexChanged()
        {

            string RegID = ViewState["RegistrationID"].ToString();// cla.GetExecuteScalar("select RegistrationID from Tbl_M_RegistrationDetails where GramPanchayatCode='" + ddlGramPanchayatCode.SelectedItem + "' ");
            if (RegID.Length > 0)
            {
                List<String> lst = new List<String>();
                lst.Add(RegID.ToString());

                dt = new DataTable();
                dt = cla.GetDtByProcedure("SP_CommunityRegistration_View_Details_comm", lst);
                ViewState["RegistrationID"] = Convert.ToInt32(RegID);
                if (dt.Rows.Count > 0)
                {
                    txtName.Text = dt.Rows[0]["RegisterName"].ToString();//RegisterName  
                    lblVillage.Text= dt.Rows[0]["GramPanchayatCode"].ToString();//VillageName
                    txtName.Text = Convert.ToString( dt.Rows[0]["RegisterName"]);
                    txtCom_PeriodofMicroplanningFrom.Text = Convert.ToString(dt.Rows[0]["Com_PeriodofMicroplanningFrom"]);
                    txtCom_PeriodofMicroplanningFromTo.Text = Convert.ToString(dt.Rows[0]["Com_PeriodofMicroplanningTo"]);
                    txtCom_ProjectReportDate.Text = Convert.ToString(dt.Rows[0]["Com_ProjectReportDate"]);
                    txtCom_DCCApprovalDate.Text = Convert.ToString(dt.Rows[0]["Com_DCCApprovalDate"]);
                    txtTotal1.Text = Convert.ToString(dt.Rows[0]["Com_For21ProjectReport"]);
                    txtTotal2.Text = Convert.ToString(dt.Rows[0]["Com_UseOfWaterProjectReport"]);
                    txtTotal3.Text = Convert.ToString(dt.Rows[0]["Com_CFCReport"]);
                  
                   txtCom_VillageDevelopmentPlan.Text = Convert.ToString(dt.Rows[0]["Com_VillageDevelopmentPlan"]);
                    txtCom_UpscaleTCM.Text = Convert.ToString(dt.Rows[0]["Com_UpscaleTCM"]);
                    txtCom_PreventionTCM.Text = Convert.ToString(dt.Rows[0]["Com_PreventionTCM"]);
                    txtCom_BalanceExclusionTCM.Text = Convert.ToString(dt.Rows[0]["Com_BalanceExclusionTCM"]);
                    txtCom_TroubleshootTCM.Text = Convert.ToString(dt.Rows[0]["Com_Troubleshoot"]);
                    Label3.Text = Convert.ToString(dt.Rows[0]["eTerndingBy"]);
                    txtForestrybasedfarmingPractices.Text = Convert.ToString(dt.Rows[0]["ForestrybasedfarmingPractices"]);
                    txtTotalorchardPlanting.Text = Convert.ToString(dt.Rows[0]["TotalorchardPlanting"]);

                    txtTotalalkalinelandmanagement.Text = Convert.ToString(dt.Rows[0]["Totalalkalinelandmanagement"]);
                    txtTotalprotectedfarming.Text = Convert.ToString(dt.Rows[0]["Totalprotectedfarming"]);
                    txtTotalIntegratedfarming.Text = Convert.ToString(dt.Rows[0]["TotalIntegratedfarming"]);

                    ImprovingoverallSoilhealth.Text = Convert.ToString(dt.Rows[0]["ImprovingoverallSoilhealth"]);
                    txtEfficientAndsustainableuseoftotalwater.Text = Convert.ToString(dt.Rows[0]["EfficientAndsustainableuseoftotalwater"]);
                    txtWaterStorageAtthebase.Text = Convert.ToString(dt.Rows[0]["WaterStorageAtthebase"]);
                    txtFineIrrigation.Text = Convert.ToString(dt.Rows[0]["FineIrrigation"]);
                    txtAvailabilitywaterForProtectedIrrigation.Text = Convert.ToString(dt.Rows[0]["AvailabilitywaterForProtectedIrrigation"]);
                    txtCreatingInfrastructure.Text = Convert.ToString(dt.Rows[0]["CreatingInfrastructure"]);
                    txtAgriculturalEquipmentCenter.Text = Convert.ToString(dt.Rows[0]["AgriculturalEquipmentCenter"]);

                    txtClimatefriendlyvarieties.Text = Convert.ToString(dt.Rows[0]["Climatefriendlyvarieties"]);
                    txtTotalSeedHubInfrastructure.Text = Convert.ToString(dt.Rows[0]["TotalSeedHubInfrastructure"]);

                    try
                    {
                        Label4.Text = "<a href='"+clsSettings.BaseUrl+""+dt.Rows[0]["SanctionedVDP"].ToString() + "' > View & Download </a> ";//SanctionedVDP
                    }
                    catch { }

                    try
                    {
                        if(dt.Rows[0]["MOMUpload"].ToString().Length>0)
                        Label5.Text = "<a href='" + clsSettings.BaseUrl + "" + dt.Rows[0]["MOMUpload"].ToString() + "' > View & Download </a> ";//SanctionedVDP
                    }
                    catch { }
                    //

                    dt = cla.GetDataTable("SELECT WM.SoilAndWaterRetentionWorksID ,WM.SoilAndWaterRetentionWorksGroup ,WM.SoilAndWaterRetentionWorksMr ,WM.UnitofMess,A.FYearPhysical ,A.FYearAmount  ,A.SYearPhysical ,A.SYearAmount ,A.TYearPhysical ,A.TYearAmount  ,A.TotalPhysical ,A.TotalAmount,A.TotalPysicalNumber FROM Tbl_M_SoilAndWaterRetentionWorks as  WM  left outer join Tbl_T_SoilAndWaterRetentionWorks A on A.SoilAndWaterRetentionWorksID=WM.SoilAndWaterRetentionWorksID where WM.IsDeleted is null and A.IsDeleted is null and A.RegistrationID=" + ViewState["RegistrationID"].ToString().Trim() + " order by WM.OrderbyNo,WM.SoilAndWaterRetentionWorksGroup desc ");
                    GrdWorkDetails.DataSource = dt;
                    GrdWorkDetails.DataBind();



                }
            }
        }

        protected void TextBox1_TextChangedPhysical(object sender, EventArgs e)
        {

            TextBox txtFocus = (TextBox)sender;
            GridViewRow gridViewRow = (GridViewRow)txtFocus.NamingContainer;
            Double a = 0, b = 0, c = 0, d = 0, e1 = 0, f = 0, g = 0, h = 0;
            foreach (GridViewRow gr in GrdWorkDetails.Rows)
            {
                Double Amount = 0, Physical = 0;
                TextBox TextBox1 = (TextBox)(gr.FindControl("TextBox1"));
                TextBox TextBox2 = (TextBox)(gr.FindControl("TextBox2"));
                TextBox TextBox3 = (TextBox)(gr.FindControl("TextBox3"));
                TextBox TextBox4 = (TextBox)(gr.FindControl("TextBox4"));
                TextBox TextBox5 = (TextBox)(gr.FindControl("TextBox5"));
                TextBox TextBox6 = (TextBox)(gr.FindControl("TextBox6"));
                Label Label1 = (Label)(gr.FindControl("Label1"));
                Label Label2 = (Label)(gr.FindControl("Label2"));

                if (TextBox1.Text.Trim().Length > 0)
                {
                    Physical = Physical + Convert.ToDouble(TextBox1.Text.Trim());
                    a = a + Convert.ToDouble(TextBox1.Text.Trim());
                }
                if (TextBox2.Text.Trim().Length > 0)
                {
                    Amount = Amount + Convert.ToDouble(TextBox2.Text.Trim());
                    b = b + Convert.ToDouble(TextBox2.Text.Trim());
                }
                if (TextBox3.Text.Trim().Length > 0)
                {
                    Physical = Physical + Convert.ToDouble(TextBox3.Text.Trim());
                    c = c + Convert.ToDouble(TextBox3.Text.Trim());
                }
                if (TextBox4.Text.Trim().Length > 0)
                {
                    Amount = Amount + Convert.ToDouble(TextBox4.Text.Trim());
                    d = d + Convert.ToDouble(TextBox4.Text.Trim());
                }
                if (TextBox5.Text.Trim().Length > 0)
                {
                    Physical = Physical + Convert.ToDouble(TextBox5.Text.Trim());
                    e1 = e1 + Convert.ToDouble(TextBox5.Text.Trim());
                }
                if (TextBox6.Text.Trim().Length > 0)
                {
                    Amount = Amount + Convert.ToDouble(TextBox6.Text.Trim());
                    f = f + Convert.ToDouble(TextBox6.Text.Trim());
                }

                Label2.Text = Amount.ToString("0.00");
                Label1.Text = Physical.ToString("0.00");

                if (Label2.Text.Trim().Length > 0)
                {
                    g = g + Convert.ToDouble(Label2.Text.Trim());
                }
                if (Label1.Text.Trim().Length > 0)
                {
                    h = h + Convert.ToDouble(Label1.Text.Trim());
                }
            }
            GrdWorkDetails.FooterRow.Cells[2].Text = "एकूण एकंदर ";
            GrdWorkDetails.FooterRow.Cells[3].Text = a.ToString();
            GrdWorkDetails.FooterRow.Cells[4].Text = b.ToString();
            GrdWorkDetails.FooterRow.Cells[5].Text = c.ToString();
            GrdWorkDetails.FooterRow.Cells[6].Text = d.ToString();
            GrdWorkDetails.FooterRow.Cells[7].Text = e1.ToString();
            GrdWorkDetails.FooterRow.Cells[8].Text = f.ToString();
            GrdWorkDetails.FooterRow.Cells[9].Text = h.ToString();
            GrdWorkDetails.FooterRow.Cells[10].Text = g.ToString();


            #region "Focus"
            if (txtFocus.ID == "TextBox1")
            {
                TextBox txt = (TextBox)gridViewRow.FindControl("TextBox2");
                txt.Focus();
            }
            else if (txtFocus.ID == "TextBox2")
            {
                TextBox txt = (TextBox)gridViewRow.FindControl("TextBox3");
                txt.Focus();
            }
            else if (txtFocus.ID == "TextBox3")
            {
                TextBox txt = (TextBox)gridViewRow.FindControl("TextBox4");
                txt.Focus();
            }
            else if (txtFocus.ID == "TextBox4")
            {
                TextBox txt = (TextBox)gridViewRow.FindControl("TextBox5");
                txt.Focus();
            }
            else if (txtFocus.ID == "TextBox5")
            {
                TextBox txt = (TextBox)gridViewRow.FindControl("TextBox6");
                txt.Focus();
            }
            else
            {
                GrdWorkDetails.Focus();
                txtFocus.Focus();
            }
            #endregion
        }


    }
}
