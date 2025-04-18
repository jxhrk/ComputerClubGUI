using Avalonia.Controls;
using Avalonia.Input;
using CompClubGUICore.Views.Scaling;


namespace CompClubGUICore.Views
{
    public class BaseMainAppView : UserControl
    {
        protected override Type StyleKeyOverride { get { return typeof(UserControl); } }

        public Control? MainElement;

        public ScalingManager ScalingManager;

        double originalWidth = 1424;
        double originalHeight = 720;

        protected double currentFactor = 1;

        public BaseMainAppView()
        {
            PointerPressed += OnPointerPressed;
            Initialized += OnInitialized;
        }

        protected virtual void OnInitialized(object? sender, EventArgs e)
        {
            ScalingManager = new ScalingManager(this);

            SizeChanged += BaseMainAppView_SizeChanged;
        }

        private void BaseMainAppView_SizeChanged(object? sender, SizeChangedEventArgs e)
        {
            double x = e.NewSize.Width / originalWidth;
            double y = e.NewSize.Height / originalHeight;

            currentFactor = Math.Max(x, y);

            ScalingManager.SetScaling(currentFactor * AppInfo.ScalingFactorMultiplier);
        }

        private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (MainElement != null)
            {
                MainElement.Focusable = true;
                MainElement.Focus();
                MainElement.Focusable = false;
            }
        }

        protected virtual UserControl? GetPreviousPanel() => null;

        public void NavigateBack()
        {
            UserControl? control = GetPreviousPanel();
            if (control != null)
                AppActions.SwitchFrame(control);
        }
    }
}
