using Microsoft.AspNetCore.Mvc;
using RecipeSearch.Models;
using RecipeSearch.Service;

namespace RecipeSearch.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class RequestController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly AnswerService _answerService;
        public RequestController(AnswerService answerService, IConfiguration configuration)
        {
            _answerService = answerService;
            _configuration = configuration;
        }

        [HttpPost("/postUser")]
        public async Task<IActionResult> PostUser([FromForm]string inputString, [FromForm] string country, [FromForm] bool checkboxValue)
        {
            AppSettings appSettings = _configuration.GetSection("Settings").Get<AppSettings>();
            
            var answerRecipe = await _answerService.ResponseServiceEdamam(inputString, country,appSettings.Promt, checkboxValue);
            return Ok(answerRecipe.Recipe);
        }
    }
}
