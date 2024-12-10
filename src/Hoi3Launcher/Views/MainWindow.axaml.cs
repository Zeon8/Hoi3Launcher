using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Classic.Avalonia.Theme;
using Hoi3Launcher.ViewModels;

namespace Hoi3Launcher.Views;

public partial class MainWindow : ClassicWindow
{
    public MainWindow()
    {
        InitializeComponent();
        if(!Design.IsDesignMode)
            Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var viewModel = (MainWindowViewModel)DataContext!;
        viewModel.Load();
    }
}