using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Attributes {
    public class RequiredAttribute : Attribute {
        public string Name { get; private set; }

        public RequiredAttribute(string name) {
            Name = name;
        }
    }
}
