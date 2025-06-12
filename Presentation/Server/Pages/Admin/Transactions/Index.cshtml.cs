using Application.Aggregates.Categories;
using Application.Aggregates.Transactions;
using Application.Aggregates.Transactions.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Pages.Admin.Transactions;

public class IndexModel
    (TransactionApplication transactionApplication) : BasePageModel
{
    [BindProperty]
    public CategoryResultViewModel ViewModel { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? userId, Guid? categoryId)
    {
        if (!categoryId.HasValue || !userId.HasValue)
        {
            return RedirectToPage("../Categories/Index",
                new { userId = userId });
        }

        //var result =
        //    await transactionApplication.GetByCategoryIdAsync(categoryId.Value);

        var result =
            await transactionApplication.GetResultByCategoryIdAsync(categoryId.Value);

        ViewModel = result.Data!;

        return Page();
    }
}
