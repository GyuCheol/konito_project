using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Excel {
    public class EmployeeWorkBook {
        public const string WORKBOOK_NAME = "./db/임직원_정보.xlsx";
        public const string SHEET_NAME = "data";

        public static readonly string[] COLUMNS = {
            "구분",
            "계정명"
        };

    }
}
