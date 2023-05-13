using Crud.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using DateTimeConverter = Crud.WebApi.Models.DateTimeConverter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options => // Bug resolved: Improper formatting of date in json
{
    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
});

builder.Services.AddMvc().AddSessionStateTempDataProvider();

// Use sessions
builder.Services.AddSession(o => { 
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

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
