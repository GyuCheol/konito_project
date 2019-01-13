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
using konito_project.ViewModel.Registry;
using konito_project.View.ResultInput;
using konito_project.ViewModel.ResultInput;
using konito_project.Model;

namespace konito_project.ViewModel {

    public class MainViewModel: ViewModelBase {

        public ICommand MoldDataRegisterCommand => new ActionCommand(() => new MoldDataRegistry(new MoldRegistryViewModel()).ShowDialog());
        public ICommand AccountRegisterCommand => new ActionCommand(() => new AccountRegistry().ShowDialog());
        public ICommand ClientRegisterCommand => new ActionCommand(() => new ClientRegistry(new ClientRegistryViewModel()).ShowDialog());
        public ICommand EmployeeRegisterCommand => new ActionCommand(() => new EmployeeRegistry(new EmployeeRegistryViewModel()).ShowDialog());
        public ICommand MoldQueryCommand => new ActionCommand(() => new MoldQuery().ShowDialog());
        public ICommand ClientQueryCommand => new ActionCommand(() => new ClientQuery().ShowDialog());
        public ICommand EmployeeQueryCommand => new ActionCommand(() => new EmployeeQuery().ShowDialog());
        public ICommand WorkingCalendarCommand => new ActionCommand(() => new WorkingTimeInput().ShowDialog());
        public ICommand PurchaseTaxCommand => new ActionCommand(() => new PurchaseSalesInput(new SalesSummaryViewModel(AccountType.Purchase)).ShowDialog());
        public MainViewModel(): base() {}

        protected override void InitWorkbook() {
            ExcelManager.CreateAllWorkBooks();

            //AccessKeyManager.Register(, )
        }
        
    }
}
