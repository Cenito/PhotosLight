using PhotosLight.Interfaces;
using PhotosLight.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PhotosLight.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IThumbnailService _thumbnailService;
        private readonly IShareService _shareService;
        private IViewerItem _selectedItem;
        private object _selectedObject;
        private bool _isChromeOn;
        public DelegateCommand ShareCommnad { get; private set; }
        public DelegateCommand ZoomCommnad { get; private set; }
        public DelegateCommand RotateCommnad { get; private set; }
        public DelegateCommand DeleteCommnad { get; private set; }
        public DelegateCommand FullscreenCommnad { get; private set; }
        public DelegateCommand LoadContentCommnad { get; private set; }
        public ObservableCollection<IViewerItem> Thumbnails { get { return _thumbnailService.Thumbnails; } }
        public bool IsChromeOff { get { return !IsChromeOn; } }
        public bool IsFullScreen { get { return ApplicationView.GetForCurrentView().IsFullScreenMode; } }

        public MainViewModel(IThumbnailService thumbnailService, IShareService shareService)
        {
            _thumbnailService = thumbnailService;
            _shareService = shareService;
            SelectedItem = Thumbnails.FirstOrDefault();
            IsChromeOn = true;

            ShareCommnad = new DelegateCommand(() => _shareService.ShareContent(SelectedItem?.Title, "Share an Image from Photos", ((IViewerItem)SelectedItem).Source));
            FullscreenCommnad = new DelegateCommand(() =>
            {
                ToggleFullScreenMode();
                OnPropertyChanged(() => IsFullScreen);
            });

            LoadContentCommnad = new DelegateCommand(() => _thumbnailService.SelectFolder());

        }
        public bool IsChromeOn
        {
            get { return _isChromeOn; }
            set
            {
                SetProperty(ref _isChromeOn, value);
                OnPropertyChanged(() => IsChromeOff);
            }
        }
        public object SelectedObject
        {
            get { return _selectedObject; }
            set
            {
                SelectedItem = value as IViewerItem;
                SetProperty(ref _selectedObject, value);
            }
        }
        public IViewerItem SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }   
        public void OnViewTapped(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource.GetType() == typeof(Image)) 
            {
                ToogleChrome();
            }
        }
        private void ToogleChrome()
        {
            IsChromeOn = !IsChromeOn;
        }
        private void ToggleFullScreenMode()
        {
            var view = ApplicationView.GetForCurrentView();
            if (view.IsFullScreenMode)
            {
                view.ExitFullScreenMode();
                ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.Auto;
            }
            else
            {
                if (view.TryEnterFullScreenMode())
                {
                    ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
                }
            }
        }
    }
}
