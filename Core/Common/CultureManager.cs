using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CultureManager
    {
        public static CultureInfo CurrentCultureInfo =>CultureInfo.GetCultureInfo(CultureId);
        public static CultureInfo DefaultCultureInfo => CultureInfo.GetCultureInfo(DefaultCultureId??1065);
        public static CultureInfo ShCultureInfo => CultureInfo.GetCultureInfo(1065);
        private static int cultureId=0;

        public static int CultureId
        {
            get {
                if (cultureId == 0)
                    cultureId = 1065;
                return cultureId; }
            set { cultureId = value; }
        }
        public static int? DefaultCultureId { get; set; }

    }
}
