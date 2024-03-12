using RecipeSearch.Models;
using RecipeSearch.Service.Interface;
using System.Diagnostics.Metrics;

namespace RecipeSearch.Service
{
    public class AnswerService
    {
        private readonly IGigaChatService _chatService;
        private readonly IEdamamService _edamamService;
        private AnswerRecipe AnswerRecipe { get; set; } = new();
        //private AnswerGigaChat AnswerGigaChat { get; set; } = new();
        public AnswerService(IGigaChatService chatService, IEdamamService emaService)
        {
            _chatService = chatService;
            _edamamService = emaService;
        }

        public async Task<AnswerRecipe> ResponseService(string inputString,string Country, string Promt, bool checkBox)
        {
            if(checkBox)
            {
                return await ResponseServiceEdamam(inputString, Promt, Country);
            }
            else
            {
                return await ResponseServiceGiga(inputString, Promt);
            }
        }
        public async Task<AnswerRecipe> ResponseServiceGiga(string inputString,string Promt)
        {
            
            Promt = " ";
            var answerGigaChat = await _chatService.GetAnswer(inputString, Promt);
            if(answerGigaChat.Choices !=  null)
            {
                foreach (var item in answerGigaChat.Choices)
                {
                    AnswerRecipe.AnswerAPI.Add(item.Message.Content);
                }
            }
            return AnswerRecipe;
        }


        public async Task<AnswerRecipe> ResponseServiceEdamam(string inputString, string Promt, string Country)
        {
            inputString += $".СТРАНА БЛЮДА: {Country}";
            string answer = "";
            var answerGigaChat = await _chatService.GetAnswer(inputString, Promt);
            if(answerGigaChat.Choices != null)
            {
                foreach (var item in answerGigaChat.Choices)
                {
                    answer += item.Message.Content;
                }
                Console.WriteLine(answer);
                if (answerGigaChat != null)
                {
                    AnswerRecipe = await _edamamService.GetFirstRecipe(answer, Country);
                }
                else
                {
                    AnswerRecipe.AnswerAPI.Add("No Found");
                }
            }
            return AnswerRecipe;
        }
    }
}
