using CampaignsWebAPI.Data;
using CampaignsWebAPI.Interfaces;
using CampaignsWebAPI.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<ICampaignRepository, CampaignRepository>();
builder.Services.AddTransient<IPlayerRepository, PlayerRepository>();
builder.Services.AddTransient<ICharacterClassRepository, CharacterClassRepository>();
builder.Services.AddTransient<ICharacterRepository, CharacterRepository>();
builder.Services.AddTransient<ISpeciesRepository, SpeciesRepository>();
builder.Services.AddCors(p=>p.AddPolicy("corspolicy", b=>b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
