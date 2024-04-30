using CommanClsLibrary.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanClsLibrary.Repository.Interfaces
{
    interface IErrorLogManager
    {
        bool InsertErrorLog(ErrorLogModel error);
    }
}
