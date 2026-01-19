using Microsoft.AspNetCore.Mvc;
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
        var data = await _fasonService.GetAllStokAsync();
        return Ok(data); // Unutmayın, parantez içi dolu olmalı :)
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] FasonProductEntity product)
    {
        var result = await _fasonService.CreateAsync(product);
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