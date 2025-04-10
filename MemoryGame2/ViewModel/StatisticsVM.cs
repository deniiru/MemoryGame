using MemoryGame2.Commands;
using MemoryGame2.Model;
using MemoryGame2.Services;
using MemoryGame2.ViewModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MemoryGame2.ViewModel
{
    public class StatisticsVM : BaseClass
    {
        // Collection of users and their statistics
        private ObservableCollection<UserModel> users;
        public ObservableCollection<UserModel> Users
        {
            get => users;
            set
            {
                users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public StatisticsVM()
        {
            Users = FileHelpers.LoadUserStatistics();
        }

        public void AddUserStatistic(UserModel newUserStat)
        {
           
            FileHelpers.AddOrUpdateUserStatistic(newUserStat);
            Users = FileHelpers.LoadUserStatistics();
        }

        public void UpdateUserStatistic(UserModel updatedUserStat)
        {
            FileHelpers.AddOrUpdateUserStatistic(updatedUserStat);
            Users = FileHelpers.LoadUserStatistics();
        }


        public Action? CloseAction { get; set; }

        private ICommand closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                {
                    closeCommand = new RelayCommand(_ => Cancel());
                }
                return closeCommand;
            }
        }
        private void Cancel()
        {
            CloseAction?.Invoke();
        }
    }
}
