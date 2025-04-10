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
using MemoryGame2.Model;
using MemoryGame2.ViewModel;

namespace MemoryGame2.Views
{
    /// <summary>
    /// Interaction logic for OpenGameDialog.xaml
    /// </summary>
    public partial class OpenGameDialog : Window
    {
        public OpenGameDialog(UserModel user)
        {
            this.DataContext = new SavedGamesVM(user);
            ((SavedGamesVM)DataContext).CloseAction = () => Close();
            InitializeComponent();
        }
    }
}
