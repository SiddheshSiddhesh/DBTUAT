using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanClsLibrary.Repository.Interfaces
{
    interface IDBTAppAPI
    {

        bool InsertApplication_WorkCompletionsPartial(string WorkCompletionID, string ApplicationID, string DocumentDetails, string DocumentUploaded, string CompletionDate, string DocTypes, string DocLevels, string WorkReportID, string IsInspection, string LatitudeMap, string LongitudeMap);



    }
}
