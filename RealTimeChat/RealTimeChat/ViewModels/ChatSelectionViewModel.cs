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

        public ChatSelectionViewModel(INavigation _navigationService, UserModel _user, ObservableCollection<UserModel> _usersList)
        {
            NavigationService = _navigationService;

            UsersList = _usersList;
            UserLogged = _user;

            Initialize();

            ConfirmToChat = new Command(async () => await ExecuteConfirmToChat());
            ResetUsersList = new Command(async () => await ExecuteResetUsersList());
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

        private async Task ExecuteResetUsersList()
        {
            
        }

        private async Task ExecuteConfirmToChat()
        {
            await NavigationService.PushAsync(new LiveChatView(NavigationService, UserLogged, selectedIt));

        }
    }
}
