using konito_project.Commands;
using konito_project.Controls.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace konito_project.ViewModel.Query {

    public class TradingQueryViewModel : ViewModelBase {

        public ICommand DateChangeCommand => new ActionParamCommand<object>(ChangeDate);
        
        public int Year { get; set; }
        public int Month { get; set; }

        public TradingQueryViewModel(int year, int month) {
            SetDate(year, month);
        }

        private void ChangeDate(object param) {
            int increseMonth = (int)param;
            if (increseMonth > 0) {
                Month = (Month == 12) ? 1 : Month + 1;
                Year += 1;
            }
            else {
                Month = (Month == 1) ? 12 : Month - 1;
                Year -= 1;
            }
            SetDate(Year, Month);
        }
        
        //Date 바꾸고 해당 연도, 월에 맞는 데이터 가져와 출력.
        public void SetDate(int year, int month) {
            this.Year = year;
            this.Month = month;

        }

    }

}
