using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChauffeurApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPage : ContentPage
    {
        private readonly string _id;
        private readonly string _ophaalAddress;
        private readonly string _afleverAddress;
        private readonly string _opgehaald;
        private readonly string _opgehaaldIssue;
        private readonly string _opgehaaldOmschrijving;
        private readonly string _afgeleverd;
        private readonly string _afgeleverdIssue;
        private readonly string _afgeleverdOmschrijving;

        public OrderPage(string id, string ophaalAddress, string afleverAddress, string opgehaald, string opgehaaldIssue, string opgehaaldOmschrijving, string afgeleverd, string afgeleverdIssue, string afgeleverdOmschrijving)
        {
            _id = id;
            _ophaalAddress = ophaalAddress;
            _afleverAddress = afleverAddress;
            _opgehaald = opgehaald;
            _opgehaaldIssue = opgehaaldIssue;
            _opgehaaldOmschrijving = opgehaaldOmschrijving;
            _afgeleverd = afgeleverd;
            _afgeleverdIssue = afgeleverdIssue;
            _afgeleverdOmschrijving = afgeleverdOmschrijving;
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
            if (UpdateRit())
            {
                base.OnBackButtonPressed();
            }
            return true;
        }

        private void SetLabels()
        {
            lbOrderNumber.Text = _id;

            lbOphaalAdres.Text = _ophaalAddress;
            lbAfleverAdres.Text = _afleverAddress;

            if (_opgehaald == "1")
                switchOpgehaald.IsToggled = true;

            if (_afgeleverd == "1")
                switchAfgeleverd.IsToggled = true;

            if (_opgehaaldIssue == "1")
            {
                switchIssue1.IsToggled = true;
                entryIssue1.Text = _opgehaaldOmschrijving;
            }

            if (_afgeleverdIssue == "1")
            {
                switchIssue2.IsToggled = true;
                entryIssue2.Text = _afgeleverdOmschrijving;
            }
        }

        private bool UpdateRit()
        {
            var client = new HttpClient();

            var opgehaald = "0";
            var afgehaald = "0";
            var issueOpgehaald = "0";
            var issueAfgehaald = "0";

            if (switchOpgehaald.IsToggled)
            {
                opgehaald = "1";
            }

            if (switchAfgeleverd.IsToggled)
            {
                afgehaald = "1";
            }

            if (switchIssue1.IsToggled)
            {
                issueOpgehaald = "1";
            }

            if (switchIssue2.IsToggled)
            {
                issueAfgehaald = "1";
            }

            try
            {
                if (issueOpgehaald == "1" && entryIssue1.Text == "")
                {
                    DisplayAlert("Leeg veld!", "Vul een opmerking in!", "Ok");
                    return false;
                }

                if (issueAfgehaald == "1" && entryIssue2.Text == "")
                {
                    DisplayAlert("Leeg veld!", "Vul een opmerking in!", "Ok");
                    return false;
                }

                var url = "http://webdesignwolters.nl/snelle-wiel/planningssysteem/api/updaterit/" + _id + "/" + opgehaald +
                          "/" + issueOpgehaald + "/" + entryIssue1.Text + "/" + afgehaald +
                          "/" + issueAfgehaald + "/" + entryIssue2.Text;
                client.GetStringAsync(url);
                DisplayAlert("Opgeslagen!", "De gegevens zijn opgeslagen!", "Ok");
            }
            catch (Exception)
            {
                if (DisplayAlert("Geen verbinding!", "Zet uw WIFI of mobiele data aan.\n\nOpnieuw proberen?", "ja", "niet opslaan").Result)
                {
                    UpdateRit();
                }
                return false;
            }
            return true;
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
                switchAfgeleverd.IsToggled = false;
                switchAfgeleverd.IsEnabled = false;
                entryIssue1.IsEnabled = true;
                switchOpgehaald.IsToggled = false;
                entryIssue1.Focus();
            }
            else if (e.Value == false)
            {
                switchAfgeleverd.IsEnabled = true;
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