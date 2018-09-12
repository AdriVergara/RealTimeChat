using System;
using System.Collections.Generic;
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
        ListViewPushNotificationsViewModel db;

		public ListViewPushNotificationsView (INavigation _navigation)
		{
			InitializeComponent ();

            //BindingContext = new ListViewPushNotificationsViewModel(_navigation);

            db = new ListViewPushNotificationsViewModel(_navigation);
            MyListView.BindingContext = db.getList();
        }
	}
}