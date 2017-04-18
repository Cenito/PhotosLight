using PhotosLight.DataModel;
using PhotosLight.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.Streams;

namespace PhotosLight.Services
{
    public class ShareService : IShareService
    {
        private DataTransferManager _dataTransferManager;
        private object _data;
        private string _title;
        private string _description;
        public ShareService()
        {
            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += OnDataRequested; ;
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {

            args.Request.Data.SetText("Hello from Photos Light");
            var uri = _data as Uri;
            if (uri != null)
            {
                var stream = RandomAccessStreamReference.CreateFromUri(uri);
                args.Request.Data.Properties.Thumbnail = stream;
                args.Request.Data.SetBitmap(stream);
            }

            args.Request.Data.Properties.Title = _title;
            args.Request.Data.Properties.Description = _description;
        }

        public void ShareContent(string title, string description, object data)
        {
            _data = data;
            _title = title;
            _description = description;
            DataTransferManager.ShowShareUI();
        }
    }
}
