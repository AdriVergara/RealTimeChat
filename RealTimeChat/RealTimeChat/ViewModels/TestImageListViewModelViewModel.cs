using RealTimeChat.Models;
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
    public class TestImageListViewModelViewModel : INotifyPropertyChanged
    {
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<MessageModel> MessageList { get; set; }

        public INavigation NavigationService { get; set; }
        public ICommand test { get; set; }

        public TestImageListViewModelViewModel(INavigation _navigationService)
        {
            NavigationService = _navigationService;

            InitilizeMessageList();

            test = new Command(async () => await ExecuteTest());
        }

        private async Task ExecuteTest()
        {
            //throw new NotImplementedException();
        }

        private async Task InitilizeMessageList()
        {
            ObservableCollection<MessageModel> auxList = new ObservableCollection<MessageModel>();

            //MessageModel m1 = new MessageModel("Title1", "Owner1", "sendMessage.png");
            //MessageModel m2 = new MessageModel("Title2", "Owner2", "TakePicture.png");
            //MessageModel m3 = new MessageModel("Title3", "Owner3");

            //auxList.Add(m1);
            //auxList.Add(m2);
            //auxList.Add(m3);

            MessageList = auxList;
        }
    }
}
