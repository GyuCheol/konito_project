using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace konito_project.Converters {
    public class StrToIntConverter: MarkupExtension, IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            // String to Int
            string str = value as string;

            if (str == null)
                throw new ArgumentException();

            return int.Parse(value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            // Int to String
            int? integer = value as int?;

            if (integer == null)
                throw new ArgumentException();

            return integer.ToString();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
