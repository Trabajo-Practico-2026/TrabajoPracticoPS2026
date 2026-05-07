using Microsoft.EntityFrameworkCore;
using TrabajoPracticoPS.Api.BackgroundServices;
using TrabajoPracticoPS.Api.Middlewares;
using TrabajoPracticoPS.Application.Interfaces;
using TrabajoPracticoPS.Infrastructure.Data;
using TrabajoPracticoPS.Infrastructure.Persistence;
using TrabajoPracticoPS.Infrastructure.Persistence.Repositories;

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

// Configuración de Entity Framework Core con SQL Server
var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ISectorRepository).Assembly));

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Repositorios
builder.Services.AddScoped<ISectorRepository, SectorRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
builder.Services.AddScoped<IReservationRespository, ReservationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//Workers
builder.Services.AddHostedService<ReservationReleaseWorker>();

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
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("MicroserviceCorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();