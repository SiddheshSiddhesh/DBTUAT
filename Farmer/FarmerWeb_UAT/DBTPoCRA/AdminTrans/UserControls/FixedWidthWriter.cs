using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace DBTPoCRA.AdminTrans.UserControls
{
    public class FixedWidthWriter : StreamWriter
    {


        public int[] Widths { get; set; }

        public char NonDataCharacter { get; set; }

        public FixedWidthWriter(int[] widths, string path, bool append)
            : base(path, append)
        {
            NonDataCharacter = ' ';
            Widths = widths;
        }

        public FixedWidthWriter(int[] widths, string file)
    : this(widths, file, false) { }

        //public FixedWidthWriter(int[] widths, string file, bool append)
        //    : this(widths, file, append, Encoding.UTF8) { }


        private void WriteField(string datum, int width)
        {
            //datum = RemoveBom(datum);
            String a = datum.PadRight(width);
            Write(a);

            if(a.Length!=width)
            {

            }

            //char[] characters = datum.ToCharArray();
            //if (characters.Length > width)
            //{
            //    Write(characters, 0, width);
            //}
            //else
            //{
            //    Write(characters);
            //    Write(new string(NonDataCharacter, width - characters.Length));
            //}
        }


        public void WriteLine(string[] data)
        {
            if (data.Length > Widths.Length)
                throw new InvalidOperationException("The data has too many elements.");

            for (int i = 0; i < data.Length; i++)
            {
                WriteField(data[i], Widths[i]);
            }
            WriteLine("".Trim());
           
        }

        public static string RemoveBom(string p)
        {
            string BOMMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            if (p.StartsWith(BOMMarkUtf8))
            {
                if(BOMMarkUtf8.Length>1)
                p = p.Remove(0, BOMMarkUtf8.Length);
            }
            return p.Replace("\0", "");
        }

        public static List<string[]> ConvertTable(DataTable table)
        {
            return table.Rows.Cast<DataRow>()
            .Select(row => table.Columns.Cast<DataColumn>()
            .Select(col => Convert.ToString(row[col]).Trim())
            .ToArray())
            .ToList();
        }

    }
}