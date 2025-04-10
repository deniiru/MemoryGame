using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using MemoryGame2.Model;
using MemoryGame2.Commands;
using MemoryGame2.Services;

namespace MemoryGame2.ViewModel
{
    public class UserVM: BaseClass
    {
        public ObservableCollection<UserModel> Users { get; set; }

        private UserModel selectedUser;
        public UserModel SelectedUser
        {
            get { return selectedUser; }
            set { selectedUser = value; OnPropertyChanged("SelectedUser"); }
        }

        public UserVM()
        {
            NextImageCommand = new RelayCommand(NextImage, UserSelected);
            PreviousImageCommand = new RelayCommand(PreviousImage, UserSelected);
            DeleteUserCommand = new RelayCommand(_ =>EraseUser(), UserSelected);

            LoadUsers();         
        }

        public void AddUser(UserModel user )
        {
            Users.Add(user);
            SaveUsers();
        }

        public void EraseUser()
        {
            if (SelectedUser == null)
                return;
            Services.FileHelpers.DeleteUser(SelectedUser);
            Users.Remove(SelectedUser);
           
            SaveUsers();
        }

        public void SaveUsers()
        {
            FileHelpers.SaveToFileCollection(Users);
        }

        public void LoadUsers()
        {
            Users = FileHelpers.LoadFromFileCollection();
        }

        public ICommand NextImageCommand { get; private set; }
        public ICommand PreviousImageCommand { get; private set; }

        public ICommand DeleteUserCommand {get; private set;}

        private ICommand playCommand;
        public ICommand PlayCommand
        { get { if (playCommand == null)
                    playCommand = new RelayCommand(_ => GameSettings.PlayGame(SelectedUser), UserSelected);
                return playCommand; 
            } 
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


        private void NextImage(object parameter)
        {
            if (SelectedUser != null)
            {
                // find the next user in the list
                int index = Users.IndexOf(SelectedUser);
                index = (index + 1) % Users.Count;
                SelectedUser = Users[index];
                OnPropertyChanged("SelectedUser");
            }
        }

        private void PreviousImage(object parameter)
        {
            if (SelectedUser != null)
            {
                // find the previous user in the list
                int index = Users.IndexOf(SelectedUser);
                index = (index - 1 + Users.Count) % Users.Count;
                SelectedUser = Users[index];
                OnPropertyChanged("SelectedUser");
            }
        }

        private bool UserSelected(object parameter)
        {
            return SelectedUser != null;
        }



    }
}
