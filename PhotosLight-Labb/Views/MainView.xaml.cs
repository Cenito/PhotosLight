using PhotosLight.Services;
using PhotosLight.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PhotosLight.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainView : Page
    {
        public MainViewModel ViewModel { get; private set; }
        public MainView()
        {
            ViewModel = new MainViewModel(new ThumbnailService(), new ShareService());

            this.InitializeComponent();
        }

        private void theFlipView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ViewModel.IsChromeOff = !ViewModel.IsChromeOff;
        }
    }
}
