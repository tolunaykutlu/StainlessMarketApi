using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StainlessMarketApi.Data;
using StainlessMarketApi.Entities;

[ApiController]
[Route("[controller]")]

public class FasonController : ControllerBase
{
    private readonly AppDbContext _context;
    public FasonController(AppDbContext context)//constructor
    {
        _context = context;
    }

    [HttpGet]

    public async Task<IActionResult> GetAllFasonProcesses()
    {
        var fasonProducts = await _context.FasonProducts.ToListAsync();
        return Ok(fasonProducts);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFasonProcess([FromBody] FasonProductEntity fasonProductEntity)
    {
        _context.FasonProducts.Add(fasonProductEntity);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAllFasonProcesses), new { id = fasonProductEntity.Id }, fasonProductEntity);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFasonProcess(int id, [FromBody] FasonProductEntity updatedFason)
    {
        var existingFason = await _context.FasonProducts.FindAsync(id);
        if (existingFason == null)
        {
            return NotFound();
        }

        existingFason.CompanyName = updatedFason.CompanyName;
        existingFason.ProcessType = updatedFason.ProcessType;

        await _context.SaveChangesAsync();
        return Ok("ürün güncellendi");
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFasonProcess(int id)
    {
        var fasonProcessToDel = await _context.FasonProducts.FindAsync(id);
        if (fasonProcessToDel == null)
        {
            return NotFound("ürün bulunamadı");

        }
        _context.FasonProducts.Remove(fasonProcessToDel);
        await _context.SaveChangesAsync();
        return Ok("ürün silindi");
    }
}