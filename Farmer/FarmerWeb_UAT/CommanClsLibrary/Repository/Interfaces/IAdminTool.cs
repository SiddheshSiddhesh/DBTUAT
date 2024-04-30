using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanClsLibrary.Repository.Interfaces
{
    interface IAdminTool
    {
        DataTable GetUserLastUpdatedPasswordDetails(string UserID);


        bool UpdateOfficialLoginPassword(string Username, string UserID, string IP, string Password, string Source);

        DataTable GetOfficialUserInfoByUsername(string Username);

    }
}
