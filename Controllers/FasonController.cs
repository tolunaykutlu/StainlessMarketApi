using Microsoft.AspNetCore.Mvc;
using StainlessMarketApi.Dtos;
using StainlessMarketApi.Entities;
using StainlessMarketApi.Services;

[ApiController]
[Route("[controller]")]
public class FasonController : ControllerBase
{
    private readonly IFasonService _fasonService;

    public FasonController(IFasonService fasonService)
    {
        _fasonService = fasonService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _fasonService.GetAllFasonAsync();
        return Ok(data); // Unutmayın, parantez içi dolu olmalı :)
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await _fasonService.GetByIdAsync(id);
        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] FasonProductDto product)
    {
        var result = await _fasonService.CreateAsync(product);
        return Ok(result);
    }
    [HttpPut("id")]
    public async Task<IActionResult> Update(int id, [FromBody] FasonProductDto updatedProduct)
    {
        var result = await _fasonService.UpdateAsync(id, updatedProduct);
        if (result == null) return NotFound("Böyle bir ürün yok");
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _fasonService.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}