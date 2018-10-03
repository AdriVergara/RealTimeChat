using Newtonsoft.Json;
using RealTimeChat.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

using Firebase.Database;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Response;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System.Linq;
//using Aguacongas.Firebase;

namespace RealTimeChat.ViewModels
{
    public class LiveChatViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public INavigation NavigationService { get; }

        public string _messageText { get; set; }
        public string MessageText
        {
            get
            {
                return _messageText;
            }
            set
            {
                if (_messageText == value) return;
                _messageText = value;
                OnPropertyChanged(nameof(MessageText));
            }
        }

        public UserModel _user { get; set; }
        public UserModel User
        {
            get
            {
                return _user;
            }
            set
            {
                if (_user == value) return;
                _user = value;
                OnPropertyChanged(nameof(User));
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

        private byte[] _fileData { get; set; }
        public byte[] FileData
        {
            get
            {
                return _fileData;
            }
            set
            {
                _fileData = value;
                OnPropertyChanged("FileData");
            }
        }

        public object _fileSource { get; set; }
        public object FileSource
        {
            get
            {
                return _fileSource;
            }
            set
            {
                _fileSource = value;
                OnPropertyChanged("FileSource");
            }
        }

        private string _fileName { get; set; }
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
                OnPropertyChanged("FileName");
            }
        }

        public ICommand SendMessageToChat { get; set; }
        public ICommand SelectFileToInsert { get; set; }
        public ICommand DownloadFile { get; set; }

        IFirebaseConfig config { get; set; }

        FirebaseClient client { get; set; }

        IFirebaseClient FiresharpClient { get; set; }

        public LiveChatViewModel(INavigation _navigationService, UserModel _user, ObservableCollection<UserModel> _usersList)
        {
            NavigationService = _navigationService;

            User = _user;

            config = new FirebaseConfig
            {
                //AuthSecret = "your_firebase_secret",
                BasePath = "https://realtimechat-b2228.firebaseio.com/"
            };

            client = new Firebase.Database.FirebaseClient("https://realtimechat-b2228.firebaseio.com/");

            FiresharpClient = new FireSharp.FirebaseClient(config);

            Streaming();

            SendMessageToChat = new Command(async () => await ExecuteSendMessageToChat());
            SelectFileToInsert = new Command(async () => await ExecuteSelectFileToInsert());
            DownloadFile = new Command(async (Param) => await ExecuteDownloadFile(Param));
        }

        //Every time the database changes, this function has to update the List of Messages(MessagesList) to load the other messages
        public async Task Streaming()
        {
            EventStreamResponse response = await FiresharpClient.OnAsync("Chat", (sender, args, context) =>
            {
                getMessage(); //Refresh() == Read the rtDB and update MessagesList
            });

            //This disable the streaming function
            //response.Dispose();
        }

        public async Task ExecuteDownloadFile(object data)
        {
            var dataFile = data;

            CrossFilePicker cfp = new CrossFilePicker();


        }

        public async Task ExecuteSelectFileToInsert()
        {
            FileData filedata = await CrossFilePicker.Current.PickFile();

            //Getting the filename and the data info from the image picked
            FileName = filedata.FileName;
            FileData = filedata.DataArray;

            File newFile = new File(FileName, FileData);

            var MessageToPush = new MessageModel(User.UserName, newFile);

            var item = await client
              .Child("Chat")
              //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
              .PostAsync(MessageToPush);

            MessagesList.Add(MessageToPush);
        }

        public async Task ExecuteSendMessageToChat()
        {
            var MessageToPush = new MessageModel(MessageText, User.UserName);
            //{
            //    Title = MessageText,
            //    MessageOwner = User.UserName
            //};

            var item = await client
              .Child("Chat")
              //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
              .PostAsync(MessageToPush);

            MessagesList.Add(MessageToPush);
        }

        public async void getMessage()
        {
            var List = (await client
                .Child("Chat")
                .OnceAsync<MessageModel>())
                .Select(item =>
                    new MessageModel
                    {
                        Title = item.Object.Title,
                        MessageOwner = item.Object.MessageOwner,
                        File = item.Object.File
                    }).ToList();

            MessagesList = new ObservableCollection<MessageModel>(List);

            //Call dispose to stop listening for events
            //response.Dispose();
        }

    }
}
