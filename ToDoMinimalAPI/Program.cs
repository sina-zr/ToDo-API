using ToDoLibrary.DataAccess;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<ITodoData, TodoData>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/Todos", async (ITodoData data) =>
{
    var output = await data.GetAllAssigned(1);
    return Results.Ok(output);
});

app.MapPost("/api/Todos", async (ITodoData data, [FromBody] string task) =>
{
    var output = await data.Create(1, task);
    return Results.Ok(output);
});

app.MapPut("/api/Todos/{todoId}", async (ITodoData data, int todoId, [FromBody] string task) =>
{
    await data.UpdateTask(1, todoId, task);
    return Results.Ok();
});

app.MapPut("/api/Todos/{todoId}/Complete", async (ITodoData data, int todoId) =>
{
    await data.CompleteTodo(1, todoId);
    return Results.Ok();
});

app.MapDelete("/api/Todos/{todoId}", async (ITodoData data, int todoId) =>
{
    await data.Delete(1, todoId);
    return Results.Ok();
});

app.Run();