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
using MemoryGame2.Model;
using MemoryGame2.ViewModel;

namespace MemoryGame2.Views
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : Window
    {
        public GameVM game;
        public GameView(GameVM game)
        {
            this.game = game;
            this.DataContext = this.game;
            ((GameVM)DataContext).CloseAction = () => Close();
            InitializeComponent();
        }
    }
}
