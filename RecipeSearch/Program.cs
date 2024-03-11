
using RecipeSearch.Service;
using RecipeSearch.Service.Interface;
using RecipeSearch.Service.Logic;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<IGigaChatService,GigaChatService>();
builder.Services.AddScoped<IEdamamService, EdamamService>(); 
builder.Services.AddScoped<AnswerService>();
var app = builder.Build();

app.UseDefaultFiles(new DefaultFilesOptions
{
    DefaultFileNames = new
     List<string> { "index.html" }
});
app.UseStaticFiles();
app.MapControllers();


app.Run();
