using Application.Aggregates.Categories;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Pages.Admin.Categories;

public class DeleteModel
    (CategoryApplication categoryApplication) : BasePageModel
{
    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            return RedirectToPage("Index");
        }

        var result =
            await categoryApplication.DeleteAsync(id);

        if (result.IsSuccessful == false)
        {
            AddToastError(result.ErrorMessage!.Message);

            return RedirectToPage("Index");
        }

        var message =
            string.Format(Resources.Messages.Successes.Deleted, Resources.DataDictionary.Categories);

        AddToastSuccess(message);

        return RedirectToPage("Index",
            new { userId = result.Data });
    }
}
