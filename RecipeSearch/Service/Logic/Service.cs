using RecipeSearch.Models;
using RecipeSearch.Service.Interface;

namespace RecipeSearch.Service
{
    public class AnswerService
    {
        private readonly IGigaChatService _chatService;
        private readonly IEdamamService _edamamService;
        private AnswerRecipe AnswerRecipe { get; set; } = new();
        public AnswerService(IGigaChatService chatService, IEdamamService emaService)
        {
            _chatService = chatService;
            _edamamService = emaService;
        }

        public async Task<AnswerRecipe> ResponseServiceEdamam(string inputString,string Country, string Promt, bool checkBox)
        {
            if (!checkBox)
            {
                Promt = " ";
                string answerGigaChat = await _chatService.GetAnswer(inputString, Promt);
                Console.WriteLine(answerGigaChat);
                return AnswerRecipe;
            }
            else
            {
                inputString += $"Страна блюда{Country}";
                string answerGigaChat = await _chatService.GetAnswer(inputString, Promt);
                Console.WriteLine(answerGigaChat);
                if (answerGigaChat != null)
                {
                    AnswerRecipe = await _edamamService.GetFirstRecipe(answerGigaChat, Country);
                }
                else
                {
                    AnswerRecipe.Recipe.Add("No Found");
                }
                return AnswerRecipe;
            }
        }
    }
}
