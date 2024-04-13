using ______________.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<HomeController>();
builder.Services.AddKeyedScoped<ILoginService, UsernamePasswordLoginService>("UsernamePassword");
builder.Services.AddKeyedScoped<ILoginService, SocialMediaLoginService>("SocialMedia");

builder.Services.AddTransient<UsernamePasswordLoginService>(); // Register UsernamePasswordLoginService
builder.Services.AddTransient<SocialMediaLoginService>();

builder.Services.AddSingleton<LoginServiceFactory>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
