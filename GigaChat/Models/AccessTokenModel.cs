using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaChat.Models
{
    public class AccessTokenModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("expires_at")]
        public string ExpData { get; set; }
    }
    public enum Scope
    {
        /// <summary>
        /// Общедоступный
        /// </summary>
        GIGACHAT_API_PERS,
        /// <summary>
        /// Коорпоративный
        /// </summary>
        GIGACHAT_API_CORP
    }
}
