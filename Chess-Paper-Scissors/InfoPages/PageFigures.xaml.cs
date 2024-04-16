using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chess_Paper_Scissors.InfoPages
{
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
            WentFigures.Invoke(null, EventArgs.Empty);
        }
    }
}
