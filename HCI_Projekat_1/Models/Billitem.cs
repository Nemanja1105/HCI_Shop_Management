using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HCI_Projekat_1.Models;

public partial class Billitem:INotifyPropertyChanged
{
    public Billitem()
    {

    }
    public Billitem(decimal quantity, decimal price, int productId, int billId)
    {
        Quantity = quantity;
        Price = price;
        ProductId = productId;
        BillId = billId;
    }

    public Billitem(int id, decimal quantity, decimal price, int productId, int billId)
    {
        Id = id;
        Quantity = quantity;
        Price = price;
        ProductId = productId;
        BillId = billId;
    }
    private decimal quantity;

    public int Id { get; set; }

    public decimal Quantity { get { return this.quantity; } set { this.quantity = value; OnPropertyChanged(); } }

    public decimal Price { get; set; }

    public decimal TotalPrice { get { return Price * Quantity; } }

    public int ProductId { get; set; }

    public int BillId { get; set; }

    public virtual Bill Bill { get; set; }

    public virtual Product Product { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
