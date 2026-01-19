using StainlessMarketApi.Entities;

public interface IFasonService
{
    Task<IEnumerable<FasonProductEntity>> GetAllStokAsync();
    Task<FasonProductEntity?> GetByIdAsync(int id);
    Task<FasonProductEntity> CreateAsync(FasonProductEntity fasonProduct);
    Task<FasonProductEntity?> UpdateAsync(int id, FasonProductEntity updatedProduct);
    Task<bool> DeleteAsync(int id);
}