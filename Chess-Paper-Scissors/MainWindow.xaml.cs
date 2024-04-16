using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenTK.Windowing.Desktop;
using Chess_Paper_Scissors.InfoPages;

namespace Chess_Paper_Scissors
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            using (Game game = new Game(GameWindowSettings.Default, Game.NWSettings(), this))
            {
                game.Run();
            }
        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            GoToInfoPage();
        }
        private void GoToFigurePage(object sender, EventArgs e)
        {
            PageFigures page = new PageFigures();
            PageFrame.Content = page;
            page.WentFigures += GoToInfoPage;
        }
        private void GoToInfoPage(object sender, EventArgs e)
        {
            GoToInfoPage();
        }
        private void GoToInfoPage()
        {
            Page1 page = new Page1();
            PageFrame.Content = page;
            page.WentFigures += GoToFigurePage;
        }
    }
}