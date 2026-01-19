using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StainlessMarketApi.Data;
using StainlessMarketApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();
builder.Services.AddScoped<IStokService, StokService>();
builder.Services.AddScoped<IFasonService, FasonService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
// SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=SanayiProjem.db"));

// Swagger (SADE)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});//Cors ayarları


var app = builder.Build();
app.UseCors("AllowAll");//CORS ayarlarını uygulamak için
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();//wwwroot klasörünü kullanmak için

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
