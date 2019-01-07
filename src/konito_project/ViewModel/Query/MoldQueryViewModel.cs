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

    public class MoldQueryViewModel : ViewModelBase {
        private WorkBookManager<Mold> workBook = ExcelManager.MoldWorkBook;
        private List<Mold> CacheMoldList;
        
        public ObservableCollection<Mold> MoldList { get; private set; }
        public string SearchKeyword { get; set; } = string.Empty;
        public ICommand SearchCommand => new ActionCommand(Search);
        public ICommand EditCommand => new ActionParamCommand<Mold>(EditItem);
        public ICommand RemoveCommand => new ActionParamCommand<Mold>(RemoveItem);


        public MoldQueryViewModel() : base() {
            CacheMoldList = workBook.GetAllRecords().ToList();
            MoldList = new ObservableCollection<Mold>(CacheMoldList);
        }

        private void Search() {

            MoldList.Clear();

            // 제품 번호, 수주처, 임직원 이름, 제품명
            var query = from mold in CacheMoldList
                        where mold.ProductCode.Contains(SearchKeyword) || 
                              mold.RequestedClientName.Contains(SearchKeyword) ||
                              mold.ArchitectEmployeeName.Contains(SearchKeyword) ||
                              mold.ProductName.Contains(SearchKeyword)
                        orderby mold.ProductCode
                        select mold;

            foreach (var mold in query) {
                MoldList.Add(mold);
            }
        }

        private void EditItem(Mold mold) {
            new MoldDataRegistry(new MoldRegistryViewModel(mold)).ShowDialog();
        }

        private void RemoveItem(Mold mold) {
            if (MessageBox.Show($"'{mold.ProductCode} {mold.ProductName}'를 삭제하시겠습니까?", "확인", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) {
                return;
            }

            CacheMoldList.Remove(mold);
            MoldList.Remove(mold);
            workBook.RemoveRecordByStrKey(mold.ProductCode);
        }
    }
}
