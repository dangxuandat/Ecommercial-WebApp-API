using ECommercialAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyDefaultPolicy",
        policy =>
        {
            policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
        });
});
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDatabase();
builder.Services.AddUnitOfWork();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyDefaultPolicy");

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
