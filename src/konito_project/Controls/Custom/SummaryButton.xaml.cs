using konito_project.Model;
using System;
using System.Collections.Generic;
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

namespace konito_project.Controls.Custom
{
    public partial class SummaryButton : UserControl {

        public class Data {
            public string Header { get; set;  }
            public int TradingCount { get; set; }
            public AccountType AccountType { get; set; }
            public int TotalPrice { get; set; }
            public double TotalTax { get; set; }
            public double TotalAmount => TotalPrice - (TotalPrice * TotalTax);
        }

        private static readonly SolidColorBrush HoverColorBrush = new SolidColorBrush(Color.FromRgb(94, 189, 230));
        private static readonly SolidColorBrush DefaultColorBrush = new SolidColorBrush(Color.FromRgb(65, 177, 225));
        private static readonly SolidColorBrush WhiteColorBrush = new SolidColorBrush(Colors.White);

        public static readonly DependencyProperty ForegroundBrushProperty =
            DependencyProperty.Register(
                "ForegroundBrush",
                typeof(SolidColorBrush),
                typeof(SummaryButton),
                new PropertyMetadata(WhiteColorBrush)
            );

        public static readonly DependencyProperty BackgroundBrushProperty =
            DependencyProperty.Register(
                "BackgroundBrush",
                typeof(SolidColorBrush),
                typeof(SummaryButton),
                new PropertyMetadata(DefaultColorBrush)
            );

        public static readonly DependencyProperty SummaryDataProperty =
            DependencyProperty.Register(
                "SummaryData",
                typeof(Data),
                typeof(SummaryButton)
            );

        //xaml에서 커맨드 바인딩 가능하게끔 종속성 속성 정의 (외부에서 Command 주입 가능)
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                "Command",
                typeof(ICommand),
                typeof(SummaryButton)
            );

        /* 각 월에 해당되는 SummaryButton 클릭 시 어떤 월의 버튼을 클릭했는지를
         * VM에 알려주기 위해 View에서 전달할 파라미터를 저장하기 위해 만듦.
         * InitButton 메서드의 Command.Execute 에서 이 CommandParamter를 넘겨주는 것을 볼 수 있음.
        */
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register(
                "CommandParameter",
                typeof(object),
                typeof(SummaryButton)
            );

        #region DP
        public ICommand Command {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public SolidColorBrush ForegroundBrush {
            get { return (SolidColorBrush)GetValue(ForegroundBrushProperty); }
            set { SetValue(ForegroundBrushProperty, value); }
        }

        public SolidColorBrush BackgroundBrush {
            get { return (SolidColorBrush)GetValue(BackgroundBrushProperty); }
            set { SetValue(BackgroundBrushProperty, value); }
        }


        public Data SummaryData {
            get { return (Data)GetValue(SummaryDataProperty); }
            set { SetValue(SummaryDataProperty, value); }
        }
        #endregion

        public SummaryButton() {
            InitializeComponent();
            InitButton();
            this.root.DataContext = this;
        }

        //버블링 허용 필요
        private void InitButton() {

            uc.BorderBrush = HoverColorBrush;
            uc.BorderThickness = new Thickness(2);

            root.MouseEnter += (s, e) => { HandlingMouseMoveEvent(true); };
            root.MouseLeave += (s, e) => { HandlingMouseMoveEvent(false); };

            //종속성 속성으로 정의한 Command 사용
            root.PreviewMouseDown += (s, e) => { if (Command != null) { Command.Execute(CommandParameter); } };
        }

        //마우스 움직임에 따라 컨트롤 색상 변경 (종속성 속성에 의해 색상 변경 자동 반영됨)
        private void HandlingMouseMoveEvent(bool isEntered) {
            BackgroundBrush = (isEntered) ? HoverColorBrush : DefaultColorBrush;
            ForegroundBrush = (isEntered) ? WhiteColorBrush : WhiteColorBrush;
        }

    }

}
