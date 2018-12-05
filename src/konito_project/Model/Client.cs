using konito_project.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Model {

    public class Client {
        public int Id { get; set; }

        public AccountType AccountType { get; set; }

        [Required("거래처명")]
        public string CompanyName { get; set; }

        [Required("대표자명")]
        public string OwnerName { get; set; }

        [Required("사업자번호")]
        public string CompanyCode { get; set; }

        public string ContactNumber { get; set; }

        public string FaxNumber { get; set; }

        public string Business { get; set; }

        public string BusinessClassification { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required("계좌은행")]
        public string BankName { get; set; }

        [Required("계좌번호")]
        public string BankAccountCode { get; set; }

        [Required("예금주명")]
        public string BankAccountOwnerName { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime LastestUpdatedTime { get; set; }

    }

}
