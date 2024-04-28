using System.Windows;
using System.Windows.Controls;

namespace Chess_Paper_Scissors.InfoPages;

/// <summary>
/// Логика взаимодействия для Page1.xaml
/// </summary>
public partial class Page1 : Page
{
    public Page1()
    {
        InitializeComponent();
    }
    public event EventHandler WentFigures;

    private void FiguresButton_Click(object sender, RoutedEventArgs e)
    {
        Content = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();
        WentFigures.Invoke(null, EventArgs.Empty);
    }

    private void ExitButton_Click(object sender, RoutedEventArgs e)
    {
        Content = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}
