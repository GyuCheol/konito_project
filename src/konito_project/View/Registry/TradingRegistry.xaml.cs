using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace konito_project.View.Registry {
    /// <summary>
    /// Interaction logic for TradingRegisry.xaml
    /// </summary>
    public partial class TradingRegistry : MetroWindow {
        public TradingRegistry(object context) {
            InitializeComponent();
            this.DataContext = context;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
