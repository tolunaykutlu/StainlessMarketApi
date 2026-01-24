namespace StainlessMarketApi.Entities;

public class UserEntity
{
    public int id { get; set; }
    public string username { get; set; }

    // Şifreleri açık tutmak yerine Hash ve Salt olarak saklıyoruz.
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }

}