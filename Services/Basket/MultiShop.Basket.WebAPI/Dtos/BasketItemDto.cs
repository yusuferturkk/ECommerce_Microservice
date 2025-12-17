namespace MultiShop.Basket.WebAPI.Dtos
{
    public class BasketItemDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Dictionary<string, string> SelectedAttributes { get; set; } = new Dictionary<string, string>();
    }
}
