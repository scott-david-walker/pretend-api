using Pretender;
using Pretender.Configuration;
using Pretender.Matcher;
using Pretender.Responder;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IMatcher, Matcher>();
builder.Services.AddScoped<IResponder, Responder>();
builder.Services.AddSingleton<IMatcher, Matcher>();

var yml = File.ReadAllText("./test.yaml");
var deserializer = new DeserializerBuilder()
    .WithNamingConvention(UnderscoredNamingConvention.Instance) 
    .Build();

var root = deserializer.Deserialize<Root>(yml);
builder.Services.AddSingleton(root.Config);
new ConfigValidator(root.Config).Validate();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();
