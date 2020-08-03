using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Gateway.Api.ExtensionMethods
{
    public static class StringExtensionMethods
    {
        public static string MaskString(this string value)
        {

            if (value.Length == 16)
                return value.Substring(value.Length - 4).PadLeft(value.Length, '*');
            else
            {
                string newVal = "";                
                return newVal.PadLeft(value.Length, '*'); ;
            }




        }
    }
    
}
