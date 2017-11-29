using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChauffeurApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DashboardPage : ContentPage
	{
		public DashboardPage ()
		{
			InitializeComponent ();

		    //imageBack.GestureRecognizers.Add(new TapGestureRecognizer(OnTap));

		    var imageLogout = new TapGestureRecognizer();
		    imageLogout.Tapped += (s, e) =>
		    {
		        LogOut();
		    };
		    imageBack.GestureRecognizers.Add(imageLogout);

            //var forgetPasswordTap = new TapGestureRecognizer();
		    //forgetPasswordTap.Tapped += (s, e) =>
		    //{
		        //Navigation.PushModalAsync(new OrderPage());
		    //};
            //Label.GestureRecognizers.Add(forgetPasswordTap);


		    listView.ItemsSource = new[] {
		        new TodoItem { Number = "1", Address = "Doormanlaan 50, Son en Breugel", Type = "Ophalen" },
		        new TodoItem { Number = "1", Address = "Apollolaan 31, Son en Breugel", Type = "Afleveren" },
		        new TodoItem { Number = "2", Address = "Doormanlaan 8, Son en Breugel", Type = "Ophalen" },
		        new TodoItem { Number = "2", Address = "Apollolaan 15, Son en Breugel", Type = "Afleveren" }
		    };
        }

	    //private void OnTap(View arg1, object arg2)
	    //{
	        //LogOut();
	    //}

        protected override bool OnBackButtonPressed()
	    {
	        LogOut();
	        return true;
	    }

	    private void LogOut()
	    {
	        Device.BeginInvokeOnMainThread(async () =>
	        {
	            if (!await DisplayAlert("Uitloggen?", "Weet u zeker dat u uit wilt loggen?", "Ja", "Nee")) return;
	            base.OnBackButtonPressed();
	        });
        }

	    private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
	        var passingItems = new List<object>();

            var listview = (ListView) sender;
	        var selectedItem = (TodoItem)listview.SelectedItem;

            foreach (var item in listview.ItemsSource)
	        {
	            var castItem = (TodoItem)item;

	            if (castItem.Number.Equals(selectedItem.Number))
	            {
	                passingItems.Add(castItem);
	            }
	        }

	        Navigation.PushModalAsync(new OrderPage(passingItems));
        }
	}

    public class TodoItem
    {
        public string Number { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
    }
}