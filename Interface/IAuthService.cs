using StainlessMarketApi.Dtos;
using System.Threading.Tasks;

public interface IAuthService
{
    /// <summary>
    /// Kullanıcıyı doğrular ve JWT token döner.
    /// </summary>
    /// <param name="username">Kullanıcı adı</param>
    /// <param name="password">Şifre</param>
    /// <returns>JWT token string</returns>
    Task<string> LoginAsync(string username, string password);

    /// <summary>
    /// Kullanıcı kaydı oluşturur.
    /// </summary>
    /// <param name="userDto">Kullanıcı DTO</param>
    /// <param name="password">Şifre</param>
    /// <returns>Oluşturulan kullanıcı DTO</returns>
    Task<UserDto> RegisterAsync(UserDto userDto, string password);

    /// <summary>
    /// Kullanıcı çıkış işlemi (opsiyonel, client tarafında token silinir)
    /// </summary>
    void Logout();

    /// <summary>
    /// Token doğrulama işlemi (isteğe bağlı)
    /// </summary>
    /// <param name="token">JWT token</param>
    /// <returns>Kullanıcı DTO veya null</returns>
    Task<UserDto> ValidateTokenAsync(string token);
}