namespace StainlessMarketApi.Entities;

public class UserEntity : BaseEntity
{

    required public string UserName { get; set; }

    // Şifreleri açık tutmak yerine Hash ve Salt olarak saklıyoruz.
    required public byte[] PasswordHash { get; set; }
    required public byte[] PasswordSalt { get; set; }

}