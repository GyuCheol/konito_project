﻿using konito_project.Commands;
using konito_project.Controls.Custom;
using konito_project.Model;
using konito_project.View.Query;
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

    public class TradingQueryViewModel : ViewModelBase {

        public ICommand DateChangeCommand => new ActionParamCommand<object>(ChangeDate);
        public ICommand AddCommand => new ActionCommand(AddTrading);
        public ICommand EditCommand => new ActionParamCommand<Trading>(EditTrading);
        public ICommand RemoveCommand => new ActionParamCommand<Trading>(RemoveTrading);

        public ObservableCollection<Trading> TradingList { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }

        public int TotalTradingCount { get; set; }
        public int TotalPrice { get; set; }

        public string Date { get { return $"{Year}년 {Month}월"; } private set { } }

        private TradingWorkBook workbook;

        public TradingQueryViewModel(int year, int month, AccountType accountType) {
            workbook = new TradingWorkBook(year, accountType, month);
            SetDate(year, month);
        }

        private void AddTrading() {
            new TradingRegistry().ShowDialog();
        }

        private void EditTrading(Trading tr) {
            new TradingRegistry().ShowDialog();
        }

        private void RemoveTrading(Trading tr) {
            if (MessageBox.Show("선택하신 거래내역을 삭제하시겠습니까?", "확인", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) {
                return;
            }
            TradingList.Remove(tr);
            workbook.RemoveRecordById(tr.Id);
        }

        //Date 바꾸고 해당 연도, 월에 맞는 데이터 가져와 출력.
        private void SetDate(int year, int month) {
            this.Year = year;
            this.Month = month;

            workbook.ChangeYear(year);
            workbook.ChangeMonth(month);

            TradingList = new ObservableCollection<Trading>(workbook.GetAllRecords());
            TotalTradingCount = TradingList.Count();
            TotalPrice = 0;

            if (TotalTradingCount > 0) {
                double tax = TradingList.Average(tr => tr.Tax);
                int price = TradingList.Sum(tr => tr.Price);

                TotalPrice = (tax > 0) ? price - (int)(price * tax) : price;
            }

            NotifyChanged("Year");
            NotifyChanged("Month");
            NotifyChanged("TotalTradingCount");
            NotifyChanged("TotalPrice");
            NotifyChanged("Tradings");
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


    }

}
