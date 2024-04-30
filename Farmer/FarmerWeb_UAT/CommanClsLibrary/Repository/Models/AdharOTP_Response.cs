using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CommanClsLibrary.Repository.Models
{     
    public class AdharOTP_Response
    {
        public String Txn { get; set; }
        public string Message { get; set; }
        public string MessageCode { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string gender { get; set; }
        public string dob { get; set; }
        public string name { get; set; }
        public string nameInMarathi { get; set; }
        public string ImageUrl { get; set; }
        public string MessageExp { get; set; }
        public XmlDocument xmlReturn { get; set; }

    }
}
