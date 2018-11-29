using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Model {

    public class Employee {
        public int Id { get; set; }

        public int ImgId { get; set; }

        public string EmployeeCode { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Position { get; set; }

        public string Phone { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public int Salary { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime EnteredDate { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime LastestUpdatedTime { get; set; }
    }


}
