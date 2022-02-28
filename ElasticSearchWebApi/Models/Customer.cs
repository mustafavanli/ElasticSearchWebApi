using Nest;

namespace ElasticSearchWebApi.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public GeoLocation Location { get; set; }
    }
}
