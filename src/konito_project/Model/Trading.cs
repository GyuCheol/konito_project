using konito_project.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Model {
    public class Trading {


        [ExcelColumn(1, "순번")]
        public int Id { get; set; }

        [Required("거래일자")]
        [ExcelColumn(2, "거래일자")]
        public DateTime Date { get; set; }

        [Required("거래처명")]
        [ExcelColumn(3, "거래처명")]
        public string CompanyName { get; set; }

        [Required("품명")]
        [ExcelColumn(4, "품명")]
        public string ProductName { get; set; }

        [Required("공급가액")]
        [ExcelColumn(5, "공급가액")]
        public int Price { get; set; }

        [Required("세율")]
        [ExcelColumn(6, "세율")]
        public double Tax { get; set; }

        [Required("적요")]
        [ExcelColumn(7, "적요")]
        public AccountType AccountType { get; set; }

        [Required("계정")]
        [ExcelColumn(8, "계정")]
        public string Account { get; set; }

        [Required("구분")]
        [ExcelColumn(9, "구분")]
        public TaxType TaxType { get; set; }

        [ExcelColumn(10, "비고")]
        public string Memo { get; set; }

    }
}
