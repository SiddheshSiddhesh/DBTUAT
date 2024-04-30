using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommanClsLibrary
{
    public static class ExtensionMethods
    {
        public static string RemoveAllAfterFirstSpace(this string Str)
        {

            int index = Str.IndexOf(" ");
            if (index >= 0)
                Str = Str.Substring(0, index);


            return Str;
        }


    }
}
