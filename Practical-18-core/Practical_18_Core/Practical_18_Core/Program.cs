
using Practical_18_Core.Repositories;
using Practical_18_Core.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Use Controller service with NewtonsoftJson and XML media types
builder.Services
    .AddControllers(options =>
    {
        // if Accept media type is not supported, insted of returning default JSON media
        // type it will return 406 Not Acceptable
        options.ReturnHttpNotAcceptable = true;
    })
    // Add supoort for newtonsoft json insted of normal, used for patch request
    .AddNewtonsoftJson()
    // Add support for XML media types
    .AddXmlDataContractSerializerFormatters();

// Use DbContext Service for registering EfCore with sql server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext"));
});

// Use Repositry service for IStudentRepository
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

// Use AutoMapper service
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// for Cors request
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("corsapp");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
