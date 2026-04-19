using Microsoft.EntityFrameworkCore;
using TrabajoPracticoPS.Infrastructure.Data;
using TrabajoPracticoPS.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MicroserviceCorsPolicy",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// custom
var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// Aplica migraciones y seeding al iniciar
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
    DataSeeder.Seed(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MicroserviceCorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();