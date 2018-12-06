using konito_project.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace konito_project {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {

        public App() {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e) {

            switch (e.Exception.InnerException) {
                case CouldNotOpenWorkBookException err:
                    MessageBox.Show($"'{err.WorkBookName}' 엑셀을 종료하신 후 프로그램을 사용해야 합니다.", "에러", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }

        }
    }
}
