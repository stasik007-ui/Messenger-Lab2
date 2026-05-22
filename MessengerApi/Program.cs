using MessengerApi.Services;
using MessengerApi.Storage;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Налаштовуємо базу даних SQLite (файл messenger.db)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=messenger.db"));

// Підключаємо наш сервіс
builder.Services.AddScoped<IMessageService, MessageService>();

var app = builder.Build();

// Автоматичне створення бази даних, щоб не писати міграції
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

app.MapControllers();

app.Run();