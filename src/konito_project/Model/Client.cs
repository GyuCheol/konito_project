using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Model {

    public class Client {
        public int Id { get; set; }
        public AccountType AccountType { get; set; }
        public String CompanyName { get; set; }
        public String OwnerName { get; set; }
        public String CompanyCode { get; set; }
        public String ContactNumber { get; set; }
        public String FaxNumber { get; set; }
        public String Business { get; set; }
        public String BusinessClassification { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public String BankName { get; set; }
        public String BankAccountCode { get; set; }
        public String BankAccountOwnerName { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastestUpdatedTime { get; set; }

    }

}
