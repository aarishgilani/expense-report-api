using AutoBogus;

public static class DbSeeder
{
    public static void SeedData(AppDbContext context)
    {
        if (context.ExpenseReports.Any())
            return;

        var expenses = AutoFaker.Generate<ExpenseReport>(20);
        
        context.ExpenseReports.AddRange(expenses);
        context.SaveChanges();
    }
}