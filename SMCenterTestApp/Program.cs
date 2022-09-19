using DAL;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder? builder
    = WebApplication.CreateBuilder(args);

string connectionString =
    builder.Configuration["MedicDB:ConnectionString"];

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MedicDBContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // подключаем маршрутизацию на контроллеры
});

app.Run();