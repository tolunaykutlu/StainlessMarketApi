using Microsoft.AspNetCore.Mvc;
using StainlessMarketApi.Data;
using StainlessMarketApi.Entities;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
// Stok işlemleri için API controller'ı
public class StokController : ControllerBase
{
    // Veritabanı işlemleri için kullanılan context
    private readonly AppDbContext _context;
    // Controller'ın constructor'ı, dependency injection ile AppDbContext alınır
    public StokController(AppDbContext context)
    {
        _context = context;
    }

    // Tüm stok ürünlerini getiren endpoint
    [HttpGet]
    public async Task<IActionResult> GetAllStokProducts()
    {
        // Veritabanından tüm stok ürünlerini asenkron olarak al
        var stokProducts = await _context.StokProducts.ToListAsync();
        // Sonucu 200 OK ile döndür
        return Ok(stokProducts);
    }


    // Yeni bir stok ürünü ekleyen endpoint
    [HttpPost]
    public async Task<IActionResult> CreateStokProduct([FromBody] StokProductEntities stokProduct)
    {
        // Yeni stok ürününü veritabanına ekle
        _context.StokProducts.Add(stokProduct);
        // Değişiklikleri kaydet
        await _context.SaveChangesAsync();

        // Oluşturulan ürünü ve konumunu 201 Created ile döndür
        return CreatedAtAction(nameof(GetAllStokProducts), new { id = stokProduct.Id }, stokProduct);
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> UpdateStokProduct(int id, [FromBody] StokProductEntities updatedProduct)
    {
        // Veritabanında güncellenecek ürünü bul
        var existingProduct = await _context.StokProducts.FindAsync(id);
        if (existingProduct == null)
        {
            // Ürün bulunamazsa 404 Not Found döndür
            return NotFound();
        }

        // Ürün özelliklerini güncelle
        existingProduct.Location = updatedProduct.Location;
        existingProduct.Thickness = updatedProduct.Thickness;
        existingProduct.Width = updatedProduct.Width;
        existingProduct.Length = updatedProduct.Length;
        existingProduct.Quantity = updatedProduct.Quantity;

        // Değişiklikleri kaydet
        await _context.SaveChangesAsync();

        // Güncellenen ürünü 200 OK ile döndür
        return Ok(existingProduct);
    }
    [HttpDelete("{id}")]

    public async Task<IActionResult> DeleteStokProduct(int id)
    {
        // Veritabanında silinecek ürünü bul
        var existingProduct = await _context.StokProducts.FindAsync(id);
        if (existingProduct == null)
        {
            // Ürün bulunamazsa 404 Not Found döndür
            return NotFound();
        }

        // Ürünü veritabanından sil
        _context.StokProducts.Remove(existingProduct);
        // Değişiklikleri kaydet
        await _context.SaveChangesAsync();

        // Başarılı silme işlemi için 204 No Content döndür
        return NoContent();
    }

}