using konito_project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.WorkBook {
    public class MoldWorkBook : WorkBookBase<Mold> {
        public override string WorkBookPath => "./db/금형_관리_대장.xlsx";
    }
}
