using Avalonia.Controls;
using ValveBench.Desktop.ViewModels;

namespace ValveBench.Desktop.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new MainWindowViewModel();
    }
}