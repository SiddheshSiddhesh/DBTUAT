using CommanClsLibrary.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanClsLibrary.Repository.Classes
{
    public class FarmersActivity : IFarmersActivity
    {
        public DataTable SearchFarmerByNameandAadhaar(string searchString)
        {
            MyClass cla = new MyClass();
            DataTable dt = new DataTable();
            dt = cla.GetDataTable("Select top 20 RegisterName +' /'+CONVERT(nvarchar(50),RegistrationID) as RegisterName , RegistrationID  from Tbl_M_RegistrationDetails where IsDeleted is null and RegisterName like '" + searchString + "%' order by RegisterName");
            if (dt.Rows.Count == 0)
            {
                if (searchString.Length == 12)
                {
                    //AdharVaultAPICalls api = new AdharVaultAPICalls();
                    //string ReferenceNumber = api.GetReferenceFromAdhar(searchString.Trim());
                    //dt = cla.GetDataTable("Select top 1 R.RegisterName +' /'+CONVERT(nvarchar(50),R.RegistrationID) as RegisterName , R.RegistrationID  from Tbl_M_RegistrationDetails R inner join Tbl_M_RegistrationDetails_Bolt b on b.BoltID=R.BoltID where R.IsDeleted is null and b.ADVRefrenceID='" + ReferenceNumber + "' and b.IsDeleted is null order by R.RegisterName");

                    dt = cla.GetDataTable("Select top 1 R.RegisterName +' /'+CONVERT(nvarchar(50),R.RegistrationID) as RegisterName , R.RegistrationID  from Tbl_M_RegistrationDetails R inner join Tbl_M_RegistrationDetails_Bolt b on b.BoltID=R.BoltID where R.IsDeleted is null and b.AaDharNumber='" + searchString.Trim() + "' and b.IsDeleted is null order by R.RegisterName");
                }
            }

            return dt;
        }
    }
}
