using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace HCI_Projekat_1.Models;

public partial class Product:IEquatable<Product>
{
    public Product() { }

    public Product(string name, decimal quantity, string barkod, int categoryId, string unitOfMeasure, decimal purchasePrice, decimal sellingPrice)
    {
        Name = name;
        Quantity = quantity;
        Barkod = barkod;
        CategoryId = categoryId;
        UnitOfMeasure = unitOfMeasure;
        PurchasePrice = purchasePrice;
        SellingPrice = sellingPrice;
    }

    public Product(int id, string name, decimal quantity, string barkod, int categoryId, string unitOfMeasure, decimal purchasePrice, decimal sellingPrice)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
        Barkod = barkod;
        CategoryId = categoryId;
        UnitOfMeasure = unitOfMeasure;
        PurchasePrice = purchasePrice;
        SellingPrice = sellingPrice;
    }

    public Product(Product product)
    {
        Id = product.Id;
        Name =product.Name;
        Quantity = product.Quantity;
        Barkod = product.Barkod;
        CategoryId = product.CategoryId;
        UnitOfMeasure = product.UnitOfMeasure;
        PurchasePrice = product.PurchasePrice;
        SellingPrice = product.SellingPrice;
        Billitem=product.Billitem;
        Category=product.Category;
        Procurementitem = product.Procurementitem;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Quantity { get; set; }

    public string Barkod { get; set; }

    public int CategoryId { get; set; }

    public string UnitOfMeasure { get; set; }

    public decimal PurchasePrice { get; set; }

    public decimal SellingPrice { get; set; }

    public virtual ICollection<Billitem> Billitem { get; set; } = new List<Billitem>();

    public virtual Category Category { get; set; }

    public virtual ICollection<Procurementitem> Procurementitem { get; set; } = new List<Procurementitem>();

    public bool Equals(Product other)
    {
        if (other == null)
            return false;
        return this.Id == other.Id;
    }

    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }
}
