using System.Windows;
using Kolinko_lab_01.ViewModels;

namespace Kolinko_lab_01.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }

}
