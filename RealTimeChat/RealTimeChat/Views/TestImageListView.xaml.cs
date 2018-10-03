using RealTimeChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealTimeChat.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TestImageListView : ContentPage
	{
		public TestImageListView (INavigation _navigation)
		{
            BindingContext = new TestImageListViewModelViewModel(_navigation);

			InitializeComponent ();
		}
	}
}