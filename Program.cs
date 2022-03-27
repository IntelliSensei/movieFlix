// add EF Core:
using Microsoft.EntityFrameworkCore;
using movieflix_api.API_Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(     // set class DataContext as database-context (use SQL-server)
builder.Configuration.GetConnectionString("DefaultConnection")                  // connection-string = string from appsettings.dev.json
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

var app = builder.Build();  // build the app based on above building-blocks



// at this stage, the app is built and we can add additional features to it below:


// dependency injection:
using var scope = app.Services.CreateScope();               // 'using' = creating a validity-scope (for a Service)
var services = scope.ServiceProvider;                       // ServiceProvider = allows us to instantiate any class (that we want to inject somewhere else (dependency))
var context = services.GetRequiredService<DataContext>();   // GetRequiredService<class> = gets us an instance of the class (DB connection)
await LoadData.LoadMovies(context);                         // the instantiated class (containing the DB connection) is passed into LoadMovies-method 
                                                            // allows LoadMovies to connect with DB at app-startup to perform data-checks (present or not)


// context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Movies ON;");
// context.SaveChanges();
// context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Movies OFF;");
//transaction.Commit();


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
