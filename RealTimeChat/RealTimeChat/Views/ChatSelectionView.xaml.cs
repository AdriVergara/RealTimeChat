using RealTimeChat.Models;
using RealTimeChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealTimeChat.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChatSelectionView : ContentPage
	{
		public ChatSelectionView (INavigation _navigationService, UserModel _user, ObservableCollection<UserModel> _usersList)
		{
			InitializeComponent ();

            BindingContext = new ChatSelectionViewModel(_navigationService, _user, _usersList);
		}
	}
}