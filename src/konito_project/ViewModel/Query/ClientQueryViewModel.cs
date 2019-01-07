using konito_project.Commands;
using konito_project.Model;
using konito_project.View.Registry;
using konito_project.ViewModel.Registry;
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
        private WorkBookManager<Client> workBook = ExcelManager.ClientWorkBook;

        public ObservableCollection<Client> ClientList { get; private set; }
        public string SearchKeyword { get; set; } = string.Empty;
        public ICommand SearchCommand => new ActionCommand(Search);
        public ICommand EditCommand => new ActionParamCommand<Client>(EditClient);
        public ICommand RemoveCommand => new ActionParamCommand<Client>(RemoveClient);

        public ClientQueryViewModel() : base() {
            CacheClientList = workBook.GetAllRecords().OrderBy(x => x.CompanyName).ToList();
            ClientList = new ObservableCollection<Client>(CacheClientList);
        }

        private void Search() {
            ClientList.Clear();

            // 거래처명, 대표자명, 연락처
            var query = from client in CacheClientList
                        where client.CompanyName.Contains(SearchKeyword) ||
                              client.OwnerName.Contains(SearchKeyword) ||
                              client.ContactNumber.Contains(SearchKeyword)
                        select client;

            foreach (var client in query) {
                ClientList.Add(client);
            }

        }

        private void EditClient(Client client) {
            new ClientRegistry(new ClientRegistryViewModel(client)).ShowDialog();
        }

        private void RemoveClient(Client client) {
            if (MessageBox.Show($"'{client.CompanyName}'를 삭제하시겠습니까?", "확인", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) {
                return;
            }

            CacheClientList.Remove(client);
            ClientList.Remove(client);
            workBook.RemoveRecordById(client.Id);
        }

    }

}
