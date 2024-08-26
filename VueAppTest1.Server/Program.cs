using Microsoft.EntityFrameworkCore;
using VueAppTest1Back.Context;

var builder = WebApplication.CreateBuilder(args);
var strspecifiedOrigins = "_specifiedOrigins";
var strConnectionString = builder.Configuration.GetConnectionString("WebApiDatabase");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
    options.AddPolicy(strspecifiedOrigins,
        policy =>
        {
            policy.AllowAnyHeader().AllowAnyMethod();
            policy.WithOrigins("https://localhost:5173");
        })
);



builder.Services.AddHttpClient();

builder.Services.AddDbContext<CaafiContext>(options =>
{
    options.UseMySql(strConnectionString, ServerVersion.AutoDetect(strConnectionString));
});

var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(strspecifiedOrigins);
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
