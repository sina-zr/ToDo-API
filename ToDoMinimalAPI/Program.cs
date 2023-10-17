using ToDoMinimalAPI.Endpoints;
using ToDoMinimalAPI.StartupConfig;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.AddStandardServices();
builder.AddDataAccessServices();
builder.AddAuthServices();
builder.AddHealthCheckServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.AddAuthenicationEndpoint();
app.AddTodosEndpoints();

app.MapHealthChecks("/health").AllowAnonymous();

app.Run();