using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace PhotosLight.DataModel
{
    public class ImageViewerItem : ViewerItemBase
    {
        public ImageViewerItem(string title, string picture): 
            base(title, picture)
        { }
       
    }
}
