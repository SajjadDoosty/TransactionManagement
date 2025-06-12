using Application.Aggregates.Categories;
using Application.Aggregates.Categories.ViewModels;
using Application.Aggregates.Users;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Pages.Admin.Categories;

public class CreateModel
    (CategoryApplication categoryApplication,
    UserApplication userApplication) : BasePageModel
{
    [BindProperty]
    public CreateViewModel ViewModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(Guid? userId)
    {
        if (!userId.HasValue)
        {
            return RedirectToPage("../Users/Index");
        }

        var result =
            await userApplication.GetAsync(userId.Value);

        var user = result.Data!;

        if (!result.IsSuccessful || user == null)
        {
            return RedirectToPage("Index");
        }

        ViewModel.UserId = user.Id;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var result =
            await categoryApplication.CreateAsync(ViewModel);

        if (result.IsSuccessful == false)
        {
            AddPageError(result.ErrorMessage!.Message);

            return Page();
        }

        var message =
            string.Format(Resources.Messages.Successes.Created, Resources.DataDictionary.Category);

        AddToastSuccess(message);

        return RedirectToPage("Index",
            new { userId = ViewModel.UserId });
    }
}
