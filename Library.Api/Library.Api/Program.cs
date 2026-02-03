//===============================================
//@nodirbek1535 library api program (C)
//===============================================

using Library.Api.Brokers.Loggings;
using Library.Api.Brokers.Storages;
using Library.Api.Services.Foundations.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<StorageBroker>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

AddBrokers(builder.Services);
AddFoundationServices(builder.Services);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Library.Api",
        Version = "v1"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            "Library.Api v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

static void AddBrokers(IServiceCollection services)
{
    services.AddTransient<IStorageBroker, StorageBroker>();
    services.AddTransient<ILoggingBroker, LoggingBroker>();
}

static void AddFoundationServices(IServiceCollection services)
{
    services.AddTransient<IBookService, BookService>();
}