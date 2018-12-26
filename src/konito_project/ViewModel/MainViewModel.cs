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
using konito_project.View.Registry;

namespace konito_project.ViewModel {

    public class MainViewModel: ViewModelBase {

        public ICommand MoldDataRegisterCommand => new ActionCommand(() => new MoldDataRegistry().ShowDialog());
        public ICommand AccountRegisterCommand => new ActionCommand(() => new AccountRegistry().ShowDialog());
        public ICommand ClientRegisterCommand => new ActionCommand(() => new ClientRegistry().ShowDialog());
        public ICommand EmployeeRegisterCommand => new ActionCommand(() => new EmployeeRegistry().ShowDialog());

        public MainViewModel(): base() {}

        protected override void InitWorkbook() {
            ExcelManager.CreateAllWorkBooks();
        }
        
    }
}
