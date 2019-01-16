using konito_project.Commands;
using konito_project.Controls.Custom;
using konito_project.Model;
using konito_project.WorkBook;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace konito_project.ViewModel.ResultInput {

    public class SalesSummaryViewModel : ViewModelBase {

        public ICommand PrevCommand => new ActionParamCommand<string>(ChangeYearCommand);
        public ICommand NextCommand => new ActionParamCommand<string>(ChangeYearCommand);

        public ObservableCollection<SummaryButton.Data> Data { get; private set; } = new ObservableCollection<SummaryButton.Data>();
        public int Year {
            get => year;
            set {
                year = value;
                workBook.ChangeYear(value);
                RefreshSummaryData();
            }
        }

        private TradingWorkBook workBook;
        private int year;
        private AccountType accountType;

        public SalesSummaryViewModel(AccountType accountType) {
            workBook = new TradingWorkBook(DateTime.Now.Year, accountType, DateTime.Now.Month);

            this.accountType = accountType;
            Year = DateTime.Now.Year;
        }

        private void ChangeYearCommand(string param) {
            Year += int.Parse(param);
            NotifyChanged("Year");
        }

        private void RefreshSummaryData() {
            Data.Clear();

            for (int m = 1; m <= 12; m++) {
                workBook.ChangeMonth(m);
                IEnumerable<Trading> trs = workBook.GetAllRecords();
                int recordCount = trs.Count();

                Data.Add(new SummaryButton.Data() {
                    AccountType = accountType,
                    Header = $"{m}월",
                    TradingCount = recordCount,
                    TotalPrice = (recordCount > 0) ? trs.Sum(tr => tr.Price) : 0,
                    TotalTax = (recordCount > 0 ) ? trs.Average(tr => tr.Tax) : 0
                });
            }

        }

    }

}
