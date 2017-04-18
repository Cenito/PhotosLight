using PhotosLight.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotosLight.DataModel
{
    public class ViewerItemBase : IViewerItem
    {
        private static Uri _baseUri = new Uri("ms-appx:///");
        public string Title { get; set; }
        public Uri Source { get; set; }
        public ViewerItemBase(string title, string sourceName)
        {
            Title = title;
            Source = new Uri(_baseUri, sourceName);
        }
       
    }
}
