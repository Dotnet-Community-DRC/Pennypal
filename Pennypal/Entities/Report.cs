namespace Pennypal.Entities;

public class Report
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }

    public List<Expense> Expenses { get; set; } = new();
}
