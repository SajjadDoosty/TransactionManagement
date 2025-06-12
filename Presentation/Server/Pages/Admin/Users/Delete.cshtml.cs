using Application.Aggregates.Users;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Pages.Admin.Users;

public class DeleteModel
    (UserApplication userApplication) : BasePageModel
{
    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            return RedirectToPage("Index");
        }

        var result = 
            await userApplication.DeleteAsync(id);

        if (result.IsSuccessful == false)
        {
            AddToastError(result.ErrorMessage!.Message);

            return RedirectToPage("Index");
        }

        var message =
            string.Format(Resources.Messages.Successes.Deleted, Resources.DataDictionary.User);

        AddToastSuccess(message);

        return RedirectToPage("Index");
    }
}
