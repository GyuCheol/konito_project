﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Exceptions {

    public class WrongExcelFormatException: Exception {

        public WrongExcelFormatException(): base("Wrong excel format.") {

        }

    }
}
