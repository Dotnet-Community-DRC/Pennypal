namespace Pennypal.DTOs;
public class CreateExpenseDto
{
    [Required] public string Name { get; set; }

    [Required] public decimal Amount { get; set; }

    [Required] public DateTime Date { get; set; }

    public string Description { get; set; }
    [Required] public string CategoryName { get; set; }

    [Required] public string CurrencyCode { get; set; }
    
    [Required] public string StatusCode { get; set; }
}