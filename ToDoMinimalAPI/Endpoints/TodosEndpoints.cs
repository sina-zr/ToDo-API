using ToDoLibrary.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ToDoMinimalAPI.Endpoints;

public static class TodosEndpoints
{
    public static void AddTodosEndpoints(this WebApplication app)
    {
        app.MapGet("/api/Todos", GetAllTodos);

        app.MapPost("/api/Todos", CreateTodo);

        app.MapPut("/api/Todos/{todoId}", UpdateTodo);

        app.MapPut("/api/Todos/{todoId}/Complete", CompleteTodo);

        app.MapDelete("/api/Todos/{todoId}", DeleteTodo);
    }
    private async static Task<IResult> GetAllTodos(ITodoData data)
    {
        var output = await data.GetAllAssigned(1);
        return Results.Ok(output);
    }
    private async static Task<IResult> CreateTodo(ITodoData data, [FromBody] string task)
    {
        var output = await data.Create(1, task);
        return Results.Ok(output);
    }
    private async static Task<IResult> UpdateTodo(ITodoData data, int todoId, [FromBody] string task)
    {
        await data.UpdateTask(1, todoId, task);
        return Results.Ok();
    }
    private async static Task<IResult> CompleteTodo(ITodoData data, int todoId)
    {
        await data.CompleteTodo(1, todoId);
        return Results.Ok();
    }
    private async static Task<IResult> DeleteTodo(ITodoData data, int todoId)
    {
        await data.Delete(1, todoId);
        return Results.Ok();
    }
}
