using System.ComponentModel.DataAnnotations; 

namespace OnlineShop.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Будь ласка, введіть назву товару")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Назва має бути від 3 до 100 символів")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Опис не може перевищувати 500 символів")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Вкажіть категорію")]
        public string Category { get; set; }

        [Range(0.01, 100000, ErrorMessage = "Ціна повинна бути більше 0")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Кількість не може бути від'ємною")]
        public int Stock { get; set; }

        public string? ImageUrl { get; set; }
    }
}