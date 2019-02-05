using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Model {
    public class TaxType {
        public const string TaxText = "과세";
        public const string TaxSmallText = "영세";
        public const string TaxFreeText = "비과세";

        public static readonly TaxType Tax = new TaxType(TaxText);
        public static readonly TaxType TaxSmall = new TaxType(TaxSmallText);
        public static readonly TaxType TaxFree = new TaxType(TaxFreeText);

        public string Text { get; private set; }

        private TaxType(string text) {
            Text = text;
        }

        public override string ToString() => Text;
    }
}
