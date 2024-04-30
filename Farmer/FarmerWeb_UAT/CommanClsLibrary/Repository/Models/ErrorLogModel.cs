using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanClsLibrary.Repository.Models
{
    public class ErrorLogModel
    {
        public string ErrorDescription { get; set; }
        public int ErrorSeverity { get; set; }
        public string ProjectName { get; set; }
        public string ErrorTitle { get; set; }
    }
}
