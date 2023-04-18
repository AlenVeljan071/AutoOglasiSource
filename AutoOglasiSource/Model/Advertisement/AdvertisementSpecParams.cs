using Newtonsoft.Json;

namespace AutoOglasiSource.Model.Advertisement
{
    public class AdvertisementSpecParams
    {
        [JsonProperty("pageIndex")]
        public int? PageIndex { get; set; }

        [JsonProperty("applicationUserId")]
        public int? ApplicationUserId { get; set; }

        [JsonProperty("priceFrom")]
        public decimal? PriceFrom { get; set; }

        [JsonProperty("priceTo")]
        public decimal? PriceTo { get; set; }

        [JsonProperty("mileageFrom")]
        public decimal? MileageFrom { get; set; }

        [JsonProperty("mileageTo")]
        public decimal? MileageTo { get; set; }

        [JsonProperty("sort")]
        public string Sort { get; set; }

        [JsonProperty("search")]
        public string Search { get; set; }
    }
}

