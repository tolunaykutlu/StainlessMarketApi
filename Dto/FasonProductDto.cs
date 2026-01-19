
using StainlessMarketApi.Entities;

namespace StainlessMarketApi.Dtos
{
    public class FasonProductDto
    {
        public string CompanyName { get; set; } = string.Empty;
        public string ProcessType { get; set; } = "Boy Kesim";
        public string Quality { get; set; } = string.Empty;
        public string SurfaceFinish { get; set; } = "BA"; // birşey girilmeze BA olarak kalacak
        public decimal Thickness { get; set; }
        public decimal Width { get; set; }
        public decimal Length { get; set; }
        public string Quantity { get; set; } = string.Empty;
        public DateTime EntryDate { get; set; }
    }

    // StokProductDto: Stok ürünlerinin temel bilgilerini (kalite, ölçüler, miktar, giriş tarihi) 
    // taşımak için kullanılan veri transfer nesnesi.
}