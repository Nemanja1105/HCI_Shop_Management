﻿using System;
using System.Collections.Generic;

namespace HCI_Projekat_1.Models;

public partial class Supplier:IEquatable<Supplier>
{
    public Supplier() { }

    public Supplier(string name, string phoneNumber, string address)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Address = address;
    }

    public Supplier(int id, string name, string phoneNumber, string address)
    {
        Id = id;
        Name = name;
        PhoneNumber = phoneNumber;
        Address = address;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public string PhoneNumber { get; set; }

    public string Address { get; set; }

    public virtual ICollection<Procurement> Procurement { get; set; } = new List<Procurement>();

    public bool Equals(Supplier other)
    {
        if (other == null) return false;
        return Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
