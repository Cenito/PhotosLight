using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotosLight.Interfaces
{
    public interface IThumbnailService
    {
        ObservableCollection<IViewerItem> Thumbnails { get; set; }
        void SelectFolder();

    }
}
