using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanClsLibrary.Repository.Models
{
    class AdharVaultAPIResponse
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public string UID { get; set; }
        public string Reference { get; set; }
    }
}
