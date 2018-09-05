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

namespace RealTimeChat.ViewModels
{
    public class LiveChatViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public INavigation NavigationService { get; set; }
        public string content { get; set; }


        //public int Id { get; set; }
        //public string UserName { get; set; }
        //public string Password { get; set; }

        public string MessageText { get; set; }

        public string DbJson { get; set; }

        public ICommand SendMessageToChat { get; set; }
        public ICommand UpdateChat { get; set; }

        //BD : : https://realtimechat-b2228.firebaseio.com/    http://jsonplaceholder.typicode.com/posts

        private const string Url = "https://realtimechat-b2228.firebaseio.com/.json"; //This url is a free public api intended for demos
        private readonly HttpClient _client = new HttpClient(); //Creating a new instance of HttpClient. (Microsoft.Net.Http)

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

        public LiveChatViewModel(INavigation _navigationService, UserModel _user, ObservableCollection<UserModel> _usersList)
        {
            //var reference = firebase.database();

            //DbJson = "";
            content = "";

            MessagesList = new ObservableCollection<MessageModel>();
            //client.MaxResponseContentBufferSize = 256000;

            ExecuteUpdateChat();

            UsersList = _usersList;

            NavigationService = _navigationService;

            SendMessageToChat = new Command(async () => await ExecuteSendMessageToChat());
            UpdateChat = new Command(async () => await ExecuteUpdateChat());

            //Id = _user.Id;
            //UserName = _user.UserName;
            //Password = _user.Password;
        }

        //public async Task UpdateChat2Async()
        //{
        //    content = await _client.GetStringAsync(Url).ConfigureAwait(false); //Sends a GET request to the specified Uri and returns the response body as a string in an asynchronous operation

        //    //look if content contains ",null" and delete it (This is beacause there will be a "null" entry if some record is deleted manually from the database and then you update)
        //    if (content.Contains(",null"))
        //    {
        //        content = content.Replace(",null", "");
        //    }

        //    DbJson = content;

        //    if (DbJson == "null")
        //    {
        //        DbJson = "[]";
        //    }
        //    else
        //    {
        //        List<MessageModel> messages = JsonConvert.DeserializeObject<List<MessageModel>>(DbJson); //Deserializes or converts JSON String into a collection of Post
        //        MessagesList = new ObservableCollection<MessageModel>(messages); //Converting the List to ObservalbleCollection of Post
        //        //await _client.PutAsync(Url, new StringContent(DbJson, Encoding.UTF8, "application/json")); //Send a POST request to the specified Uri as an asynchronous operation and with correct character encoding (utf9) and contenct type (application/json).
        //    }

        //}

        //Refresh the chat depending on the api messages (to show the received messages) , push to api and refresh the chat view
        private async Task ExecuteSendMessageToChat()
        {
            //ExecuteUpdateChat();//2Async();

            ExecuteUpdateChat();

            MessageModel message = new MessageModel { Title = MessageText }; //Creating a new instane of Post with a Title Property and its value in a Timestamp format
            content = JsonConvert.SerializeObject(message); //Serializes or convert the created Post into a JSON String
            DbJson = DbJson.TrimEnd(']');
            //str.Trim().Length

            if (DbJson.Trim().Length > 1)
            {
                DbJson += ("," + content + "]");
            }
            else
            {
                DbJson += (content + "]");
            }
            await _client.PutAsync(Url, new StringContent(DbJson, Encoding.UTF8, "application/json")); //Send a POST request to the specified Uri as an asynchronous operation and with correct character encoding (utf9) and contenct type (application/json).
            MessagesList.Add(message); //Updating the UI by inserting an element into the first index of the collection

            //firebase.
        }

        /// <inheritdoc />
        /// <summary>
        /// This method gets called before the UI appears.
        /// Async and await to get the value of the Task and for user experience
        /// </summary>
        public async Task ExecuteUpdateChat()
        {
            content = await _client.GetStringAsync(Url).ConfigureAwait(false); //Sends a GET request to the specified Uri and returns the response body as a string in an asynchronous operation

            //look if content contains ",null" and delete it (This is beacause there will be a "null" entry if some record is deleted manually from the database and then you update)
            if (content.Contains(",null"))
            {
                content = content.Replace(",null", "");
            }

            DbJson = content;

            if (DbJson == "null")
            {
                DbJson = "[]";
            }
            else
            {
                List<MessageModel> messages = JsonConvert.DeserializeObject<List<MessageModel>>(DbJson); //Deserializes or converts JSON String into a collection of Post
                MessagesList = new ObservableCollection<MessageModel>(messages); //Converting the List to ObservalbleCollection of Post
                //await _client.PutAsync(Url, new StringContent(DbJson, Encoding.UTF8, "application/json")); //Send a POST request to the specified Uri as an asynchronous operation and with correct character encoding (utf9) and contenct type (application/json).
            }

        }
    }
}
