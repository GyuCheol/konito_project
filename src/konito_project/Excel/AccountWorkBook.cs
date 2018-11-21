using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Excel {
    public static class AccountWorkBook {
        public const string WORKBOOK_NAME = "./db/계정_정보.xlsx";

        public static readonly string[] COLUMNS = {
            "순번",
            "구분",
            "계정명",
            "등록일",
            "수정일"
        };

    }
}
