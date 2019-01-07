using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace konito_project.Controls.Custom {
    /// <summary>
    /// Interaction logic for WorkingCalendar.xaml
    /// </summary>
    public partial class WorkingCalendar : UserControl {
        public static readonly DependencyProperty DateProperty = DependencyProperty.RegisterAttached("Date", typeof(DateTime), typeof(WorkingCalendar), new PropertyMetadata(OnChangedProperty));
        private static string[] WEEK_DAYS = { "일", "월", "화", "수", "목", "금", "토" };

        public DateTime Date {
            get { return (DateTime) GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        
        
        private static void OnChangedProperty(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var workingCalendar = d as WorkingCalendar;
            var dateTime = (DateTime) e.NewValue;

            if (workingCalendar == null) {
                throw new ArgumentException();
            }

            workingCalendar.tbHeader.Text = $"{dateTime:yyyy년 M월}";
            workingCalendar.cmbYear.SelectedItem = dateTime.Year;
            workingCalendar.cmbMonth.SelectedItem = dateTime.Month;
            workingCalendar.RefreshCalendar();
        }

        public WorkingCalendar() {
            InitializeComponent();
            InitHeader();
            InitComoboboxes();
        }

        private void InitHeader() {


            for (int i = 0; i < 7; i++) {

            }

        }

        private void InitComoboboxes() {
            // Year
            for (int year = 2018; year <= DateTime.Now.Year; year++) {
                cmbYear.Items.Add(year);
            }

            for (int month = 1; month <= 12; month++) {
                cmbMonth.Items.Add(month);
            }
        }

        private void RefreshCalendar() {

        }

        private void DateChanged(object sender, SelectionChangedEventArgs e) {
            SetValue(DateProperty, new DateTime((int) cmbYear.SelectedItem, (int) cmbMonth.SelectedItem, 1));
        }
    }
}
