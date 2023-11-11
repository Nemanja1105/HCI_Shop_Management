using System;
using System.Collections.Generic;

namespace HCI_Projekat_1.Models;

public partial class Procurement
{
    public Procurement() { }

    public Procurement(DateTime dateOfAcquisition, int totalPrice, int supplierId, int employeeId)
    {
        DateOfAcquisition = dateOfAcquisition;
        TotalPrice = totalPrice;
        SupplierId = supplierId;
        EmployeeId = employeeId;
    }

    public Procurement(int id, DateTime dateOfAcquisition, int totalPrice, int supplierId, int employeeId)
    {
        Id = id;
        DateOfAcquisition = dateOfAcquisition;
        TotalPrice = totalPrice;
        SupplierId = supplierId;
        EmployeeId = employeeId;
    }

    public int Id { get; set; }

    public DateTime DateOfAcquisition { get; set; }

    public int TotalPrice { get; set; }

    public int SupplierId { get; set; }

    public int EmployeeId { get; set; }

    public virtual Employee Employee { get; set; }

    public virtual ICollection<Procurementitem> Procurementitem { get; set; } = new List<Procurementitem>();

    public virtual Supplier Supplier { get; set; }
}
