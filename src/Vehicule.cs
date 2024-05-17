
namespace Maili
{
    public class Vehicule
    {
        public float PricePerKm { get; set; } = 0;
        public string Type { get; set; } = "";
        public Vehicule()
        {
            
        }

        public Vehicule(string type, float pricePerKm)
        {
            PricePerKm = pricePerKm;
            Type = type;
        }
    }
}