using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotosLight.DataModel
{
    public class VideoViewerItem : ViewerItemBase
    {
        public override bool IsRotateSupported { get { return false; } }
        public override bool IsZoomSupported { get { return false; } }
        public VideoViewerItem(string title, string picture): 
            base(title, picture)
        { }
      
    }
}
