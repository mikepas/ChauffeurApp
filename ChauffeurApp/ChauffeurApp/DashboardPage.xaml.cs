using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
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

		    var imageRefresher = new TapGestureRecognizer();
		    imageRefresher.Tapped += (s, e) =>
		    {
                listView.ItemsSource = null;
                _orders.Clear();
		        GetRitOrders(id);
		    };
		    imageRefresh.GestureRecognizers.Add(imageRefresher);

            GetRitOrders(id);
        }

        public async void GetRitOrders(string id)
	    {
	        HttpClient client = new HttpClient();
	        var result = await client.GetStringAsync("http://webdesignwolters.nl/snelle-wiel/planningssysteem/api/getordersfromrit/" + id);

	        try
	        {
	            var trimmedResult = result.Replace("Order id","Orderid");
	            trimmedResult = trimmedResult.Replace("Huisnummer beginadres", "Huisnummerbeginadres");
	            trimmedResult = trimmedResult.Replace("Huisnummer eindbestemming", "Huisnummereindbestemming");
	            trimmedResult = trimmedResult.Replace("Gebruiker id","gebruikerid");
	            var ritOrders = JsonConvert.DeserializeObject<List<RitOrders>>(trimmedResult);

                foreach (var ritOrder in ritOrders)
	            {
                    _orders.Add(new Ritten(ritOrder, "Ophalen"));
                    _orders.Add(new Ritten(ritOrder, "Afleveren"));
	            }

	            listView.ItemsSource = _orders;
	        }
	        catch (Exception)
	        {
	            if (!await DisplayAlert("Geen gegevens!", "Er konden geen gegevens worden opgehaald.\n\nProbeer opnieuw!", "ja", "nee")) return;
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

	    private async void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
	    {
            var listview = (ListView) sender;
	        var selectedItem = (Ritten)listview.SelectedItem;

	        try
	        {
	            var client = new HttpClient();
	            var request = "http://webdesignwolters.nl/snelle-wiel/planningssysteem/api/getorders/" + selectedItem.Number;
                var result = await client.GetStringAsync(request);
                
                var trimmedResult = result.Replace("Order id", "Orderid");
	            trimmedResult = trimmedResult.Replace("Huisnummer beginadres", "Huisnummerbeginadres");
	            trimmedResult = trimmedResult.Replace("Huisnummer eindbestemming", "Huisnummereindbestemming");
	            trimmedResult = trimmedResult.Replace("Gebruiker id", "gebruikerid");
                var ritOrders = JsonConvert.DeserializeObject<List<RitOrders>>(trimmedResult);

	            foreach (var ritOrder in ritOrders)
	            {
	                await Navigation.PushModalAsync(new OrderPage(ritOrder.Orderid, ritOrder.Huisnummerbeginadres, ritOrder.Huisnummereindbestemming, ritOrder.opgehaald, ritOrder.opgehaald_issue, ritOrder.opgehaald_opmerking, ritOrder.afgeleverd, ritOrder.afgeleverd_issue, ritOrder.afgeleverd_opmerking));
	            }
	        }
	        catch (Exception)
	        {
	            if (!await DisplayAlert("Geen gegevens!", "Er konden geen gegevens worden opgehaald.\n\nProbeer opnieuw!", "ja", "nee")) return;
	            ListView_OnItemTapped(sender, e);
	        }
        }
	}
}