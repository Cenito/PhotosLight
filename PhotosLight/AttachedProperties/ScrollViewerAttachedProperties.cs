using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PhotosLight.AttachedProperties
{
    public static class ScrollViewerAttachedProperties
    {
        public static readonly DependencyProperty ZoomLevelroperty =
       DependencyProperty.RegisterAttached(
       "ZoomLevel", typeof(double), typeof(ScrollViewerAttachedProperties),
       new PropertyMetadata(1.0, OnZoomLevelChanged));     
        
       
        public static void SetZoomLevel(DependencyObject element, double value)
        {
            element.SetValue(ZoomLevelroperty, value);
        }

        public static double GetZoomLevel(DependencyObject element)
        {
            return (double)element.GetValue(ZoomLevelroperty);
        }

        private static void OnZoomLevelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = d as ScrollViewer;
            if (scrollViewer == null)
                return;

            scrollViewer.ChangeView(null, null, (float)Convert.ToDouble(e.NewValue));
        }
    }
}
