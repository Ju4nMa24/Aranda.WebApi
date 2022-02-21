using System.Text.Json.Serialization;

namespace Aranda.Business.Commands.Base
{
    public abstract class CommandResponse
    {
        public CommandResponseContext InnerContext { get; set; } = new CommandResponseContext();
    }

    public class CommandResponseContext
    {
        [JsonPropertyName("Result")]
        public Result Result { get; set; } = new Result();
    }

    public class Result
    {
        [JsonPropertyName("Success")]
        public bool Success { get; set; }
        [JsonPropertyName("ReasonCode")]
        public string ReasonCode { get; set; }
        [JsonPropertyName("ReasonPhrase")]
        public string ReasonPhrase { get; set; }
        [JsonPropertyName("Details")]
        public dynamic Details { get; set; }
    }
}
