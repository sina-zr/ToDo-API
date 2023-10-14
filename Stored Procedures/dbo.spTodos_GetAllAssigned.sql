CREATE PROCEDURE [dbo].[spTodos_GetAllAssigned]
	@AssignedTo int
as
Begin
	select Id, Task, AssignedTo, IsComplete
	from dbo.Todos
	where AssignedTo = @AssignedTo
End