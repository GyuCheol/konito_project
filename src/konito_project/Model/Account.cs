using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Model {

    public class Account {

        public enum Type {
            Purchase,
            Sales
        }

        public Type AccountType { get; set;  }

        public string Name { get; set; }

        public DateTime CreatedTime { get; set; }
        
        public DateTime LastestUpdatedTime { get; set; }
    }
}
