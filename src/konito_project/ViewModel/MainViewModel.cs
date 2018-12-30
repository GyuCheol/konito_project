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
using konito_project.View.Registry;
using konito_project.View.Query;

namespace konito_project.ViewModel {

    public class MainViewModel: ViewModelBase {

        public ICommand MoldDataRegisterCommand => new ActionCommand(() => new MoldDataRegistry().ShowDialog());
        public ICommand AccountRegisterCommand => new ActionCommand(() => new AccountRegistry().ShowDialog());
        public ICommand ClientRegisterCommand => new ActionCommand(() => new ClientRegistry().ShowDialog());
        public ICommand EmployeeRegisterCommand => new ActionCommand(() => new EmployeeRegistry().ShowDialog());
        public ICommand MoldQueryCommand => new ActionCommand(() => new MoldQuery().ShowDialog());
        public ICommand ClientQueryCommand => new ActionCommand(() => new ClientQuery().ShowDialog());
        public ICommand EmployeeQueryCommand => new ActionCommand(() => new EmployeeQuery().ShowDialog());

        public MainViewModel(): base() {}

        protected override void InitWorkbook() {
            ExcelManager.CreateAllWorkBooks();

            //AccessKeyManager.Register(, )
        }
        
    }
}
