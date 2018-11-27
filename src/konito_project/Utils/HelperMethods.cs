using ClosedXML.Excel;
using konito_project.Exceptions;
using konito_project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Utils {

    public static class HelperMethods {
        private static readonly string[,] CLIENT_PROPERTY_LIST = {
            { nameof(Client.CompanyCode), "사업자번호" },
            { nameof(Client.CompanyName), "거래처명" },
            { nameof(Client.OwnerName), "대표자명" },
            { nameof(Client.BankAccountCode), "계좌번호" },
            { nameof(Client.BankAccountOwnerName), "계좌 예금주명" },
            { nameof(Client.BankName), "주계좌 은행명" },
            //{ nameof(Client.Address1), "주소" },
            //{ nameof(Client.Address2), "상세 주소" },
            //{ nameof(Client.Business), "업종" },
            //{ nameof(Client.BusinessClassification), "종목" },
            //{ nameof(Client.ContactNumber), "전화번호" },
            //{ nameof(Client.FaxNumber), "팩스번호" },
        };

        public class ValidateErrorHandler {
            public bool HasError { get; private set; }
            public string ErrMsg { get; private set; }

            public ValidateErrorHandler(bool hasError, string errMsg) {
                HasError = hasError;
                ErrMsg = errMsg;
            }
        }

        public static ValidateErrorHandler Validate(this Client client) {
            var clientType = typeof(Client);

            for (int i = 0; i < CLIENT_PROPERTY_LIST.GetLength(0); i++) {
                var value = clientType.GetProperty(CLIENT_PROPERTY_LIST[i,0]).GetValue(client) as string;

                if(String.IsNullOrEmpty(value)) 
                    return new ValidateErrorHandler(true, CLIENT_PROPERTY_LIST[i, 1] + "을(를) 입력해주세요.");
            }
            return new ValidateErrorHandler(false, string.Empty);
        }

        public static Account CreateAccount(AccountType type, String text) {
            DateTime now = DateTime.Now;

            return new Account() {
                AccountType = type,
                Name = text
            };
        }

    }
}
