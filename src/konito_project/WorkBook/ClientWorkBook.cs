using ClosedXML.Excel;
using konito_project.Exceptions;
using konito_project.Model;
using konito_project.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.WorkBook
{
    public class ClientWorkBook: WorkBookBase<Client> {
        public override string WorkBookPath => "./db/거래처_정보.xlsx";
        public override Type ModelType => typeof(Client);
    }
}
