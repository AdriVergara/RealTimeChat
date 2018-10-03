using RealTimeChat.Models;
using RealTimeChat.Views;
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
            LiveChat = new Command(async () => await ExecuteLiveChat());
        }

        private async Task ExecuteTestImageListView()
        {
            await NavigationService.PushAsync(new TestImageListView(NavigationService));
        }

        public async Task ExecuteLiveChat()
        {
            await NavigationService.PushAsync(new LiveChatView(NavigationService, selectedIt, UsersList));
        }

        private void InitializeUsers()
        {
            UsersList = new ObservableCollection<UserModel>();

            UserModel User1 = new UserModel
            {
                Id = 0,
                UserName = "Adri",
                Password = "Adri"
            };

            UserModel User2 = new UserModel
            {
                Id = 1,
                UserName = "Andreu",
                Password = "Andreu"
            };

            UsersList.Add(User1);
            UsersList.Add(User2);
        }
        
    }
}
