using konito_project.Commands;
using konito_project.WorkBook;
using konito_project.Exceptions;
using konito_project.Model;
using konito_project.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static konito_project.Utils.HelperMethods;

namespace konito_project.ViewModel.Registry {

    public class ClientRegistryViewModel: ViewModelBase {
        private RegistMode CurrentMode;
        private WorkBookManager<Client> workBook = ExcelManager.ClientWorkBook;

        public ICommand SaveCommand => new ActionCommand(ClickSaveCommand);
        public Client CurrentClient { get; private set; }
        public string ClientId => $"{CurrentClient?.Id ?? 0:00000}";
        public bool Purchase { get; set; } = true;
        public bool Sales { get; set; } = false;

        public ClientRegistryViewModel(): base() {
            CurrentMode = RegistMode.Append;
            CurrentClient = new Client() {
                Id = workBook.GetNewRecordId()
            };
        }

        public ClientRegistryViewModel(Client client): base() {
            CurrentMode = RegistMode.Edit;
            // Find client data
            CurrentClient = client;

            if (CurrentClient.AccountType == AccountType.Purchase) {
                Purchase = true;
            } else {
                Sales = true;
            }
        }
        
        private void ClickSaveCommand() {
            ValidateErrorHandler validate = CurrentClient.ValidateRequired();

            CurrentClient.AccountType = Purchase ? AccountType.Purchase : AccountType.Sales;

            if (validate.HasProblem) {
                MessageBox.Show($"{validate.ErrMsg}를(을) 입력해주세요!", "에러", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            switch (CurrentMode) {
                case RegistMode.Append:
                    workBook.AddRecord(CurrentClient);
                    MessageBox.Show("신규 거래처 정보가 등록되었습니다!", "확인", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case RegistMode.Edit:
                    workBook.EditRecordById(CurrentClient, CurrentClient.Id);
                    MessageBox.Show("거래처 정보가 수정되었습니다!", "확인", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }

            UtilCommands.CloseCommand.Execute(null);
        }
    }

}
