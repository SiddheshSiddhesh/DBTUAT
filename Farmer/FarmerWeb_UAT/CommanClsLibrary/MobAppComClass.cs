using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace CommanClsLibrary
{
    public class MobAppComClass
    {

        MyClass cla = new MyClass();

        public static string GetUniqueKey()
        {
            int maxSize = 8;
            char[] chars = new char[62];
            string a;
            a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            { result.Append(chars[b % (chars.Length - 1)]); }
            return result.ToString().ToUpper();
        }

        public DataSet ddlBindDesignation(DropDownList dll, int HeadId)
        {
            DataSet dt = new DataSet();
            dt = cla.GetDataSet("SELECT Desig_ID , ISNULL(ParentDesig_ID, 0) ParentDesig_ID , Desig_Name FROM Tbl_M_Designation WHERE (" + HeadId + " = 0 OR Desig_ID = 1)And( IsDeleted is null) ORDER BY ParentDesig_ID, Desig_ID ");
            return dt;

        }
        public DataTable BindDesigHead(int? HeadId)
        {
            return cla.GetDataTable("Select Desig_ID, Desig_Name from Tbl_M_Designation where ParentDesig_ID IS NULL And IsDeleted is null");
        }
        public DataTable BindChildDesigHead(int? HeadId)
        {
            return cla.GetDataTable("Select Desig_ID, Desig_Name from Tbl_M_Designation where ParentDesig_ID=" + HeadId + " And IsDeleted is null");
        }


        public DataTable GetCityAll(String State_ID, String LevelType_ID)
        {
            DataTable dt = new DataTable();
            if (LevelType_ID.Trim().Length == 0)
            {
                dt = cla.GetDataTable("SELECT City_ID, Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + "    order by Cityname");
            }
            else
            {
                if (LevelType_ID.Trim() == "2")
                {
                    dt = cla.GetDataTable("SELECT City_ID, Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + " AND City_ID in (SELECT distinct City_ID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))   order by Cityname");
                }
                else
                {
                    dt = cla.GetDataTable("SELECT City_ID, Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + "    order by Cityname");
                }
            }
            return dt;
        }

        public DataTable GetTalukaMaster(String City_ID)//, String LevelType_ID
        {
            DataTable dt = new DataTable();

            dt = cla.GetDataTable("SELECT DISTINCT T.TalukaID, T.Taluka FROM Tbl_M_VillageMaster AS V INNER JOIN Tbl_M_TalukaMaster AS T ON V.TalukaID = T.TalukaID WHERE  (T.IsDeleted IS NULL) AND (V.City_ID =" + City_ID + ") order by T.Taluka");

            return dt;

        }
        public DataTable GetCityPocra(String State_ID, String LevelType_ID, String Lang)
        {

            //DataTable dt = new DataTable();

            //dt = cla.GetDataTable("SELECT City_ID, Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + "  AND UserInPocra is not null  order by Cityname");

            //return dt;
            DataTable dt = new DataTable();
            if (LevelType_ID.Trim().Length == 0)
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT City_ID, Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + "  AND UserInPocra is not null  order by Cityname");
                else
                    dt = cla.GetDataTable("SELECT City_ID, ISNULL(CitynameMr,Cityname) as Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + "  AND UserInPocra is not null  order by Cityname");
            }
            //else
            //{
            //    if (LevelType_ID.Trim() == "2")
            //    {
            //        if (Lang.Trim() == "en-IN")
            //            dt = cla.GetDataTable("SELECT City_ID, Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + " AND UserInPocra is not null AND City_ID in (SELECT distinct City_ID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))   order by Cityname");
            //        else
            //            dt = cla.GetDataTable("SELECT City_ID, ISNULL(CitynameMr,Cityname) as Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + " AND UserInPocra is not null AND City_ID in (SELECT distinct City_ID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))   order by Cityname");
            //    }
            //    else
            //    {
            //        if (Lang.Trim() == "en-IN")
            //            dt = cla.GetDataTable("SELECT City_ID, Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + "  AND UserInPocra is not null   order by Cityname");
            //        else
            //            dt = cla.GetDataTable("SELECT City_ID, ISNULL(CitynameMr,Cityname) as Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + "  AND UserInPocra is not null   order by Cityname");
            //    }
            //}
            return dt;
        }
        public DataTable GetVillage(String City_ID, String TalukaID)
        {

            // return cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID="+ TalukaID + " order by VillageName");
            DataTable dt = new DataTable();

            dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " order by VillageName");
            //}  SELECT VillageID, VillageName  FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=15 AND TalukaID=123 order by VillageName

            return dt;
        }

        public DataTable GetBankBranchWiseIFSC(String NameOFbank, String BranchName)
        {

            return cla.GetDataTable("SELECT distinct  IFSCCode, RBIBankID  FROM  Tbl_M_RBIBankMaster where IsDeleted is null  and NameOFbank='" + NameOFbank.Trim() + "' and BranchName='" + BranchName + "' ");

        }
        public DataTable SubGetComponent(String ComponentID)
        {
            if (ComponentID != "")
            {
                return cla.GetDataTable("SELECT SubComponentID, SubComponentCode+' '+SubComponentName as SubComponentName FROM Tbl_M_ComponentSub where IsDeleted is null and ComponentID=" + ComponentID + " AND Tbl_M_ComponentSub.SubComponentID in (select distinct SubComponentID from Tbl_M_ActivityBeneficiary where IsDeleted is null) order by SubComponentCode");
            }
            else
            {
                return null;
            }
        }
        public DataTable SubGetComponentMr(String ComponentID)
        {

            return cla.GetDataTable("SELECT SubComponentID, SubComponentCode+' '+SubComponentNameMr as SubComponentName FROM Tbl_M_ComponentSub where IsDeleted is null and ComponentID=" + ComponentID + " AND Tbl_M_ComponentSub.SubComponentID in (select distinct SubComponentID from Tbl_M_ActivityBeneficiary where IsDeleted is null) order by SubComponentCode");

        }
        //public DataTable SubGetComponent()
        //{

        //    return cla.GetDataTable("SELECT SubComponentID, SubComponentCode+' '+SubComponentName as SubComponentName ,SubComponentCode FROM Tbl_M_ComponentSub where IsDeleted is null order by SubComponentCode ");

        //}
        public DataTable GetGPCodeByUser(String UpdateByRegID)
        {
            return cla.GetDataTable("SELECT ApplicationMeetingID As ID ,VCRMCGPCode+' - '+ Convert(nvarchar(20),MeetingnDate,103) as Names FROM Tbl_T_ApplicationVCRMC_Meeting where IsDeleted is null and ApplicationMeetingID in (Select distinct Tbl_T_ApplicationVCRMC_MeetingChild.ApplicationMeetingID from Tbl_T_ApplicationVCRMC_MeetingChild where UpdateByRegID=" + UpdateByRegID + ") order by MeetingnDate desc ");
        }
        public DataTable GetPoCRAGPCode()//UserChildId
        {
            string Lang = "";
            DataTable dt = new DataTable();

            if (Lang.Trim() == "en-IN")
                dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V inner join Tbl_M_City As C on C.City_ID=V.City_ID where V.IsDeleted is null AND C.UserInPocra is not null order by VillageName");
            else
                dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V inner join Tbl_M_City As C on C.City_ID=V.City_ID where V.IsDeleted is null AND C.UserInPocra is not null  order by VillageName");

            return dt;

        }
        public DataTable GetPocraVillageAsPerWorkArea(String Lang, String UserId, String LevelType_ID)
        {

            DataTable dt = new DataTable();
            if (LevelType_ID == null)
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V inner join Tbl_M_City As C on C.City_ID=V.City_ID where V.IsDeleted is null AND C.UserInPocra is not null order by VillageName");
                else
                    dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V inner join Tbl_M_City As C on C.City_ID=V.City_ID where V.IsDeleted is null AND C.UserInPocra is not null  order by VillageName");
            }
            else
            {
                if (LevelType_ID.ToString().Trim() == "5")
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V inner join Tbl_M_City As C on C.City_ID=V.City_ID where V.IsDeleted is null  AND V.VillageID in (SELECT distinct VillageID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + UserId + ")) AND C.UserInPocra is not null order by VillageName");
                    else
                        dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V inner join Tbl_M_City As C on C.City_ID=V.City_ID where V.IsDeleted is null  AND V.VillageID in (SELECT distinct VillageID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + UserId + ")) AND C.UserInPocra is not null order by VillageName");
                }
                else if (LevelType_ID.ToString().Trim() == "8")
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V inner join Tbl_M_City As C on C.City_ID=V.City_ID where V.IsDeleted is null  AND ClustersMasterID in (SELECT distinct ClustersMasterID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + UserId + ")) AND C.UserInPocra is not null order by VillageName");
                    else
                        dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V inner join Tbl_M_City As C on C.City_ID=V.City_ID where V.IsDeleted is null  AND ClustersMasterID in (SELECT distinct ClustersMasterID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + UserId + ")) AND C.UserInPocra is not null order by VillageName");
                }
                else
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V inner join Tbl_M_City As C on C.City_ID=V.City_ID where V.IsDeleted is null  AND C.UserInPocra is not null order by VillageName");
                    else
                        dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster  As V inner join Tbl_M_City As C on C.City_ID=V.City_ID where V.IsDeleted is null AND C.UserInPocra is not null  order by VillageName");
                }
            }
            return dt;
        }
    }
}

