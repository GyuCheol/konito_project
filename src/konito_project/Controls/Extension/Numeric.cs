using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace konito_project.Controls.Extension {
    public static class Numeric {
        public static readonly DependencyProperty IsNumericProperty = DependencyProperty.RegisterAttached("IsNumeric",
            typeof(bool), typeof(Numeric),
            new FrameworkPropertyMetadata(OnMaskChanged));

        private static readonly Regex REGEXP = new Regex(@"[^0123456789]");

        private static void OnMaskChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e) {
            var textBox = dependencyObject as TextBox;
            bool isNumeric = (bool) e.NewValue;

            if(isNumeric) {
                applyNumericAttribte(textBox);
            } else {
                removeNumericAttribte(textBox);
            }
        }

        public static string GetIsNumericMask(TextBox textBox) {
            if (textBox == null) {
                throw new ArgumentNullException("textBox");
            }

            return textBox.GetValue(IsNumericProperty) as string;
        }

        public static void SetIsNumeric(TextBox textBox, bool isNumeric) {
            if (textBox == null) {
                throw new ArgumentNullException("textBox");
            }

            textBox.SetValue(IsNumericProperty, isNumeric);
        }

        private static void applyNumericAttribte(TextBox textBox) {
            textBox.PreviewKeyDown += TextBox_PreviewKeyDown;
        }

        private static void TextBox_PreviewKeyDown(object sender, KeyEventArgs e) {
            e.Handled = !canEnterKey(e.Key);
        }

        private static bool canEnterKey(Key key) {
            switch (key) {
                case Key.Back:
                case Key.Delete:
                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                case Key.NumPad0:
                case Key.NumPad1:
                case Key.NumPad2:
                case Key.NumPad3:
                case Key.NumPad4:
                case Key.NumPad5:
                case Key.NumPad6:
                case Key.NumPad7:
                case Key.NumPad8:
                case Key.NumPad9:
                    return true;
                default:
                    return false;
            }
        }

        private static void removeNumericAttribte(TextBox textBox) {
            textBox.PreviewKeyDown -= TextBox_PreviewKeyDown;
        }

    }
}
