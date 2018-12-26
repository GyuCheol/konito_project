using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace konito_project.Utils {
    public static class Dialogs {
        public static readonly OpenFileDialog OpenDialogImage_JpgPng;

        static Dialogs() {
            OpenDialogImage_JpgPng = new OpenFileDialog();
            OpenDialogImage_JpgPng.Multiselect = false;
            OpenDialogImage_JpgPng.Filter = "Image files|*.jpg;*.png";
        }

    }
}
