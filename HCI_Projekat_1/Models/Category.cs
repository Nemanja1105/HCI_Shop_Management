using System;
using System.Collections.Generic;

namespace HCI_Projekat_1.Models;

public partial class Category : IEquatable<Category>
{
    public Category() { }

    public Category(string name)
    {
        Name = name;
    }

    public Category(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }

    public string Name { get; set; } = "";

    public virtual ICollection<Product> Product { get; set; } = new List<Product>();

    public bool Equals(Category other)
    {
        if(other == null) return false;
        return Id.Equals(other.Id);
    }
    
}
