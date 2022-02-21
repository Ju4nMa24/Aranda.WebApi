using Aranda.Common.Generics;
using Aranda.Common.Types.Products;
using System;
using System.Text.Json.Serialization;

namespace Aranda.Business.Commands.Products
{
    public class TransactionalProductCommand : Base.CommandRequest<TransactionalProductResponse>
    {
        [JsonPropertyName("Product")]
        public Product ProductRequest { get; set; }
        [JsonPropertyName("CategoryId")]
        public Guid CategoryId { get; set; }
        [JsonPropertyName("ProccessType")]
        public Proccesors ProccessType { get; set; }
    }
    public class TransactionalProductResponse : Base.CommandResponse
    {
        [JsonPropertyName("StatusCode")]
        public string StatusCode { get; set; }
        [JsonPropertyName("ProductList")]
        public dynamic ProductList { get; set; }
        [JsonPropertyName("Product")]
        public Product ProductResponse { get; set; }
        [JsonPropertyName("ProductImages")]
        public dynamic ProductImages { get; set; }
    }
}
