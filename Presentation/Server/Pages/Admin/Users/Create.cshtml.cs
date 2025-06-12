using Application.Aggregates.Users;
using Application.Aggregates.Users.ViewModels;
using Domain.Aggregates.Users.Enums;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Pages.Admin.Users;

public class CreateModel
    (UserApplication userApplication) : BasePageModel
{
    [BindProperty]
    public CreateViewModel ViewModel { get; set; } = new();

    public async Task OnGetAsync()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        ViewModel.Role = RoleType.USER;

        var result =
            await userApplication.CreateAsync(ViewModel);

        if (result.IsSuccessful == false)
        {
            AddPageError(result.ErrorMessage!.Message);

            return Page();
        }

        var message =
            string.Format(Resources.Messages.Successes.Created, Resources.DataDictionary.User);

        AddToastSuccess(message);

        return RedirectToPage("Index");
    }
}
