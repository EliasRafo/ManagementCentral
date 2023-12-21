using ServiceApi.Endpoints;
using ManagementCentral.Shared.Domain;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var specOrigin = "MySpecOrigin";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: specOrigin, policy =>
    {
        //policy.WithOrigins("https://localhost:7084")
        policy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



// device
//app.MapGet("/device", () => "Getting a device from API");

//app.MapGet("/device/{deviceId}/button/{buttonId}",
//    (int deviceId, int buttonId) => $"Deviceid {deviceId} and ButtonId {buttonId}");

//app.MapGet("/device/{deviceId}", (int deviceId) =>
//{
//    var DeviceService = new DeviceService();
//    return DeviceService.DeviceList[deviceId].DeviceType;
//});

app.RegisterUserEndpoint();

app.UseCors(specOrigin);
app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

class DeviceService
{
    public List<Device> DeviceList { get; set; } = new List<Device>();

    public DeviceService()
    {
        DeviceList.Add(new Device() { DeviceId = 1, Location = Location.Sweden, Date = DateTime.Now, DeviceType = "Sensor", Status = Status.online });
        DeviceList.Add(new Device() { DeviceId = 2, Location = Location.England, Date = DateTime.Now.AddDays(-30), DeviceType = "Machine", Status = Status.offline });
    }
}