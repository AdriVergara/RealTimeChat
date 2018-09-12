//using Aguacongas.Firebase;
using Firebase.Database;
using RealTimeChat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RealTimeChat.ViewModels
{
    public class ListViewPushNotificationsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public INavigation NavigationService { get; set; }

        FirebaseClient Client;

        public ObservableCollection<MessageModel> _messagesList { get; set; }
        public ObservableCollection<MessageModel> MessagesList
        {
            get
            {
                return _messagesList;
            }
            set
            {
                if (_messagesList == value) return;
                _messagesList = value;
                OnPropertyChanged(nameof(MessagesList));
            }
        }

        public ListViewPushNotificationsViewModel(INavigation _navigationService)
        {
            NavigationService = _navigationService;

            Client = new FirebaseClient("https://realtimechat-b2228.firebaseio.com/");

            InitializeMessagesList();
        }

        public async Task<List<MessageModel>> getList()
        {
            var List = (await Client
                .Child("Chat")
                .OnceAsync<MessageModel>())
                .Select(item =>
                    new MessageModel
                    {
                        Title = item.Object.Title,
                        MessageOwner = item.Object.MessageOwner
                    }).ToList();

            return List;
        }

        public ObservableCollection<MessageModel> getMessage()
        {
            var ChatData = Client
                .Child("Chat")
                .AsObservable<MessageModel>()
                .AsObservableCollection();

            return ChatData;
        }

        public async Task InitializeMessagesList()
        {
            MessagesList = getList();

        }
    }
}
