using konito_project.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Model {

    public class Account {

        [ExcelColumn(1, "구분")]
        public AccountType AccountType { get; set;  }

        [ExcelColumn(2, "계정명")]
        public string Name { get; set; }
    }
}
