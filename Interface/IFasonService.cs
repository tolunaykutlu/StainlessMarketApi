using StainlessMarketApi.Dtos;
using StainlessMarketApi.Entities;

public interface IFasonService
{
    Task<IEnumerable<FasonProductDto>> GetAllFasonAsync();
    Task<FasonProductDto> GetByIdAsync(int id);
    Task<FasonProductDto> CreateAsync(FasonProductDto fasonProduct);
    Task<FasonProductDto?> UpdateAsync(int id, FasonProductDto updatedProduct);
    Task<bool> DeleteAsync(int id);
}