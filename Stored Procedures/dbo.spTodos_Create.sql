CREATE PROCEDURE [dbo].[spTodos_Create]
	@Task nvarchar(50),
	@AssignedTo int
as
Begin
	insert into dbo.Todos (Task, AssignedTo)
	values (@Task, @AssignedTo);

	select Id,Task, AssignedTo, IsComplete
	from dbo.Todos
	where Id = SCOPE_IDENTITY(); -- @@Identity gives U the Identity of the last created Identiy value
	-- SCOPE_IDENTITY give U the last created in a given scope but @@Identity give U the last in the entire database
End