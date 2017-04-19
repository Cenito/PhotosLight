using PhotosLight.Interfaces;
using PhotosLight.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private readonly IDialogService _dialogService;
        private IViewerItem _selectedItem;
        private object _selectedObject;
        private bool _isChromeOn;
        private double _zoomFactor;
        private bool _isFileInfoOpen;
        public DelegateCommand ShareCommnad { get; private set; }
        public DelegateCommand FileInfoCommnad { get; private set; }
        public DelegateCommand ZoomCommnad { get; private set; }
        public DelegateCommand RotateCommnad { get; private set; }
        public DelegateCommand DeleteCommnad { get; private set; }
        public DelegateCommand FullscreenCommnad { get; private set; }
        public DelegateCommand LoadContentCommnad { get; private set; }
        public ObservableCollection<IViewerItem> Thumbnails { get { return _thumbnailService.Thumbnails; } }
        public bool IsFullScreen { get { return ApplicationView.GetForCurrentView().IsFullScreenMode; } }

        public MainViewModel(IThumbnailService thumbnailService, IShareService shareService, IDialogService dialogService)
        {
            _thumbnailService = thumbnailService;
            _shareService = shareService;
            _dialogService = dialogService;
            SelectedItem = Thumbnails.FirstOrDefault();
            IsChromeOn = true;
            _zoomFactor = 1;

            ShareCommnad = new DelegateCommand(() => _shareService.ShareContent(SelectedItem?.Title, "Share an Image from Photos", ((IViewerItem)SelectedItem).Source));
            FullscreenCommnad = new DelegateCommand(() =>
            {
                ToggleFullScreenMode();
                OnPropertyChanged(() => IsFullScreen);
            });

            FileInfoCommnad = new DelegateCommand(() =>
            {
                IsFileInfoOpen = !IsFileInfoOpen;
            });

            RotateCommnad = new DelegateCommand(() =>
            {
                if (SelectedItem == null)
                    return;

                SelectedItem.Angle = SelectedItem.Angle + 90;
            });
            DeleteCommnad = new DelegateCommand(async () => await _dialogService.ShowDialogAsync("Not implemented yet!"));
            LoadContentCommnad = new DelegateCommand(() => _thumbnailService.SelectFolder());
        }
        public bool IsChromeOn
        {
            get { return _isChromeOn && ZoomFactor == 1; }
            set { SetProperty(ref _isChromeOn, value); }
        }
        public bool IsFileInfoOpen
        {
            get { return _isFileInfoOpen; }
            set { SetProperty(ref _isFileInfoOpen, value); }
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
            set
            {
                if (_selectedItem != null)
                {
                    _selectedItem.Angle = 0;
                    ((INotifyPropertyChanged)_selectedItem).PropertyChanged -= OnViewerItemPropertyChanged;
                }

                SetProperty(ref _selectedItem, value);
                if (_selectedItem != null)
                {
                    ((INotifyPropertyChanged)_selectedItem).PropertyChanged += OnViewerItemPropertyChanged;
                }

            }
        }

        private void OnViewerItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

        public void OnViewTapped(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource.GetType() == typeof(Image))
            {
                ToogleChrome();
            }
        }

        public double ZoomFactor
        {
            get { return SelectedItem != null ? SelectedItem.ZoomFactor : _zoomFactor; }
            set
            {
                if (SelectedItem != null)
                {
                    SelectedItem.ZoomFactor = value;
                }
                SetProperty(ref _zoomFactor, value);
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
