using System;
using System.Collections.Generic;

namespace HCI_Projekat_1.Models;

public partial class Bill { 

    public Bill() { }
    public Bill(int id, DateTime dateOfIssue, bool paymentType, decimal totalPrice, int employeeId)
    {
        Id = id;
        DateOfIssue = dateOfIssue;
        PaymentType = paymentType;
        TotalPrice = totalPrice;
        EmployeeId = employeeId;
    }

    public int Id { get; set; }

    public DateTime DateOfIssue { get; set; }

    public bool PaymentType { get; set; }

    public decimal TotalPrice { get; set; }

    public bool isCanceled { get { return Canceledbill.Count > 0; } }

    public int EmployeeId { get; set; }

    public virtual ICollection<Billitem> Billitem { get; set; } = new List<Billitem>();

    public virtual ICollection<Canceledbill> Canceledbill { get; set; } = new List<Canceledbill>();

    public virtual Employee Employee { get; set; }
}
