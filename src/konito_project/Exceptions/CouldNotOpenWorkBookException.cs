using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Exceptions {
    public class CouldNotOpenWorkBookException: Exception {
        public string WorkBookName { get; private set; }

        public CouldNotOpenWorkBookException(string workBook) {
            WorkBookName = workBook;
        }

    }
}
