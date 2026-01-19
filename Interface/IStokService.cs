using StainlessMarketApi.Dtos;
using StainlessMarketApi.Entities;

public interface IStokService
{
    Task<IEnumerable<StokProductDto>> GetAllStokAsync();
    Task<StokProductDto?> GetByIdAsync(int id);
    Task<StokProductDto> CreateAsync(StokProductDto productDto);
    Task<StokProductDto?> UpdateAsync(int id, StokProductDto updatedProduct);
    Task<bool> DeleteAsync(int id);
}