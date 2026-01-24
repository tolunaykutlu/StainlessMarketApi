using Microsoft.AspNetCore.Mvc;
using StainlessMarketApi.Dtos;

namespace StainlessMarketApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    // Servis bağlantısı (Dependency Injection)
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // KAYIT OLMA ENDPOINT'i
    // Adres: /api/auth/register
    // Kullanıcıdan JSON olarak { username, password, email } alır.
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(UserRegisterDto request)
    {
        // Gelen veriyi Service metodunun istediği formata (UserDto) çeviriyoruz
        var userDto = new UserDto 
        { 
            Username = request.Username
        };

        // Servis katmanına gönderip işi orada hallediyoruz.
        var result = await _authService.RegisterAsync(userDto, request.Password);
        return Ok(result); // İşlem başarılı, 200 OK dönüyoruz.
    }

    // GİRİŞ YAPMA ENDPOINT'i
    // Adres: /api/auth/login
    // Kullanıcıdan JSON olarak { username, password } alır.
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserLoginDto request)
    {
        // Servis katmanında giriş yapmayı dene ve token iste.
        var token = await _authService.LoginAsync(request.Username, request.Password);
        
        // Eğer hata mesajı geldiyse kullanıcıya hata döndür.
        if (token == "Kullanıcı bulunamadı." || token == "Şifre yanlış.")
        {
            return BadRequest(token); // 400 Bad Request
        }

        // Başarılıysa Token'ı kullanıcıya ver.
        return Ok(token); // 200 OK + Token
    }
}
