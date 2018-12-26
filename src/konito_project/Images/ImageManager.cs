using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace konito_project.Images {
    public static class ImageManager {
        public static readonly ImageSource DEFAULT_IMAGE = new BitmapImage(new Uri("/konito_project;component/Assets/no_image.png", UriKind.RelativeOrAbsolute));
        public static readonly string IMG_DIR_NAME = "./img/";

        static ImageManager() {
            if(Directory.Exists(IMG_DIR_NAME) == false) {
                Directory.CreateDirectory(IMG_DIR_NAME);
            }
        }

        public static BitmapSource GetImage(int id) {
            string filePath = Directory.GetFiles(IMG_DIR_NAME, $"{id}.*").FirstOrDefault();

            if (filePath == null)
                throw new FileNotFoundException();

            var uri = new Uri("file://" + AppDomain.CurrentDomain.BaseDirectory + filePath);

            return new BitmapImage(uri);
        }

        private static int GetNewImageId() {

            var fileListByInt = Directory.GetFiles(IMG_DIR_NAME)
                .Select(x => Path.GetFileNameWithoutExtension(x))
                .Where(x => int.TryParse(x, out int n))
                .Select(x => int.Parse(x))
                .OrderByDescending(x => x);

            int? maxId = fileListByInt.FirstOrDefault();

            if (maxId == null) {
                return 1;
            } else {
                return maxId.Value + 1;
            }
        }

        public static int AddImage(string path) {
            string fileExtension = Path.GetExtension(path);
            int newImgId = GetNewImageId();

            File.Copy(path, $"{IMG_DIR_NAME}{newImgId}{fileExtension}");

            return newImgId;
        }


    }
}
