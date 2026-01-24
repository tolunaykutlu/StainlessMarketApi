using System.Text.Json.Serialization;

namespace StainlessMarketApi.Entities;

public abstract class BaseEntity
{
    [JsonPropertyOrder(-100)]
    public int Id { get; set; }
    public string Quality { get; set; } = "430";

    public string SurfaceFinish { get; set; } = "BA";

    public decimal Thickness { get; set; }
    public decimal Width { get; set; }
    public decimal Length { get; set; } = 0;
    public int Quantity { get; set; } = 0;
    public DateTime EntryDate { get; set; }

    protected BaseEntity()
    {
        EntryDate = DateTime.Now;
    }
}