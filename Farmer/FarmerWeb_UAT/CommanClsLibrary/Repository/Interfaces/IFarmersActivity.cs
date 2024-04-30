using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanClsLibrary.Repository.Interfaces
{
    interface IFarmersActivity
    {
        DataTable SearchFarmerByNameandAadhaar(string searchString);
    }
}
