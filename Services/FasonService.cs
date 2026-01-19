using System.Net.Http.Headers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StainlessMarketApi.Data;
using StainlessMarketApi.Dtos;
using StainlessMarketApi.Entities;

namespace StainlessMarketApi.Services
{
    public class FasonService : IFasonService
    {
        private readonly AppDbContext _context;

        private readonly IMapper _mapper;

        public FasonService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<FasonProductDto> CreateAsync(FasonProductDto fasonProduct)
        {
            var product = _mapper.Map<FasonProductEntity>(fasonProduct);
            await _context.SaveChangesAsync();
            return _mapper.Map<FasonProductDto>(product);
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

        public async Task<IEnumerable<FasonProductDto>> GetAllFasonAsync()
        {
            var products = await _context.FasonProducts.ToListAsync();
            return _mapper.Map<IEnumerable<FasonProductDto>>(products);
        }


        public async Task<FasonProductDto> GetByIdAsync(int id)
        {
            var fason = await _context.FasonProducts.FindAsync(id);
            return _mapper.Map<FasonProductDto>(fason);

        }

        public async Task<FasonProductDto?> UpdateAsync(int id, FasonProductEntity updatedProduct)
        {
            var existingProduct = await _context.FasonProducts.FindAsync(id);
            if (existingProduct == null) return null;

            existingProduct.CompanyName = updatedProduct.CompanyName;
            existingProduct.Quality = updatedProduct.Quality;
            existingProduct.SurfaceFinish = updatedProduct.SurfaceFinish;
            existingProduct.ProcessType = updatedProduct.ProcessType;
            existingProduct.Thickness = updatedProduct.Thickness;
            existingProduct.Width = updatedProduct.Width;
            existingProduct.Length = updatedProduct.Length;
            existingProduct.Quantity = updatedProduct.Quantity;

            await _context.SaveChangesAsync();

            return _mapper.Map<FasonProductDto>(existingProduct);
        }

        public Task<FasonProductDto?> UpdateAsync(int id, FasonProductDto updatedProduct)
        {
            throw new NotImplementedException();
        }
    }
}