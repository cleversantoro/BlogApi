using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BlogApi.Context;
using BlogApi.Endpoints;
using BlogApi.Services;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IPostService, PostService>();

string mySqlConnection =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new
    Exception("A string de conexão 'DefaultConnection' não foi configurada.");

builder.Services.AddDbContext<BlogApiContext>(options =>
                    options.UseMySql(mySqlConnection,
                    ServerVersion.AutoDetect(mySqlConnection)));

//middleware para Identity
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<BlogApiContext>();

builder.Services.AddAuthorization();

var app = builder.Build();

// middleware de exceção
app.UseStatusCodePages(async statusCodeContext
    => await Results.Problem(statusCode: statusCodeContext.HttpContext.Response.StatusCode)
        .ExecuteAsync(statusCodeContext.HttpContext));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("/identity/").MapIdentityApi<IdentityUser>();

app.PostEndpoints();

app.Run();