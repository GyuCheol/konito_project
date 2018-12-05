using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace konito_project.Controls.Extension {
    public static class DatePickerPlaceHolder {
        public static DependencyProperty TextProperty = DependencyProperty.RegisterAttached("Text",
            typeof(string), typeof(DatePickerPlaceHolder), new FrameworkPropertyMetadata(PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var dtp = d as DatePicker;

            if (dtp == null)
                throw new ArgumentException();

            dtp.Loaded += Dtp_Loaded;
        }

        private static void Dtp_Loaded(object sender, RoutedEventArgs e) {
            DatePicker datePicker = sender as DatePicker;

            if (datePicker == null)
                return;

            DatePickerTextBox datePickerTextBox = FindVisualChild<DatePickerTextBox>(datePicker);

            if (datePickerTextBox == null)
                return;

            var watermark = datePickerTextBox.Template.FindName("PART_Watermark", datePickerTextBox) as ContentControl;

            if (watermark == null)
                return;

            watermark.Content = GetText(datePicker);
        }

        private static T FindVisualChild<T>(DependencyObject depencencyObject) where T : DependencyObject {
            if (depencencyObject != null) {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depencencyObject); ++i) {
                    DependencyObject child = VisualTreeHelper.GetChild(depencencyObject, i);
                    T result = (child as T) ?? FindVisualChild<T>(child);
                    if (result != null)
                        return result;
                }
            }

            return null;
        }

        public static string GetText(DatePicker dtp) {
            if (dtp == null)
                throw new ArgumentException();

            return dtp.GetValue(TextProperty) as string;
        }

        public static void SetText(DatePicker dtp, string placeHoldText) {
            if (dtp == null)
                throw new ArgumentException();

            dtp.SetValue(TextProperty, placeHoldText);
        }

    }
}
