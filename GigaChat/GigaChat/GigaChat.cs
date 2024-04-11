using GigaChat.Models;
using GigaChat.Models.Codes;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace GigaChatRequest
{
    public class GigaChatAnswer
    {
        private Scope Scope { get; set; }
        private AccessTokenModel? AccessTokenModels { get; set; }
        private HttpClient client { get; set; } = new();
        private ApiCodes apiCodes { get; set; } = new();
        

        public GigaChatAnswer(Scope scope)
        {
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });
            Scope = scope;
        }

        // Authorization - get token
        public async Task Authorize()
        {
            try
            {
                using (HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "https://ngw.devices.sberbank.ru:9443/api/v2/oauth"))
                {
                    message.Headers.Add("Authorization", $"Bearer {apiCodes.Auth}");
                    message.Headers.Add("RqUID", apiCodes.ClientSecret);
                    message.Content = new StringContent($"scope={Scope}");
                    message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                    var response = await client.SendAsync(message);
                    var data = await response.Content.ReadAsStringAsync();
                    AccessTokenModels = JsonConvert.DeserializeObject<AccessTokenModel>(data);
                    if (AccessTokenModels.AccessToken == null)
                    {
                        throw new Exception("Error 401: no authorization");
                    }
                    else
                    {
                        Console.WriteLine("Ok 200");
                    }
                    message.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error 401: no authorization {ex.Message}");
            }
        }

        // Send question
        public async Task<RootObjectGigaChat> SendMessage(string GigaRequest, string promt)
        {
            await Authorize();
            ParametersModel dataParams = new ParametersModel();
            GigaRequest += promt;
            string answer = "";
            try
            {
                dataParams.Messages = dataParams.Messages.Append(new MessageQuery() { Role = "user", Content = GigaRequest }).ToArray();
                if (AccessTokenModels == null)
                {
                    throw new Exception($"Error 401 Unauthorized Token");
                }
                else
                {
                    using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://gigachat.devices.sberbank.ru/api/v1/chat/completions"))
                    {
                        request.Headers.Add("Accept", "application/json");
                        request.Headers.Add("Authorization", $"Bearer {AccessTokenModels.AccessToken}");
                        request.Content = new StringContent(JsonConvert.SerializeObject(dataParams), Encoding.UTF8);
                        var response = await client.SendAsync(request);
                        var data = JsonConvert.DeserializeObject<RootObjectGigaChat>(await response.Content.ReadAsStringAsync());
                        if (data == null)
                        {
                            throw new Exception($"Error 500 Internal Server Error");
                        }

                        request.Dispose();
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error 401: no authorization {ex.Message}");
                return null;
            }
        }
    }
}
