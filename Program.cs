// add EF Core:
using Microsoft.EntityFrameworkCore;
using movieflix_api.API_Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(     // set class DataContext as database-context (use SQL-server)
builder.Configuration.GetConnectionString("DefaultConnection")                  // setup connection-string to SQL ("DefaultConnection")
));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var reactApp = "_reactAppPolicy";   // setting up CORS-policy

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: reactApp, builder =>
    {
        builder.WithOrigins("*");   // allow all * 
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

app.UseCors(reactApp);      // use the CORS-policy from above

app.UseAuthorization();

app.MapControllers();

app.Run();
