using GigaChat.Models;
using GigaChatRequest;
using Microsoft.Extensions.FileProviders;
using RecipeSearch.Controller;
using System;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();

app.UseDefaultFiles(new DefaultFilesOptions
{
    DefaultFileNames = new
     List<string> { "index.html" }
});
app.UseStaticFiles();
app.MapControllers();


app.Run();
