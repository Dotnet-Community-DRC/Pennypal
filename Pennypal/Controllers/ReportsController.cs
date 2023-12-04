using Pennypal.Persistence.Data;

namespace Pennypal.Controllers;

public class ReportsController : BaseController
{
   private readonly AppDbContext _context;

   public ReportsController(AppDbContext context)
   {
      _context = context;
   }
   
   [HttpGet]
    public async Task<ActionResult<List<Report>>> GetReports()
    {
        var reports = await _context.Reports.ToListAsync();
        return Ok(reports);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Report>> GetReport(Guid id)
    {
        var report = await _context.Reports.FindAsync(id);
        if (report == null)
        {
            return NotFound();
        }
        return Ok(report);
    }
}