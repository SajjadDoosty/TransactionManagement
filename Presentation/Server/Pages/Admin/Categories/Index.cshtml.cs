using Application.Aggregates.Categories;
using Application.Aggregates.Categories.ViewModels;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Server.Pages.Admin.Categories;

public class IndexModel
    (CategoryApplication categoryApplication) : BasePageModel
{
    [BindProperty]
    public List<IndexViewModel> ViewModel { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(Guid? userId)
    {
        if (!userId.HasValue)
        {
            return RedirectToPage("../Users/Index");
        }

        var result =
            await categoryApplication.GetAllAsync(userId.Value);

        ViewModel = result.Data!;

        return Page();
    }
}
