using RichLabel.Maui.Handlers;

namespace Sample.RichLabel.Maui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InitActionTouchHandler();
        }

        public void InitActionTouchHandler()
        {
            RichActionHandler.Register("phone", async value =>
            {
                await Shell.Current.DisplayAlert("Phone Data", $"Parameter value: {value}", "Close");
            });
            RichActionHandler.Register("text", async value =>
            {
                await Shell.Current.DisplayAlert("Text Data", $"Parameter value: {value}", "Close");
            });
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}