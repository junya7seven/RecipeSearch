using GigaChat;
using EdamamRequest;
using GigaChat.Models;
using GigaChatRequest;
using RecipeSearch;
using Edamam.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using RecipeSearch.Models;

namespace RecipeSearch.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class RequestController : ControllerBase
    {
        [HttpPost("/postUser")]
        public async Task<IActionResult> PostUser([FromForm]string inputString)
        {
            Response.ContentType = "text/html; charset=utf-8";
            GigaChatAnswer giga = new GigaChatAnswer(Scope.GIGACHAT_API_PERS);
            
            string answer = await giga.SendMessage(inputString, "q");
            return Content(answer, "text/html; charset=utf-8");


        }
    }
}
