using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace konito_project.Controls.Extension {

    public static class PlaceHolder {
        public static readonly DependencyProperty PlaceHolderProperty = DependencyProperty.RegisterAttached("Text", 
            typeof(string), typeof(PlaceHolder), new FrameworkPropertyMetadata(PropertyChangedCallback));

        public static string GetText(TextBox textBox) {
            if (textBox == null)
                throw new ArgumentException();

            return textBox.GetValue(PlaceHolderProperty) as string;
        }

        public static void SetText(TextBox textBox, string placeHoldText) {
            if (textBox == null)
                throw new ArgumentException();

            textBox.SetValue(PlaceHolderProperty, placeHoldText);
        }

        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var textBox = d as TextBox;

            if (textBox== null)
                throw new ArgumentException();

            //textBox.Template
            textBox.Template = new PlaceHolderTemplate(GetText(textBox));
        }

        private class PlaceHolderTemplate: ControlTemplate {
            private static readonly string SOURCE_NAME = "textSource";
            
            public PlaceHolderTemplate(string placeHoldText): base(typeof(TextBox)) {
                var elemFactory = new FrameworkElementFactory(typeof(Grid));
                
                elemFactory.AppendChild(CreateInputTextBox());
                elemFactory.AppendChild(CreateHoldPlaceTextBox(placeHoldText));
                
                VisualTree = elemFactory;
            }

            private FrameworkElementFactory CreateInputTextBox() {
                var inputTextBox = new FrameworkElementFactory(typeof(TextBox));
                var textBinding = new Binding(nameof(TextBox.Text));

                inputTextBox.SetValue(TextBox.BackgroundProperty, Brushes.Transparent);
                inputTextBox.SetValue(Grid.ZIndexProperty, 2);

                textBinding.Mode = BindingMode.TwoWay;
                textBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                textBinding.RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent);

                inputTextBox.SetBinding(TextBox.TextProperty, textBinding);

                return inputTextBox;
            }

            private FrameworkElementFactory CreateHoldPlaceTextBox(string placeHoldText) {
                var placeHoldTextBox = new FrameworkElementFactory(typeof(TextBox));

                placeHoldTextBox.SetValue(TextBox.TextProperty, placeHoldText);
                placeHoldTextBox.SetValue(Grid.ZIndexProperty, 1);

                var style = new Style(typeof(TextBox));
                var dataTrigger = new DataTrigger();
                var triggerBinding = new Binding(nameof(TextBox.Text));

                style.Setters.Add(new Setter(TextBox.BorderBrushProperty, Brushes.Transparent));
                style.Setters.Add(new Setter(TextBox.PaddingProperty, new Thickness(2, 2, 0, 0)));
                style.Setters.Add(new Setter(TextBox.ForegroundProperty, Brushes.Transparent));

                dataTrigger.Value = "";
                dataTrigger.Binding = triggerBinding;

                var fontBrush = placeHoldText[0] == '*' ? Brushes.Red : Brushes.Gray;

                dataTrigger.Setters.Add(new Setter(TextBox.ForegroundProperty, fontBrush));

                // x:Reference?
                // triggerBinding.Source = new Reference(SOURCE_NAME);
                triggerBinding.RelativeSource = RelativeSource.TemplatedParent;

                style.Triggers.Add(dataTrigger);

                placeHoldTextBox.SetValue(TextBox.StyleProperty, style);

                return placeHoldTextBox;
            }
        }

    }

}
