using CommanClsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBTPoCRA.UsersTrans
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        MyClass cla = new MyClass();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            String s = cla.GetExecuteScalar("SELECT  UserId   FROM   Tbl_M_LoginDetails  WHERE     (UserId =" + Session["UserId"].ToString() + ") AND (UPass ='" + TextBox1.Text + "')");
            if (s.Trim().Length != 0)
            {
                String a = "";
                a = cla.ExecuteCommand("UPDATE Tbl_M_LoginDetails  SET  UPass ='" + TextBox2.Text + "'   WHERE     (UserId =" + Session["UserId"].ToString() + ")");
                if (a.Trim().Length == 0)
                    clsMessages.Sucessmsg(Label1, "C");
            }
            else
            {
                //Label1.Text = "";
                clsMessages.Errormsg(Label1, "Please Enter Correct Old Password");
            }
        }
    }
}