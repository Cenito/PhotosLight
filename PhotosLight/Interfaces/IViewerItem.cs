using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace PhotosLight.Interfaces
{
    public interface IViewerItem
    {
        string Title { get; set; }
        Uri Source { get; set; }
        BitmapImage ImageSource { get; set; }
        bool IsRotateSupported { get; }
        bool IsZoomSupported { get; }
        double ZoomFactor { get; set; }
        double Angle { get; set; }
    }
}
