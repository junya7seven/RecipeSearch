using GigaChat.Models;
using GigaChatRequest;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
GigaChatAnswer gigaChat = new GigaChatAnswer("ClientSecret", "Auth", Scope.GIGACHAT_API_PERS);
await gigaChat.SendMessage("hi","who are you?");


app.MapGet("/", () => $"Hello World!");

app.Run();
