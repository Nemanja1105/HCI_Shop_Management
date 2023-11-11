using System;
using System.Collections.Generic;

namespace HCI_Projekat_1.Models;

public partial class Procurementitem
{
    public Procurementitem() { }

    public Procurementitem(decimal price, decimal? quantity, int procurementId, int productId)
    {
        Price = price;
        Quantity = quantity;
        ProcurementId = procurementId;
        ProductId = productId;
    }

    public Procurementitem(int id, decimal price, decimal? quantity, int procurementId, int productId)
    {
        Id = id;
        Price = price;
        Quantity = quantity;
        ProcurementId = procurementId;
        ProductId = productId;
    }

    public int Id { get; set; }

    public decimal Price { get; set; }

    public decimal? Quantity { get; set; }

    public int ProcurementId { get; set; }

    public int ProductId { get; set; }

    public virtual Procurement Procurement { get; set; }

    public virtual Product Product { get; set; }
}
