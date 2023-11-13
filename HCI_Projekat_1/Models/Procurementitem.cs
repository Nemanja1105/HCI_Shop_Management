using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HCI_Projekat_1.Models;

public partial class Procurementitem : INotifyPropertyChanged
{
    public Procurementitem() { }

    public Procurementitem(decimal price, decimal quantity, int procurementId, int productId)
    {
        Price = price;
        Quantity = quantity;
        ProcurementId = procurementId;
        ProductId = productId;
    }

    public Procurementitem(int id, decimal price, decimal quantity, int procurementId, int productId)
    {
        Id = id;
        Price = price;
        Quantity = quantity;
        ProcurementId = procurementId;
        ProductId = productId;
    }

    private decimal quantity;

    public int Id { get; set; }

    public decimal Price { get; set; }

    public decimal Quantity { get { return this.quantity; } set { this.quantity = value; OnPropertyChanged(); } }

    public decimal TotalPrice { get { return Price * Quantity; } }

    public int ProcurementId { get; set; }

    public int ProductId { get; set; }

    public virtual Procurement Procurement { get; set; }

    public virtual Product Product { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
