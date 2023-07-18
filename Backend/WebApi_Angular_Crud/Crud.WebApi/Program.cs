using Crud.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using DateTimeConverter = Crud.WebApi.Models.DateTimeConverter;
using Crud.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options => // Bug resolved: Improper formatting of date in json
{
    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
});

builder.Services.AddMvc().AddSessionStateTempDataProvider();

// Use sessions
builder.Services.AddSession(o =>
{
    o.IdleTimeout = TimeSpan.FromMinutes(20);
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the db context.
builder.Services.AddDbContext<WebApiDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("WebApiConnectionString")));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
             .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSession();

//app.UseMvcWithDefaultRoute();
app.UseCors("AllowSpecificOrigin");

// Use a custom middleware to check if the request header contains the speciifc value.
// otherwise return forbidden status code.
app.Use(async (context, next) =>
{
    if (!context.Request.isValidRequest())
    {
        context.Response.StatusCode = 403; // Forbidden
        return;
    }

    await next.Invoke();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
