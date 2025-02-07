using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MindSpace.Application.Features.Authentication.DTOs
{
    public class LoginResponseDTO
    {
        [JsonPropertyName("id_token")]
        public string IdToken { get; set; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = "Bearer";
    }
}
