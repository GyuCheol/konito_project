using konito_project.Commands;
using konito_project.Model;
using konito_project.View.Registry;
using konito_project.WorkBook;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace konito_project.ViewModel.Query {

    public class ClientQueryViewModel : ViewModelBase {
        private List<Client> CacheClientList;
        private ClientWorkBook workBook = new ClientWorkBook();

        public ObservableCollection<Client> ClientList { get; private set; }
        public string SearchKeyword { get; set; } = string.Empty;
        public ICommand SearchCommand => new ActionCommand(Search);
        public ICommand EditCommand => new ActionParamCommand<Client>(EditClient);
        public ICommand RemoveCommand => new ActionParamCommand<Client>(RemoveClient);

        public ClientQueryViewModel() : base() {
            CacheClientList = workBook.GetAllRecords().ToList();
            ClientList = new ObservableCollection<Client>(CacheClientList);
        }

        private void Search() {

        }

        private void EditClient(Client client) {
            new ClientRegistry(client).ShowDialog();
        }

        private void RemoveClient(Client client) {
            if (MessageBox.Show($"'{client.CompanyName}'를 삭제하시겠습니까?", "확인", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                return;

            CacheClientList.Remove(client);
            ClientList.Remove(client);
            workBook.RemoveRecordById(client.Id);
        }

    }

}
