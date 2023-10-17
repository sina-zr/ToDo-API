using ToDoLibrary.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
    private async static Task<IResult> GetAllTodos(ITodoData data, HttpContext context)
    {
        var output = await data.GetAllAssigned(GetUserId(context));
        return Results.Ok(output);
    }
    private async static Task<IResult> CreateTodo(ITodoData data, HttpContext context, [FromBody] string task)
    {
        var output = await data.Create(GetUserId(context), task);
        return Results.Ok(output);
    }
    private async static Task<IResult> UpdateTodo(ITodoData data, int todoId, HttpContext context, [FromBody] string task)
    {
        await data.UpdateTask(GetUserId(context), todoId, task);
        return Results.Ok();
    }
    private async static Task<IResult> CompleteTodo(ITodoData data, HttpContext context, int todoId)
    {
        await data.CompleteTodo(GetUserId(context), todoId);
        return Results.Ok();
    }
    private async static Task<IResult> DeleteTodo(ITodoData data, HttpContext context, int todoId)
    {
        await data.Delete(GetUserId(context), todoId);
        return Results.Ok();
    }

    private static int GetUserId(HttpContext context)
    {
        var userIdText = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        return int.TryParse(userIdText, out int userId) ? userId : 0; // Return 0 or handle invalid case as needed
    }
}
