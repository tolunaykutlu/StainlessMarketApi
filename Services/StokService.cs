using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using StainlessMarketApi.Data;
using StainlessMarketApi.Dtos;
using StainlessMarketApi.Entities;


namespace StainlessMarketApi.Services
{
    public class StokService : IStokService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public StokService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        /// <summary>
        /// Tüm stok ürünlerini asenkron olarak getirir.
        /// </summary>
        /// <returns>Tüm stok ürünlerinin listesi.</returns>
        public async Task<IEnumerable<StokProductDto>> GetAllStokAsync()
        {
            var products = await _context.StokProducts.ToListAsync();
            return _mapper.Map<IEnumerable<StokProductDto>>(products);
        }

        /// <summary>
        /// Belirtilen ID'ye sahip stok ürününü asenkron olarak getirir.
        /// </summary>
        /// <param name="id">Stok ürününün ID'si.</param>
        /// <returns>Stok ürünü veya bulunamazsa null.</returns>
        public async Task<StokProductDto?> GetByIdAsync(int id)
        {
            var product = await _context.StokProducts.FindAsync(id);
            return _mapper.Map<StokProductDto>(product);
        }

        /// <summary>
        /// Yeni bir stok ürünü oluşturur ve veritabanına ekler.
        /// </summary>
        /// <param name="product">Eklenecek stok ürünü.</param>
        /// <returns>Eklenen stok ürünü.</returns>
        public async Task<StokProductDto> CreateAsync(StokProductDto productDto)
        {
            var createStokPro = _mapper.Map<StokProductEntity>(productDto);//Dto'yu Entity'e dönüştür
            await _context.StokProducts.AddAsync(createStokPro);//Entity'yi Context'e ekle
            await _context.SaveChangesAsync();

            return _mapper.Map<StokProductDto>(createStokPro);
        }

        /// <summary>
        /// Belirtilen ID'ye sahip stok ürününü günceller.
        /// </summary>
        /// <param name="id">Güncellenecek stok ürününün ID'si.</param>
        /// <param name="updatedProduct">Yeni değerler içeren stok ürünü.</param>
        /// <returns>Güncellenen stok ürünü veya bulunamazsa null.</returns>
        public async Task<StokProductDto?> UpdateAsync(int id, StokProductDto updatedProduct)
        {
            var existingProduct = await _context.StokProducts.FindAsync(id);
            if (existingProduct == null) return null;




           
             // AutoMapper, DTO'daki property isimleriyle Entity'dekileri eşleştirip günceller.
             _mapper.Map(updatedProduct, existingProduct);

            await _context.SaveChangesAsync();
            
            // Bu satırın sonucu bir yere atanmadığı için boşa çalışıyordu.
            // _mapper.Map<StokProductDto>(updatedProduct);
            return _mapper.Map<StokProductDto>(existingProduct);
            
        }

        /// <summary>
        /// Belirtilen ID'ye sahip stok ürününü siler.
        /// </summary>
        /// <param name="id">Silinecek stok ürününün ID'si.</param>
        /// <returns>Silme işlemi başarılıysa true, değilse false.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var existingProduct = await _context.StokProducts.FindAsync(id);
            if (existingProduct == null) return false;

            _context.StokProducts.Remove(existingProduct);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}