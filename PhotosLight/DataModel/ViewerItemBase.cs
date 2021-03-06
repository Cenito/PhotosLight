﻿using PhotosLight.Interfaces;
using PhotosLight.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace PhotosLight.DataModel
{
    public class ViewerItemBase : ViewModelBase, IViewerItem
    {
        private static Uri _baseUri = new Uri("ms-appx:///");
        private double _zoomFactor;
        private double _angle;
        public string Title { get; set; }
        public Uri Source { get; set; }

        public BitmapImage ImageSource { get; set; }

        public virtual bool IsRotateSupported { get { return true; } }
        public virtual bool IsZoomSupported { get { return true; } }
        public ViewerItemBase(string title)
        {
            Title = title;
            ZoomFactor = 1;
        }
        public ViewerItemBase(string title, string sourceName)
        {
            Title = title;
            Source = new Uri(_baseUri, sourceName);
            ImageSource = new BitmapImage(Source);
        }
        public double ZoomFactor
        {
            get { return _zoomFactor; }
            set { SetProperty(ref _zoomFactor, value); }
        }
        public double Angle
        {
            get { return _angle; }
            set { SetProperty(ref _angle, value); }
        }

        

    }
}
