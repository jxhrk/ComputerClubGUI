using Avalonia.Controls;
using Avalonia.Interactivity;

namespace CompClubGUI.Admin;

public partial class ConfirmationWindow : Window
{
    public delegate void OnYes();
    public delegate void OnNo();
    public delegate void OnCancel();

    OnYes? OnYesDelegate;
    OnNo? OnNoDelegate;
    OnCancel? OnCancelDelegate;

    public ConfirmationWindow(OnYes onYes, OnNo onNo, OnCancel onCancel)
    {
        InitializeComponent();
        OnYesDelegate = onYes;
        OnNoDelegate = onNo;
        OnCancelDelegate = onCancel;
    }

    public ConfirmationWindow()
    {
        InitializeComponent();
    }

    private void YesButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
        OnYesDelegate?.Invoke();
    }
    private void NoButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
        OnNoDelegate?.Invoke();
    }
    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        Close();
        OnCancelDelegate?.Invoke();
    }
}