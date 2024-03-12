using Edamam.Models;
using GigaChat.Models;
using RecipeSearch.Models;

namespace RecipeSearch.Service.Interface
{
    public interface IGigaChatService
    {
        Task<RootObjectGigaChat> GetAnswer(string inputString, string promt);
    }
    public interface IEdamamService: IEdamamServiceGetModel
    {
        Task<RootObjectCook?> GetAllRecipe(string food, string country);
    }
    public interface IEdamamServiceGetModel
    {
        Task<AnswerRecipe> GetFirstRecipe(string food, string country);
    }
}
