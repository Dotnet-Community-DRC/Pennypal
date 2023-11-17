namespace Pennypal.Entities;

public class Expense
{ 
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public Currency? Currency { get; set; }
    public Status? Status { get; set; }
}