namespace Pennypal.Controllers;

public class ExpenseController : BaseApiController
{
    private readonly AppDbContext _context;
    private readonly ILogger<CategoriesController> _logger;
    private readonly IMapper _mapper;
    public ExpenseController(AppDbContext context, ILogger<CategoriesController> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<ExpenseDto>>> GetExpenses()
    {
        var expenses = await _context.Expenses
            .Include(e => e.Category)
            .ToListAsync();

        return _mapper.Map<List<ExpenseDto>>(expenses);
    }

    [HttpGet("{id}", Name = "GetExpenseById")]
    public async Task<ActionResult<ExpenseDto>> GetExpenseById(Guid id)
    {
        var expense = await _context.Expenses
            .Include(e => e.Category)
            .FirstOrDefaultAsync(e => e.Id == id);
        if (expense is null) 
            return NotFound("We could not find the Expense!");

        var expenseDto = _mapper.Map<ExpenseDto>(expense);
        if (expenseDto is null)
        {
            throw new Exception("There was a problem mapping the expense");
        }

        return expenseDto;
    }

    [HttpPost]
    public async Task<ActionResult<ExpenseDto>> CreateExpense(CreateExpenseDto expenseDto)
    {
        _logger.LogInformation("Adding a new expense:  {@expense}", expenseDto);
        var expense = _mapper.Map<Expense>(expenseDto);
        
        _context.Expenses.Add(expense);

        try
        {
              await _context.Entry(expense).Reference(e => e.Category).LoadAsync();
              await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving expense: {@expense}", expense);
            return BadRequest(new ProblemDetails {Title = "Problem saving Expense"});
        }
        
        return CreatedAtRoute(nameof(GetExpenseById), new { expense.Id }, expense);
    }

    
    // Handle update here

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateExpense(Guid id, UpdateExpenseDto expenseDto)
    {
        _logger.LogInformation("Updating expense: {@expenseDto}", expenseDto);

        var expense = await _context.Expenses
            .Include(e => e.Category)
            .FirstOrDefaultAsync(e => e.Id == id);
        if (expense is null)
            return NotFound("We could not find the expense");

        try
        {
            expense.Category.Description = expenseDto.Category.Description ?? expense.Category.Description;
            expense.Category.Name = expenseDto.Category.Name ?? expense.Category.Name;

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Updating Expense!");
            return BadRequest(new ProblemDetails {Title = "Problem Updating Expense"});
        }

        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteExpense(Guid id)
    {
        _logger.LogInformation("Deleting an expense");
        var expense = await _context.Expenses.FindAsync(id);

        if (expense is null) 
            return NotFound("We could not find the expense to delete!");

        try
        {
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error Deleting Expense!");
            return BadRequest(new ProblemDetails {Title = "Problem deleting Expense"});
        }

        return Ok();
    }
}