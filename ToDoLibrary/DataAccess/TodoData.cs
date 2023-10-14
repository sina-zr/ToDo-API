using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoLibrary.Models;

namespace ToDoLibrary.DataAccess
{
    public class TodoData : ITodoData
    {
        private readonly ISqlDataAccess _sql;
        private const string _connectionStringName = "Default";

        public TodoData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public Task<List<TodoModel>> GetAllAssigned(int assignedTo)
        {
            return _sql.LoadData<TodoModel, dynamic>(
                "dbo.spTodos_GetAllAssigned",
                new { AssignedTo = assignedTo },
                _connectionStringName);
        }

        public async Task<TodoModel?> GetOneAssigned(int assignedTo, int todoId)
        {
            var results = await _sql.LoadData<TodoModel, dynamic>(
                "dbo.spTodos_GetOneAssigned",
                new { AssignedTo = assignedTo, TodoId = todoId },
                _connectionStringName);

            return results.FirstOrDefault();
        }

        public async Task<TodoModel?> Create(int assignedTo, string task)
        {
            var results = await _sql.LoadData<TodoModel, dynamic>(
                "dbo.spTodos_Create",
                new { AssignedTo = assignedTo, Task = task },
                _connectionStringName);

            return results.FirstOrDefault();
        }

        public Task Update(int assignedTo, int todoId, string task)
        {
            return _sql.SaveData<dynamic>(
                "dbo.spTodos_Update",
                new { AssignedTo = assignedTo, TodoId = todoId, Task = task },
                _connectionStringName);
        }

        public Task CompleteTodo(int assignedTo, int todoId)
        {
            return _sql.SaveData<dynamic>(
                "dbo.spTodos_CompleteTodo",
                new { AssignedTo = assignedTo, TodoId = todoId },
                _connectionStringName);
        }

        public Task Delete(int assignedTo, int todoId)
        {
            return _sql.SaveData<dynamic>(
                "dbo.spTodos_Delete",
                new { AssignedTo = assignedTo, TodoId = todoId },
                _connectionStringName);
        }
    }
}
