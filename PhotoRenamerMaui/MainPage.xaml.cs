using Utility;

namespace PhotoRenamerMaui
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {

            InitializeComponent();
        }

        private async void OnStartClicked(object sender, EventArgs e)
        {
            string sourceDir = _entrySourceDir.Text;
            string destDir = _entryDestDir.Text;

            _label.Text = $"Путь к папке с фотографиями: {sourceDir}. Путь к папке назначения: {destDir}.";

            PhotoCollector collector = new PhotoCollector(sourceDir, destDir);
            await collector.CollectPhotos();

            _label.Text += "\nФотографии успешно собраны.";
        }
    }

}
