public class ExpenseReport
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ReceiptPath { get; set; } = string.Empty;
    public string SubmittedBy { get; set; } = string.Empty;
    public DateTime SubmittedDate { get; set; } = DateTime.UtcNow;
    public ExpenseStatus Status { get; set; } = ExpenseStatus.Pending;
    public string? ApprovalComments { get; set; }
    public string? ApprovedBy { get; set; }
    public DateTime? ApprovedDate { get; set; }
}

public enum ExpenseStatus
{
    Pending,
    Approved,
    Rejected
}