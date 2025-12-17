using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderTime { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Введіть ім'я")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введіть прізвище")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Введіть адресу доставки")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Введіть номер телефону")]
        public string PhoneNumber { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        [NotMapped]
        public decimal TotalAmount => OrderItems?.Sum(x => x.Price * x.Quantity) ?? 0;
    }
}