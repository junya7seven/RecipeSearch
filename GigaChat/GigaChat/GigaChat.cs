using GigaChat.Models;
using GigaChat.Models.PostUrl;
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
        private string ClientSecret {  get; set; }
        private string AuthData { get; set; }
        private Scope Scope { get; set; }
        private AccessTokenModel AccessTokenModels { get; set; }
        private ModelList? modelList { get; set; }
        private HttpClient client { get; set; }
        private Urls url { get; set; }
        

        public GigaChatAnswer(string clientSecret, string authData, Scope scope)
        {
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(delegate { return true; });
            ClientSecret = clientSecret;
            AuthData = authData;
            Scope = scope;
        }

        // Authorization - get token
        public async Task Authorize()
        {
            try
            {
                using( client = new HttpClient())
                {
                    using (HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "https://ngw.devices.sberbank.ru:9443/api/v2/oauth"))
                    {
                        message.Headers.Add("Authorization", $"Bearer {AuthData}");
                        message.Headers.Add("RqUID", ClientSecret);
                        message.Content = new StringContent($"scope={Scope}");
                        message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                        var response = await client.SendAsync(message);
                        var data = await response.Content.ReadAsStringAsync();
                        AccessTokenModels = JsonConvert.DeserializeObject<AccessTokenModel>(data);
                        if(AccessTokenModels.AccessToken == null)
                        {
                            throw new Exception("Error 401: no authorization");
                        }
                        else
                        {
                            Console.WriteLine("Ok 200");
                        }
                        client.Dispose();
                        message.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error 401: no authorization {ex.Message}");
            }
        }


        // Get models
        public async Task GetModels()
        {
            try
            {
                using (client = new HttpClient())
                {
                    using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://gigachat.devices.sberbank.ru/api/v1/models"))
                    {
                        if (AccessTokenModels != null)
                        {
                            request.Headers.Add("Accept", "application/json");
                            request.Headers.Add("Authorization", $"Bearer {AccessTokenModels.AccessToken}");
                            var response = await client.SendAsync(request);
                            modelList = JsonConvert.DeserializeObject<ModelList>(await response.Content.ReadAsStringAsync());
                            Console.WriteLine("Ok 200");
                        }
                        else
                        {
                            throw new Exception("Eror 401: no authorization");
                        }
                        client.Dispose(); 
                        request.Dispose(); 
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error 401: no authorization {ex.Message}");
            }
        }

        // Send question
        public async Task<RootObjectGigaChat?> SendMessage(string GigaRequest, string promt)
        {
            await Authorize();
            await GetModels();
            ParametersModel dataParams = new ParametersModel();

            try
            {
                dataParams.Messages = dataParams.Messages.Append(new MessageQuery() { Role = "user", Content = GigaRequest }).ToArray();
                if (AccessTokenModels == null)
                {
                    throw new Exception($"Error 401 Unauthorized Token");
                }
                else
                {
                    using (client = new HttpClient())
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
                            client.Dispose();
                            request.Dispose();
                            return data;
                        }
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
