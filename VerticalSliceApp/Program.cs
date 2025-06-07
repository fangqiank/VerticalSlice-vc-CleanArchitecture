using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VerticalSliceApp;
using VerticalSliceApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TodoDB"));

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.EnsureCreatedAsync();

    await DataSeeder.SeedAsync(db);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGroup("/todos")
   .MapTodosEndpoints();

app.UseHttpsRedirection();

app.Run();

static class EndpointRouteBuilderExtensions
{
    public static void MapTodosEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("");
        group.MapCreateTodoEndpoint();
        group.MapListTodosEndpoint();
        group.MapGetTodoByIdEndpoint();
        group.MapSearchTodosEndpoint();
        group.MapToggleTodoEndpoint();
        group.MapUpdateTodoEndpoint();
        group.MapDeleteTodoEndpoint();
    }
}
