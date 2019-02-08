using konito_project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.WorkBook {
    public class WorkingTimeWorkBook : YearMonthlyWorkBook<WorkingTime> {

        protected override string DefaultPath => "./db/근태";

        public WorkingTimeWorkBook(int year, int month) : base(year, month) {
        }

    }
}
