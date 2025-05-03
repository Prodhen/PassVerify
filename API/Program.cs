using API.Services;

var builder = WebApplication.CreateBuilder(args);

// Register services (including controllers and password service)
builder.Services.AddHttpClient();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddControllers();

// Add Swagger/OpenAPI for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

// Map controllers
app.MapControllers();

app.Run();
