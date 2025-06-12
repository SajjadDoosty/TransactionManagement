using Application.Aggregates.Categories;
using Application.Aggregates.Transactions;
using Application.Aggregates.Transactions.ViewModels;
using Application.Aggregates.Users;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Pages.Admin.Transactions;

public class CreateModel
    (TransactionApplication transactionApplication,
    CategoryApplication categoryApplication,
    UserApplication userApplication) : BasePageModel
{
    [BindProperty]
    public CreateViewModel ViewModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(Guid? userId, Guid? categoryId)
    {
        if (!userId.HasValue || !categoryId.HasValue)
        {
            return RedirectToPage("../Users/Index");
        }

        var categoryResult =
            await categoryApplication.GetAsync(categoryId.Value);
        var userResult =
            await userApplication.GetAsync(userId.Value);

        var category = categoryResult.Data!;
        var user = userResult.Data!;

        if (!categoryResult.IsSuccessful
            || !userResult.IsSuccessful
            || category == null
            || user == null)
        {
            return RedirectToPage("../Users/Index");
        }

        ViewModel.UserId = user.Id;
        ViewModel.CategoryId = category.Id;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var result =
            await transactionApplication.CreateAsync(ViewModel);

        if (result.IsSuccessful == false)
        {
            AddPageError(result.ErrorMessage!.Message);

            return Page();
        }

        var message =
            string.Format(Resources.Messages.Successes.Created, Resources.DataDictionary.Transaction);

        AddToastSuccess(message);

        return RedirectToPage("Index",
            new { userId = ViewModel.UserId, categoryId = ViewModel.CategoryId });
    }
}
