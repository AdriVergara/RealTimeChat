using RealTimeChat.Models;
using RealTimeChat.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

using Firebase.Database;
using Firebase.Database.Query;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Response;

namespace RealTimeChat.ViewModels
{
    public class ChatSelectionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
        public INavigation NavigationService { get; set; }

        public ICommand ConfirmToChat { get; set; }
        public ICommand ResetUsersList { get; set; }
        

        public UserModel selectedIt { get; set; }

        public UserModel _userLogged { get; set; }
        public UserModel UserLogged
        {
            get
            {
                return _userLogged;
            }
            set
            {
                if (_userLogged == value) return;
                _userLogged = value;
                OnPropertyChanged(nameof(UserLogged));
            }
        }

        public ObservableCollection<CurrentUserChatModel> _currentUserChatsList { get; set; }
        public ObservableCollection<CurrentUserChatModel> CurrentUserChatsList
        {
            get
            {
                return _currentUserChatsList;
            }
            set
            {
                if (_currentUserChatsList == value) return;
                _currentUserChatsList = value;
                OnPropertyChanged(nameof(CurrentUserChatsList));
            }
        }

        public string _chatName { get; set; }
        public string ChatName
        {
            get
            {
                return _chatName;
            }
            set
            {
                if (_chatName == value) return;
                _chatName = value;
                OnPropertyChanged(nameof(ChatName));
            }
        }

        public ObservableCollection<UserModel> _usersList { get; set; }
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

        private CurrentUserChatModel _listViewItemSelected;
        public CurrentUserChatModel ListViewItemSelected
        {
            get
            {
                return _listViewItemSelected;
            }
            set
            {
                if (_listViewItemSelected == value) return;
                _listViewItemSelected = value;
                OnPropertyChanged(nameof(ListViewItemSelected));
            }
        }

        IFirebaseConfig config { get; set; }
        FirebaseClient client { get; set; }
        IFirebaseClient FiresharpClient { get; set; }

        public ChatSelectionViewModel(INavigation _navigationService, UserModel _user, ObservableCollection<UserModel> _usersList)
        {
            NavigationService = _navigationService;

            UsersList = _usersList;
            UserLogged = _user;

            CurrentUserChatsList = new ObservableCollection<CurrentUserChatModel>();

            LoadCurrentChats();

            Initialize();

            ConfirmToChat = new Command(async () => await ExecuteConfirmToChat());
            //ResetUsersList = new Command(async () => await ExecuteResetUsersList());
        }

        private void Initialize()
        {
            int cont = 0;
            int final = 0;
            bool found = false;

            foreach (UserModel user in UsersList)
            {
                if (user.UserName == UserLogged.UserName && found == false)
                {
                    found = true;
                    final = cont;
                }
                else
                {
                    cont++;
                }
            }
            UsersList.RemoveAt(final);
        }

        private async void LoadCurrentChats()
        {
            config = new FirebaseConfig
            {
                //AuthSecret = "your_firebase_secret",
                BasePath = "https://realtimechat-b2228.firebaseio.com/"
            };

            client = new FirebaseClient("https://realtimechat-b2228.firebaseio.com/");

            FiresharpClient = new FireSharp.FirebaseClient(config);



            var collection = await client
                .Child("")
                //.Child("")
                .OnceAsync<UserModel>();

            foreach (var col in collection)
            {
                //ChatName = col.Key;
                var obj = new CurrentUserChatModel(col.Key); 
                CurrentUserChatsList.Add(obj);
                //Console.WriteLine($"{dino.Key} is {dino.Object.Height}m high.");
            }

            CleanCurrentUserChatsList();
        }

        private void CleanCurrentUserChatsList()
        {

            for (int i = CurrentUserChatsList.Count - 1; i >= 0; i--)
            {
                if (CurrentUserChatsList[i].ChatParticipants.Contains(UserLogged.UserName))
                {
                    int index;
                    string cleanPath;

                    index = CurrentUserChatsList[i].ChatParticipants.IndexOf(UserLogged.UserName);

                    cleanPath = (index < 0)
                        ? CurrentUserChatsList[i].ChatParticipants
                        : CurrentUserChatsList[i].ChatParticipants.Remove(index, UserLogged.UserName.Length);


                    if (cleanPath.Contains("_"))
                    {
                        cleanPath = cleanPath.TrimStart('_');
                        cleanPath = cleanPath.TrimEnd('_');

                    }

                    CurrentUserChatsList[i].ChatParticipants = cleanPath;
                }
                else
                {
                    CurrentUserChatsList.RemoveAt(i);
                }
            }

        }

        private async Task ExecuteConfirmToChat()
        {
            UserModel obj = new UserModel(0, ListViewItemSelected.ChatParticipants); 

            await NavigationService.PushAsync(new LiveChatView(NavigationService, UserLogged, obj));

        }
    }
}
