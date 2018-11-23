using konito_project.Classess;
using konito_project.Excel;
using konito_project.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static konito_project.ViewModel.ClientRegistViewModel;

namespace konito_project.ViewModel {

    public class MainViewModel {

        public ICommand MoldDataRegisterCommand { get; private set; }

        public ICommand AccountRegisterCommand { get; private set; }

        public ICommand ClientRegisterCommand { get; private set; }

        public ICommand EmployeeRegisterCommand { get; private set; }

        public MainViewModel() {
            MoldDataRegisterCommand = new ButtonCommand(ClickMoldDataRegister);
            AccountRegisterCommand = new ButtonCommand(ClickAccountRegister);
            ClientRegisterCommand = new ButtonCommand(ClickClientRegister);
            EmployeeRegisterCommand = new ButtonCommand(ClickEmployeeRegister);


            // In designer mode, not working
            if(DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;

            // Spreadsheet Init
            ExcelManager.CreateAllWorkBooks();
        }

        private void ClickMoldDataRegister() {
            new MoldDataRegister().ShowDialog();
        }

        private void ClickAccountRegister() {
            new AccountRegister().ShowDialog();
        }

        private void ClickClientRegister() {
            new ClientRegister().ShowDialog();
        }

        private void ClickEmployeeRegister() {

        }

    }
}
