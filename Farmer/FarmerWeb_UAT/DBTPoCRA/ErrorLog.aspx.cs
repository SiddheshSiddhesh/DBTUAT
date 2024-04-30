using CommanClsLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBTPoCRA
{
    public partial class ErrorLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MyClass cla = new MyClass();
            List<String> lst = new List<string>();
            cla.GetDtByProcedure("SP_CommanProCall", lst);
            if(Request.QueryString.Count>0)
            Literal1.Text = Request.QueryString["message"].ToString();

 
            DataTable dt = cla.GetDataTable("Select distinct v.VillageCode, a.APP_VillageID from Tbl_T_ApplicationDetails a inner join Tbl_M_VillageMaster v on v.VillageID=a.APP_VillageID where v.IsDeleted is not null and a.IsDeleted is null and a.ApplicationStatusID not in (2,25)");

            for (int x = 0; x != dt.Rows.Count; x++)
            {
               // String ApplicationID = dt.Rows[x]["ApplicationID"].ToString();
                String VillageCode = dt.Rows[x]["VillageCode"].ToString().Trim();
                String APP_VillageID = dt.Rows[x]["APP_VillageID"].ToString().Trim();

                String NewVillageID = cla.GetExecuteScalar("Select VillageID from Tbl_M_VillageMaster where VillageCode='"+VillageCode.Trim()+"' and IsDeleted is null");
                if(NewVillageID.Trim().Length>0)
                {
                    cla.ExecuteCommand("UPDATE Tbl_T_ApplicationDetails set APP_VillageID="+ NewVillageID + " where APP_VillageID="+ APP_VillageID + "");
                }

            }
        }
    }
}