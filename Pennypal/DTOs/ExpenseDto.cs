namespace Pennypal.DTOs;

public class ExpenseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }

    [JsonIgnore] 
    public Currency Currency { get; set; }

    [JsonIgnore] 
    public Status Status { get; set; }
    public string CurrencyCode => Currency.ToString();
    public string StatusCode => Status.ToString();
}
