namespace Pennypal.DTOs;

public class UpdateExpenseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }
    public string CurrencyCode { get; set; }
    public string StatusCode { get; set; }
}