using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParamedicMedicosPrestaciones.Helpers
{
    public class Helpers
    {
        public string formatDate(string date)
        {
            if (date == "") return "";

            if (date.Contains('-'))
            {
                date = date.Replace("-", "");
            }
        
            string año = date.Substring(0, 4);
            string mes = date.Substring(4, 2);
            string dia = date.Substring(6, 2);

            return dia + "/" + mes + "/" + año;

        }
    }
}