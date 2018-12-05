using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace konito_project.WorkBook {
    public class EmployeeWorkBook: WorkBookBase<EmployeeWorkBook> {
        private static readonly string[] COLUMNS = {
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

        public override string WorkBookPath => "./db/임직원_정보.xlsx";
        public override string[] InitColumns => COLUMNS;

        protected override EmployeeWorkBook CovertToDataFromRow(IXLRow row) {
            throw new NotImplementedException();
        }

        protected override void InsertRow(IXLRow row, EmployeeWorkBook data) {
            throw new NotImplementedException();
        }
    }
}
