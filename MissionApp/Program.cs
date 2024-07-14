using MissionApp.Managers;
using MissionApp.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var googleApiKey = "AIzaSyBgQgCvMhkYKHB9uKpXau8MvUISDDM_pQg";
var dbContext = new MongoDbContext("mongodb://localhost:27017/", "Mission");
ILocationService locationService = new LocationService(googleApiKey);
IDatabaseService dbService = new MongoDBService(dbContext);
var missionService = new MissionService(locationService, dbService);

var indexManager = new IndexManager(dbContext);
indexManager.CreateIndexes();

builder.Services.AddSingleton(dbContext);
builder.Services.AddSingleton(locationService);
builder.Services.AddSingleton(dbService);
builder.Services.AddSingleton(missionService);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }