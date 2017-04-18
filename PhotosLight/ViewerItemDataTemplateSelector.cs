using PhotosLight.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PhotosLight
{
    public class ViewerItemDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ImageItemTemplate { get; set; }
        public DataTemplate VideoItemTemplate { get; set; }
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is ImageViewerItem)
            {
                return ImageItemTemplate;
            }
            if (item is VideoViewerItem)
            {
                return VideoItemTemplate;
            }

            return base.SelectTemplateCore(item);
        }
    }
}
