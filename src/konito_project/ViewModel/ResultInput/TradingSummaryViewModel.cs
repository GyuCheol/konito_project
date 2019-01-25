using konito_project.Commands;
using konito_project.Controls.Custom;
using konito_project.Model;
using konito_project.View.Query;
using konito_project.ViewModel.Query;
using konito_project.WorkBook;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace konito_project.ViewModel.ResultInput {

    public class TradingSummaryViewModel : ViewModelBase {

        public ICommand PrevCommand => new ActionParamCommand<string>(ChangeYearCommand);
        public ICommand NextCommand => new ActionParamCommand<string>(ChangeYearCommand);
        public ICommand ShowDetailCommand => new ActionParamCommand<string>(ShowMonthDetail);

        public ObservableCollection<SummaryButton.Data> Data { get; private set; } = new ObservableCollection<SummaryButton.Data>();
        public int Year {
            get => year;
            set {
                year = value;
                workbook.ChangeYear(value);
                RefreshSummaryData();
            }
        }

        public int TotalPrice { get; set; }
        public int TotalTradingCount { get; set; }

        private TradingWorkBook workbook;
        private int year;
        private AccountType accountType;

        public TradingSummaryViewModel(AccountType accountType) {
            workbook = new TradingWorkBook(DateTime.Now.Year, accountType, DateTime.Now.Month);

            this.accountType = accountType;
            Year = DateTime.Now.Year;
        }

        //SummaryButton의 CommandParameter(월)를 받아 처리
        private void ShowMonthDetail(string param) {
            int month = int.Parse(param);
            new TradingQuery(new TradingQueryViewModel(year, month, accountType)).ShowDialog();
            RefreshSummaryData();
        }

        private void ChangeYearCommand(string param) {
            Year += int.Parse(param);
            NotifyChanged("Year");
        }

        private void RefreshSummaryData() {
            Data.Clear();
            TotalPrice = 0;
            TotalTradingCount = 0;

            for (int m = 1; m <= 12; m++) {
                workbook.ChangeMonth(m);
                IEnumerable<Trading> trs = workbook.GetAllRecords();
                int recordCount = trs.Count();
                int price = (recordCount > 0) ? trs.Sum(tr => tr.Price) : 0;
                double tax = (recordCount > 0) ? trs.Average(tr => tr.Tax) : 0;
                Data.Add(new SummaryButton.Data() {
                    AccountType = accountType,
                    Header = $"{m}월",
                    TradingCount = recordCount,
                    TotalPrice = price,
                    TotalTax = tax
                });
                TotalTradingCount += recordCount;
                TotalPrice += (tax > 0) ? (int)(price - (price * tax)) : price;
            }

            NotifyChanged("TotalPrice");
            NotifyChanged("TotalTradingCount");
            NotifyChanged("Data");
        }

    }

}
