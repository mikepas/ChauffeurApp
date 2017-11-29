using System;
using System.Collections.Generic;
using System.Net.Http;
using ChauffeurApp.classes;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChauffeurApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPage : ContentPage
    {
        private readonly List<object> _items;

        public OrderPage(List<object> items)
        {
            _items = items;
            InitializeComponent();

            imageMap.Source = ImageSource.FromResource("ChauffeurApp.loading_apple.gif");

            var imageBackGestureRecognizer = new TapGestureRecognizer();
            imageBackGestureRecognizer.Tapped += (s, e) =>
            {
                OnBackButtonPressed();
            };
            imageBack.GestureRecognizers.Add(imageBackGestureRecognizer);

            SetLabels();
            SetMapImage();
        }
        protected override bool OnBackButtonPressed()
        {
            UpdateRit();
            return true;
        }

        private void SetLabels()
        {
            foreach (var item in _items)
            {
                var orderItem = (Ritten)item;

                lbOrderNumber.Text = orderItem.Number;

                if (orderItem.Type == "Ophalen")
                    lbOphaalAdres.Text = orderItem.Address;
                else if (orderItem.Type == "Afleveren")
                {
                    lbAfleverAdres.Text = orderItem.Address;
                }
            }
        }

        private async void UpdateRit()
        {
            HttpClient client = new HttpClient();

            var entryIssue1Text = entryIssue1.Text;
            var entryIssue2Text = entryIssue2.Text;

            if (entryIssue1Text == null)
            {
                entryIssue1Text = "null";
            }

            if (entryIssue2Text == null)
            {
                entryIssue2Text = "null";
            }

            try
            {
                var url = "http://webdesignwolters.nl/snelle-wiel/admin/api/updaterit/" + switchOpgehaald.IsToggled +
                          "/" + switchIssue1.IsToggled + "/" + entryIssue1Text + "/" + switchAfgeleverd.IsToggled +
                          "/" + switchIssue2.IsToggled + "/" + entryIssue2Text;
                await client.GetStringAsync(url);

                base.OnBackButtonPressed();
            }
            catch (Exception)
            {
                if (await DisplayAlert("Geen verbinding!", "Zet uw WIFI of mobiele data aan.\n\nOpnieuw proberen?", "ja", "nee"))
                {
                    UpdateRit();
                }
                else
                {
                    base.OnBackButtonPressed();
                }
            }
        }

        private void SetMapImage()
        {
            var ophaalAdres = lbOphaalAdres.Text.Replace(",", "").Replace(" ", "+");
            var afleverAdres = lbAfleverAdres.Text.Replace(",", "").Replace(" ", "+");
            var url = "http://maps.googleapis.com/maps/api/staticmap?markers=color:blue|label:ophaaladres|" + ophaalAdres + "&markers=color:green|label:afleveradres|" + afleverAdres + "&size=400x300";
            imageMap.Source = url;
        }

        private void SwitchIssue1_OnToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                entryIssue1.IsEnabled = true;
                switchOpgehaald.IsToggled = false;
                entryIssue1.Focus();
            }
            else if (e.Value == false)
            {
                entryIssue1.IsEnabled = false;
                entryIssue1.Text = "";
            }
        }

        private void SwitchIssue2_OnToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                entryIssue2.IsEnabled = true;
                switchAfgeleverd.IsToggled = false;
                entryIssue2.Focus();
            }
            else if (e.Value == false)
            {
                entryIssue2.IsEnabled = false;
                entryIssue2.Text = "";
            }
        }

        private void SwitchOpgehaald_OnToggled(object sender, ToggledEventArgs e)
        {
            if (!e.Value) return;
            switchIssue1.IsToggled = false;
            entryIssue1.Text = "";
        }

        private void SwitchAfgeleverd_OnToggled(object sender, ToggledEventArgs e)
        {
            if (!e.Value) return;
            switchIssue2.IsToggled = false;
            entryIssue2.Text = "";
        }
    }
}