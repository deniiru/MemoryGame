using MemoryGame2.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MemoryGame2.Model;
using MemoryGame2.Commands;

namespace MemoryGame2.ViewModel
{
    public class NewUserVM : BaseClass
    {
        public UserModel User {get; set;} = new UserModel();
        private UserVM UserVM;

        private List<string> ImageUrls { get; set; }

        private int currentImageIndex = 0;

        private ICommand nextImageCommand;
        public ICommand NextImageCommand
        {
            get
            {
                if (nextImageCommand == null)
                {
                    nextImageCommand = new RelayCommand(_ => ChangeImage(1));
                }
                return nextImageCommand;
            }
        }

        private ICommand previousImageCommand;
        public ICommand PreviousImageCommand
        {
            get
            {
                if (previousImageCommand == null)
                {
                    previousImageCommand = new RelayCommand(_ => ChangeImage(-1));
                }
                return previousImageCommand;
            }
        }

        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new RelayCommand(_ => SaveUser());
                }
                return saveCommand;
            }
        }
        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new RelayCommand(_ => Cancel());
                }
                return cancelCommand;
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

        public NewUserVM(UserVM userVM)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "profile_picture");
            ImageUrls = Directory.GetFiles(folderPath, "*.png").ToList();

            User.Name = "user123";
            User.ImageUrl = ImageUrls[currentImageIndex + 2];
            UserVM = userVM;
        }

        private void ChangeImage(int direction)
        {
            currentImageIndex = (currentImageIndex + direction + ImageUrls.Count) % ImageUrls.Count;
            User.ImageUrl = ImageUrls[currentImageIndex];
        }

        private void SaveUser()
        {
            UserVM.AddUser(User);
            Services.FileHelpers.AddOrUpdateUserStatistic(User);
            CloseAction?.Invoke();
        }

        

    }
}
