# StainlessMarketApi

StainlessMarketApi, paslanmaz çelik ürünleriyle ilgili stok ve fason işlemlerini yönetmek için geliştirilmiş bir RESTful API projesidir. Proje, .NET 9.0 ve Entity Framework Core kullanılarak geliştirilmiştir.

## Özellikler
- Kullanıcı kayıt ve giriş işlemleri (JWT Authentication)
- Stok ürünlerinin listelenmesi, eklenmesi, güncellenmesi ve silinmesi
- Fason ürün işlemleri (ekleme, listeleme, güncelleme, silme)
- Katmanlı mimari (Controller, Service, Data, Entities, DTOs, Mapping)
- Swagger/OpenAPI desteği (isteğe bağlı)

## Proje Yapısı
```
StainlessMarketApi/
├── Controllers/         # API controller dosyaları
├── Data/                # DbContext ve veri erişim katmanı
├── Entities/            # Entity ve DTO tanımları
├── Interface/           # Servis arayüzleri
├── Mapping/             # AutoMapper profilleri
├── Migrations/          # EF Core migration dosyaları
├── Services/            # Servis implementasyonları
├── wwwroot/             # Statik dosyalar (HTML, CSS)
├── appsettings.json     # Genel konfigürasyon
├── Program.cs           # Uygulama başlangıç noktası
└── StainlessMarketApi.csproj
```

## Kurulum
1. **Projeyi klonlayın:**
   
   git clone <repo-url>
   cd StainlessMarketApi
  
2. **Bağımlılıkları yükleyin:**
   
   dotnet restore
  
3. **Veritabanı migrasyonlarını uygulayın:**
   
   dotnet ef database update
  
4. **Projeyi başlatın:**
 
   dotnet run
   

## API Kullanımı
- Temel endpointler için `Controllers` klasörüne bakabilirsiniz.
- JWT ile kimlik doğrulama gerektiren endpointler mevcuttur.
- Örnek istekler için `StainlessMarketApi.http` dosyasını kullanabilirsiniz.



## Lisans
Bu proje MIT lisansı ile lisanslanmıştır.
