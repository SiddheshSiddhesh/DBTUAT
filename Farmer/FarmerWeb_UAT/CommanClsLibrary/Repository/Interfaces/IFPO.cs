using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanClsLibrary.Repository.Interfaces
{
    interface IFPO
    {
        string GetFPORegistrationID(string RegistrationNo);

        string CheckFPORegistrationID(string RegistrationNo);

        bool IsFPOApplicationPaid(string ApplicationID);

        DataSet GetAllFPODeskCountData();

        DataSet GetAllFPODDeskData();         
    }
}
