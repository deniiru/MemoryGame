using MemoryGame2.Model;
using MemoryGame2.ViewModel;
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
using System.Windows.Shapes;

namespace MemoryGame2.Views
{
    /// <summary>
    /// Interaction logic for SetupGame.xaml
    /// </summary>
    public partial class SetupGame : Window
    {
        public UserModel UserModel { get; set; } 
        public GameVM game {  get; set; }
        public SetupGame(UserModel user)
        {
            SetupGameVM setupGameVM = new SetupGameVM(user);
            this.DataContext = setupGameVM;
            ((SetupGameVM)DataContext).CloseAction = () => Close();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new GameView(game).Show();
        }
    }
}
