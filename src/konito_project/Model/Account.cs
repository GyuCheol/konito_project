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

        public int Id { get; set; }

        public Type AccountType { get; set;  }

        public string Name { get; set; }

        public DateTime CreatedTime { get; set; }
        
        public DateTime UpdatedTime { get; set; }
    }
}
