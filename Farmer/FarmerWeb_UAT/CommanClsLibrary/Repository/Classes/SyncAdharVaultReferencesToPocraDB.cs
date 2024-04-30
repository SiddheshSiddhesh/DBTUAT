using CommanClsLibrary.Repository.Interfaces;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanClsLibrary.Repository.Classes
{
    public class SyncAdharVaultReferencesToPocraDB : ISyncAdharVaultReferencesToPocraDB
    {

        public DataSet GetFPOPending()
        {
            DataSet ds = new DataSet();
            string CommandString = "select top 1000 FPOBoltID,AadharNo,ADVRefrenceID from FPO_M_RegistrationDetails_Bolt where ADVRefrenceID is null";
            try
            {
                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString(), CommandType.Text, CommandString);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public bool UpdateFPOReference(string FPOBoltID, string AadharNo, string ADVRefrenceID)
        {

            string spName = "Usp_UpdateFPO_AdharVaultReference";
            int RowsAffected = 0;
            SqlParameter[] parameter = new SqlParameter[10];

            parameter[0] = new SqlParameter("@FPOBoltID", FPOBoltID);
            parameter[1] = new SqlParameter("@AadharNo", AadharNo);
            parameter[2] = new SqlParameter("@ADVRefrenceID", ADVRefrenceID);

            try
            {
                RowsAffected = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString(), CommandType.StoredProcedure, spName, parameter);
            }
            catch (Exception ex)
            {

            }

            return true;
        }


        public DataSet GetFarmersPending()
        {
            DataSet ds = new DataSet();
            //string CommandString = "SELECT top 10000 BoltID,AaDharNumber,ADVRefrenceID   FROM  Tbl_M_RegistrationDetails_Bolt  WHERE ADVRefrenceID is null and IsDeleted is null order by BoltID asc";
            string CommandString = @"select top 10000 R.BoltID,B.AaDharNumber,B.ADVRefrenceID 
from Tbl_M_RegistrationDetails R
left join Tbl_M_RegistrationDetails_Bolt B
on R.BoltID=B.BoltID and B.IsDeleted is null 
where 
B.ADVRefrenceID is null 
and R.BoltID is not null 
and B.AaDharNumber is not null
and R.RegistrationID in 
(select distinct RegistrationID from Tbl_T_ApplicationDetails App where App.IsDeleted is null)
order by R.BoltID asc";


            try
            {
                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString(), CommandType.Text, CommandString);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public DataSet GetFarmersPendingDesc()
        {
            DataSet ds = new DataSet();
            //string CommandString = "SELECT top 10000 BoltID,AaDharNumber,ADVRefrenceID   FROM  Tbl_M_RegistrationDetails_Bolt  WHERE ADVRefrenceID is null and IsDeleted is null order by BoltID desc";

            string CommandString = @"select top 10000 R.BoltID,B.AaDharNumber,B.ADVRefrenceID 
from Tbl_M_RegistrationDetails R
left
join Tbl_M_RegistrationDetails_Bolt B
on R.BoltID = B.BoltID and B.IsDeleted is null
where
B.ADVRefrenceID is null
and R.BoltID is not null
and B.AaDharNumber is not null
and R.RegistrationID in 
(select distinct RegistrationID from Tbl_T_ApplicationDetails App where App.IsDeleted is null)
order by R.BoltID desc";
                
            try
            {
                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString(), CommandType.Text, CommandString);
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        public bool UpdateFarmersReference(string BoltID, string AadharNo, string ADVRefrenceID)
        {

            string spName = "Usp_UpdateFarmers_AdharVaultReference";
            int RowsAffected = 0;
            SqlParameter[] parameter = new SqlParameter[10];

            parameter[0] = new SqlParameter("@BoltID", BoltID);
            parameter[1] = new SqlParameter("@AadharNo", AadharNo);
            parameter[2] = new SqlParameter("@ADVRefrenceID", ADVRefrenceID);

            try
            {
                RowsAffected = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionString(), CommandType.StoredProcedure, spName, parameter);
            }
            catch (Exception ex)
            {

            }

            return true;
        }
    }
}
