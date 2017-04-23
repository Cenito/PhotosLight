using PhotosLight.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using PhotosLight.DataModel;
using Windows.Storage.Pickers;
using Windows.Storage;
using Windows.Storage.Search;
using System.IO;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;

namespace PhotosLight.Services
{
    public class ThumbnailService : IThumbnailService
    {
        public ObservableCollection<IViewerItem> Thumbnails { get; set; }

        public ThumbnailService()
        {
            Thumbnails = new ObservableCollection<IViewerItem>();
            Thumbnails.Add(new ImageViewerItem("Cliff", "Assets/Cliff.jpg"));
            Thumbnails.Add(new ImageViewerItem("Grapes", "Assets/Grapes.jpg"));
            Thumbnails.Add(new ImageViewerItem("Rainier", "Assets/Rainier.jpg"));
            Thumbnails.Add(new VideoViewerItem("Argumented Reality", "Assets/TetrAReality21.mp4"));
            Thumbnails.Add(new ImageViewerItem("Sunset", "Assets/Sunset.jpg"));
            Thumbnails.Add(new ImageViewerItem("Beach", "Assets/beach.jpg"));
            Thumbnails.Add(new ImageViewerItem("Beach 2", "Assets/images.jpg"));
            Thumbnails.Add(new ImageViewerItem("climbing", "Assets/climbing.jpg"));
            Thumbnails.Add(new ImageViewerItem("Motocross", "Assets/maxresdefault.jpg"));
            Thumbnails.Add(new ImageViewerItem("Snowboard", "Assets/snowboard.jpg"));
            Thumbnails.Add(new ImageViewerItem("PalmBeach", "Assets/WaterPark_PalmBeach.jpg"));
            Thumbnails.Add(new ImageViewerItem("Valley", "Assets/Valley.jpg"));
        }

        public async void SelectFolder()
        {
            var folderPicker = new FolderPicker();
            folderPicker.ViewMode = PickerViewMode.Thumbnail;
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            folderPicker.FileTypeFilter.Add(".jpg");
            folderPicker.FileTypeFilter.Add(".jpeg");
            folderPicker.FileTypeFilter.Add(".png");
            folderPicker.FileTypeFilter.Add(".bmp");
            folderPicker.FileTypeFilter.Add("*");
            var folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                Windows.Storage.AccessCache.StorageApplicationPermissions.
                FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                LoadContent(folder);
            }
        }

        public void LoadContent(StorageFolder folder)
        {
            Thumbnails.Clear();
            List<string> fileTypeFilter = new List<string>();
            fileTypeFilter.Add(".jpg");
            fileTypeFilter.Add(".jpeg");
            fileTypeFilter.Add(".png");
            fileTypeFilter.Add(".bmp");
            QueryOptions queryOptions = new QueryOptions(CommonFileQuery.OrderBySearchRank, fileTypeFilter);

            Task.Run(async () =>
            {
                StorageFileQueryResult queryResult = folder.CreateFileQueryWithOptions(queryOptions);
                var files = await queryResult.GetFilesAsync();
                foreach (var file in files)
                {
                    if (IsImageFile(file))
                    {
                        await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                          {
                              Thumbnails.Add(new ImageViewerItem(file));
                              
                          });                   
                    }
                }
            });
        }

        private bool IsImageFile(StorageFile file)
        {
            var extension = Path.GetExtension(file.Path);
            return string.Compare(extension, ".jpg", true) == 0 ||
                string.Compare(extension, ".jpeg", true) == 0 ||
                string.Compare(extension, ".png", true) == 0 ||
                string.Compare(extension, ".bmp", true) == 0;
        }


    }
}
