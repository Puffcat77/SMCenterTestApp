using Newtonsoft.Json;

namespace SMCenterTestApp.ListHelpers
{
    public class ListProperties
    {
        [JsonProperty("useOrder")]
        public bool UseOrder { get; set; }

        [JsonProperty("orderByDescending")]
        public bool OrderByDescending { get; set; }

        [JsonProperty("pageNumber")]
        public int PageNumber { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("orderProperty")]
        public string OrderProperty { get; set; }
    }
}