using Marten;

var builder = WebApplication.CreateBuilder(args);

//add services to the container

//add cater
builder.Services.AddCarter();
//add mediatR
builder.Services.AddMediatR(configuration =>
{
	configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(options =>
{
	options.Connection(builder.Configuration.GetConnectionString("Database")!);
});

var app = builder.Build();

//add cater
app.MapCarter();

// configure the http request pipline

app.Run();
