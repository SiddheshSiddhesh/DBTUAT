using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanClsLibrary.Repository.Interfaces
{
    interface ISyncAdharVaultReferencesToPocraDB
    {
        DataSet GetFPOPending();

        bool UpdateFPOReference(string FPOBoltID, string AadharNo, string ADVRefrenceID);

        DataSet GetFarmersPending();

        DataSet GetFarmersPendingDesc();

        bool UpdateFarmersReference(string BoltID, string AadharNo, string ADVRefrenceID);
    }
}
