using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaChat.Models.PostUrl
{
    public class Urls
    {
        public string AuthUrl { get; set; } = "https://ngw.devices.sberbank.ru:9443/api/v2/oauth";
        public string ModelUrl { get; set; } = "https://gigachat.devices.sberbank.ru/api/v1/models";
        public string MessageUrl { get; set; } = "https://gigachat.devices.sberbank.ru/api/v1/chat/completions";
        public string ContentType { get; set; } = "application/x-www-form-urlencoded";
        public string Accept { get; set; } = "application/json";
    }
}
