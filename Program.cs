using Catedra1.src.data;
using Catedra1.src.interfaces;
using Catedra1.src.repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => 
{    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddControllers();
var app = builder.Build();
using (var scope  = app.Services.CreateScope())
{
    var context  = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    context.Database.Migrate();

    DataSeeder.Initializable(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();

