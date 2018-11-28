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
            "순번",
            "이미지ID",
            "사번",
            "성",
            "이름",
            "직책",
            "연락처",
            "주소",
            "급여",
            "생년월일",
            "입사일",
            "생성일",
            "최근 수정일"
        };

    }
}
