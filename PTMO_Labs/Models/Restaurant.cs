namespace PTMO_Labs.Models
{
    public enum RestaurantTypes
    {
        SitDown,
        TakeOut,
        Delivery
    }

    public class Restaurant
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public RestaurantTypes Type { get; set; }
    }
}