using konito_project.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Model {

    public class Employee {
        public int Id { get; set; }

        public int ImgId { get; set; }

        [Required("사원번호")]
        public string EmployeeCode { get; set; }

        [Required("성")]
        public string LastName { get; set; }

        [Required("이름")]
        public string FirstName { get; set; }

        [Required("직책")]
        public string Position { get; set; }

        public string Phone { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required("급여")]
        public int Salary { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime EnteredDate { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime LastestUpdatedTime { get; set; }
    }


}
