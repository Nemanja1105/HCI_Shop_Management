using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HCI_Projekat_1.Models;

public partial class Canceledbill
{
    public Canceledbill()
    {

    }

    public Canceledbill(DateTime date, string reason, int billId, int employeeId)
    {
        Date = date;
        Reason = reason;
        BillId = billId;
        EmployeeId = employeeId;
    }

    public Canceledbill(int id, DateTime date, string reason, int billId, int employeeId)
    {
        Id = id;
        Date = date;
        Reason = reason;
        BillId = billId;
        EmployeeId = employeeId;
    }

    public int Id { get; set; }

    public DateTime Date { get; set; }

    public string Reason { get; set; }

    public int BillId { get; set; }

    public int EmployeeId { get; set; }

    public virtual Bill Bill { get; set; }

    public virtual Employee Employee { get; set; }
}
