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

        public UserModel _selectedIt { get; set; }
        public UserModel SelectedIt
        {
            get
            {
                return _selectedIt;
            }
            set
            {
                if (_selectedIt == value) return;
                _selectedIt = value;
                OnPropertyChanged(nameof(SelectedIt));
            }
        }

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

        //For Binding
        public ICommand SendMessageToChat { get; set; }
        public ICommand SelectFileToInsert { get; set; }
        public ICommand DownloadFile { get; set; }

        FirebaseClient client { get; set; } //It's used to post or get messages in the Real Time Database (RTD)

        //Initial configurations to connect with the RTD
        IFirebaseConfig config { get; set; }            
        IFirebaseClient FiresharpClient { get; set; }

        public string ChatKey { get; set; }
        public bool InverseChat { get; set; }

        public string OriginalKey { get; set; }

        public LiveChatViewModel(INavigation _navigationService, UserModel _userLogged, UserModel _selectedIt)
        {
            NavigationService = _navigationService;

            MessagesList = new ObservableCollection<MessageModel>();

            UserLogged = _userLogged;
            SelectedIt = _selectedIt;

            ChatKey = UserLogged.UserName + "_" + SelectedIt.UserName; //RTD Child's Name
            OriginalKey = ChatKey;
            InverseChat = false;

            config = new FirebaseConfig
            {
                //AuthSecret = "your_firebase_secret",
                BasePath = "https://realtimechat-b2228.firebaseio.com/"
            };

            client = new FirebaseClient("https://realtimechat-b2228.firebaseio.com/");

            FiresharpClient = new FireSharp.FirebaseClient(config);

            Streaming();
            getMessage();

            SendMessageToChat = new Command(async () => await ExecuteSendMessageToChat());
            //SelectFileToInsert = new Command(async () => await ExecuteSelectFileToInsert());
            DownloadFile = new Command(async (Param) => await ExecuteDownloadFile(Param));
        }

        //Every time the database changes, this function has to update the List of Messages(MessagesList) to load the other messages
        public async Task Streaming()
        {

            EventStreamResponse response;

            string InverseChatKey = String.Empty;

            string[] usersKey = ChatKey.Split('_');

            InverseChatKey = usersKey[1] + "_" + usersKey[0];

            //This will be called when the RTD changes (and the user started the chat)
            response = await FiresharpClient.OnAsync(ChatKey, (sender, args, context) =>
            {
                getMessage(); //Refresh() == Read the rtDB and update MessagesList
            });

            //This will be called when the RTD changes (and the user didn't start the chat)
            response = await FiresharpClient.OnAsync(InverseChatKey, (sender, args, context) =>
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

        //public async Task ExecuteSelectFileToInsert()
        //{
        //    FileData filedata = await CrossFilePicker.Current.PickFile();

        //    //Getting the filename and the data info from the image picked
        //    FileName = filedata.FileName;
        //    FileData = filedata.DataArray;

        //    File newFile = new File(FileName, FileData);

        //    var MessageToPush = new MessageModel(UserLogged.UserName, newFile);

        //    var item = await client
        //      .Child("Chat")
        //      //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
        //      .PostAsync(MessageToPush);

        //    MessagesList.Add(MessageToPush);
        //}

        public async Task ExecuteSendMessageToChat()
        {
            var MessageToPush = new MessageModel(MessageText, UserLogged.UserName);
            //{
            //    Title = MessageText,
            //    MessageOwner = User.UserName
            //};

            //if (!InverseChat)
            //{
            //    ChatKey = UserLogged.UserName + "_" + SelectedIt.UserName;
            //}

            var item = await client
              .Child(ChatKey) //Este Item va dentro del nodo:
              //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
              .PostAsync(MessageToPush);

            MessagesList.Add(MessageToPush);
        }

        public async void getMessage()
        {
            //string key = UserLogged.UserName + "_" + SelectedIt.UserName;

            var List = new List<MessageModel>();

            List = (await client
                .Child(ChatKey)
                .OnceAsync<MessageModel>())
                .Select(item =>
                    new MessageModel
                    {
                        Title = item.Object.Title,
                        MessageOwner = item.Object.MessageOwner,
                        File = item.Object.File
                    }).ToList();
            
            if (List.Count == 0) //Hasta aquí, no se ha encontrado ningún nodo child en la BD con exactamente el mismo nombre, falta mirar si existe el mismo chat pero si está iniciado por la otra persona.
            {
                ChatKey = SelectedIt.UserName + "_" + UserLogged.UserName;

                List = (await client
                .Child(ChatKey)
                .OnceAsync<MessageModel>())
                .Select(item =>
                    new MessageModel
                    {
                        Title = item.Object.Title,
                        MessageOwner = item.Object.MessageOwner,
                        File = item.Object.File
                    }).ToList();

                if (List.Count == 0) //No existe ningun chat entre estas 2 personas
                {
                    ChatKey = OriginalKey;
                }
                else //Hay un chat entre estas 2 personas, pero en la BD el nombre del "Child" empieza con el nombre del destinatarion del 1r mensaje
                {
                    //Do Nothing
                    InverseChat = true;
                }

            }

            MessagesList = new ObservableCollection<MessageModel>(List);

            //Call dispose to stop listening for events
            //response.Dispose();
        }

    }
}
