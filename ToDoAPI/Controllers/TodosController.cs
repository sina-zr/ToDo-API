using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoLibrary.DataAccess;
using ToDoLibrary.Models;

namespace ToDoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodosController : ControllerBase
{
    private readonly ITodoData _data;
    private readonly ILogger<TodosController> _logger;

    public TodosController(ITodoData data, ILogger<TodosController> logger)
    {
        _data = data;
        _logger = logger;
    }

    private int GetUserId()
    {
        var userIdText = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userIdText);
    }

    // GET: api/Todos
    /// <summary>
    /// Gets all the Tasks Assigned to a User from Database
    /// where userId is automatically taken from Token
    /// </summary>
    /// <returns> List of TodoModel </returns>
    [HttpGet]
    public async Task<ActionResult<List<TodoModel>>> Get()
    {
        _logger.LogInformation("GET: api/Todos");

        try
        {
            var output = await _data.GetAllAssigned(GetUserId());


            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The GET call to api/Todos failed.");
            return BadRequest();
        }

    }

    // GET api/Todos/5
    /// <summary>
    /// Gets only one Task of a user from Database
    /// And userId is taken from Token
    /// </summary>
    /// <param name="todoId">The Id of the Task you want to get</param>
    /// <returns>One TodoModel</returns>
    [HttpGet("{todoId}")]
    public async Task<ActionResult<TodoModel>> Get(int todoId)
    {
        _logger.LogInformation("GET: api/Todos/{todoId}", todoId);

        try
        {
            var output = await _data.GetOneAssigned(GetUserId(), todoId);

            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The GET call to api/Todos/{todoId} failed.", todoId);
            return BadRequest();
        }
    }

    // POST api/Todos
    /// <summary>
    /// Creates a Task in the Database
    /// And userId is taken from Token
    /// </summary>
    /// <param name="task">description of the task you want to add as string</param>
    /// <returns>Returns the Task you created as TodoModel obj</returns>
    [HttpPost]
    public async Task<ActionResult<TodoModel>> Post([FromBody] string task)
    {
        _logger.LogInformation("POST: api/Todos");

        try
        {
            var output = await _data.Create(GetUserId(), task);

            return Ok(output);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The POST call to api/Todos failed. The task was {task}", task);
            return BadRequest();
        }
    }

    // PUT api/Todos/5
    /// <summary>
    /// Updates one Task of User you have in Database
    /// Takes userId from Token
    /// </summary>
    /// <param name="todoId">Id of the task you wanna Update</param>
    /// <param name="task">description of the Task as string</param>
    /// <returns></returns>
    [HttpPut("{todoId}")]
    public async Task<IActionResult> Put(int todoId, [FromBody] string task)
    {
        _logger.LogInformation("PUT: api/Todos/{todoId}", todoId);

        try
        {
            await _data.UpdateTask(GetUserId(), todoId, task);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The PUT call to api/Todos/{todoId} failed.", todoId);
            return BadRequest();
        }
    }

    // PUT api/Todos/5/Complete
    /// <summary>
    /// Mark a Task of user as Complete in Database
    /// Takes userId from Token
    /// </summary>
    /// <param name="todoId">Id of the task</param>
    /// <returns></returns>
    [HttpPut("{todoId}/Complete")]
    public async Task<IActionResult> Complete(int todoId)
    {
        _logger.LogInformation("PUT: api/Todos/{todoId}/Complete", todoId);

        try
        {
            await _data.CompleteTodo(GetUserId(), todoId);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The PUT call to api/Todos/{todoId}/Complete failed.", todoId);
            return BadRequest();
        }
    }

    // DELETE api/Todos/5
    /// <summary>
    /// Deletes a Task of User from Database
    /// userId is taken from Database
    /// </summary>
    /// <param name="todoId">Id of the Task you wanna Delete</param>
    /// <returns></returns>
    [HttpDelete("{todoId}")]
    public async Task<IActionResult> Delete(int todoId)
    {
        _logger.LogInformation("DELETE: api/Todos/{todoId}", todoId);

        try
        {
            await _data.Delete(GetUserId(), todoId);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The DELETE call to api/Todos/{todoId} failed.", todoId);
            return BadRequest();
        }
    }
}