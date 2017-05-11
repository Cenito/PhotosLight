using PhotosLight.Interfaces;
using PhotosLight.MVVM;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.ViewManagement;

namespace PhotosLight.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IThumbnailService _thumbnailService;
        private readonly IShareService _shareService;
        public ObservableCollection<IViewerItem> Thumbnails { get { return _thumbnailService.Thumbnails; } }

        public MainViewModel(IThumbnailService thumbnailService, IShareService shareService)
        {
            _thumbnailService = thumbnailService;
            _shareService = shareService;
            SelectedItem = thumbnailService.Thumbnails.FirstOrDefault();
            FullScreenCommand = new DelegateCommand(() =>
            {
                ToggleFullScreenMode();
                OnPropertyChanged(() => IsFullScreen);
            });
        }

        public DelegateCommand FullScreenCommand { get; private set; }

        public bool IsFullScreen { get => ApplicationView.GetForCurrentView().IsFullScreenMode; }

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

        private IViewerItem _selectedItem;
            
        public IViewerItem SelectedItem
        {
            get { return _selectedItem; }
            set {
                SetProperty(ref _selectedItem, value);
            }
        }

        private bool _isChromeOff;

        public bool IsChromeOff
        {
            get { return _isChromeOff; }
            set { SetProperty(ref _isChromeOff, value); }
        }
    }
}
