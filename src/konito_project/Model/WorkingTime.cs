using konito_project.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Model {
    public class WorkingTime {
        
        [ExcelColumn(1, "순번")]
        public int Id { get; set; }

        [ExcelColumn(2, "임직원Id")]
        public int EmpId { get; set; }

        [ExcelColumn(3, "사번")]
        public int EmpCode { get; set; }

        [ExcelColumn(4, "임직원명")]
        public string EmpName { get; set; }

        [ExcelColumn(5, "일자")]
        public DateTime Date { get; set; }

        [ExcelColumn(6, "출근시간")]
        public DateTime AttandanceTime { get; set; }

        [ExcelColumn(7, "퇴근시간")]
        public DateTime QuittingTime { get; set; }

    }
}
