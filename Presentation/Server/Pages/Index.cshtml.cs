using Application.Aggregates.Categories;
using Application.Aggregates.Transactions;
using Application.Aggregates.Users;
using Application.Aggregates.Users.ViewModels;
using Domain.Aggregates.Transactions.Enums;
using Domain.Aggregates.Users.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Persistence;
namespace Server.Pages;

public class IndexModel
    (UserApplication userApplication,
    CategoryApplication categoryApplication,
    TransactionApplication transactionApplication) : PageModel
{
    public async Task OnGet()
    {
        //var a = await userApplication.CreateAsync(new CreateViewModel()
        //{
        //    Email = "qw@gmail.com",
        //    Password = "Aa@12345678",
        //    Username = "Testtt",
        //    Role = RoleType.USER,
        //});

        //var b = await categoryApplication.CreateAsync(new Application.Aggregates.Categories.ViewModels.CreateViewModel()
        //{
        //    Title = "News",
        //    UserId = Guid.Parse("3A008C58-6BDC-417B-B13C-1F7CDA84C36A"),
        //    IsSystemic = true,
        //});

        //var c = await categoryApplication.GetAllAsync(Guid.Parse("3A008C58-6BDC-417B-B13C-1F7CDA84C36A"));

        ////var d = await transactionApplication.CreateAsync(new Application.Aggregates.Transactions.ViewModels.CreateViewModel()
        ////{
        ////    Amount = 50_000,
        ////    Type = TransactionType.EXPENSE,
        ////    UserId = Guid.Parse("3A008C58-6BDC-417B-B13C-1F7CDA84C36A"),
        ////    CategoryId = Guid.Parse("68495EF3-B2C2-4B9F-AD62-109F2874F306"),
        ////});

        //var e = await transactionApplication.GetAllAsync(Guid.Parse("3A008C58-6BDC-417B-B13C-1F7CDA84C36A"));

        //var f = await transactionApplication.GetAllGroupByCategoryAsync(Guid.Parse("3A008C58-6BDC-417B-B13C-1F7CDA84C36A"));
    }
}
