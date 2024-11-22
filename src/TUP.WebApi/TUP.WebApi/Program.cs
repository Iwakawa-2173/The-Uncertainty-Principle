using TUP.WebApi.Domain.Entities;
using TUP.WebApi.Domain.Interfaces;
using TUP.WebApi.Domain.Services;
using TUP.WebApi.Infrastrucutre;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepository<ChatMessage>, ChatMessagesRepository>();
builder.Services.AddScoped<IRepository<Player>, PlayersRepository>();
builder.Services.AddScoped<IRepository<Event>, EventsRepository>();
builder.Services.AddScoped<IRepository<Response>, ResponsesRepository>();
builder.Services.AddScoped<IRepository<ScoreScale>, ScoreScalesRepository>();
builder.Services.AddScoped<IChatMessageService, ChatMessageService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IResponseService, ResponseService>();
builder.Services.AddScoped<IScoreScaleService, ScoreScaleService>();

builder.Services.AddDbContext<TUPContext>(
    options => options.UseSqlServer("name=ConnectionStrings:Main"));
builder.UseSeriLogger();

var app = builder.Build();

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
