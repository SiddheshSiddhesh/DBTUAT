using CommanClsLibrary.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanClsLibrary.Repository.Interfaces
{
    interface IAdharVaultAPICalls
    {
        string GetAdharFromReference(string ReferenceNumber);

        string GetReferenceFromAdhar(string AdharNumber);

        AdharOTP_Response GetAdharOTP(string AdharNumber);

        AdharOTP_Response VerifyAadharOTP(string AdharNumber,string TxnNo, string OTP);
    }
}
