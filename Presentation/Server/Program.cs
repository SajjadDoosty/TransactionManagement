using System.Globalization;
using Application.Aggregates.Categories;
using Application.Aggregates.Transactions;
using Application.Aggregates.Users;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Server;

public class Program
{
    public static void Main()
    {
        var builder =
            WebApplication.CreateBuilder();

        var services =
            builder.Services;

        services.AddRazorPages();

        services.AddScoped<UnitOfWork>();
        services.AddScoped<UserApplication>();
        services.AddScoped<CategoryApplication>();
        services.AddScoped<TransactionApplication>();

        services.AddDbContext<DatabaseContext>
            (optionsAction: options =>
            {
                options
                    .UseSqlite(connectionString: "Data Source=Database.db");
            });



        var app = builder.Build();

        var cultureInfo =
            new CultureInfo("fa-IR");

        Thread.CurrentThread.CurrentCulture = cultureInfo;
        Thread.CurrentThread.CurrentUICulture = cultureInfo;

        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("fa-IR"),
        });

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}