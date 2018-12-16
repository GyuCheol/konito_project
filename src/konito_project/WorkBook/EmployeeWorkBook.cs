using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using konito_project.Model;

namespace konito_project.WorkBook {
    public class EmployeeWorkBook: WorkBookBase<Employee> {
        public override string WorkBookPath => "./db/임직원_정보.xlsx";
        public override Type ModelType => typeof(Employee);
    }
}
