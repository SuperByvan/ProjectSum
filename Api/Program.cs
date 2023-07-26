using System.Reflection;
using Application;
using Application.Queries.GetOneTrackBySearching;
using MediatR;
using Persistence;

var builder = WebApplication.CreateBuilder(args);
const string corsName = "AllowAll";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsName,
        policy =>
        {
            policy.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddPersistance(builder.Configuration);
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddMediatR(typeof(GetOneTrackBySearchingQueryHandler).GetTypeInfo().Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsName);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();