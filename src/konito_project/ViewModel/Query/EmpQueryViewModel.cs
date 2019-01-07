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
    public class EmpQueryViewModel : ViewModelBase {
        private List<Employee> CacheEmployeeList;
        private WorkBookManager<Employee> workBook = ExcelManager.EmployeeWorkBook;
        
        public ObservableCollection<Employee> EmpList { get; set; }
        public string SearchKeyword { get; set; } = string.Empty;
        public ICommand SearchCommand => new ActionCommand(Search);
        public ICommand EditCommand => new ActionParamCommand<Employee>(EditEmployee);
        public ICommand RemoveCommand => new ActionParamCommand<Employee>(RemoveEmployee);

        public EmpQueryViewModel() : base() {
            CacheEmployeeList = workBook.GetAllRecords().OrderBy(x => x.DisplayName).ToList();
            EmpList = new ObservableCollection<Employee>(CacheEmployeeList);
        }

        private void Search() {
            EmpList.Clear();

            // 이름, 직책, 연락처, 사번
            var query = from emp in CacheEmployeeList
                        where emp.DisplayName.Contains(SearchKeyword) ||
                              emp.Phone.Contains(SearchKeyword) ||
                              emp.EmployeeCode.Contains(SearchKeyword)
                        select emp;

            foreach (var emp in query) {
                EmpList.Add(emp);
            }
        }

        private void EditEmployee(Employee employee) {
            new EmployeeRegistry(new EmployeeRegistryViewModel(employee)).ShowDialog();
        }

        private void RemoveEmployee(Employee employee) {
            if (MessageBox.Show($"'{employee.DisplayName}'님을 삭제하시겠습니까?", "확인", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) {
                return;
            }

            CacheEmployeeList.Remove(employee);
            EmpList.Remove(employee);
            workBook.RemoveRecordById(employee.Id);
        }

    }
}
