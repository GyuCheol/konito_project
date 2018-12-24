using konito_project.Commands;
using konito_project.Model;
using konito_project.WorkBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace konito_project.ViewModel {
    public class MoldRegistViewModel : ViewModelBase {
        private RegistMode CurrentMode;
        private MoldWorkBook workBook = new MoldWorkBook();

        public Mold CurrentMold { get; private set; }
        public ICommand SaveCommand => new ActionCommand(OnMoldDataSave);

        public MoldRegistViewModel(): base() {
            CurrentMode = RegistMode.Append;
            CurrentMold = new Mold();
        }

        public MoldRegistViewModel(string productCode): base() {
            CurrentMode = RegistMode.Edit;
            CurrentMold = workBook.GetDataByStrKeyOrNull(productCode);
        }

        protected override void InitWorkbook() {
            
        }

        private void OnMoldDataSave() {

        }
    }
}
