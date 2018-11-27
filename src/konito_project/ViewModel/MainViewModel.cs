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

    public class MainViewModel: ViewModelBase {

        public ICommand MoldDataRegisterCommand { get; private set; }

        public ICommand AccountRegisterCommand { get; private set; }

        public ICommand ClientRegisterCommand { get; private set; }

        public ICommand EmployeeRegisterCommand { get; private set; }

        protected override void InitWorkbook() {
            ExcelManager.CreateAllWorkBooks();
        }

        protected override void InitCmd() {
            MoldDataRegisterCommand = new ActionCommand(() => new MoldDataRegister().ShowDialog());
            AccountRegisterCommand = new ActionCommand(() => new AccountRegister().ShowDialog());
            ClientRegisterCommand = new ActionCommand(() => new ClientRegister().ShowDialog());
            EmployeeRegisterCommand = new ActionCommand(() => new EmployeeRegister().ShowDialog());
        }
    }
}
