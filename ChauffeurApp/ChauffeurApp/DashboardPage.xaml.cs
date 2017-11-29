using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using ChauffeurApp.classes;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChauffeurApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DashboardPage : ContentPage
	{
	    private readonly List<Ritten> _orders = new List<Ritten>();

        public DashboardPage (string id, string naam)
		{
		    InitializeComponent ();

		    lbChauffeurName.Text = "Welkom " + naam;

		    var imageLogout = new TapGestureRecognizer();
		    imageLogout.Tapped += (s, e) =>
		    {
		        LogOut();
		    };
		    imageBack.GestureRecognizers.Add(imageLogout);

            GetRitOrders(id);
        }

	    public async void GetRitOrders(string id)
	    {
	        HttpClient client = new HttpClient();
	        var result = await client.GetStringAsync("http://webdesignwolters.nl/snelle-wiel/admin/api/getordersfromrit/" + id);

	        try
	        {
	            var trimmedResult = result.Replace(" ","");
	            var ritOrders = JsonConvert.DeserializeObject<List<RitOrders>>(trimmedResult);

                foreach (var ritOrder in ritOrders)
	            {
                    _orders.Add(new Ritten(ritOrder.Orderid, ritOrder.Postcodebeginbestemming, "Ophalen"));
                    _orders.Add(new Ritten(ritOrder.Orderid, ritOrder.Postcodeeindbestemming, "Afleveren"));
	            }

	            listView.ItemsSource = _orders;
	        }
	        catch (Exception)
	        {
	            if (!await DisplayAlert("Geen gegevens!", "Er konden geen gegevens worden opgehaald.\nProbeer opnieuw!", "ja", "nee")) return;
                GetRitOrders(id);
            }
	    }

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
	        var selectedItem = (Ritten)listview.SelectedItem;

            foreach (var item in listview.ItemsSource)
	        {
	            var castItem = (Ritten)item;

	            if (castItem.Number.Equals(selectedItem.Number))
	            {
	                passingItems.Add(castItem);
	            }
	        }

	        Navigation.PushModalAsync(new OrderPage(passingItems));
        }
	}
}