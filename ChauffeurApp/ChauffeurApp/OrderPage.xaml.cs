using System.Collections.Generic;
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

        private void SetLabels()
        {
            foreach (var item in _items)
            {
                var orderItem = (TodoItem)item;

                lbOrderNumber.Text = orderItem.Number;

                if (orderItem.Type == "Ophalen")
                    lbOphaalAdres.Text = orderItem.Address;
                else if (orderItem.Type == "Afleveren")
                {
                    lbAfleverAdres.Text = orderItem.Address;
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