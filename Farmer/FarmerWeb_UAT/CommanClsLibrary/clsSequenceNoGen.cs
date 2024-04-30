using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanClsLibrary
{

    
    public class clsSequenceNoGen
    {
        

        public static void PreSequenceNoGen(String UserID,String ApprovalStageID)
        {
            MyClass cla = new MyClass();
            DataTable dt = new DataTable();
            List<String> lst = new List<string>();
            lst.Add(UserID);
            lst.Add(ApprovalStageID);
            dt = cla.GetDtByProcedure("SP_Get_ApplicationSequenceGen", lst);

            int x = 1;
            foreach(DataRow dr in dt.Rows)
            {
                String ApplicationID = dr["ApplicationID"].ToString();
                cla.ExecuteCommand("UPDATE Tbl_T_ApplicationDetails SET SequenceNo=NULL where ApplicationID="+ ApplicationID + "  UPDATE Tbl_T_ApplicationDetails SET SequenceNo="+x.ToString()+" where ApplicationID=" + ApplicationID + "   ");
                x++;
            }


        }

        public static void PreSequenceNoGen(String UserID, String ApprovalStageID , String VillageID)
        {
            MyClass cla = new MyClass();
            DataTable dt = new DataTable();
            List<String> lst = new List<string>();
            lst.Add(UserID);
            lst.Add(ApprovalStageID);
            lst.Add(VillageID);
            dt = cla.GetDtByProcedure("SP_Get_ApplicationSequenceGenWell", lst);

            int x = 1;
            foreach (DataRow dr in dt.Rows)
            {
                String ApplicationID = dr["ApplicationID"].ToString();
                cla.ExecuteCommand("UPDATE Tbl_T_ApplicationDetails SET SequenceNo=NULL where ApplicationID=" + ApplicationID + "  UPDATE Tbl_T_ApplicationDetails SET SequenceNo=" + x.ToString() + " where ApplicationID=" + ApplicationID + "   ");
                x++;
            }


        }


    }
}
