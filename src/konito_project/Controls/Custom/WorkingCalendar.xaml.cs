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
        private static Brush[] WEEK_DAY_BRUSHES = { Brushes.Red, Brushes.Black, Brushes.Black, Brushes.Black, Brushes.Black, Brushes.Black, Brushes.Blue };
        private static readonly Thickness[] BORDER_WIDTH_LIST = {
            new Thickness(1, 1, 0, 1),
            new Thickness(1, 1, 0, 1),
            new Thickness(1, 1, 0, 1),
            new Thickness(1, 1, 0, 1),
            new Thickness(1, 1, 0, 1),
            new Thickness(1, 1, 0, 1),
            new Thickness(1)
        };

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

        private FrameworkElement CreateWeekDayHeader(int week, string text) {
            var container = new Border();
            var textBlock = new TextBlock();

            container.HorizontalAlignment = HorizontalAlignment.Stretch;
            container.VerticalAlignment = VerticalAlignment.Stretch;
            container.BorderThickness = BORDER_WIDTH_LIST[week];
            container.BorderBrush = Brushes.LightGray;

            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.VerticalAlignment= VerticalAlignment.Center;
            textBlock.Text = text;
            textBlock.Foreground = WEEK_DAY_BRUSHES[week];
            container.Child = textBlock;

            Grid.SetRow(container, 0);
            Grid.SetColumn(container, week);

            return container;
        }

        private void InitHeader() {
            for (int w = 0; w < 7; w++) {
                gridHeader.Children.Add(CreateWeekDayHeader(w, WEEK_DAYS[w]));
            }
        }

        private void RefreshCalendar() {
            gridCalendar.Children.Clear();

            DateTime curDate = Date;
            int month = curDate.Month;
            int row = 0;

            while (curDate.Month == month) {
                int w = (int) curDate.DayOfWeek;

                //gridCalendar.Children.Add(CreateWeekDayHeader(row, w, curDate.Day.ToString()));

                if (curDate.DayOfWeek == DayOfWeek.Saturday) {
                    row++;
                }

                curDate = curDate.AddDays(1);
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

        private void DateChanged(object sender, SelectionChangedEventArgs e) {

            if (cmbYear.SelectedItem == null || cmbMonth.SelectedItem == null) {
                return;
            }

            SetValue(DateProperty, new DateTime((int) cmbYear.SelectedItem, (int) cmbMonth.SelectedItem, 1));
        }

        private void YearDown_Click(object sender, RoutedEventArgs e) {
            SetValue(DateProperty, Date.AddYears(-1));
        }

        private void YearUp_Click(object sender, RoutedEventArgs e) {
            SetValue(DateProperty, Date.AddYears(1));
        }

        private void MonthDown_Click(object sender, RoutedEventArgs e) {
            SetValue(DateProperty, Date.AddMonths(-1));
        }

        private void MonthUp_Click(object sender, RoutedEventArgs e) {
            SetValue(DateProperty, Date.AddMonths(1));
        }

    }
}
