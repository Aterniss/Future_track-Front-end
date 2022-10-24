namespace FoodDelivery.FrontEnd.Models
{
    public class Rider
    {
        public int RiderId { get; set; }
        public string RiderName { get; set; } = null!;
        public int ZoneId { get; set; }

        public virtual Zone Zone { get; set; } = null!;
    }
}
