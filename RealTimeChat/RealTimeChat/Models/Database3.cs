using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RealTimeChat.Models
{
    public class Database3
    {
        public Database3()
        {

        }

        public ObservableCollection<MessageModel> test()
        {
            var firebase = new FirebaseClient("https://realtimechat-b2228.firebaseio.com/");

            var lists = firebase.Child("Chat")
                .AsObservable<MessageModel>()
                .AsObservableCollection();

            return lists;
        }

        public ObservableCollection<MessageModel> getMessage()
        {
            var firebase = new FirebaseClient("https://realtimechat-b2228.firebaseio.com/");

            var lists = firebase.Child("Chat")
                .AsObservable<MessageModel>()
                .AsObservableCollection();

            return lists;
        }
    }
}
