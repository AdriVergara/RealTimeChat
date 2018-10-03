using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealTimeChat.Models;
using RealTimeChat.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealTimeChat.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListViewPushNotificationsView : ContentPage
	{
		public ListViewPushNotificationsView (INavigation _navigation, UserModel _user, ObservableCollection<UserModel> _usersList)
		{
			InitializeComponent ();

            BindingContext = new Database3(_navigation, _user, _usersList);
        }
	}
}