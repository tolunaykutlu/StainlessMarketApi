using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StainlessMarketApi.Data;
using StainlessMarketApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// --- SERVİSLERİN EKLENDİĞİ BÖLÜM (Dependency Injection Container) ---

// Controller desteği
builder.Services.AddControllers();

// Servislerimizi sisteme tanıtıyoruz (Scoped: Her istekte yeni bir tane üretilir)
builder.Services.AddScoped<IStokService, StokService>();
builder.Services.AddScoped<IFasonService, FasonService>();
builder.Services.AddScoped<IAuthService, AuthService>(); // Auth servisimizi de ekledik.

// Automapper (Veri dönüştürücü)
builder.Services.AddAutoMapper(typeof(MappingProfile));

// SQLite Veritabanı bağlantısı
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=SanayiProjem.db"));

// Swagger Ayarları (API Dokümantasyonu)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Swagger ekranına "Authorize" (Kilit) butonu eklemek için gerekli ayar.
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Buraya 'Bearer {token}' formatında yapıştırın.",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

// KİMLİK DOĞRULAMA AYARLARI (Authentication)
// Sisteme JWT kullanacağımızı söylüyoruz.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true, // İmzayı kontrol et (Bu token'ı biz mi ürettik?)
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)), // Gizli anahtarımızla kontrol et.
            ValidateIssuer = false, // Yayıncı kontrolü kapalı
            ValidateAudience = false // Alıcı kontrolü kapalı
        };
    });

// CORS (Tarayıcı güvenlik izinleri)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// --- UYGULAMA ÇALIŞMA AYARLARI (Middleware Pipeline) ---

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

// SIRALAMA ÖNEMLİ!
app.UseAuthentication(); // 1. Kimlik Kontrolü: "Sen kimsin?" (Token var mı?)
app.UseAuthorization();  // 2. Yetki Kontrolü: "Buraya girmeye yetkin var mı?"

app.MapControllers();

app.Run();
