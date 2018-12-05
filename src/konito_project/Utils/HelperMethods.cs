using ClosedXML.Excel;
using konito_project.Attributes;
using konito_project.Exceptions;
using konito_project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Utils {

    public static class HelperMethods {

        public static ValidateErrorHandler Validate(object data) {
            var type = data.GetType();

            foreach (var prop in type.GetProperties()) {
                var reuqiredProps = prop.GetCustomAttributes(typeof(RequiredAttribute), true).OfType<RequiredAttribute>();

                foreach (var reqAttr in reuqiredProps) {
                    Type propType = prop.PropertyType;

                    if(propType.Name == typeof(string).Name) {
                        string value = prop.GetValue(data) as string;

                        if (String.IsNullOrWhiteSpace(value))
                            return new ValidateErrorHandler(true, reqAttr.Name);

                    } else if (propType.Name == typeof(int).Name) {
                        int value = (int) prop.GetValue(data);

                        if (value == 0)
                            return new ValidateErrorHandler(true, reqAttr.Name);
                    }
                }
            }

            return new ValidateErrorHandler(false, string.Empty);
        }

        public static ValidateErrorHandler ClientValidate(this Client client) => Validate(client);
        public static ValidateErrorHandler EmpValidate(this Employee emp) => Validate(emp);

        public static Account CreateAccount(AccountType type, String text) {
            DateTime now = DateTime.Now;

            return new Account() {
                AccountType = type,
                Name = text
            };
        }

    }
}
