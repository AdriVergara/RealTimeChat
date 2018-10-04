using RealTimeChat.Models;
using RealTimeChat.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RealTimeChat.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public INavigation NavigationService { get; set; }

        //public ICommand ConfirmUser { get; set; }
        public ICommand LiveChat { get; set; }
        public ICommand ChatSelection { get; set; }
        public ICommand ResetUsersList { get; set; }
        //public ICommand TestImageListView { get; set; }

        private ObservableCollection<UserModel> _usersList;
        public ObservableCollection<UserModel> UsersList
        {
            get
            {
                return _usersList;
            }
            set
            {
                if (_usersList == value) return;
                _usersList = value;
                OnPropertyChanged(nameof(UsersList));
            }
        }

        //public string UserName { get; set; }
        //public string Password { get; set; }

        public UserModel selectedIt { get; set; }

        public LoginViewModel(INavigation _navigationService)
        {
            InitializeUsers();
            //LoadFirebaseDatabase();

            NavigationService = _navigationService;
            //ConfirmUser = new Command(async () => await ExecuteConfirmUser());
            //TestImageListView = new Command(async () => await ExecuteTestImageListView());
            //LiveChat = new Command(async () => await ExecuteLiveChat());
            ChatSelection = new Command(async () => await ExecuteChatSelection());
            ResetUsersList = new Command(async () => await ExecuteResetUsersList());
        }

        private async Task ExecuteResetUsersList()
        {
            InitializeUsers();
        }

        private async Task ExecuteChatSelection()
        {
            await NavigationService.PushAsync(new ChatSelectionView(NavigationService, selectedIt, UsersList));
        }

        //private async Task ExecuteTestImageListView()
        //{
        //    await NavigationService.PushAsync(new TestImageListView(NavigationService));
        //}

        //public async Task ExecuteLiveChat()
        //{
        //    await NavigationService.PushAsync(new LiveChatView(NavigationService, selectedIt, UsersList));
        //}



        private void InitializeUsers()
        {
            UsersList = new ObservableCollection<UserModel>();

            UserModel User1 = new UserModel(0, "Adri");
            UserModel User2 = new UserModel(1, "Andreu");
            UserModel User3 = new UserModel(2, "Martina");

            UsersList.Add(User1);
            UsersList.Add(User2);
            UsersList.Add(User3);
        }
        
    }
}
