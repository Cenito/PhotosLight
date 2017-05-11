using PhotosLight.Interfaces;
using PhotosLight.MVVM;
using System.Collections.ObjectModel;
using System.Linq;

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
        }

        private IViewerItem _selectedItem;
            
        public IViewerItem SelectedItem
        {
            get { return _selectedItem; }
            set {
                SetProperty(ref _selectedItem, value);
            }
        }
    }
}
