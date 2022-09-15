using Microsoft.EntityFrameworkCore;
using SMCenterTestApp.DAL;

WebApplicationBuilder? builder
    = WebApplication.CreateBuilder(args);

string connectionString =
    builder.Configuration["MedicDB:ConnectionString"];

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MedicDBContext>(options =>
options.UseSqlServer(connectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication? app = builder.Build();

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