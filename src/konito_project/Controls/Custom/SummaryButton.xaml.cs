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

        private static readonly SolidColorBrush HoverColorBrush = new SolidColorBrush(Color.FromRgb(135, 199, 217));
        private static readonly SolidColorBrush DefaultColorBrush = new SolidColorBrush(Color.FromRgb(156, 209, 224));
        private static readonly SolidColorBrush WhiteColorBrush = new SolidColorBrush(Colors.White);

        #region DP
        //xaml에서 커맨드 바인딩 가능하게끔 종속성 속성 정의 (외부에서 Command 주입 가능)
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                "Command",
                typeof(ICommand),
                typeof(SummaryButton)
            );

        public ICommand Command {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty ForegroundBrushProperty =
            DependencyProperty.Register(
                "ForegroundBrush",
                typeof(SolidColorBrush),
                typeof(SummaryButton),
                new PropertyMetadata(WhiteColorBrush)
            );

        public SolidColorBrush ForegroundBrush {
            get { return (SolidColorBrush)GetValue(ForegroundBrushProperty); }
            set { SetValue(ForegroundBrushProperty, value); }
        }

        public static readonly DependencyProperty BackgroundBrushProperty =
            DependencyProperty.Register(
                "BackgroundBrush",
                typeof(SolidColorBrush),
                typeof(SummaryButton),
                new PropertyMetadata(DefaultColorBrush)
            );

        public SolidColorBrush BackgroundBrush {
            get { return (SolidColorBrush)GetValue(BackgroundBrushProperty); }
            set { SetValue(BackgroundBrushProperty, value); }
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
            root.MouseDown += (s, e) => { if (Command != null) { Command.Execute(null); } };

            //버블링 허용 (root Grid 위에 놓여진 컨트롤 클릭해도 root 까지 이벤트 이어나가도록)
            foreach (var o in LogicalTreeHelper.GetChildren(root)) {
                if (o is UIElement) {
                    ((UIElement)o).MouseDown += (s, e) => { e.Handled = false; };
                }
            }

        }

        //마우스 움직임에 따라 컨트롤 색상 변경 (종속성 속성에 의해 색상 변경 자동 반영됨)
        private void HandlingMouseMoveEvent(bool isEntered) {
            BackgroundBrush = (isEntered) ? HoverColorBrush : DefaultColorBrush;
            ForegroundBrush = (isEntered) ? WhiteColorBrush : WhiteColorBrush;
        }

    }

}
