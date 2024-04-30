using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CommanClsLibrary.Repository.Classes.StaticClasses
{
    public static class CommonUtility
    {
        public static bool IsValidDataset(DataSet ds)
        {
            return ds.Tables.Count > 0 ? true : false;
        }

        public static bool IsValidDataTable(DataTable dt)
        {
            return dt.Rows.Count > 0 ? true : false;
        }

        public static string ConvertObjectToJson<T>(T obj)
        {
            string JsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

            return JsonStr;
        }

        public static T ConvertJsonToObject<T>(string JsonStr)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            dynamic item = serializer.Deserialize<T>(JsonStr);
            return item;
        }

        public static string ConvertDataTableToJsonString(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }

        public static T ConvertDataTableToList<T>(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            return ConvertJsonToObject<T>(serializer.Serialize(rows).ToString());
        }


        public static string Sanitize(this string input)
        {
            string output = input.Trim().Replace("'", "").Replace("+", " ").Replace("=", " ");

            return output;
        }


        public static string XMLToString(this System.Xml.XmlNode node, int indentation)
        {
            using (var sw = new System.IO.StringWriter())
            {
                using (var xw = new System.Xml.XmlTextWriter(sw))
                {
                    xw.Formatting = System.Xml.Formatting.Indented;
                    xw.Indentation = indentation;
                    node.WriteContentTo(xw);
                }
                return sw.ToString();
            }
        }


    }
}
