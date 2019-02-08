using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
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
    public partial class WorkingCalendar : UserControl, INotifyPropertyChanged {
        public static readonly DependencyProperty DateProperty = DependencyProperty.RegisterAttached("Date", typeof(DateTime), typeof(WorkingCalendar), new PropertyMetadata(OnDateChangedProperty));
        public static readonly DependencyProperty DataProperty = DependencyProperty.RegisterAttached("Data", typeof(INotifyCollectionChanged), typeof(WorkingCalendar), new PropertyMetadata(OnDataChangedProperty));

        private static readonly Thickness ContainerPadding = new Thickness(5);
        private static readonly SolidColorBrush HeaderBackground = new SolidColorBrush(Color.FromRgb(240, 240, 240));
        private static readonly SolidColorBrush ClickedBackground = new SolidColorBrush(Color.FromRgb(255, 160, 160));
        private static readonly SolidColorBrush ClickedHoverBackground = new SolidColorBrush(Color.FromRgb(255, 110, 110));
        private static string[] WEEK_DAYS = { "일", "월", "화", "수", "목", "금", "토" };
        private static Brush[] WEEK_DAY_BRUSHES = { Brushes.Red, Brushes.Black, Brushes.Black, Brushes.Black, Brushes.Black, Brushes.Black, Brushes.Blue };

        private static void OnDateChangedProperty(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var workingCalendar = d as WorkingCalendar;
            var dateTime = (DateTime)e.NewValue;

            if (workingCalendar == null) {
                throw new ArgumentException();
            }

            if (dateTime.Year < 2018 || dateTime.Year > DateTime.Now.Year) {
                workingCalendar.SetValue(DateProperty, e.OldValue);
                return;
            }

            workingCalendar.cmbYear.SelectedItem = dateTime.Year;
            workingCalendar.cmbMonth.SelectedItem = dateTime.Month;

            workingCalendar.RefreshCalendar();
        }

        private static void OnDataChangedProperty(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var workingCalendar = d as WorkingCalendar;
            var newValue = e.NewValue as INotifyCollectionChanged;

            if (e.OldValue is INotifyCollectionChanged oldValue) {
                oldValue.CollectionChanged -= CollectionChanged;
            }

            if (newValue == null) {
                throw new ArgumentNullException();
            }

            newValue.CollectionChanged += CollectionChanged;
        }

        private static void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            
        }

        public class CalendarData {
            public DateTime Date { get; set;  }
            public int Attandance { get; set; }
            public int Holiday { get; set; }
            public int OverTime { get; set; }
            public int WeekendWork { get; set; }
            public int NonInput { get; set; }
        }

        private class ContentElement {
            public Border Container { get; private set; } = new Border();
            public Grid Grid { get; private set; } = new Grid();
            public TextBlock[] TextBlockArray { get; private set; } = new TextBlock[6];
            public DateTime Date { get; private set; }

            private WorkingCalendar outer;
            private int y;
            private int x;

            public ContentElement(WorkingCalendar outer, DateTime date, int y, int x) {
                this.outer = outer;
                this.y = y;
                this.x = x;
                this.Date = date;

                InitContainer();
                InitGrid();
                InitTextBlocks();

                Container.Child = Grid;
                Grid.SetRow(Container, y);
                Grid.SetColumn(Container, x);
            }

            public void RefreshContent(CalendarData calendarData) {
                TextBlockArray[1].Text = $"출근 : {calendarData.Attandance}명";
                TextBlockArray[2].Text = $"야근 : {calendarData.OverTime}명";
                TextBlockArray[3].Text = $"특근 : {calendarData.WeekendWork}명";
                TextBlockArray[4].Text = $"휴가 : {calendarData.Holiday}명";
                TextBlockArray[5].Text = $"미입력 : {calendarData.NonInput}명";
            }

            private void InitGrid() {
                for (int i = 0; i < 6; i++) {
                    var rowDef = new RowDefinition();

                    rowDef.Height = new GridLength(1, GridUnitType.Star);
                    Grid.RowDefinitions.Add(rowDef);
                }
            }

            private void InitTextBlocks() {

                for (int i = 0; i < TextBlockArray.Length; i++) {
                    TextBlock textBlock = new TextBlock();

                    textBlock.HorizontalAlignment = HorizontalAlignment.Left;
                    textBlock.VerticalAlignment = VerticalAlignment.Center;
                    textBlock.Foreground = WEEK_DAY_BRUSHES[x];
                    Grid.SetRow(textBlock, i);

                    Grid.Children.Add(textBlock);

                    TextBlockArray[i] = textBlock;
                }

                TextBlockArray[0].Text = outer.map[y, x].Item1;

            }

            private void InitContainer() {
                Container.BorderThickness = outer.GetItemThickness(x, y);
                Container.BorderBrush = Brushes.LightGray;
                Container.Background = Brushes.White;
                Container.Padding = ContainerPadding;

                if (Date != DateTime.MinValue) {
                    // 현재 선택, 마우스 커서 Hover, 내부 컨텐츠 등.
                    Container.Cursor = Cursors.Hand;

                    Container.MouseEnter += (sender, e) => {
                        if (this != outer.lastestClickedContent) {
                            Container.Background = Brushes.LightGray;
                        } else {
                            Container.Background = ClickedHoverBackground;
                        }
                    };

                    Container.PreviewMouseDown += (sender, e) => {

                        if (outer.lastestClickedContent != null) {
                            outer.lastestClickedContent.Container.Background = Brushes.White;
                        }

                        outer.ClickDateContainer(Date);
                        Container.Background = ClickedHoverBackground;
                        outer.lastestClickedContent = this;
                    };

                    Container.MouseLeave += (sender, e) => {
                        if (this != outer.lastestClickedContent) {
                            Container.Background = Brushes.White;
                        } else {
                            Container.Background = ClickedBackground;
                        }
                    };
                }
            }
        }

        private class HeaderElement {
            public Border Container { get; private set; } = new Border();
            public TextBlock TextBlock { get; private set; } = new TextBlock();
            public string Text { get; private set; }

            public HeaderElement(WorkingCalendar outer, int x) {
                int y = 0;

                Text = WEEK_DAYS[x];

                Container.HorizontalAlignment = HorizontalAlignment.Stretch;
                Container.VerticalAlignment = VerticalAlignment.Stretch;
                Container.BorderThickness = outer.GetItemThickness(x, y);
                Container.BorderBrush = Brushes.LightGray;
                Container.Background = HeaderBackground;

                TextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                TextBlock.VerticalAlignment = VerticalAlignment.Center;
                TextBlock.Text = Text;
                TextBlock.FontWeight = FontWeights.Bold;
                TextBlock.Foreground = WEEK_DAY_BRUSHES[x];
                Container.Child = TextBlock;

                Grid.SetRow(Container, y);
                Grid.SetColumn(Container, x);
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public DateTime CurrentDate { get; private set; }

        public DateTime Date {
            get { return (DateTime) GetValue(DateProperty); }
            set { SetValue(DateProperty, value); }
        }

        public INotifyCollectionChanged Data {
            get { return GetValue(DataProperty) as INotifyCollectionChanged; }
            set { SetValue(DataProperty, value); }
        }

        private ContentElement lastestClickedContent;

        private Dictionary<DateTime, ContentElement> contentElementMap = new Dictionary<DateTime, ContentElement>();
        private Tuple<string, DateTime>[,] map = new Tuple<string, DateTime>[7, 7];

        public WorkingCalendar() {
            InitializeComponent();
            InitHeader();
            InitComoboboxes();
        }

        private void ClickDateContainer(DateTime date) {
            CurrentDate = date;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentDate)));
        }

        private Thickness GetItemThickness(int x, int y) {
            double left = 1;
            double right = 0;
            double top = 1;
            double bottom = 0;

            if (map[y, x].Item1 != string.Empty) {
                if (x == 6) {
                    right = 1;
                }

                if (y == 6) {
                    bottom = 1;
                }
            } else {

                if (y != 1 && map[y - 1, x].Item1 == string.Empty) {
                    top = 0;
                }

                if (y != 1 && (x == 0 || map[y, x - 1].Item1 == string.Empty)) {
                    left = 0;
                }

            }
            
            return new Thickness(left, top, right, bottom);
        }
        
        private void InitHeader() {
            for (int w = 0; w < 7; w++) {
                map[0, w] = new Tuple<string, DateTime>(WEEK_DAYS[w], DateTime.MinValue);
                gridCalendar.Children.Add(new HeaderElement(this, w).Container);
            }
        }

        private void RefreshCalendar() {

            gridCalendar.Children.RemoveRange(7, gridCalendar.Children.Count - 7);

            DateTime curDate = Date;
            int month = curDate.Month;
            int row = 1;

            contentElementMap.Clear();

            for (int y = 1; y < 7; y++) {
                for (int x = 0; x < 7; x++) {
                    map[y, x] = new Tuple<string, DateTime>(string.Empty, DateTime.MinValue);
                }
            }

            while (curDate.Month == month) {
                int w = (int) curDate.DayOfWeek;

                map[row, w] = new Tuple<string, DateTime>(curDate.Day.ToString(), curDate);

                if (curDate.DayOfWeek == DayOfWeek.Saturday) {
                    row++;
                }

                curDate = curDate.AddDays(1);
            }

            for (int y = 1; y < 7; y++) {
                for (int x = 0; x < 7; x++) {
                    DateTime date = map[y, x].Item2;
                    var content = new ContentElement(this, date, y, x);

                    gridCalendar.Children.Add(content.Container);

                    if (date != DateTime.MinValue) {
                        contentElementMap.Add(date, content);
                    }

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
