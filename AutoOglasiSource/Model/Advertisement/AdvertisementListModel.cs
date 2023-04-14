using AutoOglasiSource.Responses;

namespace AutoOglasiSource.Model.Advertisement
{
    public class AdvertisementListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Year { get; set; }
        public AddressEntity Address { get; set; }
        public ImageResponse Image { get; set; }
    }
}
