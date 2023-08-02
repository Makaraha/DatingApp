using DatingApp.Extensions;
using DatingApp.Middlewares;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.RegisterSwagger();
builder.Services.RegisterConnectionString(builder.Configuration);

builder.Services.RegisterRepositories();
builder.Services.RegisterServices();
builder.Services.RegisterIdentity();
builder.Services.RegisterJWT(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.RegisterSwagger();
}

app.UseMiddleware<ExceptionHandler>();

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseMiddleware<InfinityTokenMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<CultureMiddleware>();

app.MigrateDb();

app.MapControllers();

app.Run();
