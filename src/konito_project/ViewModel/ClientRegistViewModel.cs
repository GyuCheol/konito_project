﻿using konito_project.Commands;
using konito_project.Excel;
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

        public ICommand SaveCommand { get; private set; }

        private RegistMode CurrentMode;

        public Client Client { get; private set; }

        public string ClientId => $"{Client?.Id ?? 0:00000}";
        public bool Purchase { get; set; }
        public bool Sales { get; set; }

        public ClientRegistViewModel(): base() {
            CurrentMode = RegistMode.Append;
            Client = new Client() {
                Id = ClientWorkBook.GetNewClientId()
            };
        }

        public ClientRegistViewModel(int clientId) {
            InitCmd();
            CurrentMode = RegistMode.Edit;
            // Find client data
            Client = ClientWorkBook.GetClientByIdOrNull(clientId);

            if (Client == null)
                throw new NotFoundClientException();
        }

        private void ClickSaveCommand(Window w) {
            ValidateErrorHandler validate = Client.Validate();

            Client.AccountType = Purchase ? AccountType.Purchase : AccountType.Sales;

            if(validate.HasError) {
                MessageBox.Show(validate.ErrMsg, "에러", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            switch (CurrentMode) {
                case RegistMode.Append:
                    ClientWorkBook.AddClientRecord(Client);
                    MessageBox.Show("신규 거래처 정보가 등록되었습니다!", "확인", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
                case RegistMode.Edit:

                    MessageBox.Show("거래처 정보가 수정되었습니다!", "확인", MessageBoxButton.OK, MessageBoxImage.Information);
                    break;
            }

            w.Close();
        }

        protected override void InitWorkbook() {
            CurrentMode = RegistMode.Append;

            Client = new Client() {
                Id = ClientWorkBook.GetNewClientId()
            };
            Purchase = true;
            Sales = false;
        }

        protected override void InitCmd() {
            SaveCommand = new WindowHandlerCommand(ClickSaveCommand);
        }
    }

}
