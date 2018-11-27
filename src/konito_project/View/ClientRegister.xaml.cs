using konito_project.ViewModel;
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

namespace konito_project.View
{
    /// <summary>
    /// Interaction logic for ClientRegister.xaml
    /// </summary>
    public partial class ClientRegister : Window
    {
        /// <summary>
        /// Appending Mode for new client
        /// </summary>
        public ClientRegister()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Editing Mode
        /// </summary>
        /// <param name="id">The client id for editing</param>
        public ClientRegister(int id) {
            InitializeComponent();
            DataContext = new ClientRegistViewModel(id);
        }

    }
}
