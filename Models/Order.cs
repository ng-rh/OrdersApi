using System;
using System.ComponentModel.DataAnnotations.Schema;

[Table("orders")]   // force lowercase table
public class Order
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
