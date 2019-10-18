using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace GStore.WebUI.Controllers
{
    public class FormatHandler
    {

        /// <summary>
        /// Function to check phone number
        /// via take out all non-numeric value, then check the length
        /// finally, add () and - into phone number to adjust to format
        /// </summary>
        /// <returns></returns>
        public string PhoneCheck(string phone)
        {
            phone = "(" + phone.Substring(0, 3) + ")" + phone.Substring(3, 3)
                    + "-" + phone.Substring(6, 4);
            return phone;
        }
        /// <summary>
        /// Function to handle users' input on their name
        /// and fix its format
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string NameFormat(string name)
        {
            name = Regex.Replace(name, @"[^A-z]+", "");
            string outputName = name.Substring(0, 1).ToUpper()
                              + name.Substring(1, name.Length - 1).ToLower();

            return outputName;
        }
    }
}
