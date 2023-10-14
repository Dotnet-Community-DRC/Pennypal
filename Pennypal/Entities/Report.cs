namespace Pennypal.Entities;

public class Report
{
    public Guid Id { get; set; }
    public string ReportName { get; set; }
    public DateTime ReportDate { get; set; }
    public decimal TotalAmount { get; set; }

    public List<Expense> Expenses { get; set; }
}
