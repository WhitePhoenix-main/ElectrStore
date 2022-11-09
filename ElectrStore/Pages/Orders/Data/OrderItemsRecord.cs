using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectrStore
{
    [Table("OrdersItems")]
    public class OrderItemsRecord
    {
        [Key] public string Id { get; set; } = "";
        public string ProductId { get; set; } = "";
        public string OrderId { get; set; } = "";
        //Name, quantity, price
    }
}