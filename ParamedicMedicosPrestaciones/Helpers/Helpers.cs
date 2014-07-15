using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        public string StringEscape(string str)
        {
            return Regex.Replace(str, @"[\x00'""\b\n\r\t\cZ\\%_]",
                delegate(Match match)
                {
                    string v = match.Value;
                    switch (v)
                    {
                        case "\x00":            // ASCII NUL (0x00) character
                            return "\\0";
                        case "\b":              // BACKSPACE character
                            return "\\b";
                        case "\n":              // NEWLINE (linefeed) character
                            return "\\n";
                        case "\r":              // CARRIAGE RETURN character
                            return "\\r";
                        case "\t":              // TAB
                            return "\\t";
                        case "\u001A":          // Ctrl-Z
                            return "\\Z";
                        default:
                            return "\\" + v;
                    }
                });
        } 
    }
}