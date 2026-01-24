using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StainlessMarketApi.Data;
using StainlessMarketApi.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using StainlessMarketApi.Dtos;


[ApiController]
[Route("[controller]")]
[Authorize]
// Stok işlemleri için API controller'ı
public class StokController : ControllerBase
{
    private readonly IStokService _stokService;



    public StokController(IStokService stokService)
    {
        _stokService = stokService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStokAsync()
    {
        var products = await _stokService.GetAllStokAsync();

        return Ok(products);

    }

    [HttpPost]

    public async Task<IActionResult> CreateStokAsync([FromBody] StokProductDto productDto)
    {
        var created = await _stokService.CreateAsync(productDto);
        return Ok(created);
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> UpdateStokAsync(int id, [FromBody] StokProductDto updatedProduct)
    {
        var updated = await _stokService.UpdateAsync(id, updatedProduct);
        if (updated == null) return NotFound("Böyle bir ürün yok");
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStokProduct(int id)
    {
        var succeeded = await _stokService.DeleteAsync(id);
        if (!succeeded) return NotFound();

        return NoContent();
    }

}