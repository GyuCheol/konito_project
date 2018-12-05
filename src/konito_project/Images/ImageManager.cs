using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace konito_project.Images {
    public static class ImageManager {
        public static readonly string IMG_DIR_NAME = "./img/";

        static ImageManager() {
            if(Directory.Exists(IMG_DIR_NAME) == false) {
                Directory.CreateDirectory(IMG_DIR_NAME);
            }
        }

        public static BitmapSource GetImage(int id) {

            string path = Directory.GetFiles(IMG_DIR_NAME, $"{id}.*").FirstOrDefault();

            if (path == null)
                throw new FileNotFoundException();

            return new BitmapImage(new Uri(path, UriKind.Relative));
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

        public static void AddImage(string path) {
            string fileExtension = Path.GetExtension(path);

            File.Copy(path, $"{IMG_DIR_NAME}{GetNewImageId()}{fileExtension}");
        }


    }
}
