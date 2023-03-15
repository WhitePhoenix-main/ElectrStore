using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectrStore
{
    [Table("WishListRecord")]
    public class WishListRecord
    {
        [Key] public string Id { get; set; } = "";
        public string ProductId { get; set; } = "";
        public string OrderId { get; set; } = "";

        public string ProductName { get; set; } = "";

        public int Price { get; set; }
    }
}