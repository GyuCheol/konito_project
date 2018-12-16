using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Attributes {
    public class ExcelColumnAttribute: Attribute {

        public int Order { get; private set; }
        public string HeaderName { get; private set; }

        public ExcelColumnAttribute(int order, string headerName) {
            Order = order;
            HeaderName = headerName;
        }
    }
}
