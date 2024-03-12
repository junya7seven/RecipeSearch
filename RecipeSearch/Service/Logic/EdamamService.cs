using Edamam.Models;
using EdamamRequest;
using RecipeSearch.Models;
using RecipeSearch.Service.Interface;

namespace RecipeSearch.Service.Logic
{
    public class EdamamService : IEdamamService
    {
        public async Task<RootObjectCook?> GetAllRecipe(string food, string country)
        {
            EdamamAnswer edamam = new EdamamAnswer(country);
            return await edamam.GetRecipe(food);
        }
        public async Task<AnswerRecipe?> GetFirstRecipe(string food, string country)
        {
            RootObjectCook rootObjectCook = await GetAllRecipe(food,country);
            AnswerRecipe answerRecipe = new AnswerRecipe();
            if(rootObjectCook != null)
            {
                foreach (var item in rootObjectCook.hits)
                {
                    foreach (var item2 in item.Recipe.IngredientLines)
                    {
                        answerRecipe.AnswerAPI.Add(item2);
                    }
                    break;
                }
                return answerRecipe;
            }
            else
            {
                return answerRecipe;
            }
        }
    }
}
