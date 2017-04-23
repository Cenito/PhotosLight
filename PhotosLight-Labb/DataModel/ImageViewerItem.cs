using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace PhotosLight.DataModel
{
    public class ImageViewerItem : ViewerItemBase
    {
        public ImageViewerItem(StorageFile file) : 
            base(file?.Name)
        {
            LoadImage(file);
        }

        private async void LoadImage(StorageFile file)
        {
            ImageSource  = new BitmapImage();
            FileRandomAccessStream stream = (FileRandomAccessStream)await file.OpenAsync(FileAccessMode.Read);

            ImageSource.SetSource(stream);
        }

        public ImageViewerItem(string title, string picture) :
            base(title, picture)
        {
            ZoomFactor = 1.0;
        }

    }
}
