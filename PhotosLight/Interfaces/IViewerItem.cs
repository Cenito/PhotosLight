using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotosLight.Interfaces
{
    public interface IViewerItem
    {
        string Title { get; set; }
        Uri Source { get; set; }
        bool IsRotateSupported { get; }
        bool IsZoomSupported { get; }
    }
}
