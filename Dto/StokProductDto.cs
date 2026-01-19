
namespace StainlessMarketApi.Dtos
{
    public class StokProductDto
    {
        public int Id { get; set; }
        public string Quality { get; set; } = string.Empty;
        public decimal Thickness { get; set; }
        public decimal Width { get; set; }
        public decimal Length { get; set; }
        public string Quantity { get; set; } = string.Empty;
        public DateTime EntryDate { get; set; }
    }

    // StokProductDto: Stok ürünlerinin temel bilgilerini (kalite, ölçüler, miktar, giriş tarihi) 
    // taşımak için kullanılan veri transfer nesnesi.
}