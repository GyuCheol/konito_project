using konito_project.Images;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konito_project.Tests.ImageAPI {

    [TestClass]
    public class ImageMngTest {

        [TestInitialize]
        public void InitTesting() {
            string[] files = Directory.GetFiles(ImageManager.IMG_DIR_NAME);

            foreach (string file in files) {
                File.Delete(file);
            }
        }

        [TestMethod]
        public void Test_GetImage() {
            ImageManager.AddImage("./Assets/icon1.png");
            ImageManager.AddImage("./Assets/icon2.jpg");
            ImageManager.AddImage("./Assets/icon1.png");
            ImageManager.AddImage("./Assets/icon2.jpg");

            ImageManager.GetImage(1);
            ImageManager.GetImage(2);
            ImageManager.GetImage(3);
            ImageManager.GetImage(4);
        }

    }

}
