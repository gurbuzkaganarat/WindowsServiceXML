using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceXML.Entity
{
    public class CountryInfo
    {

        [Key]
        public string sISOCode { get; set; }

        public string sName { get; set; }

        public string sCapitalCity { get; set; }

        public string sPhoneCode { get; set; }

        public string sContinentCode { get; set; }

        public string sCurrencyISOCode { get; set; }

        public string sCountryFlag { get; set; }

    }
}
