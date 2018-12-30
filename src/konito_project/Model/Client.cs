using konito_project.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Model {

    public class Client {

        public string TypeName => AccountType == AccountType.Purchase ? "매입" : "매출";

        [ExcelColumn(1, "순번")]
        public int Id { get; set; }

        [ExcelColumn(2, "구분")]
        public AccountType AccountType { get; set; }

        [ExcelColumn(3, "거래처명")]
        [Required("거래처명")]
        public string CompanyName { get; set; }

        [ExcelColumn(4, "대표자명")]
        [Required("대표자명")]
        public string OwnerName { get; set; }

        [ExcelColumn(5, "사업자번호")]
        [Required("사업자번호")]
        public string CompanyCode { get; set; }

        [ExcelColumn(6, "연락처")]
        public string ContactNumber { get; set; }

        [ExcelColumn(7, "팩스")]
        public string FaxNumber { get; set; }

        [ExcelColumn(8, "업태")]
        public string Business { get; set; }

        [ExcelColumn(9, "종목")]
        public string BusinessClassification { get; set; }

        [ExcelColumn(10, "주소")]
        public string Address1 { get; set; }

        [ExcelColumn(11, "상세주소")]
        public string Address2 { get; set; }

        [ExcelColumn(12, "계좌은행")]
        [Required("계좌은행")]
        public string BankName { get; set; }

        [ExcelColumn(13, "계좌번호")]
        [Required("계좌번호")]
        public string BankAccountCode { get; set; }

        [ExcelColumn(14, "예금주명")]
        [Required("예금주명")]
        public string BankAccountOwnerName { get; set; }

        [ExcelColumn(15, "생성일")]
        public DateTime CreatedTime { get; set; }

        [ExcelColumn(16, "최근 수정 시각")]
        public DateTime LastestUpdatedTime { get; set; }

    }

}
