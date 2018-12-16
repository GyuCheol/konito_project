using konito_project.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Model {

    public class Employee {
        [ExcelColumn(1, "순번")]
        public int Id { get; set; }

        [ExcelColumn(2, "이미지Id")]
        public int ImgId { get; set; }

        [ExcelColumn(3, "사원번호")]
        [Required("사원번호")]
        public string EmployeeCode { get; set; }

        [ExcelColumn(4, "성")]
        [Required("성")]
        public string LastName { get; set; }

        [ExcelColumn(5, "이름")]
        [Required("이름")]
        public string FirstName { get; set; }

        [ExcelColumn(6, "직책")]
        [Required("직책")]
        public string Position { get; set; }

        [ExcelColumn(7, "연락처")]
        public string Phone { get; set; }

        [ExcelColumn(8, "주소")]
        public string Address1 { get; set; }

        [ExcelColumn(9, "상세주소")]
        public string Address2 { get; set; }

        [ExcelColumn(10, "급여")]
        [Required("급여")]
        public int Salary { get; set; }

        [ExcelColumn(11, "생년월일")]
        public DateTime BirthDate { get; set; }

        [ExcelColumn(12, "입사일")]
        public DateTime EnteredDate { get; set; }

        [ExcelColumn(13, "생성일")]
        public DateTime CreatedTime { get; set; }

        [ExcelColumn(14, "최근 수정 시각")]
        public DateTime LastestUpdatedTime { get; set; }
    }


}
