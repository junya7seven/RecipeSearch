using GigaChat.Models;
using GigaChatRequest;
using RecipeSearch.Service.Interface;

namespace RecipeSearch.Service.Logic
{
    public class GigaChatService : IGigaChatService
    {
        public async Task<RootObjectGigaChat> GetAnswer(string inputString, string promt)
        {
            GigaChatAnswer giga = new GigaChatAnswer(Scope.GIGACHAT_API_PERS);
            return await giga.SendMessage(inputString, promt);
        }
    }
}
