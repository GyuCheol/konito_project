using konito_project.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Model {

    public class Mold {
        [Required("제번")]
        [ExcelColumn(1, "제번")]
        public string ProductCode { get; set; }

        [ExcelColumn(2, "이미지ID")]
        public int ImgId { get; set; }

        [ExcelColumn(3, "수주처")]
        [Required("수주처")]
        public string RequestedClientName { get; set; }

        [ExcelColumn(4, "설계 임직원")]
        [Required("설계 임직원")]
        public string ArchitectEmployeeName { get; set; }

        [ExcelColumn(5, "금형비(외화)")]
        public int CostForMolding { get; set; }

        [ExcelColumn(6, "금형비(원화)")]
        public int CostForMoldingKRW { get; set; }

        [ExcelColumn(7, "수축율")]
        public string ShrinkRate { get; set; }

        [ExcelColumn(8, "모델코드")]
        public string ModelCode { get; set; }

        [ExcelColumn(9, "품명")]
        public string ProductName { get; set; }

        [ExcelColumn(10, "품번")]
        public string ProductNumber { get; set; }

        [ExcelColumn(11, "재질")]
        public string Material { get; set; }

        [ExcelColumn(12, "구조")]
        public string Architecture { get; set; }

        [ExcelColumn(13, "CA")]
        public string CA { get; set; }

        [ExcelColumn(14, "확정도")]
        public string ContractionRate { get; set; }

        [ExcelColumn(15, "사출기")]
        public string MoldMachine { get; set; }

        [ExcelColumn(16, "제품 크기")]
        public string ProductSize { get; set; }

        [ExcelColumn(17, "금형 크기")]
        public string MoldingSize { get; set; }

        [ExcelColumn(18, "상코어")]
        public string TopCore { get; set; }

        [ExcelColumn(19, "하코어")]
        public string BottomCore { get; set; }

        [ExcelColumn(20, "이슈")]
        public string Issue { get; set; }

        [ExcelColumn(21, "설계일")]
        public DateTime DesignedDate { get; set; }

        [ExcelColumn(22, "초품 일정")]
        public DateTime Prototype { get; set; }

        [ExcelColumn(23, "출항일")]
        public DateTime LogsticalDate { get; set; }

        [ExcelColumn(24, "수정 초품 일정")]
        public DateTime UpdatedPrototype { get; set; }

        [ExcelColumn(25, "생성시간")]
        public DateTime CreatedTime { get; set; }

        [ExcelColumn(26, "최근 수정 시간")]
        public DateTime LastestUpdatedTime { get; set; }
    }

}
