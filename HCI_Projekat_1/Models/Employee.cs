using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace HCI_Projekat_1.Models;

public partial class Employee: INotifyPropertyChanged
{
    public Employee() { }

    public Employee(string username, string password, string name, string surname, string jmb, string adresa, string phoneNumber, bool uloga, string theme, string language)
    {
        Username = username;
        Password = password;
        Name = name;
        Surname = surname;
        Jmb = jmb;
        Adresa = adresa;
        PhoneNumber = phoneNumber;
        Uloga = uloga;
        Theme = theme;
        Language = language;
    }

    public Employee(int id, string username, string password, string name, string surname, string jmb, string adresa, string phoneNumber, bool uloga, string theme, string language)
    {
        Id = id;
        Username = username;
        Password = password;
        Name = name;
        Surname = surname;
        Jmb = jmb;
        Adresa = adresa;
        PhoneNumber = phoneNumber;
        Uloga = uloga;
        Theme = theme;
        Language = language;
    }

    public int Id { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string Name { get; set; }

    public string FullName { get { return Name+" "+ Surname; } }

    public string Surname { get; set; }

    public string Jmb { get; set; }

    public string Adresa { get; set; }

    public string PhoneNumber { get; set; }

    public bool Uloga { get; set; }

    public string Theme { get; set; }

    public string Language { get; set; }

    public virtual ICollection<Bill> Bill { get; set; } = new List<Bill>();

    public virtual ICollection<Canceledbill> Canceledbill { get; set; } = new List<Canceledbill>();

    public virtual ICollection<Procurement> Procurement { get; set; } = new List<Procurement>();

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
