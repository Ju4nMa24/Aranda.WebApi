using System.Text.Json.Serialization;

namespace Aranda.Business.Commands.Categories
{
    public class CategoryAllCommand : Base.CommandRequest<CategoryAllResponse>
    {
    }
    public class CategoryAllResponse : Base.CommandResponse
    {
        [JsonPropertyName("StatusCode")]
        public string StatusCode { get; set; }
        [JsonPropertyName("Categories")]
        public dynamic Categories { get; set; }
    }
}
