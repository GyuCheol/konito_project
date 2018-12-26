using konito_project.ViewModel;
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
using static konito_project.ViewModel.ClientRegistViewModel;

namespace konito_project.View.Registry {
    /// <summary>
    /// Interaction logic for ClientRegister.xaml
    /// </summary>
    public partial class ClientRegistry : MetroWindow
    {
        public ClientRegistry()
        {
            InitializeComponent();
            DataContext = new ClientRegistViewModel();
        }

        public ClientRegistry(int clientId) {
            InitializeComponent();
            DataContext = new ClientRegistViewModel(clientId);
        }

    }
}
