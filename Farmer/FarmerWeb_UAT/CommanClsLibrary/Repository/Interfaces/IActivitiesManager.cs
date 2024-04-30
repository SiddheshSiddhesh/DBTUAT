using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanClsLibrary.Repository.Interfaces
{
    interface IActivitiesManager
    {
        DataSet GetRemainingB2BNotification();

        bool UpdateB2BNotificationSendStatus(int ApplicationID);


        DataSet GetPresanctionCancellationListByFarmerID(string FarmerRegistrationID);

        DataSet GetApplicationDetailsByID(string ApplicationID);

        bool SendSMSForAddingPrevocationDate(string MobileNumber, string ApplicationCode, string ActivityGroupName, string ActivityGroupNameMr, string PresanctionLimitDate);

        bool UpdateRevokePresanctionCancellation(int ApplicationID, DateTime PresanctionRevokeDate, int PresanctionRevokeBy);

        bool SendSMSForRemovingPresanction_ForAddedInvocation(string MobileNumber, string ApplicationCode, string ActivityGroupName, string ActivityGroupNameMr, string PresanctionCancellationDate);

        DataSet GetListOfPresanctionInvocationApplicationsToCancel();

        bool UpdatePresanctionInvocation_SetToRejectedAfterInvocation(int ApplicationID);
    }
}
