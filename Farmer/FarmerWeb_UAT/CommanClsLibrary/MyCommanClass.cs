using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using CommanClsLibrary.Repository.Models;
using static CommanClsLibrary.Repository.Enums;

namespace CommanClsLibrary
{

    public class MyCommanClass
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

        public static bool ValidateDate(string date)
        {
            try
            {
                // for US, alter to suit if splitting on hyphen, comma, etc.
                string[] dateParts = date.Split('/');
                // 
                // create new date from the parts; if this does not fail
                // the method will return true and the date is valid
                // yy/mm/dd
                DateTime testDate = new
                    DateTime(Convert.ToInt32(dateParts[2]),
                    Convert.ToInt32(dateParts[1]),
                    Convert.ToInt32(dateParts[0]));

                return true;
            }
            catch
            {
                // if a test date cannot be created, the
                // method will return false
                return false;
            }
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

        public DataTable GetState(String CountryID)
        {
            if (System.Web.HttpContext.Current.Session["Lang"].ToString().Trim() == "en-IN")
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

        public DataTable GetCityAll(String State_ID, String LevelType_ID)
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }

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
        public DataTable GetCityPocra(String State_ID, String LevelType_ID)
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }

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




        public DataTable GetCityAll(String State_ID)
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }

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
        public DataTable GetCityPocra(String State_ID)
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }

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


        public DataTable GetVillage(String City_ID)
        {

            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }

            DataTable dt = new DataTable();
            if (System.Web.HttpContext.Current.Session["LevelType_ID"] == null)
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " order by VillageName");
                else
                    dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " order by VillageName");
            }
            else
            {
                if (System.Web.HttpContext.Current.Session["LevelType_ID"].ToString().Trim() == "5")
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND VillageID in (SELECT distinct VillageID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                    else
                        dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND VillageID in (SELECT distinct VillageID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                }
                else if (System.Web.HttpContext.Current.Session["LevelType_ID"].ToString().Trim() == "8")
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND ClustersMasterID in (SELECT distinct ClustersMasterID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                    else
                        dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND ClustersMasterID in (SELECT distinct ClustersMasterID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                }
                else
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " order by VillageName");
                    else
                        dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " order by VillageName");
                }
            }
            return dt;
        }
        public DataTable GetVillage(String City_ID, String TalukaID)
        {



            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }

            String UserInPocra = cla.GetExecuteScalar("Select City_ID from Tbl_M_City where City_ID=" + City_ID + " and UserInPocra is not null").Trim();

            DataTable dt = new DataTable();
            if (UserInPocra.Trim().Length == 0)
            {
                if (System.Web.HttpContext.Current.Session["LevelType_ID"] == null)
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " order by VillageName");
                    else
                        dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " order by VillageName");
                }
                else
                {
                    if (System.Web.HttpContext.Current.Session["LevelType_ID"].ToString().Trim() == "5")
                    {
                        if (Lang.Trim() == "en-IN")
                            dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " AND VillageID in (SELECT distinct VillageID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                        else
                            dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " AND VillageID in (SELECT distinct VillageID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                    }
                    else if (System.Web.HttpContext.Current.Session["LevelType_ID"].ToString().Trim() == "8")
                    {
                        if (Lang.Trim() == "en-IN")
                            dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " AND ClustersMasterID in (SELECT distinct ClustersMasterID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                        else
                            dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " AND ClustersMasterID in (SELECT distinct ClustersMasterID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                    }
                    else
                    {
                        if (Lang.Trim() == "en-IN")
                            dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " order by VillageName");
                        else
                            dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " order by VillageName");
                    }
                }

            }
            else
            {

                if (System.Web.HttpContext.Current.Session["LevelType_ID"] == null)
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null AND UserInPocra is not null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " order by VillageName");
                    else
                        dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null AND UserInPocra is not null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " order by VillageName");
                }
                else
                {
                    if (System.Web.HttpContext.Current.Session["LevelType_ID"].ToString().Trim() == "5")
                    {
                        if (Lang.Trim() == "en-IN")
                            dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null AND UserInPocra is not null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " AND VillageID in (SELECT distinct VillageID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                        else
                            dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null AND UserInPocra is not null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " AND VillageID in (SELECT distinct VillageID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                    }
                    else if (System.Web.HttpContext.Current.Session["LevelType_ID"].ToString().Trim() == "8")
                    {
                        if (Lang.Trim() == "en-IN")
                            dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null AND UserInPocra is not null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " AND ClustersMasterID in (SELECT distinct ClustersMasterID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                        else
                            dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null AND UserInPocra is not null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " AND ClustersMasterID in (SELECT distinct ClustersMasterID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                    }
                    else
                    {
                        if (Lang.Trim() == "en-IN")
                            dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null AND UserInPocra is not null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " order by VillageName");
                        else
                            dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null AND UserInPocra is not null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " order by VillageName");
                    }
                }

            }









            return dt;
        }

        public DataTable GetAllVillage(String City_ID, String TalukaID, String UserInPocra)
        {



            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }

            //String UserInPocra = "";// cla.GetExecuteScalar("Select City_ID from Tbl_M_City where City_ID=" + City_ID + " and UserInPocra is not null").Trim();

            DataTable dt = new DataTable();
            if (UserInPocra.Trim().Length == 0)
            {
                if (System.Web.HttpContext.Current.Session["LevelType_ID"] == null)
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " order by VillageName");
                    else
                        dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " order by VillageName");
                }
                else
                {
                    if (System.Web.HttpContext.Current.Session["LevelType_ID"].ToString().Trim() == "5")
                    {
                        if (Lang.Trim() == "en-IN")
                            dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " AND VillageID in (SELECT distinct VillageID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                        else
                            dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " AND VillageID in (SELECT distinct VillageID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                    }
                    else if (System.Web.HttpContext.Current.Session["LevelType_ID"].ToString().Trim() == "8")
                    {
                        if (Lang.Trim() == "en-IN")
                            dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " AND ClustersMasterID in (SELECT distinct ClustersMasterID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                        else
                            dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " AND ClustersMasterID in (SELECT distinct ClustersMasterID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                    }
                    else
                    {
                        if (Lang.Trim() == "en-IN")
                            dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " order by VillageName");
                        else
                            dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " order by VillageName");
                    }
                }

            }
            else
            {

                if (System.Web.HttpContext.Current.Session["LevelType_ID"] == null)
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null AND UserInPocra is not null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " order by VillageName");
                    else
                        dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null AND UserInPocra is not null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " order by VillageName");
                }
                else
                {
                    if (System.Web.HttpContext.Current.Session["LevelType_ID"].ToString().Trim() == "5")
                    {
                        if (Lang.Trim() == "en-IN")
                            dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null AND UserInPocra is not null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " AND VillageID in (SELECT distinct VillageID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                        else
                            dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null AND UserInPocra is not null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " AND VillageID in (SELECT distinct VillageID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                    }
                    else if (System.Web.HttpContext.Current.Session["LevelType_ID"].ToString().Trim() == "8")
                    {
                        if (Lang.Trim() == "en-IN")
                            dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null AND UserInPocra is not null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " AND ClustersMasterID in (SELECT distinct ClustersMasterID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                        else
                            dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null AND UserInPocra is not null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " AND ClustersMasterID in (SELECT distinct ClustersMasterID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                    }
                    else
                    {
                        if (Lang.Trim() == "en-IN")
                            dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null AND UserInPocra is not null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " order by VillageName");
                        else
                            dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null AND UserInPocra is not null and City_ID=" + City_ID + " AND TalukaID=" + TalukaID + " order by VillageName");
                    }
                }

            }









            return dt;
        }

        public DataTable GetVillageAsPerWorkArea()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            // return cla.GetDataTable("SELECT VillageID, VillageCode  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and VillageCode is not null and VillageCode<>'' order by VillageName");
            DataTable dt = new DataTable();
            if (System.Web.HttpContext.Current.Session["LevelType_ID"] == null)
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null AND UserInPocra IS NOT NULL  order by VillageName");
                else
                    dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null AND UserInPocra IS NOT NULL  order by VillageName");
            }
            else
            {
                if (System.Web.HttpContext.Current.Session["LevelType_ID"].ToString().Trim() == "5")
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null AND UserInPocra IS NOT NULL  AND VillageID in (SELECT distinct VillageID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))  order by VillageName");
                    else
                        dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null AND UserInPocra IS NOT NULL  AND VillageID in (SELECT distinct VillageID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                }
                else if (System.Web.HttpContext.Current.Session["LevelType_ID"].ToString().Trim() == "8")
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null AND UserInPocra IS NOT NULL  AND ClustersMasterID in (SELECT distinct ClustersMasterID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                    else
                        dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null AND UserInPocra IS NOT NULL  AND ClustersMasterID in (SELECT distinct ClustersMasterID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                }
                else
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null AND UserInPocra IS NOT NULL  order by VillageName");
                    else
                        dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null  AND UserInPocra IS NOT NULL order by VillageName");
                }
            }
            return dt;
        }

        public DataTable GetPocraVillageAsPerWorkArea()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            // return cla.GetDataTable("SELECT VillageID, VillageCode  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and VillageCode is not null and VillageCode<>'' order by VillageName");
            DataTable dt = new DataTable();
            if (System.Web.HttpContext.Current.Session["LevelType_ID"] == null)
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V  where V.IsDeleted is null AND V.UserInPocra IS NOT NULL order by VillageName");
                else
                    dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V  where V.IsDeleted is null AND V.UserInPocra IS NOT NULL  order by VillageName");
            }
            else
            {
                if (System.Web.HttpContext.Current.Session["LevelType_ID"].ToString().Trim() == "5")
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V  where V.IsDeleted is null AND V.UserInPocra IS NOT NULL AND V.VillageID in (SELECT distinct VillageID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))  order by VillageName");
                    else
                        dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V  where V.IsDeleted is null AND V.UserInPocra IS NOT NULL  AND V.VillageID in (SELECT distinct VillageID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) order by VillageName");
                }
                else if (System.Web.HttpContext.Current.Session["LevelType_ID"].ToString().Trim() == "8")
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V where V.IsDeleted is null  AND V.UserInPocra IS NOT NULL AND ClustersMasterID in (SELECT distinct ClustersMasterID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))  order by VillageName");
                    else
                        dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V where V.IsDeleted is null AND V.UserInPocra IS NOT NULL AND ClustersMasterID in (SELECT distinct ClustersMasterID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))  order by VillageName");
                    // dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V inner join Tbl_M_City As C on C.City_ID=V.City_ID where V.IsDeleted is null  AND ClustersMasterID in (SELECT distinct ClustersMasterID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) AND C.UserInPocra is not null order by VillageName");
                }
                else if (System.Web.HttpContext.Current.Session["LevelType_ID"].ToString().Trim() == "3")
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V where V.IsDeleted is null  AND V.UserInPocra IS NOT NULL AND TalukaID in (SELECT distinct TalukaID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))  order by VillageName");
                    else
                        dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V where V.IsDeleted is null AND V.UserInPocra IS NOT NULL AND TalukaID in (SELECT distinct TalukaID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))  order by VillageName");
                    // dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V inner join Tbl_M_City As C on C.City_ID=V.City_ID where V.IsDeleted is null  AND ClustersMasterID in (SELECT distinct ClustersMasterID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) AND C.UserInPocra is not null order by VillageName");
                }
                else
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V  where V.IsDeleted is null  AND V.UserInPocra is not null order by VillageName");
                    else
                        //dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName As V  FROM Tbl_M_VillageMaster where V.IsDeleted is null AND C.UserInPocra is not null  order by VillageName");
                        dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')as VillageName FROM Tbl_M_VillageMaster As V  where V.IsDeleted is null  AND V.UserInPocra is not null order by VillageName");
                }
            }
            return dt;
        }
        public DataTable GetVillage()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT VillageID, VillageCode  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and VillageCode is not null and VillageCode<>'' and UserInPocra is not null  order by VillageName");
            else
                return cla.GetDataTable("SELECT VillageID, VillageCode  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and VillageCode is not null and VillageCode<>'' and UserInPocra is not null order by VillageName");

        }
        public DataTable GetClustersMaster()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT ClustersMasterID, Clusters FROM  Tbl_M_ClustersMaster where IsDeleted is null  order by Clusters");
            else
                return cla.GetDataTable("SELECT ClustersMasterID, ISNULL(ClustersMr,Clusters) as Clusters FROM  Tbl_M_ClustersMaster where IsDeleted is null  order by Clusters");

        }

        public DataTable GetTalukaMaster()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT TalukaID, Taluka FROM  Tbl_M_TalukaMaster where IsDeleted is null  order by Taluka");
            else
                return cla.GetDataTable("SELECT TalukaID, ISNULL(TalukaMr,Taluka) as Taluka FROM  Tbl_M_TalukaMaster where IsDeleted is null  order by Taluka");

        }

        public DataTable GetTalukaMaster(String City_ID)
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }

            DataTable dt = new DataTable();
            if (System.Web.HttpContext.Current.Session["LevelType_ID"] == null)
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT DISTINCT T.TalukaID, T.Taluka FROM Tbl_M_VillageMaster AS V INNER JOIN Tbl_M_TalukaMaster AS T ON V.TalukaID = T.TalukaID WHERE  V.IsDeleted is null and UserInPocra is not null and  (T.IsDeleted IS NULL) AND (V.City_ID =" + City_ID + ") order by T.Taluka");
                else
                    dt = cla.GetDataTable("SELECT DISTINCT T.TalukaID, ISNULL(T.TalukaMr,T.Taluka) as Taluka  FROM Tbl_M_VillageMaster AS V INNER JOIN Tbl_M_TalukaMaster AS T ON V.TalukaID = T.TalukaID WHERE V.IsDeleted is null and UserInPocra is not null and  (T.IsDeleted IS NULL) AND (V.City_ID =" + City_ID + ") order by Taluka");
            }
            else
            {
                if (System.Web.HttpContext.Current.Session["LevelType_ID"].ToString().Trim() == "3")
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT DISTINCT T.TalukaID, T.Taluka FROM Tbl_M_VillageMaster AS V INNER JOIN Tbl_M_TalukaMaster AS T ON V.TalukaID = T.TalukaID WHERE V.IsDeleted is null and UserInPocra is not null and  (T.IsDeleted IS NULL) AND (V.City_ID =" + City_ID + ") AND T.TalukaID  in (SELECT distinct TalukaID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))   order by T.Taluka");
                    else
                        dt = cla.GetDataTable("SELECT DISTINCT T.TalukaID, ISNULL(T.TalukaMr,T.Taluka) as Taluka FROM Tbl_M_VillageMaster AS V INNER JOIN Tbl_M_TalukaMaster AS T ON V.TalukaID = T.TalukaID WHERE V.IsDeleted is null and UserInPocra is not null and (T.IsDeleted IS NULL) AND (V.City_ID =" + City_ID + ") AND T.TalukaID  in (SELECT distinct TalukaID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))   order by Taluka");
                }
                else
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT DISTINCT T.TalukaID, T.Taluka FROM Tbl_M_VillageMaster AS V INNER JOIN Tbl_M_TalukaMaster AS T ON V.TalukaID = T.TalukaID WHERE V.IsDeleted is null and UserInPocra is not null and (T.IsDeleted IS NULL) AND (V.City_ID =" + City_ID + ") order by T.Taluka");
                    else
                        dt = cla.GetDataTable("SELECT DISTINCT T.TalukaID, ISNULL(T.TalukaMr,T.Taluka) as Taluka FROM Tbl_M_VillageMaster AS V INNER JOIN Tbl_M_TalukaMaster AS T ON V.TalukaID = T.TalukaID WHERE V.IsDeleted is null and UserInPocra is not null and (T.IsDeleted IS NULL) AND (V.City_ID =" + City_ID + ") order by Taluka");
                }
            }
            return dt;

        }

        public DataTable GetTalukaMasterAll(String City_ID)
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }

            DataTable dt = new DataTable();
            if (System.Web.HttpContext.Current.Session["LevelType_ID"] == null)
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT DISTINCT T.TalukaID, T.Taluka FROM Tbl_M_VillageMaster AS V INNER JOIN Tbl_M_TalukaMaster AS T ON V.TalukaID = T.TalukaID WHERE  V.IsDeleted is null  and  (T.IsDeleted IS NULL) AND (V.City_ID =" + City_ID + ") order by T.Taluka");
                else
                    dt = cla.GetDataTable("SELECT DISTINCT T.TalukaID, ISNULL(T.TalukaMr,T.Taluka) as Taluka  FROM Tbl_M_VillageMaster AS V INNER JOIN Tbl_M_TalukaMaster AS T ON V.TalukaID = T.TalukaID WHERE V.IsDeleted is null  and  (T.IsDeleted IS NULL) AND (V.City_ID =" + City_ID + ") order by Taluka");
            }
            else
            {
                if (System.Web.HttpContext.Current.Session["LevelType_ID"].ToString().Trim() == "3")
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT DISTINCT T.TalukaID, T.Taluka FROM Tbl_M_VillageMaster AS V INNER JOIN Tbl_M_TalukaMaster AS T ON V.TalukaID = T.TalukaID WHERE V.IsDeleted is null  and  (T.IsDeleted IS NULL) AND (V.City_ID =" + City_ID + ") AND T.TalukaID  in (SELECT distinct TalukaID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))   order by T.Taluka");
                    else
                        dt = cla.GetDataTable("SELECT DISTINCT T.TalukaID, ISNULL(T.TalukaMr,T.Taluka) as Taluka FROM Tbl_M_VillageMaster AS V INNER JOIN Tbl_M_TalukaMaster AS T ON V.TalukaID = T.TalukaID WHERE V.IsDeleted is null  and (T.IsDeleted IS NULL) AND (V.City_ID =" + City_ID + ") AND T.TalukaID  in (SELECT distinct TalukaID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))   order by Taluka");
                }
                else
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT DISTINCT T.TalukaID, T.Taluka FROM Tbl_M_VillageMaster AS V INNER JOIN Tbl_M_TalukaMaster AS T ON V.TalukaID = T.TalukaID WHERE V.IsDeleted is null  and (T.IsDeleted IS NULL) AND (V.City_ID =" + City_ID + ") order by T.Taluka");
                    else
                        dt = cla.GetDataTable("SELECT DISTINCT T.TalukaID, ISNULL(T.TalukaMr,T.Taluka) as Taluka FROM Tbl_M_VillageMaster AS V INNER JOIN Tbl_M_TalukaMaster AS T ON V.TalukaID = T.TalukaID WHERE V.IsDeleted is null  and (T.IsDeleted IS NULL) AND (V.City_ID =" + City_ID + ") order by Taluka");
                }
            }
            return dt;

        }

        public DataTable GetPostMaster(String City_ID)
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT DISTINCT T.Post_ID, T.PostName FROM Tbl_M_CityWisePost as T  WHERE  (T.IsDeleted IS NULL) AND (T.City_ID =" + City_ID + ") order by T.PostName");
            else
                return cla.GetDataTable("SELECT DISTINCT T.Post_ID, isnull(T.PostNameMr,T.PostName) as PostName FROM Tbl_M_CityWisePost as T  WHERE  (T.IsDeleted IS NULL) AND (T.City_ID =" + City_ID + ") order by PostName");

        }
        public DataTable GetPostTalukaWiseMaster(String TalukaID, String City_ID)
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT DISTINCT T.Post_ID, T.PostName FROM Tbl_M_CityWisePost as T  WHERE  (T.IsDeleted IS NULL) AND (T.TalukaID =" + TalukaID + ") and City_ID=" + City_ID + "   order by T.PostName");
            else
                return cla.GetDataTable("SELECT DISTINCT T.Post_ID, isnull(T.PostNameMr,T.PostName) as PostName FROM Tbl_M_CityWisePost as T  WHERE  (T.IsDeleted IS NULL) AND (T.TalukaID =" + TalukaID + ") and City_ID=" + City_ID + "  order by PostName");

        }
        public DataTable GetBeneficiaryType()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT BeneficiaryTypesID, BeneficiaryTypes FROM  Tbl_M_BeneficiaryTypes where IsDeleted is null  order by BeneficiaryTypesID");
            else
                return cla.GetDataTable("SELECT BeneficiaryTypesID, isnull(BeneficiaryTypesMr,BeneficiaryTypes) as BeneficiaryTypes FROM  Tbl_M_BeneficiaryTypes where IsDeleted is null  order by BeneficiaryTypesID");

        }

        public DataTable GetApprovalStages()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT ApprovalStageID, ApprovalStages FROM Tbl_M_ApprovalStages WHERE (IsDeleted IS NULL) and ApprovalStageID not in (1) ORDER BY OrderNo");
            else
                return cla.GetDataTable("SELECT ApprovalStageID, ISNULL(ApplicationStatusMr,ApprovalStages) as ApprovalStages FROM Tbl_M_ApprovalStages WHERE (IsDeleted IS NULL) and ApprovalStageID not in (1) ORDER BY OrderNo");
        }

        public DataTable GetMbBookComponents()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT M.MbBookMasterID, M.MbBookMaster+' - '+s.ApprovalStages as MbBookMaster FROM Tbl_M_MbBookMaster as M inner join Tbl_M_ApprovalStages s on s.ApprovalStageID=m.ApprovalStageID WHERE (M.IsDeleted IS NULL) ORDER BY M.OrderNo ");
            else
                return cla.GetDataTable("SELECT M.MbBookMasterID, ISNULL(M.MbBookMasterMr,M.MbBookMaster)+' - '+s.ApprovalStages as MbBookMaster FROM Tbl_M_MbBookMaster as M inner join Tbl_M_ApprovalStages s on s.ApprovalStageID=m.ApprovalStageID WHERE (M.IsDeleted IS NULL) ORDER BY M.OrderNo");
        }



        public DataTable GetCroppingSeason()
        {
            return cla.GetDataTable("SELECT CroppingSeasonID, CroppingSeason FROM Tbl_M_CroppingSeason WHERE (IsDeleted IS NULL)  ORDER BY CroppingSeason");
        }
        public DataTable GetCostCategoryMaster()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT CategoryMasterID, CategoryMaster FROM  Tbl_M_CategoryMaster where IsDeleted is null  order by CategoryMaster");
            else
                return cla.GetDataTable("SELECT CategoryMasterID, CategoryMasterMr as CategoryMaster FROM  Tbl_M_CategoryMaster where IsDeleted is null  order by CategoryMaster");

        }
        public DataTable GetGenderMaster()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT GenderName as ID, GenderName as Text FROM  Tbl_M_Gender where IsDeleted is null  order by GenderID");
            else
                return cla.GetDataTable("SELECT GenderName as ID, GenderNameMr as Text  FROM  Tbl_M_Gender where IsDeleted is null  order by GenderID");

        }
        public DataTable GetUnit()
        {

            return cla.GetDataTable("SELECT UnitMasterID, UnitofMes FROM  Tbl_M_UnitMaster where IsDeleted is null  order by UnitofMes");

        }
        public DataTable GetBank()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT distinct NameOFbank FROM  Tbl_M_RBIBankMaster where IsDeleted is null  order by NameOFbank");
            else
                return cla.GetDataTable("SELECT distinct ISNULL(NameOFbankMr,NameOFbank) as NameOFbank FROM  Tbl_M_RBIBankMaster where IsDeleted is null  order by NameOFbank");

        }
        public DataTable GetBankWiseBrach(String NameOFbank)
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT distinct  BranchName  FROM  Tbl_M_RBIBankMaster where IsDeleted is null  and NameOFbank='" + NameOFbank.Trim() + "' order by BranchName");
            else
                return cla.GetDataTable("SELECT distinct  BranchName  FROM  Tbl_M_RBIBankMaster where IsDeleted is null  and NameOFbank='" + NameOFbank.Trim() + "' order by BranchName");

        }


        public DataTable GetBankBranchWiseIFSC(String NameOFbank)
        {

            return cla.GetDataTable("SELECT distinct  IFSCCode+' - '+BranchName as BranchName , RBIBankID   FROM  Tbl_M_RBIBankMaster where IsDeleted is null  and NameOFbank='" + NameOFbank.Trim() + "' ");

        }


        public DataTable GetBankBranchWiseIFSC(String NameOFbank, String BranchName)
        {

            return cla.GetDataTable("SELECT distinct  IFSCCode ,RBIBankID  FROM  Tbl_M_RBIBankMaster where IsDeleted is null  and NameOFbank='" + NameOFbank.Trim() + "' and BranchName='" + BranchName + "' ");

        }

        public DataTable GetBankBranchWiseMICR(String NameOFbank, String BranchName)
        {

            return cla.GetDataTable("SELECT distinct  MICRCode  FROM  Tbl_M_RBIBankMaster where IsDeleted is null  and NameOFbank='" + NameOFbank.Trim() + "' and BranchName='" + BranchName + "' ");

        }
        public DataTable GetComponent()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT distinct ComponentID , ComponentName+' - '+ISNULL(ComponentCode,'')  as ComponentName ,ComponentCode  FROM  Tbl_M_Components where IsDeleted is null  order by ComponentCode");
            else
                return cla.GetDataTable("SELECT distinct ComponentID , ISNULL(ComponentNameMr,ComponentName)+' - '+ISNULL(ComponentCode,'')  as ComponentName ,ComponentCode  FROM  Tbl_M_Components where IsDeleted is null  order by ComponentCode");

        }

        public DataTable GetVertualAccountCrList()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT ProjectID as ID ,Project As Names  FROM Tbl_M_Project where IsDeleted is null");
            else
                return cla.GetDataTable("SELECT ProjectID ,ProjectMr as Names  FROM Tbl_M_Project where IsDeleted is null");

        }
        public DataTable GetVertualAccountDrList()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT SubdivisionID as ID, Subdivisions as Names FROM Tbl_M_Subdivision where IsDeleted is null order by Subdivisions");
            else
                return cla.GetDataTable("SELECT SubdivisionID as ID, SubdivisionsMr as Names FROM Tbl_M_Subdivision where IsDeleted is null order by Subdivisions");

        }

        public DataTable GetComponent(String BeneficiaryTypesID)
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
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


        public DataTable GetApplicationStatus(String StatusFor)
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }


            DataTable dt = new DataTable();
            if (StatusFor.Trim() == "R")
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UsedInVerification is not null  order by OrderByNo");
                else
                    dt = cla.GetDataTable("SELECT  ISNULL(ApplicationStatusMr,ApplicationStatus) as ApplicationStatus ,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UsedInVerification is not null  order by OrderByNo");
            }
            if (StatusFor.Trim() == "A")
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UserInApproval is not null  order by OrderByNo");
                else
                    dt = cla.GetDataTable("SELECT  ISNULL(ApplicationStatusMr,ApplicationStatus) as ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UserInApproval is not null  order by OrderByNo");
            }
            if (StatusFor.Trim() == "com")
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND  ApplicationStatusID in(5,6,2,26) order by OrderByNo desc");
                else
                    dt = cla.GetDataTable("SELECT  ISNULL(ApplicationStatusMr,ApplicationStatus) as ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null   and ApplicationStatusID in(5,6,2,26) order by OrderByNo desc");
            }
            if (StatusFor.Trim() == "comB")
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND  ApplicationStatusID in(5,6,2,27,1) order by OrderByNo desc");
                else
                    dt = cla.GetDataTable("SELECT  ISNULL(ApplicationStatusMr,ApplicationStatus) as ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null   and ApplicationStatusID in(5,6,2,27,1) order by OrderByNo desc");
            }
            if (StatusFor.Trim() == "P")
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UserInPayment is not null  order by OrderByNo");
                else
                    dt = cla.GetDataTable("SELECT  ISNULL(ApplicationStatusMr,ApplicationStatus) as ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UserInPayment is not null  order by OrderByNo");
            }
            if (StatusFor.Trim() == "comNR")
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null and ApplicationStatusID in(5,6,26) order by OrderByNo desc");
                else
                    dt = cla.GetDataTable("SELECT  ISNULL(ApplicationStatusMr,ApplicationStatus) as ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null   and ApplicationStatusID in(5,6,26) order by OrderByNo desc");
            }

            return dt;
        }


        public DataTable GetApplicationStatusWithFarmerIndividualApplicationStatus(String StatusFor, string ApplicationID)
        {

            DataTable dtApp = new DataTable();
            dtApp = cla.GetDataTable("select * from Vw_FarmerIndividualApplicationWithRepeatedRequests where ApplicationID=" + ApplicationID);


            DataTable dt = new DataTable();
            if (dtApp.Rows.Count > 0)
            {
                if (StatusFor.Trim() == "A")
                {
                    dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UserInApproval is not null and ApplicationStatusID in (2)  order by OrderByNo");
                }
                if (StatusFor.Trim() == "D5")
                {
                    dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where ApplicationStatusID in (2,100)  order by OrderByNo");
                }
            }
            else
            {
                if (StatusFor.Trim() == "A")
                {
                    dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UserInApproval is not null  order by OrderByNo");
                }
                if (StatusFor.Trim() == "D5")
                {
                    dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where   ApplicationStatusID in (2,5,100)  order by OrderByNo");
                }
            }


            //if (StatusFor.Trim() == "R")
            //{
            //    if (Lang.Trim() == "en-IN")
            //        dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UsedInVerification is not null  order by OrderByNo");
            //    else
            //        dt = cla.GetDataTable("SELECT  ISNULL(ApplicationStatusMr,ApplicationStatus) as ApplicationStatus ,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UsedInVerification is not null  order by OrderByNo");
            //}

            //if (StatusFor.Trim() == "com")
            //{
            //    if (Lang.Trim() == "en-IN")
            //        dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND  ApplicationStatusID in(5,6,2,26) order by OrderByNo desc");
            //    else
            //        dt = cla.GetDataTable("SELECT  ISNULL(ApplicationStatusMr,ApplicationStatus) as ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null   and ApplicationStatusID in(5,6,2,26) order by OrderByNo desc");
            //}
            //if (StatusFor.Trim() == "comB")
            //{
            //    if (Lang.Trim() == "en-IN")
            //        dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND  ApplicationStatusID in(5,6,2,27,1) order by OrderByNo desc");
            //    else
            //        dt = cla.GetDataTable("SELECT  ISNULL(ApplicationStatusMr,ApplicationStatus) as ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null   and ApplicationStatusID in(5,6,2,27,1) order by OrderByNo desc");
            //}
            //if (StatusFor.Trim() == "P")
            //{
            //    if (Lang.Trim() == "en-IN")
            //        dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UserInPayment is not null  order by OrderByNo");
            //    else
            //        dt = cla.GetDataTable("SELECT  ISNULL(ApplicationStatusMr,ApplicationStatus) as ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UserInPayment is not null  order by OrderByNo");
            //}
            //if (StatusFor.Trim() == "comNR")
            //{
            //    if (Lang.Trim() == "en-IN")
            //        dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null and ApplicationStatusID in(5,6,26) order by OrderByNo desc");
            //    else
            //        dt = cla.GetDataTable("SELECT  ISNULL(ApplicationStatusMr,ApplicationStatus) as ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null   and ApplicationStatusID in(5,6,26) order by OrderByNo desc");
            //}

            return dt;
        }


        public DataTable GetFarmerRepeatRequestDetails(string ApplicationID)
        {
            DataTable dtApp = new DataTable();
            dtApp = cla.GetDataTable("select * from Vw_FarmerIndividualApplicationWithRepeatedRequests where ApplicationID=" + ApplicationID);
            return dtApp;
        }



        //public DataTable GetApplicationStatus(String StatusFor)
        //{
        //    String Lang = "en-IN";
        //    try
        //    {
        //        if (System.Web.HttpContext.Current.Session["Lang"] != null)
        //        {
        //            Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
        //        }
        //    }
        //    catch { }


        //    DataTable dt = new DataTable();
        //    if (StatusFor.Trim() == "R")
        //    {
        //        if (Lang.Trim() == "en-IN")
        //            dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UsedInVerification is not null  order by OrderByNo");
        //        else
        //            dt = cla.GetDataTable("SELECT  ISNULL(ApplicationStatusMr,ApplicationStatus) as ApplicationStatus ,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UsedInVerification is not null  order by OrderByNo");
        //    }
        //    if (StatusFor.Trim() == "A")
        //    {
        //        if (Lang.Trim() == "en-IN")
        //            dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UserInApproval is not null  order by OrderByNo");
        //        else
        //            dt = cla.GetDataTable("SELECT  ISNULL(ApplicationStatusMr,ApplicationStatus) as ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UserInApproval is not null  order by OrderByNo");
        //    }
        //    if (StatusFor.Trim() == "P")
        //    {
        //        if (Lang.Trim() == "en-IN")
        //            dt = cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UserInPayment is not null  order by OrderByNo");
        //        else
        //            dt = cla.GetDataTable("SELECT  ISNULL(ApplicationStatusMr,ApplicationStatus) as ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null AND UserInPayment is not null  order by OrderByNo");
        //    }
        //    return dt;

        //}
        //public DataTable GetApplicationStatus2()
        //{

        //    return cla.GetDataTable("SELECT  ApplicationStatus,ApplicationStatusID FROM  Tbl_M_ApplicationStatus where IsDeleted is null and ApplicationStatusID<>5 order by ApplicationStatus");

        //}
        public DataTable GetActivityCategory()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT ActivityCategory, ActivityCategoryID  FROM  Tbl_M_ActivityCategory where IsDeleted is null  order by ActivityCategory");
            else
                return cla.GetDataTable("SELECT ActivityCategoryMr as ActivityCategory, ActivityCategoryID  FROM  Tbl_M_ActivityCategory where IsDeleted is null  order by ActivityCategory");

        }

        public DataTable GetActivityGroup()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT ActivityGroupName, ActivityGroupID  FROM  Tbl_M_Activity_Groups where IsDeleted is null  order by ActivityGroupName");
            else
                return cla.GetDataTable("SELECT ActivityGroupNameMr as ActivityGroupName, ActivityGroupID  FROM  Tbl_M_Activity_Groups where IsDeleted is null  order by ActivityGroupName");

        }
        public DataTable GetActivityGroupFarmers()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT ActivityGroupName, ActivityGroupID  FROM  Tbl_M_Activity_Groups where IsDeleted is null  and ActivityGroupCode not in ('FPC','NRM') order by ActivityGroupName");
            else
                return cla.GetDataTable("SELECT ActivityGroupNameMr as ActivityGroupName, ActivityGroupID  FROM  Tbl_M_Activity_Groups where IsDeleted is null and ActivityGroupCode not in ('FPC','NRM')  order by ActivityGroupName");

        }
        public DataTable GetActivityGroupForFPO()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT ActivityGroupName, ActivityGroupID,ImageOfActivityGroup  FROM  Tbl_M_Activity_Groups where IsDeleted is null and ActivityGroupCode in ('FPC')  order by ActivityGroupName");
            else
                return cla.GetDataTable("SELECT ActivityGroupNameMr as ActivityGroupName, ActivityGroupID,ImageOfActivityGroup  FROM  Tbl_M_Activity_Groups where IsDeleted is null and ActivityGroupCode in ('FPC') order by ActivityGroupName");

        }



        public DataTable SubGetComponent(String ComponentID)
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
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
        public DataTable SubGetComponent(String ComponentID, String BeneficiaryTypesID)
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
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
        public DataTable SubGetComponent()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT SubComponentID, SubComponentCode+' '+SubComponentName as SubComponentName ,SubComponentCode FROM Tbl_M_ComponentSub where IsDeleted is null order by SubComponentCode ");
            else
                return cla.GetDataTable("SELECT SubComponentID, SubComponentCode+' '+ISNULL(SubComponentNameMr,SubComponentName) as SubComponentName ,SubComponentCode FROM Tbl_M_ComponentSub where IsDeleted is null order by SubComponentCode ");

        }

        public DataTable GetEligibilityCriteria()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT EligibilityID , EligibilityCriteria  FROM  Tbl_M_EligibilityCriteria where IsDeleted is null  order by EligibilityCriteria");
            else
                return cla.GetDataTable("SELECT EligibilityID , EligibilityCriteriaMr as EligibilityCriteria  FROM  Tbl_M_EligibilityCriteria where IsDeleted is null  order by EligibilityCriteria");

        }
        public DataTable GetRequiredDoc()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT RequiredDocID , RequiredDoc  FROM  Tbl_M_DocumentRequired where IsDeleted is null  order by RequiredDoc");
            else
                return cla.GetDataTable("SELECT RequiredDocID , RequiredDocMr as RequiredDoc  FROM  Tbl_M_DocumentRequired where IsDeleted is null  order by RequiredDoc");

        }

        public DataTable GetDeskWiseRequiredDoc(String DocumentGroupID)
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT  D.DocumentTypeNameMr+'~'+A.MandatoryOptional as DocumentTypeName,D.DeskWiseDocumentID FROM FPO_M_DeskWiseDocument as D inner join Tbl_M_MandatoryOptional A on a.MandatoryOptionalID=D.MandatoryOptionalID where D.DocumentGroupID=" + DocumentGroupID + " and D.IsDeleted is null  order by D.DocumentTypeName");
            else
                return cla.GetDataTable("SELECT  D.DocumentTypeNameMr+'~'+A.MandatoryOptional as DocumentTypeName,D.DeskWiseDocumentID FROM FPO_M_DeskWiseDocument as D inner join Tbl_M_MandatoryOptional A on a.MandatoryOptionalID=D.MandatoryOptionalID where D.DocumentGroupID=" + DocumentGroupID + " and D.IsDeleted is null  order by D.DocumentTypeName");

        }




        public DataTable GetApprovalCheckList(String DocumentGroupID, String CheckListTypeID)
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT A.FPOCheckListID, ISNULL(A.CheckListNameMr,A.CheckListName) +'~'+M.MandatoryOptional as FeasibilityRpt FROM FPO_M_DeskWiseCheckList AS A  INNER JOIN Tbl_M_MandatoryOptional AS M ON A.MandatoryOptionalID = M.MandatoryOptionalID  WHERE (A.IsDeleted IS NULL)  AND A.DocumentGroupID=" + DocumentGroupID.Trim() + " and CheckListTypeID=" + CheckListTypeID + " ORDER BY A.FPOCheckListID");
            else
                return cla.GetDataTable("SELECT A.FPOCheckListID, A.CheckListNameMr+'~'+M.MandatoryOptional as FeasibilityRpt FROM FPO_M_DeskWiseCheckList AS A  INNER JOIN Tbl_M_MandatoryOptional AS M ON A.MandatoryOptionalID = M.MandatoryOptionalID  WHERE (A.IsDeleted IS NULL)  AND A.DocumentGroupID=" + DocumentGroupID.Trim() + " and CheckListTypeID=" + CheckListTypeID + " ORDER BY A.FPOCheckListID");

        }


        public DataTable GetFeasibility()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT FeasibilityRptID , FeasibilityRpt  FROM  Tbl_M_FeasibilityRpt where IsDeleted is null  order by FeasibilityRpt");
            else
                return cla.GetDataTable("SELECT FeasibilityRptID , FeasibilityRptMr as FeasibilityRpt  FROM  Tbl_M_FeasibilityRpt where IsDeleted is null  order by FeasibilityRpt");

        }
        public DataTable GetFeasibility(String ActivityID)
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }

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
        public DataTable GetPaymentFeasibility(String ActivityID)
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT A.FeasibilityRptID, A.FeasibilityRpt+'~'+M.MandatoryOptional as FeasibilityRpt FROM Tbl_M_FeasibilityRpt AS A INNER JOIN Tbl_M_ActivityFeasibility AS B ON A.FeasibilityRptID = B.FeasibilityRptID INNER JOIN Tbl_M_MandatoryOptional AS M ON A.MandatoryOptionalID = M.MandatoryOptionalID WHERE (A.IsDeleted IS NULL) AND (B.IsDeleted IS NULL) AND (B.ActivityID = " + ActivityID.Trim() + ") AND A.FeasibilityRptTypeID=2 ORDER BY A.FeasibilityRpt");
            else
                return cla.GetDataTable("SELECT A.FeasibilityRptID, ISNULL(A.FeasibilityRptMr,A.FeasibilityRpt)+'~'+M.MandatoryOptional as FeasibilityRpt FROM Tbl_M_FeasibilityRpt AS A INNER JOIN Tbl_M_ActivityFeasibility AS B ON A.FeasibilityRptID = B.FeasibilityRptID INNER JOIN Tbl_M_MandatoryOptional AS M ON A.MandatoryOptionalID = M.MandatoryOptionalID WHERE (A.IsDeleted IS NULL) AND (B.IsDeleted IS NULL) AND (B.ActivityID = " + ActivityID.Trim() + ") AND A.FeasibilityRptTypeID=2 ORDER BY A.FeasibilityRpt");

        }
        public DataTable GetCrop()
        {

            return cla.GetDataTable("SELECT CropID as ID,Crop as Name FROM Tbl_M_Crop where IsDeleted is null order by Crop");

        }

        public DataTable GetForm8A(String RegistrationID)
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
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
        public DataTable GetLevelType_ForProject()
        {

            return cla.GetDataTable("SELECT LevelTypeID, LevelType FROM  Tbl_M_LevelType where IsDeleted is null and IsUsedInProject is not null order by OrderByNo");

        }
        public DataTable GetUserRole()
        {

            return cla.GetDataTable("SELECT UserRoleID, UserRole FROM  Tbl_M_UserRole where IsDeleted is null  order by UserRoleID");

        }
        public DataTable GetHierarchy()
        {

            return cla.GetDataTable("SELECT D.Desig_ID, D.Desig_Name +' _ '+ L.LevelType+' Level ' as Desig_Name FROM  Tbl_M_Designation AS D INNER JOIN Tbl_M_LevelType AS L ON D.LevelType_ID = L.LevelTypeID WHERE (D.IsDeleted IS NULL) ORDER BY D.Desig_Name");

        }
        public DataTable GetRegisteredUnder()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT RegisterUnderID as ID ,RegisterUnder As Test FROM Tbl_M_RegisterUnder where IsDeleted is null order by RegisterUnder");
            else
                return cla.GetDataTable("SELECT RegisterUnderID as ID ,RegisterUnderMr as RegisterUnder FROM Tbl_M_RegisterUnder where IsDeleted is null order by RegisterUnder");

        }
        public DataTable GetRegisteredThrough()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT RegisteredThroughID as ID ,RegisteredThrough As Test  FROM Tbl_M_RegisteredThrough where IsDeleted is null order by RegisteredThrough");
            else
                return cla.GetDataTable("SELECT RegisteredThroughID as ID ,RegisteredThroughMr As Test  FROM Tbl_M_RegisteredThrough where IsDeleted is null order by RegisteredThrough");

        }
        public DataTable GetCircle()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT CircleID As ID ,CircleName as Names FROM Tbl_M_Circle where IsDeleted is null order by CircleName ");
            else
                return cla.GetDataTable("SELECT CircleID As ID ,CircleNameMr as Names FROM Tbl_M_Circle where IsDeleted is null order by CircleName ");
        }



        public DataTable GetSubDevesions(String City_ID)
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable(" SELECT SubdivisionID ,Subdivisions FROM Tbl_M_Subdivision where City_ID=" + City_ID + " and IsDeleted is null order by Subdivisions ");
            else
                return cla.GetDataTable(" SELECT SubdivisionID ,SubdivisionsMr as Subdivisions FROM Tbl_M_Subdivision where City_ID=" + City_ID + " and IsDeleted is null order by Subdivisions ");
        }


        public DataTable GetReasonVerifecation(String ApprovalStageID, String ApplicationStatusID)
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT ReasonID ,Reasons ,ReasonsMr ,ReasonsCode FROM Tbl_M_Reason where IsDeleted IS NULL AND ApprovalStageID=" + ApprovalStageID + " and ApplicationStatusID=" + ApplicationStatusID + " ");
            else
                return cla.GetDataTable("SELECT ReasonID ,ReasonsMr as Reasons ,ReasonsMr ,ReasonsCode FROM Tbl_M_Reason where IsDeleted IS NULL AND ApprovalStageID=" + ApprovalStageID + " and ApplicationStatusID=" + ApplicationStatusID + " ");
        }
        public DataTable GetReasonVerifecationFPO(String ApprovalStageID, String ApplicationStatusID)
        {

            return cla.GetDataTable("SELECT ReasonID ,ReasonsMr as Reasons ,ReasonsMr ,ReasonsCode FROM Tbl_M_Reason where IsDeleted IS NULL AND ApprovalStageID=" + ApprovalStageID + " and ApplicationStatusID=" + ApplicationStatusID + " ");
        }
        public DataTable GetImageTitle()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT ImageTitleID, ImageTitleName  FROM Tbl_M_ImageTitle where IsDeleted is null order by ImageTitleName");
            else
                return cla.GetDataTable("SELECT ImageTitleID, ImageTitleNameMr as ImageTitleName  FROM Tbl_M_ImageTitle where IsDeleted is null order by ImageTitleName");

        }
        public DataTable GetPaymentTerm()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT PaymentTermID As ID ,PaymentTermName as Names FROM Tbl_M_PaymentTerm where IsDeleted is null order by PaymentTermName ");
            else
                return cla.GetDataTable("SELECT PaymentTermID As ID ,PaymentTermNameMr as Names FROM Tbl_M_PaymentTerm where IsDeleted is null order by PaymentTermName ");
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
        //
        public DataTable GetActivity()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT ActivityID, ActivityName  FROM  Tbl_M_ActivityMaster where IsDeleted is null  order by ActivityName");
            else
                return cla.GetDataTable("SELECT ActivityNameMr as ActivityName, ActivityID  FROM  Tbl_M_ActivityMaster where IsDeleted is null  order by ActivityName");

        }
        public DataTable GetActivity(String ActivityGroupID)
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT ActivityID, ActivityName  FROM  Tbl_M_ActivityMaster where IsDeleted is null and ActivityGroupID=" + ActivityGroupID + "  order by ActivityName");
            else
                return cla.GetDataTable("SELECT ActivityNameMr as ActivityName, ActivityID  FROM  Tbl_M_ActivityMaster where IsDeleted is null and ActivityGroupID=" + ActivityGroupID + "  order by ActivityName");

        }

        public DataTable GetSubDivision()
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("Select SubdivisionID, Subdivisions from Tbl_M_Subdivision where IsDeleted is null order by Subdivisions");
            else
                return cla.GetDataTable("Select SubdivisionsMr as Subdivisions, SubdivisionID from Tbl_M_Subdivision where IsDeleted is null order by Subdivisions");

        }

        public DataTable GetProjects()
        {
            return cla.GetDataTable("Select ProjectID,ProjectName from Project_Master where Isnull(IsDeleted,0)=0 order by ProjectID");
        }

        public DataTable GetActivityPanel()
        {
            return cla.GetDataTable("Select PanelId, PanelCode from Tbl_M_DeskFourActivityPanel where IsDeleted is null");
        }
        //
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
                err.ErrorTitle = "MyCommonClass:RecordExistanceChk";
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
            if (cla.GetExecuteScalar("SELECT  " + PrimaryFieldName + "  FROM  " + TableName + "  WHERE (" + ChkFieldName + " = '" + ChkFieldValue + "') AND (" + PrimaryFieldName + "<>" + PrimaryFieldValue + ") AND IsDeleted is null ").Length > 0)
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
                return cla.GetExecuteScalar("Select MessageInMr from Tbl_M_Message Where MessageCode='" + MsgCode + "'");
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
            //cla.ExecuteCommand("UPDATE Tbl_M_RegistrationDetails SET  RegistrationNo =NULL");

            DataTable dt = cla.GetDataTable("SELECT RegistrationID FROM Tbl_M_RegistrationDetails  where  RegistrationNo is null and IsDeleted is null order by RegistrationDate ");
            for (int x = 0; x != dt.Rows.Count; x++)
            {
                try
                {
                    String RegistrationID = dt.Rows[x]["RegistrationID"].ToString();
                    String RegistrationNo = "";
                    String GPCode = cla.GetExecuteScalar("SELECT TOP 1 V.VillageCode FROM Tbl_M_RegistrationDetails AS R INNER JOIN Tbl_M_VillageMaster AS V ON R.Work_VillageID = V.VillageID WHERE (R.RegistrationID = " + RegistrationID.Trim() + ") ");
                    if (GPCode.Length == 0)
                    {
                        GPCode = "1000";
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
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static string PutIntoQuotes(string value)
        {
            return "\"" + value + "\"";
        }


        public static string Crypto(string _salt)
        {
            var hashmd5 = new MD5CryptoServiceProvider();
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(_salt);

            byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(_salt));
            hashmd5.Clear();

            TripleDESCryptoServiceProvider TripleDesProvider = new TripleDESCryptoServiceProvider();
            TripleDesProvider.Key = keyArray;
            TripleDesProvider.Mode = CipherMode.ECB;
            TripleDesProvider.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = TripleDesProvider.CreateEncryptor();

            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string Decrypto(string _salt)
        {
            try
            {

                var hashmd5 = new MD5CryptoServiceProvider();
                byte[] toEncryptArray = Convert.FromBase64String(_salt);

                byte[] keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(_salt));

                hashmd5.Clear();
                TripleDESCryptoServiceProvider TripleDesProvider = new TripleDESCryptoServiceProvider();
                TripleDesProvider.Key = keyArray;
                TripleDesProvider.Mode = CipherMode.ECB;
                TripleDesProvider.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = TripleDesProvider.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                TripleDesProvider.Clear();

                return Encoding.UTF8.GetString(resultArray);
                //return Encoding.UTF8.GetString(resultArray);

                //using (var md5 = new MD5CryptoServiceProvider())
                //{
                //    using (var tdes = new TripleDESCryptoServiceProvider())
                //    {
                //        tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //        tdes.Mode = CipherMode.ECB;
                //        tdes.Padding = PaddingMode.PKCS7;

                //        using (var transform = tdes.CreateDecryptor())
                //        {
                //            byte[] cipherBytes = Convert.FromBase64String(cipher);
                //            byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                //            return UTF8Encoding.UTF8.GetString(bytes);
                //        }
                //    }
                //}
            }
            catch
            {
                return string.Empty;
            }
        }



        public static void UpdateLandStatusWiseWorkVillage(String RegistrationID)
        {
            try
            {
                MyClass cla = new MyClass();
                DataTable dtLand = cla.GetDataTable("SELECT  TOP (1) L.VillageID, V.City_ID, V.ClustersMasterID, V.TalukaID, V.SubdivisionID FROM Tbl_M_RegistrationLand AS L INNER JOIN Tbl_M_VillageMaster AS V ON V.VillageID = L.VillageID WHERE (L.RegistrationID = " + RegistrationID.Trim() + ") AND (L.ParentLandID IS NULL) and L.isdeleted is null and L.VillageID <> 0 ORDER BY L.LandID");
                if (dtLand.Rows.Count > 0)
                {
                    for (int x = 0; x != dtLand.Rows.Count; x++)
                    {
                        String str = " UPDATE  Tbl_M_RegistrationDetails SET Work_City_ID=" + dtLand.Rows[x]["City_ID"].ToString() + ", Work_TalukaID=" + dtLand.Rows[x]["TalukaID"].ToString() + ", Work_VillageID=" + dtLand.Rows[x]["VillageID"].ToString() + " ";
                        if (dtLand.Rows[x]["ClustersMasterID"].ToString().Trim().Length > 0)
                        {
                            str += " , Work_ClustersMasterID=" + dtLand.Rows[x]["ClustersMasterID"].ToString() + "";
                        }
                        if (dtLand.Rows[x]["SubdivisionID"].ToString().Trim().Length > 0)
                        {
                            str += " , Work_SubdivisionID=" + dtLand.Rows[x]["SubdivisionID"].ToString().Trim() + "";
                        }
                        str += " WHERE (RegistrationID = " + RegistrationID.Trim() + ")";
                        cla.ExecuteCommand(str);
                    }
                }
            }
            catch { }


        }






        //---------------com

        public DataTable GetIfscCodeSearch(String Ifsccodesearch)
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT distinct  NameOFbank,BranchName,IFSCCode,RBIBankID  FROM  Tbl_M_RBIBankMaster where IsDeleted is null  and IFSCCode='" + Ifsccodesearch.Trim() + "' order by NameOFbank");
            else
                return cla.GetDataTable("SELECT distinct  NameOFbank,BranchName,IFSCCode,RBIBankID  FROM  Tbl_M_RBIBankMaster where IsDeleted is null  and IFSCCode='" + Ifsccodesearch.Trim() + "' order by NameOFbank");

        }

        public DataTable GetVCRMC(string vCode)
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT VCRMCID, VCRMCName  as VCRMCName FROM Tbl_M_VCRMC_LIST where IsDeleted is null and VillageCode=" + vCode + " order by VCRMCName");
            else
                return cla.GetDataTable("SELECT VCRMCID, VCRMCName  as VCRMCName FROM Tbl_M_VCRMC_LIST where IsDeleted is null and VillageCode=" + vCode + " order by VCRMCName");

        }



        //----------------------------------------------------------------------------------------------------



        public DataTable GetActivitycommunity(string sComponentID)
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT ActivityID, ActivityName  FROM  Tbl_M_ActivityMaster where IsDeleted is null and  ActivityID IN (select distinct ActivityID from Tbl_M_ActivityBeneficiary WHERE BeneficiaryTypesID =2 AND IsDeleted IS NULL) AND ComponentID = " + sComponentID + " order by ActivityName");
            else
                return cla.GetDataTable("SELECT ActivityNameMr as ActivityName, ActivityID  FROM  Tbl_M_ActivityMaster where IsDeleted is null  and  ActivityID IN (select distinct ActivityID from Tbl_M_ActivityBeneficiary WHERE BeneficiaryTypesID =2 AND IsDeleted IS NULL) AND ComponentID = " + sComponentID + " order by ActivityName");

        }

        //---------------------------------------------------------------------------------------------------------------



        public DataTable GetSubDevesionsVillageMaster(String City_ID)
        {
            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT DISTINCT SubdivisionID , Subdivision FROM Tbl_M_VillageMaster where City_ID=" + City_ID + " and IsDeleted is null and Subdivision is not NULL order by Subdivision ");
            else
                return cla.GetDataTable("SELECT DISTINCT SubdivisionID , Subdivision FROM Tbl_M_VillageMaster where City_ID=" + City_ID + " and IsDeleted is null and Subdivision is not NULL order by Subdivision ");
        }


        //-------------------------------------------------------------------------------------------------



        public DataTable GetTalukaMasterwithsubdivision(string subdivision)
        {

            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            if (Lang.Trim() == "en-IN")
                return cla.GetDataTable("SELECT TalukaID, Taluka FROM Tbl_M_TalukaMaster where TalukaID in (SELECT DISTINCT TalukaID FROM Tbl_M_VillageMaster where SubdivisionID=" + subdivision + " and IsDeleted is null and TalukaID is not NULL)");
            else
                return cla.GetDataTable("SELECT TalukaID, ISNULL(TalukaMr,Taluka) as Taluka FROM Tbl_M_TalukaMaster where TalukaID in (SELECT DISTINCT TalukaID FROM Tbl_M_VillageMaster where SubdivisionID=" + subdivision + "and IsDeleted is null and TalukaID is not NULL)");

        }

        //--------------------------------------------------------------------------------------------------------------------

        //public DataTable GetPocraVillageAsPerWorkAreaCommunity()
        //{

        //    String Lang = "en-IN";
        //    try
        //    {
        //        if (System.Web.HttpContext.Current.Session["Lang"] != null)
        //        {
        //            Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
        //        }
        //    }
        //    catch { }
        //    // return cla.GetDataTable("SELECT VillageID, VillageCode  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and VillageCode is not null and VillageCode<>'' order by VillageName");
        //    DataTable dt = new DataTable();



        //    if (Lang.Trim() == "en-IN")
        //        if (Lang.Trim() == "en-IN")
        //            dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V  where V.IsDeleted is null  AND V.UserInPocra is not null and VillageID not in (select DISTINCT VillageID from Tbl_M_RegistrationDetails WHERE  Isdeleted is null ) order by VillageName");
        //        else
        //            //dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName As V  FROM Tbl_M_VillageMaster where V.IsDeleted is null AND C.UserInPocra is not null  order by VillageName");
        //            dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')as VillageName FROM Tbl_M_VillageMaster As V  where V.IsDeleted is null  AND V.UserInPocra is not null  and VillageID not in (select DISTINCT VillageID from Tbl_M_RegistrationDetails WHERE  Isdeleted is null ) order by VillageName");


        //    return dt;
        //}

        public DataTable GetPocraVillageAsPerWorkAreaCommunity()
        {


            String Lang = "en-IN";
            try
            {
                if (System.Web.HttpContext.Current.Session["Lang"] != null)
                {
                    Lang = System.Web.HttpContext.Current.Session["Lang"].ToString();
                }
            }
            catch { }
            // return cla.GetDataTable("SELECT VillageID, VillageCode  as VillageName FROM Tbl_M_VillageMaster where IsDeleted is null and VillageCode is not null and VillageCode<>'' order by VillageName");
            DataTable dt = new DataTable();







            if (System.Web.HttpContext.Current.Session["LevelType_ID"] == null)
            {
                if (Lang.Trim() == "en-IN")
                    dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V  where V.IsDeleted is null  AND V.UserInPocra is not null and VillageID not in (select DISTINCT VillageID from Tbl_M_RegistrationDetails WHERE  Isdeleted is null and  BeneficiaryTypesID=99  ) order by VillageName desc");
                else
                    //dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName As V  FROM Tbl_M_VillageMaster where V.IsDeleted is null AND C.UserInPocra is not null  order by VillageName");
                    dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')as VillageName FROM Tbl_M_VillageMaster As V  where V.IsDeleted is null  AND V.UserInPocra is not null  and VillageID not in (select DISTINCT VillageID from Tbl_M_RegistrationDetails WHERE  Isdeleted is null and  BeneficiaryTypesID=99  ) order by VillageName desc");
            }
            else
            {
                if (System.Web.HttpContext.Current.Session["LevelType_ID"].ToString().Trim() == "5")
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V  where V.IsDeleted is null AND V.UserInPocra IS NOT NULL AND V.VillageID in (SELECT distinct VillageID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))  AND  VillageID not in (select DISTINCT VillageID from Tbl_M_RegistrationDetails WHERE  Isdeleted is null and  BeneficiaryTypesID=99  ) order by VillageName");
                    else
                        dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V  where V.IsDeleted is null AND V.UserInPocra IS NOT NULL  AND V.VillageID in (SELECT distinct VillageID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + ")) and VillageID not in (select DISTINCT VillageID from Tbl_M_RegistrationDetails WHERE  Isdeleted is null and  BeneficiaryTypesID=99  ) order by VillageName");
                }
                else if (System.Web.HttpContext.Current.Session["LevelType_ID"].ToString().Trim() == "8")
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V where V.IsDeleted is null  AND V.UserInPocra IS NOT NULL AND ClustersMasterID in (SELECT distinct ClustersMasterID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))  and VillageID not in (select DISTINCT VillageID from Tbl_M_RegistrationDetails WHERE  Isdeleted is null and  BeneficiaryTypesID=99  ) order by VillageName");
                    else
                        dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V where V.IsDeleted is null AND V.UserInPocra IS NOT NULL AND ClustersMasterID in (SELECT distinct ClustersMasterID FROM Tbl_M_LoginDetails_Child WHERE (IsDeleted IS NULL) AND (UserId =" + System.Web.HttpContext.Current.Session["UserId"].ToString() + "))  and VillageID not in (select DISTINCT VillageID from Tbl_M_RegistrationDetails WHERE  Isdeleted is null and  BeneficiaryTypesID=99  ) order by VillageName");

                }
                else
                {
                    if (Lang.Trim() == "en-IN")
                        dt = cla.GetDataTable("SELECT VillageID, VillageName + ' - ' + ISNULL(VillageCode,'')  as VillageName FROM Tbl_M_VillageMaster As V  where V.IsDeleted is null  AND V.UserInPocra is not null and VillageID not in (select DISTINCT VillageID from Tbl_M_RegistrationDetails WHERE  Isdeleted is null and  BeneficiaryTypesID=99  ) order by VillageName desc");
                    else
                        //dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')  as VillageName As V  FROM Tbl_M_VillageMaster where V.IsDeleted is null AND C.UserInPocra is not null  order by VillageName");
                        dt = cla.GetDataTable("SELECT VillageID, ISNULL(VillageNameMr,VillageName) + ' - ' + ISNULL(VillageCode,'')as VillageName FROM Tbl_M_VillageMaster As V  where V.IsDeleted is null  AND V.UserInPocra is not null  and VillageID not in (select DISTINCT VillageID from Tbl_M_RegistrationDetails WHERE  Isdeleted is null and  BeneficiaryTypesID=99  ) order by VillageName desc");

                }
            }

            return dt;
        }
        public static void CreateTalukaWiseStatusReport()
        {

            MyClass cla = new MyClass();
            cla.ExecuteCommand("Delete from Tbl_RPT_TalukaWiseDeskReport ");
            DataTable dt = cla.GetDataTable("SELECT distinct sd.Subdivisions, C.Taluka ,c.TalukaID FROM   Tbl_M_TalukaMaster C inner join Tbl_M_VillageMaster V on v.TalukaID=c.TalukaID inner join Tbl_M_Subdivision sd on sd.SubdivisionID=v.SubdivisionID WHERE C.Isdeleted is null and v.UserInPocra is not null and v.IsDeleted is null ");
            foreach (DataRow dr in dt.Rows)
            {
                String TalukaID = dr["TalukaID"].ToString();
                String Taluka = dr["Taluka"].ToString();
                String Subdivision = dr["Subdivisions"].ToString();


                String Desk1_Total_Received = cla.GetExecuteScalar("Select Count(ApplicationID) from Tbl_T_ApplicationDetails AP INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=AP.RegistrationID  where R.Isdeleted is null and Ap.IsDeleted is null and R.Work_TalukaID=" + TalukaID + " and R.BeneficiaryTypesID=1 AND Ap.ApprovalStageID>=2 and R.ApprovalStatus='Verified' ");
                String Desk1_Total_Approved = cla.GetExecuteScalar("Select Count(ApplicationID) from Tbl_T_ApplicationDetails AP INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=AP.RegistrationID  where R.Isdeleted is null and Ap.IsDeleted is null and R.Work_TalukaID=" + TalukaID + " and R.BeneficiaryTypesID=1 AND Ap.ApprovalStageID>2 and R.ApprovalStatus='Verified' ");
                String Desk1_Total_Pending = cla.GetExecuteScalar("Select Count(ApplicationID) from Tbl_T_ApplicationDetails AP INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=AP.RegistrationID  where R.Isdeleted is null and Ap.IsDeleted is null and R.Work_TalukaID=" + TalukaID + " and R.BeneficiaryTypesID=1 AND Ap.ApprovalStageID=2 and R.ApprovalStatus='Verified' ");

                String Desk2_Total_Received = cla.GetExecuteScalar("Select Count(ApplicationID) from Tbl_T_ApplicationDetails AP INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=AP.RegistrationID  where R.Isdeleted is null and Ap.IsDeleted is null and R.Work_TalukaID=" + TalukaID + " and R.BeneficiaryTypesID=1 AND Ap.ApprovalStageID>=3 and R.ApprovalStatus='Verified' ");
                String Desk2_Total_Approved = cla.GetExecuteScalar("Select Count(ApplicationID) from Tbl_T_ApplicationDetails AP INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=AP.RegistrationID  where R.Isdeleted is null and Ap.IsDeleted is null and R.Work_TalukaID=" + TalukaID + " and R.BeneficiaryTypesID=1 AND Ap.ApprovalStageID>3 and R.ApprovalStatus='Verified' ");
                String Desk2_Total_Pending = cla.GetExecuteScalar("Select Count(ApplicationID) from Tbl_T_ApplicationDetails AP INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=AP.RegistrationID  where R.Isdeleted is null and Ap.IsDeleted is null and R.Work_TalukaID=" + TalukaID + " and R.BeneficiaryTypesID=1 AND Ap.ApprovalStageID=3 and R.ApprovalStatus='Verified' ");

                String Desk3_Total_Received = cla.GetExecuteScalar("Select Count(ApplicationID) from Tbl_T_ApplicationDetails AP INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=AP.RegistrationID  where R.Isdeleted is null and Ap.IsDeleted is null and R.Work_TalukaID=" + TalukaID + " and R.BeneficiaryTypesID=1 AND Ap.ApprovalStageID>=4 and R.ApprovalStatus='Verified' ");
                String Desk3_Total_Approved = cla.GetExecuteScalar("Select Count(ApplicationID) from Tbl_T_ApplicationDetails AP INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=AP.RegistrationID  where R.Isdeleted is null and Ap.IsDeleted is null and R.Work_TalukaID=" + TalukaID + " and R.BeneficiaryTypesID=1 AND Ap.ApprovalStageID>4 and R.ApprovalStatus='Verified' ");
                String Desk3_Total_Pending = cla.GetExecuteScalar("Select Count(ApplicationID) from Tbl_T_ApplicationDetails AP INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=AP.RegistrationID  where R.Isdeleted is null and Ap.IsDeleted is null and R.Work_TalukaID=" + TalukaID + " and R.BeneficiaryTypesID=1 AND Ap.ApprovalStageID=4 and R.ApprovalStatus='Verified' ");


                String Desk4_Total_Received = cla.GetExecuteScalar("Select Count(WorkReportID) from Tbl_T_Application_WorkReport as W INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=W.RegistrationID where W.Isdeleted is null  and R.Isdeleted is null and R.ApprovalStatus='Verified' AND W.ApprovalStageID>=6  and r.Work_TalukaID=" + TalukaID + "");
                String Desk4_Total_Approved = cla.GetExecuteScalar("Select Count(WorkReportID) from Tbl_T_Application_WorkReport as W INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=W.RegistrationID where W.Isdeleted is null  and R.Isdeleted is null and R.ApprovalStatus='Verified' AND W.ApprovalStageID>6  and r.Work_TalukaID=" + TalukaID + "");
                String Desk4_Total_Pending = cla.GetExecuteScalar("Select Count(WorkReportID) from Tbl_T_Application_WorkReport as W INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=W.RegistrationID where W.Isdeleted is null  and R.Isdeleted is null and R.ApprovalStatus='Verified' AND W.ApprovalStageID=6  and r.Work_TalukaID=" + TalukaID + "");


                String Desk5_Total_Received = cla.GetExecuteScalar("Select Count(WorkReportID) from Tbl_T_Application_WorkReport as W INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=W.RegistrationID where W.Isdeleted is null  and R.Isdeleted is null and R.ApprovalStatus='Verified' AND W.ApprovalStageID>=7  and r.Work_TalukaID=" + TalukaID + "");
                String Desk5_Total_Approved = cla.GetExecuteScalar("Select Count(WorkReportID) from Tbl_T_Application_WorkReport as W INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=W.RegistrationID where W.Isdeleted is null  and R.Isdeleted is null and R.ApprovalStatus='Verified' AND W.ApprovalStageID>7  and r.Work_TalukaID=" + TalukaID + "");
                String Desk5_Total_Pending = cla.GetExecuteScalar("Select Count(WorkReportID) from Tbl_T_Application_WorkReport as W INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=W.RegistrationID where W.Isdeleted is null  and R.Isdeleted is null and R.ApprovalStatus='Verified' AND W.ApprovalStageID=7  and r.Work_TalukaID=" + TalukaID + "");


                String Desk6_Total_Received = cla.GetExecuteScalar("Select Count(WorkReportID) from Tbl_T_Application_WorkReport as W INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=W.RegistrationID where W.Isdeleted is null  and R.Isdeleted is null and R.ApprovalStatus='Verified' AND W.ApprovalStageID>=8  and r.Work_TalukaID=" + TalukaID + "");
                String Desk6_Total_Approved = cla.GetExecuteScalar("Select Count(WorkReportID) from Tbl_T_Application_WorkReport as W INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=W.RegistrationID where W.Isdeleted is null  and R.Isdeleted is null and R.ApprovalStatus='Verified' AND W.ApprovalStageID>8  and r.Work_TalukaID=" + TalukaID + "");
                String Desk6_Total_Pending = cla.GetExecuteScalar("Select Count(WorkReportID) from Tbl_T_Application_WorkReport as W INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=W.RegistrationID where W.Isdeleted is null  and R.Isdeleted is null and R.ApprovalStatus='Verified' AND W.ApprovalStageID=8  and r.Work_TalukaID=" + TalukaID + "");

                String Desk7_Total_Received = cla.GetExecuteScalar("Select Count(WorkReportID) from Tbl_T_Application_WorkReport as W INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=W.RegistrationID where W.Isdeleted is null  and R.Isdeleted is null and R.ApprovalStatus='Verified' AND W.ApprovalStageID>=9  and r.Work_TalukaID=" + TalukaID + "");
                String Desk7_Total_Approved = cla.GetExecuteScalar("Select Count(WorkReportID) from Tbl_T_Application_WorkReport as W INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=W.RegistrationID where W.Isdeleted is null  and R.Isdeleted is null and R.ApprovalStatus='Verified' AND W.ApprovalStageID>9  and r.Work_TalukaID=" + TalukaID + "");
                String Desk7_Total_Pending = cla.GetExecuteScalar("Select Count(WorkReportID) from Tbl_T_Application_WorkReport as W INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=W.RegistrationID where W.Isdeleted is null  and R.Isdeleted is null and R.ApprovalStatus='Verified' AND W.ApprovalStageID=9  and r.Work_TalukaID=" + TalukaID + "");

                String str = " INSERT INTO Tbl_RPT_TalukaWiseDeskReport (TalukaID ,Taluka ,Subdivision  ,Desk1_Total_Received ,Desk1_Total_Approved";
                str += " , Desk1_Total_Pending, Desk2_Total_Received, Desk2_Total_Approved, Desk2_Total_Pending, Desk3_Total_Received, Desk3_Total_Approved";
                str += " , Desk3_Total_Pending, Desk4_Total_Received, Desk4_Total_Approved, Desk4_Total_Pending, Desk5_Total_Received, Desk5_Total_Approved";
                str += " , Desk5_Total_Pending, Desk6_Total_Received, Desk6_Total_Approved, Desk6_Total_Pending, Desk7_Total_Received, Desk7_Total_Approved, Desk7_Total_Pending)";
                str += "  VALUES (" + TalukaID + ", '" + Taluka + "', '" + Subdivision + "', " + Desk1_Total_Received + ", " + Desk1_Total_Approved + "";
                str += " , " + Desk1_Total_Pending + ", " + Desk2_Total_Received + ", " + Desk2_Total_Approved + ", " + Desk2_Total_Pending + ", " + Desk3_Total_Received + ", " + Desk3_Total_Approved + "";
                str += " , " + Desk3_Total_Pending + "," + Desk4_Total_Received + ", " + Desk4_Total_Approved + ", " + Desk4_Total_Pending + ", " + Desk5_Total_Received + ", " + Desk5_Total_Approved + "";
                str += "  ,  " + Desk5_Total_Pending + ", " + Desk6_Total_Received + ", " + Desk6_Total_Approved + ", " + Desk6_Total_Pending + ", " + Desk7_Total_Received + ", " + Desk7_Total_Approved + ", " + Desk7_Total_Pending + ")";
                cla.ExecuteCommand(str);
            }

        }


        public static void CreateVillageWiseStatusReport()
        {

            MyClass cla = new MyClass();
            cla.ExecuteCommand("Delete from Tbl_RPT_TalukaWiseDeskReport ");
            DataTable dt = cla.GetDataTable("SELECT distinct sd.Subdivisions, C.Taluka ,c.TalukaID , V.VillageCode, V.VillageName,V.VillageID FROM   Tbl_M_TalukaMaster C inner join Tbl_M_VillageMaster V on v.TalukaID=c.TalukaID inner join Tbl_M_Subdivision sd on sd.SubdivisionID=v.SubdivisionID WHERE C.Isdeleted is null and v.UserInPocra is not null and v.IsDeleted is null ");
            foreach (DataRow dr in dt.Rows)
            {
                String TalukaID = dr["TalukaID"].ToString();
                String Taluka = dr["Taluka"].ToString();
                String Subdivision = dr["Subdivisions"].ToString();
                String VillageCode = dr["VillageCode"].ToString();
                String VillageName = dr["VillageName"].ToString();
                String VillageID = dr["VillageID"].ToString();


                String Desk1_Total_Received = cla.GetExecuteScalar("Select Count(ApplicationID) from Tbl_T_ApplicationDetails AP INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=AP.RegistrationID  where R.Isdeleted is null and Ap.IsDeleted is null and R.Work_VillageID=" + VillageID + " and R.BeneficiaryTypesID=1 AND Ap.ApprovalStageID>=2 and R.ApprovalStatus='Verified' ");
                String Desk1_Total_Approved = cla.GetExecuteScalar("Select Count(ApplicationID) from Tbl_T_ApplicationDetails AP INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=AP.RegistrationID  where R.Isdeleted is null and Ap.IsDeleted is null and R.Work_VillageID=" + VillageID + " and R.BeneficiaryTypesID=1 AND Ap.ApprovalStageID>2 and R.ApprovalStatus='Verified' ");
                String Desk1_Total_Pending = cla.GetExecuteScalar("Select Count(ApplicationID) from Tbl_T_ApplicationDetails AP INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=AP.RegistrationID  where R.Isdeleted is null and Ap.IsDeleted is null and R.Work_VillageID=" + VillageID + " and R.BeneficiaryTypesID=1 AND Ap.ApprovalStageID=2 and R.ApprovalStatus='Verified' ");

                String Desk2_Total_Received = cla.GetExecuteScalar("Select Count(ApplicationID) from Tbl_T_ApplicationDetails AP INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=AP.RegistrationID  where R.Isdeleted is null and Ap.IsDeleted is null and R.Work_VillageID=" + VillageID + " and R.BeneficiaryTypesID=1 AND Ap.ApprovalStageID>=3 and R.ApprovalStatus='Verified' ");
                String Desk2_Total_Approved = cla.GetExecuteScalar("Select Count(ApplicationID) from Tbl_T_ApplicationDetails AP INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=AP.RegistrationID  where R.Isdeleted is null and Ap.IsDeleted is null and R.Work_VillageID=" + VillageID + " and R.BeneficiaryTypesID=1 AND Ap.ApprovalStageID>3 and R.ApprovalStatus='Verified' ");
                String Desk2_Total_Pending = cla.GetExecuteScalar("Select Count(ApplicationID) from Tbl_T_ApplicationDetails AP INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=AP.RegistrationID  where R.Isdeleted is null and Ap.IsDeleted is null and R.Work_VillageID=" + VillageID + " and R.BeneficiaryTypesID=1 AND Ap.ApprovalStageID=3 and R.ApprovalStatus='Verified' ");

                String Desk3_Total_Received = cla.GetExecuteScalar("Select Count(ApplicationID) from Tbl_T_ApplicationDetails AP INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=AP.RegistrationID  where R.Isdeleted is null and Ap.IsDeleted is null and R.Work_VillageID=" + VillageID + " and R.BeneficiaryTypesID=1 AND Ap.ApprovalStageID>=4 and R.ApprovalStatus='Verified' ");
                String Desk3_Total_Approved = cla.GetExecuteScalar("Select Count(ApplicationID) from Tbl_T_ApplicationDetails AP INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=AP.RegistrationID  where R.Isdeleted is null and Ap.IsDeleted is null and R.Work_VillageID=" + VillageID + " and R.BeneficiaryTypesID=1 AND Ap.ApprovalStageID>4 and R.ApprovalStatus='Verified' ");
                String Desk3_Total_Pending = cla.GetExecuteScalar("Select Count(ApplicationID) from Tbl_T_ApplicationDetails AP INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=AP.RegistrationID  where R.Isdeleted is null and Ap.IsDeleted is null and R.Work_VillageID=" + VillageID + " and R.BeneficiaryTypesID=1 AND Ap.ApprovalStageID=4 and R.ApprovalStatus='Verified' ");


                String Desk4_Total_Received = cla.GetExecuteScalar("Select Count(WorkReportID) from Tbl_T_Application_WorkReport as W INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=W.RegistrationID where W.Isdeleted is null  and R.Isdeleted is null and R.ApprovalStatus='Verified' AND W.ApprovalStageID>=6  and r.Work_VillageID=" + VillageID + "");
                String Desk4_Total_Approved = cla.GetExecuteScalar("Select Count(WorkReportID) from Tbl_T_Application_WorkReport as W INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=W.RegistrationID where W.Isdeleted is null  and R.Isdeleted is null and R.ApprovalStatus='Verified' AND W.ApprovalStageID>6  and r.Work_VillageID=" + VillageID + "");
                String Desk4_Total_Pending = cla.GetExecuteScalar("Select Count(WorkReportID) from Tbl_T_Application_WorkReport as W INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=W.RegistrationID where W.Isdeleted is null  and R.Isdeleted is null and R.ApprovalStatus='Verified' AND W.ApprovalStageID=6  and r.Work_VillageID=" + VillageID + "");


                String Desk5_Total_Received = cla.GetExecuteScalar("Select Count(WorkReportID) from Tbl_T_Application_WorkReport as W INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=W.RegistrationID where W.Isdeleted is null  and R.Isdeleted is null and R.ApprovalStatus='Verified' AND W.ApprovalStageID>=7  and r.Work_VillageID=" + VillageID + "");
                String Desk5_Total_Approved = cla.GetExecuteScalar("Select Count(WorkReportID) from Tbl_T_Application_WorkReport as W INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=W.RegistrationID where W.Isdeleted is null  and R.Isdeleted is null and R.ApprovalStatus='Verified' AND W.ApprovalStageID>7  and r.Work_VillageID=" + VillageID + "");
                String Desk5_Total_Pending = cla.GetExecuteScalar("Select Count(WorkReportID) from Tbl_T_Application_WorkReport as W INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=W.RegistrationID where W.Isdeleted is null  and R.Isdeleted is null and R.ApprovalStatus='Verified' AND W.ApprovalStageID=7  and r.Work_VillageID=" + VillageID + "");


                String Desk6_Total_Received = cla.GetExecuteScalar("Select Count(WorkReportID) from Tbl_T_Application_WorkReport as W INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=W.RegistrationID where W.Isdeleted is null  and R.Isdeleted is null and R.ApprovalStatus='Verified' AND W.ApprovalStageID>=8  and r.Work_VillageID=" + VillageID + "");
                String Desk6_Total_Approved = cla.GetExecuteScalar("Select Count(WorkReportID) from Tbl_T_Application_WorkReport as W INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=W.RegistrationID where W.Isdeleted is null  and R.Isdeleted is null and R.ApprovalStatus='Verified' AND W.ApprovalStageID>8  and r.Work_VillageID=" + VillageID + "");
                String Desk6_Total_Pending = cla.GetExecuteScalar("Select Count(WorkReportID) from Tbl_T_Application_WorkReport as W INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=W.RegistrationID where W.Isdeleted is null  and R.Isdeleted is null and R.ApprovalStatus='Verified' AND W.ApprovalStageID=8  and r.Work_VillageID=" + VillageID + "");

                String Desk7_Total_Received = cla.GetExecuteScalar("Select Count(WorkReportID) from Tbl_T_Application_WorkReport as W INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=W.RegistrationID where W.Isdeleted is null  and R.Isdeleted is null and R.ApprovalStatus='Verified' AND W.ApprovalStageID>=9  and r.Work_VillageID=" + VillageID + "");
                String Desk7_Total_Approved = cla.GetExecuteScalar("Select Count(WorkReportID) from Tbl_T_Application_WorkReport as W INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=W.RegistrationID where W.Isdeleted is null  and R.Isdeleted is null and R.ApprovalStatus='Verified' AND W.ApprovalStageID>9  and r.Work_VillageID=" + VillageID + "");
                String Desk7_Total_Pending = cla.GetExecuteScalar("Select Count(WorkReportID) from Tbl_T_Application_WorkReport as W INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=W.RegistrationID where W.Isdeleted is null  and R.Isdeleted is null and R.ApprovalStatus='Verified' AND W.ApprovalStageID=9  and r.Work_VillageID=" + VillageID + "");

                String str = " INSERT INTO Tbl_RPT_TalukaWiseDeskReport (TalukaID ,Taluka ,Subdivision  ,Desk1_Total_Received ,Desk1_Total_Approved";
                str += " , Desk1_Total_Pending, Desk2_Total_Received, Desk2_Total_Approved, Desk2_Total_Pending, Desk3_Total_Received, Desk3_Total_Approved";
                str += " , Desk3_Total_Pending, Desk4_Total_Received, Desk4_Total_Approved, Desk4_Total_Pending, Desk5_Total_Received, Desk5_Total_Approved";
                str += " , Desk5_Total_Pending, Desk6_Total_Received, Desk6_Total_Approved, Desk6_Total_Pending, Desk7_Total_Received, Desk7_Total_Approved, Desk7_Total_Pending,VillageName,VillageCode,VillageID)";
                str += "  VALUES (" + TalukaID + ", '" + Taluka + "', '" + Subdivision + "', " + Desk1_Total_Received + ", " + Desk1_Total_Approved + "";
                str += " , " + Desk1_Total_Pending + ", " + Desk2_Total_Received + ", " + Desk2_Total_Approved + ", " + Desk2_Total_Pending + ", " + Desk3_Total_Received + ", " + Desk3_Total_Approved + "";
                str += " , " + Desk3_Total_Pending + "," + Desk4_Total_Received + ", " + Desk4_Total_Approved + ", " + Desk4_Total_Pending + ", " + Desk5_Total_Received + ", " + Desk5_Total_Approved + "";
                str += "  ,  " + Desk5_Total_Pending + ", " + Desk6_Total_Received + ", " + Desk6_Total_Approved + ", " + Desk6_Total_Pending + ", " + Desk7_Total_Received + ", " + Desk7_Total_Approved + ", " + Desk7_Total_Pending + ",'" + VillageName + "','" + VillageCode + "'," + VillageID + ")";
                cla.ExecuteCommand(str);
            }

        }


        public static void CreateCAPerformanceReport()
        {

            MyClass cla = new MyClass();

            DataTable dt = cla.GetDataTable("SELECT distinct sd.Subdivisions, C.Taluka ,c.TalukaID , V.VillageCode, V.VillageName,V.VillageID,a.FullName,d.Desig_Name,a.UserId, c.City_ID, v.VillageID FROM   Tbl_M_TalukaMaster C inner join Tbl_M_VillageMaster V on v.TalukaID=c.TalukaID inner join Tbl_M_Subdivision sd on sd.SubdivisionID=v.SubdivisionID inner join Tbl_M_LoginDetails_Child l on l.VillageID=v.VillageID inner join Tbl_M_LoginDetails a on a.UserId=l.UserId inner join Tbl_M_Designation d on d.Desig_ID=a.Desig_ID WHERE C.Isdeleted is null and v.UserInPocra is not null and v.IsDeleted is null and l.IsDeleted is null and a.IsDeleted is null and d.Desig_ID=12  order by c.City_ID, v.VillageID ");
            foreach (DataRow dr in dt.Rows)
            {
                String TalukaID = dr["TalukaID"].ToString();
                String Taluka = dr["Taluka"].ToString();
                String Subdivision = dr["Subdivisions"].ToString();
                String VillageCode = dr["VillageCode"].ToString();
                String VillageName = dr["VillageName"].ToString();
                String VillageID = dr["VillageID"].ToString();
                String UserId = dr["UserId"].ToString();

                String Desk1_Total_Received = cla.GetExecuteScalar("Select Count(ApplicationID) from Tbl_T_ApplicationDetails AP INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=AP.RegistrationID  where R.Isdeleted is null and Ap.IsDeleted is null and R.Work_VillageID=" + VillageID + " and R.BeneficiaryTypesID=1 AND Ap.ApprovalStageID>=2 and R.ApprovalStatus='Verified' ");
                String Desk1_Total_Approved = cla.GetExecuteScalar("Select Count(ApplicationID) from Tbl_T_ApplicationDetails AP INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=AP.RegistrationID  where R.Isdeleted is null and Ap.IsDeleted is null and R.Work_VillageID=" + VillageID + " and R.BeneficiaryTypesID=1 AND Ap.ApprovalStageID>2 and R.ApprovalStatus='Verified' ");
                String Desk1_Total_Pending = cla.GetExecuteScalar("Select Count(ApplicationID) from Tbl_T_ApplicationDetails AP INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=AP.RegistrationID  where R.Isdeleted is null and Ap.IsDeleted is null and R.Work_VillageID=" + VillageID + " and R.BeneficiaryTypesID=1 AND Ap.ApprovalStageID=2 and R.ApprovalStatus='Verified' ");

                String Desk2_Total_Received = cla.GetExecuteScalar("Select Count(ApplicationID) from Tbl_T_ApplicationDetails AP INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=AP.RegistrationID  where R.Isdeleted is null and Ap.IsDeleted is null and R.Work_VillageID=" + VillageID + " and R.BeneficiaryTypesID=1 AND Ap.ApprovalStageID>=3 and R.ApprovalStatus='Verified' ");
                String Desk2_Total_Approved = cla.GetExecuteScalar("Select Count(ApplicationID) from Tbl_T_ApplicationDetails AP INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=AP.RegistrationID  where R.Isdeleted is null and Ap.IsDeleted is null and R.Work_VillageID=" + VillageID + " and R.BeneficiaryTypesID=1 AND Ap.ApprovalStageID>3 and R.ApprovalStatus='Verified' ");
                String Desk2_Total_Pending = cla.GetExecuteScalar("Select Count(ApplicationID) from Tbl_T_ApplicationDetails AP INNER JOIN Tbl_M_RegistrationDetails R ON R.RegistrationID=AP.RegistrationID  where R.Isdeleted is null and Ap.IsDeleted is null and R.Work_VillageID=" + VillageID + " and R.BeneficiaryTypesID=1 AND Ap.ApprovalStageID=3 and R.ApprovalStatus='Verified' ");


            }

        }

        public static void CreateDeskWiseLiabilityDetailsAc()
        {

            MyClass cla = new MyClass();
            List<String> lst = new List<string>();
            cla.ExecuteByProcedure("CreateDeskWiseLiabilityDetailsAc", lst);


        }

    }

    public static class CoordinatesDistanceExtensions
    {
        public static double DistanceTo(this Coordinates baseCoordinates, Coordinates targetCoordinates)
        {
            return DistanceTo(baseCoordinates, targetCoordinates, UnitOfLength.Kilometers);
        }

        public static double DistanceTo(this Coordinates baseCoordinates, Coordinates targetCoordinates, UnitOfLength unitOfLength)
        {
            var baseRad = Math.PI * baseCoordinates.Latitude / 180;
            var targetRad = Math.PI * targetCoordinates.Latitude / 180;
            var theta = baseCoordinates.Longitude - targetCoordinates.Longitude;
            var thetaRad = Math.PI * theta / 180;

            double dist =
                Math.Sin(baseRad) * Math.Sin(targetRad) + Math.Cos(baseRad) *
                Math.Cos(targetRad) * Math.Cos(thetaRad);
            dist = Math.Acos(dist);

            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            return unitOfLength.ConvertFromMiles(dist);
        }
    }
    public class Coordinates
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public Coordinates(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
    public class UnitOfLength
    {
        public static UnitOfLength Kilometers = new UnitOfLength(1.609344);
        public static UnitOfLength NauticalMiles = new UnitOfLength(0.8684);
        public static UnitOfLength Miles = new UnitOfLength(1);

        private readonly double _fromMilesFactor;

        private UnitOfLength(double fromMilesFactor)
        {
            _fromMilesFactor = fromMilesFactor;
        }

        public double ConvertFromMiles(double input)
        {
            return input * _fromMilesFactor;
        }
    }
}
