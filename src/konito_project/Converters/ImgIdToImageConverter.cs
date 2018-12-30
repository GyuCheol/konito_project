using konito_project.Images;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace konito_project.Converters {
    public class ImgIdToImageConverter : MarkupExtension, IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            int? imgId = value as int?;

            if (imgId == null)
                throw new ArgumentException();
            
            return ImageManager.GetImage(imgId.Value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
