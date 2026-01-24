using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StainlessMarketApi.Dtos;
using StainlessMarketApi.Entities;
using StainlessMarketApi.Services;

[ApiController]
[Route("[controller]")]
[Authorize]
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
        var allData = await _fasonService.GetAllFasonAsync();
        return Ok(allData); // Unutmayın, parantez içi dolu olmalı :)
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await _fasonService.GetByIdAsync(id);
        return Ok(data);
    }
    [HttpGet("companyName")]
    public async Task<IActionResult> GetOneCompanyAllWork(string companyName)
    {
        var allWorkForOneCompany = await _fasonService.GetOneCompanyAllWork(companyName);
        return Ok(allWorkForOneCompany);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] FasonProductDto product)
    {
        var createData = await _fasonService.CreateAsync(product);
        return Ok(createData);
    }
    [HttpPut("id")]
    public async Task<IActionResult> Update(int id, [FromBody] FasonProductDto updatedProduct)
    {
        var updatingProduct = await _fasonService.UpdateAsync(id, updatedProduct);
        if (updatingProduct == null) return NotFound("Böyle bir ürün yok");
        return Ok(updatingProduct);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _fasonService.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}