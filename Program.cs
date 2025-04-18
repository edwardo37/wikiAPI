using Microsoft.EntityFrameworkCore;
using wikiAPI.Configurations;
using wikiAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add controllers, ignore recursive calls, and don't automatically try to validate
builder.Services.AddControllers()
.ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Db Context
builder.Services.AddDbContext<WikiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add EF implementation
builder.Services.AddScoped<IWikiRepository, WikiCategoryEfImpl>();

// Add Error Handling
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use the custom error handler, with default settings
app.UseExceptionHandler(_ => { });

app.UseAuthorization();

app.MapControllers();

app.Run();
