using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Utils {
    public class ValidateErrorHandler {
        public bool HasProblem { get; private set; }
        public string ErrMsg { get; private set; }

        public ValidateErrorHandler(bool hasError, string errMsg) {
            HasProblem = hasError;
            ErrMsg = errMsg;
        }
    }
}
