using CommanClsLibrary.Repository.Interfaces;
using CommanClsLibrary.Repository.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CommanClsLibrary.Repository.Enums;

namespace CommanClsLibrary.Repository.Classes
{
    public class DBTAppAPI : IDBTAppAPI
    {

        public bool InsertApplication_WorkCompletionsPartial(string WorkCompletionID, string ApplicationID, string DocumentDetails, string DocumentUploaded, string CompletionDate, string DocTypes, string DocLevels, string WorkReportID, string IsInspection, string LatitudeMap, string LongitudeMap)
        {
            bool result = false;

            String Sql = "INSERT INTO Tbl_T_Application_WorkCompletions(WorkCompletionID, ApplicationID,  DocumentDetails, DocumentUploaded,CompletionDate,DocTypes,DocLevels,IsInspection, LatitudeMap,LongitudeMap)";
            Sql += " VALUES(" + WorkCompletionID + "," + ApplicationID.Trim() + ",N'" + DocumentDetails.Trim().Replace("'", "") + "','" + DocumentUploaded + "',GETDATE(),N'" + DocTypes.Trim() + "',N'" + DocLevels.Trim() + "','1','" + LatitudeMap.Trim() + "','" + LongitudeMap.Trim() + "') ";

            int RowsAffected = 0;

            try
            {
                RowsAffected = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString(), CommandType.Text, Sql);

                if (RowsAffected > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;

                    ErrorLogModel err = new ErrorLogModel();
                    err.ErrorTitle = "InsertApplication_WorkCompletionsPartial:DBTAppAPI";
                    err.ProjectName = "POCRA WEB CLASS LIBRARY";
                    err.ErrorDescription = "Sql :" + Sql + " | Not Saved ";
                    err.ErrorSeverity = (int)ErrorSeverity.High;
                    new ErrorLogManager().InsertErrorLog(err);
                }
            }
            catch (Exception ex)
            {
                result = false;

                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "InsertApplication_WorkCompletionsPartial:DBTAppAPI";
                err.ProjectName = "POCRA WEB CLASS LIBRARY";
                err.ErrorDescription = "Sql :" + Sql + "\n Exception : " + ex.Message + " \n Inner Exception :" + ex.InnerException;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new ErrorLogManager().InsertErrorLog(err);
            }

            return result;
        }
    }
}
