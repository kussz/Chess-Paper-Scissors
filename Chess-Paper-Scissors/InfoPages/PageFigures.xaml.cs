using System.Windows;
using System.Windows.Controls;

namespace Chess_Paper_Scissors.InfoPages;

/// <summary>
/// Логика взаимодействия для Page1.xaml
/// </summary>
public partial class PageFigures : Page
{
    public PageFigures()
    {
        InitializeComponent();
    }
    public event EventHandler WentFigures;
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Content = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();
        WentFigures.Invoke(null, EventArgs.Empty);
    }
}
