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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MemoryGame2.Views
{
    /// <summary>
    /// Interaction logic for StartPageView.xaml
    /// </summary>
    public partial class StartPageView : UserControl
    {
        public UserVM users; 
        public StartPageView()
        {
            InitializeComponent();
            users = new UserVM();
            DataContext = users;
            //users.CloseAction = () => this.Close();
            

        }

        private void NewUser_Click(object sender, RoutedEventArgs e)
        {
            new NewUserView(users).Show();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            new SetupGame(users.SelectedUser).Show();
        }
    }
}
