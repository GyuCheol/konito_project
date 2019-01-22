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

        public DateTime Date {
            get { return (DateTime) GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        private string[,] map = new string[7, 7];

        private static void OnChangedProperty(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var workingCalendar = d as WorkingCalendar;
            var dateTime = (DateTime) e.NewValue;

            if (workingCalendar == null) {
                throw new ArgumentException();
            }

            if (dateTime.Year < 2018 || dateTime.Year > DateTime.Now.Year ) {
                workingCalendar.SetValue(DateProperty, e.OldValue);
                return;
            }

            workingCalendar.cmbYear.SelectedItem = dateTime.Year;
            workingCalendar.cmbMonth.SelectedItem = dateTime.Month;
            
            workingCalendar.RefreshCalendar();
        }

        public WorkingCalendar() {
            InitializeComponent();
            InitHeader();
            InitComoboboxes();
        }

        private Thickness GetItemThickness(int x, int y) {
            double left = 1;
            double right = 0;
            double top = 1;
            double bottom = 0;

            if (map[y, x] != string.Empty) {
                if (x == 6) {
                    right = 1;
                }

                if (y == 6) {
                    bottom = 1;
                }
            } else {

                if (y != 1 && map[y - 1, x] == string.Empty) {
                    top = 0;
                }

                if (y != 1 && (x == 0 || map[y, x - 1] == string.Empty)) {
                    left = 0;
                }

            }
            
            return new Thickness(left, top, right, bottom);
        }

        private FrameworkElement CreateWeekDayHeader(int x, int y) {
            var container = new Border();
            var textBlock = new TextBlock();

            container.HorizontalAlignment = HorizontalAlignment.Stretch;
            container.VerticalAlignment = VerticalAlignment.Stretch;
            container.BorderThickness = GetItemThickness(x, y);
            container.BorderBrush = Brushes.LightGray;

            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.VerticalAlignment= VerticalAlignment.Center;
            textBlock.Text = map[y, x];
            textBlock.Foreground = WEEK_DAY_BRUSHES[x];
            container.Child = textBlock;

            Grid.SetRow(container, y);
            Grid.SetColumn(container, x);

            return container;
        }

        private void InitHeader() {
            for (int w = 0; w < 7; w++) {
                map[0, w] = WEEK_DAYS[w];
                gridCalendar.Children.Add(CreateWeekDayHeader(w, 0));
            }
        }

        private void RefreshCalendar() {

            gridCalendar.Children.RemoveRange(7, gridCalendar.Children.Count - 7);

            DateTime curDate = Date;
            int month = curDate.Month;
            int row = 1;

            for (int y = 1; y < 7; y++) {
                for (int x = 0; x < 7; x++) {
                    map[y, x] = string.Empty;
                }
            }

            while (curDate.Month == month) {
                int w = (int) curDate.DayOfWeek;

                map[row, w] = curDate.Day.ToString();

                if (curDate.DayOfWeek == DayOfWeek.Saturday) {
                    row++;
                }

                curDate = curDate.AddDays(1);
            }

            for (int y = 1; y < 7; y++) {
                for (int x = 0; x < 7; x++) {
                    gridCalendar.Children.Add(CreateWeekDayHeader(x, y));
                }
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

            cmbYear.SelectionChanged += DateChanged;
            cmbMonth.SelectionChanged += DateChanged;
        }

        private void DateChanged(object sender, SelectionChangedEventArgs e) {
            if (DateTime.TryParse($"{cmbYear.SelectedItem}-{cmbMonth.SelectedItem}-01", out DateTime date)) {
                SetValue(DateProperty, date);
            }
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
