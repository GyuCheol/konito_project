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

        public static AccountType ConvertAccountTypeFromStr(this string str) {
            switch (str) {
                case AccountType.PurchaseText:
                    return AccountType.Purchase;
                case AccountType.SalesText:
                    return AccountType.Sales;
                default:
                    throw new ArgumentException();
            }
        }

        public static TaxType ConvertTaxTypeFromStr(this string str) {
            switch (str) {
                case TaxType.TaxText:
                    return TaxType.Tax;
                case TaxType.TaxSmallText:
                    return TaxType.TaxSmall;
                case TaxType.TaxFreeText:
                    return TaxType.TaxFree;
                default:
                    throw new ArgumentException();
            }
        }

        public static ValidateErrorHandler Validate(object data) {
            var type = data.GetType();

            var properties = from prop in type.GetProperties()
                             select new {
                                 Property = prop,
                                 Attr = prop.GetCustomAttributes(typeof(RequiredAttribute), true).OfType<RequiredAttribute>().FirstOrDefault()
                             } into item
                             where item.Attr != null
                             select item;

            foreach (var item in properties) {
                var prop = item.Property;
                var attr = item.Attr;
                Type propType = prop.PropertyType;

                if (propType == typeof(string)) {
                    string value = prop.GetValue(data) as string;

                    if (string.IsNullOrWhiteSpace(value)) {
                        return new ValidateErrorHandler(true, attr.Name);
                    }
                } else if (propType == typeof(int)) {
                    int value = (int)prop.GetValue(data);

                    if (value == 0) {
                        return new ValidateErrorHandler(true, attr.Name);
                    }
                }
            }

            return new ValidateErrorHandler(false, string.Empty);
        }

        public static ValidateErrorHandler ValidateRequired<T>(this T data) => Validate(data);

        public static Account CreateAccount(AccountType type, String text) {
            DateTime now = DateTime.Now;

            return new Account() {
                AccountType = type,
                Name = text
            };
        }

    }
}
