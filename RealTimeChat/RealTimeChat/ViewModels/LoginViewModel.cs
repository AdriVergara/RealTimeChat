using Newtonsoft.Json;
using RealTimeChat.Models;
using RealTimeChat.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RealTimeChat.ViewModels
{
    public class LoginViewModel
    {
        

        public INavigation NavigationService { get; set; }

        public ICommand ConfirmUser { get; set; }

        public ObservableCollection<UserModel> UsersList;

        public string UserName { get; set; }
        public string Password { get; set; }

        public LoginViewModel(INavigation _navigationService)
        {
            InitializeUsers();
            //LoadFirebaseDatabase();

            NavigationService = _navigationService;
            ConfirmUser = new Command(async () => await ExecuteConfirmUser());
        }

        //private async Task LoadFirebaseDatabase()
        //{
        //    string content = await _client.GetStringAsync(Url); //Sends a GET request to the specified Uri and returns the response body as a string in an asynchronous operation
        //    List<MessageModel> messages = JsonConvert.DeserializeObject<List<MessageModel>>(content); //Deserializes or converts JSON String into a collection of Post
        //    MessagesList = new ObservableCollection<MessageModel>(messages); //Converting the List to ObservalbleCollection of Post
        //    //MyListView.ItemsSource = _posts; //Assigning the ObservableCollection to MyListView in the XAML of this file
        //    //base.OnAppearing();
        //}

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

        private async Task ExecuteConfirmUser()
        {
            UserModel us = new UserModel();

            //if (UserName == "Adri")
            //{
            //    us = UsersList[0];
            //}
            //else if(UserName == "Andreu")
            //{
            //    us = UsersList[1];
            //}

            await NavigationService.PushAsync(new LiveChatView(NavigationService, us, UsersList));
        }
    }
}
