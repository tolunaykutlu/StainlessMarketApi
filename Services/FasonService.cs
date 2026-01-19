using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using StainlessMarketApi.Data;
using StainlessMarketApi.Entities;

namespace StainlessMarketApi.Services
{
    public class FasonService : IFasonService
    {
        private readonly AppDbContext _context;

        public FasonService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<FasonProductEntity> CreateAsync(FasonProductEntity fasonProduct)
        {
            _context.FasonProducts.Add(fasonProduct);
            await _context.SaveChangesAsync();
            return fasonProduct;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var fasonToDelete = _context.FasonProducts.Find(id);
            if (fasonToDelete == null)
            {
                return false;
            }
            _context.FasonProducts.Remove(fasonToDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<FasonProductEntity>> GetAllStokAsync()
        {
            return await _context.FasonProducts.ToListAsync();
        }

        public async Task<FasonProductEntity?> GetByIdAsync(int id)
        {
            return await _context.FasonProducts.FindAsync(id);

        }

        public async Task<FasonProductEntity?> UpdateAsync(int id, FasonProductEntity updatedProduct)
        {
            var existingProduct = await _context.FasonProducts.FindAsync(id);
            if (existingProduct == null) return null;

            existingProduct.CompanyName = updatedProduct.CompanyName;
            existingProduct.ProcessType = updatedProduct.ProcessType;
            existingProduct.Thickness = updatedProduct.Thickness;
            existingProduct.Width = updatedProduct.Width;
            existingProduct.Length = updatedProduct.Length;
            existingProduct.Quantity = updatedProduct.Quantity;

            await _context.SaveChangesAsync();
            return existingProduct;
        }
    }
}