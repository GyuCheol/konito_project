using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.WorkBook {
    public interface IWorkBookInitializer {
        void InitWorkBook();
        bool RemoveWorkBook();
    }
}
