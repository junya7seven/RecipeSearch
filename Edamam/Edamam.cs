using Edamam.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace EdamamRequest
{
    public class EdamamAnswer
    {
        private ParametersModelEdamam dataParams {  get; set; } = new ParametersModelEdamam();
        private HttpClient client { get; set; } = new();
        private string Country {  get; set; }
        public EdamamAnswer(string country)
        {
            Country = country;
        }

        public async Task<RootObjectCook?> GetRecipe(string food)
        {
            string url = "";
            if(Country.Equals("World"))
            {
                url = $"{dataParams.Url}?type={dataParams.type}&q={food}&app_id={dataParams.appId}&app_key={dataParams.appKey}&field={dataParams.field}";

            }
            else
            {
                url = $"{dataParams.Url}?type={dataParams.type}&q={food}&app_id={dataParams.appId}&app_key={dataParams.appKey}&cuisineType={Country}&field={dataParams.field}";
            }
            try
            {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    request.Headers.Add("accept", "application/json");
                    request.Headers.Add("Accept-Language", "en");

                    var response = await client.SendAsync(request);
                    var rootObject = JsonConvert.DeserializeObject<RootObjectCook>(await response.Content.ReadAsStringAsync());
                    if (rootObject.hits != null && rootObject.hits.Any())
                    {
                        return rootObject;
                    }
                    else
                    {
                        throw new Exception("No recipes found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Recipe no found: {ex.Message}");
                return null;
            }

        }
    }
}
