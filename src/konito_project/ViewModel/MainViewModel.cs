using konito_project.Commands;
using konito_project.WorkBook;
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

        public ICommand MoldDataRegisterCommand => new ActionCommand(() => new MoldDataRegister().ShowDialog());
        public ICommand AccountRegisterCommand => new ActionCommand(() => new AccountRegister().ShowDialog());
        public ICommand ClientRegisterCommand => new ActionCommand(() => new ClientRegister().ShowDialog());
        public ICommand EmployeeRegisterCommand => new ActionCommand(() => new EmployeeRegister().ShowDialog());

        public MainViewModel(): base() {}

        protected override void InitWorkbook() {
            ExcelManager.CreateAllWorkBooks();
        }
        
    }
}
