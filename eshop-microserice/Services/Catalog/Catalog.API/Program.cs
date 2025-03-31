using BuildingBlocks.Behaviors;
using Marten;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;
//add services to the container

//add mediatR
builder.Services.AddMediatR(configuration =>
{
	configuration.RegisterServicesFromAssembly(assembly);
	configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddMarten(options =>
{
	options.Connection(builder.Configuration.GetConnectionString("Database")!);
});

//add cater
builder.Services.AddCarter();

builder.Services.AddValidatorsFromAssembly(assembly);

var app = builder.Build();

//add cater
app.MapCarter();

// configure the http request pipline

app.Run();
