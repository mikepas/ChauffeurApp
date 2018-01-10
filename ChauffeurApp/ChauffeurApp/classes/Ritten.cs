using Xamarin.Forms;

namespace ChauffeurApp.classes
{
    class Ritten
    {
        public Ritten(RitOrders order, string type)
        {
            Number = order.Orderid;
            Address = order.Huisnummerbeginadres;
            Type = type;

            if (Type == "Ophalen")
            {
                if (order.opgehaald == "0")
                {
                    RitColor = order.opgehaald_issue=="0" ? Color.White : Color.Red;
                }
                else
                {
                    RitColor = Color.YellowGreen;
                }
            }
            else
            {
                if (order.afgeleverd == "0")
                {
                    RitColor = order.afgeleverd_issue == "0" ? Color.White : Color.Red;
                }
                else
                {
                    RitColor = Color.YellowGreen;
                }
            }
        }

        public string Number { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }

        public Color RitColor { get; set; }
    }
}
