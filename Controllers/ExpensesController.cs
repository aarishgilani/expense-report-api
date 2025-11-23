using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly AppDbContext _context;

    /**
    * Constructor for ExpensesController
    *
    * @param context The database context for accessing expense reports
    *   injected via dependency injection.
    */
    public ExpensesController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/expenses
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExpenseReport>>> GetExpenses()
    {
        return await _context.ExpenseReports
            .OrderByDescending(e => e.SubmittedDate)
            .ToListAsync();
    }

    // GET: api/expenses/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ExpenseReport>> GetExpense(int id)
    {
        var expense = await _context.ExpenseReports.FindAsync(id);

        if (expense == null)
            return NotFound();

        return expense;
    }

    // POST: api/expenses
    [HttpPost]
    public async Task<ActionResult<ExpenseReport>> SubmitExpense(SubmitExpenseRequest request)
    {
        var expense = new ExpenseReport
        {
            Amount = request.Amount,
            Category = request.Category,
            Description = request.Description,
            SubmittedBy = request.SubmittedBy,
            SubmittedDate = DateTime.UtcNow,
            Status = ExpenseStatus.Pending
        };

        _context.ExpenseReports.Add(expense);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetExpense), new { id = expense.Id }, expense);
    }

    // PUT: api/expenses/5/approve
    [HttpPut("{id}/approve")]
    public async Task<IActionResult> ApproveExpense(int id, ApprovalRequest request)
    {
        var expense = await _context.ExpenseReports.FindAsync(id);

        if (expense == null)
            return NotFound();

        expense.Status = request.Approved ? ExpenseStatus.Approved : ExpenseStatus.Rejected;
        expense.ApprovalComments = request.Comments;
        expense.ApprovedBy = request.ApprovedBy;
        expense.ApprovedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // GET: api/expenses/pending
    [HttpGet("pending")]
    public async Task<ActionResult<IEnumerable<ExpenseReport>>> GetPendingExpenses()
    {
        return await _context.ExpenseReports
            .Where(e => e.Status == ExpenseStatus.Pending)
            .OrderByDescending(e => e.SubmittedDate)
            .ToListAsync();
    }
}

// Request DTOs (Data Transfer Objects)
public record SubmitExpenseRequest(
    decimal Amount, 
    string Category, 
    string Description, 
    string SubmittedBy
);

public record ApprovalRequest(
    bool Approved, 
    string Comments, 
    string ApprovedBy
);