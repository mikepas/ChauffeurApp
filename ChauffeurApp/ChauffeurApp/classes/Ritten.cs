namespace ChauffeurApp.classes
{
    class Ritten
    {
        public Ritten(string number, string address, string type)
        {
            Number = number;
            Address = address;
            Type = type;
        }

        public string Number { get; set; }
        public string Address { get; set; }
        public string Type { get; set; }
    }
}
