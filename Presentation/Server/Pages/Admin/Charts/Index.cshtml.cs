using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;
using Application.Aggregates.Transactions;
using Domain.Aggregates.Transactions.Enums;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Server.Pages.Admin.Charts;

public class IndexModel
    (TransactionApplication transactionApplication) : BasePageModel
{
    public string LabelsJson { get; set; } = string.Empty;
    public string DatasetsJson { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            return RedirectToPage("../Users/Index");
        }

        var result =
            await transactionApplication.GetAllGroupByCategoryAsync(userId);

        var grouped = result.Data!;

        var labels = grouped.Select(r => r.Category.Title).ToList();
        var incomeData = grouped.Select(r => r.TotalIncome).ToList();
        var expenseData = grouped.Select(r => r.TotalExpense).ToList();
        var total = grouped.Select(r => r.Total).ToList();

        var datasets = new List<object>
        {
            new {
                label = Resources.DataDictionary.Income,
                data = incomeData,
                backgroundColor = "rgba(75, 192, 192, 0.5)",
                borderColor = "rgba(75, 192, 192, 1)",
                borderWidth = 1
            },
            new {
                label = Resources.DataDictionary.Expense,
                data = expenseData,
                backgroundColor = "rgba(255, 99, 132, 0.5)",
                borderColor = "rgba(255, 99, 132, 1)",
                borderWidth = 1
            },
            new {
                label = "مانده",
                data = total,
                backgroundColor = "rgba(0, 0, 0, 0.5)",
                borderColor = "rgba(0, 0, 0, 1)",
                borderWidth = 1
            }
        };

        LabelsJson = JsonSerializer.Serialize(labels);
        DatasetsJson = JsonSerializer.Serialize(datasets);

        return Page();
    }

    public async Task<IActionResult> OnGetDailyAsync(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            return RedirectToPage("../Users/Index");
        }

        var transactions = (await transactionApplication.GetAllAsync(userId)).Data;

        var grouped = transactions?.Transactions
            .GroupBy(x => x.InsertDateTimes.Date)
            .OrderBy(x => x.Key.Day);

        var labels = 
            grouped?
            .Select(r => r.Key.ToString("yyyy/MM/dd", new CultureInfo("fa-IR")))
            .TakeLast(30)
            .ToList();

        var incomeData = 
            grouped?
            .Select(g => g.Where(x => x.Type == TransactionType.INCOME).Sum(x => x.Amount))
            .ToList();

        var expenseData = 
            grouped?
            .Select(g => g.Where(x => x.Type == TransactionType.EXPENSE).Sum(x => x.Amount))
            .ToList();

        var datasets = new List<object>
        {
            new {
                label = Resources.DataDictionary.Income,
                data = incomeData,
                backgroundColor = "rgba(75, 192, 192, 0.5)",
                borderColor = "rgba(75, 192, 192, 1)",
                borderWidth = 1
            },
            new {
                label = Resources.DataDictionary.Expense,
                data = expenseData,
                backgroundColor = "rgba(255, 99, 132, 0.5)",
                borderColor = "rgba(255, 99, 132, 1)",
                borderWidth = 1
            }
        };

        LabelsJson = JsonSerializer.Serialize(labels);
        DatasetsJson = JsonSerializer.Serialize(datasets);

        return Page();
    }

    public async Task<IActionResult> OnGetMonthlyAsync(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            return RedirectToPage("../Users/Index");
        }

        var transactions = (await transactionApplication.GetAllAsync(userId)).Data;

        var grouped = transactions?.Transactions
            .GroupBy(t => new { t.InsertDateTimes.Year, t.InsertDateTimes.Month })
            .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
            .TakeLast(12)
            .ToList();

        var labels = grouped
            .Select(g => new DateTime(g.Key.Year, g.Key.Month, 1).ToString("yyyy/MM", new CultureInfo("fa-IR")))
            .ToList();

        var incomeData =
            grouped?
            .Select(g => g.Where(x => x.Type == TransactionType.INCOME).Sum(x => x.Amount))
            .ToList();

        var expenseData =
            grouped?
            .Select(g => g.Where(x => x.Type == TransactionType.EXPENSE).Sum(x => x.Amount))
            .ToList();

        var datasets = new List<object>
        {
            new {
                label = Resources.DataDictionary.Income,
                data = incomeData,
                backgroundColor = "rgba(75, 192, 192, 0.5)",
                borderColor = "rgba(75, 192, 192, 1)",
                borderWidth = 1
            },
            new {
                label = Resources.DataDictionary.Expense,
                data = expenseData,
                backgroundColor = "rgba(255, 99, 132, 0.5)",
                borderColor = "rgba(255, 99, 132, 1)",
                borderWidth = 1
            }
        };

        LabelsJson = JsonSerializer.Serialize(labels);
        DatasetsJson = JsonSerializer.Serialize(datasets);

        return Page();
    }

}
