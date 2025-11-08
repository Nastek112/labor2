using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2.Documents
{
    /// <summary>
    /// Маленький класс для позиции в накладной.
    /// </summary>
    public class LineItem
    {
        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total => Quantity * UnitPrice;

        public LineItem(string description, decimal quantity, decimal unitPrice)
        {
            Description = description ?? string.Empty;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public override string ToString()
        {
            return $"{Description}: {Quantity} × {UnitPrice:C} = {Total:C}";
        }
    }

    /// <summary>
    /// Накладная (Invoice) — содержит список позиций.
    /// </summary>
    public class Invoice : Document
    {
        public List<LineItem> Items { get; } = new List<LineItem>();

        public Invoice(string number, DateTime issueDate, Organization from = null, Organization to = null)
            : base(number, issueDate, from, to) { }

        public override decimal GetTotal()
        {
            return Items.Sum(i => i.Total);
        }

        public override void Print()
        {
            // Демонстрационная печать накладной
            Console.WriteLine(ToString());
            Console.WriteLine("Items:");
            foreach (var it in Items)
            {
                Console.WriteLine("  " + it.ToString());
            }
            Console.WriteLine($"TOTAL: {GetTotal():C}\n");
        }

        public override string ToString()
        {
            return base.ToString() + $" | Invoice.Total = {GetTotal():C}";
        }
    }

    /// <summary>
    /// Квитанция (Receipt) — простая сущность с суммой оплаты.
    /// </summary>
    public class Receipt : Document
    {
        public decimal PaidAmount { get; set; }

        public Receipt(string number, DateTime issueDate, decimal paidAmount, Organization from = null, Organization to = null)
            : base(number, issueDate, from, to)
        {
            PaidAmount = paidAmount;
        }

        public override decimal GetTotal() => PaidAmount;

        public override void Print()
        {
            Console.WriteLine(ToString());
            Console.WriteLine($"Paid: {PaidAmount:C}\n");
        }

        public override string ToString()
        {
            return base.ToString() + $" | Receipt.Paid = {PaidAmount:C}";
        }
    }

    /// <summary>
    /// Чек — сделаем 'бесплодным' (sealed).
    /// </summary>
    public sealed class Check : Document
    {
        public decimal Amount { get; set; }
        public string Cashier { get; set; }

        public Check(string number, DateTime issueDate, decimal amount, string cashier = "", Organization from = null, Organization to = null)
            : base(number, issueDate, from, to)
        {
            Amount = amount;
            Cashier = cashier ?? string.Empty;
        }

        public override decimal GetTotal() => Amount;

        public override void Print()
        {
            Console.WriteLine(ToString());
            Console.WriteLine($"Amount: {Amount:C} | Cashier: {Cashier}\n");
        }

        public override string ToString()
        {
            return base.ToString() + $" | Check.Amount = {Amount:C} | Cashier: {Cashier}";
        }
    }
}
