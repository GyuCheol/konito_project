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

namespace konito_project.ViewModel {

    public class ClientRegistViewModel: ViewModelBase {
        private RegistMode CurrentMode;
        private ClientWorkBook workBook = new ClientWorkBook();

        public ICommand SaveCommand => new ActionCommand(ClickSaveCommand);
        public Client Client { get; private set; }
        public string ClientId => $"{Client?.Id ?? 0:00000}";
        public bool Purchase { get; set; } = true;
        public bool Sales { get; set; } = false;

        public ClientRegistViewModel(): base() {
            CurrentMode = RegistMode.Append;
            Client = new Client() {
                Id = workBook.GetNewRecordId()
            };
        }

        public ClientRegistViewModel(int clientId): base() {
            CurrentMode = RegistMode.Edit;
            // Find client data
            Client = workBook.GetDataByIdOrNull(clientId);

            if (Client == null)
                throw new NotFoundClientException();
        }
        
        private void ClickSaveCommand() {
            ValidateErrorHandler validate = Client.ValidateRequired();

            Client.AccountType = Purchase ? AccountType.Purchase : AccountType.Sales;

            if (validate.HasProblem) {
                MessageBox.Show($"{validate.ErrMsg}를(을) 입력해주세요!", "에러", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            switch (CurrentMode) {
                case RegistMode.Append:
                    workBook.AddRecord(Client);
                    MessageBox.Show("신규 거래처 정보가 등록되었습니다!", "확인", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case RegistMode.Edit:

                    MessageBox.Show("거래처 정보가 수정되었습니다!", "확인", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }

            UtilCommands.CloseCommand.Execute(null);
        }
    }

}
