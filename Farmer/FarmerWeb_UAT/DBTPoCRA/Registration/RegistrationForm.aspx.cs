using CommanClsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBTPoCRA.Registration
{
    public partial class RegistrationForm : System.Web.UI.Page
    {
        #region"Declarection"
        MyClass cla = new MyClass();
        MyCommanClass Comcls = new MyCommanClass();
       
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    FillDropDowns();

            //}
        }

        #region "ddls"
        private void FillDropDowns()
        {
           
            ddlBENEFICIARY.DataSource = Comcls.GetBeneficiaryType();
            ddlBENEFICIARY.DataTextField = "BeneficiaryTypes";
            ddlBENEFICIARY.DataValueField = "BeneficiaryTypesID";
            ddlBENEFICIARY.DataBind();
            ddlBENEFICIARY.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlBENEFICIARY.SelectedIndex = 0;
        }
        #endregion

        protected void ddlBENEFICIARY_SelectedIndexChanged(object sender, EventArgs e)
        {

            frmIfrem.Attributes["src"] = "";
            if (ddlBENEFICIARY.SelectedItem.Value.Trim() == "1")
            {
                //Individual
                frmIfrem.Attributes["src"] = "IndividualRegistration.aspx";
            }
            else if (ddlBENEFICIARY.SelectedItem.Value.Trim() == "2")
            {
                //Community
                frmIfrem.Attributes["src"] = "CommunityRegistration.aspx";
            }
            else 
            {
                //Others
                frmIfrem.Attributes["src"] = "OthersRegistration.aspx";
            }

        }

    }
}