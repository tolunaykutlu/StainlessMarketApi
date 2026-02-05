using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StainlessMarketApi.Data;
using StainlessMarketApi.Dtos;
using StainlessMarketApi.Entities;

namespace StainlessMarketApi.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    // Dependency Injection (Bağımlılık Enjeksiyonu)
    // Veritabanına ulaşmak için AppDbContext, ayarlara (appsettings.json) ulaşmak için IConfiguration alıyoruz.
    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    // KAYIT OLMA (Register) İşlemi
    public async Task<UserDto> RegisterAsync(UserDto userDto, string password)
    {
        // 1. Önce böyle bir kullanıcı adı var mı diye kontrol ediyoruz.
        if (await UserExists(userDto.Username))
        {
            throw new Exception("Kullanıcı zaten mevcut.");
        }

        // 2. Şifreyi güvenli hale getiriyoruz (Hashing).
        // password: Kullanıcının girdiği "123456" gibi düz metin şifre.
        // out ... : Metottan dışarıya çıkacak olan şifreli veriler.
        CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

        // 3. Veritabanı nesnesini (Entity) oluşturuyoruz.
        var user = new UserEntity
        {
            UserName = userDto.Username,
            PasswordHash = passwordHash, // Şifreli hali
            PasswordSalt = passwordSalt  // Şifreleme anahtarı
        };

        // 4. Veritabanına ekle ve kaydet.
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return userDto;
    }

    // GİRİŞ YAPMA (Login) İşlemi
    public async Task<string> LoginAsync(string username, string password)
    {
        // 1. Kullanıcıyı veritabanında ara.
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
        if (user == null)
        {
            return "Kullanıcı bulunamadı."; // Kullanıcı yoksa, işlem biter.
        }

        // 2. Şifreyi doğrula.
        // Girilen şifre (password) ile veritabanındaki hash'li şifreyi (PasswordHash) karşılaştırır.
        if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        {
            return "Şifre yanlış.";
        }

        // 3. Her şey doğruysa, kullanıcıya giriş bileti (Token) ver.
        return CreateToken(user);
    }

    // YARDIMCI METOT: Kullanıcı var mı kontrolü
    private async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(x => x.UserName == username);
    }

    // YARDIMCI METOT: Şifre Hashleme (Kriptolama)
    // HMACSHA512 algoritması kullanarak şifreyi okunamaz hale getirir.
    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key; // Her şifreleme için benzersiz bir tuz (salt) oluşturulur.
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); // Şifreyi bu tuz ile karıştırıp hash'ler.
        }
    }

    // YARDIMCI METOT: Şifre Doğrulama
    // Kullanıcının girdiği şifreyi, kayıtlı olan Salt ile tekrar hash'ler ve veritabanındaki Hash ile aynı mı diye bakar.
    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt)) // Kayıtlı Salt kullanılır.
        {
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash); // Sonuçlar birebir aynı mı?
        }
    }

    // YARDIMCI METOT: Token (JWT) Oluşturma
    // Kullanıcının kimliğini kanıtlayan dijital bir kimlik kartı üretir.
    private string CreateToken(UserEntity user)
    {
        // 1. Token'ın içine koyacağımız bilgiler (Claims)
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName), // Kullanıcı adı
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) // ID
        };

        // 2. Güvenlik Anahtarı (appsettings.json dosyasındaki gizli şifre)
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

        // 3. İmzalama (Anahtar ve Algoritma)
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        // 4. Token Ayarları
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1), // Token 1 gün boyunca geçerli olsun.
            signingCredentials: creds
        );

        // 5. Token'ı oluştur ve yazı (string) olarak döndür.
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }

    public void Logout()
    {
        // JWT yapısında çıkış sunucuda değil, tarayıcıda Token silinerek yapılır.
    }

    public Task<UserDto> ValidateTokenAsync(string token)
    {
        throw new NotImplementedException();
    }
}