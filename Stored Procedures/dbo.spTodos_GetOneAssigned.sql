CREATE PROCEDURE [dbo].[spTodos_GetOneAssigned]
	@AssignedTo int,
	@TodoId int
as
Begin
	select Id, Task, AssignedTo, IsComplete
	from dbo.Todos
	where AssignedTo = @AssignedTo and Id = @TodoId;
End