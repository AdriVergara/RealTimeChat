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

        public FirebaseClient Client { get; set; }

        public string _messageOwner { get; set; }
        public string MessageOwner
        {
            get
            {
                return _messageOwner;
            }
            set
            {
                if (_messageOwner == value) return;
                _messageOwner = value;
                OnPropertyChanged(nameof(MessageOwner));
            }
        }

        public string _title { get; set; }
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (_title == value) return;
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

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

            //var ei = getList().Result;

            var taskList = getList();
            //MessagesList = new ObservableCollection<MessageModel>(taskList);

            //MessagesList = new ObservableCollection<MessageModel>(getList().Result);
            
            //MessagesList = new ObservableCollection<MessageModel>(ei);
            //MessagesList = new ObservableCollection<MessageModel>(ei.Result);
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

            //MessagesList = new ObservableCollection<MessageModel>(List);

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
            //MessagesList = getList();

        }
    }
}
