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
                    if (order.opgehaald_issue=="0")
                    {
                        RitColor = Color.White;
                    }
                    else
                    {
                        RitColor = Color.Red;
                    }
                }
                else
                {
                    RitColor = Color.Green;
                }
            }
            else
            {
                if (order.afgeleverd == "0")
                {
                    if (order.afgeleverd_issue == "0")
                    {
                        RitColor = Color.White;
                    }
                    else
                    {
                        RitColor = Color.Red;
                    }
                }
                else
                {
                    RitColor = Color.Green;
                }
            }
        }

        public string Number { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }

        public Color RitColor { get; set; }
    }
}
