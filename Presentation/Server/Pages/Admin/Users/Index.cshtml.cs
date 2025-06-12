using Application.Aggregates.Users;
using Application.Aggregates.Users.ViewModels;
using Infrastructure;

namespace Server.Pages.Admin.Users;

public class IndexModel
    (UserApplication userApplication) : BasePageModel
{
    public List<IndexViewModel> ViewModel { get; set; } = [];

    public async Task OnGetAsync()
    {
        var result =
            await userApplication.GetAllAsync();

        ViewModel = result.Data!;
    }
}
