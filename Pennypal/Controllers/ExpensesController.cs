using Pennypal.Persistence.Data;

namespace Pennypal.Controllers;

public class ExpensesController : BaseController
{
    private readonly AppDbContext _context;
    public ExpensesController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Expense>>> GetExpenses()
    {
        return await _context.Expenses.Include(x => x.Category).ToListAsync();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Expense>> GetExpense(Guid id)
    {
        var expense = await _context.Expenses.Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (expense == null)
        {
            return NotFound();
        }
        return expense;
    }
    
    [HttpPost]
    public async Task<ActionResult<Expense>> CreateExpense(Expense expense)
    {
        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(Guid id, Expense expense)
    {
        if (id != expense.Id)
        {
            return BadRequest();
        }
        _context.Entry(expense).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ExpenseExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(Guid id)
    {
        var expense = await _context.Expenses.FindAsync(id);
        if (expense == null)
        {
            return NotFound();
        }
        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    private bool ExpenseExists(Guid id)
    {
        return _context.Expenses.Any(e => e.Id == id);
    }
}