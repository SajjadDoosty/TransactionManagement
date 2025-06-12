using Application.Aggregates.Categories;
using Application.Aggregates.Transactions;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Pages.Admin.Transactions;

public class DeleteModel
    (TransactionApplication transactionApplication) : BasePageModel
{
    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            return RedirectToPage("Index");
        }

        var result =
            await transactionApplication.DeleteAsync(id);

        if (result.IsSuccessful == false)
        {
            AddToastError(result.ErrorMessage!.Message);

            return RedirectToPage("Index");
        }

        var message =
            string.Format(Resources.Messages.Successes.Deleted, Resources.DataDictionary.Transaction);

        AddToastSuccess(message);

        return RedirectToPage("Index",
            new { userId = result.Data.Item1, categoryId = result.Data.Item2 });
    }
}
