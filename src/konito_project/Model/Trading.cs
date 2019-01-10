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

        [ExcelColumn(2, "거래일자")]
        public DateTime Date { get; set; }

        [ExcelColumn(3, "거래처명")]
        public string CompanyName { get; set; }

        [ExcelColumn(4, "품명")]
        public string ProductName { get; set; }

        [ExcelColumn(5, "공급가액")]
        public int Price { get; set; }

        [ExcelColumn(6, "세율")]
        public double Tax { get; set; }

        [ExcelColumn(7, "적요")]
        public AccountType AccountType { get; set; }

        [ExcelColumn(8, "구분")]
        public TaxType TaxType { get; set; }

        [ExcelColumn(9, "비고")]
        public string Memo { get; set; }

    }
}
