using konito_project.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Model {

    public class Employee {

        public string DisplayName => $"{LastName}{FirstName} ({Position})";

        [ExcelColumn(1, "순번")]
        public int Id { get; set; }

        [ExcelColumn(2, "이미지Id")]
        public int ImgId { get; set; }

        [ExcelColumn(3, "사원번호")]
        [Required("사원번호")]
        public string EmployeeCode { get; set; }

        [ExcelColumn(4, "계약구분")]
        public ContractType ContractType { get; set; }

        [ExcelColumn(5, "성")]
        [Required("성")]
        public string LastName { get; set; }

        [ExcelColumn(6, "이름")]
        [Required("이름")]
        public string FirstName { get; set; }

        [ExcelColumn(7, "직책")]
        [Required("직책")]
        public string Position { get; set; }

        [ExcelColumn(8, "연락처")]
        public string Phone { get; set; }

        [ExcelColumn(9, "주소")]
        public string Address1 { get; set; }

        [ExcelColumn(10, "상세주소")]
        public string Address2 { get; set; }

        [ExcelColumn(11, "급여")]
        [Required("급여")]
        public int Salary { get; set; }

        [ExcelColumn(12, "갑근세")]
        [Required("갑근세")]
        public int IncomeTax { get; set; }

        [ExcelColumn(13, "주민세")]
        [Required("주민세")]
        public int ResidenceTax { get; set; }

        [ExcelColumn(14, "국민연금")]
        [Required("국민연금")]
        public int NationalPension { get; set; }

        [ExcelColumn(15, "건강보험")]
        [Required("건강보험")]
        public int HealthInsurance { get; set; }

        [ExcelColumn(16, "고용보험")]
        [Required("고용보험")]
        public int EmploymentInsurance { get; set; }

        [ExcelColumn(17, "생년월일")]
        public DateTime BirthDate { get; set; }

        [ExcelColumn(18, "입사일")]
        public DateTime EnteredDate { get; set; }

        [ExcelColumn(19, "퇴사일")]
        public DateTime? ResignationDate { get; set; }

        [ExcelColumn(20, "생성일")]
        public DateTime CreatedTime { get; set; }

        [ExcelColumn(21, "최근 수정 시각")]
        public DateTime LastestUpdatedTime { get; set; }
    }


}
