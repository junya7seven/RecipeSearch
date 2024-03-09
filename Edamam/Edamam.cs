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
        private string Calories { get; set; }
        public EdamamAnswer(string country,string calories)
        {
            Country = country;
            Calories = calories;
        }
        public EdamamAnswer(string country)
        {
            Country = country;
        }

        public async Task<RootObjectCook?> GetRecipe(string food)
        {
            string url = $"{dataParams.Url}?type={dataParams.type}&q={food}&app_id={dataParams.appId}&app_key={dataParams.appKey}&ingr={dataParams.ingr}&diet={dataParams.diet}&cuisineType={Country}&mealType={dataParams.mealType}&calories={Calories}&field={dataParams.field}";
            try
            {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    request.Headers.Add("accept", "application/json");
                    request.Headers.Add("Accept-Language", "en"); // Русский не поддерживается

                    var response = await client.SendAsync(request);
                    var rootObject = JsonConvert.DeserializeObject<RootObjectCook>(await response.Content.ReadAsStringAsync());
                    if (rootObject.hits != null && rootObject.hits.Any())
                    {
                        return rootObject;
                    }
                    else
                    {
                        Console.WriteLine("No recipes found.");
                        return null;
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
