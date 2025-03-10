using Newtonsoft.Json;

namespace MindSpace.Application.DTOs.Chat
{
    public sealed class Content
    {
        public Part[] Parts { get; set; }

        [JsonIgnore]
        public string Role { get; set; }  // Only used in responses, ignored in requests when serializing
    }
}
