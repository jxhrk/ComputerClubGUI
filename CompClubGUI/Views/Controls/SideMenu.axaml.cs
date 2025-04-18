using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Transformation;
using CompClubGUI.Views;
using CompClubGUICore;
using CompClubGUICore.Views;
using CompClubGUICore.Views.Scaling;

namespace CompClubGUI;

public partial class SideMenu : UserControl
{
    private bool IsExpanded = false;

    private const int AnimationDuration = 150;

    public Scalable<ITransform> BorderScalable;

    public SideMenu()
    {
        InitializeComponent();
        ZIndex = 100;
        Loaded += OnLoaded;

        ScalableObject scalableObject = new ScalableObject(ContentBorder);
        BorderScalable = scalableObject.Register(Border.RenderTransformProperty, ContentBorder.RenderTransform);
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        SetState(false);
    }

    public void Button_Click(object sender, RoutedEventArgs e)
    {
        SetState(!IsExpanded);
    }

    public void Border_PointerPressed(object sender, PointerPressedEventArgs e)
    {
        SetState(false);
    }

    private async void SetState(bool expanded)
    {
        IsExpanded = expanded;

        DarkBackg.Opacity = expanded ? 1 : 0;
        ScalingManager manager = (Parent.Parent as BaseMainAppView).ScalingManager;
        manager.SetScale(BorderScalable, TransformOperations.Parse($"translateX({(expanded ? 0 : -200)}px)"));

        if (!expanded)
        {
            await Task.Delay(AnimationDuration);
        }

        ContentBorder.IsVisible = expanded;
        DarkBackg.IsVisible = expanded;
    }

    public void LogOut_Click(object sender, RoutedEventArgs e)
    {
        AppActions.SwitchFrame(new LoginRegView());
    }

    public void Settings_Click(object sender, RoutedEventArgs e)
    {
        AppActions.SwitchFrame(new SettingsView());
    }
    
    public void LeaveFeedback_Click(object sender, RoutedEventArgs e)
    {
        AppActions.SwitchFrame(new ReviewsView());
    }

}