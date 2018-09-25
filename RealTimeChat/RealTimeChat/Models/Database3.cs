//using Firebase.Database;
using Firebase.Database.Extensions;
using Firebase.Database.Query;
using Firebase.Database.Streaming;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RealTimeChat.Models
{
    public class Database3 : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public INavigation NavigationService { get; }

        //public IObserver<FirebaseEvent<MessageModel>> Observer;

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

        public ICommand SendMessageToChat { get; set; }

        IFirebaseConfig config { get; set; }
        IFirebaseClient client { get; set; }

        public Database3(INavigation _navigation, UserModel _user, ObservableCollection<UserModel> _usersList)
        {
            MessagesList = new ObservableCollection<MessageModel>();

            User = _user;

            config = new FirebaseConfig
            {
                //AuthSecret = "your_firebase_secret",
                BasePath = "https://realtimechat-b2228.firebaseio.com/"
            };

            client = new FirebaseClient(config);

            NavigationService = _navigation;

            //Observer.OnCompleted();

            MessagesList = new ObservableCollection<MessageModel>();

            getMessage();

            //task.Wait();

            //var e = task;//.Result;

            SendMessageToChat = new Command(async () => await ExecuteSendMessageToChat());


            //FUNCIONA!! (Actualizar MessagesList)
            Streaming();


            


        }

        public async Task Streaming()
        {
            EventStreamResponse response = await client.OnAsync("", (sender, args, context) => {
                Console.WriteLine(args.Data);
            });

            //Call dispose to stop listening for events
            
            //response.Dispose();
        }

        public async Task ExecuteSendMessageToChat()
        {
            var MessageToPush = new MessageModel()
            {
                Title = MessageText,
                MessageOwner = User.UserName
            };

            PushResponse response = await client.PushAsync("Chat", MessageToPush);
            //response.Result.name; //The result will contain the child name of the new data that was added

            //string hola = response.Result.name;

            MessagesList.Add(MessageToPush);
        }

        public async void getMessage()
        {
            //todo el json
            FirebaseResponse response = await client.GetAsync("Chat");

            //MessagesList = response.ResultAs<ObservableCollection<MessageModel>>();

            ObservableCollection<MessageModel> ListaAuxiliar = response.ResultAs<ObservableCollection<MessageModel>>();

            //var items = await response.Body

            //var items = await firebase
        //        .Child("Chat")
        //        //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
        //        .OnceAsync<MessageModel>();

            //    foreach (var item in items)
            //    {
            //        var message = new MessageModel { Title = item.Object.Title, MessageOwner = item.Object.MessageOwner };

            //        MessagesList.Add(message);
            //    }








            //EventStreamResponse response = await client
            //    .OnAsync("Chat", (sender, args, context) => {
            //    //System.Console.WriteLine(args.Data);
            //    //
            //    string a = args.Data;

            //    //
            //});

            //Call dispose to stop listening for events
            //response.Dispose();
        }

        //public void getList()
        //{
        //    var firebase = new FirebaseClient("https://realtimechat-b2228.firebaseio.com/");



        //    //Observer.

        //    var observable = firebase
        //        .Child("Chat")
        //        //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
        //        //.AsRealtimeDatabase<MessageModel>()
        //        .AsObservable<MessageModel>();
        //        //.Subscribe(/*Observer*/);
        //        //.AsObservable<MessageModel>()
        //        //.

        //    //foreach (var item in items)
        //    //{
        //    //    var message = new MessageModel { Title = item.Object.Title, MessageOwner = item.Object.MessageOwner };

        //    //    MessagesList.Add(message);
        //    //}
        //}

        //public async void getMessage()
        //{
        //    var firebase = new FirebaseClient("https://realtimechat-b2228.firebaseio.com/");

        //    var items = await firebase
        //        .Child("Chat")
        //        //.WithAuth("<Authentication Token>") // <-- Add Auth token if required. Auth instructions further down in readme.
        //        .OnceAsync<MessageModel>();

        //    foreach (var item in items)
        //    {
        //        var message = new MessageModel { Title = item.Object.Title, MessageOwner = item.Object.MessageOwner };

        //        MessagesList.Add(message);
        //    }

        //    //.OnceAsync<MessageModel>())
        //    //.Select(item =>
        //    //    new MessageModel
        //    //    {
        //    //        Title = item.Object.Title,
        //    //        MessageOwner = item.Object.MessageOwner
        //    //    }).ToList();

        //    //return new ObservableCollection<MessageModel>(List);
        //}
    }
}
