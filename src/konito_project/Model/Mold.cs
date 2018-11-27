using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Model {

    public class Mold {
        public int Id { get; set; }
        public int ImageId { get; set; }
        public int RequestedClientId { get; set; }
        public int ArchitectEmployeeId { get; set; }

        public int CostForMolding { get; set; }
        public int CostForMoldingKRW { get; set; }

        public string SequenceId { get; set; }
        public string ModelCode { get; set; }
        public string ProductName { get; set; }
        public string ProductNumber { get; set; }

        public string Material { get; set; }
        public string Architecture { get; set; }
        public string CA { get; set; }

        public string ContractionRate { get; set; }
        public string MoldMachine { get; set; }

        public string ProductSize { get; set; }
        public string MoldingSize { get; set; }

        public string TopCoreMaterial { get; set; }
        public string BottomCoreMaterial { get; set; }

        public string Memo { get; set; }

        public DateTime DesignedDate { get; set; }
        public DateTime Prototype { get; set; }
        public DateTime LogsticalDate { get; set; }
        public DateTime UpdatedPrototype { get; set; }
    }

}
