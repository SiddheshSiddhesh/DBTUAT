using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using CommanClsLibrary.Repository.Models;
using static CommanClsLibrary.Repository.Enums;

namespace CommanClsLibrary
{
    public class MyCommanClassAPI
    {


        MyClass cla = new MyClass();

        public static string GetUniqueKey()
        {
            int maxSize = 8;
            // int minSize = 5;
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

        public DataTable GetState(String CountryID, String Lang)
        {
            if (Lang.ToString().Trim() == "en-IN")
            {
                return cla.GetDataTable("SELECT State_ID, StateName FROM  Tbl_M_State where IsDeleted is null  and CountryID=" + CountryID + " order by StateName");
            }
            else
            {
                return cla.GetDataTable("SELECT State_ID, ISNULL(StateNameMr,StateName) as StateName FROM  Tbl_M_State where IsDeleted is null  and CountryID=" + CountryID + " order by StateName");
            }


        }

        //public DataTable GetCity(String State_ID)
        //{

        //    return cla.GetDataTable("SELECT City_ID, Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + " order by Cityname");

        //}

        public DataTable GetCityAll(String State_ID, String LevelType_ID, String Lang)
        {

            DataTable dt = new DataTable();
            if (LevelType_ID.Trim().Length == 0)
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT City_ID, Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + "    order by Cityname");
                else
                    dt = cla.GetDataTable("SELECT City_ID, ISNULL(CitynameMr,Cityname) as Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + "    order by Cityname");
            }
            else
            {
                if (LevelType_ID.Trim() == "2")
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT City_ID, Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + " AND City_ID in (SELECT distinct City_ID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))   order by Cityname");
                    else
                        dt = cla.GetDataTable("SELECT City_ID, ISNULL(CitynameMr,Cityname) as Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + " AND City_ID in (SELECT distinct City_ID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))   order by Cityname");
                }
                else
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT City_ID, Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + "    order by Cityname");
                    else
                        dt = cla.GetDataTable("SELECT City_ID, ISNULL(CitynameMr,Cityname) as Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + "    order by Cityname");
                }
            }
            return dt;

        }
        public DataTable GetCityPocra(String State_ID, String LevelType_ID, String Lang)
        {

            DataTable dt = new DataTable();
            if (LevelType_ID.Trim().Length == 0)
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT City_ID, Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + "  AND UserInPocra is not null  order by Cityname");
                else
                    dt = cla.GetDataTable("SELECT City_ID, ISNULL(CitynameMr,Cityname) as Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + "  AND UserInPocra is not null  order by Cityname");
            }
            else
            {
                if (LevelType_ID.Trim() == "2")
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT City_ID, Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + " AND UserInPocra is not null AND City_ID in (SELECT distinct City_ID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))   order by Cityname");
                    else
                        dt = cla.GetDataTable("SELECT City_ID, ISNULL(CitynameMr,Cityname) as Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + " AND UserInPocra is not null AND City_ID in (SELECT distinct City_ID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))   order by Cityname");
                }
                else
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT City_ID, Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + "  AND UserInPocra is not null   order by Cityname");
                    else
                        dt = cla.GetDataTable("SELECT City_ID, ISNULL(CitynameMr,Cityname) as Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + "  AND UserInPocra is not null   order by Cityname");
                }
            }
            return dt;
        }




        public DataTable GetCityAll(String State_ID, String Lang)
        {


            DataTable dt = new DataTable();
            if (System.Web.HttpContext.Current.Session["LevelType_ID"] == null)
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT City_ID, Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + "    order by Cityname");
                else
                    dt = cla.GetDataTable("SELECT City_ID, ISNULL(CitynameMr,Cityname) as Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + "    order by Cityname");
            }
            else
            {
                if (System.Web.HttpContext.Current.Session["LevelType_ID"].ToString().Trim() == "2")
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT City_ID, Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + " AND City_ID in (SELECT distinct City_ID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))   order by Cityname");
                    else
                        dt = cla.GetDataTable("SELECT City_ID, ISNULL(CitynameMr,Cityname) as Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + " AND City_ID in (SELECT distinct City_ID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))   order by Cityname");
                }
                else
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT City_ID, Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + "    order by Cityname");
                    else
                        dt = cla.GetDataTable("SELECT City_ID, ISNULL(CitynameMr,Cityname) as Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + "    order by Cityname");
                }
            }
            return dt;

        }
        public DataTable GetCityPocra(String State_ID, String Lang)
        {

            DataTable dt = new DataTable();
            if (System.Web.HttpContext.Current.Session["LevelType_ID"] == null)
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT City_ID, Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + "  AND UserInPocra is not null  order by Cityname");
                else
                    dt = cla.GetDataTable("SELECT City_ID, ISNULL(CitynameMr,Cityname) as Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + "  AND UserInPocra is not null  order by Cityname");

            }
            else
            {
                if (System.Web.HttpContext.Current.Session["LevelType_ID"].ToString().Trim() == "2")
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT City_ID, Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + " AND UserInPocra is not null AND City_ID in (SELECT distinct City_ID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))   order by Cityname");
                    else
                        dt = cla.GetDataTable("SELECT City_ID, ISNULL(CitynameMr,Cityname) as Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + " AND UserInPocra is not null AND City_ID in (SELECT distinct City_ID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))   order by Cityname");
                }
                else
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT City_ID, Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + "  AND UserInPocra is not null   order by Cityname");
                    else
                        dt = cla.GetDataTable("SELECT City_ID, ISNULL(CitynameMr,Cityname) as Cityname FROM  Tbl_M_City where IsDeleted is null  and State_ID=" + State_ID + "  AND UserInPocra is not null   order by Cityname");
                }
            }
            return dt;
        }


        //public DataTable GetVillage(String City_ID, String Lang)
        //{

        //String LevelType_ID = "";
        //    DataTable dt = new DataTable();

        //if ( LevelType_ID  == null)
        //{
        //        if (Lang.Trim() == "en-IN")
        //            dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " order by VillageName");
        //        else
        //            dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " order by VillageName");
        //    }
        //    else
        //    {

        //    if ( LevelType_ID.ToString().Trim() == "5")
        //    {
        //            if (Lang.Trim() == "en-IN")
        //                dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND VillageID in (SELECT distinct VillageID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
        //            else
        //                dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND VillageID in (SELECT distinct VillageID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
        //        }

        //    else if ( LevelType_ID.ToString().Trim() == "8")
        //    {
        //            if (Lang.Trim() == "en-IN")
        //                dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND ClustersMasterID in (SELECT distinct ClustersMasterID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
        //            else
        //                dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND ClustersMasterID in (SELECT distinct ClustersMasterID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
        //        }
        //        else
        //        {
        //            if (Lang.Trim() == "en-IN")
        //                dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " order by VillageName");
        //            else
        //                dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " order by VillageName");
        //        }
        //    }
        //    return dt;
        //}
        public DataTable GetVillage(String City_ID, String TalukaID, String Lang)
        {



            String UserInPocra = cla.GetExecuteScalar("Select City_ID from Tbl_M_City where City_ID=" + City_ID + " and UserInPocra is not null").Trim();


            DataTable dt = new DataTable();
            if (UserInPocra.Length == 0)
            {

                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " order by VillageName");
                else
                    dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " order by VillageName");

                //if (LevelType_ID == null)
                //{
                //    if (Lang.Trim() == "en-IN")
                //        dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " order by VillageName");
                //    else
                //        dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " order by VillageName");
                //}
                //else
                //{

                //    if (LevelType_ID.ToString().Trim() == "5")
                //    {
                //        if (Lang.Trim() == "en-IN")
                //            dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " AND VillageID in (SELECT distinct VillageID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                //        else
                //            dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " AND VillageID in (SELECT distinct VillageID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                //    }

                //    else if (LevelType_ID.ToString().Trim() == "8")
                //    {
                //        if (Lang.Trim() == "en-IN")
                //            dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " AND ClustersMasterID in (SELECT distinct ClustersMasterID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                //        else
                //            dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " AND ClustersMasterID in (SELECT distinct ClustersMasterID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                //    }
                //    else
                //    {
                //        if (Lang.Trim() == "en-IN")
                //            dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " order by VillageName");
                //        else
                //            dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " order by VillageName");
                //    }
                //}
            }
            else
            {

                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null AND UserInPocra is not null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " order by VillageName");
                else
                    dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null AND UserInPocra is not null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " order by VillageName");

            }
            return dt;
        }



        public DataTable GetTalukaMaster(String City_ID, String Lang)
        {

            String LevelType_ID = "";
            DataTable dt = new DataTable();

            if (LevelType_ID == null)
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT DISTINCT T.TalukaID, T.Taluka FROM Tbl_M_VillageMaster AS V INNER JOIN Tbl_M_TalukaMaster AS T ON V.TalukaID = T.TalukaID WHERE  (T.IsDeleted IS NULL) AND (V.City_ID =" + City_ID + ") order by T.Taluka");
                else
                    dt = cla.GetDataTable("SELECT DISTINCT T.TalukaID, ISNULL(T.TalukaMr,T.Taluka) as Taluka  FROM Tbl_M_VillageMaster AS V INNER JOIN Tbl_M_TalukaMaster AS T ON V.TalukaID = T.TalukaID WHERE  (T.IsDeleted IS NULL) AND (V.City_ID =" + City_ID + ") order by Taluka");
            }
            else
            {
                if (LevelType_ID.ToString().Trim() == "3")
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT DISTINCT T.TalukaID, T.Taluka FROM Tbl_M_VillageMaster AS V INNER JOIN Tbl_M_TalukaMaster AS T ON V.TalukaID = T.TalukaID WHERE  (T.IsDeleted IS NULL) AND (V.City_ID =" + City_ID + ") AND T.TalukaID  in (SELECT distinct TalukaID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))   order by T.Taluka");
                    else
                        dt = cla.GetDataTable("SELECT DISTINCT T.TalukaID, ISNULL(T.TalukaMr,T.Taluka) as Taluka FROM Tbl_M_VillageMaster AS V INNER JOIN Tbl_M_TalukaMaster AS T ON V.TalukaID = T.TalukaID WHERE  (T.IsDeleted IS NULL) AND (V.City_ID =" + City_ID + ") AND T.TalukaID  in (SELECT distinct TalukaID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))   order by Taluka");
                }
                else
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT DISTINCT T.TalukaID, T.Taluka FROM Tbl_M_VillageMaster AS V INNER JOIN Tbl_M_TalukaMaster AS T ON V.TalukaID = T.TalukaID WHERE  (T.IsDeleted IS NULL) AND (V.City_ID =" + City_ID + ") order by T.Taluka");
                    else
                        dt = cla.GetDataTable("SELECT DISTINCT T.TalukaID, ISNULL(T.TalukaMr,T.Taluka) as Taluka FROM Tbl_M_VillageMaster AS V INNER JOIN Tbl_M_TalukaMaster AS T ON V.TalukaID = T.TalukaID WHERE  (T.IsDeleted IS NULL) AND (V.City_ID =" + City_ID + ") order by Taluka");
                }
            }
            return dt;

        }
        public DataTable GetPostMaster(String City_ID, String Lang, String TalukaID)
        {

            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT DISTINCT T.Post_ID, T.PostName FROM Tbl_M_CityWisePost as T  WHERE  (T.IsDeleted IS NULL) AND (T.TalukaID =" + TalukaID + ") and City_ID=" + City_ID + "  order by T.PostName");
            else
                return cla.GetDataTable("SELECT DISTINCT T.Post_ID, isnull(T.PostNameMr,T.PostName) as PostName FROM Tbl_M_CityWisePost as T  WHERE  (T.IsDeleted IS NULL) AND (T.TalukaID =" + TalukaID + ") and City_ID=" + City_ID + " order by PostName");

        }
        public DataTable GetPostTalukaWiseMaster(String TalukaID, String Lang)
        {

            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT DISTINCT T.Post_ID, T.PostName FROM Tbl_M_CityWisePost as T  WHERE  (T.IsDeleted IS NULL) AND (T.TalukaID =" + TalukaID + ") order by T.PostName");
            else
                return cla.GetDataTable("SELECT DISTINCT T.Post_ID, isnull(T.PostNameMr,T.PostName) as PostName FROM Tbl_M_CityWisePost as T  WHERE  (T.IsDeleted IS NULL) AND (T.TalukaID =" + TalukaID + ") order by PostName");

        }
        public DataTable GetBeneficiaryType(String Lang)
        {

            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT BeneficiaryTypesID, BeneficiaryTypes FROM  Tbl_M_BeneficiaryTypes where IsDeleted is null  order by BeneficiaryTypesID");
            else
                return cla.GetDataTable("SELECT BeneficiaryTypesID, isnull(BeneficiaryTypesMr,BeneficiaryTypes) as BeneficiaryTypes FROM  Tbl_M_BeneficiaryTypes where IsDeleted is null  order by BeneficiaryTypesID");

        }

        public DataTable GetApprovalStages(String Lang)
        {

            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT ApprovalStageID, ApprovalStages FROM Tbl_M_ApprovalStages WHERE (IsDeleted IS NULL) and ApprovalStageID not in (1) ORDER BY OrderNo");
            else
                return cla.GetDataTable("SELECT ApprovalStageID, ISNULL(ApplicationStatusMr,ApprovalStages) as ApprovalStages FROM Tbl_M_ApprovalStages WHERE (IsDeleted IS NULL) and ApprovalStageID not in (1) ORDER BY OrderNo");
        }
        public DataTable GetCroppingSeason()
        {
            return cla.GetDataTable("SELECT CroppingSeasonID, CroppingSeason FROM Tbl_M_CroppingSeason WHERE (IsDeleted IS NULL)  ORDER BY CroppingSeason");
        }
        public DataTable GetCostCategoryMaster(String Lang)
        {

            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT CategoryMasterID, CategoryMaster FROM  Tbl_M_CategoryMaster where IsDeleted is null  order by CategoryMaster");
            else
                return cla.GetDataTable("SELECT CategoryMasterID, ISNULL(CategoryMasterMr,CategoryMaster) as CategoryMaster FROM  Tbl_M_CategoryMaster where IsDeleted is null  order by CategoryMaster");

        }
        public DataTable GetGenderMaster(String Lang)
        {

            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT GenderName as ID, GenderName as Text FROM  Tbl_M_Gender where IsDeleted is null  order by GenderID");
            else
                return cla.GetDataTable("SELECT GenderName as ID, GenderNameMr as Text  FROM  Tbl_M_Gender where IsDeleted is null  order by GenderID");

        }
        public DataTable GetUnit()
        {

            return cla.GetDataTable("SELECT UnitMasterID, UnitofMes FROM  Tbl_M_UnitMaster where IsDeleted is null  order by UnitofMes");

        }
        public DataTable GetBank(String Lang)
        {

            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT distinct NameOFbank FROM  Tbl_M_RBIBankMaster where IsDeleted is null  order by NameOFbank");
            else
                return cla.GetDataTable("SELECT distinct ISNULL(NameOFbankMr,NameOFbank) as NameOFbank FROM  Tbl_M_RBIBankMaster where IsDeleted is null  order by NameOFbank");

        }
        public DataTable GetBankWiseBrach(String NameOFbank, String Lang)
        {

            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT distinct  BranchName  FROM  Tbl_M_RBIBankMaster where IsDeleted is null  and NameOFbank='" + NameOFbank.Trim() + "' order by BranchName");
            else
                return cla.GetDataTable("SELECT distinct  BranchName  FROM  Tbl_M_RBIBankMaster where IsDeleted is null  and NameOFbank='" + NameOFbank.Trim() + "' order by BranchName");

        }
        public DataTable GetBankBranchWiseIFSC(String NameOFbank, String BranchName)
        {

            return cla.GetDataTable("SELECT distinct  IFSCCode  FROM  Tbl_M_RBIBankMaster where IsDeleted is null  and NameOFbank='" + NameOFbank.Trim() + "' and BranchName='" + BranchName + "' ");

        }

        public DataTable GetBankBranchWiseMICR(String NameOFbank, String BranchName)
        {

            return cla.GetDataTable("SELECT distinct  MICRCode  FROM  Tbl_M_RBIBankMaster where IsDeleted is null  and NameOFbank='" + NameOFbank.Trim() + "' and BranchName='" + BranchName + "' ");

        }
        public DataTable GetComponent(String Lang)
        {

            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT distinct ComponentID , ComponentName+' - '+ISNULL(ComponentCode,'')  as ComponentName ,ComponentCode  FROM  Tbl_M_Components where IsDeleted is null  order by ComponentCode");
            else
                return cla.GetDataTable("SELECT distinct ComponentID , ISNULL(ComponentNameMr,ComponentName)+' - '+ISNULL(ComponentCode,'')  as ComponentName ,ComponentCode  FROM  Tbl_M_Components where IsDeleted is null  order by ComponentCode");

        }
        public DataTable GetComponent(String BeneficiaryTypesID, String Lang)
        {

            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT distinct ComponentID , ComponentName+' - '+ISNULL(ComponentCode,'')  as ComponentName ,ComponentCode  FROM  Tbl_M_Components where IsDeleted is null AND Tbl_M_Components.ComponentID in (select distinct ComponentID from Tbl_M_ActivityBeneficiary where  BeneficiaryTypesID=" + BeneficiaryTypesID + ") order by ComponentCode");
            else
                return cla.GetDataTable("SELECT distinct ComponentID , ISNULL(ComponentNameMr,ComponentName)+' - '+ISNULL(ComponentCode,'')  as ComponentName ,ComponentCode  FROM  Tbl_M_Components where IsDeleted is null AND Tbl_M_Components.ComponentID in (select distinct ComponentID from Tbl_M_ActivityBeneficiary where  BeneficiaryTypesID=" + BeneficiaryTypesID + ") order by ComponentCode");

        }
        public DataTable GetPreSenLetterType()
        {

            return cla.GetDataTable("SELECT PreSenLetterTypeID,PreSenLetterType FROM Tbl_M_PreSenLetterType where IsDeleted is null ");
        }

        public DataTable GetInsLetterType()
        {
            return cla.GetDataTable("SELECT InpectionDocTypeID,InpectionDocType FROM Tbl_M_InpectionDocType where IsDeleted is null ");
        }

        public DataTable GetApplicationStatus(String StatusFor, String Lang)
        {



            DataTable dt = new DataTable();
            if (StatusFor.Trim() == "R")
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UsedInVerification is not null  order by ApplicationStatus");
                else
                    dt = cla.GetDataTable("SELECT  ISNULL(ApplicationStatusMr,ApplicationStatus) as ApplicationStatus ,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UsedInVerification is not null  order by ApplicationStatus");
            }
            if (StatusFor.Trim() == "A")
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UserInApproval is not null  order by ApplicationStatus");
                else
                    dt = cla.GetDataTable("SELECT  ISNULL(ApplicationStatusMr,ApplicationStatus) as ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UserInApproval is not null  order by ApplicationStatus");
            }
            if (StatusFor.Trim() == "P")
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UserInPayment is not null  order by ApplicationStatus");
                else
                    dt = cla.GetDataTable("SELECT  ISNULL(ApplicationStatusMr,ApplicationStatus) as ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UserInPayment is not null  order by ApplicationStatus");
            }
            if (StatusFor.Trim() == "AA")
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND ApplicationStatusID in (5,6,2,1,8,300)  order by ApplicationStatus");
                else
                    dt = cla.GetDataTable("SELECT  ISNULL(ApplicationStatusMr,ApplicationStatus) as ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND ApplicationStatusID in (5,6,2,1,8,300)  order by ApplicationStatus");
            }
            return dt;

        }


        public DataTable GetApplicationStatusListForDesk4(String StatusFor, String Lang, string ApplicationID)
        {



            DataTable dt = new DataTable();
            if (StatusFor.Trim() == "R")
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UsedInVerification is not null  order by ApplicationStatus");
                else
                    dt = cla.GetDataTable("SELECT  ISNULL(ApplicationStatusMr,ApplicationStatus) as ApplicationStatus ,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UsedInVerification is not null  order by ApplicationStatus");
            }
            if (StatusFor.Trim() == "A")
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UserInApproval is not null  order by ApplicationStatus");
                else
                    dt = cla.GetDataTable("SELECT  ISNULL(ApplicationStatusMr,ApplicationStatus) as ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UserInApproval is not null  order by ApplicationStatus");
            }
            if (StatusFor.Trim() == "P")
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UserInPayment is not null  order by ApplicationStatus");
                else
                    dt = cla.GetDataTable("SELECT  ISNULL(ApplicationStatusMr,ApplicationStatus) as ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UserInPayment is not null  order by ApplicationStatus");
            }
            if (StatusFor.Trim() == "AA")
            {
                MyCommanClass Comcls = new MyCommanClass();
                DataTable dtApp = Comcls.GetFarmerRepeatRequestDetails(ApplicationID);

                if (dtApp.Rows.Count > 0)
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND ApplicationStatusID in (2)  order by ApplicationStatus");
                    else
                        dt = cla.GetDataTable("SELECT  ISNULL(ApplicationStatusMr,ApplicationStatus) as ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND ApplicationStatusID in (2)  order by ApplicationStatus");
                }
                else
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND ApplicationStatusID in (5,6,2,1,8,300)  order by ApplicationStatus");
                    else
                        dt = cla.GetDataTable("SELECT  ISNULL(ApplicationStatusMr,ApplicationStatus) as ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND ApplicationStatusID in (5,6,2,1,8,300)  order by ApplicationStatus");
                }
            }
            return dt;

        }


        //public DataTable GetApplicationStatus2()
        //{

        //    return cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null and ApplicationStatusID<>5 order by ApplicationStatus");

        //}
        public DataTable GetActivityCategory(String Lang)
        {

            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT ActivityCategory, ActivityCategoryID  FROM  Tbl_M_ActivityCategory where IsDeleted is null  order by ActivityCategory");
            else
                return cla.GetDataTable("SELECT ISNULL(ActivityCategoryMr,ActivityCategory)  as ActivityCategory, ActivityCategoryID  FROM  Tbl_M_ActivityCategory where IsDeleted is null  order by ActivityCategory");

        }
        public DataTable SubGetComponent(String ComponentID, String Lang)
        {

            if (Lang.Trim() == "en-IN")
            {
                if (ComponentID != "")
                {
                    return cla.GetDataTable("SELECT SubComponentID, SubComponentCode+' '+SubComponentName as SubComponentName FROM Tbl_M_ComponentSub where IsDeleted is null and ComponentID=" + ComponentID + " order by SubComponentCode ");
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (ComponentID != "")
                {
                    return cla.GetDataTable("SELECT SubComponentID, SubComponentCode+' '+ISNULL(SubComponentNameMr,SubComponentName) as SubComponentName FROM Tbl_M_ComponentSub where IsDeleted is null and ComponentID=" + ComponentID + " order by SubComponentCode ");
                }
                else
                {
                    return null;
                }
            }
        }
        public DataTable SubGetComponent(String ComponentID, String BeneficiaryTypesID, String Lang)
        {

            if (Lang.Trim() == "en-IN")
            {
                if (ComponentID != "")
                {
                    return cla.GetDataTable("SELECT SubComponentID, SubComponentCode+' '+SubComponentName as SubComponentName FROM Tbl_M_ComponentSub where IsDeleted is null and ComponentID=" + ComponentID + " AND Tbl_M_ComponentSub.SubComponentID in (select distinct SubComponentID from Tbl_M_ActivityBeneficiary where  BeneficiaryTypesID=" + BeneficiaryTypesID + ") order by SubComponentCode");
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (ComponentID != "")
                {
                    return cla.GetDataTable("SELECT SubComponentID, SubComponentCode+' '+ISNULL(SubComponentNameMr,SubComponentName) as SubComponentName FROM Tbl_M_ComponentSub where IsDeleted is null and ComponentID=" + ComponentID + " AND Tbl_M_ComponentSub.SubComponentID in (select distinct SubComponentID from Tbl_M_ActivityBeneficiary where  BeneficiaryTypesID=" + BeneficiaryTypesID + ") order by SubComponentCode");
                }
                else
                {
                    return null;
                }
            }
        }
        public DataTable SubGetComponent(String Lang)
        {

            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT SubComponentID, SubComponentCode+' '+SubComponentName as SubComponentName ,SubComponentCode FROM Tbl_M_ComponentSub where IsDeleted is null order by SubComponentCode ");
            else
                return cla.GetDataTable("SELECT SubComponentID, SubComponentCode+' '+ISNULL(SubComponentNameMr,SubComponentName) as SubComponentName ,SubComponentCode FROM Tbl_M_ComponentSub where IsDeleted is null order by SubComponentCode ");

        }

        public DataTable GetEligibilityCriteria(String Lang)
        {

            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT EligibilityID , EligibilityCriteria  FROM  Tbl_M_EligibilityCriteria where IsDeleted is null  order by EligibilityCriteria");
            else
                return cla.GetDataTable("SELECT EligibilityID , ISNULL(EligibilityCriteriaMr,EligibilityCriteria) as EligibilityCriteria  FROM  Tbl_M_EligibilityCriteria where IsDeleted is null  order by EligibilityCriteria");

        }
        public DataTable GetRequiredDoc(String Lang)
        {

            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT RequiredDocID , RequiredDoc  FROM  Tbl_M_DocumentRequired where IsDeleted is null  order by RequiredDoc");
            else
                return cla.GetDataTable("SELECT RequiredDocID , ISNULL(RequiredDocMr,RequiredDoc) as RequiredDoc  FROM  Tbl_M_DocumentRequired where IsDeleted is null  order by RequiredDoc");

        }

        public DataTable GetFeasibility(String Lang)
        {

            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT FeasibilityRptID , FeasibilityRpt  FROM  Tbl_M_FeasibilityRpt where IsDeleted is null  order by FeasibilityRpt");
            else
                return cla.GetDataTable("SELECT FeasibilityRptID , ISNULL(FeasibilityRptMr,FeasibilityRpt) as FeasibilityRpt  FROM  Tbl_M_FeasibilityRpt where IsDeleted is null  order by FeasibilityRpt");

        }
        public DataTable GetFeasibility(String ActivityID, String Lang)
        {
            if (ActivityID != "")
            {
                if (Lang.Trim() == "en-IN")
                    return cla.GetDataTable("SELECT A.FeasibilityRptID, A.FeasibilityRpt+'~'+M.MandatoryOptional as FeasibilityRpt FROM Tbl_M_FeasibilityRpt AS A INNER JOIN Tbl_M_ActivityFeasibility AS B ON A.FeasibilityRptID = B.FeasibilityRptID INNER JOIN Tbl_M_MandatoryOptional AS M ON A.MandatoryOptionalID = M.MandatoryOptionalID WHERE (A.IsDeleted IS NULL) AND (B.IsDeleted IS NULL) AND (B.ActivityID = " + ActivityID.Trim() + ") AND A.FeasibilityRptTypeID=1 ORDER BY A.FeasibilityRpt");
                else
                    return cla.GetDataTable("SELECT A.FeasibilityRptID, ISNULL(A.FeasibilityRptMr,A.FeasibilityRpt)+'~'+M.MandatoryOptional as FeasibilityRpt FROM Tbl_M_FeasibilityRpt AS A INNER JOIN Tbl_M_ActivityFeasibility AS B ON A.FeasibilityRptID = B.FeasibilityRptID INNER JOIN Tbl_M_MandatoryOptional AS M ON A.MandatoryOptionalID = M.MandatoryOptionalID WHERE (A.IsDeleted IS NULL) AND (B.IsDeleted IS NULL) AND (B.ActivityID = " + ActivityID.Trim() + ") AND A.FeasibilityRptTypeID=1 ORDER BY A.FeasibilityRpt");
            }
            else
            {
                return null;
            }
        }
        public DataTable GetPaymentFeasibility(String ActivityID, String Lang)
        {

            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT A.FeasibilityRptID, A.FeasibilityRpt+'~'+M.MandatoryOptional as FeasibilityRpt FROM Tbl_M_FeasibilityRpt AS A INNER JOIN Tbl_M_ActivityFeasibility AS B ON A.FeasibilityRptID = B.FeasibilityRptID INNER JOIN Tbl_M_MandatoryOptional AS M ON A.MandatoryOptionalID = M.MandatoryOptionalID WHERE (A.IsDeleted IS NULL) AND (B.IsDeleted IS NULL) AND (B.ActivityID = " + ActivityID.Trim() + ") AND A.FeasibilityRptTypeID=2 ORDER BY A.FeasibilityRpt");
            else
                return cla.GetDataTable("SELECT A.FeasibilityRptID, ISNULL(A.FeasibilityRptMr,A.FeasibilityRpt)+'~'+M.MandatoryOptional as FeasibilityRpt FROM Tbl_M_FeasibilityRpt AS A INNER JOIN Tbl_M_ActivityFeasibility AS B ON A.FeasibilityRptID = B.FeasibilityRptID INNER JOIN Tbl_M_MandatoryOptional AS M ON A.MandatoryOptionalID = M.MandatoryOptionalID WHERE (A.IsDeleted IS NULL) AND (B.IsDeleted IS NULL) AND (B.ActivityID = " + ActivityID.Trim() + ") AND A.FeasibilityRptTypeID=2 ORDER BY A.FeasibilityRpt");

        }
        public DataTable GetCrop()
        {

            return cla.GetDataTable("SELECT CropID as ID,Crop as Name FROM Tbl_M_Crop where IsDeleted is null order by Crop");

        }

        public DataTable GetForm8A(String RegistrationID, String Lang)
        {

            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT C.Cityname +' - '+ T.Taluka+' - '+ V.VillageName +' - 8A AC. No. '+ L.AccountNumber8A as Names, L.LandID FROM  Tbl_M_RegistrationLand AS L INNER JOIN  Tbl_M_City AS C ON L.City_ID = C.City_ID INNER JOIN Tbl_M_TalukaMaster AS T ON L.TalukaID = T.TalukaID INNER JOIN Tbl_M_VillageMaster AS V ON L.VillageID = V.VillageID WHERE  (L.RegistrationID = " + RegistrationID + ") AND (L.ParentLandID IS NULL) AND (L.IsDeleted IS NULL) ");
            else
                return cla.GetDataTable("SELECT ISNULL(C.CitynameMr,C.Cityname) +' - '+ ISNULL(T.TalukaMr,T.Taluka)+' - '+ ISNULL(V.VillageNameMr,V.VillageName) +' - 8A AC. No. '+ L.AccountNumber8A as Names, L.LandID FROM  Tbl_M_RegistrationLand AS L INNER JOIN  Tbl_M_City AS C ON L.City_ID = C.City_ID INNER JOIN Tbl_M_TalukaMaster AS T ON L.TalukaID = T.TalukaID INNER JOIN Tbl_M_VillageMaster AS V ON L.VillageID = V.VillageID WHERE  (L.RegistrationID = " + RegistrationID + ") AND (L.ParentLandID IS NULL) AND (L.IsDeleted IS NULL) ");

        }

        public DataTable GetForm712Details(String RegistrationID, String LandID)
        {

            return cla.GetDataTable("SELECT  LandID, 'Survey No.'+SurveyNo712+' -- '+Convert(nvarchar(20),Hectare712)+' Hectare  .  '+ Convert(nvarchar(20),Are712)+' Are ' as Names FROM Tbl_M_RegistrationLand AS L WHERE  (RegistrationID =" + RegistrationID + ") AND (ParentLandID = " + LandID + ") AND (IsDeleted IS NULL)");

        }
        public DataTable GetLevelType()
        {

            return cla.GetDataTable("SELECT LevelTypeID, LevelType FROM  Tbl_M_LevelType where IsDeleted is null  order by OrderByNo");

        }
        public DataTable GetUserRole()
        {

            return cla.GetDataTable("SELECT UserRoleID, UserRole FROM  Tbl_M_UserRole where IsDeleted is null  order by UserRoleID");

        }
        public DataTable GetHierarchy()
        {

            return cla.GetDataTable("SELECT D.Desig_ID, D.Desig_Name +' _ '+ L.LevelType+' Level ' as Desig_Name FROM  Tbl_M_Designation AS D INNER JOIN Tbl_M_LevelType AS L ON D.LevelType_ID = L.LevelTypeID WHERE (D.IsDeleted IS NULL) ORDER BY D.Desig_Name");

        }
        public DataTable GetRegisteredUnder(String Lang)
        {

            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT RegisterUnderID as ID ,RegisterUnder As Test FROM Tbl_M_RegisterUnder where IsDeleted is null order by RegisterUnder");
            else
                return cla.GetDataTable("SELECT RegisterUnderID as ID , ISNULL(RegisterUnderMr,RegisterUnder) As Test FROM Tbl_M_RegisterUnder where IsDeleted is null order by RegisterUnder");

        }
        public DataTable GetRegisteredThrough(String Lang)
        {

            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT RegisteredThroughID as ID ,RegisteredThrough As Test  FROM Tbl_M_RegisteredThrough where IsDeleted is null order by RegisteredThrough");
            else
                return cla.GetDataTable("SELECT RegisteredThroughID as ID ,ISNULL(RegisteredThroughMr,RegisteredThrough) As Test  FROM Tbl_M_RegisteredThrough where IsDeleted is null order by RegisteredThrough");

        }
        public DataTable GetCircle(String Lang)
        {

            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT CircleID As ID ,CircleName as Names FROM Tbl_M_Circle where IsDeleted is null order by CircleName ");
            else
                return cla.GetDataTable("SELECT CircleID As ID ,ISNULL(CircleNameMr,CircleName) as Names FROM Tbl_M_Circle where IsDeleted is null order by CircleName ");
        }

        public DataTable GetReasonVerifecation(String ApprovalStageID, String ApplicationStatusID, String Lang)
        {

            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT ReasonID ,Reasons ,ReasonsMr ,ReasonsCode FROM Tbl_M_Reason where IsDeleted IS NULL AND ApprovalStageID=" + ApprovalStageID + " and ApplicationStatusID=" + ApplicationStatusID + " ");
            else
                return cla.GetDataTable("SELECT ReasonID ,ISNULL(ReasonsMr,Reasons) as Reasons ,ReasonsMr ,ReasonsCode FROM Tbl_M_Reason where IsDeleted IS NULL AND ApprovalStageID=" + ApprovalStageID + " and ApplicationStatusID=" + ApplicationStatusID + " ");
        }
        public DataTable GetImageTitle(String Lang)
        {

            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT ImageTitleID, ImageTitleName  FROM Tbl_M_ImageTitle where IsDeleted is null order by ImageTitleName desc");
            else
                return cla.GetDataTable("SELECT ImageTitleID, ISNULL(ImageTitleNameMr,ImageTitleName) as ImageTitleName  FROM Tbl_M_ImageTitle where IsDeleted is null order by ImageTitleName desc");

        }
        public DataTable GetPaymentTerm(String Lang)
        {

            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT PaymentTermID As ID ,PaymentTermName as Names FROM Tbl_M_PaymentTerm where IsDeleted is null order by PaymentTermName ");
            else
                return cla.GetDataTable("SELECT PaymentTermID As ID ,ISNULL(PaymentTermNameMr,PaymentTermName) as Names FROM Tbl_M_PaymentTerm where IsDeleted is null order by PaymentTermName ");
        }
        public DataTable GetApplicationMeeting(String ApplicationID)
        {
            return cla.GetDataTable("SELECT ApplicationMeetingID As ID ,VCRMCGPCode+' - '+ Convert(nvarchar(20),MeetingnDate,103) as Names FROM Tbl_T_ApplicationVCRMC_Meeting where IsDeleted is null AND (FileMinuteOfmeeting is not null or FileMinuteOfmeeting<>'') and  ApplicationMeetingID in (Select distinct Tbl_T_ApplicationVCRMC_MeetingChild.ApplicationMeetingID from Tbl_T_ApplicationVCRMC_MeetingChild where ApplicationID=" + ApplicationID + ") order by MeetingnDate desc ");
        }
        public DataTable GetPageWiseUseList(String RegistrationID, String Form_ID)
        {
            List<String> lst = new List<string>();
            lst.Add(RegistrationID);
            lst.Add(Form_ID);
            return cla.GetDtByProcedure("SP_GetPageWiseUseList", lst);
        }

        public DataTable GetPriorityLevelType()
        {
            return cla.GetDataTable("SELECT PriorityLevelID,PriorityLevelName FROM Tbl_M_PriorityLevel where IsDeleted is null ");
        }
        public DataTable GetPriorityLevelType(String PriorityLevelID)
        {
            return cla.GetDataTable("SELECT PriorityLevelID,PriorityLevelName FROM Tbl_M_PriorityLevel where IsDeleted is null and PriorityLevelID=" + PriorityLevelID + " ");
        }
        public DataTable GetOrgonization()
        {
            return cla.GetDataTable("SELECT APICOdeID,OrgName FROM Tbl_M_APIAUTHCODE where IsDeleted is null ");
        }
        public DataTable GetAPIDetails()
        {
            return cla.GetDataTable("SELECT APIID, APIName FROM Tbl_M_APIS where IsDeleted is null ");
        }
        #region RecordExistanceCheck
        public bool RecordExistanceChk(string TableName, string ChkFieldName, string ChkFieldValue, string PrimaryFieldName)
        {
            string Query = "SELECT  " + PrimaryFieldName + "  FROM  " + TableName + "  WHERE IsDeleted is null AND (" + ChkFieldName + " = '" + ChkFieldValue + "') AND (" + PrimaryFieldName + " IS NOT NULL)";
            string AllParams = " string TableName=" + TableName + ", string ChkFieldName=" + ChkFieldName + ", string ChkFieldValue=" + ChkFieldValue + ", string PrimaryFieldName=" + PrimaryFieldName;
            bool res = true;
            try
            {
                if (cla.GetExecuteScalar(Query).Length > 0)
                {
                    res = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "MyCommonClassAPI:RecordExistanceChk";
                err.ProjectName = "POCRA Web Library";
                err.ErrorDescription = "Query : " + Query + " , AllParams : " + AllParams + " , Exception : " + ex.Message;
                err.ErrorSeverity = (int)ErrorSeverity.High;
                new Repository.Classes.ErrorLogManager().InsertErrorLog(err);
            }
            return res;
        }
        public bool RecordExistanceChk(string TableName, string ChkFieldName, string ChkFieldValue, string PrimaryFieldName, int PrimaryFieldValue)
        {
            bool res = true;
            if (cla.GetExecuteScalar("SELECT  " + PrimaryFieldName + "  FROM  " + TableName + "  WHERE IsDeleted is null AND (" + ChkFieldName + " = '" + ChkFieldValue + "') AND (" + PrimaryFieldName + "<>" + PrimaryFieldValue + ")").Length > 0)
            {
                res = false;
            }

            return res;
        }
        #endregion


        /// <summary>
        /// To demonstrate extraction of file extension from base64 string.
        /// </summary>
        /// <param name="base64String">base64 string.</param>
        /// <returns>Henceforth file extension from string.</returns>
        public static string GetFileExtension(string base64String)
        {
            var data = base64String.Substring(0, 5);

            switch (data.ToUpper())
            {
                case "IVBOR":
                    return ".png";
                case "/9J/4":
                    return ".jpg";
                case "AAAAF":
                    return ".mp4";
                case "JVBER":
                    return ".pdf";
                case "AAABA":
                    return ".ico";
                case "UMFYI":
                    return ".rar";
                case "E1XYD":
                    return ".rtf";
                case "U1PKC":
                    return ".txt";
                case "MQOWM":
                case "77U/M":
                    return ".srt";
                default:
                    return string.Empty;
            }
        }

        public static string DataTableToJsonObj(DataTable dt)
        {
            DataSet ds = new DataSet();
            ds.Merge(dt);
            StringBuilder JsonString = new StringBuilder();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                JsonString.Append("[");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    JsonString.Append("{");
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        if (j < ds.Tables[0].Columns.Count - 1)
                        {
                            JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                        }
                        else if (j == ds.Tables[0].Columns.Count - 1)
                        {
                            JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == ds.Tables[0].Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }
                JsonString.Append("]");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }

        public static string GetMsgInEnForDB(string MsgCode, String Lang)
        {
            MyClass cla = new MyClass();
            if (Lang.Trim() == "en-IN")
            {
                return cla.GetExecuteScalar("Select MessageInEng from Tbl_M_Message Where MessageCode='" + MsgCode + "'");
            }
            else
            {
                return cla.GetExecuteScalar("Select ISNULL(MessageInMr,MessageInEng) from Tbl_M_Message Where MessageCode='" + MsgCode + "'");
            }
        }

        //public static string GetMsgInMrForDB(string MsgCode)
        //{
        //    MyClass cla = new MyClass();
        //    return cla.GetExecuteScalar("Select MessageInMr from Tbl_M_Message Where MessageCode=" + MsgCode + "");
        //}
        public static string CheckAllowedFileExtension(string FileExtention)
        {
            var data = FileExtention.Replace(".", "");
            string[] validFileTypes = { "bmp", "gif", "png", "jpg", "jpeg", "pdf" };
            string ext = FileExtention;
            bool isValidFile = false;
            for (int i = 0; i < validFileTypes.Length; i++)
            {
                if (ext.ToUpper() == "." + validFileTypes[i].ToUpper())
                {
                    isValidFile = true;
                    break;
                }

            }
            if (!isValidFile)
            {
                return "Invalid File. Please upload a File with extension " + string.Join(",", validFileTypes);
            }
            else
            {
                return "";

            }
        }

        public static bool CheckApiAuthrization(String APIID, String APICOdeID)
        {
            bool Rets = false;
            if (APICOdeID.Trim() == "GITIAPPS@751985")
            {

                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "CheckApiAuthrization";
                err.ProjectName = "POCRA Web Library API";
                err.ErrorDescription = "GITI API Call API ID :" + APIID + " , APICOdeID : " + APICOdeID;
                err.ErrorSeverity = (int)ErrorSeverity.Information;
                new Repository.Classes.ErrorLogManager().InsertErrorLog(err);


                Rets = true;

            }
            if (APICOdeID.Trim() == "POCRAFFS@751985")
            {
                ErrorLogModel err = new ErrorLogModel();
                err.ErrorTitle = "CheckApiAuthrization";
                err.ProjectName = "POCRA Web Library API";
                err.ErrorDescription = "POCRAFFS API Call API ID :" + APIID + " , APICOdeID : " + APICOdeID;
                err.ErrorSeverity = (int)ErrorSeverity.Information;
                new Repository.Classes.ErrorLogManager().InsertErrorLog(err);


                Rets = true;

            }
            if (Rets == false)
            {
                return false;
            }


            MyClass cla = new MyClass();
            bool Ret = false;
            if (cla.GetExecuteScalar("SELECT APIAllOWEDID FROM Tbl_M_APIAllOWED where IsDeleted is null and APIID=" + APIID + " and APICOdeID in (SELECT APICOdeID FROM Tbl_M_APIAUTHCODE where IsDeleted is null and  DepSecurityKey='" + APICOdeID.Trim() + "') ").Trim().Length > 0)
            {
                Ret = true;
            }


            return Ret;
        }
        public static void UpdateRegistrationNo()
        {
            MyClass cla = new MyClass();

            DataTable dt = cla.GetDataTable("SELECT RegistrationID FROM Tbl_M_RegistrationDetails  where  RegistrationNo is null and IsDeleted is null");
            for (int x = 0; x != dt.Rows.Count; x++)
            {
                String RegistrationID = dt.Rows[x]["RegistrationID"].ToString();
                String RegistrationNo = "";
                String GPCode = cla.GetExecuteScalar("SELECT TOP 1 V.VillageCode FROM Tbl_M_RegistrationDetails AS R INNER JOIN Tbl_M_VillageMaster AS V ON R.Work_VillageID = V.VillageID WHERE (R.RegistrationID = " + RegistrationID.Trim() + ") ");
                if (GPCode.Length == 0)
                {

                }
                String RuningNo = cla.GetExecuteScalar("Select Max(RegistrationNo) from Tbl_M_RegistrationDetails where Tbl_M_RegistrationDetails.Work_VillageID =(SELECT Work_VillageID FROM Tbl_M_RegistrationDetails AS R  WHERE (R.RegistrationID =" + RegistrationID.Trim() + "))");
                if (RuningNo.Trim().Length > 0)
                {
                    RegistrationNo = (Convert.ToDouble(RuningNo) + 1).ToString();
                }
                else
                {
                    RegistrationNo = GPCode + "0001";
                }

                cla.ExecuteCommand("UPDATE Tbl_M_RegistrationDetails SET  RegistrationNo ='" + RegistrationNo + "' where RegistrationID=" + RegistrationID + "");
            }
        }

    }
}

